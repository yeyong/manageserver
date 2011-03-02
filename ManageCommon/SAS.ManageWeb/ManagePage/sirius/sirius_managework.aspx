<%@ Page Language="C#" CodeBehind="sirius_managework.aspx.cs" Inherits="SAS.ManageWeb.ManagePage.sirius_managework" %>
<%@ Register TagPrefix="cc1" Namespace="SAS.Control" Assembly="SAS.Control" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />    
    <meta name="keywords" content="天狼星,工作室" />
    <meta name="description" content="天狼星工作室综合管理后台" />
    <title>天狼星工作室综合管理后台-团队成果信息列表</title>
    <link href="../styles/datagrid.css" type="text/css" rel="stylesheet" />
    <link href="../styles/dntmanager.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" src="../js/common.js"></script>
    <script type="text/javascript" src="../../javascript/common.js"></script>
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
</head>
<body>
     <form id="Form1" method="post" runat="server">
        <cc1:DataGrid ID="DataGrid1" runat="server" IsFixConlumnControls="true" OnPageIndexChanged="DataGrid_PageIndexChanged" OnSortCommand="Sort_Grid">
            <Columns>
                <asp:TemplateColumn HeaderText="">
                    <itemtemplate>
						<a href="sirius_editwork.aspx?wid=<%# DataBinder.Eval(Container, "DataItem.id").ToString()%>">编辑</a>
						<%# DataGrid1.LoadSelectedCheckBox(DataBinder.Eval(Container, "DataItem.id").ToString())%>
					</itemtemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="成果图片">
                    <ItemTemplate>
                        &nbsp;<span id="<%# DataBinder.Eval(Container, "DataItem.name").ToString() %>" onmouseover="showMenu(this.id, 0, 0, 1, 0);" style="font-weight:bold">
                            <img src="../images/eye.gif" style="vertical-align:middle" />
                        </span>
                        <div id="<%# DataBinder.Eval(Container, "DataItem.name").ToString() %>_menu" style="display:none">
							    <img src="<%# DataBinder.Eval(Container, "DataItem.img").ToString() %>" onerror="this.src='../../images/common/none.gif'" />
							</div>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="id" SortExpression="id" HeaderText="成果ID" Visible="false"></asp:BoundColumn>
                <asp:BoundColumn DataField="name" SortExpression="name" HeaderText="成果名称"></asp:BoundColumn>
                <asp:BoundColumn DataField="url" SortExpression="url" HeaderText="成果链接地址"></asp:BoundColumn>
                <asp:BoundColumn DataField="start" SortExpression="start" HeaderText="开始时间" readonly="true"></asp:BoundColumn>
                <asp:BoundColumn DataField="end" SortExpression="end" HeaderText="结束时间" readonly="true"></asp:BoundColumn>
            </Columns>
        </cc1:DataGrid>
        <p style="text-align:right;">
            <cc1:Button ID="EditUserGroup" runat="server" Text=" 提交 "></cc1:Button>&nbsp;&nbsp;         
        </p>
    </form>
    <%=footer%>
</body>
</html>
