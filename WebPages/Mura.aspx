<%@ Page Title="Mura Monitor" Language="C#" MasterPageFile="~/WebPages/MasterPage.master" AutoEventWireup="true" CodeFile="Mura.aspx.cs" Inherits="WebPages_Mura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <div id="SubTitle" class="SubTitle">
        <table style="width: 100%;">
            <tr>
                <td class="SubTitleTable">
                    Mura Monitor
                </td>
                <td class="ToolsTable">
                    <div id="Tools" class="Tools">
                        <button id="PreDate" type="button" runat="server" onserverclick="PreDate_Click" class="ButtonCycle"><span style="font-family:fontawesome !important;">&#xf060;</span></button>
                        <button id="SetDate" type="button" runat="server" onserverclick="SetDate_Click" class="ToolButton"><span style="font-family:fontawesome !important;">&#xf073;</span><asp:Label ID="LabelDate" runat="server" Text=""></asp:Label></button>
                        <button id="NextDate" type="button" runat="server" onserverclick="NextDate_Click" class="ButtonCycle"><span style="font-family:fontawesome !important;">&#xf061;</span>&nbsp;</button>
                        <button id="Add" type="button" runat="server" onserverclick="Add_Click" class="ToolButton"><span style="font-family:fontawesome !important;">&#xf067;</span>&nbsp;添加</button>
                        <button id="Delete" type="button" runat="server" onserverclick="Delete_Click" class="ToolButton"><span style="font-family:fontawesome !important;">&#xf014;</span>&nbsp;删除</button>
                        <button id="Edit" type="button" runat="server" onserverclick="Edit_Click" class="ToolButton"><span style="font-family:fontawesome !important;">&#xf044;</span>&nbsp;编辑</button>
                        <button id="Print" type="button" onclick="Print_Click()" class="ToolButton"><span style="font-family:fontawesome !important;">&#xf02f;</span>&nbsp;打印</button>
                        <asp:TextBox ID="TextBoxSearch" runat="server" placeholder="关键词：" CssClass="ToolInput"></asp:TextBox>
                        <button id="Search" type="button" runat="server" onserverclick="Search_Click" class="SearchButton">&nbsp;<span style="font-family:fontawesome !important;">&#xf002;</span>&nbsp;</button>
                    </div>
                </td>
            </tr>
        </table>
</div>

<div style="position:relative; z-index:100;">
<div id="Add_Dialog" class="FormDiv">
<div class="FormTitle">Mura Monitor</div>
<div class="FormInput">
<div class="FormBody">
    <p>
    机台：<asp:DropDownList ID="DropDownListJitai" runat="server" Font-Names="Microsoft Yahei" Font-Size="12px">
    <asp:ListItem  Selected="True">请选择机台</asp:ListItem>
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
    日期：<asp:TextBox ID="TextBoxDate" runat="server" Width="68px" CssClass="TextboxStyle"></asp:TextBox></p>
    <p>
    LotID：<asp:TextBox ID="TextBoxLotid" runat="server" Width="157px" CssClass="TextboxStyle"></asp:TextBox></p>
    <p>Layer ：<asp:DropDownList ID="DDLayer" runat="server" Width="157px" Font-Names="Microsoft Yahei" Font-Size="12px">
                    <asp:ListItem Selected="True">1st ITO</asp:ListItem>
					<asp:ListItem>GATE</asp:ListItem>
					<asp:ListItem>HGA</asp:ListItem>
					<asp:ListItem>ACT</asp:ListItem>
					<asp:ListItem>SD</asp:ListItem>
					<asp:ListItem>SDT</asp:ListItem>
					<asp:ListItem>ORGANIC</asp:ListItem>
					<asp:ListItem>PVX</asp:ListItem>
					<asp:ListItem>2nd ITO</asp:ListItem>
				   </asp:DropDownList>
    </p>
    <p>Mura类型：<asp:TextBox ID="TextBoxMuraType" runat="server" Width="132px" CssClass="TextboxStyle"></asp:TextBox></p>
    <p>
     形状：<asp:DropDownList ID="DropDownListShape" runat="server" Font-Names="Microsoft Yahei" Font-Size="12px">
    <asp:ListItem  Selected="True">Point</asp:ListItem>
    <asp:ListItem>Line</asp:ListItem>
    </asp:DropDownList>
    Size：<asp:DropDownList ID="DropDownListSize" runat="server" Font-Names="Microsoft Yahei" Font-Size="12px">
     <asp:ListItem  Selected="True">Small</asp:ListItem>
    <asp:ListItem>Medium</asp:ListItem>
    <asp:ListItem>Large</asp:ListItem>
    </asp:DropDownList>
    </p>
    <p>
    起点坐标X：<asp:TextBox ID="TextBoxSpx" runat="server" Width="68px" CssClass="TextboxStyle"  ime-mode="disabled" onkeypress="if ((event.keyCode<45||event.keyCode>57))event.returnValue=false; "></asp:TextBox>
    起点坐标Y：<asp:TextBox ID="TextBoxSpy" runat="server" Width="68px" CssClass="TextboxStyle" ime-mode="disabled" onkeypress="if ((event.keyCode<45 || event.keyCode>57)) event.returnValue=false;"></asp:TextBox>
    </p>
    <p>
    终点坐标X：<asp:TextBox ID="TextBoxEpx" runat="server" Width="68px" CssClass="TextboxStyle" ime-mode="disabled" onkeypress="if ((event.keyCode<45 || event.keyCode>57)) event.returnValue=false;"></asp:TextBox>
    终点坐标Y：<asp:TextBox ID="TextBoxEpy" runat="server" Width="68px" CssClass="TextboxStyle" ime-mode="disabled" onkeypress="if ((event.keyCode<45 || event.keyCode>57)) event.returnValue=false;"></asp:TextBox>
    </p>
    <p>备注：</p>
    <p><asp:TextBox ID="TextBoxBeizhu" runat="server" TextMode="MultiLine" Height="100px" Width="100%" style="margin:0;padding:0;" Font-Names="Microsoft Yahei" Font-Size="12px"></asp:TextBox></p>
    <p><asp:Label ID="ToolTips" runat="server"></asp:Label></p>
