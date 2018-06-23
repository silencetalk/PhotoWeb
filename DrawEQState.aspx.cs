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
using System.IO;//文件存取
using System.Drawing;//画图基本功能
using System.Drawing.Drawing2D;//二维画图
using System.Drawing.Imaging;//高级功能

public partial class DrawEQState : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string eq = Request.QueryString["eq"];
        Bitmap img = new Bitmap(864, 400);//创建Bitmap对象
        MemoryStream stream = draw(eq);
        img.Save(stream, ImageFormat.Jpeg);          //保存绘制的图片
        Response.Clear();
        Response.ContentType = "image/jpeg";
        Response.BinaryWrite(stream.ToArray());
    }
    public MemoryStream draw(string eq)
    {
        Bitmap img = new Bitmap(864, 200);//创建Bitmap对象
        Graphics g = Graphics.FromImage(img);//创建Graphics对象
        Brush runbrush = new SolidBrush(Color.Green);
        Brush idlebrush = new SolidBrush(Color.Pink);
        Brush downbrush = new SolidBrush(Color.Red);
        Brush mtnbrush = new SolidBrush(Color.Yellow);
        Brush etcbrush = new SolidBrush(Color.SkyBlue);
        Brush etimebrush = new SolidBrush(Color.Blue);
        Brush bgbrush = new SolidBrush(Color.White);
        Rectangle bgrec = new Rectangle(0, 0, 864, 200);
        Rectangle contentrec = new Rectangle(0, 0, 864, 200);
        g.FillRectangle(bgbrush, bgrec);
        g.FillRectangle(runbrush, contentrec);

        string sql = "Select * from EQState where EQPID ='"+eq+"' And StartTime >=#2014/9/17 06:00:00# Order by StartTime";
        string ConnectionString = ConfigurationManager.ConnectionStrings["mdbcon"].ConnectionString;
        OleDbConnection conn = new OleDbConnection(ConnectionString);
        OleDbCommand cmd = new OleDbCommand(sql, conn);
        conn.Open();
        OleDbDataReader rd = cmd.ExecuteReader();
        DateTime start = new DateTime();
        DateTime end = new DateTime();
        TimeSpan ts=new TimeSpan();
        string eqstate = "";
        float second=0;
        float span=0;
        while (rd.Read())
        {
            start = Convert.ToDateTime(rd["StartTime"].ToString());
            end = Convert.ToDateTime(rd["EndTime"].ToString());
            ts = start.Subtract(end).Duration();
            span = (ts.Hours*3600 + ts.Minutes*60 + ts.Seconds)/100;
            eqstate = rd["EQPRunState"].ToString();
            if (eqstate == "DOWN")
            {
                g.FillRectangle(downbrush, second, 0, span, 200);
            }
            if (eqstate == "ETC")
            {
                g.FillRectangle(etcbrush, second, 0, span, 200);
            }
            if (eqstate == "ETIME")
            {
                g.FillRectangle(etimebrush, second,0, span, 200);
            }
            if (eqstate == "MAINT")
            {
                g.FillRectangle(mtnbrush, second, 0, span, 200);
            }
            if (eqstate == "IDLE")
            {
                g.FillRectangle(idlebrush, second, 0, span, 200);
            }
            second += span;
        }
        MemoryStream stream = new MemoryStream();   //保存绘制的图片
        img.Save(stream, ImageFormat.Jpeg);          //保存绘制的图片
        return stream;
    }
}
