using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO;
using OpenScraping.Config;
using System.Text;
using OpenScraping;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JsonFlatFileDataStore;
using CelebsAPI.Models;

namespace CelebsAPI.Controllers
{
	public class ValuesController : ApiController
	{
		string celebsDB = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/celebs.json");

		// GET api/values
		public IEnumerable<Celebrity> Get()
		{
			var store = new DataStore(celebsDB);
			var collection = store.GetCollection<Celebrity>("celebrities");
			return collection.AsQueryable();
		}

		// GET api/values/5
		public Celebrity Get(int id)
		{		
			var store = new DataStore(celebsDB);
			
			var collection = store.GetCollection<Celebrity>("celebrities");

			try
			{
				var wt = collection.AsQueryable().Single( c => c.Id == id);
				return wt;
			}
			catch (InvalidOperationException )
			{
				throw new HttpResponseException(HttpStatusCode.NotFound);
			}
			
		}

		// POST api/values
		public void Post([FromBody]string value)
		{
			throw new HttpResponseException(HttpStatusCode.Forbidden);
		}		

		// PUT api/values/5
		public void Put(int id, [FromBody]string value)
		{
			throw new HttpResponseException(HttpStatusCode.Forbidden);
		}

		// DELETE api/values/5
		public void Delete(int id)
		{			
			var store = new DataStore(celebsDB);
			var collection = store.GetCollection<Celebrity>("celebrities");
			collection.DeleteOne(c => c.Id == id);
		}
	}
}
