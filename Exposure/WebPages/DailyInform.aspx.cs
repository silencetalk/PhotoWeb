using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class WebPages_DailyInform : System.Web.UI.Page
{
    //全局变量
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["isedit"] = false;
            ViewState["sqlid"] = "";
            CheckDate();
            ViewState["sql"] = "Select * from DailyInform where shortdate= '" + ViewState["date"].ToString() + "' Order by lindex,banci";
            LoadData();
        }
        //if (this.GridView1.Columns.Count == 7)
        //{
        //    this.GridView1.Columns.RemoveAt(6);
        //}
        this.GridView1.Attributes.Add("style", "table-layout:fixed;word-wrap:break-word;");
        this.TextBoxSearch.Attributes.Add("onkeydown", "if(event.keyCode==13) {document.all." + this.Search.ClientID + ".focus();document.all." + this.Search.ClientID + ".click();}");
        this.Delete.Attributes.Add("onclick", "javascript:if(confirm('确定要删除吗?')){}else{return false;}");
    }

    //数据处理函数
    protected void CheckDate()
    {
        string sql = "Select top 1 * from DailyInform Order by ID Desc";
        string ConnectionString = ConfigurationManager.ConnectionStrings["expmdbcon"].ConnectionString;
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
            string myscript = @"alert('加载数据出错！');window.location.href='DailyInform.aspx';";
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
        DataTable dt = ExpDbManager.ExecuteQuery(sql);
        this.GridView1.DataSource = dt.DefaultView;
        this.GridView1.DataBind();
    }


    //工具按钮函数
    protected void PreDate_Click(object sender, EventArgs e)
    {
        DateTime dt = Convert.ToDateTime(this.LabelDate.Text.ToString());
        ViewState["date"] = dt.AddDays(-1).ToShortDateString();
        this.LabelDate.Text = ViewState["date"].ToString();
        ViewState["sql"] = "Select * from DailyInform where shortdate= '" + ViewState["date"] + "' Order by lindex,banci";
        LoadData();
    }
    protected void SetDate_Click(object sender, EventArgs e)
    {
        CheckDate();
        ViewState["sql"] = "Select * from DailyInform where shortdate= '" + ViewState["date"].ToString() + "' Order by lindex,banci";
        LoadData();
    }
    protected void NextDate_Click(object sender, EventArgs e)
    {
        DateTime dt = Convert.ToDateTime(this.LabelDate.Text.ToString());
        ViewState["date"] = dt.AddDays(1).ToShortDateString();
        this.LabelDate.Text = ViewState["date"].ToString();
        ViewState["sql"] = "Select * from DailyInform where shortdate= '" + ViewState["date"].ToString() + "' Order by lindex,banci";
        LoadData();
    }
    protected void Add_Click(object sender, EventArgs e)
    {
        this.ToolTips.Text = "";
        this.DropDownListJitai.SelectedIndex = 0;
        this.TextBoxDailyInform.Text = "";
        this.TextBoxEng.Text = Session["name"].ToString();
        this.TextBoxFileTittle.Text = "";
        int hour = DateTime.Now.Hour;
        if (hour < 9)
        {
            this.DropDownListInputBanci.SelectedIndex = 0;
            this.TextBoxInputDate.Text = DateTime.Now.AddDays(-1).ToShortDateString();
        }
        else
        {
            this.DropDownListInputBanci.SelectedIndex = 1;
            this.TextBoxInputDate.Text = DateTime.Now.ToShortDateString();
        }
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
            string sqldelete = "Delete from DailyInform where ID in" + sqlid;
            string sqlcheck = "Select eng,fujian from DailyInform where  ID in " + sqlid;
            string ConnectionString = ConfigurationManager.ConnectionStrings["expmdbcon"].ConnectionString;
            OleDbConnection conn = new OleDbConnection(ConnectionString);
            OleDbCommand cmd = new OleDbCommand(sqlcheck, conn);
            conn.Open();
            OleDbDataReader rd = cmd.ExecuteReader();
            string filepath = "";
            while (rd.Read())
            {
                filepath = rd["fujian"].ToString();
            }
            conn.Close();
                try
                {
                    int i = ExpDbManager.ExecuteNonQuery(sqldelete);
                    if (File.Exists(filepath))
                    {
                        string filesql = "Delete from UploadFiles where path='" + filepath + "'";
                        ExpDbManager.ExecuteNonQuery(filesql);
                        File.Delete(filepath);
                    }
                    string myscript = @"alert('删除成功！');window.location.href='DailyInform.aspx';";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
                }
                catch
                {
                    string myscript = @"alert('删除失败，请与管理员联系！');window.location.href='DailyInform.aspx';";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
                }
        }
        else
        {
            string myscript = @"alert('请选择删除项！');window.location.href='DailyInform.aspx';";
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
            string sqlcheck = "Select * from DailyInform where ID in " + sqlid;
            string ConnectionString = ConfigurationManager.ConnectionStrings["expmdbcon"].ConnectionString;
            OleDbConnection conn = new OleDbConnection(ConnectionString);
            OleDbCommand cmd = new OleDbCommand(sqlcheck, conn);
            conn.Open();
            OleDbDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                this.DropDownListJitai.SelectedValue = rd["jitai"].ToString().Trim();
                this.DropDownListInputBanci.SelectedValue = rd["banci"].ToString().Trim();
                string content = rd["content"].ToString();
                content = Input.Outputadd(content);
                this.TextBoxDailyInform.Text = content;
                this.TextBoxInputDate.Text = rd["shortdate"].ToString();
                this.TextBoxEng.Text =Input.Outputadd(rd["eng"].ToString());
            }
                ViewState["sqlid"] = sqlid;
                ViewState["isedit"] = true;
                conn.Close();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
          }
        else
        {
            string myscript = @"alert('请选择编辑项！');window.location.href='DailyInform.aspx';";
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
        ViewState["sql"] = "Select * from DailyInform where jitai like '" + chaxunstr + "'or banci like '" + chaxunstr + "'or content like '" + chaxunstr + "'or eng like '" + chaxunstr + "'or shortdate like '" + chaxunstr + "'or reply like '" + chaxunstr + "' order by lindex,banci";
        LoadData();
    }

    //添加内容函数
    protected void AddDI_OK_Click(object sender, EventArgs e)
    {
        bool isadd = false;
        if (this.DropDownListJitai.SelectedIndex == 0)
        {
            this.ToolTips.Text = "<span style='font-family:fontawesome !important; color:Red;'>&#xf071;</span>请选择项目！";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        else if (this.TextBoxDailyInform.Text == "")
        {
            this.ToolTips.Text = "<span style='font-family:fontawesome !important; color:Red;'>&#xf071;</span>内容不能为空！";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        else if (this.TextBoxInputDate.Text == "")
        {
            this.ToolTips.Text = "<span style='font-family:fontawesome !important; color:Red;'>&#xf071;</span>日期不能为空！";
        }
        else if (this.FileUpload1.HasFile)
        {
            if (this.DropDownListFenlei.SelectedIndex == 0)
            {
                this.ToolTips.Text = "<span style='font-family:fontawesome !important; color:Red;'>&#xf071;</span>请选择文档分类！";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
            }
            else if (this.TextBoxFileTittle.Text == "")
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
                try
                {
                    DateTime dt = Convert.ToDateTime(this.TextBoxInputDate.Text.ToString());
                    isadd = true;
                }
                catch
                {
                    this.ToolTips.Text = "<span style='font-family:fontawesome !important; color:Red;'>&#xf071;</span>日期格式不对（YYYY/MM/DD）！";
                }
            }
        }
        else
        {
            isadd = true;
        }
        if (isadd)
        {
            string jitai = this.DropDownListJitai.SelectedValue;
            int lindex = this.DropDownListJitai.SelectedIndex;
            string banci = this.DropDownListInputBanci.SelectedValue;
            string informdate = this.TextBoxInputDate.Text.ToString();
            string content = this.TextBoxDailyInform.Text.ToString();
            content = Input.Inputadd(content);
            string eng = this.TextBoxEng.Text.ToString();
            eng = eng.Replace("\r\n","<br/>");
            DateTime fabiaodate = Convert.ToDateTime(informdate);
            string sql = "Insert INTO DailyInform (jitai,[lindex],banci,content,eng,shortdate,[fabiaodate]) values ('" + jitai + "','" + lindex + "','" + banci + "','" + content + "','" + eng + "','" + informdate + "',#" + fabiaodate.ToLocalTime() + "#)";
            if ((bool)ViewState["isedit"])
            {
                sql = "Update DailyInform Set jitai='" + jitai + "',[lindex]='" + lindex + "',banci='" + banci + "',content='" + content + "',eng='" + eng + "',shortdate='" + informdate + "',fabiaodate=#" + fabiaodate.ToLocalTime() + "# where ID in " + ViewState["sqlid"].ToString();
            }
            string path = "";
            if (this.FileUpload1.HasFile)
            {
                string fenlei = this.DropDownListFenlei.SelectedValue;
                string title = this.TextBoxFileTittle.Text.ToString();
                path = "~/Exposure/UploadFiles/" + this.DropDownListFenlei.SelectedValue + "/";
                path = Server.MapPath(path);
                string newname = DateTime.Now.ToString("yyyyMMddHHmmss") + FileUpload1.FileName;
                newname = newname.Replace("#", " ");
                newname = newname.Replace("&", " ");
                path = path + newname;
                string filename = this.FileUpload1.FileName;
                filename = "<br/>" + filename + "<p></p>";
                string uploadsql = "Insert into UploadFiles (eng,shortdate,title,fenlei,filename,path) values ('" + Session["name"].ToString() + "','" + informdate + "','" + title + "','" + fenlei + "','" + filename + "','" + path + "')";
                sql = "Insert INTO DailyInform (jitai,[lindex],banci,content,eng,shortdate,fujian,title,[fabiaodate]) values ('" + jitai + "','" + lindex + "','" + banci + "','" + content + "','" + eng + "','" + informdate + "','" + path + "','" + title + "',#" + fabiaodate.ToLocalTime() + "#)";
                if ((bool)ViewState["isedit"])
                {
                    string sqlcheck = "Select fujian from DailyInform where ID in" + ViewState["sqlid"].ToString();
                    string ConnectionString = ConfigurationManager.ConnectionStrings["expmdbcon"].ConnectionString;
                    OleDbConnection conn = new OleDbConnection(ConnectionString);
                    OleDbCommand cmd = new OleDbCommand(sqlcheck, conn);
                    conn.Open();
                    OleDbDataReader rd = cmd.ExecuteReader();
                    string uploadfile = "";
                    while (rd.Read())
                    {
                        uploadfile = rd["fujian"].ToString();
                    }
                    rd.Close();
                    conn.Close();
                    if (File.Exists(uploadfile))
                    {
                        File.Delete(uploadfile);
                        string filesql = "Delete from UploadFiles where path='" + uploadfile + "'";
                        ExpDbManager.ExecuteNonQuery(filesql);
                    }
                    sql = "Update DailyInform Set jitai='" + jitai + "',[lindex]='" + lindex + "',banci='" + banci + "',content='" + content + "',eng='" + eng + "',shortdate='" + informdate + "',fujian='" + path + "',title='" + title + "',fabiaodate=#" + fabiaodate.ToLocalTime() + "# where ID in " + ViewState["sqlid"].ToString();
                }
                try
                {
                    this.FileUpload1.PostedFile.SaveAs(path);
                    ExpDbManager.ExecuteNonQuery(uploadsql);
                    ExpDbManager.ExecuteNonQuery(sql);
                    string myscript = @"alert('发布成功！');window.location.href='DailyInform.aspx';";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
                }
                catch
                {
                    this.ToolTips.Text = "发布失败，请与管理员联系！";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
                }
            }
            else
            {
                try
                {
                    ExpDbManager.ExecuteNonQuery(sql);
                    string myscript = @"alert('发布成功！');window.location.href='DailyInform.aspx';";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
                }
                catch
                {
                    this.ToolTips.Text = "发布失败，请与管理员联系！";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
                }
            }
        }
    }

    protected void Add_Cancel_Click(object sender, EventArgs e)
    {
        ViewState["isedit"] = false;
    }
}
