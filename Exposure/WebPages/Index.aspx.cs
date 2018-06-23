using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.OleDb;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class Index_Index : System.Web.UI.Page
{

    private void LoadData()
    {
        string sql = ViewState["sql"].ToString();
        DataTable dt = ExpDbManager.ExecuteQuery(sql);
        this.GridView1.DataSource = dt.DefaultView;
        this.GridView1.DataBind();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.TextBoxInput.Text = "";
            ViewState["sql"] = "Select * from Gonggao Order By ID Desc";
            ViewState["isedit"] = false;
            ViewState["sqlid"] = "";
            LoadData();           
        }
        this.ToolTips.Text = "字数请不要超过900字！";
        this.TextBoxSearch.Attributes.Add("onkeydown", "if(event.keyCode==13) {document.all." + this.Search.ClientID + ".focus();document.all." + this.Search.ClientID + ".click();}");
        this.Delete.Attributes.Add("onclick", "javascript:if(confirm('确定要删除吗?')){}else{return false;}");
    }
    protected void Add_Click(object sender, EventArgs e)
    {
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
                sqlid= sqlid + Convert.ToInt32(GridView1.DataKeys[i].Value);
                break;
            }
        }
        if (sqlid != "")
        {
            sqlid = "(" + sqlid + ")";
            string sqldelete = "Delete from Gonggao where ID in"+sqlid;
            string sqlcheck = "Select Eng from Gonggao where ID in"+sqlid;
            string ConnectionString = ConfigurationManager.ConnectionStrings["expmdbcon"].ConnectionString;
            OleDbConnection conn = new OleDbConnection(ConnectionString);
            OleDbCommand cmd = new OleDbCommand(sqlcheck, conn);
            conn.Open();
            OleDbDataReader rd = cmd.ExecuteReader();
            bool isdelete = false;
            while (rd.Read())
            {
                if (((string)rd["Eng"] == (string)Session["name"]) || ((string)Session["name"] == "程邵磊"))
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
                    int i = ExpDbManager.ExecuteNonQuery(sqldelete);
                    string myscript = @"alert('删除成功！');window.location.href='Index.aspx';";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
                }
                catch
                {
                    string myscript = @"alert('删除失败，请与管理员联系！');window.location.href='Index.aspx';";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
                }
            }
            else
            {
                string myscript = @"alert('无权删除他人信息！');window.location.href='Index.aspx';";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
            }

            
        }
        else
        {
            string myscript = @"alert('请选择删除项！');window.location.href='Index.aspx';";
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
            string sqlcheck = "Select Eng,Content from Gonggao where ID in" + sqlid;
            string ConnectionString = ConfigurationManager.ConnectionStrings["expmdbcon"].ConnectionString;
            OleDbConnection conn = new OleDbConnection(ConnectionString);
            OleDbCommand cmd = new OleDbCommand(sqlcheck, conn);
            conn.Open();
            OleDbDataReader rd = cmd.ExecuteReader();
            bool isedit = false;
            while (rd.Read())
            {
                if (((string)rd["Eng"] == (string)Session["name"]) || ((string)Session["name"] == "程邵磊"))
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
               string content = (string)rd["Content"];
               content = Input.Outputadd(content);
               this.TextBoxInput.Text=content.Trim();
               ViewState["isedit"]= true;
               ViewState["sqlid"] = sqlid;
               conn.Close();
               Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
            }
            else
            {
                conn.Close();
                string myscript = @"alert('无权编辑他人信息！');window.location.href='Index.aspx';";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
            }
        }
        else
        {
            string myscript = @"alert('请选择编辑项！');window.location.href='Index.aspx';";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
        }
    }
    protected void Search_Click(object sender, EventArgs e)
    {
        string chaxunstr = this.TextBoxSearch.Text.ToString().Trim();
        chaxunstr.Replace("%", "[%]");
        chaxunstr.Replace("';", "';';");
        chaxunstr.Replace("[", "[[]");
        chaxunstr.Replace("_", "[_]");
        chaxunstr = "%" + chaxunstr + "%";
        ViewState["sql"] = "select * from Gonggao where Shortdate LIKE '" + chaxunstr + "'OR Content LIKE '" + chaxunstr + "' OR Eng LIKE '" + chaxunstr + "' OR Fabiaodate LIKE '" + chaxunstr + "' Order by ID Desc";
        LoadData();
    }
    protected void AddConfirm_Click(object sender, EventArgs e)
    {
        if (this.TextBoxInput.Text == "")
        {
            this.ToolTips.Text = "内容不能为空！";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        else
        {
            string content = this.TextBoxInput.Text.ToString();
            content = Input.Inputadd(content);
            string shortdate = DateTime.Now.ToShortDateString();
            string eng = (string)Session["name"];
            string sql = "Insert into Gonggao (Shortdate,Content,Eng,Fabiaodate) Values ('" + shortdate + "','" + content + "','" + eng + "',#" + DateTime.Now.ToLocalTime() + "#)";
            if ((bool)ViewState["isedit"])
            {
                sql = "Update Gonggao Set Shortdate='" + shortdate + "',Content='" + content + "',Eng='" + eng + "',Fabiaodate=#" + DateTime.Now.ToLocalTime() + "# where ID in" + (string)ViewState["sqlid"];
            }
            try
            {
                ExpDbManager.ExecuteNonQuery(sql);
                string myscript = @"alert('发布成功！');window.location.href='Index.aspx';";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
            }
                catch
            {
                string myscript = @"alert('发布失败，请与管理员联系！');window.location.href='Index.aspx';";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
            }
        }
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