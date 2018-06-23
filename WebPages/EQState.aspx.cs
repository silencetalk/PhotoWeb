using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Web.UI.DataVisualization.Charting;


public partial class WebPages_EQState : System.Web.UI.Page
{
    static public DataTable dt=new DataTable();
    protected void LoadData()
    {
        string sql = ViewState["datesql"].ToString();
        //string ConnectionString = ConfigurationManager.ConnectionStrings["mdbcon"].ConnectionString;
        //OleDbConnection conn = new OleDbConnection(ConnectionString);
        //OleDbCommand cmd = new OleDbCommand(sql, conn);
        //conn.Open();
        //OleDbDataReader sdr = cmd.ExecuteReader();
        //if (sdr.Read())
        //{
        //    LabelDate.Text = sdr["shortdate"].ToString();
        //    LoadImg(sdr["filepath"].ToString());
        //}
       dt = DbManager.ExecuteQuery(sql);
        if (dt.Rows.Count > 0)
        {
            ViewState["pagecount"] = dt.Rows.Count;
            LoadDate();
        }
        else
        {
            LabelDate.Text = "无数据";
        }
    }
    protected void LoadDate()
    {
        LabelDate.Text = dt.Rows[(int)ViewState["pageindex"]]["shortdate"].ToString();
        LoadImg(dt.Rows[(int)ViewState["pageindex"]]["filepath"].ToString());
    }
    protected void LoadImg(string filepath)
    {
        string sql = "select * from [EQ Summary$]";
        OleDbConnection conn_Excel = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Jet OLEDB:Database Password=;Extended properties=Excel 5.0;Data Source=" + Server.MapPath(filepath));
        OleDbCommand cmd_Excel = new OleDbCommand(sql, conn_Excel);

        try
        {
            conn_Excel.Open();
            OleDbDataAdapter myDataAdapter = new OleDbDataAdapter();
            myDataAdapter.SelectCommand = cmd_Excel;
            DataTable dt = new DataTable();
            myDataAdapter.Fill(dt);
            foreach (DataColumn dc in dt.Columns)
            {
                if ((dc.ColumnName != "Date") && (dc.ColumnName != "EQP ID"))
                {
                    Chart1.Series.Add(dc.ColumnName);
                    Chart1.Series[dc.ColumnName].ChartType = SeriesChartType.StackedBar100;
                    Chart1.Series[dc.ColumnName].Legend = "Default";
                    Chart1.Series[dc.ColumnName]["EmptyPointValue"] = "Zero";
                    switch (dc.ColumnName.ToString())
                    {
                        case "RUN":
                            {
                                Chart1.Series[dc.ColumnName].Color = System.Drawing.Color.Green;
                                Chart1.Series[dc.ColumnName].IsValueShownAsLabel = true;
                                break;
                            }
                        case "IDLE":
                            {
                                Chart1.Series[dc.ColumnName].Color = System.Drawing.Color.Pink;
                                break;
                            }
                        case "ETIME":
                            {
                                Chart1.Series[dc.ColumnName].Color = System.Drawing.Color.Snow;
                                break;
                            }
                        case "ETC":
                            {
                                Chart1.Series[dc.ColumnName].Color = System.Drawing.Color.CadetBlue;
                                break;
                            }
                        case "MAINT":
                            {
                                Chart1.Series[dc.ColumnName].Color = System.Drawing.Color.Indigo;
                                break;
                            }
                        case "DOWN":
                            {
                                Chart1.Series[dc.ColumnName].Color = System.Drawing.Color.Red;
                                break;
                            }
                        default:
                            break;

                    }
                }
            }
            foreach (DataRow dr in dt.Rows)
            {
                for (int i = 0; i < Chart1.Series.Count; i++)
                {
                    double y = 0;
                    if (dr[Chart1.Series[i].Name].ToString() != "")
                    {
                        y = Convert.ToDouble(dr[Chart1.Series[i].Name]) * 100;
                    }
                    Chart1.Series[i].Points.AddXY(dr["EQP ID"].ToString(), y);
                }
            }
            Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            conn_Excel.Dispose();
        }

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["pageindex"] = 0;
            ViewState["pagecount"] = 0;
            ViewState["datesql"] = "Select * from [EQ State] Order by ID Desc";
            LoadData();
            NextDate.Disabled = true;
        }
    }
    protected void PreDate_Click(object sender, EventArgs e)
    {
        int pageindex = int.Parse(ViewState["pageindex"].ToString());
        pageindex += 1;
        if (pageindex < (int)ViewState["pagecount"])
        {
            ViewState["pageindex"] = pageindex;
            LoadDate();
            NextDate.Disabled = false;
            if(pageindex==((int)ViewState["pagecount"]-1))
            {
                PreDate.Disabled = true;
            }
        }
    }
    protected void SetDate_Click(object sender, EventArgs e)
    {
        ViewState["pageindex"] = 0;
        LoadDate();
        NextDate.Disabled = true;
        if ((int)ViewState["pagecount"] == 1)
            PreDate.Disabled = true;
        else
            PreDate.Disabled = false;
    }
    protected void NextDate_Click(object sender, EventArgs e)
    {
        int pageindex = int.Parse(ViewState["pageindex"].ToString());
        pageindex -= 1;
        if (pageindex >= 0)
        {
            ViewState["pageindex"] = pageindex;
            LoadDate();
            PreDate.Disabled = false;
            if (pageindex == 0)
            {
                NextDate.Disabled = true;
            }
        }
    }
    protected void Add_OK_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            string path = "~/UploadFiles/ExcelData/";
            string filename = DateTime.Now.ToString("yyyyMMddhhmmss") + FileUpload1.FileName;
            path += filename;
            if (Path.GetExtension(filename) == ".xls")
            {
                try
                {
                    FileUpload1.PostedFile.SaveAs(Server.MapPath(path));
                    string excelResult = ExcelManager.CheckExcelData(Server.MapPath(path));
                    if (excelResult=="NullData")
                    {
                        string myscript = @"alert('数据导入失败！');window.location.href='EQState.aspx';";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
                    }
                    else
                    {
                        string splitflag = ".";
                        string[] resultinfo = excelResult.Split(splitflag.ToCharArray(), System.StringSplitOptions.RemoveEmptyEntries);
                        string insertsql = "Insert Into [EQ State] ([shortdate],[filepath]) values ('"+resultinfo[1]+"','"+path+"')";
                        if (resultinfo[0] == "ExistData")
                        {
                            insertsql = "Update [EQ State] Set [filepath]='"+ path +"' where [shortdate]='"+resultinfo[1]+"'";
                        }
                        try
                        {
                            DbManager.ExecuteNonQuery(insertsql);
                            string myscript = @"alert('数据导入成功！');window.location.href='EQState.aspx';";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
                        }
                        catch
                        {
                            string myscript = @"alert('数据库操作失败！');window.location.href='EQState.aspx';";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
                        }                        
                    }
                }
                catch
                {
                    string myscript = @"alert('文件上传失败！');window.location.href='EQState.aspx';";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
                }
            }
            else
            {
                ToolTips.Text = "文档格式必须为'.xls'！";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
            }
        }
        else
        {
            ToolTips.Text = "请选择数据文件！";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
    }
    protected void Add_Cancel_Click(object sender, EventArgs e)
    {
        ToolTips.Text = "";
        LoadDate();
    }
    
}
