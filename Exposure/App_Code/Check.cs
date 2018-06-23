using System;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

/// <summary>
///Check 的摘要说明
/// </summary>
public class Check
{
	public Check()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
    public bool CK(string lie, string str)
    {
        string ConnectionString = ConfigurationManager.ConnectionStrings ["mdbcon"].ConnectionString;
        OleDbConnection conn = new OleDbConnection(ConnectionString);
        if(conn.State==ConnectionState.Closed)
        {
            conn.Open();
        }
        string cx = "Select * from UserInfo where " + lie + "='" + str + "'";
        OleDbCommand cmd = new OleDbCommand(cx, conn);
        OleDbDataReader dr = cmd.ExecuteReader();
        try
        {
            if (dr.Read()) return true;
            else
            return false;

        }
        catch
        {
            return false;
        }
        finally
        {
           dr.Close();
           if (conn.State == ConnectionState.Open)
           conn.Close();
        }
    }
    public bool CKXG(string lie, string str,string id, OleDbConnection conn)
    {
        if (conn.State == ConnectionState.Closed)
        {
            conn.Open();
        }
        string cx = "SELECT * FROM logininfo WHERE " + lie + "='" + str + "'";
        OleDbCommand cmd = new OleDbCommand(cx, conn);
        OleDbDataReader dr = cmd.ExecuteReader();
        try
        {
            if (dr.Read()) 
            {
                if (dr["num"].ToString().Trim() != id)
                    return true;
                else
                    return false;
            }
            else
                return false;

        }
        catch
        {
            return false;
        }
        finally
        {
            dr.Close();
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }
    }
}
