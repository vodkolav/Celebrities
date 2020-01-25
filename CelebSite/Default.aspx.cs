using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.UI;
using CelebsAPI.Models;

namespace CelebsSite
{
	public partial class _Default : Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			var hc = new HttpClient();
			hc.BaseAddress = new Uri("http://localhost:50382/api/");

			var consumeApi = hc.GetAsync("values");
			consumeApi.Wait();

			IEnumerable<Celebrity> celebs = null;

			var readdata = consumeApi.Result;


			if (readdata.IsSuccessStatusCode)
			{
				var displayresults = readdata.Content.ReadAsAsync<IList<Celebrity>>();
				displayresults.Wait();
				celebs = displayresults.Result;
				CelebsList.DataSource = celebs;
				CelebsList.DataBind();
			}
		}
	}
}