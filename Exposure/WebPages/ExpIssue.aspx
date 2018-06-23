<%@ Page Language="C#" MasterPageFile="~/Exposure/WebPages/MasterPage.master" AutoEventWireup="true" CodeFile="ExpIssue.aspx.cs" Inherits="Exposure_WebPages_ExpIssue" Title="设备履历" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div id="SubTitle" class="SubTitle">
        <table style="width: 100%;">
            <tr>
                <td class="SubTitleTable">
                    设备履历
                </td>
                <td class="ToolsTable">
                    <div id="Tools" class="Tools">
                        <button id="Add" type="button" runat="server" onserverclick="Add_Click" class="ToolButton">
                            <span style="font-family: fontawesome !important;">&#xf067;</span>&nbsp;添加</button>
                        <button id="Delete" type="button" runat="server" onserverclick="Delete_Click" class="ToolButton">
                            <span style="font-family: fontawesome !important;">&#xf014;</span>&nbsp;删除</button>
                        <button id="Edit" type="button" runat="server" onserverclick="Edit_Click" class="ToolButton">
                            <span style="font-family: fontawesome !important;">&#xf044;</span>&nbsp;编辑</button>
                        <button id="Reply" type="button" onserverclick="Reply_Click" class="ToolButton" runat="server">
                            <span style="font-family: fontawesome !important;">&#xf112;</span>&nbsp;回复进度</button>
                        <button id="Print" type="button" onclick="Print_Click()" class="ToolButton">
                            <span style="font-family: fontawesome !important;">&#xf02f;</span>&nbsp;打印</button>
                        <asp:TextBox ID="TextBoxSearch" runat="server" placeholder="关键词：" CssClass="ToolInput"></asp:TextBox>
                        <button id="Search" type="button" runat="server" onserverclick="Search_Click" class="ToolButton">
                            &nbsp;<span style="font-family: fontawesome !important;">&#xf002;</span>&nbsp;</button>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div style="position: relative; z-index: 100;">
        <div id="Add_Dialog" class="FormDiv">
            <div class="FormTitle">设备履历</div>
            <div class="FormInput">
                <div class="FormBody">
                    <p>
                        机台：
                        <asp:DropDownList ID="DD_Item" runat="server" CssClass="InputStyle">
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
                        </asp:DropDownList>
                    </p>
                    <p>
                        日期：<asp:TextBox ID="TextBoxInputDate" runat="server" Width="80px" CssClass="TextboxStyle"></asp:TextBox>
                    </p>
                    <p>
                        <asp:TextBox ID="TextBoxDailyInform" runat="server" TextMode="MultiLine" Height="200px"
                            Width="100%" Style="margin: 0; padding: 0; font-size: 12px; font-family: Microsoft Yahei;"></asp:TextBox>
                    </p>
                    <p>状态：<asp:DropDownList ID="DD_Status" runat="server" CssClass="InputStyle">
                            <asp:ListItem Selected="True">Open</asp:ListItem>
                            <asp:ListItem>Close</asp:ListItem>
                            <asp:ListItem>Monitor</asp:ListItem>
                        </asp:DropDownList>
                    </p>
                    <p>
                        附件：</p>
                    <p>
                        标题：<asp:TextBox ID="TextBoxFileTittle" runat="server" Width="200px" CssClass="TextboxStyle"></asp:TextBox></p>
                    <p>
                        <asp:DropDownList ID="DropDownListFenlei" runat="server" CssClass="InputStyle">
                            <asp:ListItem Selected="True">请选择文档分类</asp:ListItem>
                            <asp:ListItem>周报</asp:ListItem>
                            <asp:ListItem>设备报告</asp:ListItem>
                            <asp:ListItem>工艺报告</asp:ListItem>
                            <asp:ListItem>测量数据</asp:ListItem>
                            <asp:ListItem>培训资料</asp:ListItem>
                            <asp:ListItem>事务资料</asp:ListItem>
                        </asp:DropDownList>
                        <asp:FileUpload ID="FileUpload1" runat="server" CssClass="InputStyle" Width="200px" /></p>
                    <p>
                        <span style="font-family: fontawesome !important; color: Red;">&#xf071;</span>&nbsp;<asp:Label
                            ID="ToolTips1" runat="server" Text="字数请不要超过900字！"></asp:Label></p>
                </div>
            </div>
            <div class="FormFooter">
                <asp:Button ID="Add_Dialog_OK" runat="server" Text="确定" CssClass="ConfirmButton"
                    OnClick="AddDI_OK_Click" />
                <asp:Button ID="Concel1" runat="server" Text="取消" CssClass="ConfirmButton" OnClick="Add_Cancel_Click" />
            </div>
        </div>
        <div id="Add_Reply" class="FormDiv">
            <div class="FormTitle">设备履历</div>
            <div class="FormInput">
                <div class="FormBody">
                    <p>
                        回复：<asp:Label ID="LabelReply" runat="server" Text=""></asp:Label></p>
                    <p>
                        <asp:TextBox ID="TextBoxReply" runat="server" TextMode="MultiLine" Height="200px"
                            Width="100%" Style="margin: 0; padding: 0;" Font-Names="Microsoft Yahei" Font-Size="12px"></asp:TextBox></p>
                    <p>状态：<asp:DropDownList ID="DD_Reply_Status" runat="server" CssClass="InputStyle">
                            <asp:ListItem Selected="True">Open</asp:ListItem>
                            <asp:ListItem>Close</asp:ListItem>
                            <asp:ListItem>Monitor</asp:ListItem>
                        </asp:DropDownList>
                    </p>
                    <p>
                        附件：</p>
                    <p>
                        标题：<asp:TextBox ID="TextBoxFileTittle2" runat="server" Width="200px" CssClass="TextboxStyle"></asp:TextBox></p>
                    <p>
                        <asp:DropDownList ID="DropDownListFenlei2" runat="server" CssClass="InputStyle">
                            <asp:ListItem Selected="True">请选择文档分类</asp:ListItem>
                            <asp:ListItem>周报</asp:ListItem>
                            <asp:ListItem>设备报告</asp:ListItem>
                            <asp:ListItem>工艺报告</asp:ListItem>
                            <asp:ListItem>测量数据</asp:ListItem>
                            <asp:ListItem>培训资料</asp:ListItem>
                            <asp:ListItem>事务资料</asp:ListItem>
                        </asp:DropDownList>
                        <asp:FileUpload ID="FileUpload2" runat="server" CssClass="InputStyle" Width="200px" /></p>
                    <p>
                        <span style="font-family: fontawesome !important; color: Red;">&#xf071;</span>&nbsp;<asp:Label
                            ID="ToolTips2" runat="server" Text="字数请不要超过900字！"></asp:Label></p>
                </div>
            </div>
            <div class="FormFooter">
                <asp:Button ID="Add_Reply_OK" runat="server" Text="确定" CssClass="ConfirmButton" OnClick="AddReply_OK_Click" />
                <asp:Button ID="Button2" runat="server" Text="取消" CssClass="ConfirmButton" OnClick="Add_Cancel_Click" />
            </div>
        </div>
    </div>
    <!--startprint-->
    <div class="SubView">
        <div id="View" class="View">
            <p>
                <asp:Label ID="LabelForCount" runat="server" Text=""></asp:Label></p>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
                CssClass="gridtable" DataKeyNames="ID">
                <Columns>
                    <asp:TemplateField HeaderText="选择">
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="Server" /><br />
                            <asp:Label ID="LB_Data_EQ" runat="server"><%#Eval("jitai").ToString()%></asp:Label><br />
                            <asp:Label ID="LB_Data_Date" runat="server"><%#Eval("shortdate").ToString()%></asp:Label><br />
                        </ItemTemplate>
                        <HeaderStyle CssClass="gridtitle" Width="12%" />
                        <ItemStyle CssClass="griditemcenter" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="内容">
                        <ItemTemplate>
                            <%#Eval("content").ToString() %>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gridtitle" Width="40%" />
                        <ItemStyle CssClass="griditem" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="eng" HeaderText="工程师">
                        <HeaderStyle CssClass="gridtitle" Width="8%" />
                        <ItemStyle CssClass="griditemcenter" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="措施">
                        <ItemTemplate>
                            <%#Eval("reply").ToString() %>
                            <a class="moreinfo" runat="server" id="sb" onclick="$(this).next().slideToggle('fast');"
                                visible='<%#Eval("allreply").ToString() !=""?true:false%>'>more</a>
                            <div style="display: none; margin: 0; padding: 4px 0; border-top: solid 1px Gray">
                                <%#Eval("allreply").ToString()%>
                            </div>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gridtitle" Width="24%" />
                        <ItemStyle CssClass="griditem" ForeColor="Blue" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="附件">
                        <ItemTemplate>
                            <asp:HyperLink ID="fujian" runat="server" Text='<%#Eval("title") %>' Font-Size="Small"
                                Visible='<%#Eval("fujian").ToString()==""?false:true%>' NavigateUrl='<%#"DownLoad.aspx?File="+Eval("fujian") %>'></asp:HyperLink><br />
                            <asp:HyperLink ID="refujian" runat="server" Text='<%#Eval("retitle") %>' Font-Size="Small"
                                Visible='<%#Eval("refujian").ToString()==""?false:true%>' NavigateUrl='<%#"DownLoad.aspx?File="+Eval("refujian") %>'></asp:HyperLink>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gridtitle" Width="10%" />
                        <ItemStyle CssClass="griditemcenter" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="status" HeaderText="状态">
                        <HeaderStyle CssClass="gridtitle" Width="6%" />
                        <ItemStyle CssClass="griditemcenter" />
                    </asp:BoundField>
                </Columns>
                <HeaderStyle CssClass="gridtitle" Font-Names="微软雅黑" ForeColor="#3366FF" />
            </asp:GridView>
        </div>
    </div>
    <!--endprint-->

</asp:Content>

