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

public partial class WebPages_Alarm : System.Web.UI.Page
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
            ViewState["sql"] = "Select * from Alarm Order by downtime Desc";
            LoadData();
        }
        this.TextBoxSearch.Attributes.Add("onkeydown", "if(event.keyCode==13) {document.all." + this.Search.ClientID + ".focus();document.all." + this.Search.ClientID + ".click();}");
        this.Delete.Attributes.Add("onclick", "javascript:if(confirm('确定要删除吗?')){}else{return false;}");
    }

    //工具按钮函数

    protected void Add_Click(object sender, EventArgs e)
    {
        this.ToolTips.Text = "";
        this.DropDownListJitai.SelectedIndex = 0;
        this.DropDownListDanyuan.SelectedIndex = 0;
        this.TextBoxDownTime.Text = DateTime.Now.ToString();
        this.TextBoxHours.Text = "";
        this.TextBoxAlarm.Text = "";
        this.TextBoxFenxi.Text = "";
        this.TextBoxAction.Text = "";
        this.TextBoxYingxiang.Text = "";
        this.TextBoxFileTittle.Text = "Down机报告";
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
            string sqldelete = "Delete from Alarm where ID in" + sqlid;
            string sqlcheck = "Select * from Alarm where eng like '%" + Session["name"].ToString() + "%' AND ID in " + sqlid;
            string ConnectionString = ConfigurationManager.ConnectionStrings["expmdbcon"].ConnectionString;
            OleDbConnection conn = new OleDbConnection(ConnectionString);
            OleDbCommand cmd = new OleDbCommand(sqlcheck, conn);
            conn.Open();
            OleDbDataReader rd = cmd.ExecuteReader();
            string filepath = "";
            string alarmname = "";
            bool isdelete = false;
            if (rd.HasRows || Session["name"].ToString() == "葫芦娃")
            {
                isdelete = true;
            }
            while (rd.Read())
            {
                filepath = rd["fujian"].ToString();
                alarmname = rd["alarm"].ToString();
            }
            if (isdelete)
            {
                conn.Close();
                try
                {
                    if (File.Exists(filepath))
                    {
                        File.Delete(filepath);
                        string filesql = "Delete from UploadFiles where path='" + filepath + "'";
                        ExpDbManager.ExecuteNonQuery(filesql);
                    }
                    ExpDbManager.ExecuteNonQuery(sqldelete);
                    sqlcheck = "Select alarm from Alarm where alarm ='" + alarmname + "'";
                    ConnectionString = ConfigurationManager.ConnectionStrings["expmdbcon"].ConnectionString;
                    OleDbCommand cmd1 = new OleDbCommand(sqlcheck, conn);
                    conn.Open();
                    OleDbDataReader rd1 = cmd1.ExecuteReader();
                    int count = 0;
                    while (rd1.Read())
                    {
                        count += 1;
                    }
                    string updatesql = "Update Alarm set [cishu]=" + count + " where alarm='" + alarmname + "'";
                    ExpDbManager.ExecuteNonQuery(updatesql);
                    string myscript = @"alert('删除成功！');window.location.href='Alarm.aspx';";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
                }
                catch
                {
                    string myscript = @"alert('删除失败，请与管理员联系！');window.location.href='Alarm.aspx';";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
                }
            }
            else
            {
                conn.Close();
                string myscript = @"alert('无权删除他人信息！');window.location.href='Alarm.aspx';";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
            }
        }
        else
        {
            string myscript = @"alert('请选择删除项！');window.location.href='Alarm.aspx';";
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
            string sqlcheck = "Select * from Alarm where eng like '%" + Session["name"].ToString() + "%' AND ID in " + sqlid;
            string ConnectionString = ConfigurationManager.ConnectionStrings["expmdbcon"].ConnectionString;
            OleDbConnection conn = new OleDbConnection(ConnectionString);
            OleDbCommand cmd = new OleDbCommand(sqlcheck, conn);
            conn.Open();
            OleDbDataReader rd = cmd.ExecuteReader();
            bool isedit = false;
            if (rd.HasRows || Session["name"].ToString() == "葫芦娃")
            {
                isedit = true;
            }
            if (isedit)
            {
                 while( rd.Read())
                {
                    this.DropDownListJitai.SelectedValue = rd["jitai"].ToString();
                    this.DropDownListDanyuan.SelectedValue = rd["danyuan"].ToString();
                    if (rd["fenlei"].ToString() != "")
                    {
                        this.DropDownListFenlei.SelectedValue = rd["fenlei"].ToString();
                    }
                    this.TextBoxAlarm.Text = rd["alarm"].ToString();
                    this.TextBoxDownTime.Text = rd["downtime"].ToString();
                    this.TextBoxHours.Text = rd["hours"].ToString();
                    this.TextBoxFenxi.Text = Input.Outputadd(rd["fenxi"].ToString());
                    this.TextBoxAction.Text = Input.Outputadd(rd["action"].ToString());
                    this.TextBoxYingxiang.Text = Input.Outputadd(rd["yingxiang"].ToString());
                }
                ViewState["sqlid"] = sqlid;
                ViewState["isedit"] = true;
                conn.Close();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
            }
        else
            {
                conn.Close();
                string myscript = @"alert('无权编辑他人信息！');window.location.href='Alarm.aspx';";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
            }
        }
        else
        {
            string myscript = @"alert('请选择编辑项！');window.location.href='Alarm.aspx';";
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
        ViewState["sql"] = "Select * from Alarm where jitai like '" + chaxunstr + "'or danyuan like '" + chaxunstr + "'or [downtime] like '" + chaxunstr + "'or [hours] like '" + chaxunstr + "'or fenlei like '" + chaxunstr + "'or title like '" + chaxunstr + "'or fenxi like '" + chaxunstr + "'or [action] like '" + chaxunstr + "'or yingxiang like '" + chaxunstr + "' order by downtime Desc";
        LoadData();
    }

    //添加数据函数

    protected void Add_OK_Click(object sender, EventArgs e)
    {
        if (this.DropDownListJitai.SelectedIndex==0)
        {
            this.ToolTips.Text = "<span style='font-family:fontawesome !important; color:Red;'>&#xf071;</span>请选择机台！";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        else if(this.DropDownListDanyuan.SelectedIndex==0)
        {
            this.ToolTips.Text = "<span style='font-family:fontawesome !important; color:Red;'>&#xf071;</span>请选择单元！";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        else if (this.TextBoxHours.Text == "")
        {
            this.ToolTips.Text = "<span style='font-family:fontawesome !important; color:Red;'>&#xf071;</span>Down机时间不能为空！";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        else if (this.TextBoxAlarm.Text == "")
        {
            this.ToolTips.Text = "<span style='font-family:fontawesome !important; color:Red;'>&#xf071;</span>Alarm名称不能为空！";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        else if (this.TextBoxFenxi.Text == "")
        {
            this.ToolTips.Text = "<span style='font-family:fontawesome !important; color:Red;'>&#xf071;</span>Alarm分析不能为空！";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        else if (this.TextBoxAction.Text == "")
        {
            this.ToolTips.Text = "<span style='font-family:fontawesome !important; color:Red;'>&#xf071;</span>Action内容不能为空！";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        else
        {
            bool isadd = false;
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
            if (isadd)
            {
                string alarm = this.TextBoxAlarm.Text.ToString();
                string jitai = this.DropDownListJitai.SelectedValue;
                string danyuan = this.DropDownListDanyuan.SelectedValue;
                string hours = this.TextBoxHours.Text.ToString();
                string downtime = this.TextBoxDownTime.Text.ToString();
                DateTime dt=Convert.ToDateTime(downtime);
                string fenxi = Input.Inputadd(this.TextBoxFenxi.Text.ToString());
                string action = Input.Inputadd(this.TextBoxAction.Text.ToString());
                string yingxiang = Input.Inputadd(this.TextBoxYingxiang.Text.ToString());
                string eng = Session["name"].ToString();
                string sql = "Insert into Alarm (jitai,danyuan,alarm,[downtime],hours,fenxi,[action],yingxiang,eng) values ('" + jitai + "','" + danyuan + "','" + alarm + "',#" + downtime + "#,'" + hours + "','" + fenxi+"','"+action+"','"+yingxiang+"','"+eng+"')";   
                if (this.FileUpload1.HasFile)
                {
                    string fenlei = this.DropDownListFenlei.SelectedValue;
                    string title = this.TextBoxFileTittle.Text.ToString();
                    string path = "~/Exposure/UploadFiles/" + this.DropDownListFenlei.SelectedValue + "/";
                    path = Server.MapPath(path);
                    string newname = DateTime.Now.ToString("yyyyMMddHHmmss") + FileUpload1.FileName;
                    newname = newname.Replace("#", " ");
                    newname = newname.Replace("&", " ");
                    path = path + newname;
                    string filename = this.FileUpload1.FileName;
                    filename = "<br/>" + filename + "<p></p>";
                    sql = "Insert into Alarm (jitai,danyuan,fenlei,alarm,[downtime],hours,fenxi,[action],yingxiang,eng,title,fujian) values ('" + jitai + "','" + danyuan + "','" + fenlei + "','" + alarm + "',#" + downtime + "#,'" + hours + "','" + fenxi + "','" + action + "','" + yingxiang + "','" + eng + "','" + title + "','" + path + "')";
                    string filesql = "Insert into UploadFiles (fenlei,title,path,filename,shortdate,eng) values ('"+fenlei+"','"+title+"','"+path+"','"+filename+"','"+dt.ToShortDateString()+"','"+ eng+"')";
                    if ((bool)ViewState["isedit"])
                    {
                        string sqlcheck = "Select fujian from Alarm where ID in" + ViewState["sqlid"].ToString();
                        string ConnectionString = ConfigurationManager.ConnectionStrings["expmdbcon"].ConnectionString;
                        OleDbConnection conn = new OleDbConnection(ConnectionString);
                        OleDbCommand cmd = new OleDbCommand(sqlcheck, conn);
                        conn.Open();
                        OleDbDataReader rd = cmd.ExecuteReader();
                        rd.Read();
                        string oldfile = rd["fujian"].ToString();
                        rd.Close();
                        conn.Close();
                        string deletesql = "Delete from UploadFiles where path='" + oldfile + "'";
                        if (File.Exists(oldfile))
                        {
                            File.Delete(oldfile);
                            ExpDbManager.ExecuteNonQuery(deletesql);
                        }
                        sql = "Update Alarm Set jitai='" + jitai + "',danyuan='" + danyuan + "',alarm='" + alarm + "',[downtime]=#" + downtime + "#,hours='" + hours + "',fenxi='" + fenxi + "',fenlei='" + fenlei + "',[action]='" + action + "',yingxiang='" + yingxiang + "',fujian='" + path + "',title='" + title + "' where ID in" + ViewState["sqlid"].ToString();
                    }
                    try
                    {
                        ExpDbManager.ExecuteNonQuery(sql);
                        string sqlcheck = "Select alarm from Alarm where alarm ='" + alarm+"'";
                        string ConnectionString = ConfigurationManager.ConnectionStrings["expmdbcon"].ConnectionString;
                        OleDbConnection conn = new OleDbConnection(ConnectionString);
                        OleDbCommand cmd = new OleDbCommand(sqlcheck, conn);
                        conn.Open();
                        OleDbDataReader rd = cmd.ExecuteReader();
                        int count = 0;
                        while (rd.Read())
                        {
                            count += 1;
                        }
                        string updatesql = "Update Alarm set [cishu]="+count+" where alarm='"+alarm+"'";
                        ExpDbManager.ExecuteNonQuery(updatesql);
                        this.FileUpload1.PostedFile.SaveAs(path);
                        ExpDbManager.ExecuteNonQuery(filesql);
                        string myscript = @"alert('发布成功！');window.location.href='Alarm.aspx';";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
                    }
                    catch
                    {
                        string myscript = @"alert('发布失败，请与管理员联系！');window.location.href='Alarm.aspx';";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
                    }
                }
                else
                {
                    if ((bool)ViewState["isedit"])
                    {
                        sql = "Update Alarm Set jitai='"+jitai+"',danyuan='"+danyuan+"',alarm='"+alarm+"',[downtime]=#"+downtime+"#,hours='"+hours+"',fenxi='"+fenxi+"',[action]='"+action+"',yingxiang='"+yingxiang+"' where ID in"+ViewState["sqlid"].ToString();
                    }
                    try
                    {
                        ExpDbManager.ExecuteNonQuery(sql);
                        string sqlcheck = "Select alarm from Alarm where alarm ='" + alarm + "'";
                        string ConnectionString = ConfigurationManager.ConnectionStrings["expmdbcon"].ConnectionString;
                        OleDbConnection conn = new OleDbConnection(ConnectionString);
                        OleDbCommand cmd = new OleDbCommand(sqlcheck, conn);
                        conn.Open();
                        OleDbDataReader rd = cmd.ExecuteReader();
                        int count = 0;
                        while (rd.Read())
                        {
                            count += 1;
                        }
                        string updatesql = "Update Alarm set [cishu]=" + count + " where alarm='" + alarm + "'";
                        ExpDbManager.ExecuteNonQuery(updatesql);
                        string myscript = @"alert('发布成功！');window.location.href='Alarm.aspx';";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
                    }
                    catch
                    {
                        string myscript = @"alert('发布失败，请与管理员联系！');window.location.href='Alarm.aspx';";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
                    }
                }
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
    protected void CST_Click(object sender, EventArgs e)
    {
        Response.Redirect("CST.aspx");
    }
    protected void PM_Click(object sender, EventArgs e)
    {
    }
    protected void Filter_Click(object sender, EventArgs e)
    {
    }
    protected void EQ_Click(object sender, EventArgs e)
    {

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