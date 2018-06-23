<%@ Page Title="跟机日报" Language="C#" MasterPageFile="~/WebPages/MasterPage.master" AutoEventWireup="true" CodeFile="SetupReport.aspx.cs" Inherits="WebPages_SetupReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div style="position:relative; z-index:100;">

<div id="Add_Dialog" class="FormDiv">
<div class="FormTitle">跟机日报</div>
<div class="FormInput">
<div class="FormBody">
    <p>
    机台：<asp:DropDownList ID="DropDownListJitai" runat="server" Font-Names="Microsoft Yahei" Font-Size="12px" AutoPostBack="True" OnSelectedIndexChanged="JitaiChanged">
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
					<asp:ListItem>UT</asp:ListItem>
    </asp:DropDownList>
    日期：<asp:TextBox ID="TextBoxInputDate" runat="server" Width="68px" CssClass="TextboxStyle"></asp:TextBox></p>
    <p><asp:Label ID="ToolTips" runat="server"></asp:Label></p>
    <p>今日工作：</p>
    <p><asp:TextBox ID="TextBoxToday" runat="server" TextMode="MultiLine" Height="100px" Width="100%" style="margin:0;padding:0;" Font-Names="Microsoft Yahei" Font-Size="12px"></asp:TextBox></p>
    <p>跟进事项：</p>
    <p><asp:TextBox ID="TextBoxGenjin" runat="server" TextMode="MultiLine" Height="100px" Width="100%" style="margin:0;padding:0;" Font-Names="Microsoft Yahei" Font-Size="12px"></asp:TextBox></p>
    <p>明日工作：</p>
    <p><asp:TextBox ID="TextBoxTomorrow" runat="server" TextMode="MultiLine" Height="100px" Width="100%" style="margin:0;padding:0;" Font-Names="Microsoft Yahei" Font-Size="12px"></asp:TextBox></p>
    <p>工程师：</p>
    <p><asp:TextBox ID="TextBoxEng" runat="server" TextMode="MultiLine" Height="50px" Width="100%" style="margin:0;padding:0;" Font-Names="Microsoft Yahei" Font-Size="12px"></asp:TextBox></p>
    <p>附件：</p>
    <p>标题：<asp:TextBox ID="TextBoxFileTittle" runat="server" Width="200px" CssClass="TextboxStyle"></asp:TextBox></p>
    <p>
    <asp:DropDownList ID="DropDownListFenlei" runat="server" CssClass="InputStyle">
    <asp:ListItem  Selected="True">请选择文档分类</asp:ListItem>
    <asp:ListItem>周报</asp:ListItem>
    <asp:ListItem>设备报告</asp:ListItem>
    <asp:ListItem>工艺报告</asp:ListItem>
    <asp:ListItem>测量数据</asp:ListItem>
    <asp:ListItem>培训资料</asp:ListItem>
    <asp:ListItem>事务资料</asp:ListItem>
    </asp:DropDownList> 
    <asp:FileUpload ID="FileUpload1" runat="server" CssClass="InputStyle" Width="200px"/></p>
    
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
                     跟机日报
                </td>
                <td class="ToolsTable">
                    <div id="Tools" class="Tools">
                        <button id="PreDate" type="button" runat="server" onserverclick="PreDate_Click" class="ButtonCycle"><span style="font-family:fontawesome !important;">&#xf060;</span></button>
                        <button id="SetDate" type="button" runat="server" onserverclick="SetDate_Click" class="ToolButton"><span style="font-family:fontawesome !important;">&#xf073;</span>&nbsp<asp:Label ID="LabelDate" runat="server" Text=""></asp:Label></button>
                        <button id="NextDate" type="button" runat="server" onserverclick="NextDate_Click" class="ButtonCycle"><span style="font-family:fontawesome !important;">&#xf061;</span></button>
                        <button id="Add" type="button" runat="server" onserverclick="Add_Click" class="ToolButton"><span style="font-family:fontawesome !important;">&#xf067;</span>&nbsp;添加</button>
                        <button id="Delete" type="button" runat="server" onserverclick="Delete_Click" class="ToolButton"><span style="font-family:fontawesome !important;">&#xf014;</span>&nbsp;删除</button>
                        <button id="Edit" type="button" runat="server" onserverclick="Edit_Click" class="ToolButton"><span style="font-family:fontawesome !important;">&#xf044;</span>&nbsp;编辑</button>
                        <button id="Print" type="button" onclick="Print_Click()" class="ToolButton"><span style="font-family:fontawesome !important;">&#xf02f;</span>&nbsp;打印</button>
                        <asp:TextBox ID="TextBoxSearch" runat="server" placeholder="关键词：" CssClass="ToolInput"></asp:TextBox>
                        <button id="Search" type="button" runat="server" onserverclick="Search_Click" class="SearchButton"><span style="font-family:fontawesome !important;">&#xf002;</span>&nbsp;搜索</button>
                    </div>
                </td>
            </tr>
        </table>
