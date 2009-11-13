<%@ Page Language="C#" AutoEventWireup="true" Inherits="SAS.Sirius.Admin.manageteam" %>
<%@ Register TagPrefix="cc1" Namespace="SAS.Control" Assembly="SAS.Control" %>
<%@ Register Src="../UserControls/PageInfo.ascx" TagName="PageInfo" TagPrefix="uc1" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />    
    <meta name="keywords" content="天狼星,工作室" />
    <meta name="description" content="天狼星工作室综合管理后台" />
    <title>天狼星工作室综合管理后台-团队信息列表</title>
    <link href="../styles/datagrid.css" type="text/css" rel="stylesheet" />
    <link href="../styles/dntmanager.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" src="../js/common.js"></script>
    <script type="text/javascript" src="../../javascript/common.js"></script>
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
</head>
<body>
     <form id="Form1" method="post" runat="server">
        <uc1:PageInfo ID="info1" runat="server" Icon="information" Text="增加用户组方法有两种:<br />方法1: 进入<a href=&quot;global_addusergroup.aspx&quot;>用户组添加</a>, 增加一个新的用户组,同时编辑该组的其他设置. <br />方法2: 点击下面的相关用户组记录上的 &quot;添加&quot; 链接,系统会用相关用户组的信息作为模板初始化&quot;添加表单&quot;,同时编辑该组的其他设置." />
        <cc1:DataGrid ID="DataGrid1" runat="server" IsFixConlumnControls="true" OnPageIndexChanged="DataGrid_PageIndexChanged" OnSortCommand="Sort_Grid">
            <Columns>
                <asp:TemplateColumn HeaderText="">
                    <itemtemplate>
						<a href="sirius_editteam.aspx?teamID=<%# DataBinder.Eval(Container, "DataItem.teamID").ToString()%>">编辑</a>
						<%# DataGrid1.LoadSelectedCheckBox(DataBinder.Eval(Container, "DataItem.teamID").ToString())%>
					</itemtemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="团队图片">
                    <ItemTemplate>
                        &nbsp;<span id="<%# DataBinder.Eval(Container, "DataItem.name").ToString() %>" onmouseover="showMenu(this.id, 0, 0, 1, 0);" style="font-weight:bold">
                            <img src="../images/eye.gif" style="vertical-align:middle" />
                        </span>
                        <div id="<%# DataBinder.Eval(Container, "DataItem.name").ToString() %>_menu" style="display:none">
							    <img src="<%# DataBinder.Eval(Container, "DataItem.imgs").ToString() %>" onerror="this.src='../../images/common/none.gif'" />
							</div>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="teamID" SortExpression="teamID" HeaderText="团队ID" Visible="false"></asp:BoundColumn>
                <asp:BoundColumn DataField="name" SortExpression="name" HeaderText="团队名称"></asp:BoundColumn>
                <asp:BoundColumn DataField="teamdomain" SortExpression="teamdomain" HeaderText="团队域名" ItemStyle-Width="200px"></asp:BoundColumn>
                <asp:BoundColumn DataField="displayorder" SortExpression="displayorder" HeaderText="显示顺序" ItemStyle-Width="65px"></asp:BoundColumn>
                <asp:BoundColumn DataField="buildDate" SortExpression="buildDate" HeaderText="成立时间" readonly="true"></asp:BoundColumn>
                <asp:BoundColumn DataField="createDate" SortExpression="createDate" HeaderText="创建时间" readonly="true"></asp:BoundColumn>
                <asp:BoundColumn DataField="updateDate" SortExpression="updateDate" HeaderText="修改时间" readonly="true"></asp:BoundColumn>
                <asp:BoundColumn DataField="pageviews" SortExpression="pageviews" HeaderText="查看次数" HeaderStyle-Width="65px" readonly="true"></asp:BoundColumn>                
                <asp:BoundColumn DataField="creater" SortExpression="creater" HeaderText="创建人" readonly="true"></asp:BoundColumn>
                <asp:TemplateColumn HeaderText="状态">
                    <ItemTemplate>
                        <%#DataBinder.Eval(Container, "DataItem.stutas").ToString() == "1" ? "启用" : "停用"%>
                    </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
        </cc1:DataGrid>
        <p style="text-align:right;">
            <cc1:Button ID="EditUserGroup" runat="server" Text=" 提交 "></cc1:Button>&nbsp;&nbsp;         
        </p>
    </form>
    <%=footer%>
</body>
</html>
