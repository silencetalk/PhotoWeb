using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Regist : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Regist_Click(object sender, EventArgs e)
    {
        string name = this.TextBoxName.Text.ToString().Trim();
        string num = this.TextBoxNum.Text.ToString().Trim();
        string password = this.TextBoxPassword.Text.ToString().Trim();
        string email = this.TextBoxEmail.Text.ToString().Trim();
        string phone = this.TextBoxPhone.Text.ToString().Trim();
        string sql = "Insert into UserInfo (username,num,[password],email,phone) VALUES ('" + name + "','" + num + "','" + password + "','" + email + "','" + phone + "')";
        Check ck=new Check();
        bool isname = ck.CK("username",name);
        bool isnum = ck.CK("num", num);
        bool isemail = ck.CK("email", email);
        bool isphone = ck.CK("phone", phone);
        if (isname)
        {
            string myscript = @"alert('该用户名已被注册！');window.location.href='Regist.aspx';";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
        }
        else if (isnum)
        {
            string myscript = @"alert('该账号已被注册！');window.location.href='Regist.aspx';";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
        }
        else if (isemail)
        {
            string myscript = @"alert('该邮箱已被注册！');window.location.href='Regist.aspx';";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
        }
        else if(isphone)
        {
            string myscript = @"alert('该号码已被注册！');window.location.href='Regist.aspx';";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
        }
        else
        {
          try
            {
               DbManager.ExecuteNonQuery(sql);
               string myscript = @"alert('注册成功，请返回登录！');window.location.href='Login.aspx';";
               Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
           }
            catch
          {
              string myscript = @"alert('不好意思，注册失败请与管理员联系！');window.location.href='Regist.aspx';";
              Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
           }
        }
    }
}