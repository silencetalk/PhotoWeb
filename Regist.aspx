<%@ Page Title="" Language="C#" MasterPageFile="~/Login.master" AutoEventWireup="true" CodeFile="Regist.aspx.cs" Inherits="Regist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="Login" class="Login">
<div style="margin:10px auto;height:30px; line-height:30px;font-size:30px;font-family:Microsoft Yahei;">用户注册</div>
<table style="width:100%; border-collaspe:collape;margin:5px auto;">
<tr>
<td class="tableleft">姓名：</td>
<td class="tableright"><asp:TextBox ID="TextBoxName" runat="server" CssClass="InputBox" placeholder="" ></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
        ControlToValidate="TextBoxName" ErrorMessage="*"></asp:RequiredFieldValidator>
    </td>
</tr>
<tr>
<td class="tableleft">工号：</td>
<td class="tableright"><asp:TextBox ID="TextBoxNum" runat="server" CssClass="InputBox" placeholder="" ></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
        ControlToValidate="TextBoxNum" ErrorMessage="*"></asp:RequiredFieldValidator>
    </td>
</tr>
<tr>
<td class="tableleft">密码：</td>
<td class="tableright"><asp:TextBox ID="TextBoxPassword" runat="server" CssClass="InputBox" TextMode="Password" placeholder=""></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
        ControlToValidate="TextBoxPassword" ErrorMessage="*"></asp:RequiredFieldValidator>
    </td>
</tr>
<tr>
<td class="tableleft">确认密码：</td>
<td class="tableright"><asp:TextBox ID="TextBoxConfirmPW" runat="server" CssClass="InputBox" TextMode="Password" placeholder=""></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
        ControlToValidate="TextBoxConfirmPW" ErrorMessage="*"></asp:RequiredFieldValidator>
    <asp:CompareValidator ID="CompareValidator1" runat="server" 
        ControlToCompare="TextBoxPassword" ControlToValidate="TextBoxConfirmPW" 
        ErrorMessage="密码不一致"></asp:CompareValidator>
    </td>
</tr>
<tr>
<td class="tableleft">邮箱：</td>
<td class="tableright"><asp:TextBox ID="TextBoxEmail" runat="server" CssClass="InputBox" placeholder=""></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
        ControlToValidate="TextBoxEmail" ErrorMessage="*"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
        ErrorMessage="格式不正确" 
        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
        ControlToValidate="TextBoxEmail"></asp:RegularExpressionValidator>
    </td>
</tr>
<tr>
<td class="tableleft">手机：</td>
<td class="tableright"><asp:TextBox ID="TextBoxPhone" runat="server" CssClass="InputBox" placeholder="" ></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
        ControlToValidate="TextBoxPhone" ErrorMessage="*"></asp:RequiredFieldValidator>
    </td>
</tr>
</table>
<div style="width:100%;margin:10px auto;height:auto;">
  <asp:Button ID="Zhuce" runat="server" Text="注册" CssClass="LoginButton" onclick="Regist_Click"/>
</div>
</div>
</asp:Content>

