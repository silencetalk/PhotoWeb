using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
///ExcelManager 的摘要说明
/// </summary>
public class ExcelManager
{
	public ExcelManager()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
    public static string CheckExcelData(string excelpath)
    {
        string result = "NullData";
        string sqlcheck = "Select top 1 [Date] from [EQ Summary$]";
        OleDbConnection conn_Excel = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Jet OLEDB:Database Password=;Extended properties=Excel 5.0;Data Source=" + excelpath);
        OleDbCommand cmd_Excel = new OleDbCommand(sqlcheck, conn_Excel);
        conn_Excel.Open();
        OleDbDataReader rd = cmd_Excel.ExecuteReader();
        if (rd.Read())
        {
            string sql = "Select [shortdate] from [EQ State] where [shortdate]='" + rd["Date"].ToString() + "'";
            string ConnectionString = ConfigurationManager.ConnectionStrings["mdbcon"].ConnectionString;
            OleDbConnection conn = new OleDbConnection(ConnectionString);
            OleDbCommand cmd = new OleDbCommand(sql, conn);
            conn.Open();
            OleDbDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                result = "ExistData."+rd["Date"].ToString();
            }
            else
            {
                result = "NewData." + rd["Date"].ToString();
            }
            conn.Dispose();
            conn_Excel.Dispose();
            return result;
        }
        else
        {
            conn_Excel.Dispose();
            return result;
        }


    }
}
