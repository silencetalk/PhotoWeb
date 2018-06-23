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

public partial class WebPages_GongsiPerson : System.Web.UI.Page
{
    protected void LoadData()
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
            ViewState["isedit"] = false;
            ViewState["sqlid"] = "";
            ViewState["sql"] = "Select * from Gongsi Order by ID Desc";
            LoadData();
        }
        this.TextBoxSearch.Attributes.Add("onkeydown", "if(event.keyCode==13) {document.all." + this.Search.ClientID + ".focus();document.all." + this.Search.ClientID + ".click();}");
        this.Delete.Attributes.Add("onclick", "javascript:if(confirm('确定要删除吗?')){}else{return false;}");
    }

    protected void Add_Click(object sender, EventArgs e)
    {
        this.TextBoxName.Text = "";
        this.TextBoxNum.Text = "";
        this.TextBoxKeshi.Text = "";
        this.TextBoxBumen.Text = "";
        this.TextBoxYewu.Text = "";
        this.TextBoxPhone.Text = "";
        this.TextBoxemail.Text = "";
        this.ToolTips.Text = "";
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
            string sqldelete = "Delete from Gongsi where ID in" + sqlid;
            try
            {
                ExpDbManager.ExecuteNonQuery(sqldelete);
                string myscript = @"alert('删除成功！');window.location.href='GongsiPerson.aspx';";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
            }
            catch
            {
                string myscript = @"alert('删除失败，请与管理员联系！');window.location.href='GongsiPerson.aspx';";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
            }
        }
        else
        {
            string myscript = @"alert('请选择删除项！');window.location.href='GongsiPerson.aspx';";
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
            string sqlcheck = "Select * from Gongsi where  ID in " + sqlid;
            string ConnectionString = ConfigurationManager.ConnectionStrings["expmdbcon"].ConnectionString;
            OleDbConnection conn = new OleDbConnection(ConnectionString);
            OleDbCommand cmd = new OleDbCommand(sqlcheck, conn);
            conn.Open();
            OleDbDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                this.TextBoxName.Text = rd["pname"].ToString();
                this.TextBoxNum.Text = rd["num"].ToString();
                this.TextBoxBumen.Text = rd["bumen"].ToString();
                this.TextBoxKeshi.Text = rd["keshi"].ToString();
                this.TextBoxYewu.Text = rd["yewu"].ToString();
                this.TextBoxPhone.Text = rd["phone"].ToString();
                this.TextBoxemail.Text = rd["email"].ToString();
            }
            ViewState["sqlid"] = sqlid;
            ViewState["isedit"] = true;
            conn.Close();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        else
        {
            string myscript = @"alert('请选择编辑项！');window.location.href='GongsiPerson.aspx';";
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
        ViewState["sql"] = "Select * from Gongsi where pname like '" + chaxunstr + "'or num like '" + chaxunstr + "'or bumen like '" + chaxunstr + "'or keshi like '" + chaxunstr + "'or yewu like '" + chaxunstr + "'or phone like '" + chaxunstr + "'or email like '" + chaxunstr + "' order by ID Desc";
        LoadData();
    }

    //添加数据函数

    protected void Add_OK_Click(object sender, EventArgs e)
    {
        if (this.TextBoxName.Text == "")
        {
            this.ToolTips.Text = "<span style='font-family:fontawesome !important; color:Red;'>&#xf071;</span>姓名不能为空！";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        else
        {
            string name = this.TextBoxName.Text.ToString();
            string bumen = this.TextBoxBumen.Text.ToString();
            string keshi = this.TextBoxKeshi.Text.ToString();
            string num = this.TextBoxNum.Text.ToString();
            string yewu = this.TextBoxYewu.Text.ToString();
            string phone = this.TextBoxPhone.Text.ToString();
            string email = this.TextBoxemail.Text.ToString();
            string sql = "Insert Into Gongsi (pname,bumen,keshi,num,yewu,phone,email) values ('" + name + "','" + bumen + "','" + keshi + "','" + num + "','" +yewu+ "','" +phone+ "','" +email+ "')" ;
            if ((bool)ViewState["isedit"])
            {

                sql = "Update Gongsi set pname='" + name + "', bumen='" + bumen + "',keshi='" + keshi + "', num='" + num + "',yewu='" + yewu + "',phone='" + phone + "',email='" + email + "' where ID in" + ViewState["sqlid"].ToString();
            }
            try
            {
                ExpDbManager.ExecuteNonQuery(sql);
                string myscript = @"alert('发布成功！');window.location.href='GongsiPerson.aspx';";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
            }
            catch
            {
                string myscript = @"alert('发布失败，请与管理员联系！');window.location.href='GongsiPerson.aspx';";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
            }
        }
    }
    protected void Add_Cancel_Click(object sender, EventArgs e)
    {
        ViewState["isedit"] = false;
    }
    //跳转页面函数
    protected void Keshi_Click(object sender, EventArgs e)
    {
        Response.Redirect("UserInfo.aspx");
    }
    protected void Changshang_Click(object sender, EventArgs e)
    {
        Response.Redirect("Changshang.aspx");
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
