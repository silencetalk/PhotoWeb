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

public partial class WebPages_ProcessReport : System.Web.UI.Page
{

    protected void CheckDate()
    {
        string sql = "Select top 1 * from ProcessReport Order by ID Desc";
        string ConnectionString = ConfigurationManager.ConnectionStrings["mdbcon"].ConnectionString;
        OleDbConnection conn = new OleDbConnection(ConnectionString);
        OleDbCommand cmd = new OleDbCommand(sql, conn);
        conn.Open();
        OleDbDataReader sdr = cmd.ExecuteReader();
        try
        {
            sdr.Read();
            if (sdr.HasRows)
            {
                ViewState["date"] = sdr["shortdate"].ToString().Trim();
                this.LabelDate.Text = ViewState["date"].ToString();
            }
            else
            {
                ViewState["date"] = DateTime.Now.ToShortDateString();
                this.LabelDate.Text = ViewState["date"].ToString();
            }
        }
        catch
        {
            ViewState["date"] = DateTime.Now.ToShortDateString();
            string myscript = @"alert('加载数据出错！');window.location.href='ProcessReport.aspx';";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
        }
        finally
        {
            sdr.Close();
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }
    }

    protected void LoadData()
    {
        string sql = ViewState["sql"].ToString();
        DataTable dt = DbManager.ExecuteQuery(sql);
        this.GridView1.DataSource = dt.DefaultView;
        this.GridView1.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["isedit"] = false;
            ViewState["sqlid"] = "";
            CheckDate();
            ViewState["sql"] = "Select * from ProcessReport where shortdate= '" + ViewState["date"] + "' Order by eng Desc,ID Desc";
            LoadData();
        }
        this.GridView1.Attributes.Add("style", "table-layout:fixed;word-wrap:break-word;");
        this.TextBoxSearch.Attributes.Add("onkeydown", "if(event.keyCode==13) {document.all." + this.Search.ClientID + ".focus();document.all." + this.Search.ClientID + ".click();}");
        this.Delete.Attributes.Add("onclick", "javascript:if(confirm('确定要删除吗?')){}else{return false;}");
    }

    protected void Eng_Changed(object sender, EventArgs e)
    {
        if (this.DropDownListEng.SelectedIndex == 0)
        {
            ViewState["sql"] = "Select * from ProcessReport where shortdate= '" + ViewState["date"] + "' Order by eng Desc,ID Desc";
            LoadData();
        }
        else
        {
            ViewState["sql"] = "Select top 20 * from ProcessReport where eng='"+this.DropDownListEng.SelectedValue+"' Order by ID Desc";
            LoadData();
        }
    }
    //工具按钮函数
    protected void PreDate_Click(object sender, EventArgs e)
    {
        DateTime dt = Convert.ToDateTime(this.LabelDate.Text.ToString());
        ViewState["date"] = dt.AddDays(-1).ToShortDateString();
        this.LabelDate.Text = ViewState["date"].ToString();
        ViewState["sql"] = "Select * from ProcessReport where shortdate= '" + ViewState["date"] + "' Order by eng Desc,ID Desc";
        LoadData();
    }
    protected void SetDate_Click(object sender, EventArgs e)
    {
        CheckDate();
        ViewState["sql"] = "Select * from ProcessReport where shortdate= '" + ViewState["date"].ToString() + "' Order by eng Desc,ID Desc";
        LoadData();
    }
    protected void NextDate_Click(object sender, EventArgs e)
    {
        DateTime dt = Convert.ToDateTime(this.LabelDate.Text.ToString());
        ViewState["date"] = dt.AddDays(1).ToShortDateString();
        this.LabelDate.Text = ViewState["date"].ToString();
        ViewState["sql"] = "Select * from ProcessReport where shortdate= '" + ViewState["date"] + "' Order by eng Desc,ID Desc";
        LoadData();
    }
    protected void Add_Click(object sender, EventArgs e)
    {
        this.TextBoxToday.Text = "";
        this.TextBoxTomorrow.Text = "";
        this.ToolTips.Text = "";
        this.DropDownListFenlei.SelectedIndex = 0;
        this.TextBoxInputDate.Text = DateTime.Now.ToShortDateString();
        this.TextBoxFileTittle.Text = "";
        this.TextBoxEng.Text = Session["name"].ToString();
        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
    }
  
    protected void Delete_Click(object sender, EventArgs e)
    {
        string sqlid = "";
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            CheckBox cbx = (CheckBox)GridView1.Rows[i].FindControl("CheckBox1");

            if (cbx.Checked == true)
            {
                sqlid = sqlid + Convert.ToInt32(GridView1.DataKeys[i].Value);
                break;
            }
        }
        if (sqlid != "")
        {
            sqlid = "(" + sqlid + ")";
            bool isdelete = false;
            string sqldelete = "Delete from ProcessReport where ID in" + sqlid;
            string sqlcheck = "Select * from ProcessReport where eng like '%" + Session["name"].ToString() + "%' AND ID in " + sqlid;
            if (Session["name"].ToString() == "程邵磊")
            {
                sqlcheck = "Select * from ProcessReport where ID in" + sqlid;
                isdelete = true;
            }
            string ConnectionString = ConfigurationManager.ConnectionStrings["mdbcon"].ConnectionString;
            OleDbConnection conn = new OleDbConnection(ConnectionString);
            OleDbCommand cmd = new OleDbCommand(sqlcheck, conn);
            conn.Open();
            OleDbDataReader rd = cmd.ExecuteReader();
            string filepath = "";
            while (rd.Read())
            {
                isdelete = true;
                filepath = rd["fujian"].ToString();
                break;
            }
            if (isdelete)
            {
                conn.Close();
                try
                {
                    if (File.Exists(Server.MapPath(filepath)))
                    {
                        File.Delete(Server.MapPath(filepath));
                        string filesql = "Delete from UploadFiles where path='" + filepath + "'";
                        DbManager.ExecuteNonQuery(filesql);
                    }
                    DbManager.ExecuteNonQuery(sqldelete);
                    string myscript = @"alert('删除成功！');window.location.href='ProcessReport.aspx';";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
                }
                catch
                {
                    string myscript = @"alert('删除失败，请与管理员联系！');window.location.href='ProcessReport.aspx';";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
                }
            }
            else
            {
                conn.Close();
                string myscript = @"alert('无权删除他人信息！');window.location.href='ProcessReport.aspx';";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
            }


        }
        else
        {
            string myscript = @"alert('请选择删除项！');window.location.href='ProcessReport.aspx';";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
        }
    }
    protected void Edit_Click(object sender, EventArgs e)
    {
        string sqlid = "";
        bool isedit = false;
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            CheckBox cbx = (CheckBox)GridView1.Rows[i].FindControl("CheckBox1");
            if (cbx.Checked == true)
            {
                sqlid = sqlid + Convert.ToInt32(GridView1.DataKeys[i].Value);
                break;
            }
        }
        if (sqlid != "")
        {
            sqlid = "(" + sqlid + ")";
            string sqlcheck = "Select * from ProcessReport where eng like '%" + Session["name"].ToString() + "%' AND ID in " + sqlid;
            if (Session["name"].ToString() == "程邵磊")
            {
                sqlcheck = "Select * from ProcessReport where ID in" + sqlid;
                isedit = true;
            }
            string ConnectionString = ConfigurationManager.ConnectionStrings["mdbcon"].ConnectionString;
            OleDbConnection conn = new OleDbConnection(ConnectionString);
            OleDbCommand cmd = new OleDbCommand(sqlcheck, conn);
            conn.Open();
            OleDbDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                isedit = true;
                break;
            }
            if (isedit)
            {

                this.DropDownListFenlei.SelectedValue = rd["fenlei"].ToString().Trim();
                string content = rd["today"].ToString();
                content = Input.Outputadd(content);
                this.TextBoxToday.Text = content;
                content = rd["tomorrow"].ToString();
                content = Input.Outputadd(content);
                this.TextBoxTomorrow.Text = content;
                content = rd["eng"].ToString();
                content = Input.Outputadd(content);
                this.TextBoxEng.Text = content;
                this.TextBoxFileTittle.Text = rd["title"].ToString();
                this.TextBoxInputDate.Text = rd["shortdate"].ToString();
                ViewState["sqlid"] = sqlid;
                ViewState["isedit"] = true;
                conn.Close();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
            }
            else
            {
                conn.Close();
                string myscript = @"alert('无权编辑他人信息！');window.location.href='ProcessReport.aspx';";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
            }
        }
        else
        {
            string myscript = @"alert('请选择编辑项！');window.location.href='ProcessReport.aspx';";
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
        ViewState["sql"] = "Select * from ProcessReport where today like '" + chaxunstr + "'or tomorrow like '" + chaxunstr + "'or eng like '" + chaxunstr + "'or shortdate like '" + chaxunstr + "'or fujian like '" + chaxunstr + "'or title like '" + chaxunstr + "' order by eng Desc,ID Desc";
        //ViewState["sql"] = "Select * from ProcessReport where * like '"+chaxunstr+"' Order by jita,ID Desc";
        LoadData();
    }

    //添加数据函数

    protected void Add_OK_Click(object sender, EventArgs e)
    {
        bool isadd = false;
        if (this.TextBoxInputDate.Text == "")
        {
            isadd = false;
            this.ToolTips.Text = "<span style='font-family:fontawesome !important; color:Red;'>&#xf071;</span>日期不能为空！";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        else if (this.TextBoxToday.Text == "")
        {
            isadd = false;
            this.ToolTips.Text = "<span style='font-family:fontawesome !important; color:Red;'>&#xf071;</span>今日工作不能为空！";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        else if (this.TextBoxTomorrow.Text == "")
        {
            isadd = false;
            this.ToolTips.Text = "<span style='font-family:fontawesome !important; color:Red;'>&#xf071;</span>明日工作不能为空！";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        else
        {
            if (this.FileUpload1.HasFile)
            {
                if (this.DropDownListFenlei.SelectedIndex == 0)
                {
                    isadd = false;
                    this.ToolTips.Text = "<span style='font-family:fontawesome !important; color:Red;'>&#xf071;</span>请选择文档分类！";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
                }
                else if (this.TextBoxFileTittle.Text == "")
                {
                    isadd = false;
                    this.ToolTips.Text = "<span style='font-family:fontawesome !important; color:Red;'>&#xf071;</span>请为附件添加一个简短的标题！";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
                }
                else if (this.FileUpload1.PostedFile.ContentLength > 20480000)
                {
                    isadd = false;
                    this.ToolTips.Text = "<span style='font-family:fontawesome !important; color:Red;'>&#xf071;</span>文档大小不等超过20M！";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
                }
                else
                {
                    isadd = true;
                }
            }
            else
            {
                isadd = true;
            }
        }
        if (isadd)
        {
            string shortdate = this.TextBoxInputDate.Text.ToString();
            string today = this.TextBoxToday.Text.ToString();
            today = Input.Inputadd(today);
            string tomorrow = this.TextBoxTomorrow.Text.ToString();
            tomorrow = Input.Inputadd(tomorrow);
            string eng = this.TextBoxEng.Text.ToString();
            eng = eng.Replace("\r\n", "<br/>");
            string path = "";
            string uploadsql = "";
            string sql = "Insert Into ProcessReport (eng,shortdate,today,tomorrow) values ('" + eng + "','" + shortdate + "','" + today + "','" + tomorrow + "')";
            if ((bool)ViewState["isedit"])
            {
                sql = "Update ProcessReport Set eng='" + eng + "',shortdate='" + shortdate + "',today='" + today + "',tomorrow='" + tomorrow + "' where ID in" + ViewState["sqlid"].ToString(); ;
            }
            if (this.FileUpload1.HasFile)
            {
                string fenlei = this.DropDownListFenlei.SelectedValue;
                string title = this.TextBoxFileTittle.Text.ToString();
                path = "~/UploadFiles/" + this.DropDownListFenlei.SelectedValue + "/";
                string newname = DateTime.Now.ToString("yyyyMMddHHmmss") + FileUpload1.FileName;
                newname = newname.Replace("#", " ");
                newname = newname.Replace("&", " ");
                path = path + newname;
                string filename = this.FileUpload1.FileName;
                filename = "<br/>" + filename + "<p></p>";
                uploadsql = "Insert into UploadFiles (eng,shortdate,title,fenlei,filename,path) values ('" + Session["name"].ToString() + "','" + shortdate + "','" + title + "','" + fenlei + "','" + filename + "','" + path + "')";
                sql = "Insert Into ProcessReport (eng,shortdate,title,fenlei,fujian,today,tomorrow) values ('" + eng + "','" + shortdate + "','" + title + "','" + fenlei + "','" + path + "','" + today + "','" + tomorrow + "')";
                if ((bool)ViewState["isedit"])
                {
                    string sqlcheck = "Select fujian from ProcessReport where ID in" + ViewState["sqlid"].ToString();
                    string ConnectionString = ConfigurationManager.ConnectionStrings["mdbcon"].ConnectionString;
                    OleDbConnection conn = new OleDbConnection(ConnectionString);
                    OleDbCommand cmd = new OleDbCommand(sqlcheck, conn);
                    conn.Open();
                    OleDbDataReader rd = cmd.ExecuteReader();
                    string uploadfile = "";
                    while (rd.Read())
                    {
                        uploadfile = rd["fujian"].ToString();
                    }
                    rd.Close();
                    conn.Close();
                    if (File.Exists(Server.MapPath(uploadfile)))
                    {
                        File.Delete(Server.MapPath(uploadfile));
                        string filesql = "Delete from UploadFiles where path='" + uploadfile + "'";
                        DbManager.ExecuteNonQuery(filesql);
                    }
                    sql = "Update ProcessReport set eng='" + eng + "',shortdate='" + shortdate + "',title='" + title + "',fenlei='" + fenlei + "',fujian='" + path + "',today='" + today + "',tomorrow='" + tomorrow + "' where ID in" + ViewState["sqlid"].ToString();
                }         
            }
            try
            {
                if (this.FileUpload1.HasFile)
                {
                    FileUpload1.PostedFile.SaveAs(Server.MapPath(path));
                    DbManager.ExecuteNonQuery(uploadsql);
                }
                DbManager.ExecuteNonQuery(sql);
                string myscript = @"alert('发布成功！');window.location.href='ProcessReport.aspx';";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
            }
            catch
            {
                string myscript = @"alert('发布失败，请与管理员联系！');window.location.href='ProcessReport.aspx';";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
            }
        }
    }
    protected void Add_Cancel_Click(object sender, EventArgs e)
    {
        ViewState["isedit"] = false;
    }

    //跳转页面函数
    protected void Zhiban_Click(object sender, EventArgs e)
    {
        Response.Redirect("ZhibanReport.aspx");
    }
    protected void Setup_Click(object sender, EventArgs e)
    {
        Response.Redirect("SetupReport.aspx");
    }

    //GridView函数
}
