
using CelebsAPI.Models;
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
		
		// GET api/<controller>
		public IEnumerable<string> Get()
		{
			
			Random r = new Random();
			string Token = (r.Next(10000,99999)).ToString();
			File.WriteAllText(TokenFile, Token);
			return new string[] { "Resetting a database takes a long time.",
				" If you are sure you want to reset the database, send confirmation code: ", Token,
				"You were warned","If you changed your mind, send -1" };
		}

		// GET api/<controller>/5
		public string Get(int id)
		{
			int Token = int.Parse(File.ReadAllText(TokenFile));
			if (id == Token)
			{
				Stopwatch stopwatch = Stopwatch.StartNew(); //creates and start the instance of Stopwatch
				ICelebsDB DB = new CelebsDB();
				DB.Reset();
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
	}
}