<%@ Page Title="Alarm" Language="C#" MasterPageFile="~/WebPages/MasterPage.master"
    AutoEventWireup="true" CodeFile="Alarm.aspx.cs" Inherits="WebPages_Alarm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="position: relative; z-index: 100;">                                                         
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
        <div id="inform_dlg" class="FormDiv">
            <div class="FormTitle">
                交接</div>
            <div class="FormInput">
                <div class="FormBody">
                    <p><asp:Label ID="Label_Inform" runat="server" Text="" Font-Bold="true"></asp:Label></p>
                    <p><asp:TextBox ID="TB_Add_Inform" runat="server" TextMode="MultiLine" Height="100px" Width="100%" Style="margin: 0; padding: 0;" Font-Names="Microsoft Yahei" Font-Size="12px"></asp:TextBox></p>
                    <p>*请填写关于该Alarm要交接的内容，例如：请白班跟进此报警，并检查LC是否有异常</p>
                </div>
            </div>
            <div class="FormFooter">
                <asp:Button ID="Inform_Confirm" runat="server" Text="确定" CssClass="ConfirmButton" OnClick="Inform_OK_Click"/>
                <asp:Button ID="Inform_Cancel" runat="server" Text="取消" CssClass="ConfirmButton" OnClick="Inform_Cancel_Click"/>
            </div>
        </div>
        <div id="Add_Dialog" class="FormDiv">
            <div class="FormTitle">
                设备履历</div>
            <div class="FormInput">
                <div class="FormBody">
                    <p>
                        <asp:Label ID="ToolTips" runat="server"></asp:Label></p>
                    <p>
                        机台：<asp:DropDownList ID="DropDownListJitai" runat="server" CssClass="InputStyle">
                            <asp:ListItem Selected="True">请选择机台</asp:ListItem>
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
                        单元：<asp:DropDownList ID="DropDownListDanyuan" runat="server" CssClass="InputStyle">
                            <asp:ListItem Selected="True">请选择单元</asp:ListItem>
                            <asp:ListItem>Indexer</asp:ListItem>
                            <asp:ListItem>Cleaner</asp:ListItem>
                            <asp:ListItem>DB</asp:ListItem>
                            <asp:ListItem>LC</asp:ListItem>
                            <asp:ListItem>VCD</asp:ListItem>
                            <asp:ListItem>SB</asp:ListItem>
                            <asp:ListItem>IF Buffer</asp:ListItem>
                            <asp:ListItem>DEV</asp:ListItem>
                            <asp:ListItem>HB</asp:ListItem>
                            <asp:ListItem>Bypass</asp:ListItem>
                            <asp:ListItem>OVEN</asp:ListItem>
                            <asp:ListItem>AOI</asp:ListItem>
                            <asp:ListItem>DCS</asp:ListItem>
                            <asp:ListItem>Dry Pump</asp:ListItem>
                        </asp:DropDownList>
                    </p>
                    <p>
                        发生时间：<asp:TextBox ID="TextBoxDownTime" runat="server" Width="185px" CssClass="TextboxStyle"></asp:TextBox></p>
                    <p>
                        Down机时间(Hours)：<asp:TextBox ID="TextBoxHours" runat="server" Width="120px" CssClass="TextboxStyle"
                            Style="ime-mode: disabled" onkeypress="if ((event.keyCode>=48 && event.keyCode<=57)||(event.keyCode==46)) {event.returnValue=true;}else{event.returnValue=false;}"></asp:TextBox></p>
                    <p>
                        Alarm：<asp:TextBox ID="TextBoxAlarm" runat="server" Width="200px" CssClass="TextboxStyle"></asp:TextBox></p>
                    <p>
                        <asp:CheckBox ID="CB_Add_Status" runat="server" Text="为该Alarm添加交接" /></p>
                    <p>
                        现象及分析：</p>
                    <p>
                        <asp:TextBox ID="TextBoxFenxi" runat="server" TextMode="MultiLine" Height="100px"
                            Width="100%" Style="margin: 0; padding: 0;" Font-Names="Microsoft Yahei" Font-Size="12px"></asp:TextBox></p>
                    <p>
                        Action：</p>
                    <p>
                        <asp:TextBox ID="TextBoxAction" runat="server" TextMode="MultiLine" Height="100px"
                            Width="100%" Style="margin: 0; padding: 0;" Font-Names="Microsoft Yahei" Font-Size="12px"></asp:TextBox></p>
                    <p>
                        影响：</p>
                    <p>
                        <asp:TextBox ID="TextBoxYingxiang" runat="server" TextMode="MultiLine" Height="100px"
                            Width="100%" Style="margin: 0; padding: 0;" Font-Names="Microsoft Yahei" Font-Size="12px"></asp:TextBox></p>
                    <p>
                        附件：<asp:DropDownList ID="DropDownListFenlei" runat="server" CssClass="InputStyle">
                            <asp:ListItem Selected="True">设备报告</asp:ListItem>
                            <asp:ListItem>周报</asp:ListItem>
                            <asp:ListItem>工艺报告</asp:ListItem>
                            <asp:ListItem>测量数据</asp:ListItem>
                            <asp:ListItem>培训资料</asp:ListItem>
                            <asp:ListItem>事务资料</asp:ListItem>
                        </asp:DropDownList>
                        标题：<asp:TextBox ID="TextBoxFileTittle" runat="server" Width="100px" CssClass="TextboxStyle"></asp:TextBox></p>
                    <p>
                        <asp:FileUpload ID="FileUpload1" runat="server" Width="265px" CssClass="InputStyle" /></p>
                </div>
            </div>
            <div class="FormFooter">
                <asp:Button ID="Add_Dialog_OK" runat="server" Text="确定" CssClass="ConfirmButton"
                    OnClick="Add_OK_Click" />
                <asp:Button ID="Concel1" runat="server" Text="取消" CssClass="ConfirmButton" OnClick="Add_Cancel_Click" />
            </div>
        </div>
    </div>
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
                        <button id="Print" type="button" onclick="Print_Click()" class="ToolButton">
                            <span style="font-family: fontawesome !important;">&#xf02f;</span>&nbsp;打印</button>
                        <%-- <asp:TextBox ID="TextBoxSearch" runat="server" placeholder="关键词：" CssClass="ToolInput"></asp:TextBox>
                        <button id="Search" type="button" runat="server" onserverclick="Search_Click" class="SearchButton">&nbsp;<span style="font-family:fontawesome !important;">&#xf002;</span>&nbsp;</button>--%>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <!--startprint-->
         <div class="SubMenu">
     <asp:Button ID="Alarm" runat="server" Text="Alarm" OnClick="Alarm_Click" CssClass="MenuSelected"/>
     <asp:Button ID="Defect" runat="server" Text="GlassBroken" OnClick="Defect_Click"  CssClass="MenuButton"/>
    </div>
    <div class="View">
        <p>
                            机台：
                            <asp:DropDownList ID="DD_Eq_Search" runat="server" CssClass="InputStyle">
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
                             </asp:DropDownList>
                             单元：
                            <asp:DropDownList ID="DD_Unit_Search" runat="server" CssClass="InputStyle">
                            <asp:ListItem Selected="True">All</asp:ListItem>
                            <asp:ListItem>Indexer</asp:ListItem>
                            <asp:ListItem>Cleaner</asp:ListItem>
                            <asp:ListItem>DB</asp:ListItem>
                            <asp:ListItem>LC</asp:ListItem>
                            <asp:ListItem>VCD</asp:ListItem>
                            <asp:ListItem>SB</asp:ListItem>
                            <asp:ListItem>IF Buffer</asp:ListItem>
                            <asp:ListItem>DEV</asp:ListItem>
                            <asp:ListItem>HB</asp:ListItem>
                            <asp:ListItem>Bypass</asp:ListItem>
                            <asp:ListItem>OVEN</asp:ListItem>
                            <asp:ListItem>AOI</asp:ListItem>
                            <asp:ListItem>DCS</asp:ListItem>
                            <asp:ListItem>Dry Pump</asp:ListItem>
                            </asp:DropDownList>
            关键词：
            <asp:TextBox ID="Keyword_Search" runat="server" placeholder="关键词：" CssClass="TextboxStyle"></asp:TextBox>
            <button id="Search" type="button" runat="server" onserverclick="Search_Click" class="SearchButton">
                &nbsp;<span style="font-family: fontawesome !important;">&#xf002;</span>&nbsp;</button>
            <%-- <button id="Excel" type="button" runat="server" onserverclick="ToExcel_Click" class="SearchButton">&nbsp;<i class="icon-table"></i>&nbsp;</button>--%>
        </p>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
            Width="100%" AllowPaging="True" PageSize="8" OnPageIndexChanging="GridView1_PageIndexChanging"
            OnRowCommand="GridView1_RowCommand" DataKeyNames="ID" CssClass="gridtable">
            <Columns>
                <asp:TemplateField HeaderText="选择">
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="Server" />
                    </ItemTemplate>
                    <HeaderStyle CssClass="gridtitle" Width="5%" />
                    <ItemStyle CssClass="griditemcenter" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="信息">
                    <ItemTemplate>
                        <asp:Label ID="LB_Data_EQ" runat="server" Font-Bold="true"><%#Eval("jitai").ToString()%>&nbsp;&nbsp;<%#Eval("danyuan").ToString() %></asp:Label><br />
                        <asp:Label ID="LB_Data_Date" runat="server"><%#Eval("downtime").ToString()%></asp:Label><br />
                        Down机时间：<asp:Label ID="LB_Data_hours" runat="server"><%#Eval("hours").ToString()%>h</asp:Label><br />
                        累计次数：<asp:Label ID="LB_Data_Count" runat="server"><%#Eval("cishu").ToString()%></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle CssClass="gridtitle" Width="15%" />
                    <ItemStyle CssClass="griditemcenter" />
                </asp:TemplateField>
                <asp:BoundField DataField="alarm" HeaderText="Alarm">
                    <HeaderStyle CssClass="gridtitle" Width="15%" />
                    <ItemStyle CssClass="griditem" />
                </asp:BoundField>
                <asp:BoundField DataField="fenxi" HeaderText="分析" HtmlEncode="False">
                    <HeaderStyle CssClass="gridtitle" Width="25%" />
                    <ItemStyle CssClass="griditem" />
                </asp:BoundField>
                <asp:BoundField DataField="action" HeaderText="Action" HtmlEncode="False">
                    <HeaderStyle CssClass="gridtitle" Width="15%" />
                    <ItemStyle CssClass="griditem" />
                </asp:BoundField>
                <asp:BoundField DataField="yingxiang" HeaderText="影响" HtmlEncode="False">
                    <HeaderStyle CssClass="gridtitle" Width="13%" />
                    <ItemStyle CssClass="griditem" />
                </asp:BoundField>
                <asp:BoundField DataField="eng" HeaderText="工程师">
                    <HeaderStyle CssClass="gridtitle" Width="7%" />
                    <ItemStyle CssClass="griditemcenter" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="附件">
                    <ItemTemplate>
                        <asp:HyperLink ID="fujian" runat="server" Text='<%#Eval("title") %>' Font-Size="Small"
                            Visible='<%#Eval("fujian").ToString()==""?false:true%>' NavigateUrl='<%#"DownLoad.aspx?File="+Eval("fujian") %>'></asp:HyperLink>
                    </ItemTemplate>
                    <HeaderStyle CssClass="gridtitle" Width="5%" />
                    <ItemStyle CssClass="griditemcenter" />
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
            <HeaderStyle Font-Names="微软雅黑" ForeColor="#3366FF" />
        </asp:GridView>
    </div>
    <!--endprint-->
</asp:Content>
