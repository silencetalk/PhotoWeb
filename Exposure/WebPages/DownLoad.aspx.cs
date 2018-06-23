using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class Signed_DownLoad : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string filepath = Request.QueryString["File"];
        String filename = System.IO.Path.GetFileName(filepath);
	    filename=HttpUtility.UrlEncode(filename);
	    filename=filename.Replace("+","%20");
        filename=filename.Replace("#","%20");
        Response.Clear();
        Response.ContentType = "application/octet-stream";
        Response.AddHeader("Content-Disposition", "attachment; filename=" + filename);
        Response.Flush();
        Response.WriteFile(filepath);
        Response.End(); 
    }
}
