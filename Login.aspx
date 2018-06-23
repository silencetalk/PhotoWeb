<%@ Page Language="C#" MasterPageFile="~/Login.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" Title="Track 事务管理系统" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div>
<div style="margin:10px auto;height:30px; line-height:30px;font-size:30px;font-family:Microsoft Yahei;">用户登录</div>
     <table server="" style="width: 100%; border-collaspe<asp: Login runat=">
         <asp:loginname runat="server"></asp:loginname>
<tr>
<td class="tableleft">工号：</td>
<td class="tableright"><asp:TextBox ID="TextBoxNum" runat="server" CssClass="InputBox" placeholder="" ></asp:TextBox>
    <asp:Label ID="TipsNum" runat="server" Font-Strikeout="False" ForeColor="Red" 
        Text="*请输入工号"></asp:Label>
    </td>
</tr>
<tr>
<td class="tableleft">密码：</td>
<td class="tableright">
 <asp:TextBox ID="TextBoxPassword" runat="server" CssClass="InputBox" TextMode="Password" placeholder=""></asp:TextBox>
    <asp:Label ID="TipsPassword" runat="server" ForeColor="Red" Text="*请输入密码"></asp:Label>
    </td>
</tr>
</table>
<div style="width:100%;margin:10px auto;height:auto;">
  <asp:Button ID="Loginin" runat="server" Text="登录" CssClass="LoginButton" onclick="Loginin_Click"/>
</div>
</div>
</asp:Content>

