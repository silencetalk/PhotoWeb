using System;
using System.IO;
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

public partial class WebPages_Mura : System.Web.UI.Page
{
    protected void LoadData()
    {
        string sql = ViewState["sql"].ToString();
        DataTable dt = DbManager.ExecuteQuery(sql);
        this.GridView1.DataSource = dt.DefaultView;
        this.GridView1.DataBind();
        this.ImageMura.ImageUrl = "DrawImage.aspx?sql=" + sql ;
    }

    protected void CheckDate()
    {
        string sql = "Select top 1 * from Mura Order by ID Desc";
        string ConnectionString = ConfigurationManager.ConnectionStrings["mdbcon"].ConnectionString;
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
            string myscript = @"alert('加载数据出错！');window.location.href='Mura.aspx';";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
        }
        finally
        {
            sdr.Close();
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["isedit"] = false;
            ViewState["sqlid"] = "";
            CheckDate();
            ViewState["sql"] = "Select * from Mura where shortdate= '" + ViewState["date"] + "' Order by lindex";
            LoadData();
        }
        this.GridView1.Attributes.Add("style", "table-layout:fixed;word-wrap:break-word;");
        this.TextBoxSearch.Attributes.Add("onkeydown", "if(event.keyCode==13) {document.all." + this.Search.ClientID + ".focus();document.all." + this.Search.ClientID + ".click();}");
        this.Delete.Attributes.Add("onclick", "javascript:if(confirm('确定要删除吗?')){}else{return false;}");
    }
    protected void PreDate_Click(object sender, EventArgs e)
    {
        DateTime dt = Convert.ToDateTime(this.LabelDate.Text.ToString());
        ViewState["date"] = dt.AddDays(-1).ToShortDateString();
        this.LabelDate.Text = ViewState["date"].ToString();
        ViewState["sql"] = "Select * from Mura where shortdate= '" + ViewState["date"] + "' Order by lindex";
        LoadData();
    }
    protected void SetDate_Click(object sender, EventArgs e)
    {
        CheckDate();
        ViewState["sql"] = "Select * from Mura where shortdate= '" + ViewState["date"].ToString() + "' Order by lindex";
        LoadData();
    }
    protected void NextDate_Click(object sender, EventArgs e)
    {
        DateTime dt = Convert.ToDateTime(this.LabelDate.Text.ToString());
        ViewState["date"] = dt.AddDays(1).ToShortDateString();
        this.LabelDate.Text = ViewState["date"].ToString();
        ViewState["sql"] = "Select * from Mura where shortdate= '" + ViewState["date"] + "' Order by lindex";
        LoadData();
    }
    protected void Add_Click(object sender, EventArgs e)
    {
        this.DropDownListJitai.SelectedIndex = 0;
        this.TextBoxBeizhu.Text = "";
        this.TextBoxDate.Text = DateTime.Now.ToShortDateString();
        this.TextBoxEpx.Text = "";
        this.TextBoxEpy.Text = "";
        this.TextBoxLotid.Text = "";
        this.TextBoxMuraType.Text = "";
        this.TextBoxSpx.Text = "";
        this.TextBoxSpy.Text = "";
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
            string sqlcheck = "Select * from Mura where ID in" + sqlid;
            string ConnectionString = ConfigurationManager.ConnectionStrings["mdbcon"].ConnectionString;
            OleDbConnection conn = new OleDbConnection(ConnectionString);
            OleDbCommand cmd = new OleDbCommand(sqlcheck, conn);
            conn.Open();
            OleDbDataReader rd = cmd.ExecuteReader();
            bool isedit = false;
            while (rd.Read())
            {
                if (((string)rd["eng"] == (string)Session["name"]) || ((string)Session["name"] == "王明"))
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
                this.DropDownListJitai.SelectedValue = rd["jitai"].ToString().Trim();
                this.DropDownListShape.SelectedValue = rd["shape"].ToString().Trim();
                this.DropDownListSize.SelectedValue = rd["sizename"].ToString().Trim();
                DDLayer.SelectedValue = rd["layer"].ToString().Trim();
                this.TextBoxBeizhu.Text =Input.Outputadd(rd["beizhu"].ToString());
                this.TextBoxDate.Text = rd["shortdate"].ToString();
                this.TextBoxEpx.Text = rd["epx"].ToString();
                this.TextBoxEpy.Text = rd["epy"].ToString();
                this.TextBoxLotid.Text = rd["lotid"].ToString();
                this.TextBoxMuraType.Text = rd["muraname"].ToString();
                this.TextBoxSpx.Text = rd["spx"].ToString();
                this.TextBoxSpy.Text = rd["spy"].ToString();
                ViewState["sqlid"] = sqlid;
                ViewState["isedit"] = true;
                conn.Close();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
            }
            else
            {
                conn.Close();
                string myscript = @"alert('无权编辑他人信息！');window.location.href='Mura.aspx';";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
            }
        }
        else
        {
            string myscript = @"alert('请选择编辑项！');window.location.href='Mura.aspx';";
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
            string sqldelete = "Delete from Mura where ID in" + sqlid;
            string sqlcheck = "Select eng from Mura where ID in" + sqlid;
            string ConnectionString = ConfigurationManager.ConnectionStrings["mdbcon"].ConnectionString;
            OleDbConnection conn = new OleDbConnection(ConnectionString);
            OleDbCommand cmd = new OleDbCommand(sqlcheck, conn);
            conn.Open();
            OleDbDataReader rd = cmd.ExecuteReader();
            bool isdelete = false;
            while (rd.Read())
            {
                if (((string)rd["eng"] == (string)Session["name"]) || ((string)Session["name"] == "王明"))
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
                    int i = DbManager.ExecuteNonQuery(sqldelete);
                    string myscript = @"alert('删除成功！');window.location.href='Mura.aspx';";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
                }
                catch
                {
                    string myscript = @"alert('删除失败，请与管理员联系！');window.location.href='Mura.aspx';";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
                }
            }
            else
            {
                string myscript = @"alert('无权删除他人信息！');window.location.href='Mura.aspx';";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
            }
        }
        else
        {
            string myscript = @"alert('请选择删除项！');window.location.href='Mura.aspx';";
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
        ViewState["sql"] = "Select * from Mura where jitai like '" + chaxunstr + "'or lotid like '" + chaxunstr + "'or layer like '" + chaxunstr + "'or beizhu like '" + chaxunstr + "'or eng like '" + chaxunstr + "'or shortdate like '" + chaxunstr + "'or shape like '" + chaxunstr + "' order by lindex";
        LoadData();
    }
    protected void Add_OK_Click(object sender, EventArgs e)
    {
        if (this.DropDownListJitai.SelectedIndex == 0)
        {
            this.ToolTips.Text = "<span style='font-family:fontawesome !important; color:Red;'>&#xf071;</span>请选择机台！";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        else if (this.TextBoxLotid.Text=="")
        {
            this.ToolTips.Text = "<span style='font-family:fontawesome !important; color:Red;'>&#xf071;</span>请填写LotId！";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        else if(this.TextBoxMuraType.Text=="")
        {
            string jitai = this.DropDownListJitai.SelectedValue;
            int lindex = this.DropDownListJitai.SelectedIndex;
            string layer = DDLayer.SelectedValue;
            string shortdate = this.TextBoxDate.Text.ToString();
            string eng = Session["name"].ToString();
            string beizhu = Input.Inputadd(this.TextBoxBeizhu.Text.ToString());
            string sql = "Insert into Mura (jitai,[lindex],layer,shortdate,eng,beizhu) values ('" + jitai + "','" + lindex + "','" + layer+ "','" + shortdate + "','" + eng + "','" + beizhu + "')";
            if ((bool)ViewState["isedit"])
            {
                sql = "Update Mura Set jitai='" + jitai + "',[lindex]='" + lindex + "',shortdate='" + shortdate + "',layer='" + layer + "',eng='" + eng + "',beizhu='" + beizhu + "' where ID in" + ViewState["sqlid"].ToString();
            }
            try
            {
                DbManager.ExecuteNonQuery(sql);
                string myscript = @"alert('发布成功！');window.location.href='Mura.aspx';";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
            }
            catch
            {
                string myscript = @"alert('发布失败，请与管理员联系！');window.location.href='Mura.aspx';";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
            }
        }
        else if (this.TextBoxSpx.Text == "" || this.TextBoxSpy.Text == "")
        {
            this.ToolTips.Text = "<span style='font-family:fontawesome !important; color:Red;'>&#xf071;</span>请填写起点坐标值！";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        else if (this.DropDownListShape.SelectedIndex == 1 && (this.TextBoxEpx.Text == "" || this.TextBoxEpy.Text == ""))
        {
            this.ToolTips.Text = "<span style='font-family:fontawesome !important; color:Red;'>&#xf071;</span>请填写终点坐标值！";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        else
        {
            bool isadd = true;
            string jitai = this.DropDownListJitai.SelectedValue;
            string layer = DDLayer.SelectedValue;
            int lindex=this.DropDownListJitai.SelectedIndex;
            string lotid = this.TextBoxLotid.Text.ToString();
            string muraname = this.TextBoxMuraType.Text.ToString();
            string shape = this.DropDownListShape.SelectedValue;
            string sizename = this.DropDownListSize.SelectedValue;
            int size = (this.DropDownListSize.SelectedIndex + 2) * 20;
            string shortdate = this.TextBoxDate.Text.ToString();
            string eng = Session["name"].ToString();
            string beizhu =Input.Inputadd(this.TextBoxBeizhu.Text.ToString());
            float spx = 0,spy=0;
            float.TryParse(this.TextBoxSpx.Text.ToString().Trim(),out spx);
            float.TryParse(this.TextBoxSpy.Text.ToString().Trim(), out spy);
            if ((spx > 1250) || (spx < -1205) || (spy > 1100) || (spy < -1100))
            {
                isadd = false;
                this.ToolTips.Text = "<span style='font-family:fontawesome !important; color:Red;'>&#xf071;</span>请填写正确坐标值！";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
            }
            string startp = "(" + this.TextBoxSpx.Text.ToString().Trim() + "," + this.TextBoxSpy.Text.ToString().Trim() + ")";
            string sql = "Insert into Mura (jitai,[lindex],shortdate,layer,eng,lotid,muraname,shape,sizename,beizhu,startp,[size],[spx],[spy])";
            sql = sql + "values ('" + jitai + "','" + lindex + "','" + shortdate + "','" + layer + "','" + eng + "','" + lotid + "','" + muraname + "','" + shape + "','" + sizename + "','" + beizhu + "','" + startp + "','" + size + "','" + spx + "','" + spy + "')";
            if ((bool)ViewState["isedit"])
            {
                sql = "Update Mura Set jitai='" + jitai + "',[lindex]='" + lindex + "',shortdate='" + shortdate + "',layer='" + layer + "',eng='" + eng + "',lotid='" + lotid + "',muraname='" + muraname
                    + "',shape='" + shape + "',sizename='" + sizename + "',beizhu='" + beizhu + "',startp='" + startp + "',[size]='" + size + "',[spx]='" + spx + "',[spy]='" + spy + "' where ID in " + ViewState["sqlid"].ToString();
            }
            if (this.DropDownListShape.SelectedIndex == 1)
            {
                float epx = 0, epy = 0;
                float.TryParse(this.TextBoxEpx.Text.ToString().Trim(), out epx);
                float.TryParse(this.TextBoxEpy.Text.ToString().Trim(), out epy);
                if ((epx > 1250) || (epx < -1205) || (epy > 1100) || (epy < -1100))
                {
                    isadd = false;
                    this.ToolTips.Text = "<span style='font-family:fontawesome !important; color:Red;'>&#xf071;</span>请填写正确坐标值！";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
                }
               string endp = "(" + this.TextBoxEpx.Text.ToString().Trim() + "," + this.TextBoxEpy.Text.ToString().Trim() + ")";
               sql = "Insert into Mura (jitai,[lindex],shortdate,layer,eng,lotid,muraname,shape,sizename,beizhu,startp,endp,[size],[spx],[spy],[epx],[epy])";
               sql = sql + "values ('" + jitai + "','" + lindex + "','" + shortdate + "','" + layer + "','" + eng + "','" + lotid + "','" + muraname + "','" + shape + "','" + sizename + "','" + beizhu + "','" + startp + "','" + endp + "','" + size + "','" + spx + "','" + spy + "','" + epx + "','" + epy + "')";
               if ((bool)ViewState["isedit"])
               {
                   sql = "Update Mura Set jitai='" + jitai + "',[lindex]='" + lindex + "',shortdate='" + shortdate + "',layer='" + layer + "',eng='" + eng + "',lotid='" + lotid + "',muraname='" + muraname
                       + "',shape='" + shape + "',sizename='" + sizename + "',beizhu='" + beizhu + "',startp='" + startp + "',endp='" + endp + "',[size]='" + size + "',[spx]='" + spx + "',[spy]='" + spy + "',[epx]='" + epx + "',[epy]='" + epy + "' where ID in " + ViewState["sqlid"].ToString();
               }
            }
            if (isadd)
            {
                try
                {
                    DbManager.ExecuteNonQuery(sql);
                    string myscript = @"alert('发布成功！');window.location.href='Mura.aspx';";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
                }
                catch
                {
                    string myscript = @"alert('发布失败，请与管理员联系！');window.location.href='Mura.aspx';";
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
    protected void Thickness_Click(object sender, EventArgs e)
    {
        Response.Redirect("PRThickness.aspx");
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