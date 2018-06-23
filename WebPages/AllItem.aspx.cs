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

public partial class WebPages_AllItem : System.Web.UI.Page
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
            ViewState["sql"]="Select * from AllItem where itemstatus='open' Order by ID Desc";
            LoadData();
        }
    }

    #region 工具响应函数

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

    protected void Add_Click(object sender, EventArgs e)
    {
        ViewState["status"] = "Open";
        this.Close.Visible = false;
        this.Open.Visible = true;
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
            string sqlcheck = "Select itemstatus,item,beizhu,unit from AllItem where ID in" + sqlid;
            string ConnectionString = ConfigurationManager.ConnectionStrings["mdbcon"].ConnectionString;
            OleDbConnection conn = new OleDbConnection(ConnectionString);
            OleDbCommand cmd = new OleDbCommand(sqlcheck, conn);
            conn.Open();
            OleDbDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                if (rd["itemstatus"].ToString() == "Open")
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
                TB_Add_Item.Text = Input.Outputadd(rd["item"].ToString());
                TB_Add_Beizhu.Text = Input.Outputadd(rd["beizhu"].ToString());
                DropDownListDanyuan.SelectedValue = rd["unit"].ToString();
                ViewState["sqlid"] = sqlid;
                ViewState["isedit"] = true;
                conn.Close();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
            }
            else
            {
                conn.Close();
                string myscript = @"alert('读取数据库失败！');window.location.href='AllItem.aspx';";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
            }
        }
        else
        {
            string myscript = @"alert('请选择编辑项！');window.location.href='AllItem.aspx';";
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
            string sqldelete = "Delete from AllItem where ID in" + sqlid;
            try
            {
                int i = DbManager.ExecuteNonQuery(sqldelete);
                string myscript = @"alert('删除成功！');window.location.href='AllItem.aspx';";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
            }
            catch
            {
                string myscript = @"alert('删除失败，请与管理员联系！');window.location.href='AllItem.aspx';";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
            }
        }
        else
        {
            string myscript = @"alert('请选择删除项！');window.location.href='AllItem.aspx';";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
        }
    }

    protected void Search_Click(object sender, EventArgs e)
    {
        string chaxunstr = TextBoxSearch.Text.ToString();
        chaxunstr.Replace("%", "[%]");
        chaxunstr.Replace("';", "';';");
        chaxunstr.Replace("[", "[[]");
        chaxunstr.Replace("_", "[_]");
        chaxunstr = "%" + chaxunstr + "%";
        ViewState["sql"] = "Select * from AllItem where item like '" + chaxunstr + "' Order by ID Desc";
        LoadData();
    }

    #endregion

    #region 更新机台状态

    protected void Update_Click(object sender, EventArgs e)
    {
        string sql = "Update AllItem Set "+ViewState["ph"].ToString()+"status='"+RadioButtonList1.SelectedValue+
            "'," + ViewState["ph"].ToString() + "date='" + TB_Update_Date.Text + "'," + ViewState["ph"].ToString()+
            "beizhu='"+Input.Inputadd(TB_Update_Beizhu.Text.ToString())+"' where ID in("+ViewState["updateid"].ToString()+")";
        try
        {
            DbManager.ExecuteNonQuery(sql);
            string myscript = @"alert('操作成功！');window.location.href='AllItem.aspx';";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
        }
        catch
        {
            string myscript = @"alert('操作失败！');window.location.href='AllItem.aspx';";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
        }
    }

    protected void Update_Cancel_Click(object sender, EventArgs e)
    {

    }

    #endregion

    #region 添加信息函数

    protected void Add_OK_Click(object sender, EventArgs e)
    {
        if (DropDownListDanyuan.SelectedIndex == 0)
        {
            ToolTips.Text = "*请选择单元";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        else if (TB_Add_Item.Text == "")
        {
            ToolTips.Text = "*项目不能为空";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        else
        {
            string sql = "Insert into AllItem (item,unit,itemstatus,beizhu,itemdate,eng) values ('"+Input.Inputadd(TB_Add_Item.Text)+"','"+
                DropDownListDanyuan.SelectedValue+"','Open','"+Input.Inputadd(TB_Add_Beizhu.Text)+"','"+DateTime.Now.ToString("yyyy/MM/ss")+"','"+Session["name"].ToString()+"')";
            if ((bool)ViewState["isedit"])
            {
                sql = "Update AllItem Set item='"+Input.Inputadd(TB_Add_Item.Text)+"',unit='"+DropDownListDanyuan.SelectedValue+"',itemdate='"+
                    DateTime.Now.ToString("yyyy/MM/ss")+"',itemstatus='"+ViewState["status"].ToString()+"',beizhu='"+Input.Inputadd(TB_Add_Beizhu.Text)+"' where ID in ("+
                    ViewState["sqlid"].ToString()+")";
            }
            try
            {
                DbManager.ExecuteNonQuery(sql);
                string myscript = @"alert('操作成功！');window.location.href='AllItem.aspx';";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
            }
            catch
            {
                string myscript = @"alert('操作失败！');window.location.href='AllItem.aspx';";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
            }
        }
    }

    protected void Add_Cancel_Click(object sender, EventArgs e)
    {
        ViewState["isedit"] = false;
    }
    #endregion

    #region GridView函数

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
            string eq = e.CommandArgument.ToString();
            string splitflag = "+";
            string[] eqinfo = eq.Split(splitflag.ToCharArray(), System.StringSplitOptions.RemoveEmptyEntries);
            int index = Convert.ToInt32(eqinfo[0]);
            ViewState["ph"] = eqinfo[1];
            ViewState["updateid"] = GridView1.DataKeys[index][0].ToString();
            string updatesql = "Select * from AllItem where ID in("+ViewState["updateid"].ToString()+")";
            string ConnectionString = ConfigurationManager.ConnectionStrings["mdbcon"].ConnectionString;
            OleDbConnection conn = new OleDbConnection(ConnectionString);
            OleDbCommand cmd = new OleDbCommand(updatesql, conn);
            conn.Open();
            OleDbDataReader rd = cmd.ExecuteReader();
            try
            {
                rd.Read();
                LB_Update_EQ.Text = eqinfo[1];
                TB_Update_Date.Text = DateTime.Now.ToString("yyyy/MM/dd");
                TB_Update_Beizhu.Text = Input.Outputadd(rd[eqinfo[1] + "beizhu"].ToString());
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Update_Dialog');</script>");
            }
            catch
            {
                string myscript = @"alert('数据操作失败！');window.location.href='AllItem.aspx';";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
            }
            finally
            {
                rd.Dispose();
                conn.Dispose();
            }
        }
    }

    #endregion
}
