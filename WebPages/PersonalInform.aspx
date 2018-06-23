<%@ Page Title="个人交接" Language="C#" MasterPageFile="~/WebPages/MasterPage.master" AutoEventWireup="true" CodeFile="PersonalInform.aspx.cs" Inherits="WebPages_PersonalInform" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <div id="SubTitle" class="SubTitle">
        <table style="width: 100%;">
            <tr>
                <td class="SubTitleTable">
                    工作交接
                </td>
                <td class="ToolsTable">
                    <div id="Tools" class="Tools">
                        <button id="Add" type="button" runat="server" onserverclick="Add_Click" class="ToolButton"><span style="font-family:fontawesome !important;">&#xf067;</span>&nbsp;添加</button>
                        <button id="Delete" type="button" runat="server" onserverclick="Delete_Click" class="ToolButton"><span style="font-family:fontawesome !important;">&#xf014;</span>&nbsp;删除</button>
                        <button id="Edit" type="button" runat="server" onserverclick="Edit_Click" class="ToolButton"><span style="font-family:fontawesome !important;">&#xf044;</span>&nbsp;编辑</button>
                        <button id="Reply" type="button" onserverclick="Reply_Click" class="ToolButton" runat="server"><span style="font-family:fontawesome !important;">&#xf112;</span>&nbsp;回复</button>
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
<div class="FormTitle">个人交接</div>
<div class="FormInput">
<div class="FormBody">
    <p>
    TO：<asp:DropDownList ID="DropDownListEng" runat="server" CssClass="InputStyle" >
    <asp:ListItem  Selected="True">请选择工程师</asp:ListItem>
					<asp:ListItem>朱攀</asp:ListItem>
					<asp:ListItem>郭峰言</asp:ListItem>
					<asp:ListItem>汪宗源</asp:ListItem>
					<asp:ListItem>胡瑞煌</asp:ListItem>
					<asp:ListItem>王鹭</asp:ListItem>
					<asp:ListItem>施倩</asp:ListItem>
					<asp:ListItem>宁磊</asp:ListItem>
					<asp:ListItem>王胜</asp:ListItem>
					<asp:ListItem>董昌文</asp:ListItem>
					<asp:ListItem>王明</asp:ListItem>
					<asp:ListItem>王新伟</asp:ListItem>
					<asp:ListItem>刘亚鑫</asp:ListItem>
					<asp:ListItem>翁志杰</asp:ListItem>
					<asp:ListItem>陈敦明</asp:ListItem>
					<asp:ListItem>郑章坚</asp:ListItem>
					<asp:ListItem>胡小军</asp:ListItem>
					<asp:ListItem>刘子迪</asp:ListItem>
					<asp:ListItem>谢晓兴</asp:ListItem>
					<asp:ListItem>陈聪</asp:ListItem>
					<asp:ListItem>武军伟</asp:ListItem>
					<asp:ListItem>王祖恒</asp:ListItem>
					<asp:ListItem>陈伟涛</asp:ListItem>
					<asp:ListItem>杨志杰</asp:ListItem>
					<asp:ListItem>刘秀</asp:ListItem>
					<asp:ListItem>周海宽</asp:ListItem>
					<asp:ListItem>倪联强</asp:ListItem>
					<asp:ListItem>陈浩</asp:ListItem>
					<asp:ListItem>苗翰文</asp:ListItem>
					<asp:ListItem>肖震林</asp:ListItem>
					<asp:ListItem>黄圣炜</asp:ListItem>
					<asp:ListItem>李建强</asp:ListItem>
					<asp:ListItem>赵荣辉</asp:ListItem>
					<asp:ListItem>汤海星</asp:ListItem>
					<asp:ListItem>卢敏</asp:ListItem>
					<asp:ListItem>游露</asp:ListItem>
					<asp:ListItem>徐悦</asp:ListItem>
					<asp:ListItem>钟天发</asp:ListItem>
					<asp:ListItem>黄彬彬</asp:ListItem>
					<asp:ListItem>李声扬</asp:ListItem>
					<asp:ListItem>李丹丹</asp:ListItem>
					<asp:ListItem>赵豪杰</asp:ListItem>
					<asp:ListItem>张昊阳</asp:ListItem>
					<asp:ListItem>王利铭</asp:ListItem>
					<asp:ListItem>熊衍续</asp:ListItem>
					<asp:ListItem>施传伟</asp:ListItem>
					<asp:ListItem>李琛</asp:ListItem>
					<asp:ListItem>岩糯坎</asp:ListItem>
					<asp:ListItem>黄明强</asp:ListItem>
					<asp:ListItem>庄青斌</asp:ListItem>
					<asp:ListItem>何颖</asp:ListItem>
					<asp:ListItem>张小波</asp:ListItem>
					<asp:ListItem>刘棚主</asp:ListItem>
					<asp:ListItem>何赛</asp:ListItem>
					<asp:ListItem>张健</asp:ListItem>
					<asp:ListItem>高奇奇</asp:ListItem>
					<asp:ListItem>刘书文</asp:ListItem>
    </asp:DropDownList>
    日期：<asp:TextBox ID="TextBoxGerenDate" runat="server" Width="68px" CssClass="TextboxStyle"></asp:TextBox></p>
    <p><asp:TextBox ID="TextBoxGeren" runat="server" TextMode="MultiLine" Height="200px" Width="100%" style="margin:0;padding:0;" Font-Names="Microsoft Yahei" Font-Size="12px"></asp:TextBox></p>
    <p><span style="font-family:fontawesome !important; color:Red;">&#xf071;</span>&nbsp;<asp:Label ID="ToolTips1" runat="server" Text="字数请不要超过900字！"></asp:Label></p>
