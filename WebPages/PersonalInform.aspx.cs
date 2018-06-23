using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebPages_PersonalInform : System.Web.UI.Page
{
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
            ViewState["sql"] = "Select * from GerenInform Order by ID Desc";
            LoadData();
        }
        this.GridView1.Attributes.Add("style", "table-layout:fixed;word-wrap:break-word;");
        this.TextBoxSearch.Attributes.Add("onkeydown", "if(event.keyCode==13) {document.all." + this.Search.ClientID + ".focus();document.all." + this.Search.ClientID + ".click();}");
        this.Delete.Attributes.Add("onclick", "javascript:if(confirm('确定要删除吗?')){}else{return false;}");
    }
    //工具按钮函数
    protected void Add_Click(object sender, EventArgs e)
    {
        this.ToolTips1.Text = "字数请勿超过900字！";
        this.DropDownListEng.SelectedIndex = 0;
        this.TextBoxGerenDate.Text = DateTime.Now.ToShortDateString();
        this.TextBoxGeren.Text = "";
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
            string sqldelete = "Delete from GerenInform where ID in" + sqlid;
            string sqlcheck = "Select eng from GerenInform where ID in" + sqlid;
            string ConnectionString = ConfigurationManager.ConnectionStrings["mdbcon"].ConnectionString;
            OleDbConnection conn = new OleDbConnection(ConnectionString);
            OleDbCommand cmd = new OleDbCommand(sqlcheck, conn);
            conn.Open();
            OleDbDataReader rd = cmd.ExecuteReader();
            bool isdelete = false;
            while (rd.Read())
            {
                if (((string)rd["Eng"] == (string)Session["name"]) || ((string)Session["name"] == "王明"))
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
                    string myscript = @"alert('删除成功！');window.location.href='PersonalInform.aspx';";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
                }
                catch
                {
                    string myscript = @"alert('删除失败，请与管理员联系！');window.location.href='PersonalInform.aspx';";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
                }
            }
            else
            {
                string myscript = @"alert('无权删除他人信息！');window.location.href='PersonalInform.aspx';";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
            }


        }
        else
        {
            string myscript = @"alert('请选择删除项！');window.location.href='PersonalInform.aspx';";
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
            string sqlcheck = "Select * from GerenInform where ID in" + sqlid;
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
                    isedit = true;
                    break;
                }
                else
                {
                    isedit = false;
                    break;
                }
            }
            if (isedit)
            {
                this.DropDownListEng.SelectedValue = rd["to"].ToString();
                this.TextBoxGerenDate.Text = rd["shortdate"].ToString();
                string content = rd["content"].ToString();
                content = Input.Outputadd(content);
                this.TextBoxGeren.Text = content;
                ViewState["sqlid"] = sqlid;
                ViewState["isedit"] = true;
                conn.Close();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
            }
            else
            {
                conn.Close();
                string myscript = @"alert('无权编辑他人信息！');window.location.href='PersonalInform.aspx';";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
            }
        }
        else
        {
            string myscript = @"alert('请选择编辑项！');window.location.href='PersonalInform.aspx';";
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
            string sqlcheck = "Select * from GerenInform where ID in" + sqlid;
            string ConnectionString = ConfigurationManager.ConnectionStrings["mdbcon"].ConnectionString;
            OleDbConnection conn = new OleDbConnection(ConnectionString);
            OleDbCommand cmd = new OleDbCommand(sqlcheck, conn);
            conn.Open();
            OleDbDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                this.LabelReply.Text = rd["eng"].ToString();
                string reply = rd["reply"].ToString();
                reply = Input.Outputadd(reply);
                reply = reply + "\r\n" + Session["name"].ToString() + "  回复：";
                this.TextBoxReply.Text = reply;
            }
            ViewState["sqlid"] = sqlid;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Reply');</script>");
        }
        else
        {
            string myscript = @"alert('请选择回复项！');window.location.href='PersonalInform.aspx';";
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
        ViewState["sql"] = "Select * from GerenInform where [to] like '" + chaxunstr + "'or content like '" + chaxunstr + "'or eng like '" + chaxunstr + "'or shortdate like '" + chaxunstr + "'or reply like '" + chaxunstr + "' order by ID Desc";
        LoadData();
    }

    //添加内容函数
    protected void Add_OK_Click(object sender, EventArgs e)
    {
        if (this.DropDownListEng.SelectedIndex==0)
        {
            this.ToolTips1.Text = "交接对象不能为空！";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        else if (this.TextBoxGerenDate.Text == "")
        {
            this.ToolTips1.Text = "日期不能为空！";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        else if (this.TextBoxGeren.Text == "")
        {
            this.ToolTips1.Text = "交接内容不能为空！";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        else
        {
            string to = this.DropDownListEng.SelectedValue;
            string content = this.TextBoxGeren.Text.ToString();
            content = Input.Inputadd(content);
            string eng = Session["name"].ToString();
            string shortdate = this.TextBoxGerenDate.Text.ToString();
            string sql = "Insert into GerenInform ([to],content,eng,shortdate) values ('" + to + "','" + content + "','" + eng + "','" + shortdate + "')";
            if ((bool)ViewState["isedit"])
            {
                sql = "Update GerenInform Set [to]='" + to + "',content='" + content + "',eng='" + eng + "',shortdate='" + shortdate + "' where ID in "+ ViewState["sqlid"].ToString();
            }
            try
            {
                DbManager.ExecuteNonQuery(sql);
                string myscript = @"alert('发布成功！');window.location.href='PersonalInform.aspx';";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
            }
            catch
            {
                this.ToolTips2.Text = "发布失败，请与管理员联系！";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
            }

        }
    }
    protected void AddReply_OK_Click(object sender, EventArgs e)
    {
        string reply = this.TextBoxReply.Text.ToString();
        reply = Input.Inputadd(reply);
        string name = Session["name"].ToString();
        string sql = "Update GerenInform Set reply='" + reply + "' where ID in " + ViewState["sqlid"].ToString();
        try
        {
            DbManager.ExecuteNonQuery(sql);
            string myscript = @"alert('回复成功！');window.location.href='PersonalInform.aspx';";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
        }
        catch
        {
            this.ToolTips2.Text = "回复失败，请与管理员联系！";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Reply');</script>");
        }
    }
    protected void Add_Cancel_Click(object sender, EventArgs e)
    {
        ViewState["isedit"] = false;
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
    protected void Work_Click(object sender, EventArgs e)
    {
        Response.Redirect("DailyWork.aspx");
    }
    protected void Setup_Click(object sender, EventArgs e)
    {
        Response.Redirect("SetupReport.aspx");
    }
    protected void Task_Click(object sender, EventArgs e)
    {
        Response.Redirect("InformTask.aspx");
    }
      protected void personal_Click(object sender, EventArgs e)
    {
        Response.Redirect("personalInform.aspx");
    }


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