using CelebsAPI.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;

namespace CelebsAPI.Controllers
{
	public class ValuesController : ApiController
	{
		private ICelebsDB DB;

		public ValuesController()
		{
			DB = new CelebsDB();
		}

		// GET api/values
		public IEnumerable<Celebrity> Get()
		{
			return DB.Get();
		}

		// GET api/values/5
		public Celebrity Get(int id)
		{	
			try
			{
				return DB.Get(id);
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
			DB.Delete(id);
		}
	}
}
