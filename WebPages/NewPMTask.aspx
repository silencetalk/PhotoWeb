<%@ Page Language="C#" MasterPageFile="~/WebPages/MasterPage.master" AutoEventWireup="true" CodeFile="NewPMTask.aspx.cs" Inherits="WebPages_NewPMTask" Title="添加待办事项" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    </div>
    <div id="SubTitle" class="SubTitle">
        <table style="width: 100%;">
            <tr>
                <td class="SubTitleTable">
                    添加PM项目
                </td>
                <td class="ToolsTable">
                    <div id="Tools" class="Tools">
                        <%--<button id="Add" type="button" runat="server" onserverclick="Add_Click" class="ToolButton"><span style="font-family: fontawesome !important;">&#xf067;</span>&nbsp;添加项目</button>
                        <button id="Print" type="button" onclick="Print_Click()" class="ToolButton"><span style="font-family: fontawesome !important;">&#xf02f;</span>&nbsp;打印</button>--%>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div class="View">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <p>
                    <asp:Label ID="ToolTips" runat="server" ForeColor="Red"></asp:Label></p>
                <p>
                    机台：<asp:DropDownList ID="DD_EQ" runat="server" CssClass="InputStyle">
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
                    最迟日期：<asp:TextBox ID="TB_Date" runat="server" Width="100px" CssClass="TextboxStyle"
                        onFocus="WdatePicker({isShowClear:false,readOnly:true})" Text=""></asp:TextBox>
                </p>
            </ContentTemplate>
        </asp:UpdatePanel>
        <p>
            <asp:Button ID="Bt_Add_Item" runat="server" Text="保存" CssClass="ConfirmButton" OnClick="Add_Item_Click" />
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Bt_Add_Item_Cancel" runat="server" Text="返回" CssClass="ConfirmButton" OnClick="Add_Cancel_Click" />
        </p>
    </div>
</asp:Content>

