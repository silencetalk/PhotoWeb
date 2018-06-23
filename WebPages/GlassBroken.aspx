<%@ Page Language="C#" MasterPageFile="~/WebPages/MasterPage.master" AutoEventWireup="true" CodeFile="GlassBroken.aspx.cs" Inherits="WebPages_GlassBroken" Title="GlassBroken" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div id="imgdiv" class="zoomimg" onclick="$(this).hide();">
    <img id="zoomimg" alt="map" src="" style="width:100%;height:100%"/>
</div>



<div style="position:relative; z-index:100;">
<div id="Add_Dialog" class="FormDiv">
<div class="FormTitle">Glass Broken</div>
<div class="FormInput">
<div class="FormBody">
    <p>
    机台：<asp:DropDownList ID="DropDownListJitai" runat="server" Font-Names="Microsoft Yahei" Font-Size="12px">
    <asp:ListItem  Selected="True">请选择机台</asp:ListItem>
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
     单元：<asp:DropDownList ID="DropDownListDanyuan" runat="server" CssClass="InputStyle">
    <asp:ListItem  Selected="True">请选择单元</asp:ListItem>
					<asp:ListItem>Indexer</asp:ListItem>
					<asp:ListItem>Cleaner</asp:ListItem>
					<asp:ListItem>DB</asp:ListItem>
					<asp:ListItem>LC</asp:ListItem>
					<asp:ListItem>VCD</asp:ListItem>
					<asp:ListItem>SB</asp:ListItem>
					<asp:ListItem>IF Buffer</asp:ListItem>
					<asp:ListItem>Turn Table</asp:ListItem>
					<asp:ListItem>DEV</asp:ListItem>
					<asp:ListItem>HB</asp:ListItem>
					<asp:ListItem>OVEN</asp:ListItem>
					<asp:ListItem>Bypass</asp:ListItem>
					<asp:ListItem>AOI</asp:ListItem>
					<asp:ListItem>Dry Pump</asp:ListItem>
    </asp:DropDownList></p>
    <p>日期：<asp:TextBox ID="TextBoxDate" runat="server" Width="68px" CssClass="TextboxStyle"></asp:TextBox></p>
    <p>
    Lot：<asp:TextBox ID="TextBoxLot" runat="server" Width="120px" CssClass="TextboxStyle"></asp:TextBox>
    Layer：<asp:DropDownList ID="DropDownListLayer" runat="server" Font-Names="Microsoft Yahei" Font-Size="12px">
    <asp:ListItem  Selected="True">请选择Layer</asp:ListItem>
					<asp:ListItem>1st ITO</asp:ListItem>
					<asp:ListItem>GATE</asp:ListItem>
					<asp:ListItem>HGA</asp:ListItem>
					<asp:ListItem>ACT</asp:ListItem>
					<asp:ListItem>SD</asp:ListItem>
					<asp:ListItem>SDT</asp:ListItem>
					<asp:ListItem>ORGANIC</asp:ListItem>
					<asp:ListItem>PVX</asp:ListItem>
					<asp:ListItem>2nd ITO</asp:ListItem>
    </asp:DropDownList>
    </p>
    <p>
    前工序1：<asp:DropDownList ID="DropDownListbefore1" runat="server" Font-Names="Microsoft Yahei" Font-Size="12px">
                    <asp:ListItem  Selected="True">Sputter</asp:ListItem>
					<asp:ListItem>PECVD</asp:ListItem>
					<asp:ListItem>WE</asp:ListItem>
					<asp:ListItem>DE</asp:ListItem>
					<asp:ListItem>检测设备</asp:ListItem>
    </asp:DropDownList>
    前工序2：<asp:DropDownList ID="DropDownListbefore2" runat="server" Font-Names="Microsoft Yahei" Font-Size="12px">
				    <asp:ListItem  Selected="True">Sputter</asp:ListItem>
					<asp:ListItem>PECVD</asp:ListItem>
					<asp:ListItem>WE</asp:ListItem>
					<asp:ListItem>DE</asp:ListItem>
					<asp:ListItem>检测设备</asp:ListItem>
    </asp:DropDownList>
    </p>
    <p>Reson：</p>
    <p><asp:TextBox ID="TextBoxReason" runat="server" TextMode="MultiLine" Height="100px" Width="100%" style="margin:0;padding:0;" Font-Names="Microsoft Yahei" Font-Size="12px"></asp:TextBox></p>
    <p>Map：
    <asp:FileUpload ID="FileUpload2" runat="server" Width="200px" CssClass="InputStyle"/></p>  
     <p>附件：
         <asp:FileUpload ID="FileUpload1" runat="server" Width="200px" CssClass="InputStyle"/></p>  
    <p><asp:Label ID="ToolTips" runat="server"></asp:Label></p>
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
                    设备履历
                </td>
                <td class="ToolsTable">
                    <div id="Tools" class="Tools">
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
    <asp:Button ID="Alarm" runat="server" Text="Alarm" OnClick="Alarm_Click" CssClass="MenuButton"/>
    <asp:Button ID="GlassBroken" runat="server" Text="GlassBroken"  CssClass="MenuSelected"/>
    </div>
    <div class="SubView">
  <%--  <button id="fullscreen" onclick="fullscreenclick()"><span style="font-family:fontawesome !important;">&#xf0b2;</span></button>--%>
    <div id="View" class="View">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            EnableModelValidation="True" Width="100%"
             BorderColor="#A0A0A0" BorderStyle="Solid" 
            BorderWidth="1px" DataKeyNames="ID" 
            onpageindexchanging="GridView1_PageIndexChanging" 
            onrowcommand="GridView1_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="选择">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="Server" /><br />
                                        <asp:Label ID="Labeleq" runat="server"><%#Eval("jitai").ToString() %></asp:Label><br />
                                        <asp:Label ID="Labeldanyuan" runat="server"><%#Eval("danyuan").ToString() %></asp:Label><br />
                                        <asp:Label ID="Labeleng" runat="server"><%# Eval("eng").ToString() %></asp:Label><br />
                                        <asp:Label ID="Labeldate" runat="server"><%#Eval("shortdate").ToString() %></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField> 
                                <asp:BoundField DataField="lot" HeaderText="Lot">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="layer" HeaderText="Layer">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                 <asp:TemplateField HeaderText="前工序">
                                    <ItemTemplate>
                                        <asp:Label ID="Labelbefore1" runat="server"><%#Eval("before1").ToString() %></asp:Label><br />
                                        <asp:Label ID="Labelbefore2" runat="server"><%#Eval("before2").ToString() %></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField> 
                                <asp:BoundField DataField="reason" HeaderText="产生原因及处理方法" HtmlEncode="False">
                                    <HeaderStyle Width="30%" />
                                    <ItemStyle HorizontalAlign="Left" />
                                 </asp:BoundField>
                                  <asp:TemplateField HeaderText="Map">
                                  <ItemTemplate>
                                      <asp:Image ID="Image1" runat="server"  ImageUrl='<%#Eval("map") %>' Visible='<%#Eval("map").ToString()==""?false:true %>' Width="139px" Height="104px" onclick="ImgZoom(this)" style="cursor:url(../App_Themes/Main/big.cur)"/>
                                   </ItemTemplate>
                                    <HeaderStyle Width="20%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="附件">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="file" runat="server" Font-Size="Small"  NavigateUrl='<%#"DownLoad.aspx?File="+Eval("fujian") %>' CssClass="fontlink" ><span style="font-family:fontawesome !important; font-size:20px;">&#xf0ed;</span></asp:HyperLink>
                                    </ItemTemplate>
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle HorizontalAlign="Center" />
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
                            <HeaderStyle Font-Names="Microsoft Yahei" ForeColor="#3366FF"/>
     </asp:GridView> 
    </div>
    </div>
<!--endprint-->



</asp:Content>

