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

public partial class WebPages_Parts : System.Web.UI.Page
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
            string group = "Select DISTINCT GroupName from PartsList";
            string ConnectionString = ConfigurationManager.ConnectionStrings["mdbcon"].ConnectionString;
            OleDbConnection conn = new OleDbConnection(ConnectionString);
            OleDbCommand cmd = new OleDbCommand(group, conn);
            conn.Open();
            OleDbDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                DD_Group.Items.Add(rd["GroupName"].ToString());
            }
            rd.Close();
            conn.Close();
            TB_Add_Count.Text = "0";
            ViewState["isedit"] = false;
            ViewState["sqlid"] = "";
            ViewState["sql"] = "Select * from PartsList Order by LastDate Desc,GroupName";
            LoadData();
        }
    }

    protected void Load_Changed(object sender, EventArgs e)
    {
        if (DD_Group.SelectedIndex == 0)
        {
            ViewState["sql"] = "Select * from PartsList Order by LastDate Desc,GroupName";
        }
        else
        {
            ViewState["sql"] = "Select * from PartsList where GroupName='" + DD_Group.SelectedValue + "' Order by LastDate Desc,GroupName";
        }
        LoadData();
    }

    protected void Add_Click(object sender, EventArgs e)
    {
        TB_Add_Count.Text = "0";
        TB_Add_PartsGroup.Text = "";
        TB_Add_PartsName.Text = "";
        TB_Add_CHNName.Text = "";
        TB_Add_Beizhu.Text = "";
        TB_Add_BOENo.Text = "";
        TB_Add_CHNName.Text = "";
        //TB_Add_Date.Text = DateTime.Now.ToString("yyyy/MM/dd");
        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
    }
    protected void Add_OK_Click(object sender, EventArgs e)
    {
        if (TB_Add_PartsName.Text == "")
        {
            ToolTips.Text = "<span style='font-family:fontawesome !important; color:Red;'>&#xf071;</span>请填写PartsName！";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        else if(TB_Add_CHNName.Text=="")
        {
            ToolTips.Text = "<span style='font-family:fontawesome !important; color:Red;'>&#xf071;</span>请填写中文名称！";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        else if (TB_Add_PartsGroup.Text=="")
        {
            ToolTips.Text = "<span style='font-family:fontawesome !important; color:Red;'>&#xf071;</span>请填写物料组！";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        else
        {
            string sql = "Insert Into PartsList (PartsName,CHNName,GroupName,BOENo,VenderNo,Beizhu,[Count],[LastDate],Eng) Values ('" + TB_Add_PartsName.Text
                + "','" + TB_Add_CHNName.Text + "','"+ TB_Add_PartsGroup.Text+"','"+ TB_Add_BOENo.Text + "','" + TB_Add_VenderNo.Text + "','" + Input.Inputadd(TB_Add_Beizhu.Text.ToString()) + "','" + TB_Add_Count.Text
                + "',#" + DateTime.Now.ToLocalTime()+ "#,'"+Session["name"].ToString()+"')";
            string historysql = "Insert Into PartsHistory ([PartsID],[Count],Beizhu,[LastDate],Eng) Values ('" + ViewState["sqlid"].ToString() + "','" + TB_Add_Count.Text + "','" + Input.Inputadd(TB_Add_Beizhu.Text.ToString()) + "',#" + DateTime.Now.ToLocalTime() + "#,'"+Session["name"].ToString()+"')";
            if ((bool)ViewState["isedit"])
            {
                sql = "Update PartsList Set Beizhu='"+Input.Inputadd(TB_Add_Beizhu.Text.ToString())+"',Eng='"+Session["name"].ToString()+"',[Count]='"+TB_Add_Count.Text+"',[LastDate]=#"+DateTime.Now.ToLocalTime()+"# where ID in ("+ViewState["sqlid"].ToString()+")";
            }
            try
            {
                DbManager.ExecuteNonQuery(sql);
                if ((bool)ViewState["isedit"])
                {
                    DbManager.ExecuteNonQuery(historysql);
                }
                ViewState["isedit"] = false;
                string myscript = @"alert('发布成功！');window.location.href='Parts.aspx';";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
            }
            catch
            {
                ToolTips.Text = "<span style='font-family:fontawesome !important; color:Red;'>&#xf071;</span>数据录入出错！";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
            }
        }
    }
    protected void Add_Cancel_Click(object sender, EventArgs e)
    {
        ViewState["isedit"] = false;
    }
    protected void Plus_Click(object sender, EventArgs e)
    {
        int count = Convert.ToInt32(TB_Add_Count.Text);
        if (RB_Count.SelectedIndex == 0)
        {
            count += 1;
        }
        else
        {
            count += 5 * RB_Count.SelectedIndex;
        }
        TB_Add_Count.Text = count.ToString();
        if (count == 0)
        {
            Add_Minus.Disabled = true;
        }
        else
        {
            Add_Minus.Disabled = false;
        }
    }
    protected void Minus_Click(object sender, EventArgs e)
    {
        int count = Convert.ToInt32(TB_Add_Count.Text);
        if (RB_Count.SelectedIndex == 0)
        {
            count -= 1;
        }
        else
        {
            count -= 5 * RB_Count.SelectedIndex;
        }
        TB_Add_Count.Text = count.ToString();
        if (count == 0)
        {
            Add_Minus.Disabled = true;
        }
        else
        {
            Add_Minus.Disabled = false;
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
        ViewState["sql"] = "Select * from PartsList where PartsName like '" + chaxunstr + "'or CHNName like '" + chaxunstr + "'or BOENo like '" + chaxunstr + "'or VenderNo like '" + chaxunstr + "'or [LastDate] like '" + chaxunstr + "'or Beizhu like '" + chaxunstr + "' order by LastDate Desc,GroupName";
        LoadData();
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
        if (e.CommandName == "add_update")
        {
            int i = Convert.ToInt32(e.CommandArgument);
            ViewState["sqlid"] = GridView1.DataKeys[i][0].ToString();
            TB_Add_PartsName.Text = GridView1.DataKeys[i][1].ToString();
            TB_Add_CHNName.Text = GridView1.DataKeys[i][2].ToString();
            TB_Add_PartsGroup.Text = GridView1.DataKeys[i][3].ToString();
            TB_Add_BOENo.Text = GridView1.DataKeys[i][4].ToString();
            TB_Add_VenderNo.Text = GridView1.DataKeys[i][5].ToString();
            TB_Add_Count.Text = GridView1.DataKeys[i][6].ToString();
            int count = Convert.ToInt32(TB_Add_Count.Text);
            if (count == 0)
            {
                Add_Minus.Disabled = true;
            }
            else
            {
                Add_Minus.Disabled = false;
            }
            ViewState["isedit"] = true;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        if (e.CommandName == "more")
        {
            int i = Convert.ToInt32(e.CommandArgument);
            ViewState["sql"] = "SELECT PartsList.BOENo, PartsList.CHNName, PartsList.PartsName, PartsList.VenderNo, PartsList.GroupName, PartsList.ID, " +
            "PartsHistory.PartsID, PartsHistory.Count, PartsHistory.Beizhu, PartsHistory.LastDate, PartsHistory.Eng FROM PartsList Inner Join PartsHistory On ((PartsList.ID=PartsHistory.PartsID) AND" +
            " (PartsHistory.[PartsID]="+GridView1.DataKeys[i][0].ToString()+")) Order by PartsHistory.LastDate Desc";
            LoadData();
        }
    }
}
