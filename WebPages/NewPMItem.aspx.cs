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

public partial class WebPages_NewPMItem : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Unit_Selected(sender, e);
            //TB_Date.Text = DateTime.Now.ToString("yyyy/MM/dd");
        }
    }

    protected void Unit_Selected(object sender, EventArgs e)
    {
        DD_Items.Items.Clear();
        if (DD_Item_Option.SelectedIndex > 0)
        {
            DD_Items.Visible = false;
            TB_New_Item.Visible = true;
            DD_Position.Items.Clear();
            DD_Position_Option.SelectedIndex = 1;
            DD_Position.Visible = false;
            TB_New_Position.Visible = true;
            TB_Cycle.Text = "";
        }
        else
        {
            DD_Items.Visible = true;
            TB_New_Item.Visible = false;
            string sqlcheck = "Select DISTINCT(item) from PMState where unit ='" + DD_Unit.SelectedValue + "'";
            if(DD_EQ.SelectedIndex!=0)
                sqlcheck = "Select DISTINCT(item) from PMState where eq='"+DD_EQ.SelectedValue+"' AND unit ='" + DD_Unit.SelectedValue + "'";
            string ConnectionString = ConfigurationManager.ConnectionStrings["mdbcon"].ConnectionString;
            OleDbConnection conn = new OleDbConnection(ConnectionString);
            OleDbCommand cmd = new OleDbCommand(sqlcheck, conn);
            conn.Open();
            OleDbDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                DD_Items.Items.Add(rd["item"].ToString());
            }
            rd.Close();
            conn.Close();
            Item_Selected(sender, e);
        }
    }

    protected void Item_Selected(object sender, EventArgs e)
    {
        DD_Position.Items.Clear();
        if (DD_Position_Option.SelectedIndex > 0)
        {
            DD_Position.Visible = false;
            TB_New_Position.Visible = true;
            TB_Cycle.Text = "";
        }
        else
        {
            DD_Position.Visible = true;
            TB_New_Position.Visible = false;
            string sqlcheck = "Select DISTINCT [position] from PMState where [unit] ='" + DD_Unit.SelectedValue + "' AND [item]='" + DD_Items.SelectedValue + "'";
            if(DD_EQ.SelectedIndex!=0)
                sqlcheck = "Select DISTINCT [position] from PMState where eq='"+DD_EQ.SelectedValue+"' AND [unit] ='" + DD_Unit.SelectedValue + "' AND [item]='" + DD_Items.SelectedValue + "'";
            string ConnectionString = ConfigurationManager.ConnectionStrings["mdbcon"].ConnectionString;
            OleDbConnection conn = new OleDbConnection(ConnectionString);
            OleDbCommand cmd = new OleDbCommand(sqlcheck, conn);
            conn.Open();
            OleDbDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                DD_Position.Items.Add(rd["position"].ToString());
            }
            rd.Close();
            conn.Close();
            Position_Selected(sender, e);
        }
    }

    protected void Position_Selected(object sender, EventArgs e)
    {
        string sqlcheck = "Select Distinct [zhouqi] from PMState where [unit]='" + DD_Unit.SelectedValue + "' And [item]='" + DD_Items.SelectedValue + "' And [position]='" + DD_Position.SelectedValue + "'";
        if(DD_EQ.SelectedIndex!=0)
            sqlcheck = "Select Distinct [zhouqi] from PMState where eq='"+DD_EQ.SelectedValue+"' AND [unit]='" + DD_Unit.SelectedValue + "' And [item]='" + DD_Items.SelectedValue + "' And [position]='" + DD_Position.SelectedValue + "'";
        string ConnectionString = ConfigurationManager.ConnectionStrings["mdbcon"].ConnectionString;
        OleDbConnection conn = new OleDbConnection(ConnectionString);
        OleDbCommand cmd = new OleDbCommand(sqlcheck, conn);
        conn.Open();
        OleDbDataReader rd = cmd.ExecuteReader();
        while (rd.Read())
        {
            TB_Cycle.Text = rd["zhouqi"].ToString();
        }
        rd.Close();
        conn.Close();
    }

    protected bool IsWithDB(int index)
    {
        int[] eqWithOutDB = new int[8] { 1, 2, 7, 8, 9, 10, 11, 12 };
        bool isWithDB = true;
        for (int i = 0; i < 8; i++)
        {
            if (index == eqWithOutDB[i])
            {
                isWithDB = false;
            }
        }
        return isWithDB;
    }

    protected void Add_Item_Click(object sender, EventArgs e)
    {
        ArrayList sqllist = new ArrayList();
        if (TB_Cycle.Text == "")
        {
            ToolTips.Text = "更新的周期不能为空";
        }
        else
        {
            int cycle = Convert.ToInt32(TB_Cycle.Text);
            if (cycle > 0)
            {
                if (DD_Item_Option.SelectedIndex == 0 && DD_Position_Option.SelectedIndex == 0)
                {
                    if (DD_EQ.SelectedIndex == 0)
                    {
                        sqllist.Add("Update PMState Set [zhouqi]='" + cycle + "' where [unit]='" + DD_Unit.SelectedValue + "' AND [item]='" + DD_Items.SelectedValue + "' AND [position]='" + DD_Position.SelectedValue + "'");
                    }
                    else
                    {
                        sqllist.Add("Update PMState Set [zhouqi]='" + cycle + "' where eq='" + DD_EQ.SelectedValue + "' AND [unit]='" + DD_Unit.SelectedValue + "' AND [item]='" + DD_Items.SelectedValue + "' AND [position]='" + DD_Position.SelectedValue + "'");
                    }
                }
                else
                {
                    if (DD_Item_Option.SelectedIndex == 0)
                    {
                        if (TB_New_Position.Text == "")
                        {
                            ToolTips.Text = "新位置不能为空";
                        }
                        else
                        {
                            DateTime last = Convert.ToDateTime("2015/01/01");
                            if (TB_Date.Text != "")
                                last = Convert.ToDateTime(TB_Date.Text);
                            DateTime duedate = last.AddDays(cycle);
                            if (DD_EQ.SelectedIndex == 0)
                            {
                                if (DD_Unit.SelectedValue != "DB")
                                {
                                    for (int i = 0; i < 16; i++)
                                    {
                                        sqllist.Add("Insert into PMState (eq,[item],[unit],[position],[zhouqi],[last],[duedate]) Values ('" + 
                                            DD_EQ.Items[i + 1].Value + "','" + DD_Items.SelectedValue + "','" + DD_Unit.SelectedValue + "','" + TB_New_Position.Text +
                                            "','" + cycle + "',#" + last.ToLocalTime() + "#,#" + duedate.ToLocalTime() + "#)");
                                    }
                                }
                                else
                                {
                                    for (int i = 0; i < 16; i++)
                                    {
                                        if (IsWithDB(i))
                                        {
                                            sqllist.Add("Insert into PMState (eq,[item],[unit],[position],[zhouqi],[last],[duedate]) Values ('" +
                                                DD_EQ.Items[i + 1].Value + "','" + DD_Items.SelectedValue + "','" + DD_Unit.SelectedValue + "','" + TB_New_Position.Text +
                                                "','" + cycle + "',#" + last.ToLocalTime() + "#,#" + duedate.ToLocalTime() + "#)");
                                        }
                                    }
                                }
                            }
                            else
                            {
                                sqllist.Add("Insert into PMState (eq,[item],[unit],[position],[zhouqi],[last],[duedate]) Values ('" +
                                    DD_EQ.SelectedValue + "','" + DD_Items.SelectedValue + "','" + DD_Unit.SelectedValue + "','" + TB_New_Position.Text +
                                    "','" + cycle + "',#" + last.ToLocalTime() + "#,#" + duedate.ToLocalTime() + "#)");
                            }
                        }
                    }
                    else
                    {
                        if ((TB_New_Item.Text == "") || (TB_New_Position.Text == ""))
                        {
                            ToolTips.Text = "新项目或新位置不能为空";
                        }
                        else
                        {
                            DateTime last = Convert.ToDateTime("2015/01/01");
                            if (TB_Date.Text != "")
                                last = Convert.ToDateTime(TB_Date.Text);
                            DateTime duedate = last.AddDays(cycle);
                            if (DD_EQ.SelectedIndex == 0)
                            {
                                if (DD_Unit.SelectedValue != "DB")
                                {
                                    for (int i = 0; i < 16; i++)
                                    {
                                        sqllist.Add( "Insert into PMState (eq,[item],[unit],[position],[zhouqi],[last],[duedate]) Values ('" + DD_EQ.Items[i+1].Value + "','" + TB_New_Item.Text + "','" + DD_Unit.SelectedValue + "','" + TB_New_Position.Text +
                                    "','" + cycle + "',#" + last.ToLocalTime() + "#,#" + duedate.ToLocalTime() + "#)");
                                    }
                                }
                                else
                                {
                                    for (int i = 0; i < 16; i++)
                                    {
                                        if (IsWithDB(i))
                                        {
                                            sqllist.Add( "Insert into PMState (eq,[item],[unit],[position],[zhouqi],[last],[duedate]) Values ('" + DD_EQ.Items[i + 1].Value + "','" + TB_New_Item.Text + "','" + DD_Unit.SelectedValue + "','" + TB_New_Position.Text +
                                         "','" + cycle + "',#" + last.ToLocalTime() + "#,#" + duedate.ToLocalTime() + "#)");
                                        }
                                    }
                                }
                            }
                            else
                            {
                                sqllist.Add("Insert into PMState (eq,[item],[unit],[position],[zhouqi],[last],[duedate]) Values ('" +
                                    DD_EQ.SelectedValue + "','" + TB_New_Item.Text + "','" + DD_Unit.SelectedValue + "','" + TB_New_Position.Text +
                                    "','" + cycle + "',#" + last.ToLocalTime() + "#,#" + duedate.ToLocalTime() + "#)");
                            }
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
                        string myscript = @"alert('发布成功！');window.location.href='NewPMItem.aspx';";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
                    }
                    catch
                    {
                        tx.Rollback();
                        conn.Close();
                        ToolTips.Text = "发布失败！";
                    }
                }
            }
            else
            {
                ToolTips.Text = "更新的周期必须大于0";
            }
        }
    }
    protected void Add_Cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("PM.aspx");
    }
}
