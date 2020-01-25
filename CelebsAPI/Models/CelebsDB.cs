using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenScraping;
using OpenScraping.Config;
using System.Web.Hosting;
using System.IO;
using System.Net;
using JsonFlatFileDataStore;
using System.Threading.Tasks;

namespace CelebsAPI.Models
{
	public interface ICelebsDB
	{
		IEnumerable<Celebrity> Get();
		Celebrity Get(int id);
		void Delete(int id);
		void Scrape();
		Task Reset();
	}

	public  class CelebsDB : ICelebsDB
	{
		private readonly string CelebConfig = File.ReadAllText(HostingEnvironment.MapPath(@"~/App_Data/CelebConfig.json"));
		private readonly string Top100Config = File.ReadAllText(HostingEnvironment.MapPath(@"~/App_Data/Top100Config.json"));
		private readonly string DBfile = HostingEnvironment.MapPath(@"~/App_Data/celebs.json");
		private readonly string IMDBhtml = HostingEnvironment.MapPath(@"~/App_Data/IMDB.html");
		private readonly string imgPath = HostingEnvironment.MapPath(@"~/App_Data/Images/");

		private DataStore store;
		
		public CelebsDB()
		{
			store = new DataStore(DBfile);
		}

		public async Task Reset()
		{
			await Task.Run(()=>Scrape());
		}
		

		public  void Scrape()
		{					
			var config = StructuredDataConfig.ParseJsonString(Top100Config);

			var html = DownloadPage(saveTo: IMDBhtml);

			var openScraping = new StructuredDataExtractor(config);

			var scrapingResults = openScraping.Extract(html);

			using (WebClient client = new WebClient())
			{
				foreach (var celeb in scrapingResults["celebrities"])
				{
					celeb["birth"] = ScrapeCeleb(celeb);
					var wat = celeb["image"].ToString();
					Uri uri = new Uri(wat);
					string fn = Path.GetFileName(uri.LocalPath);
					client.DownloadFile(wat, imgPath + fn);
				}
			}

			JsonSerializerSettings jss = new JsonSerializerSettings
			{
				StringEscapeHandling = StringEscapeHandling.Default
			};

			string textresult = JsonConvert.SerializeObject(scrapingResults, jss);

			File.WriteAllText(DBfile, textresult);

		}

		private  JToken ScrapeCeleb(JToken jToken)
		{
			var config = StructuredDataConfig.ParseJsonString(CelebConfig);
			jToken["page"] = "https://www.imdb.com" + jToken["page"];
			var html = DownloadPage(jToken["page"].ToString());
			var openScraping = new StructuredDataExtractor(config);
			var scrapingResults = openScraping.Extract(html);
			return scrapingResults["celebrities"]["birth"];
		}

		private  string DownloadPage(string url = "https://www.imdb.com/list/ls052283250/", string saveTo = null)
		{
			// Create a request for the URL.   
			WebRequest request = WebRequest.Create(url);

			// Get the response.  
			WebResponse response = request.GetResponse();

			string responseFromServer;

			// Get the stream containing content returned by the server. 
			using (Stream dataStream = response.GetResponseStream())
			{
				// Open the stream using a StreamReader for easy access.  
				StreamReader reader = new StreamReader(dataStream);
				// Read the content.  
				responseFromServer = reader.ReadToEnd();
				// Display the content.  
				if (saveTo != null)
				{
					File.WriteAllText(saveTo, responseFromServer);
				}
			}
			// Close the response.  
			response.Close();
			return responseFromServer;
		}
					
		public IEnumerable<Celebrity> Get()
		{			
			var collection = store.GetCollection<Celebrity>("celebrities");
			return collection.AsQueryable();
		}

		public Celebrity Get(int id)
		{
			var collection = store.GetCollection<Celebrity>("celebrities");			
			var wt = collection.AsQueryable().Single(c => c.Id == id);
			return wt;		
		}	

		public void Delete(int id)
		{			
			var collection = store.GetCollection<Celebrity>("celebrities");
			collection.DeleteOne(c => c.Id == id);
			store.Reload();
		}
	}
}