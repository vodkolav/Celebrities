using System;
using System.Net.Http;
using System.Web.UI;
using CelebsAPI.Models;

namespace CelebsSite
{
	public partial class CelebSheet : UserControl
	{
		public Celebrity Celeb
		{
			get
			{
				return (Celebrity) ViewState["Celeb"];
			}
			set => ViewState["Celeb"] = value;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			imgFace.ImageUrl = Celeb.Image;
		}

		protected void btnDelete_Click(object sender, EventArgs e)
		{
			var hc = new HttpClient();
			hc.BaseAddress = new Uri("http://localhost:50382/api/");
			var consumeApi = hc.DeleteAsync($"values/{Celeb.Id.ToString()}");
			consumeApi.Wait();
			Response.Redirect(Request.RawUrl);
		}
	}
}