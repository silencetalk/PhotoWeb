using System;
using System.IO;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebPages_AOI_Monitor : System.Web.UI.Page
{
    protected void CheckDate()
    {
        string sql = "Select top 1 * from AOI Order by ID Desc";
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
            string myscript = @"alert('加载数据出错！');window.location.href='Defect.aspx';";
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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["isedit"] = false;
            ViewState["sqlid"] = "";
            CheckDate();
            ViewState["sql"] = "Select * from AOI where shortdate='"+ViewState["date"].ToString()+"' Order by banci,lindex";
            LoadData();
        }
        this.GridView1.Attributes.Add("style", "table-layout:fixed;word-wrap:break-word;");
        this.TextBoxSearch.Attributes.Add("onkeydown", "if(event.keyCode==13) {document.all." + this.Search.ClientID + ".focus();document.all." + this.Search.ClientID + ".click();}");
        this.Delete.Attributes.Add("onclick", "javascript:if(confirm('确定要删除吗?')){}else{return false;}");
    }

    protected void Add_Click(object sender, EventArgs e)
    {
		DD_Jitai.SelectedIndex=0;
		TB_Date.Text=DateTime.Now.ToShortDateString();
        int hour = DateTime.Now.Hour;
        if (hour < 12)
        {
            DD_Banci.SelectedIndex = 0;
            TB_Date.Text = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd");
        }
        else
        {
            DD_Banci.SelectedIndex=1;
            TB_Date.Text = DateTime.Now.ToString("yyyy/MM/dd");
        }
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
            string sqlcheck = "Select * from AOI where ID in" + sqlid;
            string ConnectionString = ConfigurationManager.ConnectionStrings["mdbcon"].ConnectionString;
            OleDbConnection conn = new OleDbConnection(ConnectionString);
            OleDbCommand cmd = new OleDbCommand(sqlcheck, conn);
            conn.Open();
            OleDbDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
				DD_Jitai.SelectedValue=rd["jitai"].ToString();
                DD_Banci.SelectedValue = rd["banci"].ToString();
				TB_Date.Text=rd["shortdate"].ToString();
                TB_LotID.Text = rd["lotid"].ToString();
                TB_Yichang.Text = Input.Outputadd(rd["yichang"].ToString());
                TB_Chuli.Text = Input.Outputadd(rd["chuli"].ToString());
            }
            ViewState["sqlid"] = sqlid;
            ViewState["isedit"] = true;
            conn.Close();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        else
        {
            string myscript = @"alert('请选择编辑项！');window.location.href='Defect.aspx';";
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
            string sqldelete = "Delete from AOI where ID in" + sqlid;
            string sqlcheck = "Select mapimg,defectimg from AOI where ID in" + sqlid;
            string ConnectionString = ConfigurationManager.ConnectionStrings["mdbcon"].ConnectionString;
            OleDbConnection conn = new OleDbConnection(ConnectionString);
            OleDbCommand cmd = new OleDbCommand(sqlcheck, conn);
            conn.Open();
            OleDbDataReader rd = cmd.ExecuteReader();
            string mapimg="";
			string defectimg="";
            while (rd.Read())
            {
				mapimg=rd["mapimg"].ToString();
				defectimg=rd["defectimg"].ToString();
            }
            conn.Close();
            try
            {
				if (File.Exists(mapimg))
                {
                    File.Delete(mapimg);
                    string filesql = "Delete from UploadFiles where path='" + mapimg + "'";
                    DbManager.ExecuteNonQuery(filesql);
                }
				if (File.Exists(defectimg))
                {
                    File.Delete(defectimg);
                    string filesql = "Delete from UploadFiles where path='" + defectimg + "'";
                    DbManager.ExecuteNonQuery(filesql);
                }
                int i = DbManager.ExecuteNonQuery(sqldelete);
                string myscript = @"alert('删除成功！');window.location.href='Defect.aspx';";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
            }
            catch
            {
                string myscript = @"alert('删除失败，请与管理员联系！');window.location.href='Defect.aspx';";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
            }
        }
        else
        {
            string myscript = @"alert('请选择删除项！');window.location.href='Defect.aspx';";
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
        ViewState["sql"] = "Select * from AOI where shortdate like '" + chaxunstr + "'or lotid like '" + chaxunstr + "'or yichang like '" + chaxunstr + "'or chuli like '" + chaxunstr + "'or jitai like '" + chaxunstr + "'or eng like '" + chaxunstr + "' order by ID Desc";
        LoadData();
    }

    protected void PreDate_Click(object sender, EventArgs e)
    {
        DateTime dt = Convert.ToDateTime(this.LabelDate.Text.ToString());
        ViewState["date"] = dt.AddDays(-1).ToString("yyyy/MM/dd");
        this.LabelDate.Text = ViewState["date"].ToString();
        ViewState["sql"] = "Select * from AOI where shortdate='" + ViewState["date"].ToString() + "' Order by banci,lindex";
        LoadData();
    }
    protected void SetDate_Click(object sender, EventArgs e)
    {
        CheckDate();
        ViewState["sql"] = "Select * from AOI where shortdate='" + ViewState["date"].ToString() + "' Order by banci,lindex";
        LoadData();
    }
    protected void NextDate_Click(object sender, EventArgs e)
    {
        DateTime dt = Convert.ToDateTime(this.LabelDate.Text.ToString());
        ViewState["date"] = dt.AddDays(1).ToString("yyyy/MM/dd");
        this.LabelDate.Text = ViewState["date"].ToString();
        ViewState["sql"] = "Select * from AOI where shortdate='" + ViewState["date"].ToString() + "' Order by banci,lindex";
        LoadData();
    }

    protected void Add_OK_Click(object sender, EventArgs e)
    {
        if ( DD_Jitai.SelectedIndex == 0)
        {
            this.ToolTips.Text = "<span style='font-family:fontawesome !important; color:Red;'>&#xf071;</span>请选择机台！";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        else if ( TB_LotID.Text == "") 
        {
            this.ToolTips.Text = "<span style='font-family:fontawesome !important; color:Red;'>&#xf071;</span>LotID不能为空！";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        else if( TB_Yichang.Text == "")
        {
            this.ToolTips.Text = "<span style='font-family:fontawesome !important; color:Red;'>&#xf071;</span>异常信息不能为空！";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        else if( TB_Chuli.Text=="")
        {
             this.ToolTips.Text = "<span style='font-family:fontawesome !important; color:Red;'>&#xf071;</span>处理方法不能为空！";
             Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
        }
        else
        {
            bool isadd = true;
            string mapimg = "";
            string defectimg = "";
            string shortdate = TB_Date.Text;
            string banci = DD_Banci.SelectedValue;
            string jitai = DD_Jitai.SelectedValue;
            string lotid = TB_LotID.Text;
            string yichang = Input.Inputadd(TB_Yichang.Text);
            string chuli = Input.Inputadd(TB_Chuli.Text);
            string eng = Session["name"].ToString();
            if (TB_ImgUrl.Text != "")
            {
                string[] defectimgs = TB_ImgUrl.Text.Split(new string[]{".jpg"}, System.StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < defectimgs.Length; i++)
                {
                    defectimgs[i] = "[img]" + defectimgs[i] + ".jpg[/img]";
                    defectimg += defectimgs[i];
                }
            }
            string sql = "Insert into AOI (shortdate,banci,jitai,lindex,lotid,yichang,chuli,eng,defectimg) values ('"+
                shortdate + "','" + banci + "','" + jitai + "','" + DD_Jitai.SelectedIndex + "','" + lotid + "','" + yichang + "','" + chuli + "','" + eng + "','" + defectimg + "')";
            if ((bool)ViewState["isedit"])
            {
                sql = "Update AOI Set shortdate='"+shortdate+"',banci='"+ banci +"',jitai='"+jitai+"',lindex='"+DD_Jitai.SelectedIndex+"',lotid='"+
                    lotid + "',yichang='" + yichang + "',chuli='" + chuli + "',eng='" + eng + "',defectimg='" + defectimg + "' where ID in" + ViewState["sqlid"].ToString();
            }
            if (this.FileUpload1.HasFile)
            {
                string path = "~/UploadFiles/AOI/Map/";
                string filename = lotid + DateTime.Now.ToString("yyyyMMddHHmmss") + FileUpload1.FileName;
                mapimg = path + filename;
                if(System.IO.Path.GetExtension(this.FileUpload1.FileName).ToLower()==".jpg")
                {
                    sql = "Insert into AOI (shortdate,banci,jitai,lindex,lotid,yichang,chuli,eng,defectimg,mapimg) values ('" +
                 shortdate + "','" + banci + "','" + jitai + "','" + DD_Jitai.SelectedIndex + "','" + lotid + "','" + yichang + "','" + chuli + "','" + eng + "','" + defectimg + "','" + mapimg + "')";
                    if ((bool)ViewState["isedit"])
                   {
                    string sqlcheck = "Select mapimg from AOI where ID in" + ViewState["sqlid"].ToString();
                    string ConnectionString = ConfigurationManager.ConnectionStrings["mdbcon"].ConnectionString;
                    OleDbConnection conn = new OleDbConnection(ConnectionString);
                    OleDbCommand cmd = new OleDbCommand(sqlcheck, conn);
                    conn.Open();
                    OleDbDataReader rd = cmd.ExecuteReader();
                    string uploadfile = "";
                    while (rd.Read())
                    {
                        uploadfile = rd["mapimg"].ToString();
                    }
                    rd.Close();
                    conn.Close();
                    uploadfile = Server.MapPath(uploadfile);
                    if (File.Exists(uploadfile))
                    {
                        File.Delete(uploadfile);
                    }
                    sql = "Update AOI Set shortdate='" + shortdate + "',banci='" + banci + "',jitai='" + jitai + "',lindex='" + DD_Jitai.SelectedIndex + "',lotid='" +
                    lotid + "',yichang='" + yichang + "',chuli='" + chuli + "',defectimg='" + defectimg + "',mapimg='" + mapimg + "',eng='" + eng + "' where ID in" + ViewState["sqlid"].ToString();
                   }
                }
                else
                {
                    isadd = false;
                     this.ToolTips.Text = "<span style='font-family:fontawesome !important; color:Red;'>&#xf071;</span>图片格式应为.jpg！";
                     Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
                }
            }
            //if (this.FileUpload2.HasFile)
            //{
            //    string path = "~/UploadFiles/AOI/Defect/";
            //    string filename = lotid + DateTime.Now.ToString("yyyyMMddHHmmss") + FileUpload2.FileName;
            //    defectimg= path + filename;
            //    if (System.IO.Path.GetExtension(this.FileUpload2.FileName).ToLower() == ".jpg")
            //    {
            //        sql = "Insert into AOI (shortdate,banci,jitai,lindex,lotid,yichang,chuli,eng,defectimg) values ('" +
            //            shortdate + "','" + banci + "','" + jitai + "','" + DD_Jitai.SelectedIndex + "','" + lotid + "','" + yichang + "','" + chuli + "','" + eng + "','" + defectimg + "')";
            //        if ((bool)ViewState["isedit"])
            //        {
            //            string sqlcheck = "Select defectimg from AOI where ID in" + ViewState["sqlid"].ToString();
            //            string ConnectionString = ConfigurationManager.ConnectionStrings["mdbcon"].ConnectionString;
            //            OleDbConnection conn = new OleDbConnection(ConnectionString);
            //            OleDbCommand cmd = new OleDbCommand(sqlcheck, conn);
            //            conn.Open();
            //            OleDbDataReader rd = cmd.ExecuteReader();
            //            string uploadfile = "";
            //            while (rd.Read())
            //            {
            //                uploadfile = rd["defectimg"].ToString();
            //            }
            //            rd.Close();
            //            conn.Close();
            //            uploadfile = Server.MapPath(uploadfile);
            //            if (File.Exists(uploadfile))
            //            {
            //                File.Delete(uploadfile);
            //            }
            //            sql = "Update AOI Set shortdate='" + shortdate + "',banci='" + banci + "',jitai='" + jitai + "',lindex='" + DD_Jitai.SelectedIndex + "',lotid='" +
            //                lotid + "',yichang='" + yichang + "',chuli='" + chuli + "',defectimg='" + defectimg + "',eng='" + eng + "' where ID in" + ViewState["sqlid"].ToString();
            //        }
            //    }
            //    else
            //    {
            //        isadd = false;
            //        this.ToolTips.Text = "<span style='font-family:fontawesome !important; color:Red;'>&#xf071;</span>图片格式应为.jpg！";
            //        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script> ShowDialog('Add_Dialog');</script>");
            //    }
            //}
            //if (this.FileUpload1.HasFile && this.FileUpload2.HasFile)
            //{
            //    sql = "Insert into AOI (shortdate,banci,jitai,lindex,lotid,yichang,chuli,eng,mapimg,defectimg) values ('" +
            //            shortdate + "','" + banci + "','" + jitai + "','" + DD_Jitai.SelectedIndex + "','" + lotid + "','" + yichang + "','" + chuli + "','" + eng + "','" + mapimg + "','" + defectimg + "')";
            //    if ((bool)ViewState["isedit"])
            //    {
            //        sql = "Update AOI Set shortdate='" + shortdate + "',banci='" + banci + "',jitai='" + jitai + "',lindex='" + DD_Jitai.SelectedIndex + "',lotid='" +
            //            lotid + "',yichang='" + yichang + "',chuli='" + chuli + "',mapimg='" + mapimg + "',defectimg='" + defectimg + "',eng='" + eng + "' where ID in" + ViewState["sqlid"].ToString();
            //    }
            //}
            if (isadd)
            {
                try
                {
                    DbManager.ExecuteNonQuery(sql);
                    if (this.FileUpload1.HasFile)
                    {
                        mapimg = Server.MapPath(mapimg);
                        this.FileUpload1.PostedFile.SaveAs(mapimg);
                    }
                    //if (this.FileUpload2.HasFile)
                    //{
                    //    defectimg = Server.MapPath(defectimg);
                    //    this.FileUpload2.PostedFile.SaveAs(defectimg);
                    //}
                    string myscript = @"alert('发布成功！');window.location.href='Defect.aspx';";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
                }
                catch
                {
                    string myscript = @"alert('发布失败，请与管理员联系！');window.location.href='Defect.aspx';";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myscript", myscript, true);
                }
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