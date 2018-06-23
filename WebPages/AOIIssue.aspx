<%@ Page Language="C#" MasterPageFile="~/WebPages/MasterPage.master" AutoEventWireup="true"
    CodeFile="AOIIssue.aspx.cs" Inherits="WebPages_AOIIssue" Title="AOI Issue" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    </div>
    <div style="position: relative; z-index: 100;">
        <div id="Add_Dialog" class="FormDiv">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="FormTitle">AOI Issue</div>
                    <div class="FormInput">
                        <div class="FormBody">
                            <p>
                                <asp:Label ID="ToolTips" runat="server" ForeColor="Red"></asp:Label></p>
                            <p>
                                机台：<asp:DropDownList ID="DD_EQ" runat="server" CssClass="InputStyle">
                                        <asp:ListItem Selected="True">请选择机台</asp:ListItem>
                                        <asp:ListItem>PH01</asp:ListItem>
                                        <asp:ListItem>PH02</asp:ListItem>
                                        <asp:ListItem>PH03</asp:ListItem>
                                        <asp:ListItem>PH04</asp:ListItem>
                                        <asp:ListItem>PH05</asp:ListItem>
                                        <asp:ListItem>PH06</asp:ListItem>
                                        <asp:ListItem>PH07</asp:ListItem>
                                        <asp:ListItem>PH08</asp:ListItem>
                                        <asp:ListItem>PH09</asp:ListItem>
                                        <asp:ListItem>PH10</asp:ListItem>
                                        <asp:ListItem>PH11</asp:ListItem>
                                        <asp:ListItem>PH12</asp:ListItem>
                                        <asp:ListItem>PH13</asp:ListItem>
                                        <asp:ListItem>PH14</asp:ListItem>
                                        <asp:ListItem>PH15</asp:ListItem>
                                        <asp:ListItem>PH16</asp:ListItem>
                                        <asp:ListItem>PH17</asp:ListItem>
                                        <asp:ListItem>PH18</asp:ListItem>
                                        <asp:ListItem>PH19</asp:ListItem>
                                        <asp:ListItem>PH20</asp:ListItem>
                                        <asp:ListItem>HBT RONS</asp:ListItem>
                                        <asp:ListItem>FAVIT YMS</asp:ListItem>
                                </asp:DropDownList>
                            </p>
                            <p>
                                原因：<asp:DropDownList ID="DD_Reasons" runat="server" CssClass="InputStyle">
                                        <asp:ListItem Selected="True">请选择原因</asp:ListItem>
                                        <asp:ListItem>IP Fail</asp:ListItem>
                                        <asp:ListItem>PLC ERROR</asp:ListItem>
                                        <asp:ListItem>Alignment Fail</asp:ListItem>
                                        <asp:ListItem>Recipe Error</asp:ListItem>
                                        <asp:ListItem>Rons Server Down</asp:ListItem>
                                        <asp:ListItem>控制PC Down</asp:ListItem>
                                        <asp:ListItem>Uploader Fail</asp:ListItem>
                                        <asp:ListItem>YMS Server Down</asp:ListItem>
                                        <asp:ListItem>通信异常</asp:ListItem>
                                </asp:DropDownList>
                            </p>
                            <p>
                                发生时间：<asp:TextBox ID="TB_Date" runat="server" Width="160px" CssClass="TextboxStyle" onFocus="WdatePicker({isShowClear:false,readOnly:true,dateFmt:'yyyy/MM/dd HH:mm'})" Text=""></asp:TextBox>
                            </p>
                            <p>
                                Down机时间(Hours)：<asp:TextBox ID="TB_Hours" runat="server" Width="80px" CssClass="TextboxStyle" Style="ime-mode: disabled" onkeypress="if ((event.keyCode>=48 && event.keyCode<=57)||(event.keyCode==46)) {event.returnValue=true;}else{event.returnValue=false;}"></asp:TextBox></p>
                            <p>
                                备注：</p>
                            <p>
                                <asp:TextBox ID="TB_Beizhu" runat="server" TextMode="MultiLine" Height="100px" Width="100%" Style="margin: 0; padding: 0;" Font-Names="Microsoft Yahei" Font-Size="12px"></asp:TextBox></p>
                        </div>
                    </div>
                    <div class="FormFooter">
                        <asp:Button ID="Add_Dialog_OK" runat="server" Text="确定" CssClass="ConfirmButton"
                            OnClick="Add_OK_Click" />
                        <asp:Button ID="Concel1" runat="server" Text="取消" CssClass="ConfirmButton" OnClick="Add_Cancel_Click" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div id="SubTitle" class="SubTitle">
        <table style="width: 100%;">
            <tr>
                <td class="SubTitleTable">
                    AOI Issue
                </td>
                <td class="ToolsTable">
                    <div id="Tools" class="Tools">
                        <button id="Add" type="button" runat="server" onserverclick="Add_Click" class="ToolButton">
                            <span style="font-family: fontawesome !important;">&#xf067;</span>&nbsp;添加</button>
                        <button id="Delete" type="button" runat="server" onserverclick="Delete_Click" class="ToolButton">
                            <span style="font-family: fontawesome !important;">&#xf014;</span>&nbsp;删除</button>
                        <button id="Edit" type="button" runat="server" onserverclick="Edit_Click" class="ToolButton">
                            <span style="font-family: fontawesome !important;">&#xf044;</span>&nbsp;编辑</button>
                        <button id="Print" type="button" onclick="Print_Click()" class="ToolButton">
                            <span style="font-family: fontawesome !important;">&#xf02f;</span>&nbsp;打印</button>
                        <asp:TextBox ID="TextBoxSearch" runat="server" placeholder="关键词：" CssClass="ToolInput"></asp:TextBox>
                        <button id="Search" type="button" runat="server" onserverclick="Search_Click" class="SearchButton">&nbsp;<span style="font-family:fontawesome !important;">&#xf002;</span>&nbsp;</button>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <!--startprint-->
    <div class="View">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" EnableModelValidation="True" Width="100%" AllowPaging="True" PageSize="15" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCommand="GridView1_RowCommand" DataKeyNames="ID" CssClass="gridtable">
            <Columns>
                <asp:TemplateField HeaderText="选择">
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="Server" />
                    </ItemTemplate>
                    <HeaderStyle CssClass="gridtitle" Width="5%" />
                    <ItemStyle CssClass="griditemcenter" />
                </asp:TemplateField>
                <asp:BoundField DataField="eq" HeaderText="EQ">
                    <HeaderStyle CssClass="gridtitle" Width="10%" />
                    <ItemStyle CssClass="griditemcenter" />
                </asp:BoundField>
                <asp:BoundField DataField="downtime" HeaderText="Time" DataFormatString="{0:yyyy/MM/dd hh:mm}">
                    <HeaderStyle CssClass="gridtitle" Width="15%" />
                    <ItemStyle CssClass="griditemcenter" />
                </asp:BoundField>
                <asp:BoundField DataField="hours" HeaderText="Down(h)">
                    <HeaderStyle CssClass="gridtitle" Width="7%" />
                    <ItemStyle CssClass="griditemcenter" />
                </asp:BoundField>
                <asp:BoundField DataField="reason" HeaderText="Reason">
                    <HeaderStyle CssClass="gridtitle" Width="20%" />
                    <ItemStyle CssClass="griditemcenter" />
                </asp:BoundField>
                <asp:BoundField DataField="beizhu" HeaderText="备注" HtmlEncode="False">
                    <HeaderStyle CssClass="gridtitle" Width="35%" />
                    <ItemStyle CssClass="griditem" />
                </asp:BoundField>
                <asp:BoundField DataField="eng" HeaderText="工程师">
                    <HeaderStyle CssClass="gridtitle" Width="8%" />
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
    <!--endprint-->
</asp:Content>
