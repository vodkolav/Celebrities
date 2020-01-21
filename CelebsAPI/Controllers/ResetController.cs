using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenScraping;
using OpenScraping.Config;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Web.Hosting;
using System.Web.Http;

namespace CelebsAPI.Controllers
{
	public class ResetController : ApiController
	{
		private readonly string TokenFile = HostingEnvironment.MapPath(@"~/App_Data/token.txt");
		readonly string  jsonConfig = File.ReadAllText(HostingEnvironment.MapPath(@"~/App_Data/CelebConfig.json"));
		// GET api/<controller>
		public IEnumerable<string> Get()
		{
			Random r = new Random();
			string Token = r.Next().ToString();
			File.WriteAllText(TokenFile, Token);
			return new string[] { "Resetting a database takes a long time.",
				" If you are sure you want to reset the database, send a Get request with id: ", Token,
				"You were warned","If you changed your mind, send -1" };
		}

		// GET api/<controller>/5
		public string Get(int id)
		{
			int Token = int.Parse(File.ReadAllText(TokenFile));
			if (id == Token)
			{
				Stopwatch stopwatch = Stopwatch.StartNew(); //creates and start the instance of Stopwatch
				Scrape();
				stopwatch.Stop();
				string soMuch = stopwatch.Elapsed.TotalSeconds.ToString();
				File.WriteAllText(TokenFile, "-1");
				return $"Database has been reset. This took {soMuch} seconds. You happy?";				
			}
			else
			{
				File.WriteAllText(TokenFile, "-1");
				return "DataBase was NOT reset. Wise Choice. ";
			}			
		}

		// POST api/<controller>
		public void Post([FromBody]string value)
		{
			throw new HttpResponseException(HttpStatusCode.Forbidden);
		}

		// PUT api/<controller>/5
		public void Put(int id, [FromBody]string value)
		{
			throw new HttpResponseException(HttpStatusCode.Forbidden);
		}

		// DELETE api/<controller>/5
		public void Delete(int id)
		{
			throw new HttpResponseException(HttpStatusCode.Forbidden);
		}

		private string Scrape( )
		{
			var IMDBhtml = HostingEnvironment.MapPath(@"~/App_Data/IMDB.html");

			var jsonConfig = File.ReadAllText(HostingEnvironment.MapPath(@"~/App_Data/Top100Config.json"));

			var config = StructuredDataConfig.ParseJsonString(jsonConfig);

			var html = DownloadPage(saveTo: IMDBhtml);
			
			var openScraping = new StructuredDataExtractor(config);

			var scrapingResults = openScraping.Extract(html);

			using (WebClient client = new WebClient())
			{
				string imgPath = HostingEnvironment.MapPath(@"~/App_Data/Images/");

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

			var celebs = HostingEnvironment.MapPath(@"~/App_Data/celebs.json");

			string textresult = JsonConvert.SerializeObject(scrapingResults, jss);

			File.WriteAllText(celebs, textresult);
			
			return textresult;

		}

		private JToken ScrapeCeleb(JToken jToken)
		{
			var config = StructuredDataConfig.ParseJsonString(jsonConfig);
			jToken["page"] = "https://www.imdb.com" + jToken["page"];
			var html = DownloadPage(jToken["page"].ToString());
			var openScraping = new StructuredDataExtractor(config);
			var scrapingResults = openScraping.Extract(html);
			return scrapingResults["celebrities"]["birth"];
		}

		private string DownloadPage(string url = "https://www.imdb.com/list/ls052283250/", string saveTo = null)
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

	}
}