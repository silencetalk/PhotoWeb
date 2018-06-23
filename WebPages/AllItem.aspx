<%@ Page Language="C#" MasterPageFile="~/WebPages/MasterPage.master" AutoEventWireup="true"
    CodeFile="AllItem.aspx.cs" Inherits="WebPages_AllItem" Title="平行展开项目" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div style="position: relative; z-index: 100;">
        <div id="Add_Dialog" class="FormDiv">
            <div class="FormTitle">
                平行展开项目</div>
            <div class="FormInput">
                <div class="FormBody">
                    <p>
                        <asp:Label ID="ToolTips" runat="server" ForeColor="Red"></asp:Label></p>
                    <p>
                        单元：<asp:DropDownList ID="DropDownListDanyuan" runat="server" CssClass="InputStyle">
                            <asp:ListItem Selected="True">请选择单元</asp:ListItem>
                            <asp:ListItem>Indexer</asp:ListItem>
                            <asp:ListItem>Cleaner</asp:ListItem>
                            <asp:ListItem>DB</asp:ListItem>
                            <asp:ListItem>LC</asp:ListItem>
                            <asp:ListItem>VCD</asp:ListItem>
                            <asp:ListItem>SB</asp:ListItem>
                            <asp:ListItem>IF</asp:ListItem>
                            <asp:ListItem>Buffer</asp:ListItem>
                            <asp:ListItem>Turn Table</asp:ListItem>
                            <asp:ListItem>DEV</asp:ListItem>
                            <asp:ListItem>HB</asp:ListItem>
                            <asp:ListItem>AOI</asp:ListItem>
                            <asp:ListItem>DCS</asp:ListItem>
                            <asp:ListItem>Pump</asp:ListItem>
                        </asp:DropDownList>
                    </p>
                    <p>
                        项目：</p>
                    <p>
                        <asp:TextBox ID="TB_Add_Item" runat="server" TextMode="MultiLine" Height="100px"
                            Width="100%" Style="margin: 0; padding: 0;" Font-Names="Microsoft Yahei" Font-Size="12px"></asp:TextBox></p>
                    <p>
                        备注：</p>
                    <p>
                        <asp:TextBox ID="TB_Add_Beizhu" runat="server" TextMode="MultiLine" Height="100px"
                            Width="100%" Style="margin: 0; padding: 0;" Font-Names="Microsoft Yahei" Font-Size="12px"></asp:TextBox></p>
                    <div style="margin: 10px 0px 0px 0px; padding: 0px;">
                        <div class="status">
                            Status：</div>
                        <div class="switch">
                            <asp:Button ID="Open" runat="server" Text="Open" CssClass="open" OnClick="Open_Click"
                                Visible="true" />
                            <asp:Button ID="Close" runat="server" Text="Close" CssClass="close" OnClick="Close_Click"
                                Visible="false" /></div>
                    </div>
                </div>
            </div>
            <div class="FormFooter">
                <asp:Button ID="Add_Dialog_OK" runat="server" Text="确定" CssClass="ConfirmButton"
                    OnClick="Add_OK_Click" />
                <asp:Button ID="Concel1" runat="server" Text="取消" CssClass="ConfirmButton" OnClick="Add_Cancel_Click" />
            </div>
        </div>
        
        <div id="Update_Dialog" class="FormDiv">
            <div class="FormTitle">
                平行展开项目</div>
            <div class="FormInput">
                <div class="FormBody">
                    <p>
                        <asp:Label ID="Tooltips_Update" runat="server" ForeColor="Red"></asp:Label></p>
                    <p>
                        机台：<asp:Label ID="LB_Update_EQ" runat="server" Text=""></asp:Label>
                    </p>
                    <p>
                        状态：
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server">
                            <asp:ListItem Selected="True" Value="OK">完成</asp:ListItem>
                            <asp:ListItem Value="NO">未完成</asp:ListItem>
                        </asp:RadioButtonList>
                        </p>
                    <p> 日期：<asp:TextBox ID="TB_Update_Date" runat="server" Width="185px" CssClass="TextboxStyle"></asp:TextBox></p>
                    <p>
                        备注：</p>
                    <p>
                        <asp:TextBox ID="TB_Update_Beizhu" runat="server" TextMode="MultiLine" Height="100px"
                            Width="100%" Style="margin: 0; padding: 0;" Font-Names="Microsoft Yahei" Font-Size="12px"></asp:TextBox></p>
                </div>
            </div>
            <div class="FormFooter">
                <asp:Button ID="Button3" runat="server" Text="确定" CssClass="ConfirmButton"
                    OnClick="Update_Click" />
                <asp:Button ID="Button4" runat="server" Text="取消" CssClass="ConfirmButton" OnClick="Update_Cancel_Click" />
            </div>
        </div>
        
    </div>
    
    <div id="SubTitle" class="SubTitle">
        <table style="width: 100%;">
            <tr>
                <td class="SubTitleTable">
                    平行展开项目
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
            
    <!--startprint-->
    <div class="SubMenu">
        <a href="PM.aspx" class="MenuButton">PM</a>
        <a href="Parts.aspx" class="MenuButton">Parts</a>
        <a href="CST.aspx" class="MenuButton">CST</a>
        <a href="AllItem.aspx" class="MenuButton MenuSelected">平行展开项目</a>
    </div>
    
    <div class="SubView">
        <div class="View">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" EnableModelValidation="True" CssClass="gridtable" DataKeyNames="ID"
                AllowPaging="True"  PageSize="8" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCommand="GridView1_RowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="平行展开项目">
                        <ItemTemplate>
                            <div>
                                <asp:CheckBox ID="CheckBox1" runat="Server" />
                                <%#Eval("unit").ToString() %>
                                <%#Eval("eng").ToString() %>
                                <%#Eval("itemdate").ToString() %>
                                <%#Eval("itemstatus").ToString() %><br />
                                <p>
                                    <%#Eval("item") %>
                                </p>
                            </div>
                            <table style="width:96%;">
                                <tr>
                                    <td width="10%">机台</td>
                                    <td width="6%">状态</td>
                                    <td width="10%">时间</td>
                                    <td>备注</td>
                                    <td width="10%">机台</td>
                                    <td width="6%">状态</td>
                                    <td width="10%">时间</td>
                                    <td>备注</td>
                                </tr>
                                <tr>
                                    <td>
                                        PH01:
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="LB_Update" runat="server" CssClass="fontlink" CommandName="dataupdate"
                                                CommandArgument='<%#((GridViewRow)Container).RowIndex +"+PH01" %>'><i class='<%#Eval("PH01status").ToString()=="OK"?"icon-ok":"icon-circle" %>'></i></asp:LinkButton>
                                    </td>
                                    <td>
                                        <%#Eval("PH01date").ToString() %>
                                    </td>
                                    <td>
                                        <%#Eval("PH01beizhu").ToString() %>
                                    </td>
                                    <td>
                                        PH11:
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="fontlink" CommandName="dataupdate"
                                                CommandArgument='<%#((GridViewRow)Container).RowIndex +"+PH11" %>'><i class='<%#Eval("PH11status").ToString()=="OK"?"icon-ok":"icon-circle" %>'></i></asp:LinkButton>
                                    </td>
                                    <td>
                                        <%#Eval("PH11date").ToString() %>
                                    </td>
                                    <td>
                                        <%#Eval("PH11beizhu").ToString() %>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        PH02:
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="LinkButton2" runat="server" CssClass="fontlink" CommandName="dataupdate"
                                                CommandArgument='<%#((GridViewRow)Container).RowIndex +"+PH02" %>'><i class='<%#Eval("PH02status").ToString()=="OK"?"icon-ok":"icon-circle" %>'></i></asp:LinkButton>
                                    </td>
                                    <td>
                                        <%#Eval("PH02date").ToString() %>
                                    </td>
                                    <td>
                                        <%#Eval("PH02beizhu").ToString() %>
                                    </td>
                                    <td>
                                        PH12:
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="LinkButton3" runat="server" CssClass="fontlink" CommandName="dataupdate"
                                                CommandArgument='<%#((GridViewRow)Container).RowIndex +"+PH12" %>'><i class='<%#Eval("PH12status").ToString()=="OK"?"icon-ok":"icon-circle" %>'></i></asp:LinkButton>
                                    </td>
                                    <td>
                                        <%#Eval("PH12date").ToString() %>
                                    </td>
                                    <td>
                                        <%#Eval("PH12beizhu").ToString() %>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        PH03:
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="LinkButton4" runat="server" CssClass="fontlink" CommandName="dataupdate"
                                                CommandArgument='<%#((GridViewRow)Container).RowIndex +"+PH03" %>'><i class='<%#Eval("PH03status").ToString()=="OK"?"icon-ok":"icon-circle" %>'></i></asp:LinkButton>
                                    </td>
                                    <td>
                                        <%#Eval("PH03date").ToString() %>
                                    </td>
                                    <td>
                                        <%#Eval("PH03beizhu").ToString() %>
                                    </td>
                                    <td>
                                        PH13:
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="LinkButton5" runat="server" CssClass="fontlink" CommandName="dataupdate"
                                                CommandArgument='<%#((GridViewRow)Container).RowIndex +"+PH13" %>'><i class='<%#Eval("PH13status").ToString()=="OK"?"icon-ok":"icon-circle" %>'></i></asp:LinkButton>
                                    </td>
                                    <td>
                                        <%#Eval("PH13date").ToString() %>
                                    </td>
                                    <td>
                                        <%#Eval("PH13beizhu").ToString() %>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        PH04:
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="LinkButton6" runat="server" CssClass="fontlink" CommandName="dataupdate"
                                                CommandArgument='<%#((GridViewRow)Container).RowIndex +"+PH04" %>'><i class='<%#Eval("PH04status").ToString()=="OK"?"icon-ok":"icon-circle" %>'></i></asp:LinkButton>
                                    </td>
                                    <td>
                                        <%#Eval("PH04date").ToString() %>
                                    </td>
                                    <td>
                                        <%#Eval("PH04beizhu").ToString() %>
                                    </td>
                                    <td>
                                        PH14:
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="LinkButton7" runat="server" CssClass="fontlink" CommandName="dataupdate"
                                                CommandArgument='<%#((GridViewRow)Container).RowIndex +"+PH14" %>'><i class='<%#Eval("PH14status").ToString()=="OK"?"icon-ok":"icon-circle" %>'></i></asp:LinkButton>
                                    </td>
                                    <td>
                                        <%#Eval("PH14date").ToString() %>
                                    </td>
                                    <td>
                                        <%#Eval("PH14beizhu").ToString() %>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        PH05:
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="LinkButton8" runat="server" CssClass="fontlink" CommandName="dataupdate"
                                                CommandArgument='<%#((GridViewRow)Container).RowIndex +"+PH05" %>'><i class='<%#Eval("PH05status").ToString()=="OK"?"icon-ok":"icon-circle" %>'></i></asp:LinkButton>
                                    </td>
                                    <td>
                                        <%#Eval("PH05date").ToString() %>
                                    </td>
                                    <td>
                                        <%#Eval("PH05beizhu").ToString() %>
                                    </td>
                                    <td>
                                        PH15:
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="LinkButton9" runat="server" CssClass="fontlink" CommandName="dataupdate"
                                                CommandArgument='<%#((GridViewRow)Container).RowIndex +"+PH15" %>'><i class='<%#Eval("PH15status").ToString()=="OK"?"icon-ok":"icon-circle" %>'></i></asp:LinkButton>
                                    </td>
                                    <td>
                                        <%#Eval("PH15date").ToString() %>
                                    </td>
                                    <td>
                                        <%#Eval("PH15beizhu").ToString() %>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        PH06:
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="LinkButton10" runat="server" CssClass="fontlink" CommandName="dataupdate"
                                                CommandArgument='<%#((GridViewRow)Container).RowIndex +"+PH06" %>'><i class='<%#Eval("PH06status").ToString()=="OK"?"icon-ok":"icon-circle" %>'></i></asp:LinkButton>
                                    </td>
                                    <td>
                                        <%#Eval("PH06date").ToString() %>
                                    </td>
                                    <td>
                                        <%#Eval("PH06beizhu").ToString() %>
                                    </td>
                                    <td>
                                        PH16:
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="LinkButton11" runat="server" CssClass="fontlink" CommandName="dataupdate"
                                                CommandArgument='<%#((GridViewRow)Container).RowIndex +"+PH16" %>'><i class='<%#Eval("PH16status").ToString()=="OK"?"icon-ok":"icon-circle" %>'></i></asp:LinkButton>
                                    </td>
                                    <td>
                                        <%#Eval("PH16date").ToString() %>
                                    </td>
                                    <td>
                                        <%#Eval("PH16beizhu").ToString() %>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        PH07:
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="LinkButton12" runat="server" CssClass="fontlink" CommandName="dataupdate"
                                                CommandArgument='<%#((GridViewRow)Container).RowIndex +"+PH07" %>'><i class='<%#Eval("PH07status").ToString()=="OK"?"icon-ok":"icon-circle" %>'></i></asp:LinkButton>
                                    </td>
                                    <td>
                                        <%#Eval("PH07date").ToString() %>
                                    </td>
                                    <td>
                                        <%#Eval("PH07beizhu").ToString() %>
                                    </td>
                                    <td>
                                        PH17:
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="LinkButton13" runat="server" CssClass="fontlink" CommandName="dataupdate"
                                                CommandArgument='<%#((GridViewRow)Container).RowIndex +"+PH17" %>'><i class='<%#Eval("PH17status").ToString()=="OK"?"icon-ok":"icon-circle" %>'></i></asp:LinkButton>
                                    </td>
                                    <td>
                                        <%#Eval("PH17date").ToString() %>
                                    </td>
                                    <td>
                                        <%#Eval("PH17beizhu").ToString() %>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        PH08:
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="LinkButton14" runat="server" CssClass="fontlink" CommandName="dataupdate"
                                                CommandArgument='<%#((GridViewRow)Container).RowIndex +"+PH08" %>'><i class='<%#Eval("PH08status").ToString()=="OK"?"icon-ok":"icon-circle" %>'></i></asp:LinkButton>
                                    </td>
                                    <td>
                                        <%#Eval("PH08date").ToString() %>
                                    </td>
                                    <td>
                                        <%#Eval("PH08beizhu").ToString() %>
                                    </td>
                                    <td>
                                        PH18:
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="LinkButton15" runat="server" CssClass="fontlink" CommandName="dataupdate"
                                                CommandArgument='<%#((GridViewRow)Container).RowIndex +"+PH18" %>'><i class='<%#Eval("PH18status").ToString()=="OK"?"icon-ok":"icon-circle" %>'></i></asp:LinkButton>
                                    </td>
                                    <td>
                                        <%#Eval("PH18date").ToString() %>
                                    </td>
                                    <td>
                                        <%#Eval("PH18beizhu").ToString() %>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        PH09:
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="LinkButton16" runat="server" CssClass="fontlink" CommandName="dataupdate"
                                                CommandArgument='<%#((GridViewRow)Container).RowIndex +"+PH09" %>'><i class='<%#Eval("PH09status").ToString()=="OK"?"icon-ok":"icon-circle" %>'></i></asp:LinkButton>
                                    </td>
                                    <td>
                                        <%#Eval("PH09date").ToString() %>
                                    </td>
                                    <td>
                                        <%#Eval("PH09beizhu").ToString() %>
                                    </td>
                                    <td>
                                        PH19:
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="LinkButton17" runat="server" CssClass="fontlink" CommandName="dataupdate"
                                                CommandArgument='<%#((GridViewRow)Container).RowIndex +"+PH19" %>'><i class='<%#Eval("PH19status").ToString()=="OK"?"icon-ok":"icon-circle" %>'></i></asp:LinkButton>
                                    </td>
                                    <td>
                                        <%#Eval("PH19date").ToString() %>
                                    </td>
                                    <td>
                                        <%#Eval("PH19beizhu").ToString() %>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        PH10:
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="LinkButton18" runat="server" CssClass="fontlink" CommandName="dataupdate"
                                                CommandArgument='<%#((GridViewRow)Container).RowIndex +"+PH10" %>'><i class='<%#Eval("PH10status").ToString()=="OK"?"icon-ok":"icon-circle" %>'></i></asp:LinkButton>
                                    </td>
                                    <td>
                                        <%#Eval("PH10date").ToString() %>
                                    </td>
                                    <td>
                                        <%#Eval("PH10beizhu").ToString() %>
                                    </td>
                                    <td>
                                        PH20:
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="LinkButton19" runat="server" CssClass="fontlink" CommandName="dataupdate"
                                                CommandArgument='<%#((GridViewRow)Container).RowIndex +"+PH20" %>'><i class='<%#Eval("PH20status").ToString()=="OK"?"icon-ok":"icon-circle" %>'></i></asp:LinkButton>
                                    </td>
                                    <td>
                                        <%#Eval("PH20date").ToString() %>
                                    </td>
                                    <td>
                                        <%#Eval("PH20beizhu").ToString() %>
                                    </td>
                                </tr>
                            </table> 
                        </ItemTemplate>
                        <ItemStyle CssClass="griditem" />
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
                    <asp:LinkButton ID="LinkButtonGo" runat="server" Font-Names="Arial" CommandName="go" ForeColor="#3366FF">GO</asp:LinkButton>
                </PagerTemplate>
                <PagerStyle HorizontalAlign="Center" />
                <HeaderStyle CssClass="gridtitle" Font-Names="微软雅黑" ForeColor="#3366FF" />
            </asp:GridView>
        </div>
    </div>
    <!--endprint-->
    
</asp:Content>
