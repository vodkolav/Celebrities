using System;
using System.Net.Http;
using System.Web.UI;

public partial class resetConfirmation : System.Web.UI.UserControl
{
	public string Message {
		get
		{
			if (ViewState["Message"] == null)
				return null;
			return (string)ViewState["Message"];
		}
		set { ViewState["Message"] = value;
			lblMessage.Text = value;
				
		}
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		
	}


	protected void btnOk_Click(object sender, EventArgs e)
	{				
		Message = Send($"reset/{(tbxPin.Text==""? "-1" : tbxPin.Text.Trim() )}");
		Visible = false;
		string script = $"alert(\"{Message}\");";
		ScriptManager.RegisterStartupScript(this, GetType(),
							  "ServerControlScript", script, true);
		//Response.Write($"<script>alert('{Message}');</script>");
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
		HttpClient hc = new HttpClient();
		hc.BaseAddress = new Uri("http://localhost:50382/api/");

		var consumeApi = hc.GetAsync(request);
		//consumeApi.Wait();	

		var readdaata = consumeApi.Result;

		if (readdaata.IsSuccessStatusCode)
		{
			var displayresults = readdaata.Content.ReadAsAsync<string>();
			displayresults.Wait();
			return displayresults.Result;
		}
		else return default(string);

	}

}