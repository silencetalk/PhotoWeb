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

public partial class WebPages_PMList : System.Web.UI.Page
{
    protected void LoadData()
    {
        string sql = ViewState["sql"].ToString();
        DataTable dt = DbManager.ExecuteQuery(sql);
        this.GridView1.DataSource = dt.DefaultView;
        this.GridView1.DataBind();
    }
    protected void RefreshState()
    {
        string sqlcheck = "Select top 1 * from PMList Where eq='" + Session["eq"].ToString() + "' AND unit='" + Session["unit"].ToString() + "' AND item='" + Session["item"].ToString() + "' Order by ID Desc";
        if ((Session["position"].ToString()) != "")
        {
            sqlcheck = "Select top 1 * from PMList Where eq='" + Session["eq"].ToString() + "' AND unit='" + Session["unit"].ToString() + "' AND item='" + Session["item"].ToString() + "' AND [position]='" + Session["position"].ToString() + "' Order by ID Desc";
        }
        string ConnectionString = ConfigurationManager.ConnectionStrings["mdbcon"].ConnectionString;
        OleDbConnection conn = new OleDbConnection(ConnectionString);
        OleDbCommand cmd = new OleDbCommand(sqlcheck, conn);
        conn.Open();
        OleDbDataReader rd = cmd.ExecuteReader();
        DateTime last = new DateTime();
        DateTime duedate = new DateTime();
        string fujian = "";
        string beizhu = "";
        while (rd.Read())
        {
            last = (DateTime)rd["last"];
            fujian = rd["fujian"].ToString();
            beizhu = rd["beizhu"].ToString();
        }
        string updatesql = "Update PMState Set [last]=#" + last.ToLocalTime() + "#,[duedate]=#" + duedate.ToLocalTime() + "#,beizhu='" + beizhu + "',fujian='" + fujian + "' where EQ='" + Session["eq"].ToString() + "' AND unit='" + Session["unit"].ToString() + "' AND item ='" + Session["item"].ToString() + "'";
        DbManager.ExecuteNonQuery(updatesql);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LB_Title.Text = Session["eq"].ToString() + "  " + Session["unit"].ToString() + "  " + Session["item"].ToString() +"  "+Session["position"].ToString()+ " History";
            LB_Add_Items.Text = Session["eq"].ToString() + "  " + Session["unit"].ToString() + "  " + Session["item"].ToString() + "  " + Session["position"].ToString();
            ViewState["isedit"] = false;
            ViewState["sqlid"] = "";
            ViewState["sql"] = "Select * from PMList Where eq='"+Session["eq"].ToString()+"' AND unit='"+Session["unit"].ToString()+"' AND item='"+Session["item"].ToString() +"' Order by ID desc";
            if (Session["position"].ToString() != "")
            {
                ViewState["sql"] = "Select * from PMList Where eq='" + Session["eq"].ToString() + "' AND unit='" + Session["unit"].ToString() + "' AND item='" + Session["item"].ToString() + "' AND [position]='" + Session["position"].ToString() + "' Order by ID desc";
            }
            LoadData();
        }
        this.GridView1.Attributes.Add("style", "table-layout:fixed;word-wrap:break-word;");
        this.TextBoxSearch.Attributes.Add("onkeydown", "if(event.keyCode==13) {document.all." + this.Search.ClientID + ".focus();document.all." + this.Search.ClientID + ".click();}");
        this.Delete.Attributes.Add("onclick", "javascript:if(confirm('确定要删除吗?')){}else{return false;}");
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
            string sqldelete = "Delete from PMList where ID in" + sqlid;
            string sqlcheck = "Select Count(*) As ct from PMList Where eq='" + Session["eq"].ToString() + "' AND unit='" + Session["unit"].ToString() + "' AND item='" + Session["item"].ToString() + "'";
            if (Session["position"].ToString() != "")
            {
                sqlcheck = "Select Count(*) As ct from PMList Where eq='" + Session["eq"].ToString() + "' AND unit='" + Session["unit"].ToString() + "' AND item='" + Session["item"].ToString() + "' AND [position]='" + Session["position"].ToString() + "'";
            }
            string filecheck = "Select fujian from PMList where ID in"+sqlid;
            string ConnectionString = ConfigurationManager.ConnectionStrings["mdbcon"].ConnectionString;
            OleDbConnection conn = new OleDbConnection(ConnectionString);
            OleDbCommand cmd = new OleDbCommand(sqlcheck, conn);
            conn.Open();
            int count = (int)cmd.ExecuteScalar();
            cmd.CommandText = filecheck;
            cmd.Connection = conn;
            OleDbDataReader rd = cmd.ExecuteReader();
            bool isdelete = false;
            if (count > 1)
            {
                isdelete = true;
            }
            string fujian = "";
            while (rd.Read())
            {
                fujian = rd["fujian"].ToString();
            }
            conn.Close();
            if (isdelete)
            {
                try
                {
                    if (File.Exists(Server.MapPath(fujian)))
                    {
                        File.Delete(Server.MapPath(fujian));
                        string filesql = "Delete from UploadFiles where path='" + fujian + "'";
                        DbManager.ExecuteNonQuery(filesql);
                    }
                    int i = DbManager.ExecuteNonQuery(sqldelete);
                    RefreshState();
                    string myscript = @"alert('删除成功！');window.location.href='PMList.aspx';";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
                }
                catch
                {
                    string myscript = @"alert('删除失败，请与管理员联系！');window.location.href='PMList.aspx';";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
                }
            }
            else
            {
                string myscript = @"alert('只有最后一条了，不能删！');window.location.href='PMList.aspx';";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
            }
        }
        else
        {
            string myscript = @"alert('请选择删除项！');window.location.href='PMList.aspx';";
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
            string sqlcheck = "Select last,beizhu from PMList where ID in "+sqlid;
            string ConnectionString = ConfigurationManager.ConnectionStrings["mdbcon"].ConnectionString;
            OleDbConnection conn = new OleDbConnection(ConnectionString);
            OleDbCommand cmd = new OleDbCommand(sqlcheck, conn);
            conn.Open();
            OleDbDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                TB_Add_Date.Text = ((DateTime)rd["last"]).ToString("yyyy/MM/dd");
                TB_Add_Beizhu.Text = Input.Outputadd(rd["beizhu"].ToString());
            }
            ViewState["sqlid"] = sqlid;
            ViewState["isedit"] = true;
            conn.Close();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        else
        {
            string myscript = @"alert('请选择编辑项！');window.location.href='PMList.aspx';";
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
        ViewState["sql"] = "Select * from PMList where eng like '" + chaxunstr + "'or [last] like '" + chaxunstr + "'or beizhu like '" + chaxunstr +"' order by ID Desc";
        LoadData();
    }
    protected void Add_Click(object sender, EventArgs e)
    {
        TB_Add_Date.Text = DateTime.Now.ToString("yyyy/MM/dd");
        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
    }
    protected void Add_OK_Click(object sender, EventArgs e)
    {
        DateTime last=Convert.ToDateTime(TB_Add_Date.Text.ToString());
        string beizhu=Input.Inputadd(TB_Add_Beizhu.Text.ToString());
         string position=Session["position"].ToString();
        int zhouqi = 30;
        string sqlcheck = "Select [zhouqi] from PMState where EQ ='" + Session["eq"].ToString()+ "' AND unit ='" + Session["unit"].ToString()+ "' AND item='" + Session["item"].ToString()+ "'";
        if (position!="")
        {
            sqlcheck += " AND [position]='"+position+"'";
        }
        string ConnectionString = ConfigurationManager.ConnectionStrings["mdbcon"].ConnectionString;
        OleDbConnection conn = new OleDbConnection(ConnectionString);
        OleDbCommand cmd = new OleDbCommand(sqlcheck, conn);
        conn.Open();
        OleDbDataReader rd = cmd.ExecuteReader();
        while (rd.Read())
        {
            zhouqi = Convert.ToInt32(rd["zhouqi"].ToString());
        }
        rd.Close();
        conn.Close();
        DateTime duedate=last.AddDays(zhouqi);
        string fujian = "";
        string filesql = "";
        if (FileUpload1.HasFile)
        {
            string filename = this.FileUpload1.FileName;
            fujian = "~/UploadFiles/设备报告/" + DateTime.Now.ToString("yyyyMMddHHmmss") + filename;
            filesql = "Insert into UploadFiles (fenlei,title,path,filename,shortdate,eng) values ('设备报告','PM报告','" + fujian + "','" + filename + "','" + last.ToShortDateString() + "','" + Session["name"].ToString() + "')";
        }
        string listsql = "Insert into PMList (EQ,unit,item,beizhu,[last],[duedate],[lindex],[position],eng,fujian) values ('"+Session["eq"].ToString()+"','"+
            Session["unit"].ToString()+"','"+Session["item"].ToString()+"','"+beizhu+"',#"+last.ToLocalTime()+"#,#"+duedate.ToLocalTime()+"#,'"+
            Session["lindex"] + "','" + position + "','" + Session["name"].ToString() + "','" + fujian+ "')";
        string updatesql = "Update PMState Set [last]=#" + last.ToLocalTime() + "#,[duedate]=#" + duedate.ToLocalTime() + "#,beizhu='" + beizhu + "',fujian='" + fujian + "' where EQ='" + Session["eq"].ToString() + "' AND unit='" + Session["unit"].ToString() + "' AND item ='" + Session["item"].ToString() + "'";
        if (position != "")
        {
            updatesql += " AND [position]='" + position + "'";
        }
        if ((bool)ViewState["isedit"])
        {
            listsql = "Update PMList Set EQ='"+Session["eq"].ToString()+"',unit='"+Session["unit"].ToString()+"',item='"+Session["item"].ToString()+
                "',beizhu='"+beizhu+"',[last]=#"+last.ToLocalTime()+"#,[duedate]=#"+duedate.ToLocalTime()+"#,lindex='"+ Session["lindex"]+
                "',[position]='"+position+"',eng='"+Session["name"].ToString()+"',fujian='"+fujian+"' where ID in "+ViewState["sqlid"].ToString();
            if (FileUpload1.HasFile)
            {
                FileUpload1.PostedFile.SaveAs(Server.MapPath(fujian));
                DbManager.ExecuteNonQuery(filesql);
            }
            DbManager.ExecuteNonQuery(listsql);
            RefreshState();
            LB_OK.Text = "发布成功！";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('dlg_ok');</script>");
            LoadData();
        }
        else
        {
            if (FileUpload1.HasFile)
            {
                FileUpload1.PostedFile.SaveAs(Server.MapPath(fujian));
                DbManager.ExecuteNonQuery(filesql);
            }
            DbManager.ExecuteNonQuery(listsql);
            DbManager.ExecuteNonQuery(updatesql);
            LB_OK.Text = "发布成功！";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('dlg_ok');</script>");
            LoadData();
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
    protected void Parts_Click(object sender, EventArgs e)
    {
        Response.Redirect("Parts.aspx");
    }
    protected void PM_Click(object sender, EventArgs e)
    {
        Response.Redirect("PM.aspx");
    }
    //protected void Page_Back(object sender, EventArgs e)
    //{
    //    string url = Session["preurl"].ToString();
    //    Response.Redirect(url);
    //}

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
        //if (e.CommandName == "dataupdate")
        //{
        //    //int i = Convert.ToInt32(e.CommandArgument);
        //    //ViewState["sql"] = GridView1.DataKeys[i][0].ToString();
        //    //DD_Add_Jitai.SelectedValue = GridView1.DataKeys[i][1].ToString();
        //    //EQ_Selected(sender, e);
        //    //DD_Add_Danyuan.SelectedValue = GridView1.DataKeys[i][2].ToString();
        //    //Unit_Selected(sender, e);
        //    //DD_Add_Xiangmu.SelectedValue = GridView1.DataKeys[i][3].ToString();
        //    //Xiangmu_Selected(sender, e);
        //    //DD_Add_Position.SelectedValue = GridView1.DataKeys[i][4].ToString();
        //    TB_Add_Date.Text = DateTime.Now.ToString("yyyy/MM/dd");
        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        //}
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string id = "(" + this.GridView1.DataKeys[e.RowIndex][0].ToString() + ")";
        string shortdate = DateTime.Now.ToShortDateString();
        DateTime dt = Convert.ToDateTime(shortdate);
        string sql = "Update PM Set shortdate='" + shortdate + "',[realdate]=#" + dt.ToLocalTime() + "# where ID in " + id;
        try
        {
            DbManager.ExecuteNonQuery(sql);
            string myscript = @"alert('更新完成！');window.location.href='PM.aspx';";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
        }
        catch
        {
            string myscript = @"alert('更新失败，请与管理员联系！');window.location.href='PM.aspx';";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
        }
    }
}
