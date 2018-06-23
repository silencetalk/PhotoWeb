using System;
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

public partial class WebPages_DNS : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["isedit"]= false;
            ViewState["status"] = "Close";
            ViewState["sqlid"] = "";
            CheckDate();
            ViewState["sql"] = "Select * from DNS  where shortdate= '" + ViewState["date"].ToString() + "' OR status='Open' Order by jitai,shortdate,lindex";
            LoadData();
        }
        this.GridView1.Attributes.Add("style", "table-layout:fixed;word-wrap:break-word;");
        this.TextBoxSearch.Attributes.Add("onkeydown", "if(event.keyCode==13) {document.all." + this.Search.ClientID + ".focus();document.all." + this.Search.ClientID + ".click();}");
        this.Delete.Attributes.Add("onclick", "javascript:if(confirm('确定要删除吗?')){}else{return false;}");
    }

    //数据处理函数
    protected void CheckDate()
    {
        string sql = "Select top 1 * from DNS Order by ID Desc";
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
            string myscript = @"alert('加载数据出错！');window.location.href='DNS.aspx';";
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


    //工具按钮函数
    protected void PreDate_Click(object sender, EventArgs e)
    {
        DateTime dt = Convert.ToDateTime(this.LabelDate.Text.ToString());
        ViewState["date"] = dt.AddDays(-1).ToShortDateString();
        this.LabelDate.Text = ViewState["date"].ToString();
        ViewState["sql"] = "Select * from DNS where shortdate= '" + ViewState["date"] + "' OR status='Open' Order by lindex,ID Desc";
        LoadData();
    }
    protected void SetDate_Click(object sender, EventArgs e)
    {
        CheckDate();
        ViewState["sql"] = "Select * from DNS where shortdate= '" + ViewState["date"].ToString() + "' OR status='Open' Order by lindex,ID Desc";
        LoadData();
    }
    protected void NextDate_Click(object sender, EventArgs e)
    {
        DateTime dt = Convert.ToDateTime(this.LabelDate.Text.ToString());
        ViewState["date"]= dt.AddDays(1).ToShortDateString();
        this.LabelDate.Text = ViewState["date"].ToString();
        ViewState["sql"] = "Select * from DNS where shortdate= '" + ViewState["date"].ToString() + "' OR status='Open' Order by lindex,ID Desc";
        LoadData();
    }
    protected void Add_Click(object sender, EventArgs e)
    {
        this.ToolTips1.Text = "字数请勿超过900字！";
        this.DropDownListJitai.SelectedIndex = 0;
        this.TextBoxDailyInform.Text = "";
        this.TextBoxInputDate.Text = DateTime.Now.ToShortDateString();
        ViewState["status"] = "Open";
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
            string sqldelete = "Delete from DNS where ID in" + sqlid;
            string sqlcheck = "Select eng from DNS where ID in" + sqlid;
            string ConnectionString = ConfigurationManager.ConnectionStrings["mdbcon"].ConnectionString;
            OleDbConnection conn = new OleDbConnection(ConnectionString);
            OleDbCommand cmd = new OleDbCommand(sqlcheck, conn);
            conn.Open();
            OleDbDataReader rd = cmd.ExecuteReader();
            bool isdelete = false;
            while (rd.Read())
            {
                if (((string)rd["eng"] == (string)Session["name"]) || ((string)Session["name"] == "王明"))
                {
                    isdelete = true;
                    break;
                }
                else
                {
                    isdelete = false;
                    break;
                }
            }
            conn.Close();
            if (isdelete)
            {
                try
                {
                    int i = DbManager.ExecuteNonQuery(sqldelete);
                    string myscript = @"alert('删除成功！');window.location.href='DNS.aspx';";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
                }
                catch
                {
                    string myscript = @"alert('删除失败，请与管理员联系！');window.location.href='DNS.aspx';";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
                }
            }
            else
            {
                string myscript = @"alert('无权删除他人信息！');window.location.href='DNS.aspx';";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
            }
        }
        else
        {
            string myscript = @"alert('请选择删除项！');window.location.href='DNS.aspx';";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
        }
    }
    protected void Edit_Click(object sender, EventArgs e)
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
            string sqlcheck = "Select * from DNS where ID in" + sqlid;
            string ConnectionString = ConfigurationManager.ConnectionStrings["mdbcon"].ConnectionString;
            OleDbConnection conn = new OleDbConnection(ConnectionString);
            OleDbCommand cmd = new OleDbCommand(sqlcheck, conn);
            conn.Open();
            OleDbDataReader rd = cmd.ExecuteReader();
            bool isedit = false;
            while (rd.Read())
            {
                if (((string)rd["eng"] == (string)Session["name"]) || ((string)Session["name"] == "王明"))
                {
                   isedit= true;
                    break;
                }
                else
                {
                    isedit= false;
                    break;
                }
            }
            if (isedit)
            {
                if (rd["status"].ToString() == "Open")
                {
                    this.Open.Visible = true;
                    this.Close.Visible = false;
                    ViewState["status"] = "Open";
                }
                else
                {
                    this.Open.Visible = false;
                    this.Close.Visible = true;
                    ViewState["status"] = "Close";
                }
               this.DropDownListJitai.SelectedValue = rd["jitai"].ToString().Trim();
               string content = rd["content"].ToString();
               content = Input.Outputadd(content);
               this.TextBoxDailyInform.Text = content;
               this.TextBoxInputDate.Text = rd["shortdate"].ToString();
               ViewState["sqlid"] = sqlid;
               ViewState["isedit"] = true;
               conn.Close();
               Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
             }
            else
            {
                conn.Close();
                string myscript = @"alert('无权编辑他人信息！');window.location.href='DNS.aspx';";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
            }
        }
        else
        {
            string myscript = @"alert('请选择编辑项！');window.location.href='DNS.aspx';";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
        }
    }
    protected void Reply_Click(object sender, EventArgs e)
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
            string sqlcheck = "Select * from DNS where ID in" + sqlid;
            string ConnectionString = ConfigurationManager.ConnectionStrings["mdbcon"].ConnectionString;
            OleDbConnection conn = new OleDbConnection(ConnectionString);
            OleDbCommand cmd = new OleDbCommand(sqlcheck, conn);
            conn.Open();
            OleDbDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
               this.LabelReply.Text = rd["eng"].ToString();
               string reply = Input.Outputadd(rd["reply"].ToString());
               reply = reply + "\r\n" + DateTime.Now.ToShortDateString() + "  " + "进度：";
               this.TextBoxReply.Text = reply;
               if (rd["status"].ToString() == "Open")
               {
                   this.Open1.Visible = true;
                   this.Close1.Visible = false;
                   ViewState["status"] = "Open";
               }
               else
               {
                   this.Open1.Visible = false;
                   this.Close1.Visible = true;
                   ViewState["status"] = "Close";
               }
            }
            ViewState["sqlid"] = sqlid;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Reply');</script>");
        }
        else
        {
            string myscript = @"alert('请选择事件项！');window.location.href='DNS.aspx';";
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
        ViewState["sql"] = "Select * from DNS where jitai like '" + chaxunstr + "'or banci like '" + chaxunstr + "'or content like '" + chaxunstr + "'or eng like '" + chaxunstr + "'or shortdate like '" + chaxunstr + "'or reply like '" + chaxunstr + "' order by lindex,ID Desc";
        LoadData();
    }

    //添加内容函数
    protected void AddDI_OK_Click(object sender, EventArgs e)
    {
        if (this.DropDownListJitai.SelectedIndex == 0)
        {
            this.ToolTips1.Text = "请选择机台！";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        else if (this.TextBoxDailyInform.Text == "")
        {
            this.ToolTips1.Text = "内容不能为空！";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        else if(this.TextBoxInputDate.Text =="")
        {
            this.ToolTips1.Text = "日期不能为空！";
        }
        else
        {
            try
            {
                DateTime dt=Convert.ToDateTime(this.TextBoxInputDate.Text.ToString());
            }
            catch
            {
            }
            string jitai = this.DropDownListJitai.SelectedValue;
            int lindex = this.DropDownListJitai.SelectedIndex;
            string informdate = this.TextBoxInputDate.Text.ToString();
            string content = this.TextBoxDailyInform.Text.ToString();
            content = Input.Inputadd(content);
            string eng = (string)Session["name"];
            DateTime fabiaodate = Convert.ToDateTime(informdate);
            string status = ViewState["status"].ToString();
            string sql = "Insert INTO DNS (jitai,[lindex],content,eng,shortdate,status,[fabiaodate]) values ('" + jitai + "','" + lindex + "','" + content + "','" + eng + "','" + informdate + "','" + status + "',#" + fabiaodate.ToLocalTime() + "#)";
            if ((bool)ViewState["isedit"])
            {
                sql = "Update DNS Set jitai='" + jitai + "',content='" + content + "',eng='" + eng + "',status='" + status + "',[lindex]='" + lindex + "',shortdate='" + informdate + "',fabiaodate=#" + fabiaodate.ToLocalTime() + "# where ID in " + ViewState["sqlid"].ToString();
            }
            try
            {
                DbManager.ExecuteNonQuery(sql);
                string myscript = @"alert('发布成功！');window.location.href='DNS.aspx';";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
            }
            catch
            {
                this.ToolTips1.Text = "发布失败，请与管理员联系！";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
            }
        }
    }
    protected void AddReply_OK_Click(object sender, EventArgs e)
    {
        string reply = this.TextBoxReply.Text.ToString();
        reply = Input.Inputadd(reply);
        string name = Session["name"].ToString();
        string status = ViewState["status"].ToString();
        string sql = "Update DNS Set reply='" + reply + "',status='"+status+"' where ID in " + ViewState["sqlid"].ToString();
        try
        {
            DbManager.ExecuteNonQuery(sql);
            string myscript = @"alert('添加成功！');window.location.href='DNS.aspx';";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
        }
        catch
        {
            this.ToolTips2.Text = "添加失败，请与管理员联系！";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Reply');</script>");
        }
    }
    protected void Add_Cancel_Click(object sender, EventArgs e)
    {
        ViewState["isedit"] = false;
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

    protected void Open_Reply_Click(object sender, EventArgs e)
    {
        this.Open1.Visible = false;
        this.Close1.Visible = true;
        ViewState["status"] = "Close";
        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Reply');</script>");
    }
    protected void Close_Reply_Click(object sender, EventArgs e)
    {
        this.Close1.Visible = false;
        this.Open1.Visible = true;
        ViewState["status"] = "Open";
        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Reply');</script>");
    }

    //跳转页面函数
    protected void Inform_Click(object sender, EventArgs e)
    {
        Response.Redirect("DailyInform.aspx");
    }
    protected void Work_Click(object sender, EventArgs e)
    {
        Response.Redirect("DailyWork.aspx");
    }
    protected void Setup_Click(object sender, EventArgs e)
    {
        Response.Redirect("SetupReport.aspx");
    }
          protected void personal_Click(object sender, EventArgs e)
    {
        Response.Redirect("personalInform.aspx");
    }
}
