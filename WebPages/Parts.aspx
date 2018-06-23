<%@ Page Language="C#" MasterPageFile="~/WebPages/MasterPage.master" AutoEventWireup="true" CodeFile="Parts.aspx.cs" Inherits="WebPages_Parts" Title="Parts" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

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
                Parts
            </td>
            <td class="ToolsTable">
                <div id="Tools" class="Tools">
                    <button id="Add" type="button" runat="server" onserverclick="Add_Click" class="ToolButton">
                        <span style="font-family: fontawesome !important;">&#xf067;</span>&nbsp;添加</button>
<%--                        <button id="Delete" type="button" runat="server" onserverclick="Delete_Click" class="ToolButton">
                        <span style="font-family: fontawesome !important;">&#xf014;</span>&nbsp;删除</button>
                    <button id="Edit" type="button" runat="server" onserverclick="Edit_Click" class="ToolButton">
                        <span style="font-family: fontawesome !important;">&#xf044;</span>&nbsp;编辑</button>--%>
               <button id="Print" type="button" onclick="Print_Click()" class="ToolButton"><span style="font-family: fontawesome !important;">&#xf02f;</span>&nbsp;打印</button>
                <asp:TextBox ID="TextBoxSearch" runat="server" placeholder="PartsName：" CssClass="ToolInput"></asp:TextBox>
                <button id="Search" type="button" runat="server" onserverclick="Search_Click" class="ToolButton">&nbsp;<i class="icon-search"></i>&nbsp;</button>
                </div>
            </td>
        </tr>
    </table>
