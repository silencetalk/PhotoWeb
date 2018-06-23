<%@ Page Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="DailyReport.aspx.cs" Inherits="WebPages_DailyReport" Title="值班报告" %>

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
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div style="position:relative; z-index:100;">
<div id="Add_Dialog" class="FormDiv">
<div class="FormTitle">值班报告</div>
<div class="FormInput">
<div class="FormBody">
    <p>
    标题：<asp:TextBox ID="TextBoxDate" runat="server" Width="150px" CssClass="TextboxStyle"></asp:TextBox></p>
    <p><asp:Label ID="ToolTips" runat="server"></asp:Label></p>
    <p>产品：</p>
    <p><asp:TextBox ID="TextBoxProduct" runat="server" TextMode="MultiLine" Height="100px" Width="100%" style="margin:0;padding:0;" Font-Names="Microsoft Yahei" Font-Size="12px"></asp:TextBox></p>
    <p>Alarm：</p>
    <p><asp:TextBox ID="TextBoxAlarm" runat="server" TextMode="MultiLine" Height="100px" Width="100%" style="margin:0;padding:0;" Font-Names="Microsoft Yahei" Font-Size="12px"></asp:TextBox></p>
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
                    Daily Report
                </td>
                <td class="ToolsTable">
                    <div id="Tools" class="Tools">
                        <button id="PreDate" type="button" runat="server" onserverclick="PreDate_Click" class="ToolButton"><span style="font-family:fontawesome !important;">&#xf060;</span></button>
                        <button id="SetDate" type="button" runat="server" onserverclick="SetDate_Click" class="ToolButton"><span style="font-family:fontawesome !important;">&#xf073;</span>&nbsp<asp:Label ID="LabelDate" runat="server" Text=""></asp:Label></button>
                        <button id="NextDate" type="button" runat="server" onserverclick="NextDate_Click" class="ToolButton"><span style="font-family:fontawesome !important;">&#xf061;</span></button>
                        <button id="Add" type="button" runat="server" onserverclick="Add_Click" class="ToolButton"><span style="font-family:fontawesome !important;">&#xf067;</span>&nbsp;添加</button>
                        <button id="Delete" type="button" runat="server" onserverclick="Delete_Click" class="ToolButton"><span style="font-family:fontawesome !important;">&#xf014;</span>&nbsp;删除</button>
                        <button id="Edit" type="button" runat="server" onserverclick="Edit_Click" class="ToolButton"><span style="font-family:fontawesome !important;">&#xf044;</span>&nbsp;编辑</button>
                        <button id="Print" type="button" onclick="Print_Click()" class="ToolButton"><span style="font-family:fontawesome !important;">&#xf02f;</span>&nbsp;打印</button>
                        <asp:TextBox ID="TextBoxSearch" runat="server" placeholder="关键词：" CssClass="ToolInput"></asp:TextBox>
                        <button id="Search" type="button" runat="server" onserverclick="Search_Click" class="ToolButton">&nbsp;<span style="font-family:fontawesome !important;">&#xf002;</span>&nbsp;</button>
                    </div>
                </td>
            </tr>
        </table>
</div>

<!--startprint-->
<div class="Content">
    <div class="DataView" style="text-align: center;">
    
        <asp:DataList ID="DataList1" runat="server" Width="100%" CellPadding="0"  DataKeyField="ID">    
        <ItemTemplate>
        <table style="width:100%; border-collapse:collapse; border:1px solid #A0A0A0; table-layout :fixed;word-wrap:break-word; ">
       <tr class="headertem">
       <td><%#Eval("title") %><asp:CheckBox ID="CheckBox1" runat="server"  Checked="true" style="display:none;"/></td>
       </tr>
        </table>   
        <table style="width:100%; border-collapse:collapse; border:1px solid #A0A0A0; table-layout :fixed;word-wrap:break-word; ">
        <tr>
        <td class="ItemLeft">产品</td>
        <td class="ItemRight"><br /><%#Eval("chanpin") %><p></p></td>
        </tr>
        <tr>
        <td class="ItemLeft">Alarm</td>
        <td class="ItemRight"><br /><%#Eval("alarm") %><p></p></td>
        </tr>
        <tr>
        <td class="ItemLeft">异常状况</td>
        <td class="ItemRight"><br /><%#Eval("yichang") %><p></p></td>
        </tr>
        <tr>
        <td class="ItemLeft">其他交接</td>
        <td class="ItemRight"><br /><%#Eval("other") %><p></p></td>
        </tr>
        <tr>
        <td class="ItemLeft"><br />Abnormal<br />&nbsp;</td>
        <td class="ItemRight" style="padding:0px 0px 0px 15px;">&nbsp;&nbsp;&nbsp;&nbsp;<asp:HyperLink ID="fujian2" runat="server" Font-Names="Microsoft Yahei" Font-Size="12px" Visible='<%#Eval("abnormal").ToString()==""?false:true%>' NavigateUrl='<%#"DownLoad.aspx?File="+Eval("abnormal") %>' ForeColor="Black"><%#Eval("abnormalfile") %></asp:HyperLink>
        </td>
        </tr>
        <tr>
         <td class="ItemLeftEnd">值班人员</td>
        <td class="ItemRightEnd"><br /><%#Eval("eng") %><p></p></td>
        </tr>
        </table>
        </ItemTemplate>
        </asp:DataList>    
    </div>
    </div>
<!--endprint-->

</asp:Content>

