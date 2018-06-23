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

public partial class WebPages_AOIIssue : System.Web.UI.Page
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
            ViewState["sql"] = "Select * from AOIIssue Order by ID Desc";
            LoadData();
        }
    }

    protected void Add_Click(object sender, EventArgs e)
    {
        ToolTips.Text = "";
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
            string sqldelete = "Delete from AOIIssue where ID in" + sqlid;
            try
            {
                DbManager.ExecuteNonQuery(sqldelete);
                string myscript = @"alert('删除成功！');window.location.href='AOIIssue.aspx';";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
            }
            catch
            {
                string myscript = @"alert('删除失败，请与管理员联系！');window.location.href='AOIIssue.aspx';";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
            }
        }
        else
        {
            string myscript = @"alert('请选择删除项！');window.location.href='AOIIssue.aspx';";
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
            string sqlcheck = "Select * from AOIIssue where ID in " + sqlid;
            string ConnectionString = ConfigurationManager.ConnectionStrings["mdbcon"].ConnectionString;
            OleDbConnection conn = new OleDbConnection(ConnectionString);
            OleDbCommand cmd = new OleDbCommand(sqlcheck, conn);
            conn.Open();
            OleDbDataReader rd = cmd.ExecuteReader();
            bool isedit = false;
            while (rd.Read())
            {
                if (rd["eng"].ToString() == Session["name"].ToString() || Session["name"].ToString() == "程邵磊")
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
                DD_EQ.SelectedValue = rd["eq"].ToString();
                DD_Reasons.SelectedValue = rd["reason"].ToString();
                TB_Date.Text = rd["downtime"].ToString();
                TB_Hours.Text = rd["hours"].ToString();
                TB_Beizhu.Text = Input.Outputadd(rd["beizhu"].ToString());
                ViewState["sqlid"] = sqlid;
                ViewState["isedit"] = true;
                conn.Close();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
            }
            else
            {
                conn.Close();
                string myscript = @"alert('无权编辑他人信息！');window.location.href='AOIIssue.aspx';";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
            }
        }
        else
        {
            string myscript = @"alert('请选择编辑项！');window.location.href='AOIIssue.aspx';";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
        }
    }

    protected void Search_Click(object sender, EventArgs e)
    {
        string chaxunstr = TextBoxSearch.Text;
        chaxunstr.Replace("%", "[%]");
        chaxunstr.Replace("';", "';';");
        chaxunstr.Replace("[", "[[]");
        chaxunstr.Replace("_", "[_]");
        chaxunstr = "%" + chaxunstr + "%";
        string sql = "Select * from AOIIssue where eq like '"+chaxunstr+"' OR reason like '"+chaxunstr+"' OR beizhu like '"+chaxunstr+"' Order by ID Desc";
        LoadData();
    }

    protected void Add_OK_Click(object sender, EventArgs e)
    {
        if (DD_EQ.SelectedIndex == 0)
        {
            ToolTips.Text = "请选择EQ";
        }
        else if (DD_Reasons.SelectedIndex == 0)
        {
            ToolTips.Text = "请选择原因";
        }
        else if (TB_Date.Text == "")
        {
            ToolTips.Text = "请填写Down机时间";
        }
        else if (TB_Hours.Text == "")
        {
            ToolTips.Text = "请填写Down机时长";
        }
        else
        {
            string sql = "Insert into AOIIssue (eq,[downtime],hours,reason,beizhu,[inputdate],eng) values ('"
                +DD_EQ.SelectedValue+"',#"+TB_Date.Text+"#,'"+TB_Hours.Text+"','"+DD_Reasons.SelectedValue+"','"+
                Input.Inputadd(TB_Beizhu.Text)+"',#"+DateTime.Now.ToLocalTime()+"#,'"+Session["name"].ToString()+"')";
            if ((bool)ViewState["isedit"])
            {
                sql = "Update AOIIssue set eq='"+DD_EQ.SelectedValue+"',[downtime]=#"+TB_Date.Text+"#,hours='"+TB_Hours.Text
                    +"',reason='"+DD_Reasons.SelectedValue+"',beizhu='"+Input.Inputadd(TB_Beizhu.Text)+"',[inputdate]=#"+
                    DateTime.Now.ToLocalTime()+"#,eng='"+Session["name"].ToString()+"' where ID in ("+ViewState["sqlid"].ToString()+")";
            }
            try
            {
                DbManager.ExecuteNonQuery(sql);
                Response.Redirect("AOIIssue.aspx");
            }
            catch
            {
                ToolTips.Text = "发布失败！";
            }
        }
    }

    protected void Add_Cancel_Click(object sender, EventArgs e)
    {
        ViewState["isedit"] = false;
        Response.Redirect("AOIIssue.aspx");
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
