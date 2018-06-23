<%@ Page Language="C#" MasterPageFile="~/WebPages/MasterPage.master" AutoEventWireup="true" CodeFile="ProcessReport.aspx.cs" Inherits="WebPages_ProcessReport" Title="工艺日报" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div style="position:relative; z-index:100;">

<div id="Add_Dialog" class="FormDiv">
<div class="FormTitle">个人日报</div>
<div class="FormInput">
<div class="FormBody">
    <p>
    日期：<asp:TextBox ID="TextBoxInputDate" runat="server" Width="68px" CssClass="TextboxStyle"></asp:TextBox></p>
    <p><asp:Label ID="ToolTips" runat="server"></asp:Label></p>
    <p>今日工作：</p>
    <p><asp:TextBox ID="TextBoxToday" runat="server" TextMode="MultiLine" Height="100px" Width="100%" style="margin:0;padding:0;" Font-Names="Microsoft Yahei" Font-Size="12px"></asp:TextBox></p>
    <p>明日工作：</p>
    <p><asp:TextBox ID="TextBoxTomorrow" runat="server" TextMode="MultiLine" Height="100px" Width="100%" style="margin:0;padding:0;" Font-Names="Microsoft Yahei" Font-Size="12px"></asp:TextBox></p>
    <p>工程师：</p>
    <p><asp:TextBox ID="TextBoxEng" runat="server" TextMode="MultiLine" Height="50px" Width="100%" style="margin:0;padding:0;" Font-Names="Microsoft Yahei" Font-Size="12px"></asp:TextBox></p>
    <p>附件：</p>
    <p>标题：<asp:TextBox ID="TextBoxFileTittle" runat="server" Width="200px" CssClass="TextboxStyle"></asp:TextBox></p>
    <p>
    <asp:DropDownList ID="DropDownListFenlei" runat="server" CssClass="InputStyle">
    <asp:ListItem  Selected="True">请选择文档分类</asp:ListItem>
    <asp:ListItem>周报</asp:ListItem>
    <asp:ListItem>设备报告</asp:ListItem>
    <asp:ListItem>工艺报告</asp:ListItem>
    <asp:ListItem>测量数据</asp:ListItem>
    <asp:ListItem>培训资料</asp:ListItem>
    <asp:ListItem>事务资料</asp:ListItem>
    </asp:DropDownList> 
    <asp:FileUpload ID="FileUpload1" runat="server" Width="200px" CssClass="InputStyle"/></p>
    
</div>
</div>
<div class="FormFooter">
     <asp:Button ID="Add_Dialog_OK" runat="server" Text="确定" CssClass="ConfirmButton" OnClick="Add_OK_Click"/>
     <asp:Button ID="Concel1" runat="server" Text="取消" CssClass="ConfirmButton" OnClick="Add_Cancel_Click"/>
</div>
</div>
</div>

<div id="SubTitle" class="SubTitle">
        <table style="width: 100%;">
            <tr>
                <td class="SubTitleTable">
                    日常报告
                </td>
                <td class="ToolsTable">
                    <div id="Tools" class="Tools">
                        <button id="PreDate" type="button" runat="server" onserverclick="PreDate_Click" class="ButtonCycle"><span style="font-family:fontawesome !important;">&#xf060;</span></button>
                        <button id="SetDate" type="button" runat="server" onserverclick="SetDate_Click" class="ToolButton"><span style="font-family:fontawesome !important;">&#xf073;</span><asp:Label ID="LabelDate" runat="server" Text=""></asp:Label></button>
                        <button id="NextDate" type="button" runat="server" onserverclick="NextDate_Click" class="ButtonCycle"><span style="font-family:fontawesome !important;">&#xf061;</span></button>
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

<!--startprint-->
    <div class="SubMenu">
    <asp:Button ID="zhiban" runat="server" Text="值班报告" OnClick="Zhiban_Click" CssClass="MenuButton"/>
    <asp:Button ID="Setup" runat="server" Text="跟机报告"  OnClick="Setup_Click" CssClass="MenuButton"/>
    <asp:Button ID="Process" runat="server" Text="个人报告" CssClass="MenuSelected"/>
    </div>
    <div class="SubView">
    <div style="width:99%;text-align:left;margin: 5px 0px 10px 0px;">
    <asp:DropDownList ID="DropDownListEng" runat="server" CssClass="InputStyle" AutoPostBack="true" OnSelectedIndexChanged="Eng_Changed" >
    <asp:ListItem  Selected="True">ALL</asp:ListItem>
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
    </div>
    <div class="View">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            EnableModelValidation="True" Width="100%"
                            BorderColor="#A0A0A0" BorderStyle="Solid" 
            BorderWidth="1px" DataKeyNames="ID">
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
                                <asp:BoundField DataField="eng" HeaderText="工程师" HtmlEncode="False">
                                    <HeaderStyle Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="today" HeaderText="今日工作" HtmlEncode="False">
                                    <HeaderStyle Width="37%" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="tomorrow" HeaderText="明日工作" HtmlEncode="False">
                                    <HeaderStyle Width="37%" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="附件">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="fujian" runat="server" Text='<%#Eval("title") %>' Font-Size="Small" Visible='<%#Eval("fujian").ToString()==""?false:true%>' NavigateUrl='<%#"DownLoad.aspx?File="+Eval("fujian") %>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <HeaderStyle Width="8%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle Font-Names="微软雅黑" ForeColor="#3366FF"/>
     </asp:GridView> 
     
    </div>
    </div>
<!--endprint-->

</asp:Content>

