<%@ Page Title="AOI Monitor" Language="C#" MasterPageFile="~/WebPages/MasterPage.master"
    AutoEventWireup="true" CodeFile="Defect.aspx.cs" Inherits="WebPages_AOI_Monitor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <div style="position: relative; z-index: 100;">
        <div id="imgdiv" class="zoomimg" onclick="$('#FormDivBg').hide();$(this).hide();">
            <img id="zoomimg" alt="" src="" style="width: 100%; height: 100%" />
        </div>
        <div id="Add_Dialog" class="FormDiv">
            <div class="FormTitle">
                异常Lot</div>
            <div class="FormInput">
                <div class="FormBody">
                    <p>
                        机台：<asp:DropDownList ID="DD_Jitai" runat="server" Font-Names="Microsoft Yahei" Font-Size="12px">
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
                        日期：<asp:TextBox ID="TB_Date" runat="server" Width="80px" CssClass="TextboxStyle"></asp:TextBox>
                        班次：<asp:DropDownList ID="DD_Banci" runat="server" CssClass="InputStyle">
                            <asp:ListItem>夜班</asp:ListItem>
                            <asp:ListItem>白班</asp:ListItem>
                        </asp:DropDownList>
                    </p>
                    <p>
                        LotID：<asp:TextBox ID="TB_LotID" runat="server" Width="130px" CssClass="TextboxStyle"></asp:TextBox></p>
                    <p>
                        异常：</p>
                    <p>
                        <asp:TextBox ID="TB_Yichang" runat="server" TextMode="MultiLine" Height="100px" Width="100%"
                            Style="margin: 0; padding: 0;" Font-Names="Microsoft Yahei" Font-Size="12px"></asp:TextBox></p>
                    <p>
                        处理方法：</p>
                    <p>
                        <asp:TextBox ID="TB_Chuli" runat="server" TextMode="MultiLine" Height="100px" Width="100%"
                            Style="margin: 0; padding: 0;" Font-Names="Microsoft Yahei" Font-Size="12px"></asp:TextBox></p>
                    <p>
                        Defect图片URL：</p>
                    <p>
                        <asp:TextBox ID="TB_ImgUrl" runat="server" TextMode="MultiLine" Height="100px" Width="100%"
                            Style="margin: 0; padding: 0;" Font-Names="Microsoft Yahei" Font-Size="12px"></asp:TextBox></p>
                    <p>
                        Map图片：
                        <asp:FileUpload ID="FileUpload1" runat="server" Width="200px" CssClass="InputStyle" /></p>