</div>
</div>
<div class="FormFooter">
     <asp:Button ID="Add_OK" runat="server" Text="确定" CssClass="ConfirmButton" OnClick="Add_OK_Click"/>
     <asp:Button ID="Concel2" runat="server" Text="取消" CssClass="ConfirmButton" OnClick="Add_Cancel_Click"/>
</div>
</div>

<div id="Add_Reply" class="FormDiv">
<div class="FormTitle">个人交接</div>
<div class="FormInput">
<div class="FormBody">
<p>
    回复：<asp:Label ID="LabelReply" runat="server" Text=""></asp:Label></p>
    <p><asp:TextBox ID="TextBoxReply" runat="server" TextMode="MultiLine" Height="200px" Width="100%" style="margin:0;padding:0;"></asp:TextBox></p>
<p><span style="font-family:fontawesome !important; color:Red;">&#xf071;</span>&nbsp;<asp:Label ID="ToolTips2" runat="server" Text="字数请不要超过900字！"></asp:Label></p>
</div>
</div>
<div class="FormFooter">
     <asp:Button ID="Add_Dialog_OK" runat="server" Text="确定" CssClass="ConfirmButton" OnClick="AddReply_OK_Click"/>
     <asp:Button ID="Button2" runat="server" Text="取消" CssClass="ConfirmButton" OnClick="Add_Cancel_Click"/>
</div>
</div>

</div>

<!--startprint-->
    <div class="SubMenu">
        <asp:Button ID="Jiaojie" runat="server" Text="日常交接" OnClick="Inform_Click" CssClass="MenuButton"/>
        <asp:Button ID="personal" runat="server" Text="个人交接"  CssClass="MenuSelected"/>
        <asp:Button ID="Genren" runat="server" Text="待办事项" OnClick="Geren_Click" CssClass="MenuButton" />
        <asp:Button ID="Work" runat="server" Text="工作安排" OnClick="Work_Click" CssClass="MenuButton" />
    </div>
    <div class="SubView">
    <div class="View">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" EnableModelValidation="True" Width="100%" AllowPaging="True"  PageSize="10" 
                            OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCommand="GridView1_RowCommand"
                            BorderColor="#A0A0A0" BorderStyle="Solid" BorderWidth="1px" DataKeyNames="ID">
                            <Columns>
                                <asp:TemplateField HeaderText="选择">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="Server" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="shortdate" HeaderText="日期">
                                    <HeaderStyle Width="8%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="to" HeaderText="TO">
                                    <HeaderStyle Width="8%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="content" HeaderText="内容" HtmlEncode="False">
                                    <HeaderStyle Width="51%" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="eng" HeaderText="工程师">
                                    <HeaderStyle Width="8%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="reply" HeaderText="回复" HtmlEncode="False">
                                    <HeaderStyle Width="20%" />
                                    <ItemStyle HorizontalAlign="Left"  ForeColor="Blue"/>
                                </asp:BoundField>
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
                                <asp:LinkButton ID="LinkButtonGo" runat="server" Font-Names="Arial" CommandName="go" ForeColor="#3366FF">GO</asp:LinkButton>
                            </PagerTemplate>
                            <PagerStyle HorizontalAlign="Center" />
                            <HeaderStyle Font-Names="微软雅黑" ForeColor="#3366FF"/>
     </asp:GridView> 
    </div>
    </div>
<!--endprint-->

</asp:Content>

