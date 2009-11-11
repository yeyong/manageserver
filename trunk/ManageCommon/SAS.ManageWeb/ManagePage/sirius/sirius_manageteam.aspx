<%@ Page Language="C#" AutoEventWireup="true" Inherits="SAS.Sirius.Admin.manageteam" %>
<%@ Register TagPrefix="cc1" Namespace="SAS.Control" Assembly="SAS.Control" %>
<%@ Register Src="../UserControls/PageInfo.ascx" TagName="PageInfo" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />    
    <meta name="keywords" content="天狼星,工作室" />
    <meta name="description" content="天狼星工作室综合管理后台" />
    <title>天狼星工作室综合管理后台-团队信息列表</title>
    <link href="../styles/datagrid.css" type="text/css" rel="stylesheet" />
    <link href="../styles/dntmanager.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" src="../js/common.js"></script>
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
</head>
<body>
     <form id="Form1" method="post" runat="server">
        <uc1:PageInfo ID="info1" runat="server" Icon="information" Text="增加用户组方法有两种:<br />方法1: 进入<a href=&quot;global_addusergroup.aspx&quot;>用户组添加</a>, 增加一个新的用户组,同时编辑该组的其他设置. <br />方法2: 点击下面的相关用户组记录上的 &quot;添加&quot; 链接,系统会用相关用户组的信息作为模板初始化&quot;添加表单&quot;,同时编辑该组的其他设置." />
        <cc1:DataGrid ID="DataGrid1" runat="server" IsFixConlumnControls="true" OnPageIndexChanged="DataGrid_PageIndexChanged" OnSortCommand="Sort_Grid">
            <Columns>
                <asp:TemplateColumn HeaderText="">
                    <itemtemplate>
						<a href="sirius_editteaminfo.aspx?teamID=<%# DataBinder.Eval(Container, "DataItem.teamID").ToString()%>">编辑</a>
						<%# DataGrid1.LoadSelectedCheckBox(DataBinder.Eval(Container, "DataItem.teamID").ToString())%>
					</itemtemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="团队图片">
                    <ItemTemplate>
                        
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
            </Columns>
        </cc1:DataGrid>
        <p style="text-align:right;">
            <cc1:Button ID="EditUserGroup" runat="server" Text=" 提交 "></cc1:Button>&nbsp;&nbsp;         
        </p>
    </form>
    <%=footer%>
</body>
</html>
