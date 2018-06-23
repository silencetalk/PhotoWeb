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

public partial class WebPages_GlassBroken : System.Web.UI.Page
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
            ViewState["sql"] = "Select * from GlassBroken Order by ID Desc";
            LoadData();
        }
        this.GridView1.Attributes.Add("style", "table-layout:fixed;word-wrap:break-word;");
        this.TextBoxSearch.Attributes.Add("onkeydown", "if(event.keyCode==13) {document.all." + this.Search.ClientID + ".focus();document.all." + this.Search.ClientID + ".click();}");
        this.Delete.Attributes.Add("onclick", "javascript:if(confirm('确定要删除吗?')){}else{return false;}");
    }
    protected void Add_Click(object sender, EventArgs e)
    {
        DropDownListJitai.SelectedIndex = 0;
        DropDownListDanyuan.SelectedIndex = 0;
        DropDownListLayer.SelectedIndex = 0;
        DropDownListbefore1.SelectedIndex = 0;
        DropDownListbefore2.SelectedIndex = 0;
        TextBoxDate.Text = DateTime.Now.ToShortDateString();
        TextBoxLot.Text = "";
        TextBoxReason.Text = "";
        ToolTips.Text = "";
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
            string sqlcheck = "Select * from GlassBroken where ID in" + sqlid;
            string ConnectionString = ConfigurationManager.ConnectionStrings["mdbcon"].ConnectionString;
            OleDbConnection conn = new OleDbConnection(ConnectionString);
            OleDbCommand cmd = new OleDbCommand(sqlcheck, conn);
            conn.Open();
            OleDbDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                DropDownListJitai.SelectedValue = rd["jitai"].ToString();
                DropDownListDanyuan.SelectedValue = rd["danyuan"].ToString();
                DropDownListLayer.SelectedValue = rd["layer"].ToString();
                DropDownListbefore1.SelectedValue = rd["before1"].ToString();
                TextBoxDate.Text = rd["shortdate"].ToString();
                TextBoxLot.Text = rd["lot"].ToString();
                TextBoxReason.Text = Input.Outputadd(rd["reason"].ToString());
            }
            ViewState["sqlid"] = sqlid;
            ViewState["isedit"] = true;
            conn.Close();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        else
        {
            string myscript = @"alert('请选择编辑项！');window.location.href='GlassBroken.aspx';";
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
            string sqldelete = "Delete from GlassBroken where ID in" + sqlid;
            string sqlcheck = "Select map,fujian from GlassBroken where ID in" + sqlid;
            string ConnectionString = ConfigurationManager.ConnectionStrings["mdbcon"].ConnectionString;
            OleDbConnection conn = new OleDbConnection(ConnectionString);
            OleDbCommand cmd = new OleDbCommand(sqlcheck, conn);
            conn.Open();
            OleDbDataReader rd = cmd.ExecuteReader();
            string map = "";
            string fujian = "";
            while (rd.Read())
            {
                map = rd["map"].ToString();
                fujian = rd["fujian"].ToString();
            }
            conn.Close();
            try
            {
                if (File.Exists(map))
                {
                    File.Delete(map);
                    string filesql = "Delete from UploadFiles where path='" + map + "'";
                    DbManager.ExecuteNonQuery(filesql);
                }
                if (File.Exists(fujian))
                {
                    File.Delete(fujian);
                    string filesql = "Delete from UploadFiles where path='" + fujian + "'";
                    DbManager.ExecuteNonQuery(filesql);
                }
                int i = DbManager.ExecuteNonQuery(sqldelete);
                string myscript = @"alert('删除成功！');window.location.href='GlassBroken.aspx';";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
            }
            catch
            {
                string myscript = @"alert('删除失败，请与管理员联系！');window.location.href='GlassBroken.aspx';";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
            }
        }
        else
        {
            string myscript = @"alert('请选择删除项！');window.location.href='AOI.aspx';";
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
        ViewState["sql"] = "Select * from GlassBroken where reason like '" + chaxunstr + "'or lot like '" + chaxunstr + "'or layer like '" + chaxunstr + "'or shortdate like '" + chaxunstr + "'or jitai like '" + chaxunstr + "'or eng like '" + chaxunstr + "' order by ID Desc";
        LoadData();
    }

    protected void Add_OK_Click(object sender, EventArgs e)
    {
        if (this.DropDownListJitai.SelectedIndex == 0)
        {
            this.ToolTips.Text = "<span style='font-family:fontawesome !important; color:Red;'>&#xf071;</span>请选择机台！";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        else if (this.DropDownListDanyuan.SelectedIndex == 0)
        {
            this.ToolTips.Text = "<span style='font-family:fontawesome !important; color:Red;'>&#xf071;</span>请选择单元！";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        else if (this.DropDownListLayer.SelectedIndex == 0)
        {
            this.ToolTips.Text = "<span style='font-family:fontawesome !important; color:Red;'>&#xf071;</span>请选择Layer！";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        else if (this.TextBoxLot.Text == "")
        {
            this.ToolTips.Text = "<span style='font-family:fontawesome !important; color:Red;'>&#xf071;</span>Lot信息不能为空！";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        else if (this.TextBoxReason.Text == "")
        {
            this.ToolTips.Text = "<span style='font-family:fontawesome !important; color:Red;'>&#xf071;</span>发生原因不能为空！";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        else if (this.TextBoxDate.Text == "")
        {
            this.ToolTips.Text = "<span style='font-family:fontawesome !important; color:Red;'>&#xf071;</span>日期不能为空！";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        else  if(!FileUpload2.HasFile)
        {
            this.ToolTips.Text = "<span style='font-family:fontawesome !important; color:Red;'>&#xf071;</span>请上传Map图！";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        else if (System.IO.Path.GetExtension(this.FileUpload2.FileName).ToLower() != ".jpg")
        {
            this.ToolTips.Text = "<span style='font-family:fontawesome !important; color:Red;'>&#xf071;</span>图片格式应为.jpg！";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        else if (!FileUpload1.HasFile)
        {
            this.ToolTips.Text = "<span style='font-family:fontawesome !important; color:Red;'>&#xf071;</span>请报告附件！";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        else
        {
            string jitai = DropDownListJitai.SelectedValue;
            string danyuan = DropDownListDanyuan.SelectedValue;
            string lot = TextBoxLot.Text.ToString();
            string layer = DropDownListLayer.SelectedValue;
            string before1 = DropDownListbefore1.SelectedValue;
            string before2 = DropDownListbefore2.SelectedValue;
            string reason = Input.Inputadd(TextBoxReason.Text.ToString());
            string eng = Session["name"].ToString();
            string shortdate = TextBoxDate.Text.ToString();
            string map = "~/UploadFiles/GlassBroken/Map/"+DateTime.Now.ToString("yyyyMMddHHmmss") + jitai + lot+ layer +".jpg";
            string fujian = "~/UploadFiles/GlassBroken/Report/" + DateTime.Now.ToString("yyyyMMddHHmmss") + FileUpload1.FileName;
            string sql = "Insert into GlassBroken (jitai,danyuan,lot,layer,reason,before1,before2,map,fujian,eng,shortdate) values ('" +
                                 jitai + "','" + danyuan + "','" + lot + "','" + layer + "','" + reason + "','" + before1 + "','" + before2 + "','" + map + "','" + fujian + "','" + eng + "','" + shortdate + "')";
           
            
            if ((bool)ViewState["isedit"])
            {
                sql = "Update GlassBroken Set jitai='" + jitai + "',danyuan='" + danyuan+ "',lot='" + lot + "',layer='" + layer + "',reason='" + reason + "',before1='" + before1 + "',before2='" + before2 + "',map='" + map + "',fujian='" + fujian + "',eng='" + eng + "',shortdate='" + shortdate + "' where ID in" + ViewState["sqlid"].ToString();
                string sqlcheck = "Select map,fujian from GlassBroken where ID in" + ViewState["sqlid"].ToString();
                string ConnectionString = ConfigurationManager.ConnectionStrings["mdbcon"].ConnectionString;
                OleDbConnection conn = new OleDbConnection(ConnectionString);
                OleDbCommand cmd = new OleDbCommand(sqlcheck, conn);
                conn.Open();
                OleDbDataReader rd = cmd.ExecuteReader();
                string uploadfile1 = "";
                string uploadfile2 = "";
                while (rd.Read())
                {
                    uploadfile1 = rd["map"].ToString();
                    uploadfile2 = rd["fujian"].ToString();
                }
                rd.Close();
                conn.Close();
                uploadfile1 = Server.MapPath(uploadfile1);
                uploadfile2 = Server.MapPath(uploadfile2);
                if (File.Exists(uploadfile1))
                {
                    File.Delete(uploadfile1);
                }
                if (File.Exists(uploadfile2))
                {
                    File.Delete(uploadfile2);
                }
            }      
            try
            {
                DbManager.ExecuteNonQuery(sql);
                fujian = Server.MapPath(fujian);
                this.FileUpload1.PostedFile.SaveAs(fujian);
                map = Server.MapPath(map);
                this.FileUpload2.PostedFile.SaveAs(map);
                string myscript = @"alert('发布成功！');window.location.href='GlassBroken.aspx';";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
            }
            catch
            {
                string myscript = @"alert('发布失败，请与管理员联系！');window.location.href='GlassBroken.aspx';";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
            }
        }
    }




    protected void Add_Cancel_Click(object sender, EventArgs e)
    {
        ViewState["isedit"] = false;
    }
    protected void CST_Click(object sender, EventArgs e)
    {
        Response.Redirect("CST.aspx");
    }
    protected void Alarm_Click(object sender, EventArgs e)
    {
        Response.Redirect("Alarm.aspx");
    }
    protected void PM_Click(object sender, EventArgs e)
    {
        Response.Redirect("PM.aspx");
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
