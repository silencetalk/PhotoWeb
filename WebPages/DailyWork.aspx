<%@ Page Language="C#" MasterPageFile="~/WebPages/MasterPage.master" AutoEventWireup="true" CodeFile="DailyWork.aspx.cs" Inherits="WebPages_DailyWork" Title="工作安排" %>

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

<div style="position:relative; z-index:100;">
<div id="Add_Dialog" class="FormDiv">
<div class="FormTitle">工作安排</div>
<div class="FormInput">
<div class="FormBody">
<p>
    机台：<asp:DropDownList ID="DropDownListJitai" runat="server" CssClass="InputStyle" AutoPostBack="true" OnSelectedIndexChanged="JitaiChanged">
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
    <asp:ListItem>平行展开</asp:ListItem>
    </asp:DropDownList>
    工程师：<asp:DropDownList ID="DropDownListEng" runat="server" CssClass="InputStyle" >
    <asp:ListItem  Selected="True">请选择工程师</asp:ListItem>
            <asp:ListItem>共同确认</asp:ListItem>
            <asp:ListItem>张心杰</asp:ListItem>
            <asp:ListItem>刘成伟</asp:ListItem>
            <asp:ListItem>胡文斌</asp:ListItem>
            <asp:ListItem>周伟</asp:ListItem>
            <asp:ListItem>徐加荣</asp:ListItem>
            <asp:ListItem>王朵朵</asp:ListItem>
            <asp:ListItem>李荣</asp:ListItem>
            <asp:ListItem>李晓虎</asp:ListItem>
            <asp:ListItem>王峰超</asp:ListItem>
            <asp:ListItem>余海</asp:ListItem>
            <asp:ListItem>赵武</asp:ListItem>
            <asp:ListItem>钱宏</asp:ListItem>
            <asp:ListItem>何为</asp:ListItem>
            <asp:ListItem>肖文龙</asp:ListItem>
            <asp:ListItem>宫剑</asp:ListItem>
            <asp:ListItem>刘博</asp:ListItem>
            <asp:ListItem>程邵磊</asp:ListItem>
            <asp:ListItem>张卫帅</asp:ListItem>
            <asp:ListItem>吴四权</asp:ListItem>
            <asp:ListItem>陈学法</asp:ListItem>
            <asp:ListItem>丁瑞</asp:ListItem>
            <asp:ListItem>汪琪</asp:ListItem>
            <asp:ListItem>陈号</asp:ListItem>
            <asp:ListItem>冯军</asp:ListItem>
            <asp:ListItem>胡彬</asp:ListItem>
            <asp:ListItem>章开兵</asp:ListItem>
            <asp:ListItem>王学峰</asp:ListItem>
            <asp:ListItem>杨大伟</asp:ListItem>
            <asp:ListItem>李允伟</asp:ListItem>
            <asp:ListItem>仇凯凯</asp:ListItem>
            <asp:ListItem>陈超</asp:ListItem>
    </asp:DropDownList></p>
    <p>日期：<asp:TextBox ID="TextBoxInputDate" runat="server" Width="68px" CssClass="TextboxStyle"></asp:TextBox></p>
    <p>工作安排：</p>
    <p><asp:TextBox ID="TextBoxDailyWork" runat="server" TextMode="MultiLine" Height="150px" Width="100%" style="margin:0;padding:0;" Font-Names="Microsoft Yahei" Font-Size="12px"></asp:TextBox></p>
    <p>进度：</p>
    <p><asp:TextBox ID="TextBoxJindu" runat="server" TextMode="MultiLine" Height="150px" Width="100%" style="margin:0;padding:0;" Font-Names="Microsoft Yahei" Font-Size="12px"></asp:TextBox></p>
    <div style="margin:10px 0px 0px 0px;padding:0px;"><div class="status">Status：</div><div class="switch"><asp:Button ID="Open" runat="server" Text="Open"  CssClass="open" OnClick="Open_Click" Visible="false"/>
    <asp:Button ID="Close" runat="server" Text="Close"  CssClass="close" OnClick="Close_Click" Visible="true"/></div></div>
    <p>附件：<asp:DropDownList ID="DropDownListFenlei" runat="server" CssClass="InputStyle">
    <asp:ListItem   Selected="True">设备报告</asp:ListItem>
    <asp:ListItem>周报</asp:ListItem>
    <asp:ListItem>工艺报告</asp:ListItem>
    <asp:ListItem>测量数据</asp:ListItem>
    <asp:ListItem>培训资料</asp:ListItem>
    <asp:ListItem>事务资料</asp:ListItem>
    </asp:DropDownList> 
    标题：<asp:TextBox ID="TextBoxFileTittle" runat="server" Width="100px" CssClass="TextboxStyle"></asp:TextBox></p>
    <p><asp:FileUpload ID="FileUpload1" runat="server" Width="265px" CssClass="InputStyle"/></p>
    <p><asp:Label ID="ToolTips" runat="server" Text=""></asp:Label></p>
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
                    工作安排
                </td>
                <td class="ToolsTable">
                    <div id="Tools" class="Tools">
                        <button id="PreDate" type="button" runat="server" onserverclick="PreDate_Click" class="ButtonCycle"><span style="font-family:fontawesome !important;">&#xf060;</span></button>
                        <button id="SetDate" type="button" runat="server" onserverclick="SetDate_Click" class="ToolButton"><span style="font-family:fontawesome !important;">&#xf073;</span>&nbsp;<asp:Label ID="LabelDate" runat="server" Text=""></asp:Label></button>
                        <button id="NextDate" type="button" runat="server" onserverclick="NextDate_Click" class="ButtonCycle"><span style="font-family:fontawesome !important;">&#xf061;</span></button>
                        <button id="Add" type="button" runat="server" onserverclick="Add_Click" class="ToolButton"><span style="font-family:fontawesome !important;">&#xf067;</span>&nbsp;添加</button>                        
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
    <asp:Button ID="Button1" runat="server" Text="日常交接" OnClick="Inform_Click" CssClass="MenuButton"/>
    <asp:Button ID="personal" runat="server" Text="个人交接" OnClick="personal_Click" CssClass="MenuButton"/>
    <asp:Button ID="Button2" runat="server" Text="待办事项"  OnClick="Geren_Click" CssClass="MenuButton"/>
    <asp:Button ID="Work" runat="server" Text="工作安排" CssClass="MenuSelected"/>
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
                                <asp:BoundField DataField="jitai" HeaderText="设备">
                                    <HeaderStyle Width="6%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="shortdate" HeaderText="日期">
                                    <HeaderStyle Width="8%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="content" HeaderText="内容" HtmlEncode="False">
                                    <HeaderStyle Width="31%" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                 <asp:BoundField DataField="jindu" HeaderText="进度" HtmlEncode="False">
                                    <HeaderStyle Width="31%" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="附件">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="fujian" runat="server" Text='<%#Eval("title") %>' Font-Size="Small" Visible='<%#Eval("fujian").ToString()==""?false:true%>' NavigateUrl='<%#"DownLoad.aspx?File="+Eval("fujian") %>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <HeaderStyle Width="8%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="status" HeaderText="状态">
                                    <HeaderStyle Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="eng" HeaderText="工程师" HtmlEncode="False">
                                    <HeaderStyle Width="6%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle Font-Names="微软雅黑" ForeColor="#3366FF"/>
     </asp:GridView> 
    </div>
    </div>
<!--endprint-->

</asp:Content>