</div>
</div>
<div class="FormFooter">
     <asp:Button ID="Add_Dialog_OK" runat="server" Text="确定" CssClass="ConfirmButton" OnClick="Add_OK_Click"/>
     <asp:Button ID="Concel1" runat="server" Text="取消" CssClass="ConfirmButton" OnClick="Add_Cancel_Click"/>
</div>
</div>
</div>

<!--startprint-->
<%--    <div class="SubMenu">
    <asp:Button ID="AOIMonitor" runat="server" Text="AOI Monitor"  OnClick="AOI_Click" CssClass="MenuButton"/>
    <asp:Button ID="PRThickness" runat="server" Text="PR Thickness"  OnClick="Thickness_Click" CssClass="MenuButton"/>
    <asp:Button ID="MMMonitor" runat="server" Text="Mura Monitor" CssClass="MenuSelected"/>
    </div>--%>
   <%-- <div class="SubView">--%>
  <%--  <button id="fullscreen" onclick="fullscreenclick()"><span style="font-family:fontawesome !important;">&#xf0b2;</span></button>--%>
    <div id="View" class="View">
     <%--   <table style=" border-collapse:collapse;">
    <tr>
    <td style="width:50%; vertical-align:top;">--%>
    <p>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            EnableModelValidation="True" Width="100%"
                            BorderColor="#A0A0A0" BorderStyle="Solid" 
            BorderWidth="1px" DataKeyNames="ID" 
            onpageindexchanging="GridView1_PageIndexChanging" 
            onrowcommand="GridView1_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="选择">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="Server" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="jitai" HeaderText="机台">
                                    <HeaderStyle Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="lotid" HeaderText="产品">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="layer" HeaderText="Layer">
                                    <HeaderStyle Width="8%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="muraname" HeaderText="Mura类型">
                                    <HeaderStyle Width="8%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="sizename" HeaderText="Size">
                                    <HeaderStyle Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="startp" HeaderText="起点坐标">
                                    <HeaderStyle Width="18%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="endp" HeaderText="终点坐标">
                                    <HeaderStyle Width="18%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                 </asp:BoundField>
                                    <asp:BoundField DataField="beizhu" HeaderText="备注" HtmlEncode="False">
                                    <HeaderStyle Width="23%" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle Font-Names="Microsoft Yahei" ForeColor="#3366FF"/>
     </asp:GridView> 
    </p>
     <%--</td>
    <td style="width:50%; vertical-align:middle; text-align:center;">--%>
    <p style="text-align:center">
    <asp:Image ID="ImageMura" runat="server" Width="780px" Height="750px" /></p>
     <%--</td>
    </tr>
    </table>--%>
    </div>
<!--endprint-->


</asp:Content>

