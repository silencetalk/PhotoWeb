<%@ Page Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="GongsiPerson.aspx.cs" Inherits="WebPages_GongsiPerson" Title="人员信息" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <div id="SubTitle" class="SubTitle">
        <table style="width: 100%;">
            <tr>
                <td class="SubTitleTable">
                    人员信息
                </td>
                <td class="ToolsTable">
                    <div id="Tools" class="Tools">
                        <button id="Add" type="button" runat="server" onserverclick="Add_Click" class="ToolButton"><span style="font-family:fontawesome !important;">&#xf067;</span>&nbsp;添加</button>
                        <button id="Delete" type="button" runat="server" onserverclick="Delete_Click" class="ToolButton"><span style="font-family:fontawesome !important;">&#xf014;</span>&nbsp;删除</button>
                        <button id="Edit" type="button" runat="server" onserverclick="Edit_Click" class="ToolButton"><span style="font-family:fontawesome !important;">&#xf044;</span>&nbsp;编辑</button>
                        <button id="Print" type="button" onclick="Print_Click()" class="ToolButton"><span style="font-family:fontawesome !important;">&#xf02f;</span>&nbsp;打印</button>
                        <asp:TextBox ID="TextBoxSearch" runat="server" placeholder="关键词：" CssClass="ToolInput"></asp:TextBox>
                        <button id="Search" type="button" runat="server" onserverclick="Search_Click" class="ToolButton"><span style="font-family:fontawesome !important;">&#xf002;</span>&nbsp;搜索</button>
                    </div>
                </td>
            </tr>
        </table>
</div>

<div style="position:relative; z-index:100;">

<div id="Add_Dialog" class="FormDiv" style="width:320px;left:328px;">
<div class="FormTitle">添加公司人员信息</div>
<div class="FormInput">
<div class="FormBody">
<p>姓名：<asp:TextBox ID="TextBoxName" runat="server" Width="100px" CssClass="TextboxStyle"></asp:TextBox></p>
<p>工号：<asp:TextBox ID="TextBoxNum" runat="server" Width="100px" CssClass="TextboxStyle"></asp:TextBox></p>
<p>部门：<asp:TextBox ID="TextBoxBumen" runat="server" Width="100px" CssClass="TextboxStyle"></asp:TextBox></p>
<p>科室：<asp:TextBox ID="TextBoxKeshi" runat="server" Width="100px" CssClass="TextboxStyle"></asp:TextBox></p>
<p>业务：<asp:TextBox ID="TextBoxYewu" runat="server" Width="200px" CssClass="TextboxStyle"></asp:TextBox></p>
<p>电话：<asp:TextBox ID="TextBoxPhone" runat="server" Width="200px" CssClass="TextboxStyle"></asp:TextBox></p>
<p>邮箱：<asp:TextBox ID="TextBoxemail" runat="server" Width="200px" CssClass="TextboxStyle"></asp:TextBox></p>
 <p><asp:Label ID="ToolTips" runat="server"></asp:Label></p>
</div>
</div>
<div class="FormFooter">
     <asp:Button ID="Add_Dialog_OK" runat="server" Text="确定" CssClass="ConfirmButton" OnClick="Add_OK_Click"/>
     <asp:Button ID="Concel1" runat="server" Text="取消" CssClass="ConfirmButton" OnClick="Add_Cancel_Click"/>
</div>
</div>
</div>

<!--startprint-->
    <div class="SubMenu">
    <asp:Button ID="UserInfo" runat="server" Text="科室人员" OnClick="Keshi_Click" CssClass="MenuButton" />
    <asp:Button ID="Gongsi" runat="server" Text="公司人员"  CssClass="MenuSelected"/>
    <asp:Button ID="Changshang" runat="server" Text="厂商人员"  OnClick="Changshang_Click" CssClass="MenuButton"/>
    </div>
    <div class="SubView">
  <%--  <button id="fullscreen" onclick="fullscreenclick()"><span style="font-family:fontawesome !important;">&#xf0b2;</span></button>--%>
    <div id="View" class="View">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            EnableModelValidation="True" Width="100%" AllowPaging="True"  PageSize="20"
            OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCommand="GridView1_RowCommand"
            BorderColor="#A0A0A0" BorderStyle="Solid"   BorderWidth="1px" DataKeyNames="ID">
                            <Columns>
                              <asp:TemplateField HeaderText="选择">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="Server" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="8%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="pname" HeaderText="姓名">
                                    <HeaderStyle Width="8%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="num" HeaderText="工号">
                                    <HeaderStyle Width="8%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="bumen" HeaderText="部门">
                                    <HeaderStyle Width="8%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="keshi" HeaderText="科室">
                                    <HeaderStyle Width="8%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                               <asp:BoundField DataField="yewu" HeaderText="业务">
                                    <HeaderStyle Width="20%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>                                      
                                <asp:BoundField DataField="phone" HeaderText="电话">
                                    <HeaderStyle Width="20%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="email" HeaderText="邮箱">
                                    <HeaderStyle Width="20%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
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
                                    Height="16px" Width="40px" ForeColor="#3366FF" Text='<%# (((GridView)Container.NamingContainer).PageIndex + 1)  + "/" + (((GridView)Container.NamingContainer).PageCount)%> '></asp:Label>
                                <asp:TextBox ID="TextBoxIndex" runat="server" Font-Names="Arial" Height="12px" Width="25px"
                                    ime-mode="disabled" onkeypress="if ((event.keyCode<48 || event.keyCode>57)) event.returnValue=false;"></asp:TextBox>
                                <asp:LinkButton ID="LinkButtonGo" runat="server" Font-Names="Arial" CommandName="go" ForeColor="#3366FF">GO</asp:LinkButton>
                            </PagerTemplate>
                            <PagerStyle HorizontalAlign="Center" />
                            <HeaderStyle Font-Names="微软雅黑" ForeColor="#3366FF"/>
     </asp:GridView> 
    </div>
    </div>
<!--endprint-->
</asp:Content>

