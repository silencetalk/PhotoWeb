<%@ Page Language="C#" MasterPageFile="~/WebPages/MasterPage.master" AutoEventWireup="true"
    CodeFile="InformTask.aspx.cs" Inherits="WebPages_InformTask" Title="InformTask" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="SubTitle" class="SubTitle">
        <table style="width: 100%;">
            <tr>
                <td class="SubTitleTable">
                    Inform Task
                </td>
                <td class="ToolsTable">
                    <div id="Tools" class="Tools">
                        <button id="Add" type="button" runat="server" onserverclick="Add_Click" class="ToolButton">
                            <span style="font-family: fontawesome !important;">&#xf067;</span>&nbsp;添加</button>
                        <button id="Delete" type="button" runat="server" onserverclick="Delete_Click" onclick="javascript:if(confirm('确定要删除吗?')){}else{return false;}" class="ToolButton">
                            <span style="font-family: fontawesome !important;">&#xf014;</span>&nbsp;删除</button>
                        <button id="Edit" type="button" runat="server" onserverclick="Edit_Click" class="ToolButton">
                            <span style="font-family: fontawesome !important;">&#xf044;</span>&nbsp;编辑</button>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div style="position: relative; z-index: 100;">
        <div id="Add_Dialog" class="FormDiv">
            <div class="FormTitle">
                Inform Task</div>
            <div class="FormInput">
                <div class="FormBody">
                    <p>
                        机台：<asp:DropDownList ID="DD_Add_Item" runat="server" CssClass="InputStyle">
                            <asp:ListItem Selected="True">请选择项目/机台</asp:ListItem>
                            <asp:ListItem>工作日记</asp:ListItem>
                            <asp:ListItem>值班交接</asp:ListItem>
                            <asp:ListItem>膜厚测试</asp:ListItem>
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
                    </p>
                    <p>
                        日期：<asp:DropDownList ID="DD_Add_Time" runat="server" CssClass="InputStyle" AutoPostBack="true" OnSelectedIndexChanged="Time_Option_Change">
                            <asp:ListItem Selected="True">每天</asp:ListItem>                          
                            <asp:ListItem>每星期</asp:ListItem>
                            <asp:ListItem>每月</asp:ListItem>
                            <asp:ListItem>指定日期</asp:ListItem>
                        </asp:DropDownList>   
                        <asp:DropDownList ID="DD_Add_SubTime" runat="server" CssClass="InputStyle" Visible="false">
                        </asp:DropDownList>
                        <asp:TextBox ID="TB_Add_SubTime" runat="server" CssClass="InputStyle" Visible="false" onFocus="WdatePicker({isShowClear:false,readOnly:true,minDate:'%y/%M/%d'})"></asp:TextBox>
                    </p>
                    <p><asp:TextBox ID="TB_Add_Content" runat="server" TextMode="MultiLine" Height="200px" Width="100%" Style="margin: 0; padding: 0; font-size: 12px; font-family: Microsoft Yahei;"></asp:TextBox></p>
                    <div style="margin: 10px 0 0 0; padding: 0;">
                        <div class="status">
                            Status：</div>
                        <div class="switch">
                            <asp:Button ID="Open" runat="server" Text="Open" CssClass="open" OnClick="Open_Click" Visible="true" />
                            <asp:Button ID="Close" runat="server" Text="Close" CssClass="close" OnClick="Close_Click" Visible="false" />
                        </div>
                    </div>
                    <p><asp:Label ID="ToolTips" runat="server" Text="" Visible="false" ForeColor="Red"></asp:Label></p>
                </div>
            </div>
            <div class="FormFooter">
                <asp:Button ID="Add_Dialog_OK" runat="server" Text="确定" CssClass="ConfirmButton"
                    OnClick="Add_OK_Click" />
                <asp:Button ID="Add_Dialog_Concel" runat="server" Text="取消" CssClass="ConfirmButton"
                    OnClick="Add_Cancel_Click" />
            </div>
        </div>
    </div>
    <div class="View">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
            Width="100%" AllowPaging="True" PageSize="8" OnPageIndexChanging="GridView1_PageIndexChanging"
            OnRowCommand="GridView1_RowCommand" DataKeyNames="ID" CssClass="gridtable">
            <Columns>
                <asp:TemplateField HeaderText="项目">
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="Server" /><br />
                        <asp:Label ID="LB_Data_Item" runat="server"><%#Eval("Item").ToString()%></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="gridtitle" Width="100px" />
                    <ItemStyle CssClass="griditemcenter" />
                </asp:TemplateField>
                <asp:BoundField DataField="fullzhouqi" HeaderText="周期">
                    <HeaderStyle CssClass="gridtitle" Width="100px" />
                    <ItemStyle CssClass="griditemcenter" />
                </asp:BoundField>
                <asp:BoundField DataField="content" HeaderText="内容" HtmlEncode="False">
                    <HeaderStyle CssClass="gridtitle"/>
                    <ItemStyle CssClass="griditem" />
                </asp:BoundField>
                <asp:BoundField DataField="eng" HeaderText="工程师">
                    <HeaderStyle CssClass="gridtitle" Width="100px" />
                    <ItemStyle CssClass="griditemcenter" />
                </asp:BoundField>
                <asp:BoundField DataField="lastdate" HeaderText="最新添加日期">
                    <HeaderStyle CssClass="gridtitle" Width="100px" />
                    <ItemStyle CssClass="griditemcenter" />
                </asp:BoundField>
                <asp:BoundField DataField="nextdate" HeaderText="下次添加日期">
                    <HeaderStyle CssClass="gridtitle" Width="100px" />
                    <ItemStyle CssClass="griditemcenter" />
                </asp:BoundField>
                <asp:BoundField DataField="status" HeaderText="状态">
                    <HeaderStyle CssClass="gridtitle" Width="100px" />
                    <ItemStyle CssClass="griditemcenter" />
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
                    Height="18px" Width="40px" ForeColor="#3366FF" Text='<%# (((GridView)Container.NamingContainer).PageIndex + 1)  + "/" + (((GridView)Container.NamingContainer).PageCount)%> '></asp:Label>
                <asp:TextBox ID="TextBoxIndex" runat="server" Font-Names="Arial" Height="12px" Width="25px"
                    ime-mode="disabled" onkeypress="if ((event.keyCode<48 || event.keyCode>57)) event.returnValue=false;"></asp:TextBox>
                <asp:LinkButton ID="LinkButtonGo" runat="server" Font-Names="Arial" CommandName="go"
                    ForeColor="#3366FF">GO</asp:LinkButton>
            </PagerTemplate>
            <PagerStyle HorizontalAlign="Center" />
            <HeaderStyle Font-Names="微软雅黑" ForeColor="#3366FF" />
        </asp:GridView>
    </div>
</asp:Content>
