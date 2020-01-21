using CelebsAPI.Models;
using System;
using System.Net.Http;

public partial class CelebSheet : System.Web.UI.UserControl
{
	
	public Celebrity Celeb
	{
		get
		{
			if (ViewState["Celeb"] == null)
				return null;
			return (Celebrity)ViewState["Celeb"];
		}
		set { ViewState["Celeb"] = value; }
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		imgFace.ImageUrl = Celeb.Image;
		//btnDelete.Text = Celeb.Id.ToString();
	}

	

	protected void btnDelete_Click(object sender, EventArgs e)
	{
		
		HttpClient hc = new HttpClient();
		hc.BaseAddress = new Uri("http://localhost:50382/api/");
		var consumeApi = hc.DeleteAsync($"values/{Celeb.Id.ToString()}");
		consumeApi.Wait();
		Response.Redirect(Request.RawUrl);
	}

	protected void Celeb_DataBinding(object sender, EventArgs e)
	{
	}
}