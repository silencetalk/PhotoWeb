using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
///DateManage 的摘要说明
/// </summary>
public class DateManage
{
	public DateManage()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
    public static DateTime GetWeekUpOfDate(DateTime dt, int weekday, int number)
    {
        int wd = (int)dt.DayOfWeek;
        if (weekday == wd)
            return dt.AddDays(7 * number);
        else if (weekday > wd)
            return dt.AddDays(weekday - wd);
        else
            return dt.AddDays(7 * number - wd + weekday);
    }
    public static DateTime GetDayUpOfDate(DateTime dt, int day, int number)
    {
        if (dt.Day == day)
            return dt.AddMonths(number);
        else
        {
            if ((dt.Month == 1) && (day > 28))
                day = 28;
            if (day > dt.Day)
                number = 0;
            return dt.AddMonths(number).AddDays(-dt.Day).AddDays(day);
        }
    }
}
