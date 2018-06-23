using System;
using System.IO;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class WebPages_DailyReport : System.Web.UI.Page
{
    protected void LoadData()
    {
        string sql = ViewState["sql"].ToString();
        DataTable dt = DbManager.ExecuteQuery(sql);
        PagedDataSource pds=new PagedDataSource();
        pds.AllowPaging=true;
        pds.PageSize=1;
        pds.DataSource=dt.DefaultView;
        pds.CurrentPageIndex=int.Parse(ViewState["pageindex"].ToString());
        this.PreDate.Disabled=pds.IsLastPage;
        this.NextDate.Disabled=pds.IsFirstPage;
        this.DataList1.DataSource=pds;
        this.DataList1.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.LabelDate.Text = DateTime.Now.ToShortDateString();
            ViewState["isedit"] = false;
            ViewState["sqlid"] = "";
            ViewState["pageindex"] = 0;
            ViewState["sql"] = "Select * from ZhibanReport Order by ID Desc";
            LoadData();
        }
        this.TextBoxSearch.Attributes.Add("onkeydown", "if(event.keyCode==13) {document.all." + this.Search.ClientID + ".focus();document.all." + this.Search.ClientID + ".click();}");
        this.Delete.Attributes.Add("onclick", "javascript:if(confirm('确定要删除吗?')){}else{return false;}");
    }

    //工具按钮函数
    protected void PreDate_Click(object sender, EventArgs e)
    {
        int pageindex = int.Parse(ViewState["pageindex"].ToString());
        pageindex += 1;
        ViewState["pageindex"] = pageindex;
        LoadData();
    }
    protected void SetDate_Click(object sender, EventArgs e)
    {
        ViewState["pageindex"] = 0;
        LoadData();
    }
    protected void NextDate_Click(object sender, EventArgs e)
    {
        int pageindex = int.Parse(ViewState["pageindex"].ToString());
        pageindex -= 1;
        ViewState["pageindex"] = pageindex;
        LoadData();
    }
    protected void Add_Click(object sender, EventArgs e)
    {
        int hour = DateTime.Now.Hour;
        string banci = "白班";
        string shortdate = DateTime.Now.ToShortDateString();
        if(hour<9)
        {
            banci = "夜班";
            shortdate = DateTime.Now.AddDays(-1).ToShortDateString();
        }
        string title =shortdate + banci + " 值班报告";
        this.TextBoxDate.Text = title;
        this.TextBoxProduct.Text= "";
        this.TextBoxAlarm.Text = "";
        this.TextBoxYichang.Text = "";
        this.ToolTips.Text = "";
        this.TextBoxOther.Text = "";
        this.TextBoxEng.Text = Session["name"].ToString();
        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
    }

    protected void Delete_Click(object sender, EventArgs e)
    {
        string sqlid = "";
        int i = this.DataList1.Items.Count;
        if (i > 0)
        {
            foreach (DataListItem dli in DataList1.Items)
            {
                int id = Convert.ToInt32(DataList1.DataKeys[dli.ItemIndex]);
                sqlid = id.ToString();
                break;
            }
            sqlid = "(" + sqlid + ")";
            bool isdelete = false;
            if (Session["name"].ToString() == "葫芦娃")
            {
                isdelete = true;
            }
            string sqldelete = "Delete from ZhibanReport where ID in" + sqlid;
            string sqlcheck = "Select * from ZhibanReport where eng like '%" + Session["name"].ToString() + "%' AND ID in " + sqlid;
            string ConnectionString = ConfigurationManager.ConnectionStrings["mdbcon"].ConnectionString;
            OleDbConnection conn = new OleDbConnection(ConnectionString);
            OleDbCommand cmd = new OleDbCommand(sqlcheck, conn);
            conn.Open();
            OleDbDataReader rd = cmd.ExecuteReader();
            string filepath = "";
            while (rd.Read())
            {
                filepath = rd["abnormal"].ToString();
                isdelete = true;
            }
            if (isdelete)
            {
                conn.Close();
                try
                {
                    if (File.Exists(Server.MapPath(filepath)))
                    {
                        string filesql = "Delete from UploadFiles where path='" + filepath + "'";
                        DbManager.ExecuteNonQuery(filesql);
                        File.Delete(Server.MapPath(filepath));
                    }
                    DbManager.ExecuteNonQuery(sqldelete);
                    string myscript = @"alert('删除成功！');window.location.href='DailyReport.aspx';";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
                }
                catch
                {
                    string myscript = @"alert('删除失败，请与管理员联系！');window.location.href='DailyReport.aspx';";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
                }
            }
            else
            {
                conn.Close();
                string myscript = @"alert('无权删除他人信息！');window.location.href='DailyReport.aspx';";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
            }
        }
        else
        {
            string myscript = @"alert('无有效数据！');window.location.href='DailyReport.aspx';";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
        }
    }
    protected void Edit_Click(object sender, EventArgs e)
    {
        string sqlid = "";
        int i=this.DataList1.Items.Count;
        if (i > 0)
        {
            foreach (DataListItem dli in DataList1.Items)
            {
                int id = Convert.ToInt32(DataList1.DataKeys[dli.ItemIndex]);
                sqlid = id.ToString();
                break;
            }
            sqlid = "(" + sqlid + ")";
            bool isedit = false;
            string sqlcheck = "Select * from ZhibanReport where eng like '%" + Session["name"].ToString() + "%' AND ID in " + sqlid;
            if (Session["name"].ToString() == "程邵磊")
            {
                sqlcheck = "Select * from ZhibanReport where ID in" + sqlid;
                isedit = true;
            }
            string ConnectionString = ConfigurationManager.ConnectionStrings["mdbcon"].ConnectionString;
            OleDbConnection conn = new OleDbConnection(ConnectionString);
            OleDbCommand cmd = new OleDbCommand(sqlcheck, conn);
            conn.Open();
            OleDbDataReader rd = cmd.ExecuteReader();
            if (rd.HasRows)
            {
                isedit = true;
            }
            if (isedit)
            {
                while (rd.Read())
                {
                    this.TextBoxDate.Text = rd["title"].ToString();
                    this.TextBoxYichang.Text = Input.Outputadd(rd["yichang"].ToString());
                    this.TextBoxProduct.Text = Input.Outputadd(rd["chanpin"].ToString());
                    this.TextBoxAlarm.Text = Input.Outputadd(rd["alarm"].ToString());
                    this.TextBoxOther.Text = Input.Outputadd(rd["other"].ToString());
                    this.TextBoxEng.Text = Input.Outputadd(rd["eng"].ToString());
                }
                ViewState["sqlid"] = sqlid;
                ViewState["isedit"] = true;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
                conn.Close();
            }
            else
            {
                conn.Close();
                string myscript = @"alert('无权删除他人信息！');window.location.href='DailyReport.aspx';";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
            }
        }
        else
        {
            string myscript = @"alert('无编辑项！');window.location.href='DailyReport.aspx';";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
        }
    }
    protected void Search_Click(object sender, EventArgs e)
    {
        string chaxunstr = this.TextBoxSearch.Text.ToString();
        chaxunstr.Replace("%", "[%]");
        chaxunstr.Replace("';", "';';");
        chaxunstr.Replace("[", "[[]");
        chaxunstr.Replace("_", "[_]");
        chaxunstr = "%" + chaxunstr + "%";
        ViewState["sql"] = "Select * from ZhibanReport where title like '" + chaxunstr + "'or chanpin like '" + chaxunstr + "'or eng like '" + chaxunstr + "'or shortdate like '" + chaxunstr + "'or rework like '" + chaxunstr + "'or filename like '" + chaxunstr + "'or alarm like '" + chaxunstr + "'or other like '" + chaxunstr + "' order by ID Desc";
        LoadData();
    }

    //添加数据函数

    protected void Add_OK_Click(object sender, EventArgs e)
    {
        if (this.TextBoxDate.Text == "")
        {
            this.ToolTips.Text = "<span style='font-family:fontawesome !important; color:Red;'>&#xf071;</span>标题不能为空！";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        else if (this.TextBoxEng.Text == "")
        {
            this.ToolTips.Text = "<span style='font-family:fontawesome !important; color:Red;'>&#xf071;</span>值班人员不能为空！";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        else if (this.TextBoxProduct.Text=="")
        {
            this.ToolTips.Text = "<span style='font-family:fontawesome !important; color:Red;'>&#xf071;</span>产品信息不能为空！";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        else
        {
            string shortdate = DateTime.Now.ToShortDateString();
            string title = this.TextBoxDate.Text.ToString();
            string chanpin = this.TextBoxProduct.Text.ToString();
            chanpin = Input.Inputadd(chanpin);
            string alarm = this.TextBoxAlarm.Text.ToString();
            alarm = Input.Inputadd(alarm);
            string other = this.TextBoxOther.Text.ToString();
            other = Input.Inputadd(other);
            string yichang =Input.Inputadd(this.TextBoxYichang.Text.ToString());
            string eng = this.TextBoxEng.Text.ToString();
            eng = Input.Inputadd(eng);
            string sql = "Insert Into ZhibanReport (shortdate,title,chanpin,alarm,yichang,other,eng) values ('" + shortdate + "','" + title + "','" + chanpin + "','" + alarm + "','" + yichang + "','" + other + "','" + eng + "')";
            string abnormalpath = "";
            string abnormalfile = "";
            string abnormalsql = "";
            if ((bool)ViewState["isedit"])
            {
                sql = "Update ZhibanReport Set shortdate='" + shortdate + "',title='" + title + "',chanpin='" + chanpin + "',alarm='" + alarm + "',yichang='" + yichang + "',other='" + other + "',eng='" + eng + "' where ID in" + ViewState["sqlid"].ToString();
            }
            if (this.FileUpload1.HasFile)
            {
                if (this.FileUpload1.PostedFile.ContentLength > 20480000)
                {
                    this.ToolTips.Text = "<span style='font-family:fontawesome !important; color:Red;'>&#xf071;</span>文档大小不等超过20M！";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
                }
                else
                {
                    abnormalpath = "~/UploadFiles/Abnormal/";
                    string fileext = System.IO.Path.GetExtension(this.FileUpload1.FileName).ToLower();
                    int hour = DateTime.Now.Hour;
                    string banci = "白班";
                    if (hour < 9)
                    {
                        banci = "夜班";
                    }
                    abnormalfile = DateTime.Now.ToString("yyyyMMdd") + banci + "Abnormal报告" + fileext;
                    abnormalpath = abnormalpath + abnormalfile;
                    abnormalfile = "<br/>" + abnormalfile + "<p></p>";
                    abnormalsql = "Insert into UploadFiles (eng,shortdate,title,fenlei,filename,path)  values ('" + Session["name"].ToString() + "','" + shortdate + "','RWLot','设备报告','" + abnormalfile+ "','" + abnormalpath + "')";
                    sql = "Insert Into ZhibanReport (shortdate,title,chanpin,alarm,other,yichang,abnormalfile,abnormal,eng) values ('" + shortdate + "','" + title + "','" + chanpin + "','" + alarm + "','" + other + "','" + yichang + "','" + abnormalfile + "','" + abnormalpath + "','" + eng + "')";
                    if ((bool)ViewState["isedit"])
                    {
                        string sqlcheck = "Select rework from ZhibanReport where ID in" + ViewState["sqlid"].ToString();
                        string ConnectionString = ConfigurationManager.ConnectionStrings["mdbcon"].ConnectionString;
                        OleDbConnection conn = new OleDbConnection(ConnectionString);
                        OleDbCommand cmd = new OleDbCommand(sqlcheck, conn);
                        conn.Open();
                        OleDbDataReader rd = cmd.ExecuteReader();
                        string uploadfile = "";
                        while (rd.Read())
                        {
                            uploadfile = rd["rework"].ToString();
                        }
                        rd.Close();
                        conn.Close();
                        if (File.Exists(uploadfile))
                        {
                            string deletefilesql = "Delete from UploadFiles where path='" + uploadfile + "'";
                            DbManager.ExecuteNonQuery(deletefilesql);
                            File.Delete(uploadfile);
                        }
                        sql = "Update ZhibanReport Set shortdate='" + shortdate + "',title='" + title + "',chanpin='" + chanpin + "',alarm='" + alarm + "',other='" + other + "',yichang='" + yichang + "',abnormalfile='" + abnormalfile + "',abnormal='" + abnormalpath + "',eng='" + eng + "' where ID in" + ViewState["sqlid"].ToString();
                    }
                }
            }
                try
                {
                    DbManager.ExecuteNonQuery(sql);
                    if (this.FileUpload1.HasFile)
                    {
                        abnormalpath = Server.MapPath(abnormalpath);
                        FileUpload1.PostedFile.SaveAs(abnormalpath);
                        DbManager.ExecuteNonQuery(abnormalsql);
                    }
                    string myscript = @"alert('发布成功！');window.location.href='DailyReport.aspx';";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
                }
                catch
                {
                    string myscript = @"alert('发布失败，请与管理员联系！');window.location.href='DailyReport.aspx';";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
                }
        }
    }
    protected void Add_Cancel_Click(object sender, EventArgs e)
    {
        ViewState["isedit"] = false;
    }
    //跳转页面函数
    protected void Process_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProcessReport.aspx");
    }
    protected void Setup_Click(object sender, EventArgs e)
    {
        Response.Redirect("SetupReport.aspx");
    }

}
