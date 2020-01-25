using System;
using System.Net;
using System.Web.UI;

using System.Collections.Generic;
using System.Net.Http;
using System.Web.UI.WebControls;
using CelebsAPI.Models;


namespace CelebsSite
{
	public partial class resetConfirmation : UserControl
	{
		public string Message
		{
			get
			{
				return (string) ViewState["Message"];
			}
			set
			{
				ViewState["Message"] = value;
				lblMessage.Text = value;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
		}


		protected void btnOk_Click(object sender, EventArgs e)
		{
			Message = Send($"reset/{(tbxPin.Text == "" ? "-1" : tbxPin.Text.Trim())}");
			Visible = false;
			var script = $"alert(\"{Message}\");";
			ScriptManager.RegisterStartupScript(this, GetType(),
				"ServerControlScript", script, true);
			Response.Redirect(Request.RawUrl);
		}

		protected void btnCancel_Click(object sender, EventArgs e)
		{
			Send("reset/-1");
			Message = null;
			Visible = false;
		}

		private string Send(string request)
		{
			var hc = new HttpClient();
			hc.BaseAddress = new Uri("http://localhost:50382/api/");

			var consumeApi = hc.GetAsync(request);

			var readdaata = consumeApi.Result;

			if (readdaata.IsSuccessStatusCode)
			{
				var displayresults = readdaata.Content.ReadAsAsync<string>();
				displayresults.Wait();
				return displayresults.Result;
			}

			return default(string);
		}
	}
}