</div>
<div style="position: relative; z-index: 100;">
    <div id="Add_Dialog" class="FormDiv">
        <div class="FormTitle">Parts</div>
        <div class="FormInput">
            <div class="FormBody">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>                      
                <table width="100%">
                    <tr>
                        <td colspan="2"><asp:Label ID="ToolTips" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td width="80px">PartsName：</td>
                        <td><asp:TextBox ID="TB_Add_PartsName" runat="server" Width="185px" CssClass="TextboxStyle"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>中文名称：</td>
                        <td><asp:TextBox ID="TB_Add_CHNName" runat="server" Width="185px" CssClass="TextboxStyle"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>公司物料号：</td>
                        <td><asp:TextBox ID="TB_Add_BOENo" runat="server" Width="185px" CssClass="TextboxStyle"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>厂家物料号：</td>
                        <td><asp:TextBox ID="TB_Add_VenderNo" runat="server" Width="185px" CssClass="TextboxStyle"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td> 物料组：</td>
                        <td><asp:TextBox ID="TB_Add_PartsGroup" runat="server" Width="185px" CssClass="TextboxStyle"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>数量：</td>
                        <td>
                       <asp:TextBox ID="TB_Add_Count" runat="server" Width="100px" CssClass="TextboxStyle" Enabled="false"  ime-mode="disabled" onkeypress="if ((event.keyCode<48 || event.keyCode>57)) event.returnValue=false;"></asp:TextBox>
                        <button id="Add_Plus" type="button" runat="server" onserverclick="Plus_Click" class="dialogbtn">&nbsp;<i class="icon-plus"></i>&nbsp;</button>
                        <button id="Add_Minus" type="button" runat="server" onserverclick="Minus_Click" class="dialogbtn">&nbsp;<i class="icon-minus"></i>&nbsp;</button>
                        </td>
                        <td>
                            <asp:RadioButtonList ID="RB_Count" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True">X1</asp:ListItem>
                            <asp:ListItem>X5</asp:ListItem>
                            <asp:ListItem>X10</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                    <td style="vertical-align:top">备注：</td>
                    <td colspan="2">
                        <asp:TextBox ID="TB_Add_Beizhu" runat="server" TextMode="MultiLine" Height="100px" Width="100%" Style="margin: 0; padding: 0;" Font-Names="Microsoft Yahei" Font-Size="12px"></asp:TextBox>
                     </td>
                    </tr>
                </table>
                </ContentTemplate>
                </asp:UpdatePanel> 
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
  <div class="SubMenu">
        <%--<a href="Alarm.aspx" class="MenuButton">Alarm</a>--%>
        <a href="PM.aspx" class="MenuButton">PM</a>
        <a href="Parts.aspx" class="MenuButton MenuSelected">Parts</a>
        <a href="CST.aspx" class="MenuButton">CST</a>
        <a href="AllItem.aspx" class="MenuButton">平行展开项目</a>
    </div>
    <div class="SubView">
        <div class="View">
            <div style="margin: 10px auto 10px auto; width: 100%; text-align: left;">
                <p>
                    PartsGroup：<asp:DropDownList ID="DD_Group" runat="server" CssClass="InputStyle" 
                        AutoPostBack="True" OnSelectedIndexChanged="Load_Changed">
                        <asp:ListItem Selected="True" Text="All" Value="All"></asp:ListItem>
                    </asp:DropDownList>
                </p>
            </div>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
             AllowPaging="True" PageSize="30" OnPageIndexChanging="GridView1_PageIndexChanging" CssClass="gridtable"
                OnRowCommand="GridView1_RowCommand" DataKeyNames="ID,PartsName,CHNName,GroupName,BOENo,VenderNo,Count">
                <Columns>
                    <asp:TemplateField HeaderText="历史">
                        <ItemTemplate>
                            <asp:LinkButton ID="LB_History" runat="server" CssClass="fontlink" CommandName="more"
                                CommandArgument='<%# ((GridViewRow)Container).RowIndex %>'><i class="icon-2x icon-th-list"></i></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle Width="5%" CssClass="gridtitle"/>
                        <ItemStyle CssClass="griditemcenter" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PartsName">
                        <ItemTemplate>
                            <%#Eval("PartsName").ToString()%>               
                        </ItemTemplate>
                        <HeaderStyle Width="20%"  CssClass="gridtitle"/>
                        <ItemStyle CssClass="griditemcenter"/>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="中文名称">
                        <ItemTemplate>                           
                            <%#Eval("CHNName").ToString()%>
                        </ItemTemplate>
                        <HeaderStyle Width="10%"  CssClass="gridtitle"/>
                        <ItemStyle CssClass="griditemcenter"/>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="公司物料号">
                        <ItemTemplate>
                            <%#Eval("BOENo").ToString()%>
                        </ItemTemplate>
                        <HeaderStyle Width="10%" CssClass="gridtitle"/>
                        <ItemStyle CssClass="griditemcenter" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="厂家物料号">
                        <ItemTemplate>
                            <%#Eval("VenderNo").ToString()%>
                        </ItemTemplate>
                        <HeaderStyle Width="10%" CssClass="gridtitle"/>
                        <ItemStyle CssClass="griditemcenter" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="物料组">
                        <ItemTemplate>                            
                            <%#Eval("GroupName").ToString()%>
                        </ItemTemplate>
                        <HeaderStyle Width="10%" CssClass="gridtitle"/>
                        <ItemStyle CssClass="griditemcenter" />
                    </asp:TemplateField>  
                    <asp:BoundField DataField="Beizhu" HeaderText="备注" HtmlEncode="False">
                        <HeaderStyle CssClass="gridtitle"/>                     
                        <ItemStyle CssClass="griditem"/>
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="数量">
                        <HeaderStyle Width="5%" CssClass="gridtitle"/>
                        <ItemTemplate>
                           <%#Eval("Count").ToString()%>
                        </ItemTemplate>
                         <ItemStyle CssClass="griditemcenter"/>
                    </asp:TemplateField>
                    <asp:BoundField DataField="LastDate" HeaderText="最新日期" DataFormatString="{0:yyyy/MM/dd}">
                        <HeaderStyle Width="8%" CssClass="gridtitle"/>
                        <ItemStyle CssClass="griditemcenter"/>
                    </asp:BoundField>
                    <asp:BoundField DataField="Eng" HeaderText="工程师">
                        <HeaderStyle Width="6%" CssClass="gridtitle"/>
                        <ItemStyle CssClass="griditemcenter"/>
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="更新">
                        <ItemTemplate>
                        <asp:LinkButton ID="LB_Add_Update" runat="server" CssClass="fontlink" CommandName="add_update"
                            CommandArgument='<%# ((GridViewRow)Container).RowIndex %>'><i class="icon-2x icon-refresh"></i></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle Width="5%" CssClass="gridtitle"/>
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
                <HeaderStyle Font-Names="微软雅黑" ForeColor="#3366FF" />
            </asp:GridView>
        </div>
    </div>
    <!--endprint-->
</asp:Content>

