using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;

/// <summary>
/// DbManger 是维护科日常事务管理系统的SQL Server Express数据库操作类
/// </summary>
public class ExpDbManager
{
    public ExpDbManager()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    /// <summary>
    /// 实现对SQL Server Express数据库的查询
    /// </summary>
    /// <param name="strSQL">查询语句</param>
    /// <returns>返回DataSet</returns>
    public static DataTable ExecuteQuery(string strSQL)
    {
        string ConnectionString = ConfigurationManager.ConnectionStrings
            ["expmdbcon"].ConnectionString;
        OleDbConnection conn = new OleDbConnection(ConnectionString);
        //在此使用异常语句，try表示尝试执行下面的语句
        try
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter(strSQL, conn);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds.Tables[0];
        }
        //不管上面try语句执行成功与否，最终都执行下面的语句
        finally
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();   //关闭连接
        }
    }
    /// <summary>
    /// 实现对SQL Server Express数据库的插入、删除或修改等非查询操作
    /// </summary>
    /// <param name="strSQL">插入、删除或修改的SQL语句</param>
    /// <returns>返回插入、删除或修改的SQL语句所影响的行数</returns>
    public static int ExecuteNonQuery(string strSQL)
    {
        string ConnectionString = ConfigurationManager.ConnectionStrings
            ["expmdbcon"].ConnectionString;
        OleDbConnection conn = new OleDbConnection(ConnectionString);
        conn.Open();    //打开数据库连接
        //在此使用异常语句，try表示尝试执行下面的语句
        try
        {
            OleDbCommand cmd = new OleDbCommand(strSQL, conn);
            return cmd.ExecuteNonQuery();
        }
        //不管上面try语句执行成功与否，最终都执行下面的语句
        finally
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();   //关闭连接
        }
    }
    /// <summary>
    /// 使用ExecuteScalar方法从数据库可检索单个值，用于聚合，如统计行数，求平均值等
    /// </summary>
    /// <param name="strSQL">检索单个值的SQL语句</param>
    /// <returns>返回聚合对象</returns>
    public static object ExecuteScalar(string strSQL)
    {
        string ConnectionString = ConfigurationManager.ConnectionStrings
            ["expmdbcon"].ConnectionString;
        OleDbConnection conn = new OleDbConnection(ConnectionString);
        conn.Open();    //打开数据库连接
        //在此使用异常语句，try表示尝试执行下面的语句
        try
        {
            OleDbCommand cmd = new OleDbCommand(strSQL, conn);
            cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strSQL;
            return cmd.ExecuteScalar();
        }
        //不管上面try语句执行成功与否，最终都执行下面的语句
        finally
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();   //关闭连接
        }
    }
}
