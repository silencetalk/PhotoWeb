<%@ Page Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="DailyInform.aspx.cs" Inherits="WebPages_DailyInform" Title="Daily Inform" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <div id="SubTitle" class="SubTitle">
        <table style="width: 100%;">
            <tr>
                <td class="SubTitleTable">
                    Daily Inform
                </td>
                <td class="ToolsTable">
                    <div id="Tools" class="Tools">
                        <button id="PreDate" type="button" runat="server" onserverclick="PreDate_Click" class="ToolButton">&nbsp;<span style="font-family:fontawesome !important;">&#xf060;</span>&nbsp;</button>
                        <button id="SetDate" type="button" runat="server" onserverclick="SetDate_Click" class="ToolButton"><span style="font-family:fontawesome !important;">&#xf073;</span>&nbsp<asp:Label ID="LabelDate" runat="server" Text=""></asp:Label></button>
                        <button id="NextDate" type="button" runat="server" onserverclick="NextDate_Click" class="ToolButton">&nbsp;<span style="font-family:fontawesome !important;">&#xf061;</span>&nbsp;</button>
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
<div id="Add_Dialog" class="FormDiv">
<div class="FormTitle">Daily Inform</div>
<div class="FormInput">
<div class="FormBody">
<p>
    项目：<asp:DropDownList ID="DropDownListJitai" runat="server" CssClass="InputStyle">
    <asp:ListItem  Selected="True">请选择项目</asp:ListItem>
    <asp:ListItem>传送事项</asp:ListItem>
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
    <asp:ListItem>CDHT01</asp:ListItem>
    <asp:ListItem>CDHT02</asp:ListItem>
    <asp:ListItem>CDHT03</asp:ListItem>
    <asp:ListItem>CDHT04</asp:ListItem>
    <asp:ListItem>TPCD01</asp:ListItem>
    <asp:ListItem>TPCD02</asp:ListItem>
    <asp:ListItem>建设</asp:ListItem>
    </asp:DropDownList>
    日期：<asp:TextBox ID="TextBoxInputDate" runat="server" Width="68px" CssClass="TextboxStyle"></asp:TextBox>
    班次：<asp:DropDownList ID="DropDownListInputBanci" runat="server" CssClass="InputStyle">
        <asp:ListItem>夜班</asp:ListItem>
        <asp:ListItem>白班</asp:ListItem>
    </asp:DropDownList></p>
    <p><asp:Label ID="ToolTips" runat="server" Text=""></asp:Label></p>
    <p>内容：</p>
    <p><asp:TextBox ID="TextBoxDailyInform" runat="server" TextMode="MultiLine" Height="150px" Width="100%" style="margin:0;padding:0;" Font-Names="Microsoft Yahei" Font-Size="12px"></asp:TextBox></p>
    <p>工程师：</p>
    <p><asp:TextBox ID="TextBoxEng" runat="server" TextMode="MultiLine" Height="100px" Width="100%" style="margin:0;padding:0;" Font-Names="Microsoft Yahei" Font-Size="12px"></asp:TextBox></p>
    <p>附件：<asp:DropDownList ID="DropDownListFenlei" runat="server" CssClass="InputStyle">
    <asp:ListItem  Selected="True">请选择文档分类</asp:ListItem>
    <asp:ListItem>周报</asp:ListItem>
    <asp:ListItem>设备报告</asp:ListItem>
    <asp:ListItem>工艺报告</asp:ListItem>
    <asp:ListItem>测量数据</asp:ListItem>
    <asp:ListItem>培训资料</asp:ListItem>
    <asp:ListItem>事务资料</asp:ListItem>
    </asp:DropDownList> </p>
    <p>标题：<asp:TextBox ID="TextBoxFileTittle" runat="server" Width="100px" CssClass="TextboxStyle"></asp:TextBox>
                    <asp:FileUpload ID="FileUpload1" runat="server" Width="150px" CssClass="InputStyle"/>
    </p>
</div>
</div>
<div class="FormFooter">
     <asp:Button ID="Add_Dialog_OK" runat="server" Text="确定" CssClass="ConfirmButton" OnClick="AddDI_OK_Click"/>
     <asp:Button ID="Concel1" runat="server" Text="取消" CssClass="ConfirmButton" OnClick="Add_Cancel_Click"/>
</div>
</div>
</div>

<!--startprint-->
<div class="Content">
    <div class="DataView" style="text-align: center;">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            EnableModelValidation="True" Width="98%"
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
                                <asp:BoundField DataField="jitai" HeaderText="项目">
                                    <HeaderStyle Width="6%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="banci" HeaderText="班次">
                                    <HeaderStyle Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                 </asp:BoundField>
                                <asp:BoundField DataField="content" HeaderText="内容" HtmlEncode="False">
                                    <HeaderStyle Width="68%" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="eng" HeaderText="工程师" HtmlEncode="False">
                                    <HeaderStyle Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" />
                              </asp:BoundField>
                                <asp:TemplateField HeaderText="附件">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="fujian" runat="server" Text='<%#Eval("title") %>' Font-Size="Small" Visible='<%#Eval("fujian").ToString()==""?false:true%>' NavigateUrl='<%#"DownLoad.aspx?File="+Eval("fujian") %>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <HeaderStyle Width="8%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle Font-Names="微软雅黑" ForeColor="#3366FF"/>
     </asp:GridView> 
    </div>
    </div>
<!--endprint-->

</asp:Content>

