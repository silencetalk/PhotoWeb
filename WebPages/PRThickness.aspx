<%@ Page Title="PR Thickness" Language="C#" MasterPageFile="~/WebPages/MasterPage.master" AutoEventWireup="true" CodeFile="PRThickness.aspx.cs" Inherits="WebPages_PRThickness" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div style="position:relative; z-index:100;">
<div id="Add_Dialog" class="FormDiv">
<div class="FormTitle">PR Thickness</div>
<div class="FormInput">
<div class="FormBody">
    <p>
    机台：<asp:DropDownList ID="DropDownListJitai" runat="server" Font-Names="Microsoft Yahei" Font-Size="12px">
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
    日期：<asp:TextBox ID="TextBoxDate" runat="server" Width="68px" CssClass="TextboxStyle"></asp:TextBox>
    厚度：<asp:DropDownList ID="DropDownThickness" runat="server" Font-Names="Microsoft Yahei" Font-Size="12px">
    <asp:ListItem Selected="True">1.5um</asp:ListItem>
    <asp:ListItem>1.8um</asp:ListItem>
    <asp:ListItem>2.2um</asp:ListItem>
    <asp:ListItem>1.5um</asp:ListItem>
    </asp:DropDownList>
    </p>
    <p>U值：
    SCAN：<asp:TextBox ID="TextBoxScan" runat="server" Width="50px" CssClass="TextboxStyle"  ime-mode="disabled" onkeypress="if ((event.keyCode<45||event.keyCode>57))event.returnValue=false; "></asp:TextBox>
    Nuzzle：<asp:TextBox ID="TextBoxNuzzle" runat="server" Width="50px" CssClass="TextboxStyle"  ime-mode="disabled" onkeypress="if ((event.keyCode<45||event.keyCode>57))event.returnValue=false; "></asp:TextBox>
    3D：<asp:TextBox ID="TextBox3D" runat="server" Width="50px" CssClass="TextboxStyle"  ime-mode="disabled" onkeypress="if ((event.keyCode<45||event.keyCode>57))event.returnValue=false; "></asp:TextBox>
    </p>
    <p>附件：
    <asp:FileUpload ID="FileUpload1" runat="server" Width="200px" CssClass="InputStyle"/></p> 
    <p><asp:Label ID="ToolTips" runat="server"></asp:Label></p>
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
                    Daily Monitor
                </td>
                <td class="ToolsTable">
                    <div id="Tools" class="Tools">
<%--                        <button id="PreDate" type="button" runat="server" onserverclick="PreDate_Click" class="ButtonCycle"><span style="font-family:fontawesome !important;">&#xf060;</span></button>
                        <button id="SetDate" type="button" runat="server" onserverclick="SetDate_Click" class="ToolButton"><span style="font-family:fontawesome !important;">&#xf073;</span><asp:Label ID="LabelDate" runat="server" Text=""></asp:Label></button>
                        <button id="NextDate" type="button" runat="server" onserverclick="NextDate_Click" class="ButtonCycle"><span style="font-family:fontawesome !important;">&#xf061;</span>&nbsp;</button>--%>
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
    <asp:Button ID="AOIMonitor" runat="server" Text="AOI Monitor"  OnClick="AOI_Click" CssClass="MenuButton"/>
    <asp:Button ID="Thickness" runat="server" Text="PR Thickness"  CssClass="MenuSelected"/>
    <asp:Button ID="MMMonitor" runat="server" Text="Mura Monitor" OnClick="MM_Click" CssClass="MenuButton"/>
    </div>
    <div class="SubView">
  <%--  <button id="fullscreen" onclick="fullscreenclick()"><span style="font-family:fontawesome !important;">&#xf0b2;</span></button>--%>
    <div id="View" class="View">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            EnableModelValidation="True" Width="100%" AllowPaging="True"  PageSize="20" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCommand="GridView1_RowCommand"
            BorderColor="#A0A0A0" BorderStyle="Solid" BorderWidth="1px" DataKeyNames="ID" >
                            <Columns>
                                <asp:TemplateField HeaderText="选择">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="Server" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                 <asp:BoundField DataField="shortdate" HeaderText="日期">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="jitai" HeaderText="机台">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="thickness" HeaderText="厚度">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="scan" HeaderText="Scan">
                                    <HeaderStyle Width="16%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nuzzle" HeaderText="Nuzzle">
                                    <HeaderStyle Width="16%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="3d" HeaderText="3D">
                                    <HeaderStyle Width="16%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                  <asp:TemplateField HeaderText="附件">
                                  <ItemTemplate>
                                      <asp:HyperLink ID="file" runat="server" Font-Size="Small" Visible='<%#Eval("fujian").ToString()==""?false:true%>'  NavigateUrl='<%#"DownLoad.aspx?File="+Eval("fujian") %>' CssClass="fontlink" ><span style="font-family:fontawesome !important; font-size:20px;">&#xf0ed;</span></asp:HyperLink>
                                   </ItemTemplate>
                                    <HeaderStyle Width="12%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                                                        <PagerTemplate>
                                <asp:LinkButton ID="firstpage" runat="server" Font-Names="Microsoft Yahei" Text="首页"
                                    Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>' CommandName="Page"
                                    CommandArgument="First" ForeColor="#3366FF"></asp:LinkButton>
                                <asp:LinkButton ID="uppage" runat="server" Font-Names="Microsoft Yahei" Text="上一页"
                                    Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>' CommandName="Page"
                                    CommandArgument="Prev" ForeColor="#3366FF"></asp:LinkButton>
                                <asp:LinkButton ID="downpage" runat="server" Font-Names="Microsoft Yahei" Text="下一页"
                                    Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>'
                                    CommandName="Page" CommandArgument="Next" ForeColor="#3366FF"></asp:LinkButton>
                                <asp:LinkButton ID="lastpage" runat="server" Font-Names="Microsoft Yahei" Text="尾页"
                                    Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>'
                                    CommandName="Page" CommandArgument="Last" ForeColor="#3366FF"></asp:LinkButton>
                                <asp:Label ID="Labelyeshu" runat="server" Font-Names="Microsoft Yahei" Font-Size="Small"
                                    Height="18px" Width="40px" ForeColor="#3366FF" Text='<%# (((GridView)Container.NamingContainer).PageIndex + 1)  + "/" + (((GridView)Container.NamingContainer).PageCount)%> '></asp:Label>
                                <asp:TextBox ID="TextBoxIndex" runat="server" Font-Names="Arial" Height="12px" Width="25px"
                                    ime-mode="disabled" onkeypress="if ((event.keyCode<48 || event.keyCode>57)) event.returnValue=false;"></asp:TextBox>
                                <asp:LinkButton ID="LinkButtonGo" runat="server" Font-Names="Arial" CommandName="go" ForeColor="#3366FF">GO</asp:LinkButton>
                            </PagerTemplate>
                            <PagerStyle HorizontalAlign="Center" />
                            <HeaderStyle Font-Names="Microsoft Yahei" ForeColor="#3366FF"/>
     </asp:GridView> 
    </div>
    </div>
<!--endprint-->

</asp:Content>

