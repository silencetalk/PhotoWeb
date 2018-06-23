<%@ Page Title="首页" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true"
    CodeFile="Index.aspx.cs" Inherits="Index_Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="SubTitle" class="SubTitle">
        <table style="width: 100%;">
            <tr>
                <td class="SubTitleTable">
                    首页
                </td>
                <td class="ToolsTable">
                    <div id="Tools" class="Tools">
                        <button id="BtAddTooltips" type="button" onclick="ShowDialog('dlg_Tips');" class="ToolButton"><i class="icon-tag"></i>&nbsp;添加提示信息</button>
                        <button id="Add" type="button" runat="server" onserverclick="Add_Click" class="ToolButton"><span style="font-family:fontawesome !important;">&#xf067;</span>&nbsp;添加</button>
                        <button id="Delete" type="button" runat="server" onserverclick="Delete_Click" class="ToolButton">
                            <span style="font-family:fontawesome !important;">&#xf014;</span>&nbsp;删除</button>
                        <button id="Edit" type="button" onserverclick="Edit_Click" class="ToolButton" runat="server">
                            <span style="font-family:fontawesome !important;">&#xf044;</span>&nbsp;编辑</button>
                         <button id="Print" type="button" onclick="Print_Click()" class="ToolButton">
                            <span style="font-family:fontawesome !important;">&#xf02f;</span>&nbsp;打印</button>
                        <asp:TextBox ID="TextBoxSearch" runat="server" onserverclick="SearchBox_Click" placeholder="关键词：" CssClass="ToolInput"></asp:TextBox>
                        <button id="Search" type="button" runat="server" onserverclick="Search_Click" class="SearchButton">
                            &nbsp;<span style="font-family:fontawesome !important;">&#xf002;</span>&nbsp;</button>
                    </div>
                </td>
            </tr>
        </table>
    </div>
