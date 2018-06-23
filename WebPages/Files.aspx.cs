using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class WebPages_Files : System.Web.UI.Page
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
            ViewState["sql"] = "Select * from UploadFiles Order by ID Desc";
            LoadData();
        }
        this.GridView1.Attributes.Add("style", "table-layout:fixed;word-wrap:break-word;");
        this.TextBoxSearch.Attributes.Add("onkeydown", "if(event.keyCode==13) {document.all." + this.Search.ClientID + ".focus();document.all." + this.Search.ClientID + ".click();}");
        this.Delete.Attributes.Add("onclick", "javascript:if(confirm('确定要删除吗?')){}else{return false;}");
    }

    protected void Add_Click(object sender, EventArgs e)
    {
        if (Session["name"].ToString() == "程邵磊")
        {
            //PwdManager.CheckPwd();
            this.ToolTips.Text = "";
            this.DropDownListFenlei.SelectedIndex = 0;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
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
            string sqldelete = "Delete from UploadFiles where ID in" + sqlid;
            string sqlcheck = "Select * from UploadFiles where eng like '%" + Session["name"].ToString() + "%' AND ID in " + sqlid;
            string ConnectionString = ConfigurationManager.ConnectionStrings["mdbcon"].ConnectionString;
            OleDbConnection conn = new OleDbConnection(ConnectionString);
            OleDbCommand cmd = new OleDbCommand(sqlcheck, conn);
            conn.Open();
            OleDbDataReader rd = cmd.ExecuteReader();
            string filepath = "";
            bool isdelete = false;
            if (rd.HasRows || Session["name"].ToString() == "程邵磊")
            {
                isdelete = true;
            }
            if (isdelete)
            {
                conn.Close();
                try
                {
                    DbManager.ExecuteNonQuery(sqldelete);
                    if (File.Exists(filepath))
                    {
                        File.Delete(filepath);
                    }
                    string myscript = @"alert('删除成功！');window.location.href='Files.aspx';";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
                }
                catch
                {
                    string myscript = @"alert('删除失败，请与管理员联系！');window.location.href='Files.aspx';";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
                }
            }
            else
            {
                conn.Close();
                string myscript = @"alert('无权删除他人信息！');window.location.href='Files.aspx';";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
            }
        }
        else
        {
            string myscript = @"alert('请选择删除项！');window.location.href='Files.aspx';";
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
            string sqlcheck = "Select * from UploadFiles where eng like '%" + Session["name"].ToString() + "%' AND ID in " + sqlid;
            string ConnectionString = ConfigurationManager.ConnectionStrings["mdbcon"].ConnectionString;
            OleDbConnection conn = new OleDbConnection(ConnectionString);
            OleDbCommand cmd = new OleDbCommand(sqlcheck, conn);
            conn.Open();
            OleDbDataReader rd = cmd.ExecuteReader();
            bool isedit = false;
            if (rd.HasRows || Session["name"].ToString() == "程邵磊")
            {
                isedit = true;
            }
            if (isedit)
            {
                while (rd.Read())
                {
                    this.DropDownListFenlei.SelectedValue = rd["fenlei"].ToString().Trim();
                }
                ViewState["sqlid"] = sqlid;
                ViewState["isedit"] = true;
                conn.Close();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
            }
            else
            {
                conn.Close();
                string myscript = @"alert('无权编辑他人信息！');window.location.href='Files.aspx';";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
            }
        }
        else
        {
            string myscript = @"alert('请选择编辑项！');window.location.href='Files.aspx';";
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
        ViewState["sql"] = "Select * from UploadFiles where path like '" + chaxunstr + "'or shortdate like '" + chaxunstr + "'or fenlei like '" + chaxunstr + "'or eng like '" + chaxunstr  + "' order by ID Desc";
        LoadData();
    }

    //添加数据函数

    protected void Add_OK_Click(object sender, EventArgs e)
    {
        if (this.DropDownListFenlei.SelectedIndex == 0)
        {
            this.ToolTips.Text = "<span style='font-family:fontawesome !important; color:Red;'>&#xf071;</span>请选择文档分类！";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        else if (!this.FileUpload1.HasFile)
        {
            this.ToolTips.Text = "<span style='font-family:fontawesome !important; color:Red;'>&#xf071;</span>请选择上传文件！";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        else
        {
            if (this.FileUpload1.PostedFile.ContentLength > 20480000)
            {
                this.ToolTips.Text = "<span style='font-family:fontawesome !important; color:Red;'>&#xf071;</span>文档大小不等超过20M！";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
            }
            else
            {
                string shortdate = DateTime.Now.ToShortDateString();
                string eng = Session["name"].ToString();
                string fenlei = this.DropDownListFenlei.SelectedValue;
                string path = "~/UploadFiles/" + this.DropDownListFenlei.SelectedValue + "/";
                path = Server.MapPath(path);
                string newname = DateTime.Now.ToString("yyyyMMddHHmmss") + FileUpload1.FileName;
                newname = newname.Replace("#", " ");
                newname = newname.Replace("&", " ");
                path = path + newname;
                string filename = this.FileUpload1.FileName;
                filename = "<br/>" + filename + "<p></p>";
                string sql = "Insert into UploadFiles (eng,shortdate,fenlei,filename,path) values ('" + eng + "','" + shortdate +"','" + fenlei + "','" + filename + "','" + path + "')";
                if ((bool)ViewState["isedit"])
                {
                    string sqlcheck = "Select path from UploadFiles where ID in" + ViewState["sqlid"].ToString();
                    string ConnectionString = ConfigurationManager.ConnectionStrings["mdbcon"].ConnectionString;
                    OleDbConnection conn = new OleDbConnection(ConnectionString);
                    OleDbCommand cmd = new OleDbCommand(sqlcheck, conn);
                    conn.Open();
                    OleDbDataReader rd = cmd.ExecuteReader();
                    rd.Read();
                    string oldpath = rd["path"].ToString();
                    if (File.Exists(oldpath))
                    {
                        File.Delete(oldpath);
                        sql = "Update UploadFiles Set eng='" + eng + "',shortdate='" + shortdate + "',fenlei='" + fenlei + "',filename='" + filename + "',path='" + path + "' where ID in" + ViewState["sqlid"].ToString();
                    }
                }
                try
                {
                    this.FileUpload1.PostedFile.SaveAs(path);
                    DbManager.ExecuteNonQuery(sql);
                    string myscript = @"alert('发布成功！');window.location.href='Files.aspx';";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
                }
                catch
                {
                    string myscript = @"alert('发布失败，请与管理员联系！');window.location.href='Files.aspx';";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
                }
            }
        }
    }
    protected void Add_Cancel_Click(object sender, EventArgs e)
    {
        ViewState["isedit"] = false;
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