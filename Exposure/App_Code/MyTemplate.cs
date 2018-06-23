using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;

/// <summary>
///MyTemplate 的摘要说明
/// </summary>
public class MyTemplate:ITemplate
{
    private string strColumnName;
    private DataControlRowType dcrtColumnType;
	public MyTemplate()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    /// <summary>
    /// 动态添加模版列
    /// </summary>
    /// <param name="strColumnName">列名</param>
    /// <param name="dcrtColumnType">列的类型</param>
    public MyTemplate(string strColumnName, DataControlRowType dcrtColumnType)
    {
        this.strColumnName = strColumnName;
        this.dcrtColumnType = dcrtColumnType;
    }

    public void InstantiateIn(Control ctlContainer)
    {
        
        switch (dcrtColumnType)
        {
            case DataControlRowType.Header: //列标题
                Literal ltr = new Literal();
                ltr.Text = strColumnName;
                ctlContainer.Controls.Add(ltr);
                break;
            case DataControlRowType.DataRow: //模版列内容——加载CheckBox   
                CheckBox cb = new CheckBox();
                cb.ID = "CheckBox1";
                cb.Checked = false;
                ctlContainer.Controls.Add(cb);
                break;
        }
    }
}