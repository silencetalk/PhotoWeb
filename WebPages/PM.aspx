<%@ Page Language="C#" MasterPageFile="~/WebPages/MasterPage.master" AutoEventWireup="true"
    CodeFile="PM.aspx.cs" Inherits="WebPages_PM" Title="PM" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    </div>
    <div id="SubTitle" class="SubTitle">
        <table style="width: 100%;">
            <tr>
                <td class="SubTitleTable">
                    PM
                </td>
                <td class="ToolsTable">
                    <div id="Tools" class="Tools">
                    <button id="Button1" type="button" runat="server" onserverclick="Add_NewTask_Click" class="ToolButton"><span style="font-family: fontawesome !important;">&#xf067;</span>&nbsp;添加待办事项</button>
                        <button id="Add" type="button" runat="server" onserverclick="Add_NewItem_Click" class="ToolButton"><span style="font-family: fontawesome !important;">&#xf067;</span>&nbsp;添加项目</button>
                        <button id="Print" type="button" onclick="Print_Click()" class="ToolButton"><span style="font-family: fontawesome !important;">&#xf02f;</span>&nbsp;打印</button>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div style="position: relative; z-index: 100;">
        <div id="Add_Dialog" class="FormDiv">
            <div class="FormTitle">
                更新PM项目</div>
            <div class="FormInput">
                <div class="FormBody">
                    <p><asp:Label ID="ToolTips" runat="server"></asp:Label></p>
                    <p>
                        机台：<asp:Label ID="LB_Add_EQ" runat="server" Text=""></asp:Label><br />
                        单元：<asp:Label ID="LB_Add_Unit" runat="server" Text=""></asp:Label><br />
                        项目：<asp:Label ID="LB_Add_Item" runat="server" Text=""></asp:Label><br />
                        位置：<asp:Label ID="LB_Add_Position" runat="server" Text=""></asp:Label>
                    </p>
                    <p>
                        时间：<asp:TextBox ID="TB_Add_Date" runat="server" Width="185px" CssClass="TextboxStyle"></asp:TextBox></p>
                    <p>
                        备注：</p>
                    <p><asp:TextBox ID="TB_Add_Beizhu" runat="server" TextMode="MultiLine" Height="100px" Width="100%" Style="margin: 0; padding: 0;" Font-Names="Microsoft Yahei" Font-Size="12px"></asp:TextBox></p>
                </div>
            </div>
            <div class="FormFooter">
                <asp:Button ID="Add_Dialog_OK" runat="server" Text="确定" CssClass="ConfirmButton"
                    OnClick="Add_OK_Click" />
                <asp:Button ID="Concel1" runat="server" Text="取消" CssClass="ConfirmButton" OnClick="Add_Cancel_Click" />
            </div>
        </div>
        <div id="Add_Task" class="FormDiv">
            <div class="FormTitle">添加待办事项</div>
            <div class="FormInput">
                <div class="FormBody">
                    <p><asp:Label ID="Tooltips_Task" runat="server" ForeColor="Red"></asp:Label></p>
                    <p>
                        机台：<asp:DropDownList ID="DD_EQ" runat="server" CssClass="InputStyle">
                            <asp:ListItem Selected="True">All</asp:ListItem>
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
                        </asp:DropDownList>
                        单元：<asp:DropDownList ID="DD_Unit" runat="server" CssClass="InputStyle">
                            <asp:ListItem Selected="True">AOI</asp:ListItem>
                            <asp:ListItem>Cleaner</asp:ListItem>
							<asp:ListItem>DB</asp:ListItem>
							<asp:ListItem>LC</asp:ListItem>
							<asp:ListItem>SB</asp:ListItem>
							<asp:ListItem>IF Buffer</asp:ListItem>
							<asp:ListItem>TT</asp:ListItem>
							<asp:ListItem>DEV</asp:ListItem>
							<asp:ListItem>HB</asp:ListItem>
							<asp:ListItem>OVEN</asp:ListItem>
							<asp:ListItem>Bypass</asp:ListItem>
							<asp:ListItem>整机</asp:ListItem>
                        </asp:DropDownList>
                    </p>
                    <p>
                        事项名称：<asp:TextBox ID="TB_Task" runat="server" Width="200px" CssClass="TextboxStyle"></asp:TextBox>
                    </p>
                    <p>
                        备注：</p>
                    <p><asp:TextBox ID="TB_Task_Beizhu" runat="server" TextMode="MultiLine" Height="100px" Width="100%" Style="margin: 0; padding: 0;" Font-Names="Microsoft Yahei" Font-Size="12px"></asp:TextBox></p>
                    <p><asp:CheckBox ID="CB_Task" runat="server" Text="完成" /></p>
                </div>
            </div>
            <div class="FormFooter">
                <asp:Button ID="Bt_Add_Task" runat="server" Text="保存" CssClass="ConfirmButton" OnClick="Add_Task_Click" />
                <asp:Button ID="Bt_Add_Task_Cancel" runat="server" Text="返回" CssClass="ConfirmButton" OnClick="Add_Cancel_Click" />
            </div>
        </div>
        <div id="dlg_ok" class="FormDiv">
            <div class="FormTitle">
            </div>
            <div class="FormInput">
                <div class="FormBody">
                    <p>
                        <asp:Label ID="LB_OK" runat="server" Text=""></asp:Label></p>
                </div>
            </div>
            <div class="FormFooter">
                <asp:Button ID="Close" runat="server" Text="关闭" CssClass="ConfirmButton" OnClick="Add_Cancel_Click" />
            </div>
        </div>
    </div>
    <!--startprint-->
    <div class="SubMenu">
        <a href="PM.aspx" class="MenuButton MenuSelected">PM</a>
        <a href="Parts.aspx" class="MenuButton">Parts</a>
        <a href="CST.aspx" class="MenuButton">CST</a>
        <a href="AllItem.aspx" class="MenuButton">平行展开项目</a>
    </div>
    <div class="SubView">
        <div class="View">
            <div style="margin: 10px auto 10px auto; width: 100%; text-align: left;">
                <p>
                    机台：<asp:DropDownList ID="DD_Jitai" runat="server" CssClass="InputStyle" AutoPostBack="true"
                        OnSelectedIndexChanged="Load_Changed">
                        <asp:ListItem Selected="True">请选择机台</asp:ListItem>
                        <asp:ListItem Value="PH01">1号机</asp:ListItem>
                        <asp:ListItem Value="PH02">2号机</asp:ListItem>
                        <asp:ListItem Value="PH03">3号机</asp:ListItem>
                        <asp:ListItem Value="PH04">4号机</asp:ListItem>
                        <asp:ListItem Value="PH05">5号机</asp:ListItem>
                        <asp:ListItem Value="PH06">6号机</asp:ListItem>
                        <asp:ListItem Value="PH07">7号机</asp:ListItem>
                        <asp:ListItem Value="PH08">8号机</asp:ListItem>
                        <asp:ListItem Value="PH09">9号机</asp:ListItem>
                        <asp:ListItem Value="PH10">10号机</asp:ListItem>
                        <asp:ListItem Value="PH11">11号机</asp:ListItem>
                        <asp:ListItem Value="PH12">12号机</asp:ListItem>
                        <asp:ListItem Value="PH13">13号机</asp:ListItem>
                        <asp:ListItem Value="PH14">14号机</asp:ListItem>
                        <asp:ListItem Value="PH15">15号机</asp:ListItem>
                        <asp:ListItem Value="PH16">16号机</asp:ListItem>
						<asp:ListItem Value="PH17">17号机</asp:ListItem>
                        <asp:ListItem Value="PH18">18号机</asp:ListItem>
						<asp:ListItem Value="PH19">19号机</asp:ListItem>
                        <asp:ListItem Value="PH20">20号机</asp:ListItem>
                    </asp:DropDownList>
                    项目：<asp:DropDownList ID="DD_Xiangmu" runat="server" CssClass="InputStyle" AutoPostBack="true"
                        OnSelectedIndexChanged="Load_Changed">