</div>

<!--startprint-->
    <div class="SubMenu">
        <asp:Button ID="Jiaojie" runat="server" Text="日常交接" OnClick="Inform_Click" CssClass="MenuButton"/>
        <asp:Button ID="Setup" runat="server" Text="跟机日报" CssClass="MenuSelected"/>
        <asp:Button ID="Genren" runat="server" Text="DNS工作"  OnClick="Geren_Click" CssClass="MenuButton"/>
        <asp:Button ID="Work" runat="server" Text="工作安排" OnClick="Work_Click" CssClass="MenuButton"/>
    </div>
    <div class="SubView">
    <div class="View">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            EnableModelValidation="True"  CssClass="gridtable" DataKeyNames="ID">
                            <Columns>
                                <asp:TemplateField HeaderText="选择">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="Server" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridtitle" Width="5%"/>
                                    <ItemStyle CssClass="griditemcenter"/>
                                </asp:TemplateField>
                                <asp:BoundField DataField="shortdate" HeaderText="日期">
                                    <HeaderStyle CssClass="gridtitle" Width="8%" />
                                   <ItemStyle CssClass="griditemcenter"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="jitai" HeaderText="机台">
                                    <HeaderStyle CssClass="gridtitle" Width="7%" />
                                   <ItemStyle CssClass="griditemcenter"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="today" HeaderText="今日工作" HtmlEncode="False">
                                    <HeaderStyle CssClass="gridtitle" Width="22%" />
                                    <ItemStyle CssClass="griditem" />
                                </asp:BoundField>
                                <asp:BoundField DataField="genjin" HeaderText="跟进事项" HtmlEncode="False">
                                    <HeaderStyle CssClass="gridtitle" Width="22%" />
                                    <ItemStyle CssClass="griditem" />
                                </asp:BoundField>
                                <asp:BoundField DataField="tomorrow" HeaderText="明日工作" HtmlEncode="False">
                                    <HeaderStyle CssClass="gridtitle" Width="20%" />
                                    <ItemStyle CssClass="griditem" />
                                </asp:BoundField>
                                <asp:BoundField DataField="eng" HeaderText="工程师" HtmlEncode="False">
                                    <HeaderStyle CssClass="gridtitle" Width="8%" />
                                   <ItemStyle CssClass="griditemcenter"/>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="附件">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="fujian" runat="server" Text='<%#Eval("title") %>' Font-Size="Small" Visible='<%#Eval("fujian").ToString()==""?false:true%>' NavigateUrl='<%#"DownLoad.aspx?File="+Eval("fujian") %>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="gridtitle" Width="8%" />
                                   <ItemStyle CssClass="griditemcenter"/>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="gridtitle" Font-Names="微软雅黑" ForeColor="#3366FF"/>
     </asp:GridView> 
     
    </div>
    </div>
<!--endprint-->




</asp:Content>

