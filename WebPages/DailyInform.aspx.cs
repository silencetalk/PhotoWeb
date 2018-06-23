using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class WebPages_DailyInform : System.Web.UI.Page
{
    protected void OKMessage(string content,string url)
    {
        try
        {
            string sqlcheck = "Select * from ToolTips";
            string ConnectionString = ConfigurationManager.ConnectionStrings["mdbcon"].ConnectionString;
            OleDbConnection conn = new OleDbConnection(ConnectionString);
            OleDbCommand cmd = new OleDbCommand(sqlcheck, conn);
            conn.Open();
            OleDbDataReader rd = cmd.ExecuteReader();
            bool isContainsKeyword = false;
            while (rd.Read())
            {
                if (content.Contains(rd["keyword"].ToString()))
                {
                    Label_Message.Text = rd["description"].ToString();
                    Message_Confirm.PostBackUrl = rd["returnurl"].ToString();
                    isContainsKeyword = true;
                }
            }
            if (isContainsKeyword)
            {
                string myscript = @"ShowDialog('message_dlg');";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
            }
            else
            {
                string myscript = @"alert('发布成功！');window.location.href='" + url + "';";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
            }
        }
        catch
        {
            string myscript = @"alert('发布成功！');window.location.href='" + url + "';";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["isedit"]= false;
            ViewState["status"] = "Close";
            ViewState["sqlid"] = "";
            if (DateTime.Now.Hour > 6)
                AddTask();
            CheckDate();
            ViewState["sql"] = "Select * from DailyInform  where [fabiaodate]>=#" + DateTime.Now.AddHours(-18).ToLocalTime() + "# OR status='Open' Order by status Desc,[fabiaodate] Desc";
            LoadData();
            LoadCount();
        }
        this.GridView1.Attributes.Add("style", "table-layout:fixed;word-wrap:break-word;");
        this.TextBoxSearch.Attributes.Add("onkeydown", "if(event.keyCode==13) {document.all." + this.Search.ClientID + ".focus();document.all." + this.Search.ClientID + ".click();}");
        this.Delete.Attributes.Add("onclick", "javascript:if(confirm('确定要删除吗?')){}else{return false;}");
    }
    //数据处理函数
    protected void CheckDate()
    {
        string sql = "Select top 1 * from DailyInform Order by ID Desc";
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
                ViewState["date"] = DateTime.Now.ToString("yyyy/MM/dd");
                this.LabelDate.Text = ViewState["date"].ToString();
            }
        }
        catch
        {
            ViewState["date"] = DateTime.Now.ToString("yyyy/MM/dd");
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
        DataTable dt = DbManager.ExecuteQuery(sql);
        this.GridView1.DataSource = dt.DefaultView;
        this.GridView1.DataBind();
    }
    protected void LoadCount()
    {
        string countsql = "Select count(Alarm.downtime) As UpdateCount from Alarm where Alarm.downtime >#" + DateTime.Now.AddDays(-1).ToLocalTime() + "# "
          + "Union All Select count(PMState.last) from PMState where PMState.last >#" + DateTime.Now.AddDays(-1).ToLocalTime() + "# "
          + "Union All Select count(PartsList.LastDate) from PartsList where PartsList.LastDate >#" + DateTime.Now.AddDays(-1).ToLocalTime() + "# ";
        DataTable dt = DbManager.ExecuteQuery(countsql);
        LabelForCount.Text = "Alarm: " + dt.Rows[0][0].ToString() + "  PM: " + dt.Rows[1][0].ToString() + "  Parts: " + dt.Rows[2][0].ToString();  
    }
    protected void AddTask()
    {
        DateTime dt=DateTime.Now;
        string sqlcheck = "Select * from InformTask where nextdate='" + dt.ToString("yyyy/MM/dd") + "'";
        string ConnectionString = ConfigurationManager.ConnectionStrings["mdbcon"].ConnectionString;
        OleDbConnection conn = new OleDbConnection(ConnectionString);
        OleDbCommand cmd = new OleDbCommand(sqlcheck, conn);
        conn.Open();
        OleDbDataReader rd = cmd.ExecuteReader();
        while (rd.Read())
        {
            string sqlinsert = "Insert INTO DailyInform (jitai,[lindex],banci,content,eng,shortdate,status,[fabiaodate]) values ('"
                + rd["item"].ToString() + "','20','系统提醒','" + rd["content"].ToString() + "','" + rd["eng"].ToString()
                + "','" + dt.ToString("yyyy/MM/dd") + "','" + rd["status"].ToString() + "',#" + DateTime.Now.ToLocalTime() + "#)";
            string nextdate = "";
            switch (rd["zhouqi"].ToString())
            {
                case "每天":
                    {
                        nextdate = DateTime.Now.AddDays(1).ToString("yyyy/MM/dd");
                        break;
                    }
                case "每星期":
                    {
                        int dateindex = Convert.ToInt32(rd["dateindex"].ToString());
                        nextdate = DateManage.GetWeekUpOfDate(dt, dateindex, 1).ToString("yyyy/MM/dd");
                        break;
                    }
                case "每月":
                    {
                        int dateindex = Convert.ToInt32(rd["dateindex"].ToString());
                        nextdate = DateManage.GetDayUpOfDate(dt, dateindex, 1).ToString("yyyy/MM/dd");
                        break;
                    }
                case "指定日期":
                    {
                        nextdate = "";
                        break;
                    }
                default:
                    {
                        nextdate = "";
                        break;
                    }
            }
            string sqlupdate = "Update InformTask Set lastdate='" + dt.ToString("yyyy/MM/dd") +"',nextdate='"+nextdate+ "' where ID in (" + rd["ID"].ToString() + ")";
            DbManager.ExecuteNonQuery(sqlinsert);
            DbManager.ExecuteNonQuery(sqlupdate);
        }

        //int mouth = DateTime.Today.Day;
        //string week = DateTime.Today.ToString("每dddd", new System.Globalization.CultureInfo("zh-CN"));
        //string today = DateTime.Today.ToString("yyyy/MM/dd");
        //string sqlcheck = "Select * from InformTask where (zhouqi='"+week+"' or zhouqi='每天') And (lastdate is null or lastdate<>'" + today + "')";
        //string ConnectionString = ConfigurationManager.ConnectionStrings["mdbcon"].ConnectionString;
        //OleDbConnection conn = new OleDbConnection(ConnectionString);
        //OleDbCommand cmd = new OleDbCommand(sqlcheck, conn);
        //conn.Open();
        //OleDbDataReader rd = cmd.ExecuteReader();
        //while (rd.Read())
        //{
        //    string sqlinsert = "Insert INTO DailyInform (jitai,[lindex],banci,content,eng,shortdate,status,[fabiaodate]) values ('"
        //        + rd["item"].ToString() + "','20','系统提醒','" + rd["content"].ToString() + "','" + rd["eng"].ToString()
        //        + "','" + today + "','" + rd["status"].ToString() + "',#" + DateTime.Now.ToLocalTime() + "#)";
        //    string sqlupdate = "Update InformTask Set lastdate='" + today + "' where ID in (" + rd["ID"].ToString() + ")";
        //    DbManager.ExecuteNonQuery(sqlinsert);
        //    DbManager.ExecuteNonQuery(sqlupdate);
        //}
        //rd.Close();
        //conn.Close();

        //if (mouth == 1)
        //{
        //    sqlcheck = "Select * from InformTask where zhouqi='每月1号' And (lastdate is null or lastdate<>'" + today + "')";
        //    ConnectionString = ConfigurationManager.ConnectionStrings["mdbcon"].ConnectionString;
        //    conn = new OleDbConnection(ConnectionString);
        //    cmd = new OleDbCommand(sqlcheck, conn);
        //    conn.Open();
        //    rd = cmd.ExecuteReader();
        //    while (rd.Read())
        //    {
        //        string sqlinsert = "Insert INTO DailyInform (jitai,[lindex],banci,content,eng,shortdate,status,[fabiaodate]) values ('"
        //            + rd["item"].ToString() + "','20','系统提醒','" + rd["content"].ToString() + "','" + rd["eng"].ToString()
        //            + "','" + today + "','" + rd["status"].ToString() + "',#" + DateTime.Now.ToLocalTime() + "#)";
        //        string sqlupdate = "Update InformTask Set lastdate='" + today + "' where ID in (" + rd["ID"].ToString() + ")";
        //        DbManager.ExecuteNonQuery(sqlinsert);
        //        DbManager.ExecuteNonQuery(sqlupdate);
        //    }
        //    rd.Close();
        //    conn.Close();
        //}
        //if (mouth == 15)
        //{
        //    sqlcheck = "Select * from InformTask where zhouqi='每月15号' And (lastdate is null or lastdate<>'" + today + "')";
        //    ConnectionString = ConfigurationManager.ConnectionStrings["mdbcon"].ConnectionString;
        //    conn = new OleDbConnection(ConnectionString);
        //    cmd = new OleDbCommand(sqlcheck, conn);
        //    conn.Open();
        //    rd = cmd.ExecuteReader();
        //    while (rd.Read())
        //    {
        //        string sqlinsert = "Insert INTO DailyInform (jitai,[lindex],banci,content,eng,shortdate,status,[fabiaodate]) values ('"
        //            + rd["item"].ToString() + "','20','系统提醒','" + rd["content"].ToString() + "','" + rd["eng"].ToString()
        //            + "','" + today + "','" + rd["status"].ToString() + "',#" + DateTime.Now.ToLocalTime() + "#)";
        //        string sqlupdate = "Update InformTask Set lastdate='" + today + "' where ID in (" + rd["ID"].ToString() + ")";
        //        DbManager.ExecuteNonQuery(sqlinsert);
        //        DbManager.ExecuteNonQuery(sqlupdate);
        //    }
        //    rd.Close();
        //    conn.Close();
        //}
    }
    //工具按钮函数
    protected void PreDate_Click(object sender, EventArgs e)
    {
        DateTime dt = Convert.ToDateTime(this.LabelDate.Text.ToString());
        ViewState["date"] = dt.AddDays(-1).ToString("yyyy/MM/dd");
        this.LabelDate.Text = ViewState["date"].ToString();
        ViewState["sql"] = "Select * from DailyInform  where [fabiaodate]=#" + DateTime.Now.AddDays(-1).ToLocalTime() + "# OR shortdate='" + ViewState["date"].ToString() + "'  Order by status Desc,[fabiaodate] Desc,lindex";
        LoadData();
    }
    protected void SetDate_Click(object sender, EventArgs e)
    {
        CheckDate();
        ViewState["sql"] = "Select * from DailyInform  where [fabiaodate]> #" + DateTime.Now.AddDays(-1).ToLocalTime() + "# OR shortdate='" + ViewState["date"].ToString() + "' OR status='Open' Order by status Desc,[fabiaodate] Desc,lindex";
        LoadData();
    }
    protected void NextDate_Click(object sender, EventArgs e)
    {
        DateTime dt = Convert.ToDateTime(this.LabelDate.Text.ToString());
        ViewState["date"]= dt.AddDays(1).ToString("yyyy/MM/dd");
        this.LabelDate.Text = ViewState["date"].ToString();
        ViewState["sql"] = "Select * from DailyInform  where [fabiaodate]> #" + DateTime.Now.AddDays(-1).ToLocalTime() + "# OR shortdate='" + ViewState["date"].ToString() + "' OR status='Open' Order by status Desc,[fabiaodate] Desc,lindex";
        LoadData();
    }
    protected void Add_Click(object sender, EventArgs e)
    {
        this.ToolTips1.Text = "字数请勿超过900字！";
        this.DD_Item.SelectedIndex = 0;
        //DD_EQ.Visible = false;
        //LB_EQ.Visible = false;
        this.TextBoxDailyInform.Text = "";
        int hour = DateTime.Now.Hour;
        ViewState["status"] = "Close";
        if (hour < 12)
        {
            this.DropDownListInputBanci.SelectedIndex = 0;
            this.TextBoxInputDate.Text = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd");
        }
        else
        {
            this.DropDownListInputBanci.SelectedIndex = 1;
            this.TextBoxInputDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
        }
        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
    }
    //protected void Item_Selected(object sender, EventArgs e)
    //{
    //    DD_EQ.Visible = true;
    //    LB_EQ.Visible = true;
    //    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
    //}
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
            string sqlcheck = "Select eng from DailyInform where ID in" + sqlid;
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
                string myscript = @"alert('无权删除他人信息！');window.location.href='DailyInform.aspx';";
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
            string sqlcheck = "Select * from DailyInform where ID in" + sqlid;
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
                   isedit= true;
                    break;
                }
                else
                {
                    isedit= false;
                    break;
                }
            }
            if (isedit)
            {
                if (rd["status"].ToString() == "Open")
                {
                    this.Open.Visible = true;
                    this.Close.Visible = false;
                    ViewState["status"] = "Open";
                }
                else
                {
                    this.Open.Visible = false;
                    this.Close.Visible = true;
                    ViewState["status"] = "Close";
                }
               this.DD_Item.SelectedValue = rd["jitai"].ToString().Trim();
               this.DropDownListInputBanci.SelectedValue = rd["banci"].ToString().Trim();
               string content = rd["content"].ToString();
               content = Input.Outputadd(content);
               this.TextBoxDailyInform.Text = content;
               this.TextBoxInputDate.Text = rd["shortdate"].ToString();
               ViewState["sqlid"] = sqlid;
               ViewState["isedit"] = true;
               conn.Close();
               Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
             }
            else
            {
                conn.Close();
                string myscript = @"alert('无权编辑他人信息！');window.location.href='DailyInform.aspx';";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
            }
        }
        else
        {
            string myscript = @"alert('请选择编辑项！');window.location.href='DailyInform.aspx';";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
        }
    }
    #region 回复响应
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
            string sqlcheck = "Select * from DailyInform where ID in" + sqlid;
            string ConnectionString = ConfigurationManager.ConnectionStrings["mdbcon"].ConnectionString;
            OleDbConnection conn = new OleDbConnection(ConnectionString);
            OleDbCommand cmd = new OleDbCommand(sqlcheck, conn);
            conn.Open();
            OleDbDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                ViewState["reply"] = rd["reply"].ToString();
                ViewState["allreply"] = rd["allreply"].ToString();
                LabelReply.Text = rd["eng"].ToString();
                string banci = "夜班";
                int hour = DateTime.Now.Hour;
                if (hour > 12)
                {
                    banci = "白班";
                }
                TextBoxReply.Text = Session["name"].ToString() + "  " + DateTime.Now.ToString("yyyy/MM/dd HH:mm") + " " + banci + "回复：";
                if (rd["status"].ToString() == "Open")
                {
                    this.Open1.Visible = true;
                    this.Close1.Visible = false;
                    ViewState["status"] = "Open";
                }
                else
                {
                    this.Open1.Visible = false;
                    this.Close1.Visible = true;
                    ViewState["status"] = "Close";
                }
            }
            ViewState["sqlid"] = sqlid;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Reply');</script>");
        }
        else
        {
            string myscript = @"alert('请选择回复项！');window.location.href='DailyInform.aspx';";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
        }
    }
    #endregion
    protected void Search_Click(object sender, EventArgs e)
    {
        string chaxunstr = this.TextBoxSearch.Text.ToString();
        chaxunstr.Replace("%", "[%]");
        chaxunstr.Replace("';", "';';");
        chaxunstr.Replace("[", "[[]");
        chaxunstr.Replace("_", "[_]");
        chaxunstr = "%" + chaxunstr + "%";
        ViewState["sql"] = "Select TOP 100 * from DailyInform where jitai like '" + chaxunstr + "'or banci like '" + chaxunstr + "'or content like '" + chaxunstr + "'or eng like '" + chaxunstr + "'or shortdate like '" + chaxunstr + "'or reply like '" + chaxunstr + "' order by fabiaodate Desc,ID Desc";
        LoadData();
    }
    //添加内容函数
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
        else if(this.TextBoxInputDate.Text =="")
        {
            this.ToolTips1.Text = "日期不能为空！";
        }
        else
        {
            try
            {
                DateTime dt=Convert.ToDateTime(this.TextBoxInputDate.Text.ToString());
            }
            catch
            {
                isadd = false;
                string myscript = @"alert('日期格式不对！');window.location.href='DailyInform.aspx';";
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
                string banci = this.DropDownListInputBanci.SelectedValue;
                string informdate = this.TextBoxInputDate.Text.ToString();
                string content = this.TextBoxDailyInform.Text.ToString();
                content = Input.Inputadd(content);
                string eng = (string)Session["name"];
                string path = "";
                string uploadsql = "";
                string status = ViewState["status"].ToString();
                string sql = "Insert INTO DailyInform (jitai,[lindex],banci,content,eng,shortdate,status,[fabiaodate]) values ('" + jitai + "','" + lindex + "','" + banci + "','" + content + "','" + eng + "','" + informdate + "','" + status + "',#" + DateTime.Now.ToLocalTime() + "#)";
                if ((bool)ViewState["isedit"])
                {
                    sql = "Update DailyInform Set jitai='" + jitai + "',banci='" + banci + "',content='" + content + "',eng='" + eng + "',status='" + status + "',[lindex]='" + lindex + "',shortdate='" + informdate + "',fabiaodate=#" + DateTime.Now.ToLocalTime() + "# where ID in " + ViewState["sqlid"].ToString();
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
                    sql = "Insert INTO DailyInform (jitai,[lindex],banci,title,fujian,content,eng,shortdate,status,[fabiaodate]) values ('" + jitai + "','" + lindex + "','" + banci + "','" + title + "','" + path + "','" + content + "','" + eng + "','" + informdate + "','" + status + "',#" + DateTime.Now.ToLocalTime() + "#)";
                    if ((bool)ViewState["isedit"])
                    {
                        string sqlcheck = "Select fujian from DailyInform where ID in" + ViewState["sqlid"].ToString();
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
                            File.Delete(Server.MapPath(uploadfile));
                            string filesql = "Delete from UploadFiles where path='" + uploadfile + "'";
                            DbManager.ExecuteNonQuery(filesql);
                        }
                        sql = "Update DailyInform Set jitai='" + jitai + "',banci='" + banci + "',fujian='" + path+ "',title='" + title +  "',content='" + content + "',eng='" + eng + "',status='" + status + "',[lindex]='" + lindex + "',shortdate='" + informdate + "',fabiaodate=#" + DateTime.Now.ToLocalTime() + "# where ID in " + ViewState["sqlid"].ToString();
                    }
                }
                try
                {
                    if (this.FileUpload1.HasFile)
                    {
                        this.FileUpload1.PostedFile.SaveAs(Server.MapPath(path));
                        DbManager.ExecuteNonQuery(uploadsql);
                    }
                    DbManager.ExecuteNonQuery(sql);
                    OKMessage(content, "DailyInform.aspx");              
                }
                catch(Exception ex)
                {
                    this.ToolTips1.Text = "发布失败，请与管理员联系！"+ex.Message;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
                }
            }
        }
    }
    #region Add Reply OK
    protected void AddReply_OK_Click(object sender, EventArgs e)
    {
        string allreply = ViewState["allreply"] + "<br/>" + ViewState["reply"];
        string name = Session["name"].ToString();
        string status = ViewState["status"].ToString();
        string sql = "Update DailyInform Set reply='" + TextBoxReply.Text + "',allreply='" + allreply + "',[fabiaodate]='" + DateTime.Now.ToLocalTime() + "',status='" + status + "' where ID in " + ViewState["sqlid"].ToString();
        if (ViewState["reply"].ToString()=="")
        {
            sql = "Update DailyInform Set reply='" + TextBoxReply.Text + "',[fabiaodate]='" + DateTime.Now.ToLocalTime() + "',status='" + status + "' where ID in " + ViewState["sqlid"].ToString();
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
            sql = "Update DailyInform Set reply='" + TextBoxReply.Text + "',allreply='" + allreply + "',status='" + status + "',retitle='" + title + "',[fabiaodate]='" + DateTime.Now.ToLocalTime() + "',refujian='" + path + "' where ID in " + ViewState["sqlid"].ToString();
        }
        try
        {
            if (this.FileUpload2.HasFile)
            {
                this.FileUpload2.PostedFile.SaveAs(Server.MapPath(path));
                DbManager.ExecuteNonQuery(uploadsql);
            }
            DbManager.ExecuteNonQuery(sql);
            OKMessage(TextBoxReply.Text.ToString(), "DailyInform.aspx");
        }
        catch
        {
            this.ToolTips2.Text = "回复失败，请与管理员联系！";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Reply');</script>");
        }
    } 
    #endregion
    protected void Add_Cancel_Click(object sender, EventArgs e)
    {
        ViewState["isedit"] = false;
    }
    protected void Open_Click(object sender, EventArgs e)
    {
        this.Open.Visible = false;
        this.Close.Visible = true;
        ViewState["status"] = "Close";
        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
    }
    protected void Close_Click(object sender, EventArgs e)
    {
        this.Close.Visible = false;
        this.Open.Visible = true;
        ViewState["status"] = "Open";
        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
    }
    protected void Open_Reply_Click(object sender, EventArgs e)
    {
        this.Open1.Visible = false;
        this.Close1.Visible = true;
        ViewState["status"] = "Close";
        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Reply');</script>");
    }
    protected void Close_Reply_Click(object sender, EventArgs e)
    {
        this.Close1.Visible = false;
        this.Open1.Visible = true;
        ViewState["status"] = "Open";
        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Reply');</script>");
    }

    //跳转页面函数
     protected void Geren_Click(object sender, EventArgs e)
    {
        Response.Redirect("DNS.aspx");
    }
      protected void Inform_Click(object sender, EventArgs e)
    {
        Response.Redirect("DailyInform.aspx");
    }
    protected void Work_Click(object sender, EventArgs e)
    {
        Response.Redirect("DailyWork.aspx");
    }
    protected void Setup_Click(object sender, EventArgs e)
    {
        Response.Redirect("SetupReport.aspx");
    }
    protected void Task_Click(object sender, EventArgs e)
    {
        Response.Redirect("InformTask.aspx");
    }
      protected void personal_Click(object sender, EventArgs e)
    {
        Response.Redirect("personalInform.aspx");
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Alarm")
        {
            int i = Convert.ToInt32(e.CommandArgument);
            string alarmid = GridView1.DataKeys[i][1].ToString();
            string alarmsql = "Select * from Alarm where ID in ("+alarmid+")";
            string ConnectionString = ConfigurationManager.ConnectionStrings["mdbcon"].ConnectionString;
            OleDbConnection conn = new OleDbConnection(ConnectionString);
            OleDbCommand cmd = new OleDbCommand(alarmsql, conn);
            conn.Open();
            OleDbDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                Label_Alarm_Info.Text ="<b>"+sdr["alarm"].ToString()+"</b><br/>"+sdr["jitai"].ToString() + "  " + sdr["danyuan"].ToString() + "  " + sdr["downtime"].ToString() + "  " + sdr["hours"].ToString() + "h" + "<br/><b>分析：</b><br/>"
                    + sdr["fenxi"].ToString() + "<br/><b>Action：</b><br/>" + sdr["action"].ToString() + "<br/><b>影响：</b><br/>" + sdr["yingxiang"].ToString() + "<br/><b>工程师：</b><br/>" +
                    sdr["eng"].ToString();
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('alarm_dlg');</script>");
        }
    }
}
