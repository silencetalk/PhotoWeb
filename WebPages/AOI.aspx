<%@ Page Language="C#" MasterPageFile="~/WebPages/MasterPage.master" AutoEventWireup="true" CodeFile="AOI.aspx.cs" Inherits="WebPages_AOI" Title="AOI Monitor" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">



<style type="text/css">
.defecttable
{
	width:100%;
	margin:10px 0px 0px 0px;
	padding:10px;
    border:1px solid rgb(204,204,204);
}
.tableleft
{
	text-align:right;
}
.tableright
{
	text-align:left;
}

</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <div id="SubTitle" class="SubTitle">
        <table style="width: 100%;">
            <tr>
                <td class="SubTitleTable">
                    Daily Monitor
                </td>
                <td class="ToolsTable">
                    <div id="Tools" class="Tools">
                        <button id="PreDate" type="button" runat="server" onserverclick="PreDate_Click" class="ButtonCycle"><span style="font-family:fontawesome !important;">&#xf060;</span></button>
                        <button id="SetDate" type="button" runat="server" onserverclick="SetDate_Click" class="ToolButton"><span style="font-family:fontawesome !important;">&#xf073;</span>&nbsp;<asp:Label ID="LabelDate" runat="server" Text=""></asp:Label></button>
                        <button id="NextDate" type="button" runat="server" onserverclick="NextDate_Click" class="ButtonCycle"><span style="font-family:fontawesome !important;">&#xf061;</span></button>
                        <button id="Add" type="button"  runat="server" onserverclick="Add_Click" class="ToolButton"><span style="font-family:fontawesome !important;">&#xf067;</span>&nbsp;添加</button>
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
<div class="FormTitle">AOI Monitor</div>
<div class="FormInput">
<div class="FormBody">
<p>
    机台：<asp:DropDownList ID="DropDownListJitai" runat="server" CssClass="InputStyle">
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
    日期：<asp:TextBox ID="TextBoxInputDate" runat="server" Width="68px" CssClass="TextboxStyle"></asp:TextBox>
    班次：<asp:DropDownList ID="DropDownListInputBanci" runat="server" CssClass="InputStyle">
        <asp:ListItem>夜班</asp:ListItem>
        <asp:ListItem>白班</asp:ListItem>
    </asp:DropDownList></p>
    <p>Defect 统计：</p>
    <table class="defecttable">
    <tr>
    <td class="tableleft">Coating PT</td>
    <td class="tableright"><asp:TextBox ID="TextBoxCoatingPT" runat="server" Width="30px" CssClass="TextboxStyle" ime-mode="disabled" onkeypress="if ((event.keyCode<48 || event.keyCode>57)) event.returnValue=false;"></asp:TextBox></td>
    <td class="tableleft">EXP PT</td>
    <td class="tableright"><asp:TextBox ID="TextBoxExp" runat="server" Width="30px" CssClass="TextboxStyle" ime-mode="disabled" onkeypress="if ((event.keyCode<48 || event.keyCode>57)) event.returnValue=false;"></asp:TextBox></td>
    <td class="tableleft">PR Peeling</td>
    <td class="tableright"><asp:TextBox ID="TextBoxPeeling" runat="server" Width="30px" CssClass="TextboxStyle" ime-mode="disabled" onkeypress="if ((event.keyCode<48 || event.keyCode>57)) event.returnValue=false;"></asp:TextBox></td>
    </tr>
    <tr>
    <td class="tableleft">PR Remain:</td>
    <td class="tableright"><asp:TextBox ID="TextBoxRemain" runat="server" Width="30px" CssClass="TextboxStyle" ime-mode="disabled" onkeypress="if ((event.keyCode<48 || event.keyCode>57)) event.returnValue=false;"></asp:TextBox></td>
    <td class="tableleft">PR Open:</td>
    <td class="tableright"><asp:TextBox ID="TextBoxOpen" runat="server" Width="30px" CssClass="TextboxStyle" ime-mode="disabled" onkeypress="if ((event.keyCode<48 || event.keyCode>57)) event.returnValue=false;"></asp:TextBox></td>
    <td class="tableleft">DEV Buble:</td>
    <td class="tableright"><asp:TextBox ID="TextBoxBuble" runat="server" Width="30px" CssClass="TextboxStyle" ime-mode="disabled" onkeypress="if ((event.keyCode<48 || event.keyCode>57)) event.returnValue=false;"></asp:TextBox></td>
    </tr>
     <tr>
    <td class="tableleft">DEV PT:</td>
    <td class="tableright"><asp:TextBox ID="TextBoxDEVPT" runat="server" Width="30px" CssClass="TextboxStyle" ime-mode="disabled" onkeypress="if ((event.keyCode<48 || event.keyCode>57)) event.returnValue=false;"></asp:TextBox></td>
    <td class="tableleft">水残:</td>
    <td class="tableright"><asp:TextBox ID="TextBoxWater" runat="server" Width="30px" CssClass="TextboxStyle" ime-mode="disabled" onkeypress="if ((event.keyCode<48 || event.keyCode>57)) event.returnValue=false;"></asp:TextBox></td>
    <td class="tableleft">EQ Down:</td>
    <td class="tableright"><asp:TextBox ID="TextBoxDown" runat="server" Width="30px" CssClass="TextboxStyle" ime-mode="disabled" onkeypress="if ((event.keyCode<48 || event.keyCode>57)) event.returnValue=false;"></asp:TextBox></td>
    </tr>
     <tr>
    <td class="tableleft">ReWork厘清:</td>
    <td class="tableright"><asp:TextBox ID="TextBoxRework" runat="server" Width="30px" CssClass="TextboxStyle" ime-mode="disabled" onkeypress="if ((event.keyCode<48 || event.keyCode>57)) event.returnValue=false;"></asp:TextBox></td>
    <td class="tableleft"> Others:</td>
    <td class="tableright"><asp:TextBox ID="TextBoxOther" runat="server" Width="30px" CssClass="TextboxStyle" ime-mode="disabled" onkeypress="if ((event.keyCode<48 || event.keyCode>57)) event.returnValue=false;"></asp:TextBox>    </td>
    </tr>
    </table>
    <p>附件：
    <asp:FileUpload ID="FileUpload1" runat="server" Width="232px" CssClass="InputStyle"/></p>  
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
    <div class="SubMenu">
    <asp:Button ID="AOI" runat="server" Text="AOI Monitor" CssClass="MenuSelected"/>
    <asp:Button ID="Thickness" runat="server" Text="PR Thickness"  OnClick="Thickness_Click" CssClass="MenuButton"/>
    <asp:Button ID="Mura" runat="server" Text="Mura Monitor"  OnClick="Mura_Click" CssClass="MenuButton"/>
    </div>
    <div class="SubView">
  <%--  <button id="fullscreen" onclick="fullscreenclick()"><span style="font-family:fontawesome !important;">&#xf0b2;</span></button>--%>
    <div id="View" class="View">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            EnableModelValidation="True" Width="100%" BorderColor="#A0A0A0" BorderStyle="Solid" BorderWidth="1px" DataKeyNames="ID">
                            <Columns>
                                <asp:TemplateField HeaderText="选择">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="Server" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="jitai" HeaderText="设备">
                                    <HeaderStyle Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="shortdate" HeaderText="日期">
                                    <HeaderStyle Width="8%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="banci" HeaderText="班次">
                                    <HeaderStyle Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="coating" HeaderText="Coating PT">
                                    <HeaderStyle Width="6%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="exp" HeaderText="EXP PT">
                                    <HeaderStyle Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="peeling" HeaderText="PR Peeling">
                                    <HeaderStyle Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="remain" HeaderText="PR Remain">
                                    <HeaderStyle Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="open" HeaderText="PR Open">
                                    <HeaderStyle Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="buble" HeaderText="DEV气泡">
                                    <HeaderStyle Width="6%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="devpt" HeaderText="DEV PT">
                                    <HeaderStyle Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="water" HeaderText="水残">
                                    <HeaderStyle Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="down" HeaderText="EQ Down">
                                    <HeaderStyle Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="rw" HeaderText="R/W厘清">
                                    <HeaderStyle Width="6%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="other" HeaderText="Others">
                                    <HeaderStyle Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                 <asp:BoundField DataField="total" HeaderText="Total">
                                    <HeaderStyle Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="eng" HeaderText="工程师">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                               <asp:TemplateField HeaderText="附件">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="file" runat="server" Font-Size="Small"  Visible='<%#Eval("fujian").ToString()==""?false:true%>' NavigateUrl='<%#"DownLoad.aspx?File="+Eval("fujian") %>' CssClass="fontlink" ><span style="font-family:fontawesome !important; font-size:20px;">&#xf0ed;</span></asp:HyperLink>
                                    </ItemTemplate>
                                    <HeaderStyle Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>   
                            </Columns>
                            <HeaderStyle Font-Names="微软雅黑" ForeColor="#3366FF"/>
     </asp:GridView> 
    </div>
    
    <div id="chart" class="View" style="margin-top:10px;border:solid 1px #CCCCCC">
    <div style="margin-top:10px;">
    <p>
    机台：<asp:DropDownList ID="DDTongjiJitai" runat="server" CssClass="InputStyle" AutoPostBack="true" OnSelectedIndexChanged="TongjiChanged">
                    <asp:ListItem>1号机</asp:ListItem>
                    <asp:ListItem>2号机</asp:ListItem>
                    <asp:ListItem>3号机</asp:ListItem>
                    <asp:ListItem Selected="True" >4号机</asp:ListItem>
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
    统计类型：<asp:DropDownList ID="DropDownListLeiXing" runat="server" CssClass="InputStyle" AutoPostBack="true" OnSelectedIndexChanged="TongjiChanged">
                    <asp:ListItem Selected="True">Total</asp:ListItem>
                    <asp:ListItem>Coating PT</asp:ListItem>
                    <asp:ListItem>EXP PT</asp:ListItem>
                    <asp:ListItem>PR Peeling</asp:ListItem>
                    <asp:ListItem>PR Remain</asp:ListItem>
                    <asp:ListItem>PR Open</asp:ListItem>
                    <asp:ListItem>DEV Buble</asp:ListItem>
                    <asp:ListItem>DEV PT</asp:ListItem>
                    <asp:ListItem>水残</asp:ListItem>
                    <asp:ListItem>EQ Down</asp:ListItem>
                    <asp:ListItem>R/W 厘清</asp:ListItem>
                    <asp:ListItem>Other</asp:ListItem>
    </asp:DropDownList>
    统计时间：<asp:DropDownList ID="DropDownListTongjiShijian" runat="server" CssClass="InputStyle" AutoPostBack="true" OnSelectedIndexChanged="TongjiChanged">
        <asp:ListItem Selected="True">一周内</asp:ListItem>
        <asp:ListItem>两周内</asp:ListItem>
        <asp:ListItem>一月内</asp:ListItem>
    </asp:DropDownList></p>
    </div>
        <asp:Chart ID="Chart1" runat="server" Height="400px" Width="960px" ImageStorageMode="UseImageLocation" ImageLocation="~/ChartPic/">
            <Titles>
                <asp:Title Font="微软雅黑, 20px" Name="Title1" Text="Defect统计"  DockedToChartArea="ChartArea1">
                    <Position Height="5" Width="35.8539925" X="15" Y="5" />
                </asp:Title>
                <asp:Title Font="微软雅黑, 20px" Text="Defect分析" Name="Title2" DockedToChartArea="ChartArea2">
                    <Position Height="5" Width="41.8716125" X="65" Y="5" />
                </asp:Title>
            </Titles>
            <Series>
                <asp:Series ChartArea="ChartArea1"  Legend="Legend1" Name="Total"  
                    ChartType="Line" MarkerColor="Blue" MarkerSize="7"  MarkerStyle="Circle" 
                    ToolTip="#VALX" Font="Microsoft Yahei, 8pt"  IsValueShownAsLabel="True"   
                    Label="#VAL">
                </asp:Series>
                <asp:Series ChartArea="ChartArea2" Legend="Legend2" Name="Defect"  ChartType="Pie" MarkerColor="Blue" MarkerSize="7"  MarkerStyle="Circle"  ToolTip="#VAL" LegendText="#VALX:#VAL" Font="Microsoft Yahei, 8pt"  IsValueShownAsLabel="True"   Label="#VALX:#VAL">
                </asp:Series>
            </Series>
            <ChartAreas>
                 <asp:ChartArea Name="ChartArea1" >
                <AxisY ArrowStyle="Triangle" Title="Total" TitleAlignment="Far" TitleFont="Microsoft Yahei, 12px">
                </AxisY>
                <AxisX ArrowStyle="Triangle" Title="日期"  TitleAlignment="Far" TitleFont="Microsoft Yahei, 12px" >
                    <MajorGrid Enabled="False" />
                </AxisX>
                     <Position Height="80" Width="60" Y="15" />
                 </asp:ChartArea>
                 <asp:ChartArea Name="ChartArea2">
                     <Position Height="80" Width="60" X="65" Y="10" />
                 </asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
    </div>
    
    </div>
<!--endprint-->

</asp:Content>

