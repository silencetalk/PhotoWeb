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

public partial class WebPages_PM : System.Web.UI.Page
{
    protected void LoadData()
    {
        GridView1.Visible = true;
        GridView2.Visible = false;
        string sql = ViewState["sql"].ToString();
        DataTable dt = DbManager.ExecuteQuery(sql);
        this.GridView1.DataSource = dt.DefaultView;
        this.GridView1.DataBind();
    }

    protected void LoadTask()
    {
        GridView1.Visible = false;
        GridView2.Visible = true;
        string sql = ViewState["tasksql"].ToString();
        DataTable dt = DbManager.ExecuteQuery(sql);
        this.GridView2.DataSource = dt.DefaultView;
        this.GridView2.DataBind();
    }
    protected void LoadItems()
    {
        string sqlcheck = "Select Distinct item from PMState Order by item";
        string ConnectionString = ConfigurationManager.ConnectionStrings["mdbcon"].ConnectionString;
        OleDbConnection conn = new OleDbConnection(ConnectionString);
        OleDbCommand cmd = new OleDbCommand(sqlcheck, conn);
        conn.Open();
        OleDbDataReader rd = cmd.ExecuteReader();
        DD_Xiangmu.Items.Add("请选择项目");
        while (rd.Read())
        {
            
            DD_Xiangmu.Items.Add(rd["item"].ToString());
        }
        DD_Xiangmu.Items.Add("待办事项");
        rd.Close();
        conn.Close();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["isedit"] = false;
            ViewState["sqlid"] = "";
            ViewState["sql"] = "Select top 20 * from PMState Order by last Desc,item";
            LoadData();
            LoadItems();
        }
        this.GridView1.Attributes.Add("style", "table-layout:fixed;word-wrap:break-word;");
    }

    protected void Load_Changed(object sender, EventArgs e)
    {
        DateTime duedatefiler = DateTime.Now.AddDays(30);
        CB_ShowAll.Checked = false;
        CB_ShowAll.Visible = false;
        if (DD_Jitai.SelectedIndex == 0)
        {
            if (DD_Xiangmu.SelectedIndex == 0)
            {
                ViewState["sql"] = "Select top 20 * from PMState Order by duedate";
            }
            else
            {
                if (DD_Xiangmu.SelectedValue == "待办事项")
                {
                    ViewState["tasksql"] = "Select * from PMTask where Status='Open' Order by eq,unit";
                }
                else
                {
                    ViewState["sql"] = "Select * from PMState where item='" + DD_Xiangmu.SelectedValue + "' AND duedate<=#" + duedatefiler.ToLocalTime() + "# Order by duedate";
                }
            }
        }
        else
        {
            if (DD_Xiangmu.SelectedIndex == 0)
            {
                ViewState["sql"] = "Select * from PMState where EQ='" + DD_Jitai.SelectedValue + "' AND duedate<=#" + duedatefiler.ToLocalTime() + "# Order by duedate";
            }
            else
            {
                CB_ShowAll.Visible = true;
                if (DD_Xiangmu.SelectedValue == "待办事项")
                {
                    ViewState["tasksql"] = "Select * from PMTask where Status='Open' AND eq='" + DD_Jitai.SelectedValue + "' Order by unit";
                }
                else
                {
                    ViewState["sql"] = "Select * from PMState where EQ='" + DD_Jitai.SelectedValue + "' AND item='" + DD_Xiangmu.SelectedValue + "' AND duedate<=#" + duedatefiler.ToLocalTime() + "# Order by unit,duedate";
                }
            }
        }
        if (DD_Xiangmu.SelectedValue == "待办事项")
            LoadTask();
        else
            LoadData();
    }
    protected void LoadAll_Changed(object sender, EventArgs e)
    {
        if (CB_ShowAll.Checked)
        {
            if (DD_Xiangmu.SelectedValue == "待办事项")
            {
                ViewState["tasksql"] = "Select * from PMTask where eq='" + DD_Jitai.SelectedValue + "' Order by unit";
            }
            else
            {
                ViewState["sql"] = "Select * from PMState where EQ='" + DD_Jitai.SelectedValue + "' AND item='" + DD_Xiangmu.SelectedValue + "' Order by unit,duedate";
            }
        }
        else
        {
            if (DD_Xiangmu.SelectedValue == "待办事项")
            {
                ViewState["tasksql"] = "Select * from PMTask where Status='Open' AND eq='" + DD_Jitai.SelectedValue + "' Order by unit";
            }
            else
            {
                DateTime duedatefiler = DateTime.Now.AddDays(30);
                ViewState["sql"] = "Select * from PMState where EQ='" + DD_Jitai.SelectedValue + "' AND item='" + DD_Xiangmu.SelectedValue + "' AND duedate<=#" + duedatefiler.ToLocalTime() + "# Order by unit,duedate";
            }
        }
        if (DD_Xiangmu.SelectedValue == "待办事项")
            LoadTask();
        else
            LoadData();
    }

    //工具按钮函数

    protected void Add_NewItem_Click(object sender, EventArgs e)
    {
        //TB_Add_Date.Text = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");
        //ToolTips_AddNewItem.Text = "请选择机台，单元";
        //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('dlg_add_item');</script>");
        Response.Redirect("NewPMItem.aspx");
    }
    protected void Add_NewTask_Click(object sender, EventArgs e)
    {
        //TB_Task_Date.Text = DateTime.Now.AddDays(30).ToString("yyyy/MM/dd");
        Tooltips_Task.Text = "";
        DD_EQ.Enabled = true;
        DD_Unit.Enabled = true;
        ViewState["isedit"] = false;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Task');</script>");
    }

    //添加数据函数

    protected void Add_OK_Click(object sender, EventArgs e)
    {
        string eq = LB_Add_EQ.Text;
        string unit = LB_Add_Unit.Text;
        string item = LB_Add_Item.Text;
        string position = LB_Add_Position.Text;
        string beizhu = Input.Inputadd(TB_Add_Beizhu.Text.ToString());
        int zhouqi = Convert.ToInt32(ViewState["zhouqi"].ToString());
        DateTime lastdate = Convert.ToDateTime(TB_Add_Date.Text.ToString());
        DateTime duedate = lastdate.AddDays(zhouqi);
        string sqlupdate = "Update PMState Set [last]=#"+lastdate.ToLocalTime()+"#,[duedate]=#"+duedate.ToLocalTime()+"#,beizhu='"+beizhu+"' where ID in ("+ViewState["sqlid"].ToString()+")";
        string sqlinsert = "Insert into PMList (EQ,[unit],[item],[beizhu],[position],[eng],[last],[duedate]) Values ('" +
            eq + "','" + unit + "','" + item + "','" + beizhu + "','" + position + "','" + Session["name"].ToString() + "',#" + lastdate.ToLocalTime() + "#,#" + duedate.ToLocalTime() + "#)";
        try
        {
            DbManager.ExecuteNonQuery(sqlupdate);
            DbManager.ExecuteNonQuery(sqlinsert);
            LB_OK.Text = "发布成功！";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('dlg_ok');</script>");
            Load_Changed(sender, e);
        }
        catch
        {
            LB_OK.Text = "发布失败！";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('dlg_ok');</script>");
            Load_Changed(sender, e);
        }
    }

    protected void Add_Task_Click(object sender, EventArgs e)
    {
        if (TB_Task.Text == "")
        {
            Tooltips_Task.Text = "事项名称不能为空!";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Task');</script>");
        }
        else
        {
            ArrayList sqllist = new ArrayList();
            if ((bool)ViewState["isedit"])
            {
                string sql="Update PMTask Set beizhu='" + Input.Inputadd(TB_Task_Beizhu.Text) + "',[lastdate]=#" + DateTime.Now.ToLocalTime() + "# where ID in (" + ViewState["sqlid"].ToString() + ")";
                if (CB_Task.Checked)
                    sql = "Update PMTask Set status='Close',beizhu='" + Input.Inputadd(TB_Task_Beizhu.Text) + "',[lastdate]=#" + DateTime.Now.ToLocalTime() + "# where ID in (" + ViewState["sqlid"].ToString() + ")";
                sqllist.Add(sql);
            }
            else
            {
                if (DD_EQ.SelectedIndex != 0)
                {
                    string sql = "Insert into PMTask ([eq],[unit],[taskname],[status],[beizhu],[eng]) values ('" +
                        DD_EQ.SelectedValue + "','" + DD_Unit.SelectedValue + "','" + TB_Task.Text + "','Open','" + Input.Inputadd(TB_Task_Beizhu.Text) +
                        "','" + Session["name"].ToString() + "')";
                    sqllist.Add(sql);
                }
                else
                {
                    for (int i = 0; i < 16; i++)
                    {
                        string sql = "Insert into PMTask ([eq],[unit],[taskname],[status],[beizhu],[eng]) values ('" +
                        DD_EQ.Items[i+1].Text + "','" + DD_Unit.SelectedValue + "','" + TB_Task.Text + "','Open','" + Input.Inputadd(TB_Task_Beizhu.Text) +
                        "','" + Session["name"].ToString() + "')";
                        sqllist.Add(sql);
                    }
                }
            }
            if (sqllist.Count > 0)
            {
                string ConnectionString = ConfigurationManager.ConnectionStrings["mdbcon"].ConnectionString;
                OleDbConnection conn = new OleDbConnection(ConnectionString);
                conn.Open();
                OleDbCommand cmd = new OleDbCommand();
                OleDbTransaction tx = conn.BeginTransaction();
                cmd.Connection = conn;
                cmd.Transaction = tx;
                try
                {
                    for (int i = 0; i < sqllist.Count; i++)
                    {
                        cmd.CommandText = sqllist[i].ToString();
                        cmd.ExecuteNonQuery();
                    }
                    tx.Commit();
                    LB_OK.Text = "发布成功！";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('dlg_ok');</script>");
                    Load_Changed(sender, e);
                }
                catch
                {
                    tx.Rollback();
                    LB_OK.Text = "发布失败！";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('dlg_ok');</script>");
                    Load_Changed(sender, e);
                }
                finally
                {
                    tx.Dispose();
                    cmd.Dispose();
                    conn.Dispose();
                }
            }
        }
    }

    protected void Add_Cancel_Click(object sender, EventArgs e)
    {
        ViewState["isedit"] = false;
        Load_Changed(sender, e);
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
        if (e.CommandName == "dataupdate")
        {
            int i = Convert.ToInt32(e.CommandArgument);
            ViewState["sqlid"] = GridView1.DataKeys[i][0].ToString();
            LB_Add_EQ.Text = GridView1.DataKeys[i][1].ToString();
            LB_Add_Unit.Text = GridView1.DataKeys[i][2].ToString();
            LB_Add_Item.Text = GridView1.DataKeys[i][3].ToString();
            LB_Add_Position.Text = GridView1.DataKeys[i][4].ToString();
            ViewState["zhouqi"] = GridView1.DataKeys[i][5].ToString();
            TB_Add_Date.Text = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        if (e.CommandName == "more")
        {
            int i = Convert.ToInt32(e.CommandArgument);
            Session["eq"] = GridView1.DataKeys[i][1].ToString();
            Session["unit"]= GridView1.DataKeys[i][2].ToString();
            Session["item"]= GridView1.DataKeys[i][3].ToString();
            Session["position"]= GridView1.DataKeys[i][4].ToString();
            Session["lindex"] = GridView1.DataKeys[i][5].ToString();
            Response.Redirect("PMList.aspx");
        }
    }

    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "TaskUpdate")
        {
            int i=Convert.ToInt32(e.CommandArgument);
            ViewState["sqlid"] = GridView2.DataKeys[i][0].ToString();
            DD_EQ.SelectedValue = GridView2.DataKeys[i][1].ToString();
            DD_Unit.SelectedValue = GridView2.DataKeys[i][2].ToString();
            TB_Task.Text = GridView2.DataKeys[i][3].ToString();
            TB_Task_Beizhu.Text=Input.Outputadd(GridView2.DataKeys[i][4].ToString());
            CB_Task.Checked = true;
            DD_EQ.Enabled = false;
            DD_Unit.Enabled = false;
            ViewState["isedit"] = true;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Task');</script>");
        }
    }
}
