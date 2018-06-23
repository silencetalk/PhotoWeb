using System;
using System.IO;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebPages_PRThickness : System.Web.UI.Page
{
    protected void LoadData()
    {
        string sql = ViewState["sql"].ToString();
        DataTable dt = DbManager.ExecuteQuery(sql);
        this.GridView1.DataSource = dt.DefaultView;
        this.GridView1.DataBind();
    }

    //protected void CheckDate()
    //{
    //    string sql = "Select top 1 * from Thickness Order by ID Desc";
    //    string ConnectionString = ConfigurationManager.ConnectionStrings["mdbcon"].ConnectionString;
    //    OleDbConnection conn = new OleDbConnection(ConnectionString);
    //    OleDbCommand cmd = new OleDbCommand(sql, conn);
    //    conn.Open();
    //    OleDbDataReader sdr = cmd.ExecuteReader();
    //    try
    //    {
    //        sdr.Read();
    //        if (sdr.HasRows)
    //        {
    //            ViewState["date"] = sdr["shortdate"].ToString().Trim();
    //            this.LabelDate.Text = ViewState["date"].ToString();
    //        }
    //        else
    //        {
    //            ViewState["date"] = DateTime.Now.ToShortDateString();
    //            this.LabelDate.Text = ViewState["date"].ToString();
    //        }
    //    }
    //    catch
    //    {
    //        ViewState["date"] = DateTime.Now.ToShortDateString();
    //        string myscript = @"alert('加载数据出错！');window.location.href='PRThickness.aspx';";
    //        Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
    //    }
    //    finally
    //    {
    //        sdr.Close();
    //        if (conn.State == ConnectionState.Open)
    //            conn.Close();
    //    }
    //}

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["isedit"] = false;
            ViewState["sqlid"] = "";
            //CheckDate();
            //ViewState["sql"] = "Select * from Thickness where shortdate= '" + ViewState["date"] + "' Order by lindex";
            ViewState["sql"] = "Select * from Thickness Order by ID desc";
            LoadData();
        }
        this.GridView1.Attributes.Add("style", "table-layout:fixed;word-wrap:break-word;");
        this.TextBoxSearch.Attributes.Add("onkeydown", "if(event.keyCode==13) {document.all." + this.Search.ClientID + ".focus();document.all." + this.Search.ClientID + ".click();}");
        this.Delete.Attributes.Add("onclick", "javascript:if(confirm('确定要删除吗?')){}else{return false;}");
    }
    //protected void PreDate_Click(object sender, EventArgs e)
    //{
    //    DateTime dt = Convert.ToDateTime(this.LabelDate.Text.ToString());
    //    ViewState["date"] = dt.AddDays(-1).ToShortDateString();
    //    this.LabelDate.Text = ViewState["date"].ToString();
    //    ViewState["sql"] = "Select * from Thickness where shortdate= '" + ViewState["date"] + "' Order by lindex";
    //    LoadData();
    //}
    //protected void SetDate_Click(object sender, EventArgs e)
    //{
    //    CheckDate();
    //    ViewState["sql"] = "Select * from Thickness where shortdate= '" + ViewState["date"].ToString() + "' Order by lindex";
    //    LoadData();
    //}
    //protected void NextDate_Click(object sender, EventArgs e)
    //{
    //    DateTime dt = Convert.ToDateTime(this.LabelDate.Text.ToString());
    //    ViewState["date"] = dt.AddDays(1).ToShortDateString();
    //    this.LabelDate.Text = ViewState["date"].ToString();
    //    ViewState["sql"] = "Select * from Thickness where shortdate= '" + ViewState["date"] + "' Order by lindex";
    //    LoadData();
    //}
    protected void Add_Click(object sender, EventArgs e)
    {
        this.DropDownListJitai.SelectedIndex = 0;
        this.TextBoxDate.Text = DateTime.Now.ToShortDateString();
        this.TextBoxScan.Text = "";
        this.TextBoxNuzzle.Text = "";
        this.TextBox3D.Text = "";
        this.ToolTips.Text = "";
        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
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
            string sqlcheck = "Select * from Thickness where ID in" + sqlid;
            string ConnectionString = ConfigurationManager.ConnectionStrings["mdbcon"].ConnectionString;
            OleDbConnection conn = new OleDbConnection(ConnectionString);
            OleDbCommand cmd = new OleDbCommand(sqlcheck, conn);
            conn.Open();
            OleDbDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                this.DropDownListJitai.SelectedValue = rd["jitai"].ToString().Trim();
                this.TextBoxDate.Text = rd["shortdate"].ToString();
                this.DropDownThickness.SelectedValue = rd["thickness"].ToString();
                this.TextBoxScan.Text = rd["scan"].ToString();
                this.TextBoxNuzzle.Text = rd["nuzzle"].ToString();
                this.TextBox3D.Text = rd["3d"].ToString();
            }
            ViewState["sqlid"] = sqlid;
            ViewState["isedit"] = true;
            conn.Close();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        else
        {
            string myscript = @"alert('请选择编辑项！');window.location.href='PRThickness.aspx';";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
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
            string sqldelete = "Delete from Thickness where ID in" + sqlid;
            string sqlcheck = "Select * from Thickness where ID in" + sqlid;
            string ConnectionString = ConfigurationManager.ConnectionStrings["mdbcon"].ConnectionString;
            OleDbConnection conn = new OleDbConnection(ConnectionString);
            OleDbCommand cmd = new OleDbCommand(sqlcheck, conn);
            conn.Open();
            OleDbDataReader rd = cmd.ExecuteReader();
            rd.Read();
            string filepath = rd["fujian"].ToString();
            try
            {
                if (File.Exists(Server.MapPath(filepath)))
                {
                    File.Delete(Server.MapPath(filepath));
                    string filesql = "Delete from UploadFiles where path='" + filepath + "'";
                    DbManager.ExecuteNonQuery(filesql);
                }
                DbManager.ExecuteNonQuery(sqldelete);
                string myscript = @"alert('删除成功！');window.location.href='PRThickness.aspx';";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
            }
            catch
            {
                string myscript = @"alert('删除失败，请与管理员联系！');window.location.href='PRThickness.aspx';";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
            }
        }
        else
        {
            string myscript = @"alert('请选择删除项！');window.location.href='PRThickness.aspx';";
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
        ViewState["sql"] = "Select * from Thickness where jitai like '" + chaxunstr + "'or thickness like '" + chaxunstr + "'or nuzzle like '" + chaxunstr + "'or [3d] like '" + chaxunstr + "'or shortdate like '" + chaxunstr + "'or scan like '" + chaxunstr + "' order by lindex";
        LoadData();
    }
    protected void Add_OK_Click(object sender, EventArgs e)
    {
        if (this.DropDownListJitai.SelectedIndex==0)
        {
            this.ToolTips.Text = "<span style='font-family:fontawesome !important; color:Red;'>&#xf071;</span>请选择机台！";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        else if (this.TextBoxDate.Text == "")
        {
            this.ToolTips.Text = "<span style='font-family:fontawesome !important; color:Red;'>&#xf071;</span>日期不能为空！";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        else
        {
            string shortdate = this.TextBoxDate.Text.ToString();
            string jitai = this.DropDownListJitai.SelectedValue;
            int lindex = this.DropDownListJitai.SelectedIndex;
            string thickness = this.DropDownThickness.SelectedValue;
            string scan = this.TextBoxScan.Text.ToString();
            string nuzzle = this.TextBoxNuzzle.Text.ToString();
            string threeD=this.TextBox3D.Text.ToString();
            string eng=Session["name"].ToString();
            string sql = "Insert into Thickness (jitai,[lindex],shortdate,eng,thickness,scan,nuzzle,3d) values ('" + jitai + "','" + lindex + "','" + shortdate + "','" + eng + "','" + thickness + "','" + scan + "','" + nuzzle + "','" + threeD + "')";
            if ((bool)ViewState["isedit"])
            {
                sql = "Update Thickness Set jitai='" + jitai + "',[lindex]='" + lindex + "',shortdate='" + shortdate + "',eng='" + eng + "',thickness='" + thickness + "',scan='" + scan + "',nuzzle='" + nuzzle + "',3d='" + threeD + "' where ID in" + ViewState["sqlid"].ToString();
            }
            if (this.FileUpload1.HasFile)
            {
                if (this.FileUpload1.PostedFile.ContentLength > 20480000)
                {
                    this.ToolTips.Text = "<span style='font-family:fontawesome !important; color:Red;'>&#xf071;</span>文档大小不等超过20M！";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
                }
                else
                {
                    string path = "~/UploadFiles/PRThickness/";
                    string fileext = System.IO.Path.GetExtension(this.FileUpload1.FileName).ToLower();
                    string filename = DateTime.Now.ToString("yyyyMMddHHmmss") + jitai + "膜厚数据" + fileext;
                    path = path + filename;
                    filename = "<br/>"+shortdate +"  "+jitai + "膜厚数据" + fileext+"<p></p>";
                    string filesql = "Insert into UploadFiles (eng,shortdate,title,fenlei,filename,path)  values ('" + eng + "','" + shortdate + "','膜厚数据','设备报告','" + filename + "','" + path + "')";
                    sql = "Insert into Thickness (jitai,[lindex],shortdate,eng,thickness,scan,nuzzle,3d,fujian) values ('" + jitai + "','" + lindex + "','" + shortdate + "','" + eng + "','" + thickness + "','" + scan + "','" + nuzzle + "','" + threeD + "','" + path + "')";
                    if ((bool)ViewState["isedit"])
                    {
                        string sqlcheck = "Select fujian from Thickness where ID in" + ViewState["sqlid"].ToString();
                        string ConnectionString = ConfigurationManager.ConnectionStrings["mdbcon"].ConnectionString;
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

                            string deletefilesql = "Delete from UploadFiles where path='" + uploadfile + "'";
                            DbManager.ExecuteNonQuery(deletefilesql);
                            File.Delete(Server.MapPath(uploadfile));
                        }
                        sql = "Update Thickness Set jitai='" + jitai + "',[lindex]='" + lindex + "',shortdate='" + shortdate + "',eng='" + eng + "',thickness='" + thickness + "',scan='" + scan + "',nuzzle='" + nuzzle + "',3d='" + threeD + "',fujian='" + path + "' where ID in" + ViewState["sqlid"].ToString();
                    }
                    try
                    {
                        this.FileUpload1.PostedFile.SaveAs(Server.MapPath(path));
                        DbManager.ExecuteNonQuery(filesql);
                        DbManager.ExecuteNonQuery(sql);
                        string myscript = @"alert('发布成功！');window.location.href='PRThickness.aspx';";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
                    }
                    catch
                    {
                        string myscript = @"alert('发布失败，请与管理员联系！');window.location.href='PRThickness.aspx';";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
                    }
                }
            }
            else
            {
                try
                {
                    DbManager.ExecuteNonQuery(sql);
                    string myscript = @"alert('发布成功！');window.location.href='PRThickness.aspx';";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
                }
                catch
                {
                    string myscript = @"alert('发布失败，请与管理员联系！');window.location.href='PRThickness.aspx';";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
                }
            }
        }
    }
    protected void Add_Cancel_Click(object sender, EventArgs e)
    {
        ViewState["isedit"] = false;
    }
    protected void AOI_Click(object sender, EventArgs e)
    {
        Response.Redirect("AOI.aspx");
    }
    protected void MM_Click(object sender, EventArgs e)
    {
        Response.Redirect("Mura.aspx");
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