<%@ Page Language="C#" MasterPageFile="~/WebPages/MasterPage.master" AutoEventWireup="true" CodeFile="PMList.aspx.cs" Inherits="WebPages_PMList" Title="PM List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="SubTitle" class="SubTitle">
        <table style="width: 100%;">
            <tr>
                <td class="SubTitleTable">
                    PM List
                </td>
                <td class="ToolsTable">
                    <div id="Tools" class="Tools">
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
    <div style="position: relative; z-index: 100;">
        <div id="Add_Dialog" class="FormDiv">
            <div class="FormTitle">
                PM</div>
            <div class="FormInput">
                <div class="FormBody">
                    <p> <asp:Label ID="ToolTips" runat="server"></asp:Label></p>
                    <p>
                        <asp:Label ID="LB_Add_Items" runat="server" Text=""></asp:Label>
                    </p>
                    <p>
                        时间：<asp:TextBox ID="TB_Add_Date" runat="server" Width="185px" CssClass="TextboxStyle"></asp:TextBox></p>
                    <p>
                        备注：</p>
                    <p>
                        <asp:TextBox ID="TB_Add_Beizhu" runat="server" TextMode="MultiLine" Height="100px"
                            Width="100%" Style="margin: 0; padding: 0;" Font-Names="Microsoft Yahei" Font-Size="12px"></asp:TextBox></p>
                     <p>上传附件：<asp:FileUpload ID="FileUpload1" runat="server" CssClass="InputStyle"/></p>
                </div>
            </div>
            <div class="FormFooter">
                <asp:Button ID="Add_Dialog_OK" runat="server" Text="确定" CssClass="ConfirmButton"
                    OnClick="Add_OK_Click" />
                <asp:Button ID="Concel1" runat="server" Text="取消" CssClass="ConfirmButton" OnClick="Add_Cancel_Click" />
            </div>
        </div>
        
        
         <div id="dlg_ok" class="FormDiv">
            <div class="FormTitle"></div>
            <div class="FormInput">
                <div class="FormBody">
                    <p><asp:Label ID="LB_OK" runat="server" Text=""></asp:Label></p>
                </div>
            </div>
            <div class="FormFooter">
                <asp:Button ID="Close" runat="server" Text="关闭" CssClass="ConfirmButton" OnClick="Add_Cancel_Click" />
            </div>
        </div>
    </div>
    <!--startprint-->
<%--    <div class="SubMenu">
        <asp:Button ID="Alarm" runat="server" Text="Alarm" OnClick="Alarm_Click" CssClass="MenuButton" />
        <asp:Button ID="Parts" runat="server" Text="Parts" OnClick="Parts_Click"
            CssClass="MenuButton" />
        <asp:Button ID="CST" runat="server" Text="CST" OnClick="CST_Click" CssClass="MenuButton" />
        <asp:Button ID="PM" runat="server" Text="PM" OnClick="PM_Click" CssClass="MenuSelected" />
    </div>--%>
 
        <div class="View">
            <div style="margin: 10px auto 10px auto; width: 100%; text-align: left;">
                <p>
                    <asp:Label ID="LB_Title" runat="server" Text="" Font-Size="16px"></asp:Label>
                    <a href="PM.aspx" class="fontlink"><i class="icon-2x icon-hand-right"></i></a>
<%--                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="Page_Back">LinkButton</asp:LinkButton>--%>
                </p>
            </div>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
                Width="100%" AllowPaging="True" PageSize="30" OnPageIndexChanging="GridView1_PageIndexChanging"
                OnRowCommand="GridView1_RowCommand" BorderColor="#A0A0A0" BorderStyle="Solid"
                BorderWidth="1px" DataKeyNames="ID,EQ,unit,item,position" OnRowUpdating="GridView1_RowUpdating">
                <Columns>
                    <asp:TemplateField HeaderText="选择">
                        <ItemTemplate>
                           <asp:CheckBox ID="CheckBox1" runat="Server" />
                        </ItemTemplate>
                        <HeaderStyle Width="10%" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="机台">
                        <ItemTemplate>
                            <asp:Label ID="LB_Data_Eq" runat="server"><%#Eval("EQ").ToString()%></asp:Label><br />
                            <asp:Label ID="LB_Data_Unit" runat="server"><%#Eval("unit").ToString()%></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="10%" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="项目">
                        <ItemTemplate>
                            <asp:Label ID="LB_Data_Xiangmu" runat="server"><%#Eval("item").ToString()%></asp:Label><br />
                            <asp:Label ID="LB_Data_Position" runat="server"><%#Eval("position").ToString()%></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="10%" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="last" HeaderText="最新日期" DataFormatString="{0:yyyy/MM/dd}">
                        <HeaderStyle Width="10%" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="beizhu" HeaderText="备注" HtmlEncode="False">
                        <HeaderStyle Width="40%" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
<%--                    <asp:TemplateField HeaderText="周期/剩余">
                        <HeaderStyle Width="10%" />
                        <ItemTemplate>
                            <asp:Label ID="LB_Data_Zhouqi" runat="server"><%#Eval("zhouqi").ToString()%>/</asp:Label>
                            <asp:Label ID="LabelTianshu" runat="server" Text='<%# ((DateTime)Eval("duedate")-DateTime.Now ).Days.ToString()+"  天" %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:BoundField DataField="eng" HeaderText="工程师">
                        <HeaderStyle Width="10%" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="附件">
                        <ItemTemplate>
                            <asp:HyperLink ID="fujian" runat="server" Font-Size="Small" Visible='<%#Eval("fujian").ToString()==""?false:true%>'
                                NavigateUrl='<%#"DownLoad.aspx?File="+Eval("fujian") %>' CssClass="fontlink" ><i class="icon-2x icon-cloud-download"></i></asp:HyperLink>
                        </ItemTemplate>
                        <HeaderStyle Width="10%" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
<%--                    <asp:TemplateField HeaderText="更新">
                        <ItemTemplate>
                            <asp:LinkButton ID="LB_Update" runat="server" CssClass="fontlink" CommandName="dataupdate"
                                CommandArgument='<%# ((GridViewRow)Container).RowIndex %>'><i class="icon-2x icon-refresh"></i></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle Width="7%" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>--%>
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
        </div>
    <!--endprint-->
</asp:Content>

