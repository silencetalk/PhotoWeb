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

public partial class WebPages_Alarm : System.Web.UI.Page
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
            ViewState["sql"] = "Select * from CST Order by shortdate Desc";
            LoadData();
        }
        this.GridView1.Attributes.Add("style", "table-layout:fixed;word-wrap:break-word;");
        this.TextBoxSearch.Attributes.Add("onkeydown", "if(event.keyCode==13) {document.all." + this.Search.ClientID + ".focus();document.all." + this.Search.ClientID + ".click();}");
        this.Delete.Attributes.Add("onclick", "javascript:if(confirm('确定要删除吗?')){}else{return false;}");
    }

    //工具按钮函数

    protected void Add_Click(object sender, EventArgs e)
    {
        this.TextBoxCSTID.Text = "AACN-";
        this.TextBoxGlass.Text = "";
        this.ToolTips.Text = "";
        this.TextBoxBeizhu.Text = "";
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
            string sqldelete = "Delete from CST where ID in" + sqlid;
            try
                {
                    DbManager.ExecuteNonQuery(sqldelete);
                    string myscript = @"alert('删除成功！');window.location.href='CST.aspx';";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
                }
                catch
                {
                    string myscript = @"alert('删除失败，请与管理员联系！');window.location.href='CST.aspx';";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
                }
        }
        else
        {
            string myscript = @"alert('请选择删除项！');window.location.href='CST.aspx';";
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
            string sqlcheck = "Select * from CST where  ID in " + sqlid;
            string ConnectionString = ConfigurationManager.ConnectionStrings["mdbcon"].ConnectionString;
            OleDbConnection conn = new OleDbConnection(ConnectionString);
            OleDbCommand cmd = new OleDbCommand(sqlcheck, conn);
            conn.Open();
            OleDbDataReader rd = cmd.ExecuteReader();
             while (rd.Read())
                {
                    this.TextBoxCSTID.Text = rd["cstid"].ToString();
                    string beizhu = rd["beizhu"].ToString();
                    beizhu = Input.Outputadd(beizhu);
                    this.TextBoxBeizhu.Text = beizhu;
                    string glass= rd["glass"].ToString();
                    glass = Input.Outputadd(glass);
                    this.TextBoxGlass.Text = glass;
                }
                ViewState["sqlid"] = sqlid;
                ViewState["isedit"] = true;
                conn.Close();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        else
        {
            string myscript = @"alert('请选择编辑项！');window.location.href='CST.aspx';";
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
        ViewState["sql"] = "Select * from CST where cstid like '" + chaxunstr + "'or glass like '" + chaxunstr + "'or beizhu like '" + chaxunstr + "'or [shortdate] like '" + chaxunstr + "' order by shortdate Desc";
        LoadData();
    }

    //添加数据函数

    protected void Add_OK_Click(object sender, EventArgs e)
    {
        if (this.TextBoxCSTID.Text == "")
        {
            this.ToolTips.Text = "<span style='font-family:fontawesome !important; color:Red;'>&#xf071;</span>CSTID不能为空！";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        else if (this.TextBoxGlass.Text == "")
        {
            this.ToolTips.Text = "<span style='font-family:fontawesome !important; color:Red;'>&#xf071;</span>Glass信息不能为空！";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        else
        {
            string cstid = this.TextBoxCSTID.Text.ToString();
            string beizhu = this.TextBoxBeizhu.Text.ToString();
            beizhu = Input.Inputadd(beizhu);
            string glass = this.TextBoxGlass.Text.ToString();
            glass = Input.Inputadd(glass);
            string sql = "Insert Into CST (cstid,glass,beizhu,[shortdate]) values ('" + cstid + "','" + glass+ "','" + beizhu+ "',#" + DateTime.Now.ToLocalTime() + "#)";
            if ((bool)ViewState["isedit"])
             {

                    sql = "Update CST set cstid='" +cstid + "', glass='" + glass + "',beizhu='" + beizhu + "', [shortdate]=#" + DateTime.Now.ToLocalTime() + "# where ID in" + ViewState["sqlid"].ToString();
             }
            try
            {
                DbManager.ExecuteNonQuery(sql);
                string myscript = @"alert('发布成功！');window.location.href='CST.aspx';";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
            }
            catch
            {
                string myscript = @"alert('发布失败，请与管理员联系！');window.location.href='CST.aspx';";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
            }
        }
    }
    protected void Add_Cancel_Click(object sender, EventArgs e)
    {
        ViewState["isedit"] = false;
    }

    //跳转页面函数
    protected void Alarm_Click(object sender, EventArgs e)
    {
        Response.Redirect("Alarm.aspx");
    }
    protected void Parts_Click(object sender, EventArgs e)
    {
        Response.Redirect("Parts.aspx");
    }
    protected void PM_Click(object sender, EventArgs e)
    {
        Response.Redirect("PM.aspx");
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
