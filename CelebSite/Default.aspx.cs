using CelebsAPI.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		
		HttpClient hc = new HttpClient();
		hc.BaseAddress = new Uri("http://localhost:50382/api/");

		var consumeApi = hc.GetAsync("values");
		consumeApi.Wait();

		IEnumerable<Celebrity> celebs = null;

		var readdaata = consumeApi.Result;


		if (readdaata.IsSuccessStatusCode)
		{
			var displayresults = readdaata.Content.ReadAsAsync<IList<Celebrity>>();
			displayresults.Wait();
			celebs = displayresults.Result;
			CelebsList.DataSource = celebs;
			CelebsList.DataBind();

		}

	}
	
	protected void CelebsList_ItemDataBound(object sender, ListViewItemEventArgs e)
	{
		var a = e.Item.DataItem;
	}
}