<%--                        <asp:ListItem Selected="True">请选择项目</asp:ListItem>
                        <asp:ListItem Value="PM">PM</asp:ListItem>
                        <asp:ListItem Value="Filter更换">Filter更换</asp:ListItem>
                        <asp:ListItem Value="注油">注油</asp:ListItem>
                        <asp:ListItem Value="Robot注油">Robot注油</asp:ListItem>
                        <asp:ListItem Value="刮片更换">刮片更换</asp:ListItem>
                        <asp:ListItem Value="待办事项">待办事项</asp:ListItem>--%>
                    </asp:DropDownList>
                    <asp:CheckBox ID="CB_ShowAll" runat="server" Text="显示全部" Visible="false" AutoPostBack="true" OnCheckedChanged="LoadAll_Changed"/>
                </p>
            </div>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
                AllowPaging="True" PageSize="30" OnPageIndexChanging="GridView1_PageIndexChanging"
                CssClass="gridtable" OnRowCommand="GridView1_RowCommand" DataKeyNames="ID,EQ,unit,item,position,zhouqi">
                <Columns>
                    <asp:TemplateField HeaderText="历史">
                        <ItemTemplate>
                            <asp:LinkButton ID="LB_History" runat="server" CssClass="fontlink" CommandName="more"
                                CommandArgument='<%# ((GridViewRow)Container).RowIndex %>'><i class="icon-2x icon-th-list"></i></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle Width="6%" CssClass="gridtitle" />
                        <ItemStyle CssClass="griditemcenter" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="机台">
                        <ItemTemplate>
                            <asp:Label ID="LB_Data_Eq" runat="server"><%#Eval("EQ").ToString()%></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="5%" CssClass="gridtitle" />
                        <ItemStyle CssClass="griditemcenter" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="单元">
                        <ItemTemplate>
                            <asp:Label ID="LB_Data_Unit" runat="server"><%#Eval("unit").ToString()%></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="8%" CssClass="gridtitle" />
                        <ItemStyle CssClass="griditemcenter" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="项目">
                        <ItemTemplate>
                            <asp:Label ID="LB_Data_Xiangmu" runat="server"><%#Eval("item").ToString()%></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="10%" CssClass="gridtitle" />
                        <ItemStyle CssClass="griditemcenter" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="位置">
                        <ItemTemplate>
                            <asp:Label ID="LB_Data_Position" runat="server"><%#Eval("position").ToString()%></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="20%" CssClass="gridtitle" />
                        <ItemStyle CssClass="griditemcenter" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="last" HeaderText="最新日期" DataFormatString="{0:yyyy/MM/dd}" HtmlEncode="False">
                        <HeaderStyle Width="10%" CssClass="gridtitle" />
                        <ItemStyle CssClass="griditemcenter" />
                    </asp:BoundField>
                    <asp:BoundField DataField="beizhu" HeaderText="备注" HtmlEncode="False">
                        <HeaderStyle CssClass="gridtitle" />
                        <ItemStyle CssClass="griditem" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="周期/剩余">
                        <HeaderStyle Width="10%" CssClass="gridtitle" />
                        <ItemTemplate>
                            <asp:Label ID="LB_Data_Zhouqi" runat="server"><%#Eval("zhouqi").ToString()%>/</asp:Label>
                            <asp:Label ID="LabelTianshu" runat="server" Text='<%# ((DateTime)Eval("duedate")-DateTime.Now).Days.ToString()+"  天"%>'
                                ForeColor='<%# ((DateTime)Eval("duedate")-DateTime.Now ).Days<10?System.Drawing.Color.Red:System.Drawing.Color.Black%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle CssClass="griditem" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="附件">
                        <ItemTemplate>
                            <asp:HyperLink ID="fujian" runat="server" Font-Size="Small" Visible='<%#Eval("fujian").ToString()==""?false:true%>'
                                NavigateUrl='<%#"DownLoad.aspx?File="+Eval("fujian") %>' CssClass="fontlink"><i class="icon-2x icon-cloud-download"></i></asp:HyperLink>
                        </ItemTemplate>
                        <HeaderStyle Width="5%" CssClass="gridtitle" />
                        <ItemStyle CssClass="griditem" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="更新">
                        <ItemTemplate>
                            <asp:LinkButton ID="LB_Update" runat="server" CssClass="fontlink" CommandName="dataupdate"
                                CommandArgument='<%# ((GridViewRow)Container).RowIndex %>'><i class="icon-2x icon-refresh"></i></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle Width="5%" CssClass="gridtitle" />
                        <ItemStyle CssClass="griditem" />
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
            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false"  EnableModelValidation="True" CssClass="gridtable" OnRowCommand="GridView2_RowCommand" DataKeyNames="ID,eq,unit,taskname,beizhu">
                <Columns>                    
                    <asp:BoundField DataField="eq" HeaderText="机台">
                        <HeaderStyle CssClass="gridtitle" Width="5%" />
                        <ItemStyle CssClass="griditemcenter" />
                    </asp:BoundField>
                    <asp:BoundField DataField="unit" HeaderText="单元">
                        <HeaderStyle CssClass="gridtitle" Width="8%" />
                        <ItemStyle CssClass="griditemcenter" />
                    </asp:BoundField>
                    <asp:BoundField DataField="taskname" HeaderText="事项">
                        <HeaderStyle CssClass="gridtitle" Width="18%" />
                        <ItemStyle CssClass="griditem" />
                    </asp:BoundField>
                    <asp:BoundField DataField="beizhu" HeaderText="备注" HtmlEncode="false">
                        <HeaderStyle CssClass="gridtitle" Width="38%" />
                        <ItemStyle CssClass="griditem" />
                    </asp:BoundField>
                    <asp:BoundField DataField="status" HeaderText="状态">
                        <HeaderStyle CssClass="gridtitle" Width="8%" />
                        <ItemStyle CssClass="griditemcenter" />
                    </asp:BoundField>
                    <asp:BoundField DataField="lastdate" HeaderText="最新日期" DataFormatString="{0:yyyy/MM/dd}">
                        <HeaderStyle CssClass="gridtitle" Width="10%" />
                        <ItemStyle CssClass="griditemcenter" />
                    </asp:BoundField>
                    <asp:BoundField DataField="eng" HeaderText="工程师">
                        <HeaderStyle CssClass="gridtitle" Width="8%" />
                        <ItemStyle CssClass="griditemcenter" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="更新">
                        <ItemTemplate>
                            <asp:LinkButton ID="LB_Update_Task" runat="server" CssClass="fontlink" CommandName="TaskUpdate"
                                CommandArgument='<%# ((GridViewRow)Container).RowIndex %>'><i class="icon-2x icon-refresh"></i></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle Width="5%" CssClass="gridtitle" />
                        <ItemStyle CssClass="griditem" />
                    </asp:TemplateField>
                </Columns>
                <PagerStyle HorizontalAlign="Center" />
                <HeaderStyle Font-Names="微软雅黑" ForeColor="#3366FF" />
            </asp:GridView>
        </div>
    </div>
    <!--endprint-->
</asp:Content>
