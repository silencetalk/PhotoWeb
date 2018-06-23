<%@ Page Language="C#" MasterPageFile="~/WebPages/MasterPage.master" AutoEventWireup="true"
    CodeFile="EQState.aspx.cs" Inherits="WebPages_EQState" Title="EQ State" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="SubTitle" class="SubTitle">
        <table style="width: 100%;">
            <tr>
                <td class="SubTitleTable">
                    EQ State
                </td>
                <td class="ToolsTable">
                    <div id="Tools" class="Tools">
                        <button id="PreDate" type="button" runat="server" onserverclick="PreDate_Click" class="ButtonCycle">
                            <span style="font-family: fontawesome !important;">&#xf060;</span></button>
                        <%--<asp:DataList ID="DataList1" runat="server" Width="100%" CellPadding="0">    
                            <ItemTemplate>
                                <button id="SetDate" type="button" class="ToolButton"><span style="font-family: fontawesome !important;">&#xf073;</span><%#Eval("shortdate") %></button>
                            </ItemTemplate>
                       </asp:DataList>--%>
                        <button id="SetDate" type="button" runat="server" onserverclick="SetDate_Click" class="ToolButton">
                            <span style="font-family: fontawesome !important;">&#xf073;</span><asp:Label ID="LabelDate"
                                runat="server" Text=""></asp:Label></button>
                        <button id="NextDate" type="button" runat="server" onserverclick="NextDate_Click" class="ButtonCycle">
                            <span style="font-family: fontawesome !important;">&#xf061;</span></button>
                        <button id="Add" type="button" onclick="ShowDialog('Add_Dialog');" class="ToolButton">
                            <span style="font-family: fontawesome !important;">&#xf067;</span>&nbsp;上传数据</button>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div style="position: relative; z-index: 100;">
        <div id="Add_Dialog" class="FormDiv">
            <div class="FormTitle">
                数据上传</div>
            <div class="FormInput">
                <div class="FormBody">
                    <p>
                        &nbsp;<asp:Label ID="ToolTips" runat="server" ForeColor="Red"></asp:Label></p>
                    <p>
                        Excel文档：
                        <asp:FileUpload ID="FileUpload1" runat="server" Width="200px" CssClass="InputStyle" /></p>
                    <p>
                        <i class="icon-warning-sign"></i>请使用BO模板
                        <asp:HyperLink ID="file" runat="server" NavigateUrl="DownLoad.aspx?File=~\UploadFiles\培训资料\20150311054237PH EQ State.wid">PH State.wid</asp:HyperLink>
                        刷数据，并保持为.xls格式上传</p>
                </div>
            </div>
            <div class="FormFooter">
                <asp:Button ID="Add_Dialog_OK" runat="server" Text="确定" CssClass="ConfirmButton"
                    OnClick="Add_OK_Click" />
                <asp:Button ID="Concel" runat="server" Text="取消" CssClass="ConfirmButton" OnClick="Add_Cancel_Click" />
            </div>
        </div>
    </div>
    <div id="chart" class="View" style="margin-top: 10px; border: solid 1px #CCCCCC">
        <asp:Chart ID="Chart1" runat="server" Height="426px" Width="792px" BackColor="#D3DFF0"
            Palette="BrightPastel" BorderDashStyle="Solid" BackGradientStyle="TopBottom"
            BorderWidth="2" BorderColor="26, 59, 105" ImageLocation="~/ChartPic/.png">
            <Legends>
                <asp:Legend TitleFont="Microsoft Sans Serif, 8pt, style=Bold" BackColor="Transparent"
                    Font="Trebuchet MS, 8.25pt, style=Bold" IsTextAutoFit="false" Enabled="true"
                    Name="Default" IsDockedInsideChartArea="False">
                </asp:Legend>
            </Legends>
            <BorderSkin SkinStyle="Emboss"></BorderSkin>
            <Series>
                <%--<asp:Series Name="Series1" ChartType="StackedBar100" BorderColor="180, 26, 59, 105"
                    Color="220, 65, 140, 240">
                </asp:Series>
                <asp:Series Name="Series2" ChartType="StackedBar100" BorderColor="180, 26, 59, 105"
                    Color="220, 252, 180, 65">
                </asp:Series>
                <asp:Series Name="Series3" ChartType="StackedBar100" BorderColor="180, 26, 59, 105"
                    Color="220, 224, 64, 10">
                </asp:Series>
                <asp:Series Name="Series4" ChartType="StackedBar100" BorderColor="180, 26, 59, 105"
                    Color="220, 5, 100, 146">
                </asp:Series>
                <asp:Series Name="Series5" ChartType="StackedBar100" BorderColor="180, 26, 59, 105"
                    Color="220, 5, 50, 110">
                </asp:Series>--%>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid"
                    BackSecondaryColor="Transparent" BackColor="64, 165, 191, 228" ShadowColor="Transparent"
                    BackGradientStyle="TopBottom">
                    <Area3DStyle Rotation="10" Inclination="15" WallWidth="0" />
                    <Position Y="3" Height="92" Width="85" X="2"></Position>
                    <AxisY LineColor="64, 64, 64, 64" LabelAutoFitMaxFontSize="8">
                        <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                        <MajorGrid LineColor="64, 64, 64, 64" />
                    </AxisY>
                    <AxisX LineColor="64, 64, 64, 64" LabelAutoFitMaxFontSize="8" Interval="2">
                        <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
                        <MajorGrid LineColor="64, 64, 64, 64" />
                    </AxisX>
                </asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
    </div>
</asp:Content>
