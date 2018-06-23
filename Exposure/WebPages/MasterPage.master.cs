using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

public partial class WebPages_MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string loginis = (string)Session["name"];
        if ((loginis == null))
        {
            this.LabelName.Visible = false;
            string myscript = @"alert('亲们，请先登录！');window.location.href='../../Login.aspx';";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
        }
        this.LabelName.Text = "您好！" + loginis;
        this.LabelDate.Text = DateTime.Today.ToString("yyyy年M月d日  dddd", new System.Globalization.CultureInfo("zh-CN"));
    }
    protected void zhuxiao_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Redirect("../../Login.aspx");
    }
    protected void Xiugai_Click(object sender, EventArgs e)
    {
        this.ToolTips.Text = "";
        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('New_Password');</script>");
    }
    protected void New_PassWord_Click(object sender, EventArgs e)
    {
        if (this.TextBoxOldPW.Text == Session["password"].ToString())
        {
            if (this.TextBoxNewPW.Text == "")
            {
                this.ToolTips.Text = "<span style='font-family:fontawesome !important; color:Red;'>&#xf071;</span>新密码不能为空！";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('New_Password');</script>");
            }
            else if (this.TextBoxNewPW.Text != this.TextBoxNewPWAg.Text)
            {
                this.ToolTips.Text = "<span style='font-family:fontawesome !important; color:Red;'>&#xf071;</span>两次输入密码不匹配！";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('New_Password');</script>");
            }
            else
            {
                string password = this.TextBoxNewPW.Text.ToString();
                string sql = "Update UserInfo Set [password] ='"+password+ "' where username='"+ Session["name"].ToString()+"'";
                try
                {
                    ExpDbManager.ExecuteNonQuery(sql);
                    string myscript = @"alert('修改密码成功，请重新登录！');window.location.href='../Login.aspx';";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
                }
                catch
                {
                    string myscript = @"alert('修改密码失败，请与管理员联系！');window.location.href='Index.aspx';";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
                }
            }

        }
        else
        {
            this.ToolTips.Text = "<span style='font-family:fontawesome !important; color:Red;'>&#xf071;</span>密码不正确，无法修改！";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('New_Password');</script>");
        }
    }
    protected void Password_Cancel_Click(object sender, EventArgs e)
    {

    }
}
