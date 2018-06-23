<%@ Page Language="C#" MasterPageFile="~/WebPages/MasterPage.master" AutoEventWireup="true" CodeFile="CST.aspx.cs" Inherits="WebPages_Alarm" Title="CST" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div style="position:relative; z-index:100;">
<div id="Add_Dialog" class="FormDiv">
<div class="FormTitle">CST</div>
<div class="FormInput">
<div class="FormBody">
    <p>
    CST ID：<asp:TextBox ID="TextBoxCSTID" runat="server" Width="70px" CssClass="TextboxStyle"></asp:TextBox></p>
    <p><asp:Label ID="ToolTips" runat="server"></asp:Label></p>
    <p>基板信息：</p>
    <p><asp:TextBox ID="TextBoxGlass" runat="server" TextMode="MultiLine" Height="100px" Width="100%" style="margin:0;padding:0;" Font-Names="Microsoft Yahei" Font-Size="12px"></asp:TextBox></p>
    <p>备注：</p>
    <p><asp:TextBox ID="TextBoxBeizhu" runat="server" TextMode="MultiLine" Height="100px" Width="100%" style="margin:0;padding:0;" Font-Names="Microsoft Yahei" Font-Size="12px"></asp:TextBox></p>    
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
                    CST
                </td>
                <td class="ToolsTable">
                    <div id="Tools" class="Tools">
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
      <a href="PM.aspx" class="MenuButton">PM</a>
        <a href="Parts.aspx" class="MenuButton">Parts</a>
        <a href="CST.aspx" class="MenuButton MenuSelected">CST</a>
        <a href="AllItem.aspx" class="MenuButton">平行展开项目</a>
    </div>
    <div class="SubView">
    <div class="View">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            EnableModelValidation="True" Width="100%" AllowPaging="True"  PageSize="20"
                            OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCommand="GridView1_RowCommand"
                            BorderColor="#A0A0A0" BorderStyle="Solid" 
            BorderWidth="1px" DataKeyNames="ID">
                            <Columns>
                                <asp:TemplateField HeaderText="选择">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="Server" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="8%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="cstid" HeaderText="CST" HtmlEncode="False">
                                    <HeaderStyle Width="8%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="glass" HeaderText="Glass" HtmlEncode="False">
                                    <HeaderStyle Width="38%" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="beizhu" HeaderText="备注" HtmlEncode="False">
                                    <HeaderStyle Width="38%" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                               <asp:BoundField DataField="shortdate" HeaderText="日期" HtmlEncode="False">
                                    <HeaderStyle Width="8%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>

                                <asp:TemplateField></asp:TemplateField>

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
                            <HeaderStyle Font-Names="微软雅黑" ForeColor="#3366FF"/>
     </asp:GridView> 
     
    </div>
    </div>
<!--endprint-->

</asp:Content>

