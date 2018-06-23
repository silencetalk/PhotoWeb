<%@ Page Title="首页" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true"
    CodeFile="Index.aspx.cs" Inherits="Index_Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="SubTitle" class="SubTitle">
        <table style="width: 100%;">
            <tr>
                <td class="SubTitleTable">
                    首页
                </td>
                <td class="ToolsTable">
                    <div id="Tools" class="Tools">
                        <button id="Add" type="button" runat="server" onserverclick="Add_Click" class="ToolButton"><span style="font-family:fontawesome !important;">&#xf067;</span>&nbsp;添加</button>
                        <button id="Delete" type="button" runat="server" onserverclick="Delete_Click" class="ToolButton">
                            <span style="font-family:fontawesome !important;">&#xf014;</span>&nbsp;删除</button>
                        <button id="Edit" type="button" onserverclick="Edit_Click" class="ToolButton" runat="server">
                            <span style="font-family:fontawesome !important;">&#xf044;</span>&nbsp;编辑</button>
                         <button id="Print" type="button" onclick="Print_Click()" class="ToolButton">
                            <span style="font-family:fontawesome !important;">&#xf02f;</span>&nbsp;打印</button>
                        <asp:TextBox ID="TextBoxSearch" runat="server" onserverclick="SearchBox_Click" placeholder="关键词：" CssClass="ToolInput"></asp:TextBox>
                        <button id="Search" type="button" runat="server" onserverclick="Search_Click" class="ToolButton">
                            <span style="font-family:fontawesome !important;">&#xf002;</span>&nbsp;搜索</button>
                    </div>
                </td>
            </tr>
        </table>
    </div>
<div style="position:relative; z-index:100;">
<div id="Add_Dialog" class="FormDiv">
 <div class="FormTitle">发表公告</div>
 <div class="FormInput">
 <div class="FormBody">  
  <p><asp:TextBox ID="TextBoxInput" runat="server" Height="200px" TextMode="MultiLine" Width="100%" style="margin:0;padding:0;"></asp:TextBox></p> 
  <p><span style="font-family:fontawesome !important; color:Red;">&#xf071;</span>&nbsp;<asp:Label ID="ToolTips" runat="server" Text="字数请不要超过900字！"></asp:Label></p>
 </div>
 </div>
 <div class="FormFooter">
     <asp:Button ID="AddGonggao" runat="server" Text="确定" CssClass="ConfirmButton" OnClick="AddConfirm_Click"/>
     <asp:Button ID="Concel" runat="server" Text="取消" CssClass="ConfirmButton"/>
 </div>
 </div>
 </div>    
 <!--startprint-->  
    <div class="Content">
                    <div class="DataTitle">部门公告</div>
                    <div class="DataView" style="text-align: center;">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
                            Width="98%" Style="margin: 0 auto;" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging"
                            OnRowCommand="GridView1_RowCommand" BorderColor="#A0A0A0"
                            BorderStyle="Solid" BorderWidth="1px" DataKeyNames="ID">
                            <Columns>
                                <asp:TemplateField HeaderText="选择">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="Server" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="8%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="Shortdate" HeaderText="日期">
                                    <HeaderStyle Width="12%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Content" HeaderText="内容" HtmlEncode="False">
                                    <HeaderStyle Width="70%" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Eng" HeaderText="宣导人">
                                    <HeaderStyle Width="10%" />
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
                                    Height="14px" Width="40px" ForeColor="#3366FF" Text='<%# (((GridView)Container.NamingContainer).PageIndex + 1)  + "/" + (((GridView)Container.NamingContainer).PageCount)%> '></asp:Label>
                                <asp:TextBox ID="TextBoxIndex" runat="server" Font-Names="Arial" Height="12px" Width="25px"
                                    ime-mode="disabled" onkeypress="if ((event.keyCode<48 || event.keyCode>57)) event.returnValue=false;"></asp:TextBox>
                                <asp:LinkButton ID="LinkButtonGo" runat="server" Font-Names="Arial" CommandName="go"
                                    ForeColor="#3366FF">GO</asp:LinkButton>
                            </PagerTemplate>
                            <PagerStyle HorizontalAlign="Center" />
                            <HeaderStyle Font-Names="微软雅黑" ForeColor="#3366FF"/>
                        </asp:GridView>
                    </div>
    </div>
 <!--endprint-->
</asp:Content>
