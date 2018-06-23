<%@ Page Title="资料管理" Language="C#" MasterPageFile="~/WebPages/MasterPage.master" AutoEventWireup="true" CodeFile="Files.aspx.cs" Inherits="WebPages_Files" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <div id="SubTitle" class="SubTitle">
        <table style="width: 100%;">
            <tr>
                <td class="SubTitleTable">
                    资料管理
                    <button id="Add" type="button" runat="server" onserverclick="Add_Click" class="HideButton"></button>
                </td>
                <td class="ToolsTable">
                    <div id="Tools" class="Tools">
                       <%-- <button id="Add" type="button" runat="server" onserverclick="Add_Click" class="ToolButton"><span style="font-family:fontawesome !important;">&#xf067;</span>&nbsp;添加</button>--%>
                        <button id="Delete" type="button" runat="server" onserverclick="Delete_Click" class="ToolButton">
                            <span style="font-family:fontawesome !important;">&#xf014;</span>&nbsp;删除</button>
                        <button id="Edit" type="button" onserverclick="Edit_Click" class="ToolButton" runat="server">
                            <span style="font-family:fontawesome !important;">&#xf044;</span>&nbsp;编辑</button>
                         <button id="Print" type="button" onclick="Print_Click()" class="ToolButton">
                            <span style="font-family:fontawesome !important;">&#xf02f;</span>&nbsp;打印</button>
                        <asp:TextBox ID="TextBoxSearch" runat="server" onserverclick="SearchBox_Click" placeholder="关键词：" CssClass="ToolInput"></asp:TextBox>
                        <button id="Search" type="button" runat="server" onserverclick="Search_Click" class="ToolButton">
                            &nbsp;<span style="font-family:fontawesome !important;">&#xf002;</span>&nbsp;</button>
                    </div>
                </td>
            </tr>
        </table>
    </div>

<div style="position:relative; z-index:100;">

<div id="Add_Dialog" class="FormDiv">
<div class="FormTitle">文档上传</div>
<div class="FormInput">
<div class="FormBody">

    <p>&nbsp;<asp:Label ID="ToolTips" runat="server"></asp:Label></p>
    <p>附件：
    <asp:DropDownList ID="DropDownListFenlei" runat="server" CssClass="InputStyle">
    <asp:ListItem  Selected="True">请选择文档分类</asp:ListItem>
    <asp:ListItem>周报</asp:ListItem>
    <asp:ListItem>设备报告</asp:ListItem>
    <asp:ListItem>工艺报告</asp:ListItem>
    <asp:ListItem>测量数据</asp:ListItem>
    <asp:ListItem>培训资料</asp:ListItem>
    <asp:ListItem>事务资料</asp:ListItem>
    </asp:DropDownList> 
    <asp:FileUpload ID="FileUpload1" runat="server" Width="200px" CssClass="InputStyle"/></p> 
    <p>&nbsp;</p>
</div>
</div>
<div class="FormFooter">
     <asp:Button ID="Add_Dialog_OK" runat="server" Text="确定" CssClass="ConfirmButton" OnClick="Add_OK_Click"/>
     <asp:Button ID="Concel1" runat="server" Text="取消" CssClass="ConfirmButton" OnClick="Add_Cancel_Click"/>
</div>
</div>
</div>

 <!--startprint-->  
    <div class="Content">
                    <div class="View" style="text-align: center;">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
                            Width="100%" AllowPaging="True" PageSize="10" OnPageIndexChanging="GridView1_PageIndexChanging"
                            OnRowCommand="GridView1_RowCommand" BorderColor="#A0A0A0"
                            BorderStyle="Solid" BorderWidth="1px" DataKeyNames="ID">
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
                                 <asp:BoundField DataField="fenlei" HeaderText="文档类型">
                                    <HeaderStyle Width="8%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                               <asp:BoundField DataField="filename" HeaderText="文件名" HtmlEncode="false">
                                    <HeaderStyle Width="55%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="eng" HeaderText="工程师" HtmlEncode="false">
                                    <HeaderStyle Width="8%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                 <asp:TemplateField HeaderText="下载">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="file" runat="server" Font-Size="Small"  NavigateUrl='<%#"DownLoad.aspx?File="+Eval("path") %>' CssClass="fontlink" ><span style="font-family:fontawesome !important; font-size:20px;">&#xf0ed;</span></asp:HyperLink>
                                    </ItemTemplate>
                                    <HeaderStyle Width="5%" />
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

