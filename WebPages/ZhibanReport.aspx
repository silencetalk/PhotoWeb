<%@ Page Language="C#" MasterPageFile="~/WebPages/MasterPage.master" AutoEventWireup="true" CodeFile="ZhibanReport.aspx.cs" Inherits="WebPages_ZhibanReport" Title="值班报告" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">
.headertem
{
	width:100%;
	height:40px;
	line-height:40px;
	text-align:center;
	font-family:Microsoft Yahei, Arial, Sans-Serif;
	font-size:16px;
	font-weight:bold;
}
.ItemLeft
{
	width:10%;
	text-align:center;
	vertical-align:middle;
	border-bottom:1px solid #A0A0A0;
	border-right:1px solid #A0A0A0;
	font-family:Microsoft Yahei, Arial, Sans-Serif;
	font-size:12px;
	color:Blue;
}
.ItemLeftEnd
{
	width:10%;
	text-align:center;
	vertical-align:middle;
	border-right:1px solid #A0A0A0;
	font-family:Microsoft Yahei, Arial, Sans-Serif;
	font-size:12px;
	color:Blue;
}
.ItemRight
{
    border-bottom:1px solid #A0A0A0;
	width:90%;
	border-bottom:1px solid #A0A0A0;
	text-align:left;
	vertical-align:middle;
	font-family:Microsoft Yahei, Arial, Sans-Serif;
	font-size:12px;
}
.ItemRightEnd
{
    border-bottom:1px solid #A0A0A0;
	width:90%;
	text-align:left;
	vertical-align:middle;
	font-family:Microsoft Yahei, Arial, Sans-Serif;
	font-size:12px;
}
    .addalarm
    {
    	height:17px;
    	line-height:17px;
    	margin-right:5px;
    	padding:0px 5px;
       font-family:Microsoft Yahei, Arial, Sans-Serif;
	   font-size:12px;
    	cursor:pointer;
        color:White;
        text-align:center;
    	border:none;
    	text-decoration:none;
    	outline:none;
    	background-color:rgb(66,139,202);
    	color:White;
    }
    .addalarm:hover
    {
    	background-color:blue;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div style="position:relative; z-index:100;">

<div id="Add_Alarm_Dialog" class="FormDiv">
<div class="FormTitle">Alarm</div>
<div class="FormInput">
<div class="FormBody">
    <p><asp:Label ID="ToolTipsAlarm" runat="server"></asp:Label></p>
    <p>
    机台：<asp:DropDownList ID="DropDownListJitai" runat="server" CssClass="InputStyle">
    <asp:ListItem  Selected="True">请选择机台</asp:ListItem>
    				<asp:ListItem>1号机</asp:ListItem>
					<asp:ListItem>2号机</asp:ListItem>
					<asp:ListItem>3号机</asp:ListItem>
					<asp:ListItem>4号机</asp:ListItem>
					<asp:ListItem>5号机</asp:ListItem>
					<asp:ListItem>6号机</asp:ListItem>
					<asp:ListItem>7号机</asp:ListItem>
					<asp:ListItem>8号机</asp:ListItem>
					<asp:ListItem>9号机</asp:ListItem>
					<asp:ListItem>10号机</asp:ListItem>
					<asp:ListItem>11号机</asp:ListItem>
					<asp:ListItem>12号机</asp:ListItem>
					<asp:ListItem>13号机</asp:ListItem>
					<asp:ListItem>14号机</asp:ListItem>
					<asp:ListItem>15号机</asp:ListItem>
					<asp:ListItem>16号机</asp:ListItem>
					<asp:ListItem>17号机</asp:ListItem>
					<asp:ListItem>18号机</asp:ListItem>
					<asp:ListItem>19号机</asp:ListItem>
					<asp:ListItem>20号机</asp:ListItem>
    </asp:DropDownList>
    单元：<asp:DropDownList ID="DropDownListDanyuan" runat="server" CssClass="InputStyle">
    <asp:ListItem  Selected="True">请选择单元</asp:ListItem>
                            <asp:ListItem>Indexer</asp:ListItem>
                            <asp:ListItem>Cleaner</asp:ListItem>
                            <asp:ListItem>DB</asp:ListItem>
                            <asp:ListItem>LC</asp:ListItem>
                            <asp:ListItem>VCD</asp:ListItem>
                            <asp:ListItem>SB</asp:ListItem>
                            <asp:ListItem>IF Buffer</asp:ListItem>
                            <asp:ListItem>DEV</asp:ListItem>
                            <asp:ListItem>HB</asp:ListItem>
                            <asp:ListItem>Bypass</asp:ListItem>
                            <asp:ListItem>OVEN</asp:ListItem>
                            <asp:ListItem>AOI</asp:ListItem>
                            <asp:ListItem>DCS</asp:ListItem>
                            <asp:ListItem>Dry Pump</asp:ListItem>
    </asp:DropDownList></p>
    <p>发生时间：<asp:TextBox ID="TextBoxDownTime" runat="server" Width="185px" CssClass="TextboxStyle"></asp:TextBox></p>
    <p>Down机时间(Hours)：<asp:TextBox ID="TextBoxHours" runat="server" Width="120px" CssClass="TextboxStyle" style="ime-mode:disabled"  onkeypress="if ((event.keyCode>=48 && event.keyCode<=57)||(event.keyCode==46)) {event.returnValue=true;}else{event.returnValue=false;}"></asp:TextBox></p>
    <p>Alarm：<asp:TextBox ID="TextBoxAlarm1" runat="server" Width="200px" CssClass="TextboxStyle"></asp:TextBox></p>
    <p>现象及分析：</p>
    <p><asp:TextBox ID="TextBoxFenxi" runat="server" TextMode="MultiLine" Height="100px" Width="100%" style="margin:0;padding:0;" Font-Names="Microsoft Yahei" Font-Size="12px"></asp:TextBox></p>
    <p>Action：</p>
    <p><asp:TextBox ID="TextBoxAction" runat="server" TextMode="MultiLine" Height="100px" Width="100%" style="margin:0;padding:0;" Font-Names="Microsoft Yahei" Font-Size="12px"></asp:TextBox></p>
    <p>影响：</p>
    <p><asp:TextBox ID="TextBoxYingxiang" runat="server" TextMode="MultiLine" Height="100px" Width="100%" style="margin:0;padding:0;" Font-Names="Microsoft Yahei" Font-Size="12px"></asp:TextBox></p>
    <p>附件：<asp:DropDownList ID="DropDownListFenlei" runat="server" CssClass="InputStyle">
    <asp:ListItem   Selected="True">设备报告</asp:ListItem>
    <asp:ListItem>周报</asp:ListItem>
    <asp:ListItem>工艺报告</asp:ListItem>
    <asp:ListItem>测量数据</asp:ListItem>
    <asp:ListItem>培训资料</asp:ListItem>
    <asp:ListItem>事务资料</asp:ListItem>
    </asp:DropDownList> 
    标题：<asp:TextBox ID="TextBoxFileTittle" runat="server" Width="100px" CssClass="TextboxStyle"></asp:TextBox></p>
    <p><asp:FileUpload ID="FileUpload2" runat="server" Width="265px" CssClass="InputStyle"/></p>
</div>
</div>
<div class="FormFooter">
     <asp:Button ID="Button1" runat="server" Text="确定" CssClass="ConfirmButton" OnClick="Add_OK_Alarm_Click"/>
     <asp:Button ID="Button2" runat="server" Text="取消" CssClass="ConfirmButton" OnClick="Add_Alarm_Cancel_Click"/>
</div>
</div>

<div id="Add_Dialog" class="FormDiv">
<div class="FormTitle">值班报告</div>
<div class="FormInput">
<div class="FormBody">
    <p>
    标题：<asp:TextBox ID="TextBoxDate" runat="server" Width="150px" CssClass="TextboxStyle"></asp:TextBox></p>
    <p><asp:Label ID="ToolTips" runat="server"></asp:Label></p>
    <p>产品：</p>
    <p><asp:TextBox ID="TextBoxProduct" runat="server" TextMode="MultiLine" Height="100px" Width="100%" style="margin:0;padding:0;" Font-Names="Microsoft Yahei" Font-Size="12px"></asp:TextBox></p>
    <p>Alarm： <button id="Add_Alarm" type="button" runat="server" onserverclick="Add_Alarm_Click" class="addalarm">添加</button></p>
<%--   <p>Alarm：</p>--%>
    <p><asp:TextBox ID="TextBoxAlarm" runat="server" TextMode="MultiLine" Height="100px" Width="100%" style="margin:0;padding:0;" Font-Names="Microsoft Yahei" Font-Size="12px"   ReadOnly="false"></asp:TextBox></p>
    <p>异常状况：</p>
    <p><asp:TextBox ID="TextBoxYichang" runat="server" TextMode="MultiLine" Height="100px" Width="100%" style="margin:0;padding:0;" Font-Names="Microsoft Yahei" Font-Size="12px"></asp:TextBox></p>
    <p>其他交接：</p>
    <p><asp:TextBox ID="TextBoxOther" runat="server" TextMode="MultiLine" Height="100px" Width="100%" style="margin:0;padding:0;" Font-Names="Microsoft Yahei" Font-Size="12px"></asp:TextBox></p>
    <p>值班人员：</p>
    <p><asp:TextBox ID="TextBoxEng" runat="server" TextMode="MultiLine" Height="70px" Width="100%" style="margin:0;padding:0;" Font-Names="Microsoft Yahei" Font-Size="12px"></asp:TextBox></p>
    <p>Abnormal：
    <asp:FileUpload ID="FileUpload1" runat="server" Width="200px" CssClass="InputStyle"/></p>  
</div>
</div>
<div class="FormFooter">
     <asp:Button ID="Add_Dialog_OK" runat="server" Text="确定" CssClass="ConfirmButton" OnClick="Add_OK_Click"/>
     <asp:Button ID="Concel1" runat="server" Text="取消" CssClass="ConfirmButton" OnClick="Add_Cancel_Click"/>
</div>
</div>
</div>

<div id="SubTitle" class="SubTitle">
        <table style="width: 100%;">
            <tr>
                <td class="SubTitleTable">
                    日常报告
                </td>
                <td class="ToolsTable">
                    <div id="Tools" class="Tools">
                        <button id="PreDate" type="button" runat="server" onserverclick="PreDate_Click" class="ButtonCycle"><span style="font-family:fontawesome !important;">&#xf060;</span></button>
                        <button id="SetDate" type="button" runat="server" onserverclick="SetDate_Click" class="ToolButton"><span style="font-family:fontawesome !important;">&#xf073;</span>&nbsp<asp:Label ID="LabelDate" runat="server" Text=""></asp:Label></button>
                        <button id="NextDate" type="button" runat="server" onserverclick="NextDate_Click" class="ButtonCycle" ><span style="font-family:fontawesome !important;">&#xf061;</span></button>
                        <button id="Add" type="button" runat="server" onserverclick="Add_Click" class="ToolButton"><span style="font-family:fontawesome !important;">&#xf067;</span>&nbsp;添加</button>
                        <button id="Delete" type="button" runat="server" onserverclick="Delete_Click" class="ToolButton"><span style="font-family:fontawesome !important;">&#xf014;</span>&nbsp;删除</button>
                        <button id="Edit" type="button" runat="server" onserverclick="Edit_Click" class="ToolButton"><span style="font-family:fontawesome !important;">&#xf044;</span>&nbsp;编辑</button>
                        <button id="Print" type="button" onclick="Print_Click()" class="ToolButton"><span style="font-family:fontawesome !important;">&#xf02f;</span>&nbsp;打印</button>
                        <asp:TextBox ID="TextBoxSearch" runat="server" placeholder="关键词：" CssClass="ToolInput"></asp:TextBox>
                        <button id="Search" type="button" runat="server" onserverclick="Search_Click" class="SearchButton">&nbsp;<span style="font-family:fontawesome !important;">&#xf002;</span>&nbsp;</button>
                    </div>
                </td>
            </tr>
        </table>
</div>

<!--startprint-->
    <div class="SubMenu">
    <asp:Button ID="zhiban" runat="server" Text="值班报告" CssClass="MenuSelected"/>
    <asp:Button ID="Setup" runat="server" Text="跟机报告"  OnClick="Setup_Click" CssClass="MenuButton"/>
    <asp:Button ID="Process" runat="server" Text="个人报告"  OnClick="Process_Click" CssClass="MenuButton"/>
    </div>
    <div class="SubView">
    <div class="View">
    
        <asp:DataList ID="DataList1" runat="server" Width="100%" CellPadding="0"  DataKeyField="ID">    
        <ItemTemplate>
        <div class="headertem"><%#Eval("title") %><asp:CheckBox ID="CheckBox1" runat="server"  Checked="true" style="display:none;"/></div>
        <table style="width:95%; margin:0px auto; border-collapse:collapse; border:1px solid #A0A0A0; table-layout :fixed;word-wrap:break-word; ">
        <tr>
        <td class="ItemLeft">产品</td>
        <td class="ItemRight"><%#Eval("chanpin") %></td>
        </tr>
        <tr>
        <td class="ItemLeft">Alarm</td>
        <td class="ItemRight"><%#Eval("alarm") %></td>
        </tr>
        <tr>
        <td class="ItemLeft">异常状况</td>
        <td class="ItemRight"><%#Eval("yichang") %></td>
        </tr>
        <tr>
        <td class="ItemLeft">其他交接</td>
        <td class="ItemRight"><%#Eval("other") %></td>
        </tr>
        <tr>
        <td class="ItemLeft"><br />Abnormal<br />&nbsp;</td>
        <td class="ItemRight" style="padding:0px 0px 0px 15px;">&nbsp;&nbsp;&nbsp;&nbsp;<asp:HyperLink ID="fujian2" runat="server" Font-Names="Microsoft Yahei" Font-Size="12px" Visible='<%#Eval("abnormal").ToString()==""?false:true%>' NavigateUrl='<%#"DownLoad.aspx?File="+Eval("abnormal") %>' ForeColor="Black"><%#Eval("abnormalfile") %></asp:HyperLink>
        </td>
        </tr>
        <tr>
         <td class="ItemLeftEnd">值班人员</td>
        <td class="ItemRightEnd"><%#Eval("eng") %></td>
        </tr>
        </table>
        </ItemTemplate>
        </asp:DataList>    
    </div>
    </div>
<!--endprint-->

</asp:Content>

