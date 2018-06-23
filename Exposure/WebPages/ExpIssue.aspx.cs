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

public partial class Exposure_WebPages_ExpIssue : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["isedit"] = false;
            ViewState["sqlid"] = "";
            ViewState["sql"] = "Select * from ExpIssue  where [fabiaodate]>=#" + DateTime.Now.AddHours(-18).ToLocalTime() + "# OR status<>'Close' Order by status Desc,lindex Desc";
            LoadData();
        }
        this.GridView1.Attributes.Add("style", "table-layout:fixed;word-wrap:break-word;");
        this.TextBoxSearch.Attributes.Add("onkeydown", "if(event.keyCode==13) {document.all." + this.Search.ClientID + ".focus();document.all." + this.Search.ClientID + ".click();}");
        this.Delete.Attributes.Add("onclick", "javascript:if(confirm('确定要删除吗?')){}else{return false;}");
    }
    protected void LoadData()
    {
        string sql = ViewState["sql"].ToString();
        DataTable dt = ExpDbManager.ExecuteQuery(sql);
        this.GridView1.DataSource = dt.DefaultView;
        this.GridView1.DataBind();
    }
    protected void Add_Click(object sender, EventArgs e)
    {
        this.ToolTips1.Text = "字数请勿超过900字！";
        this.DD_Item.SelectedIndex = 0;
        this.TextBoxDailyInform.Text = "";
        this.TextBoxInputDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
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
            string sqldelete = "Delete from ExpIssue where ID in" + sqlid;
            string sqlcheck = "Select eng from ExpIssue where ID in" + sqlid;
            string ConnectionString = ConfigurationManager.ConnectionStrings["expmdbcon"].ConnectionString;
            OleDbConnection conn = new OleDbConnection(ConnectionString);
            OleDbCommand cmd = new OleDbCommand(sqlcheck, conn);
            conn.Open();
            OleDbDataReader rd = cmd.ExecuteReader();
            bool isdelete = false;
            while (rd.Read())
            {
                if (((string)rd["eng"] == (string)Session["name"]) || ((string)Session["name"] == "程邵磊"))
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
                    string myscript = @"alert('删除成功！');window.location.href='ExpIssue.aspx';";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
                }
                catch
                {
                    string myscript = @"alert('删除失败，请与管理员联系！');window.location.href='ExpIssue.aspx';";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
                }
            }
            else
            {
                string myscript = @"alert('无权删除他人信息！');window.location.href='ExpIssue.aspx';";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
            }
        }
        else
        {
            string myscript = @"alert('请选择删除项！');window.location.href='ExpIssue.aspx';";
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
            string sqlcheck = "Select * from ExpIssue where ID in" + sqlid;
            string ConnectionString = ConfigurationManager.ConnectionStrings["expmdbcon"].ConnectionString;
            OleDbConnection conn = new OleDbConnection(ConnectionString);
            OleDbCommand cmd = new OleDbCommand(sqlcheck, conn);
            conn.Open();
            OleDbDataReader rd = cmd.ExecuteReader();
            bool isedit = false;
            while (rd.Read())
            {
                if (((string)rd["eng"] == (string)Session["name"]) || ((string)Session["name"] == "程邵磊"))
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
                DD_Status.SelectedValue = rd["status"].ToString();
                DD_Item.SelectedValue = rd["jitai"].ToString().Trim();
                TextBoxDailyInform.Text = Input.Outputadd(rd["content"].ToString());
                TextBoxInputDate.Text = rd["shortdate"].ToString();
                ViewState["sqlid"] = sqlid;
                ViewState["isedit"] = true;
                conn.Close();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
            }
            else
            {
                conn.Close();
                string myscript = @"alert('无权编辑他人信息！');window.location.href='ExpIssue.aspx';";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
            }
        }
        else
        {
            string myscript = @"alert('请选择编辑项！');window.location.href='ExpIssue.aspx';";
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
            string sqlcheck = "Select * from ExpIssue where ID in" + sqlid;
            string ConnectionString = ConfigurationManager.ConnectionStrings["expmdbcon"].ConnectionString;
            OleDbConnection conn = new OleDbConnection(ConnectionString);
            OleDbCommand cmd = new OleDbCommand(sqlcheck, conn);
            conn.Open();
            OleDbDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                ViewState["reply"] = rd["reply"].ToString();
                ViewState["allreply"] = rd["allreply"].ToString();
                LabelReply.Text = rd["eng"].ToString();
                TextBoxReply.Text = Session["name"].ToString() + "  " + DateTime.Now.ToString("yyyy/MM/dd") + " " + "进度：";
                DD_Status.SelectedValue = rd["status"].ToString();
            }
            ViewState["sqlid"] = sqlid;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Reply');</script>");
        }
        else
        {
            string myscript = @"alert('请选择回复项！');window.location.href='ExpIssue.aspx';";
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
        ViewState["sql"] = "Select TOP 100 * from ExpIssue where jitai like '" + chaxunstr + "'or content like '" + chaxunstr + "'or eng like '" + chaxunstr + "'or shortdate like '" + chaxunstr + "'or reply like '" + chaxunstr + "' order by fabiaodate Desc,ID Desc";
        LoadData();
    }

    protected void AddDI_OK_Click(object sender, EventArgs e)
    {
        bool isadd = true;
        if (this.DD_Item.SelectedIndex == 0)
        {
            this.ToolTips1.Text = "请选择机台！";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        else if (this.TextBoxDailyInform.Text == "")
        {
            this.ToolTips1.Text = "内容不能为空！";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        else if (this.TextBoxInputDate.Text == "")
        {
            this.ToolTips1.Text = "日期不能为空！";
        }
        else
        {
            try
            {
                DateTime dt = Convert.ToDateTime(this.TextBoxInputDate.Text.ToString());
            }
            catch
            {
                isadd = false;
                string myscript = @"alert('日期格式不对！');window.location.href='ExpIssue.aspx';";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
            }
            if (FileUpload1.HasFile)
            {
                if (this.DropDownListFenlei.SelectedIndex == 0)
                {
                    isadd = false;
                    this.ToolTips1.Text = "请选择文档分类！";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
                }
                else if (this.TextBoxFileTittle.Text == "")
                {
                    isadd = false;
                    this.ToolTips1.Text = "请为附件添加一个简短的标题！";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
                }
                else if (this.FileUpload1.PostedFile.ContentLength > 20480000)
                {
                    isadd = false;
                    this.ToolTips1.Text = "文档大小不等超过20M！";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
                }
            }
            if (isadd)
            {
                string jitai = this.DD_Item.SelectedValue;
                int lindex = this.DD_Item.SelectedIndex;
                string informdate = this.TextBoxInputDate.Text.ToString();
                string content = this.TextBoxDailyInform.Text.ToString();
                content = Input.Inputadd(content);
                string eng = (string)Session["name"];
                string path = "";
                string uploadsql = "";
                string status = DD_Status.SelectedValue;
                string sql = "Insert INTO ExpIssue (jitai,[lindex],content,eng,shortdate,status,[fabiaodate]) values ('" + jitai + "','" + lindex + "','" + content + "','" + eng + "','" + informdate + "','" + status + "',#" + DateTime.Now.ToLocalTime() + "#)";
                if ((bool)ViewState["isedit"])
                {
                    sql = "Update ExpIssue Set jitai='" + jitai + "',content='" + content + "',eng='" + eng + "',status='" + status + "',[lindex]='" + lindex + "',shortdate='" + informdate + "',fabiaodate=#" + DateTime.Now.ToLocalTime() + "# where ID in " + ViewState["sqlid"].ToString();
                }
                if (FileUpload1.HasFile)
                {
                    string fenlei = this.DropDownListFenlei.SelectedValue;
                    string title = this.TextBoxFileTittle.Text.ToString();
                    path = "~/UploadFiles/" + this.DropDownListFenlei.SelectedValue + "/";
                    string newname = FileUpload1.FileName;
                    newname = newname.Replace("#", " ");
                    newname = newname.Replace("&", " ");
                    path = path + newname;
                    string filename = this.FileUpload1.FileName;
                    filename = "<br/>" + filename + "<p></p>";
                    uploadsql = "Insert into UploadFiles (eng,shortdate,title,fenlei,filename,path) values ('" + Session["name"].ToString() + "','" + informdate + "','" + title + "','" + fenlei + "','" + filename + "','" + path + "')";
                    sql = "Insert INTO ExpIssue (jitai,[lindex],title,fujian,content,eng,shortdate,status,[fabiaodate]) values ('" + jitai + "','" + lindex + "','" + title + "','" + path + "','" + content + "','" + eng + "','" + informdate + "','" + status + "',#" + DateTime.Now.ToLocalTime() + "#)";
                    if ((bool)ViewState["isedit"])
                    {
                        string sqlcheck = "Select fujian from ExpIssue where ID in" + ViewState["sqlid"].ToString();
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
                        if (File.Exists(Server.MapPath(uploadfile)))
                        {
                            File.Delete(Server.MapPath(uploadfile));
                            string filesql = "Delete from UploadFiles where path='" + uploadfile + "'";
                            ExpDbManager.ExecuteNonQuery(filesql);
                        }
                        sql = "Update ExpIssue Set jitai='" + jitai + "',fujian='" + path + "',title='" + title + "',content='" + content + "',eng='" + eng + "',status='" + status + "',[lindex]='" + lindex + "',shortdate='" + informdate + "',fabiaodate=#" + DateTime.Now.ToLocalTime() + "# where ID in " + ViewState["sqlid"].ToString();
                    }
                }
                try
                {
                    if (this.FileUpload1.HasFile)
                    {
                        this.FileUpload1.PostedFile.SaveAs(Server.MapPath(path));
                        ExpDbManager.ExecuteNonQuery(uploadsql);
                    }
                    ExpDbManager.ExecuteNonQuery(sql);
                    string myscript = @"alert('发布成功！');window.location.href='ExpIssue.aspx';";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
                }
                catch
                {
                    this.ToolTips1.Text = "发布失败，请与管理员联系！";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
                }
            }
        }
    }

    protected void AddReply_OK_Click(object sender, EventArgs e)
    {
        string allreply = ViewState["allreply"] + "<br/>" + ViewState["reply"];
        string name = Session["name"].ToString();
        string status = DD_Reply_Status.SelectedValue;
        string sql = "Update ExpIssue Set reply='" + TextBoxReply.Text + "',allreply='" + allreply + "',[fabiaodate]='" + DateTime.Now.ToLocalTime() + "',status='" + status + "' where ID in " + ViewState["sqlid"].ToString();
        if (ViewState["reply"].ToString() == "")
        {
            sql = "Update ExpIssue Set reply='" + TextBoxReply.Text + "',[fabiaodate]='" + DateTime.Now.ToLocalTime() + "',status='" + status + "' where ID in " + ViewState["sqlid"].ToString();
        }
        string path = "";
        string uploadsql = "";
        string informdate = DateTime.Now.ToString("yyyy/MM/dd");

        if (FileUpload2.HasFile)
        {
            string fenlei = this.DropDownListFenlei2.SelectedValue;
            string title = this.TextBoxFileTittle2.Text.ToString();
            path = "~/UploadFiles/" + this.DropDownListFenlei2.SelectedValue + "/";
            string newname = DateTime.Now.ToString("yyyyMMddHHmmss") + FileUpload2.FileName;
            newname = newname.Replace("#", " ");
            newname = newname.Replace("&", " ");
            path = path + newname;
            string filename = this.FileUpload2.FileName;
            filename = "<br/>" + filename + "<p></p>";
            uploadsql = "Insert into UploadFiles (eng,shortdate,title,fenlei,filename,path) values ('" + Session["name"].ToString() + "','" + informdate + "','" + title + "','" + fenlei + "','" + filename + "','" + path + "')";
            sql = "Update ExpIssue Set reply='" + TextBoxReply.Text + "',allreply='" + allreply + "',status='" + status + "',retitle='" + title + "',[fabiaodate]='" + DateTime.Now.ToLocalTime() + "',refujian='" + path + "' where ID in " + ViewState["sqlid"].ToString();
        }
        try
        {
            if (this.FileUpload2.HasFile)
            {
                this.FileUpload2.PostedFile.SaveAs(Server.MapPath(path));
                ExpDbManager.ExecuteNonQuery(uploadsql);
            }
            ExpDbManager.ExecuteNonQuery(sql);
            string myscript = @"alert('发布成功！');window.location.href='ExpIssue.aspx';";
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
}
