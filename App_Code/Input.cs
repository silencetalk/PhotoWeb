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
///Input ��ժҪ˵��
/// </summary>
public class Input
{
	public Input()
	{
		//
		//TODO: �ڴ˴���ӹ��캯���߼�
		//
	}
    public static string Inputadd(string content)
    {
        content = content.Replace("'", " ");
        content = content.Replace("\r\n", "<br/>");
        return content;
    }
    public static string Outputadd(string content)
    {
        content = content.Replace("&nbsp;", "");
        content = content.Replace("<br/>", "\r\n");
        content = content.Replace("<p></p>", "");
        content = content.Replace("</p>", "");
        content = content.Replace("<p>", "");
        content = content.Replace("'", " ");
        return content;
    }
}