<div style="position:relative; z-index:100;">
<div id="Add_Dialog" class="FormDiv">
 <div class="FormTitle">发表公告</div>
 <div class="FormInput">
 <div class="FormBody">  
  <p><asp:TextBox ID="TextBoxInput" runat="server" Height="200px" TextMode="MultiLine" Width="100%" style="margin:0;padding:0;" Font-Names="Microsoft Yahei" Font-Size="12px"></asp:TextBox></p> 
  <p><span style="font-family:fontawesome !important; color:Red;">&#xf071;</span>&nbsp;<asp:Label ID="ToolTips" runat="server" Text="字数请不要超过900字！"></asp:Label></p>
 </div>
 </div>
 <div class="FormFooter">
     <asp:Button ID="AddGonggao" runat="server" Text="确定" CssClass="ConfirmButton" OnClick="AddConfirm_Click"/>
     <asp:Button ID="Concel" runat="server" Text="取消" CssClass="ConfirmButton"/>
 </div>
 </div>
  <div id="dlg_Tips" class="FormDiv">
        <div class="FormTitle">添加信息提示</div>
        <div class="FormInput">
            <div class="FormBody"> 
            <p>关键词：<asp:TextBox ID="TB_TipsKeyword" runat="server" Width="120px" CssClass="TextboxStyle" onfocus="$('#Tooltips_ErrorMessage').hide();"></asp:TextBox>
               目标页面：<asp:DropDownList ID="DD_ReturnUrl" runat="server" CssClass="InputStyle">
                        <asp:ListItem Value="PM.aspx">PM</asp:ListItem>
                        <asp:ListItem Value="Parts.aspx">Parts</asp:ListItem>
                        <asp:ListItem Value="Alarm.aspx">Alarm</asp:ListItem>
                     </asp:DropDownList>
            </p>
            <p>描述：</p>
            <p><asp:TextBox ID="TB_TipsDescription" runat="server" Height="200px" TextMode="MultiLine" Width="100%" style="margin:0;padding:0;" Font-Names="Microsoft Yahei" Font-Size="12px" onfocus="$('#Tooltips_ErrorMessage').hide();"></asp:TextBox></p>
            <p><span id="Tooltips_ErrorMessage" style="display:none">
                <asp:Label ID="Label_ErrorMessage_Tooltips" runat="server" Text="" ForeColor="Red"></asp:Label>
               </span></p>          
            </div>
        </div>
        <div class="FormFooter">
            <asp:Button ID="Bt_TooltipsAdd" runat="server" Text="确定" CssClass="ConfirmButton" OnClick="AddTooltips_Click"/>
            <button id="Bt_TooltipsCancel" class="ConfirmButton" onclick="HideDialog('dlg_Tips');">取消</button>
        </div>
    </div>
 </div>    
 <!--startprint-->  
    <div class="Content">
        <table style="width: 100%;padding:0;margin:10px auto;">
            <tr>
                <td class="ContentLeft">
                    <div class="DataTitle">科室公告</div>
                    <div class="DataView">
                    <div style="margin:0px auto 0px auto;width:98%;">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
                            Width="100%" AllowPaging="True"  PageSize="5" OnPageIndexChanging="GridView1_PageIndexChanging"
                            OnRowCommand="GridView1_RowCommand" BorderColor="#A0A0A0"
                            BorderStyle="Solid" BorderWidth="1px" DataKeyNames="ID">
                            <Columns>
                                <asp:TemplateField HeaderText="选择">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="Server" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="Shortdate" HeaderText="日期">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Content" HeaderText="内容" HtmlEncode="False">
                                    <HeaderStyle Width="75%" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Eng" HeaderText="宣导人">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle HorizontalAlign="Center" />
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
                                <asp:LinkButton ID="LinkButtonGo" runat="server" Font-Names="Arial" CommandName="go"
                                    ForeColor="#3366FF">GO</asp:LinkButton>
                            </PagerTemplate>
                            <PagerStyle HorizontalAlign="Center" />
                            <HeaderStyle Font-Names="微软雅黑" ForeColor="#3366FF"/>
                        </asp:GridView>
                        </div>
                    </div>
                </td>
                <td class="ContentRight">
                    <%--<div class="Aside">
                        <div class="DataTitle">最近更新</div>
                        <div class="DataView">
                            <asp:DataList ID="DataList_LastUpdate" runat="server" Width="100%">
                                <ItemTemplate>
                                    <%#Eval("UpdateItem").ToString()%>：<%#Eval("UpdateCount").ToString()%>
                                    <%--<table style="width:100%;text-align:left;padding:0 10px;">
                                        <tr>
                                            <td style="width:40%;text-align:right;"><%#Eval("UpdateItem").ToString()%></td>
                                            <td><%#Eval("UpdateCount").ToString()%></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:DataList>
                        </div>
                    </div>--%>
                    <div class="Aside">
                        <div class="DataTitle">
                            最新资料</div>
                        <div class="DataView">
                        <div style="margin:0px auto 0px auto;width:100%;">
                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
                            Width="100%" BorderColor="#A0A0A0" BorderStyle="Solid" BorderWidth="1px" DataKeyNames="ID">
                            <Columns>
                                <asp:BoundField DataField="eng" HeaderText="工程师" HtmlEncode="false">
                                    <HeaderStyle Width="20%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                 <asp:TemplateField HeaderText="下载">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="file" runat="server" Font-Size="Small"  NavigateUrl='<%#"DownLoad.aspx?File="+Eval("path") %>'><%#Eval("filename") %></asp:HyperLink>
                                    </ItemTemplate>
                                    <HeaderStyle Width="80%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>                           
                             </Columns>
                            <PagerStyle HorizontalAlign="Center" />
                            <HeaderStyle Font-Names="微软雅黑" ForeColor="#3366FF"/>
                        </asp:GridView>
                        </div>
                        </div>
                    </div>
                    <div class="Aside">
                        <div class="DataTitle">设备状况</div>
                        <div class="DataView">
                           <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" EnableModelValidation="True" Width="100%"  BorderStyle="Solid"  
                                    BorderWidth="1px" DataKeyNames="ID"  BorderColor="#A0A0A0" onrowupdating="GridView3_RowUpdating">
                            <Columns>
                                <asp:BoundField DataField="eq" HeaderText="机台">
                                    <HeaderStyle Width="25%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="item" HeaderText="项目" >
                                    <HeaderStyle Width="25%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="到期日期">
                                      <ItemTemplate>
                                         <asp:Label ID="LB_DateLast" runat="server" Text='<%#((DateTime)Eval("duedate")).ToString("yyyy/MM/dd")%>' 
                                           ForeColor='<%# ((DateTime)Eval("duedate")-DateTime.Now ).Days<0?System.Drawing.Color.Red:System.Drawing.Color.Black%>'></asp:Label>
                                      </ItemTemplate>
                                      <HeaderStyle Width="50%" />
                                      <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
<%--                                <asp:BoundField DataField="duedate" HeaderText="到期日期" DataFormatString="{0:yyyy/MM/dd}">
                                    <HeaderStyle Width="50%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>--%>
                            </Columns>
                            <PagerStyle HorizontalAlign="Center" />
                            <HeaderStyle Font-Names="微软雅黑" ForeColor="#3366FF"/>
                         </asp:GridView> 
                         </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
 <!--endprint-->
</asp:Content>
