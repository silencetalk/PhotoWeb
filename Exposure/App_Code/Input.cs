using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

/// <summary>
///Input 的摘要说明
/// </summary>
public class Input
{
	public Input()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
    public static string Inputadd(string content)
    {
        content = "&nbsp;&nbsp;&nbsp;&nbsp;" + content;
        content = content.Replace("\r\n", "<br/>&nbsp;&nbsp;&nbsp;&nbsp;");
        return content;
    }
    public static string Outputadd(string content)
    {
        content = content.Replace("&nbsp;", "");
        content = content.Replace("<p></p>", "");
        content = content.Replace("<br/>", "\n");
        return content;
    }
}
