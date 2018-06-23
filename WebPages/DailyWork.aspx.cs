using System;
using System.Collections;
using System.Configuration;
using System.IO;
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

public partial class WebPages_DailyWork : System.Web.UI.Page
{
    protected void CheckDate()
    {
        string sql = "Select top 1 * from DailyWork Order by ID Desc";
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
            string myscript = @"alert('加载数据出错！');window.location.href='DailyWork.aspx';";
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
            ViewState["status"] = "Open";
            ViewState["sqlid"] = "";
            DateTime dt = DateTime.Now.AddHours(-72);
            CheckDate();
            ViewState["sql"] = "Select * from DailyWork where status='Open' or lastdate>=#"+dt+"# Order by Status,lindex,eng";
            LoadData();
        }
        this.GridView1.Attributes.Add("style", "table-layout:fixed;word-wrap:break-word;");
        this.TextBoxSearch.Attributes.Add("onkeydown", "if(event.keyCode==13) {document.all." + this.Search.ClientID + ".focus();document.all." + this.Search.ClientID + ".click();}");
        
    }

    //工具按钮函数
    protected void PreDate_Click(object sender, EventArgs e)
    {
        DateTime dt = Convert.ToDateTime(this.LabelDate.Text.ToString());
        ViewState["date"] = dt.AddDays(-1).ToShortDateString();
        this.LabelDate.Text = ViewState["date"].ToString();
        ViewState["sql"] = "Select * from DailyWork where shortdate= '" + ViewState["date"] + "' Order by Status,lindex,eng";
        LoadData();
    }
    protected void SetDate_Click(object sender, EventArgs e)
    {
        CheckDate();
        ViewState["sql"] = "Select * from DailyWork where shortdate= '" + ViewState["date"].ToString() + "' Order by lindex,eng";
        LoadData();
    }
    protected void NextDate_Click(object sender, EventArgs e)
    {
        DateTime dt = Convert.ToDateTime(this.LabelDate.Text.ToString());
        ViewState["date"] = dt.AddDays(1).ToShortDateString();
        this.LabelDate.Text = ViewState["date"].ToString();
        ViewState["sql"] = "Select * from DailyWork where shortdate= '" + ViewState["date"] + "' Order by lindex,eng";
        LoadData();
    }
    protected void Add_Click(object sender, EventArgs e)
    {
        this.TextBoxDailyWork.Text = "";
        this.TextBoxInputDate.Text = DateTime.Now.ToShortDateString();
        this.TextBoxJindu.Text = "";
        this.ToolTips.Text = "";
        this.DropDownListJitai.SelectedIndex = 0;
        this.DropDownListFenlei.SelectedIndex = 0;
        this.TextBoxFileTittle.Text = "";
        this.Open.Visible = true;
        this.Close.Visible = false;
        ViewState["status"] = "Open";
        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
    }

    protected void JitaiChanged(object sender, EventArgs e)
    {
        this.ToolTips.Text = "";
        string sql = "Select top 1 * from DailyWork where jitai='" + this.DropDownListJitai.SelectedValue + "' AND eng Like '%"+Session["name"].ToString()+"%' Order by ID Desc";
        string ConnectionString = ConfigurationManager.ConnectionStrings["mdbcon"].ConnectionString;
        OleDbConnection conn = new OleDbConnection(ConnectionString);
        OleDbCommand cmd = new OleDbCommand(sql, conn);
        conn.Open();
        OleDbDataReader rd = cmd.ExecuteReader();
        try
        {
            rd.Read();
            this.DropDownListJitai.SelectedValue = rd["jitai"].ToString().Trim();
            this.DropDownListFenlei.SelectedValue = rd["fenlei"].ToString().Trim();
            this.TextBoxInputDate.Text = rd["shortdate"].ToString();
            this.TextBoxFileTittle.Text = rd["title"].ToString();
            this.TextBoxDailyWork.Text = Input.Outputadd(rd["content"].ToString());
            this.TextBoxJindu.Text = Input.Outputadd(rd["jindu"].ToString());
            this.DropDownListEng.SelectedValue = rd["eng"].ToString();
            //this.TextBoxEng.Text = Input.Outputadd(rd["eng"].ToString());
            if (rd["status"].ToString() == "Close")
            {
                this.Open.Visible = false;
                this.Close.Visible = true;
                ViewState["status"] = "Close";
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        catch
        {
            this.ToolTips.Text = "<span style='font-family:fontawesome !important; color:Red;'>&#xf071;</span>无历史记录，请添加新记录！";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        finally
        {
            rd.Close();
            conn.Close();
        }
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
            string sqldelete = "Delete from DailyWork where ID in" + sqlid;
            string sqlcheck = "Select * from DailyWork where ID in " + sqlid;
            string ConnectionString = ConfigurationManager.ConnectionStrings["mdbcon"].ConnectionString;
            OleDbConnection conn = new OleDbConnection(ConnectionString);
            OleDbCommand cmd = new OleDbCommand(sqlcheck, conn);
            conn.Open();
            OleDbDataReader rd = cmd.ExecuteReader();
            string filepath = "";
            bool isdelete = false;
            while (rd.Read())
            {
                if (rd["eng"].ToString() == Session["name"].ToString() || Session["name"].ToString() == "程邵磊" || Session["name"].ToString() == "吴昌桦")
                {
                    isdelete = true;
                    filepath = rd["fujian"].ToString();
                    break;
                }
                else
                {
                    isdelete = false;
                    break;
                }
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
                    string myscript = @"alert('删除成功！');window.location.href='DailyWork.aspx';";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
                }
                catch
                {
                    string myscript = @"alert('删除失败，请与管理员联系！');window.location.href='DailyWork.aspx';";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
                }
            }
            else
            {
                conn.Close();
                string myscript = @"alert('无权删除他人信息！');window.location.href='DailyWork.aspx';";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
            }
        }
        else
        {
            string myscript = @"alert('请选择删除项！');window.location.href='DailyWork.aspx';";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
        }
    }
    protected void Edit_Click(object sender, EventArgs e)
    {
        this.ToolTips.Text = "";
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
            string sqlcheck = "Select * from DailyWork where ID in " + sqlid;
            string ConnectionString = ConfigurationManager.ConnectionStrings["mdbcon"].ConnectionString;
            OleDbConnection conn = new OleDbConnection(ConnectionString);
            OleDbCommand cmd = new OleDbCommand(sqlcheck, conn);
            conn.Open();
            OleDbDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
                {
                    this.DropDownListJitai.SelectedValue = rd["jitai"].ToString().Trim();
                    this.DropDownListFenlei.SelectedValue = rd["fenlei"].ToString().Trim();
                    this.DropDownListEng.SelectedValue = rd["eng"].ToString();
                    this.TextBoxDailyWork.Text = Input.Outputadd(rd["content"].ToString());
                    this.TextBoxJindu.Text = Input.Outputadd(rd["jindu"].ToString());
                    this.TextBoxFileTittle.Text = rd["title"].ToString();
                    this.TextBoxInputDate.Text = rd["shortdate"].ToString();
                    if (rd["status"].ToString() == "Close")
                    {
                        this.Open.Visible = false;
                        this.Close.Visible = true;
                        ViewState["status"] = "Close";
                    }
                }
                ViewState["sqlid"] = sqlid;
                ViewState["isedit"] = true;
                conn.Close();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        else
        {
            string myscript = @"alert('请选择编辑项！');window.location.href='DailyWork.aspx';";
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
        ViewState["sql"] = "Select * from DailyWork where jitai like '" + chaxunstr + "'or content like '" + chaxunstr + "'or jindu like '" + chaxunstr + "'or status like '" + chaxunstr + "'or eng like '" + chaxunstr + "'or shortdate like '" + chaxunstr + "'or fujian like '" + chaxunstr + "'or title like '" + chaxunstr + "' order by lindex,ID Desc";
        LoadData();
    }

    protected void Open_Click(object sender, EventArgs e)
    {
        this.Open.Visible = false;
        this.Close.Visible = true;
        ViewState["status"] = "Close";
        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
    }
    protected void Close_Click(object sender, EventArgs e)
    {
        this.Close.Visible = false;
        this.Open.Visible = true;
        ViewState["status"] = "Open";
        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
    }


    //添加数据函数

    protected void Add_OK_Click(object sender, EventArgs e)
    {
        bool isadd = false;
        if (this.DropDownListJitai.SelectedIndex== 0)
        {
            this.ToolTips.Text = "<span style='font-family:fontawesome !important; color:Red;'>&#xf071;</span>请选择机台！";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        else if (this.DropDownListEng.SelectedIndex==0)
        {
            this.ToolTips.Text = "<span style='font-family:fontawesome !important; color:Red;'>&#xf071;</span>请选择工程师！";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        else if (this.TextBoxInputDate.Text == "")
        {
            this.ToolTips.Text = "<span style='font-family:fontawesome !important; color:Red;'>&#xf071;</span>日期不能为空！";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        else if (this.TextBoxDailyWork.Text == "")
        {
            this.ToolTips.Text = "<span style='font-family:fontawesome !important; color:Red;'>&#xf071;</span>今日工作不能为空！";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        else
        {
            if (this.FileUpload1.HasFile)
            {
                if (this.TextBoxFileTittle.Text == "")
                {
                    this.ToolTips.Text = "<span style='font-family:fontawesome !important; color:Red;'>&#xf071;</span>请为附件添加一个简短的标题！";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
                }
                else if (this.FileUpload1.PostedFile.ContentLength > 20480000)
                {
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
            string jitai = this.DropDownListJitai.SelectedValue;
            int lindex = this.DropDownListJitai.SelectedIndex + 1;
            string shortdate = this.TextBoxInputDate.Text.ToString();
            string content = Input.Inputadd(this.TextBoxDailyWork.Text.ToString());
            string jindu = Input.Inputadd(this.TextBoxJindu.Text.ToString());
            string status = ViewState["status"].ToString();
            string eng = this.DropDownListEng.SelectedValue;
            string path = "";
            string lastdate = DateTime.Now.ToShortDateString();
            string sql = "Insert Into DailyWork (eng,shortdate,jitai,[lindex],content,jindu,status,lastdate) values ('" + eng + "','" + shortdate + "','" + jitai + "','" + lindex + "','" + content + "','" + jindu + "','" + status + "','" + lastdate + "')";
            if ((bool)ViewState["isedit"])
            {
                sql = "Update DailyWork Set eng='" + eng + "',shortdate='" + shortdate + "',jitai='" + jitai + "',[lindex]='" + lindex + "',content='" + content + "',jindu='" + jindu + "',status='" + status + "',lastdate='" + lastdate + "' where ID in" + ViewState["sqlid"].ToString(); ;
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
                string uploadsql = "Insert into UploadFiles (eng,shortdate,title,fenlei,filename,path) values ('" + Session["name"].ToString() + "','" + shortdate + "','" + title + "','" + fenlei + "','" + filename + "','" + path + "')";
                sql = "Insert Into DailyWork (eng,shortdate,jitai,[lindex],content,jindu,status,title,fujian,fenlei,lastdate) values ('" + eng + "','" + shortdate + "','" + jitai + "','" + lindex + "','" + content + "','" + jindu + "','" + status + "','" + title + "','" + path + "','" + fenlei + "','" + lastdate + "')";
                if ((bool)ViewState["isedit"])
                {
                    string sqlcheck = "Select fujian from DailyWork where ID in" + ViewState["sqlid"].ToString();
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
                    sql = "Update DailyWork Set eng='" + eng + "',shortdate='" + shortdate + "',jitai='" + jitai + "',[lindex]='" + lindex + "',content='" + content + "',jindu='" + jindu + "',status='" + status + "',title='" + title + "',fujian='" + path + "',fenlei='" + fenlei + "',lastdate='" + lastdate + "' where ID in" + ViewState["sqlid"].ToString(); 
                }
                try
                {
                    this.FileUpload1.PostedFile.SaveAs(Server.MapPath(path));
                    DbManager.ExecuteNonQuery(uploadsql);
                    DbManager.ExecuteNonQuery(sql);
                    string myscript = @"alert('发布成功！');window.location.href='DailyWork.aspx';";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
                }
                catch
                {
                    string myscript = @"alert('发布失败，请与管理员联系！');window.location.href='DailyWork.aspx';";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
                }
            }
            else
            {
                try
                {
                    DbManager.ExecuteNonQuery(sql);
                    string myscript = @"alert('发布成功！');window.location.href='DailyWork.aspx';";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
                }
                catch
                {
                    string myscript = @"alert('发布失败，请与管理员联系！');window.location.href='DailyWork.aspx';";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
                }
            }
        }
    }
    protected void Add_Cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("DailyWork.aspx");
    }

    //跳转页面函数
    protected void Geren_Click(object sender, EventArgs e)
    {
        Response.Redirect("DNS.aspx");
    }
    protected void Inform_Click(object sender, EventArgs e)
    {
        Response.Redirect("DailyInform.aspx");
    }
    protected void Setup_Click(object sender, EventArgs e)
    {
        Response.Redirect("SetupReport.aspx");
    }
          protected void personal_Click(object sender, EventArgs e)
    {
        Response.Redirect("personalInform.aspx");
    }
    //GridView函数
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.GridView1.PageIndex = e.NewPageIndex;
        LoadData();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "go")
        {
            TextBox tb = (TextBox)GridView1.BottomPagerRow.FindControl("TextBoxIndex");
            int PageIndex = Int32.Parse(tb.Text.ToString().Trim());
            GridViewPageEventArgs ea = new GridViewPageEventArgs(PageIndex - 1);
            GridView1_PageIndexChanging(null, ea);
        }
    }
}
