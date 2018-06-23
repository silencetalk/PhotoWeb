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
using PasswordManager;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.TipsNum.Visible = false;
            this.TipsPassword.Visible = false;
        }
        //Response.Redirect("LoginNewYear.aspx");
        this.TextBoxNum.Focus();
        this.TextBoxNum.Attributes.Add("onkeydown", "if(event.keyCode==13) {document.all." + this.Loginin.ClientID + ".focus();document.all." + this.Loginin.ClientID + ".click();}");
    }
    protected void Loginin_Click(object sender, EventArgs e)
    {
        if (this.TextBoxNum.Text == "")
        {
            this.TipsNum.Visible = true;
        }
        else if (this.TextBoxPassword.Text == "")
        {
            this.TipsPassword.Visible = true;
        }
        else
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["mdbcon"].ConnectionString;
            OleDbConnection con = new OleDbConnection(ConnectionString);
            OleDbCommand cmd = new OleDbCommand("select * from UserInfo where num='" + this.TextBoxNum.Text.ToString().Trim() + "'", con);
            if (con.State == ConnectionState.Closed)
                con.Open();
            OleDbDataReader sdr = cmd.ExecuteReader();
            try
            {
                if (sdr.Read())
                {
                    string password = sdr["password"].ToString();
                    if (password.Trim() == this.TextBoxPassword.Text.ToString().Trim())
                    {
                        Session["num"] = sdr["num"].ToString();
                        Session["name"] = sdr["username"].ToString();
                        Session["password"] = sdr["password"].ToString();
                        //PwdManager.CheckPwd();
                        if(sdr["keshi"].ToString()=="Exposure")
                            Response.Redirect("Exposure/WebPages/DailyInform.aspx");
                        else
                            Response.Redirect("WebPages/Index.aspx");
                    }
                    else
                    {
                        string myscript = @"alert('亲，密码不对哦！');window.location.href='Login.aspx';";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
                    }
                }
                else
                {
                    string myscript = @"alert('用户名错误或不存在！');window.location.href='Login.aspx';";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
                }
            }
            catch (Exception)//异常处理
            {
                string myscript = @"alert('不好意思，登录失败，请与管理员联系！');window.location.href='Login.aspx';";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
            }
            finally
            {
                sdr.Close();
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }
    }
    protected void Regist_Click(object sender, EventArgs e)
    {
        Response.Redirect("Regist.aspx");
    }
}
