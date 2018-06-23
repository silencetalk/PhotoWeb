using System;
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

public partial class WebPages_InformTask : System.Web.UI.Page
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
        if(!IsPostBack)
        {
            ViewState["sql"] = "Select * from InformTask Order by ID Desc";
            ViewState["isedit"] = false;
            ViewState["sqlid"] = "";
            TB_Add_SubTime.Text = DateTime.Now.ToString("yyyy/MM/dd");
            LoadData();
        }
        this.GridView1.Attributes.Add("style", "table-layout:fixed;word-wrap:break-word;");
    }
    protected void Add_Click(object sender, EventArgs e)
    {
        DD_Add_Item.SelectedIndex = 0;
        TB_Add_Content.Text = "";
        ToolTips.Text = "";
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
            string sqldelete = "Delete from InformTask where ID in" + sqlid;
            string sqlcheck = "Select eng from InformTask where ID in" + sqlid;
            string ConnectionString = ConfigurationManager.ConnectionStrings["mdbcon"].ConnectionString;
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
                    DbManager.ExecuteNonQuery(sqldelete);
                    string myscript = @"alert('删除成功！');window.location.href='InformTask.aspx';";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
                }
                catch
                {
                    string myscript = @"alert('删除失败，请与管理员联系！');window.location.href='InformTask.aspx';";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
                }
            }
            else
            {
                string myscript = @"alert('无权删除他人信息！');window.location.href='InformTask.aspx';";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
            }
        }
        else
        {
            string myscript = @"alert('请选择删除项！');window.location.href='InformTask.aspx';";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
        }
    }
    protected void Time_Option_Change(object sender, EventArgs e)
    {
        switch (DD_Add_Time.SelectedIndex)
        {
            case 1:
                {
                    DD_Add_SubTime.Items.Clear();
                    TB_Add_SubTime.Visible = false;
                    string[] weekIndex = { "星期一", "星期二", "星期三", "星期四", "星期五", "星期六", "星期日" };
                    ListItem[] weekList=new ListItem[7];
                    for (int i = 0; i < 7; i++)
                    {
                        ListItem li = new ListItem();
                        li.Text = weekIndex[i];
                        li.Value = (i+1).ToString();
                        weekList[i] = li;
                    }
                    DD_Add_SubTime.Items.AddRange(weekList);
                    DD_Add_SubTime.Visible = true;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
                    break;
                }
            case 2:
                {
                    DD_Add_SubTime.Items.Clear();
                    TB_Add_SubTime.Visible = false;
                    string[] monthIndex = new string[31];
                    ListItem[] monthList = new ListItem[31];
                    for (int i = 0; i < 31; i++)
                    {
                        monthIndex[i] = (i + 1).ToString()+"号";
                        ListItem li = new ListItem();
                        li.Text = monthIndex[i];
                        li.Value = (i+1).ToString();
                        monthList[i] = li;
                    }
                    DD_Add_SubTime.Items.AddRange(monthList);
                    DD_Add_SubTime.SelectedIndex = 14;
                    DD_Add_SubTime.Visible = true;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
                    break;
                }
            case 3:
                {
                    DD_Add_SubTime.Visible = false;
                    TB_Add_SubTime.Visible = true;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
                    break;
                }
            default:
                {
                    DD_Add_SubTime.Visible = false;
                    TB_Add_SubTime.Visible = false;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
                    break;
                }
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
            string sqlcheck = "Select * from InformTask where ID in" + sqlid;
            string ConnectionString = ConfigurationManager.ConnectionStrings["mdbcon"].ConnectionString;
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
                if (rd["status"].ToString() == "Open")
                {
                    this.Open.Visible = true;
                    this.Close.Visible = false;
                }
                else
                {
                    this.Open.Visible = false;
                    this.Close.Visible = true;
                }
                DD_Add_Item.SelectedValue = rd["item"].ToString();
                DD_Add_Time.SelectedValue = rd["zhouqi"].ToString();
                TB_Add_Content.Text = Input.Outputadd(rd["content"].ToString());
                ViewState["sqlid"] = sqlid;
                ViewState["isedit"] = true;
                conn.Close();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
            }
            else
            {
                conn.Close();
                string myscript = @"alert('无权编辑他人信息！');window.location.href='InformTask.aspx';";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
            }
        }
        else
        {
            string myscript = @"alert('请选择编辑项！');window.location.href='InformTask.aspx';";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
        }
    }
    protected void Open_Click(object sender, EventArgs e)
    {
        this.Open.Visible = false;
        this.Close.Visible = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
    }
    protected void Close_Click(object sender, EventArgs e)
    {
        this.Close.Visible = false;
        this.Open.Visible = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
    }
    protected void Add_OK_Click(object sender, EventArgs e)
    {
        if(DD_Add_Item.SelectedIndex==0)
        {
            ToolTips.Text = "请选择项目/机台";
            ToolTips.Visible = true;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        else if (TB_Add_Content.Text == "")
        {
            ToolTips.Text = "交接内容不能为空";
            ToolTips.Visible = true;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        else
        {
            string status="Close";
            if(Open.Visible)
            {
                status="Open";
            }
            string fullzhouqi = "每天";
            string nextdate = "";
            DateTime dt = new DateTime();
            switch (DD_Add_Time.SelectedIndex)
            {
                case 0:
                    {
                        dt = DateTime.Now.AddDays(1);
                        nextdate = dt.ToString("yyyy/MM/dd");
                        break;
                    }
                case 1:
                    {
                        dt = DateManage.GetWeekUpOfDate(DateTime.Now, DD_Add_SubTime.SelectedIndex + 1, 1);
                        nextdate = dt.ToString("yyyy/MM/dd");
                        fullzhouqi = "每" + DD_Add_SubTime.SelectedItem.Text;
                        break;
                    }
                case 2:
                    {
                        dt = DateManage.GetDayUpOfDate(DateTime.Now, DD_Add_SubTime.SelectedIndex + 1, 1);
                        nextdate = dt.ToString("yyyy/MM/dd");
                        fullzhouqi = "每" + DD_Add_SubTime.SelectedItem.Text;
                        break;
                    }
                case 3:
                    {
                        nextdate = TB_Add_SubTime.Text;
                        fullzhouqi = DD_Add_Time.SelectedValue;
                        break;
                    }
            }
            string sql = "Insert into InformTask (item,content,zhouqi,dateindex,fullzhouqi,nextdate,status,eng,longdate) Values ('" +
                DD_Add_Item.SelectedValue + "','" + Input.Inputadd(TB_Add_Content.Text) + "','" + DD_Add_Time.SelectedValue + "','" + DD_Add_SubTime.SelectedValue + "','" + fullzhouqi + "','"+nextdate+"','"+
                status+"','"+Session["name"]+"',#"+DateTime.Now.ToLocalTime()+"#)";
            if ((bool)ViewState["isedit"])
            {
                sql = "Update InformTask Set item='" + DD_Add_Item.SelectedValue + "',content='" + Input.Inputadd(TB_Add_Content.Text)
                      + "',zhouqi='" + DD_Add_Time.SelectedValue + "',dateindex='" + DD_Add_SubTime.SelectedValue +"',fullzhouqi='" + fullzhouqi + "',nextdate='" + nextdate
                      + "',status='" + status + "',eng='" + Session["name"].ToString() + "',longdate=#" + DateTime.Now.ToLocalTime()
                      + "# where ID in (" + ViewState["sqlid"].ToString() + ")";
                    ;
            }
            try
            {
                DbManager.ExecuteNonQuery(sql);
                string myscript = @"alert('发布成功！');window.location.href='InformTask.aspx';";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
            }
            catch
            {
                ToolTips.Text = "发布失败，请检查交接内容重试或与管理员联系！";
                ToolTips.Visible = true;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
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
