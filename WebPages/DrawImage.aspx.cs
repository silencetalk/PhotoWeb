using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;//文件存取
using System.Drawing;//画图基本功能
using System.Drawing.Drawing2D;//二维画图
using System.Drawing.Imaging;//高级功能

public partial class WebPages_DrawImage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string sql= Request.QueryString["sql"];
        Bitmap img = new Bitmap(2600,2500);//创建Bitmap对象
        MemoryStream stream = draw(sql);
        img.Save(stream, ImageFormat.Jpeg);          //保存绘制的图片
        Response.Clear();
        Response.ContentType = "image/jpeg";
        Response.BinaryWrite(stream.ToArray());
    }


     
    public MemoryStream draw(string sql)
    {
        Bitmap img = new Bitmap(2600, 2500);//创建Bitmap对象
        Graphics g = Graphics.FromImage(img);//创建Graphics对象

        Pen Bp = new Pen(Color.Black,5); //定义黑色画笔
        Pen AxisPoint = new Pen(Color.Black, 5);//标尺画笔
        Pen Rp = new Pen(Color.Red,4);//红色画笔
        Pen Sp = new Pen(Color.Blue,5);//蓝色坐标系画笔

        Color[] colors = new Color []{ Color.Red,Color.Orange, Color.Maroon, Color.Green , Color.MediumBlue, Color.Olive, Color.Gold ,Color.Black,Color.Salmon,Color.Aquamarine,Color.Violet,Color.Yellow,Color.DarkRed,Color.Peru,Color.MistyRose,Color.MediumSlateBlue };

        Font Arrowfont = new Font("Arial", 50);//箭头字体
        Font Axisfont = new Font("Arial", 40);//坐标值字体
        Font Biaoshifont = new Font("Microsoft Yahei", 35);//坐标值字体
        
        //画Glass背景
        Color glasscolor = Color.FromArgb(208, 206, 206);
        Brush glassbrush = new SolidBrush(glasscolor);
        Brush backgroundbrush = new SolidBrush(Color.White);
        Rectangle backgroundrec = new Rectangle(0, 0, 2600, 2500);
        Rectangle glassrec = new Rectangle(100, 0, 2600, 2200);
        g.FillRectangle(backgroundbrush, backgroundrec);
        g.FillRectangle(glassbrush, glassrec);
        g.DrawLine(Bp, 2500, 0, 2600, 100);

        ////黑色过度型笔刷
        LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, img.Width, img.Height), Color.Black, Color.Black, 1.2F, true);
        ////蓝色过度型笔刷
        LinearGradientBrush Bluebrush = new LinearGradientBrush(new Rectangle(0, 0, img.Width, img.Height), Color.Blue, Color.Blue, 1.2F, true);

        AdjustableArrowCap aac;  //定义箭头帽
        aac = new System.Drawing.Drawing2D.AdjustableArrowCap(20, 20);
        Sp.CustomEndCap = aac;  //开始端箭头帽
        g.DrawLine(Sp, 1350, 0, 1350, 2300);//纵坐标
        g.DrawLine(Sp, 2600, 1100, 0, 1100);//横坐标
        g.DrawString("X", Arrowfont, brush, 20, 1150);
        g.DrawString("Y", Arrowfont, brush, 1400, 2220);

        //画标尺
        int x = 1200;
        int xpoint = 150;
        int xaxisstr = 100;
        for (int i = 0; i < 6; i++)
        {
            g.DrawLine(AxisPoint, xpoint, 1070, xpoint, 1100);
            string xaxis = x.ToString();
            g.DrawString(xaxis, Axisfont, brush, xaxisstr, 1020);
            xaxisstr += 200;
            xpoint += 200;
            x -= 200;
        }
        x = -200;
        xpoint = 1550;
        xaxisstr = 1450;
        for (int i = 0; i < 6; i++)
        {
            g.DrawLine(AxisPoint, xpoint, 1070, xpoint, 1100);
            string xaxis = x.ToString();
            g.DrawString(xaxis, Axisfont, brush, xaxisstr, 1020);
            xaxisstr += 200;
            xpoint += 200;
            x -= 200;
        }

        int y = -1000;
        int ypoint = 100;
        int yaxisstr = 70;
        for (int i = 0; i < 5; i++)
        {
            g.DrawLine(AxisPoint, 1350, ypoint, 1380, ypoint);
            string yasix = y.ToString();
            g.DrawString(yasix, Axisfont, brush, 1390, yaxisstr);
            yaxisstr += 200;
            ypoint += 200;
            y += 200;
        }
        y = 200;
        ypoint = 1300;
        yaxisstr = 1270;

        for (int i = 0; i < 5; i++)
        {
            g.DrawLine(AxisPoint, 1350, ypoint, 1380, ypoint);
            string yasix = y.ToString();
            g.DrawString(yasix, Axisfont, brush, 1390, yaxisstr);
            yaxisstr += 200;
            ypoint += 200;
            y += 200;
        }

        g.FillRectangle(glassbrush, 100, 2350, 2500, 150);

        int jitaix = 120;
        int jitaistrx = 260;
        for (int i = 0; i < 10; i++)
        {
            Brush jitai = new SolidBrush(colors[i]);
            g.FillRectangle(jitai, jitaix, 2360, 80, 50);
            string str = (i + 1).ToString()+"#";
            g.DrawString(str, Biaoshifont, brush, jitaistrx, 2360);
            jitaix += 240;
            jitaistrx += 240;
        }

        jitaix = 120;
        jitaistrx = 260;
        for (int i = 0; i < 10; i++)
        {
            Brush jitai = new SolidBrush(colors[i+6]);
            g.FillRectangle(jitai, jitaix, 2440, 80, 50);
            string str = (i + 11).ToString() + "#";
            g.DrawString(str, Biaoshifont, brush, jitaistrx, 2440);
            jitaix += 240;
            jitaistrx += 240;
        }

        //x = 200;
        //y = 200;
        //x=-x+1200-10;
        //y=y+1250-10;
        //g.FillEllipse(brush, x, y, 20, 20);

        string ConnectionString = ConfigurationManager.ConnectionStrings["mdbcon"].ConnectionString;
        OleDbConnection conn = new OleDbConnection(ConnectionString);
        OleDbCommand cmd = new OleDbCommand(sql, conn);
        conn.Open();
        OleDbDataReader rd = cmd.ExecuteReader();
        while (rd.Read())
        {
            int lindex=int.Parse(rd["lindex"].ToString())-1;
            int penwidth=int.Parse(rd["size"].ToString());
            Pen dp = new Pen(colors[lindex], penwidth);
            Brush db = new SolidBrush(colors[lindex]);
            if (rd["muraname"].ToString() != "")
            {
                if (rd["shape"].ToString() == "Point")
                {
                    float spx = 0, spy = 0;
                    float.TryParse(rd["spx"].ToString(), out spx);
                    float.TryParse(rd["spy"].ToString(), out spy);
                    spx = -spx + 1350- (penwidth/2) ;
                    spy = spy + 1100 - (penwidth / 2);
                    g.FillEllipse(db, spx, spy, penwidth, penwidth);
                }
                if (rd["shape"].ToString() == "Line")
                {
                    float spx = 0, spy = 0, epx=0, epy=0;
                    float.TryParse(rd["spx"].ToString(), out spx);
                    float.TryParse(rd["spy"].ToString(), out spy);
                    float.TryParse(rd["epx"].ToString(), out epx);
                    float.TryParse(rd["epy"].ToString(), out epy);
                    spx = -spx + 1350;
                    spy = spy + 1100;
                    epx = -epx + 1350;
                    epy = epy + 1100;
                    if (rd["sizename"].ToString() == "Small")
                    {
                        dp.Width = 10;
                    }
                    if (rd["sizename"].ToString() == "Medium")
                    {
                        dp.Width = 20;
                    }
                    if (rd["sizename"].ToString() == "Large")
                    {
                        dp.Width = 40;
                    }
                    g.DrawLine(dp, spx, spy, epx, epy);
                }
            }
        }


        MemoryStream stream = new MemoryStream();   //保存绘制的图片
        img.Save(stream, ImageFormat.Jpeg);          //保存绘制的图片
        return stream;
    }
}