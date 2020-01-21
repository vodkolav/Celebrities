using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LiteDB;

namespace CelebsAPI.Models
{
	public class JsonDataBase
	{
		public void comeon()
		{
			// Open database (or create if doesn't exist)
			using (var db = new LiteDatabase(@"C:\Temp\MyData.db"))
			{
				// Get a collection (or create, if doesn't exist)
				var col = db.GetCollection<Celebrity>("celebrities");

				// Create your new customer instance
				var celeb = new Celebrity
				{
					Name = "John Doe"
					//Phones = new string[] { "8000-0000", "9000-0000" },
					//IsActive = true
				};

				// Insert new customer document (Id will be auto-incremented)
				col.Insert(celeb);

				// Update a document inside a collection
				celeb.Name = "Joana Doe";

				col.Update(celeb);

				// Index document using document Name property
				col.EnsureIndex(x => x.Name);

				// Use LINQ to query documents
				var results = col.Find(x => x.Name.StartsWith("Jo"));

				// Let's create an index in phone numbers (using expression). It's a multikey index
				col.EnsureIndex(x => x.Name, "$.Phones[*]");

				// and now we can query phones
				var r = col.FindOne(x => x.Name.Contains("8888-5555"));
			}
		}
	}
}