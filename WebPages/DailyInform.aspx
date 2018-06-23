<%@ Page Language="C#" MasterPageFile="~/WebPages/MasterPage.master" AutoEventWireup="true"
    CodeFile="DailyInform.aspx.cs" Inherits="WebPages_DailyInform" Title="日常交接" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="SubTitle" class="SubTitle">
        <table style="width: 100%;">
            <tr>
                <td class="SubTitleTable">
                    工作交接
                </td>
                <td class="ToolsTable">
                    <div id="Tools" class="Tools">
                        <button id="PreDate" type="button" runat="server" onserverclick="PreDate_Click" class="ButtonCycle">
                            <span style="font-family: fontawesome !important;">&#xf060;</span></button>
                        <button id="SetDate" type="button" runat="server" onserverclick="SetDate_Click" class="ToolButton">
                            <span style="font-family: fontawesome !important;">&#xf073;</span>&nbsp;<asp:Label
                                ID="LabelDate" runat="server" Text=""></asp:Label></button>
                        <button id="NextDate" type="button" runat="server" onserverclick="NextDate_Click"
                            class="ButtonCycle">
                            <span style="font-family: fontawesome !important;">&#xf061;</span></button>
                        <button id="Add" type="button" runat="server" onserverclick="Add_Click" class="ToolButton">
                            <span style="font-family: fontawesome !important;">&#xf067;</span>&nbsp;添加</button>
                        <button id="Task" runat="server" onserverclick="Task_Click" class="ToolButton" onclick=""><i class="icon-tasks"></i>任务</button>
                        <button id="Delete" type="button" runat="server" onserverclick="Delete_Click" class="ToolButton">
                            <span style="font-family: fontawesome !important;">&#xf014;</span>&nbsp;删除</button>
                        <button id="Edit" type="button" runat="server" onserverclick="Edit_Click" class="ToolButton">
                            <span style="font-family: fontawesome !important;">&#xf044;</span>&nbsp;编辑</button>
                        <button id="Reply" type="button" onserverclick="Reply_Click" class="ToolButton" runat="server">
                            <span style="font-family: fontawesome !important;">&#xf112;</span>&nbsp;回复</button>
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
            <div class="FormTitle">
                日常交接</div>
            <div class="FormInput">
                <div class="FormBody">
                    <p>



                            <%--                    项目：<asp:DropDownList ID="DD_Item" runat="server" CssClass="InputStyle" AutoPostBack="true" OnSelectedIndexChanged="Item_Selected">--%>
                                    项目：<asp:DropDownList ID="DD_Item" runat="server" CssClass="InputStyle">
                                        <asp:ListItem Selected="True">请选择项目</asp:ListItem>
                                        <asp:ListItem>值班交接</asp:ListItem>
                                        <asp:ListItem>工作日记</asp:ListItem>
            <%--                        <asp:ListItem>PM待办事项</asp:ListItem>--%>
                                        <asp:ListItem>工艺测试</asp:ListItem>
                                    </asp:DropDownList>
            


 <%--                      <asp:Label ID="LB_EQ" runat="server" Text="机台："></asp:Label>
                        <asp:DropDownList ID="DD_EQ" runat="server" CssClass="InputStyle">
                            <asp:ListItem Selected="True">All</asp:ListItem>
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
                        </asp:DropDownList>--%>
                    </p>
                    <p>
                        日期：<asp:TextBox ID="TextBoxInputDate" runat="server" Width="80px" CssClass="TextboxStyle"></asp:TextBox>
                        班次：<asp:DropDownList ID="DropDownListInputBanci" runat="server" CssClass="InputStyle">
                            <asp:ListItem>夜班</asp:ListItem>
                            <asp:ListItem>白班</asp:ListItem>
                        </asp:DropDownList>
                    </p>
                    <p>
                        <asp:TextBox ID="TextBoxDailyInform" runat="server" TextMode="MultiLine" Height="200px"
                            Width="100%" Style="margin: 0; padding: 0; font-size: 12px; font-family: Microsoft Yahei;"></asp:TextBox></p>
                    <div style="margin: 10px 0px 0px 0px; padding: 0px;">
                        <div class="status">
                            Status：</div>
                        <div class="switch">
                            <asp:Button ID="Open" runat="server" Text="Open" CssClass="open" OnClick="Open_Click"
                                Visible="false" />
                            <asp:Button ID="Close" runat="server" Text="Close" CssClass="close" OnClick="Close_Click"
                                Visible="true" /></div>
                    </div>
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
            <div class="FormTitle">
                日常交接</div>
            <div class="FormInput">
                <div class="FormBody">
                    <p>
                        回复：<asp:Label ID="LabelReply" runat="server" Text=""></asp:Label></p>
                    <p>
                        <asp:TextBox ID="TextBoxReply" runat="server" TextMode="MultiLine" Height="200px"
                            Width="100%" Style="margin: 0; padding: 0;" Font-Names="Microsoft Yahei" Font-Size="12px"></asp:TextBox></p>
                    <div style="margin: 10px 0px 0px 0px; padding: 0px;">
                        <div class="status">
                            Status：</div>
                        <div class="switch">
                            <asp:Button ID="Open1" runat="server" Text="Open" CssClass="open" OnClick="Open_Reply_Click"
                                Visible="false" />
                            <asp:Button ID="Close1" runat="server" Text="Close" CssClass="close" OnClick="Close_Reply_Click"
                                Visible="true" /></div>
                    </div>
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
        <div id="message_dlg" class="FormDiv">
            <div class="FormTitle">
                信息</div>
            <div class="FormInput">
                <div class="FormBody">
                    <asp:Label ID="Label_Message" runat="server" Text="" Font-Size="16px"></asp:Label>
                </div>
            </div>
            <div class="FormFooter">
                <asp:Button ID="Message_Confirm" runat="server" Text="确定" CssClass="ConfirmButton"
                    PostBackUrl="PM.aspx" />
                <button id="message_cancel" class="ConfirmButton" onclick="HideDialog('message_dlg');">
                    关闭</button>
            </div>
        </div>
        
        <div id="alarm_dlg" class="FormDiv">
            <div class="FormTitle">
                Alarm
            </div>
            <div class="FormInput">
                <div class="FormBody">
                    <asp:Label ID="Label_Alarm_Info" runat="server" Text=""></asp:Label>
                </div>
            </div>
            <div class="FormFooter">
                <button id="Alarm_Cancel" class="ConfirmButton" onclick="HideDialog('alarm_dlg');">关闭</button>
            </div>
        </div>
        
    </div>
    <!--startprint-->
    <div class="SubMenu">
        <asp:Button ID="Jiaojie" runat="server" Text="日常交接" CssClass="MenuSelected" />
        <asp:Button ID="personal" runat="server" Text="个人交接" OnClick="personal_Click" CssClass="MenuButton"/>
        <asp:Button ID="Genren" runat="server" Text="待办事项" OnClick="Geren_Click" CssClass="MenuButton" />
        <asp:Button ID="Work" runat="server" Text="工作安排" OnClick="Work_Click" CssClass="MenuButton" />
    </div>
    <%--  <button id="fullscreen" onclick="fullscreenclick()"><span style="font-family:fontawesome !important;">&#xf0b2;</span></button>--%>
    <div class="SubView">
        <div id="View" class="View">
            <p>
                <asp:Label ID="LabelForCount" runat="server" Text=""></asp:Label></p>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
                CssClass="gridtable" DataKeyNames="ID,alarmid" OnRowCommand="GridView1_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="选择">
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="Server" /><br />
                            <asp:Label ID="LB_Data_EQ" runat="server"><%#Eval("jitai").ToString()%></asp:Label><br />
                            <asp:Label ID="LB_Data_Date" runat="server"><%#Eval("shortdate").ToString()%></asp:Label><br />
                            <asp:Label ID="LB_Data_Banci" runat="server"><%#Eval("banci").ToString()%></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gridtitle" Width="12%" />
                        <ItemStyle CssClass="griditemcenter" />
                    </asp:TemplateField>
                    <%--<asp:BoundField DataField="content" HeaderText="内容" HtmlEncode="False">
                        <HeaderStyle CssClass="gridtitle" Width="40%" />
                        <ItemStyle CssClass="griditem" />
                    </asp:BoundField>--%>

                    <asp:TemplateField HeaderText="内容">
                        <ItemTemplate>
                            <%#Eval("content").ToString() %>
                            <asp:LinkButton ID="AlarmLink" runat="server" CommandName="Alarm" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' visible='<%#Eval("alarmname").ToString() !=""?true:false%>'><br /><b>Alarm：</b><%#Eval("alarmname")%></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gridtitle" Width="40%" />
                        <ItemStyle CssClass="griditem" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="eng" HeaderText="工程师">
                        <HeaderStyle CssClass="gridtitle" Width="8%" />
                        <ItemStyle CssClass="griditemcenter" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="回复">
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