<%--                    <p>
                        Defect图片：
                        <asp:FileUpload ID="FileUpload2" runat="server" Width="200px" CssClass="InputStyle" /></p>--%>
                    <p>
                        <asp:Label ID="ToolTips" runat="server"></asp:Label></p>
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
    <div id="SubTitle" class="SubTitle">
        <table style="width: 100%;">
            <tr>
                <td class="SubTitleTable">
                    异常Lot
                </td>
                <td class="ToolsTable">
                    <div id="Tools" class="Tools">
                        <button id="PreDate" type="button" runat="server" onserverclick="PreDate_Click" class="ButtonCycle">
                            <span style="font-family: fontawesome !important;">&#xf060;</span></button>
                        <button id="SetDate" type="button" runat="server" onserverclick="SetDate_Click" class="ToolButton">
                            <span style="font-family: fontawesome !important;">&#xf073;</span>&nbsp;
                            <asp:Label ID="LabelDate" runat="server" Text=""></asp:Label></button>
                        <button id="NextDate" type="button" runat="server" onserverclick="NextDate_Click" class="ButtonCycle">
                            <span style="font-family: fontawesome !important;">&#xf061;</span></button>
                        <button id="Add" type="button" runat="server" onserverclick="Add_Click" class="ToolButton">
                            <span style="font-family: fontawesome !important;">&#xf067;</span>&nbsp;添加</button>
                        <button id="Delete" type="button" runat="server" onserverclick="Delete_Click" class="ToolButton">
                            <span style="font-family: fontawesome !important;">&#xf014;</span>&nbsp;删除</button>
                        <button id="Edit" type="button" runat="server" onserverclick="Edit_Click" class="ToolButton">
                            <span style="font-family: fontawesome !important;">&#xf044;</span>&nbsp;编辑</button>
                        <button id="Print" type="button" onclick="Print_Click()" class="ToolButton">
                            <span style="font-family: fontawesome !important;">&#xf02f;</span>&nbsp;打印</button>
                        <asp:TextBox ID="TextBoxSearch" runat="server" placeholder="关键词：" CssClass="ToolInput"></asp:TextBox>
                        <button id="Search" type="button" runat="server" onserverclick="Search_Click" class="SearchButton">
                            &nbsp;<span style="font-family: fontawesome !important;">&#xf002;</span>&nbsp;</button>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <!--startprint-->
    <div class="SubMenu">
        <a href="Defect.aspx" class="MenuButton MenuSelected">异常Lot</a> <a href="AOIIssue.aspx"
            class="MenuButton">AOI Issue</a>
    </div>
    <div class="SubView">
        <%--  <button id="fullscreen" onclick="fullscreenclick()"><span style="font-family:fontawesome !important;">&#xf0b2;</span></button>--%>
        <div id="View" class="View">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
                CssClass="gridtable" DataKeyNames="ID" OnPageIndexChanging="GridView1_PageIndexChanging"
                OnRowCommand="GridView1_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="选择">
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="Server" /><br />
                            <%#Eval("jitai") %><br />
                            <%#Eval("shortdate") %><br />
                            <%#Eval("banci") %>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gridtitle" Width="10%" />
                        <ItemStyle CssClass="griditemcenter" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="lotid" HeaderText="Lot ID">
                        <HeaderStyle CssClass="gridtitle" Width="10%" />
                        <ItemStyle CssClass="griditemcenter" />
                    </asp:BoundField>
                    <asp:BoundField DataField="yichang" HeaderText="异常">
                        <HeaderStyle CssClass="gridtitle" Width="10%" />
                        <ItemStyle CssClass="griditemcenter" />
                    </asp:BoundField>
                    <asp:BoundField DataField="chuli" HeaderText="处理方法" HtmlEncode="False">
                        <HeaderStyle CssClass="gridtitle" Width="10%" />
                        <ItemStyle CssClass="griditemcenter" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Defect">
                        <ItemTemplate>
                            <%--<asp:Image ID="Image1" runat="server" ImageUrl='<%#Eval("mapimg") %>' Visible='<%#Eval("mapimg").ToString()==""?false:true %>'
                                Width="139px" Height="104px" onclick="ImgZoom(this)" Style="cursor: url(../App_Themes/Main/big.cur)" />--%>
                            <%#Eval("defectimg").ToString().Replace("[img]", "<img alt='defect img' style='width:40%;margin:2px' onclick='ImgZoom(this)' src='").Replace("[/img]", "'/>")%>
                        </ItemTemplate>
                        <HeaderStyle CssClass="gridtitle" Width="40%" />
                        <ItemStyle CssClass="griditemcenter" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Map">
                        <ItemTemplate>
                            <asp:Image ID="Image2" runat="server" ImageUrl='<%#Eval("mapimg") %>' Visible='<%#Eval("mapimg").ToString()==""?false:true %>'
                                Width="139px" Height="104px" onclick="ImgZoom(this)" Style="cursor: url(../App_Themes/Main/big.cur)" />
                        </ItemTemplate>
                        <HeaderStyle CssClass="gridtitle" Width="20%" />
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
                <HeaderStyle Font-Names="Microsoft Yahei" ForeColor="#3366FF" />
            </asp:GridView>
        </div>
    </div>
    <!--endprint-->
</asp:Content>
