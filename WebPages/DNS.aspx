<%@ Page Language="C#" MasterPageFile="~/WebPages/MasterPage.master" AutoEventWireup="true" CodeFile="DNS.aspx.cs" Inherits="WebPages_DNS" Title="待办事项" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .status
        {
        	float:left;
        	width:auto;
        	height:20px;
        	line-height:20px;
        	border:none;
        	margin:0px;
        	padding:0px 2px;
        }
        .switch
        {
        	position:relative;
        	width:100px;
        	height:20px;
        	border:solid 1px rgb(204,204,204) ;
        	margin:0px;
        	padding:0px;
        }
        .open
        {
        	position:absolute;
        	top:0px;
        	left:0px;
        	width:50px;
        	height:20px;
        	line-height:20px;
            color:White;
            text-align:center;
        	border:none;
        	text-decoration:none;
        	outline:none;
        	background-color:rgb(66,139,202);
        	color:White;
        }
        .close
        {
        	position:absolute;
        	top:0px;
        	left:50px;
        	width:50px;
        	height:20px;
        	line-height:20px;
        	text-align:center;
        	border:none;
        	text-decoration:none;
        	outline:none;
        	background-color:rgb(230,230,230);
        	color:Black;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <div id="SubTitle" class="SubTitle">
        <table style="width: 100%;">
            <tr>
                <td class="SubTitleTable">
                    待办事项
                </td>
                <td class="ToolsTable">
                    <div id="Tools" class="Tools">
                        <button id="PreDate" type="button" runat="server" onserverclick="PreDate_Click" class="ButtonCycle"><span style="font-family:fontawesome !important;">&#xf060;</span></button>
                        <button id="SetDate" type="button" runat="server" onserverclick="SetDate_Click" class="ToolButton"><span style="font-family:fontawesome !important;">&#xf073;</span>&nbsp;<asp:Label ID="LabelDate" runat="server" Text=""></asp:Label></button>
                        <button id="NextDate" type="button" runat="server" onserverclick="NextDate_Click" class="ButtonCycle"><span style="font-family:fontawesome !important;">&#xf061;</span></button>
                        <button id="Add" type="button" runat="server" onserverclick="Add_Click" class="ToolButton"><span style="font-family:fontawesome !important;">&#xf067;</span>&nbsp;添加</button>
                        <button id="Delete" type="button" runat="server" onserverclick="Delete_Click" class="ToolButton"><span style="font-family:fontawesome !important;">&#xf014;</span>&nbsp;删除</button>
                        <button id="Edit" type="button" runat="server" onserverclick="Edit_Click" class="ToolButton"><span style="font-family:fontawesome !important;">&#xf044;</span>&nbsp;编辑</button>
                        <button id="Reply" type="button" onserverclick="Reply_Click" class="ToolButton" runat="server"><span style="font-family:fontawesome !important;">&#xf112;</span>&nbsp;回复</button>
                        <button id="Print" type="button" onclick="Print_Click()" class="ToolButton"><span style="font-family:fontawesome !important;">&#xf02f;</span>&nbsp;打印</button>
                        <asp:TextBox ID="TextBoxSearch" runat="server" placeholder="关键词：" CssClass="ToolInput"></asp:TextBox>
                        <button id="Search" type="button" runat="server" onserverclick="Search_Click" class="SearchButton">&nbsp;<span style="font-family:fontawesome !important;">&#xf002;</span>&nbsp;</button>
                    </div>
                </td>
            </tr>
        </table>
</div>

<div style="position:relative; z-index:100;">
<div id="Add_Dialog" class="FormDiv">
<div class="FormTitle">待办事项</div>
<div class="FormInput">
<div class="FormBody">
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
    <asp:ListItem>All</asp:ListItem>
    <asp:ListItem>Parts借用</asp:ListItem>
    </asp:DropDownList>
    日期：<asp:TextBox ID="TextBoxInputDate" runat="server" Width="68px" CssClass="TextboxStyle"></asp:TextBox></p>
    <p><asp:TextBox ID="TextBoxDailyInform" runat="server" TextMode="MultiLine" Height="200px" Width="100%" style="margin:0;padding:0;font-size:12px;font-family:Microsoft Yahei;"></asp:TextBox></p>
    <div style="margin:10px 0px 0px 0px;padding:0px;"><div class="status">Status：</div><div class="switch"><asp:Button ID="Open" runat="server" Text="Open"  CssClass="open" OnClick="Open_Click" Visible="true"/>
    <asp:Button ID="Close" runat="server" Text="Close"  CssClass="close" OnClick="Close_Click" Visible="false"/></div></div>
    <p><span style="font-family:fontawesome !important; color:Red;">&#xf071;</span>&nbsp;<asp:Label ID="ToolTips1" runat="server" Text="字数请不要超过900字！"></asp:Label></p>
</div>
</div>
<div class="FormFooter">
     <asp:Button ID="Add_Dialog_OK" runat="server" Text="确定" CssClass="ConfirmButton" OnClick="AddDI_OK_Click"/>
     <asp:Button ID="Concel1" runat="server" Text="取消" CssClass="ConfirmButton" OnClick="Add_Cancel_Click"/>
</div>
</div>

<div id="Add_Reply" class="FormDiv">
<div class="FormTitle">待办事项</div>
<div class="FormInput">
<div class="FormBody">
<p>
    进度：<asp:Label ID="LabelReply" runat="server" Text=""></asp:Label></p>
    <p><asp:TextBox ID="TextBoxReply" runat="server" TextMode="MultiLine" Height="200px" Width="100%" style="margin:0;padding:0;" Font-Names="Microsoft Yahei" Font-Size="12px"></asp:TextBox></p>
    <div style="margin:10px 0px 0px 0px;padding:0px;"><div class="status">Status：</div><div class="switch"><asp:Button ID="Open1" runat="server" Text="Open"  CssClass="open" OnClick="Open_Reply_Click" Visible="false"/>
    <asp:Button ID="Close1" runat="server" Text="Close"  CssClass="close" OnClick="Close_Reply_Click" Visible="true"/></div></div>
<p><span style="font-family:fontawesome !important; color:Red;">&#xf071;</span>&nbsp;<asp:Label ID="ToolTips2" runat="server" Text="字数请不要超过900字！"></asp:Label></p>
</div>
</div>
<div class="FormFooter">
     <asp:Button ID="Add_Reply_OK" runat="server" Text="确定" CssClass="ConfirmButton" OnClick="AddReply_OK_Click"/>
     <asp:Button ID="Button2" runat="server" Text="取消" CssClass="ConfirmButton" OnClick="Add_Cancel_Click"/>
</div>
</div>
</div>

<!--startprint-->
    <div class="SubMenu">
     <asp:Button ID="Jiaojie" runat="server" Text="日常交接" OnClick="Inform_Click" CssClass="MenuButton"/>
     <asp:Button ID="personal" runat="server" Text="个人交接" OnClick="personal_Click" CssClass="MenuButton"/>
     <asp:Button ID="Genren" runat="server" Text="待办事项"  CssClass="MenuSelected"/>
     <asp:Button ID="Work" runat="server" Text="工作安排" OnClick="Work_Click" CssClass="MenuButton"/>
    </div>
    <div class="SubView">
  <%--  <button id="fullscreen" onclick="fullscreenclick()"><span style="font-family:fontawesome !important;">&#xf0b2;</span></button>--%>
    <div id="View" class="View">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            EnableModelValidation="True" Width="100%"
                            BorderColor="#A0A0A0" BorderStyle="Solid" 
            BorderWidth="1px" DataKeyNames="ID">
                            <Columns>
                                <asp:TemplateField HeaderText="选择">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="Server" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="shortdate" HeaderText="日期">
                                    <HeaderStyle Width="8%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="jitai" HeaderText="设备">
                                    <HeaderStyle Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="content" HeaderText="内容" HtmlEncode="False">
                                    <HeaderStyle Width="50%" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="eng" HeaderText="工程师">
                                    <HeaderStyle Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="reply" HeaderText="进度" HtmlEncode="False">
                                    <HeaderStyle Width="22%" />
                                    <ItemStyle HorizontalAlign="Left" ForeColor="Blue" />
                                </asp:BoundField>
                                <asp:BoundField DataField="status" HeaderText="状态">
                                    <HeaderStyle Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle Font-Names="微软雅黑" ForeColor="#3366FF"/>
     </asp:GridView> 
    </div>
    </div>
<!--endprint-->

</asp:Content>

