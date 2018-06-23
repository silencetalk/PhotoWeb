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

public partial class WebPages_AOI : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["isedit"] = false;
            ViewState["sqlid"] = "";
            CheckDate();
            ViewState["sql"] = "Select * from AOIMonitor  where shortdate= '" + ViewState["date"].ToString() + "' Order by banci,lindex";
            LoadData();
        }
        DrawChart();
        this.GridView1.Attributes.Add("style", "table-layout:fixed;word-wrap:break-word;");
        this.TextBoxSearch.Attributes.Add("onkeydown", "if(event.keyCode==13) {document.all." + this.Search.ClientID + ".focus();document.all." + this.Search.ClientID + ".click();}");
        this.Delete.Attributes.Add("onclick", "javascript:if(confirm('确定要删除吗?')){}else{return false;}");
    }

    //数据处理函数

    protected void TongjiChanged(object sender, EventArgs e)
    {
        DrawChart();
    }

    protected void DrawChart()
    {
        string sql = "Select top " + (this.DropDownListTongjiShijian.SelectedIndex + 1) * 14 + " * from AOIMonitor where jitai='" + this.DDTongjiJitai.SelectedValue + "' Order by ID Desc";
        int[] dfcount = new int[11];
        string[] defect = { "Coating PT", "EXP PT", "PR Peeling", "PR Remain", "PR Open", "Dev PT", "Dev Buble", "水残", "EQ Down", "RW厘清", "Other" };
        string[] sqlname = { "total", "coating", "exp", "peeling", "remain", "open", "devpt", "buble", "water", "down", "rw", "other" };
        string leixing = sqlname[this.DropDownListLeiXing.SelectedIndex];
        this.Chart1.ChartAreas[0].AxisY.Title = this.DropDownListLeiXing.SelectedValue;
        string ConnectionString = ConfigurationManager.ConnectionStrings["mdbcon"].ConnectionString;
        OleDbConnection conn = new OleDbConnection(ConnectionString);
        OleDbCommand cmd = new OleDbCommand(sql, conn);
        conn.Open();
        OleDbDataReader sdr = cmd.ExecuteReader();
        Chart1.Series[0].Points.DataBind(sdr, "fabiaodate", leixing, "");
        sdr.Close();
        sdr = cmd.ExecuteReader();
        while (sdr.Read())
        {
            dfcount[0] += Convert.ToInt32(sdr["coating"].ToString());
            dfcount[1] += Convert.ToInt32(sdr["exp"].ToString());
            dfcount[2] += Convert.ToInt32(sdr["peeling"].ToString());
            dfcount[3] += Convert.ToInt32(sdr["remain"].ToString());
            dfcount[4] += Convert.ToInt32(sdr["open"].ToString());
            dfcount[5] += Convert.ToInt32(sdr["devpt"].ToString());
            dfcount[6] += Convert.ToInt32(sdr["buble"].ToString());
            dfcount[7] += Convert.ToInt32(sdr["water"].ToString());
            dfcount[8] += Convert.ToInt32(sdr["down"].ToString());
            dfcount[9] += Convert.ToInt32(sdr["rw"].ToString());
            dfcount[10] += Convert.ToInt32(sdr["other"].ToString());
        }
        for (int i = 0; i < 11; i++)
        {
            for (int j = i; j < 11; j++)
            {
                if (dfcount[i] < dfcount[j])
                {
                    int counttemp = dfcount[i];
                    string nametemp = defect[i];
                    dfcount[i] = dfcount[j];
                    defect[i] = defect[j];
                    dfcount[j] = counttemp;
                    defect[j] = nametemp;
                }
            }
        }
        int length = 5;
        for (int i = 0; i < 10; i++)
        {
            if (dfcount[i] == 0)
            {
                length = i+1;
                break;
            }
        }
        if (length > 5)
        {
            int[] displaycount = new int[5];
            string[] displaydefect = new string[5];
            for (int i = 0; i < 4; i++)
            {
                displaycount[i] = dfcount[i];
                displaydefect[i] = defect[i];
            }
            for (int i = 4; i < 10; i++)
            {
                displaycount[4] += dfcount[i];
            }
            displaydefect[4] = "其他";
            Chart1.Series[1].Points.DataBindXY(displaydefect, displaycount);
        }
        else
        {
            int[] displaycount = new int[length];
            string[] displaydefect = new string[length];
            for (int i = 0; i <(length-1); i++)
            {
                displaycount[i] = dfcount[i];
                displaydefect[i] = defect[i];
                Chart1.Series[1].Points.DataBindXY(displaydefect, displaycount);
            }
        }
        sdr.Close();
        conn.Close();
    }

    protected void CheckDate()
    {
        string sql = "Select top 1 * from AOIMonitor Order by ID Desc";
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
            string myscript = @"alert('加载数据出错！');window.location.href='AOI.aspx';";
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


    //工具按钮函数
    protected void PreDate_Click(object sender, EventArgs e)
    {
        DateTime dt = Convert.ToDateTime(this.LabelDate.Text.ToString());
        ViewState["date"] = dt.AddDays(-1).ToShortDateString();
        this.LabelDate.Text = ViewState["date"].ToString();
        ViewState["sql"] = "Select * from AOIMonitor where shortdate= '" + ViewState["date"] + "' Order by banci,lindex";
        LoadData();
    }
    protected void SetDate_Click(object sender, EventArgs e)
    {
        CheckDate();
        ViewState["sql"] = "Select * from AOIMonitor where shortdate= '" + ViewState["date"].ToString() + "' Order by banci,lindex";
        LoadData();
    }
    protected void NextDate_Click(object sender, EventArgs e)
    {
        DateTime dt = Convert.ToDateTime(this.LabelDate.Text.ToString());
        ViewState["date"] = dt.AddDays(1).ToShortDateString();
        this.LabelDate.Text = ViewState["date"].ToString();
        ViewState["sql"] = "Select * from AOIMonitor where shortdate= '" + ViewState["date"].ToString() + "' Order by banci,lindex";
        LoadData();
    }
    protected void Add_Click(object sender, EventArgs e)
    {
        this.ToolTips.Text = "";
        this.TextBoxBuble.Text = "0";
        this.TextBoxCoatingPT.Text = "0";
        this.TextBoxDEVPT.Text = "0";
        this.TextBoxDown.Text = "0";
        this.TextBoxExp.Text = "0";
        this.TextBoxInputDate.Text = "0";
        this.TextBoxOpen.Text = "0";
        this.TextBoxOther.Text = "0";
        this.TextBoxPeeling.Text = "0";
        this.TextBoxRemain.Text = "0";
        this.TextBoxWater.Text = "0";
        this.TextBoxRework.Text = "0";
        this.DropDownListJitai.SelectedIndex = 0;
        int hour = DateTime.Now.Hour;
        if (hour < 12)
        {
            this.DropDownListInputBanci.SelectedIndex = 0;
            this.TextBoxInputDate.Text = DateTime.Now.AddDays(-1).ToShortDateString();
        }
        else
        {
            this.DropDownListInputBanci.SelectedIndex = 1;
            this.TextBoxInputDate.Text = DateTime.Now.ToShortDateString();
        }
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
            string sqldelete = "Delete from AOIMonitor where ID in" + sqlid;
            string sqlcheck = "Select * from AOIMonitor where ID in" + sqlid;
            string ConnectionString = ConfigurationManager.ConnectionStrings["mdbcon"].ConnectionString;
            OleDbConnection conn = new OleDbConnection(ConnectionString);
            OleDbCommand cmd = new OleDbCommand(sqlcheck, conn);
            conn.Open();
            OleDbDataReader rd = cmd.ExecuteReader();
            bool isdelete = false;
            string filepath = "";
            while (rd.Read())
            {
                if (((string)rd["eng"] == (string)Session["name"]) || ((string)Session["name"] == "程邵磊"))
                {
                    isdelete = true;
                    filepath = rd["fujian"].ToString();
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
                    if (File.Exists(Server.MapPath(filepath)))
                    {
                        string filesql = "Delete from UploadFiles where path='" + filepath + "'";
                        DbManager.ExecuteNonQuery(filesql);
                        File.Delete(Server.MapPath(filepath));
                    }
                    int i = DbManager.ExecuteNonQuery(sqldelete);
                    string myscript = @"alert('删除成功！');window.location.href='AOI.aspx';";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
                }
                catch
                {
                    string myscript = @"alert('删除失败，请与管理员联系！');window.location.href='AOI.aspx';";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
                }
            }
            else
            {
                string myscript = @"alert('无权删除他人信息！');window.location.href='AOI.aspx';";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
            }
        }
        else
        {
            string myscript = @"alert('请选择删除项！');window.location.href='AOI.aspx';";
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
            string sqlcheck = "Select * from AOIMonitor where ID in" + sqlid;
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
                this.DropDownListJitai.SelectedValue = rd["jitai"].ToString().Trim();
                this.DropDownListInputBanci.SelectedValue = rd["banci"].ToString().Trim();
                this.TextBoxWater.Text = rd["water"].ToString();
                this.TextBoxRemain.Text = rd["remain"].ToString();
                this.TextBoxPeeling.Text = rd["peeling"].ToString();
                this.TextBoxOther.Text = rd["other"].ToString();
                this.TextBoxOpen.Text = rd["open"].ToString();
                this.TextBoxInputDate.Text = rd["shortdate"].ToString();
                this.TextBoxExp.Text = rd["exp"].ToString();
                this.TextBoxDown.Text = rd["down"].ToString();
                this.TextBoxDEVPT.Text = rd["devpt"].ToString();
                this.TextBoxCoatingPT.Text = rd["coating"].ToString();
                this.TextBoxBuble.Text = rd["buble"].ToString();
                this.TextBoxRework.Text = rd["rw"].ToString();
                ViewState["sqlid"] = sqlid;
                ViewState["isedit"] = true;
                conn.Close();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
            }
            else
            {
                conn.Close();
                string myscript = @"alert('无权编辑他人信息！');window.location.href='AOI.aspx';";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
            }
        }
        else
        {
            string myscript = @"alert('请选择编辑项！');window.location.href='AOI.aspx';";
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
        ViewState["sql"] = "Select * from AOIMonitor where jitai like '" + chaxunstr + "'or banci like '" + chaxunstr  + "'or eng like '" + chaxunstr + "'or shortdate like '" + chaxunstr + "'or [coating] like '" + chaxunstr
            + "'or [exp] like '" + chaxunstr + "'or [peeling] like '" + chaxunstr + "'or [remain] like '" + chaxunstr + "'or [open] like '" + chaxunstr + "'or [buble] like '" + chaxunstr
            + "'or [devpt] like '" + chaxunstr + "'or [water] like '" + chaxunstr + "'or [down] like '" + chaxunstr + "'or [rw] like '" + chaxunstr + "'or [other] like '" + chaxunstr + "'or [total] like '" + chaxunstr + "' Order by banci,lindex";
        LoadData();
    }

    //添加内容函数
    protected void Add_OK_Click(object sender, EventArgs e)
    {
        if (this.DropDownListJitai.SelectedIndex == 0)
        {
            this.ToolTips.Text = "<span style='font-family:fontawesome !important; color:Red;'>&#xf071;</span>请选择机台！";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        else if (this.TextBoxInputDate.Text == "")
        {
            this.ToolTips.Text = "<span style='font-family:fontawesome !important; color:Red;'>&#xf071;</span>日期不能为空！";
        }
        else
        {
            try
            {
                DateTime dt = Convert.ToDateTime(this.TextBoxInputDate.Text.ToString());
            }
            catch
            {
            }
            string jitai = this.DropDownListJitai.SelectedValue;
            int lindex = this.DropDownListJitai.SelectedIndex + 1;
            string banci = this.DropDownListInputBanci.SelectedValue;
            string eng = (string)Session["name"];
            string shortdate = this.TextBoxInputDate.Text.ToString();

            int buble=Convert.ToInt32(this.TextBoxBuble.Text.ToString());
            int coating=Convert.ToInt32(this.TextBoxCoatingPT.Text.ToString());
            int devpt=Convert.ToInt32(this.TextBoxDEVPT.Text .ToString());
            int down = Convert.ToInt32(this.TextBoxDown.Text.ToString());
            int exp = Convert.ToInt32(this.TextBoxExp.Text.ToString());
            int open = Convert.ToInt32(this.TextBoxOpen.Text.ToString());
            int other = Convert.ToInt32(this.TextBoxOther.Text.ToString());
            int peeling = Convert.ToInt32(this.TextBoxPeeling.Text.ToString());
            int rw = Convert.ToInt32(this.TextBoxRework.Text.ToString());
            int remain = Convert.ToInt32(this.TextBoxRemain.Text.ToString());
            int water = Convert.ToInt32(this.TextBoxWater.Text.ToString());
            int total = buble + coating + devpt + down + exp + open + other + peeling + rw + remain + water;
            string filepath = "";
            string filesql="";
            string sql = "Insert into AOIMonitor (jitai,banci,eng,shortdate,[lindex],[rw],[buble],[coating],[devpt],[down],[exp],[open],[other],[peeling],[remain],[water],[total],[fabiaodate]) values ('" + jitai + "','" + banci + "','" + eng + "','" + shortdate + "','" + lindex + "','" + rw + "','" + buble + "','" + coating + "','" + devpt
                + "','" + down + "','" + exp + "','" + open + "','" + other + "','" + peeling + "','" + remain + "','" + water + "','" + total + "',#" + DateTime.Now.ToLocalTime()+ "#)";
            if ((bool)ViewState["isedit"])
            {
                sql = "Update AOIMonitor Set jitai='" + jitai + "',banci='" + banci + "',eng='" + eng + "',[lindex]='" + lindex + "',shortdate='" + shortdate + "',[rw]='" + rw + "',[buble]='" + buble + "',[coating]='" + coating + "',[devpt]='" + devpt + "',[down]='" + down
                    + "',[exp]='" + exp + "',[open]='" + open + "',[other]='" + other + "',[peeling]='" + peeling + "',[remain]='" + remain + "',[water]='" + water + "',[total]='" + total + "' where ID in " + ViewState["sqlid"].ToString();
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
                    filepath = "~/UploadFiles/RWLot/";
                    string fileext = System.IO.Path.GetExtension(this.FileUpload1.FileName).ToLower();
                    string filename = DateTime.Now.ToString("yyyyMMdd") + banci + "RWLot报告" + fileext;
                    filepath = filepath + filename;
                    filename = "<br/>" + filename + "<p></p>";
                    filesql = "Insert into UploadFiles (eng,shortdate,title,fenlei,filename,path)  values ('" + Session["name"].ToString() + "','" + shortdate + "','RWLot','设备报告','" + filename + "','" + filepath + "')";
                    sql = "Insert into AOIMonitor (jitai,banci,eng,shortdate,fujian,[lindex],[rw],[buble],[coating],[devpt],[down],[exp],[open],[other],[peeling],[remain],[water],[total],[fabiaodate]) values ('" + jitai + "','" + banci + "','" + eng + "','" + shortdate + "','" + filepath + "','" + lindex + "','" + rw + "','" + buble + "','" + coating + "','" + devpt
                       + "','" + down + "','" + exp + "','" + open + "','" + other + "','" + peeling + "','" + remain + "','" + water + "','" + total + "',#" + DateTime.Now.ToLocalTime() + "#)";
                    if ((bool)ViewState["isedit"])
                    {
                        string sqlcheck = "Select fujian from AOIMonitor where ID in" + ViewState["sqlid"].ToString();
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
                        sql = "Update AOIMonitor Set jitai='" + jitai + "',banci='" + banci + "',eng='" + eng + "',fujian='" + filepath + "',[lindex]='" + lindex + "',shortdate='" + shortdate + "',[rw]='" + rw + "',[buble]='" + buble + "',[coating]='" + coating + "',[devpt]='" + devpt + "',[down]='" + down
                              + "',[exp]='" + exp + "',[open]='" + open + "',[other]='" + other + "',[peeling]='" + peeling + "',[remain]='" + remain + "',[water]='" + water + "',[total]='" + total + "' where ID in " + ViewState["sqlid"].ToString();
                    }
                }
            }
            try
            {
                if (this.FileUpload1.HasFile)
                {
                    filepath = Server.MapPath(filepath);
                    FileUpload1.PostedFile.SaveAs(filepath);
                    DbManager.ExecuteNonQuery(filesql);
                }
                DbManager.ExecuteNonQuery(sql);
                string myscript = @"alert('发布成功！');window.location.href='AOI.aspx';";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
            }
            catch
            {
                this.ToolTips.Text = "<span style='font-family:fontawesome !important; color:Red;'>&#xf071;</span>发布失败，请与管理员联系！";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
            }
        }
    }

    protected void Add_Cancel_Click(object sender, EventArgs e)
    {
        ViewState["isedit"] = false;
    }

    //跳转页面函数
    protected void Mura_Click(object sender, EventArgs e)
    {
        Response.Redirect("Mura.aspx");
    }
    protected void Thickness_Click(object sender, EventArgs e)
    {
        Response.Redirect("PRThickness.aspx");
    }
}
