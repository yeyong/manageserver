<%@ Page Language="C#" CodeBehind="global_announcegrid.aspx.cs" Inherits="SAS.ManageWeb.ManagePage.global_announcegrid" %>
<%@ Register TagPrefix="yy" Namespace="SAS.Control" Assembly="SAS.Control"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
<title>公告列表</title>
<link href="../styles/datagrid.css" type="text/css" rel="stylesheet" />
<link href="../styles/dntmanager.css" type="text/css" rel="stylesheet" />
<link href="../styles/calendar.css" type="text/css" rel="stylesheet" />
<script type="text/javascript" src="../js/common.js"></script>
<script type="text/javascript">
    function Check(form) {
        CheckAll(form);
        checkedEnabledButton(form, 'id', 'DelRec');
    }
</script>
<meta http-equiv="X-UA-Compatible" content="IE=7" />
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <yy:DataGrid ID="DataGrid1" runat="server" OnPageIndexChanged="DataGrid_PageIndexChanged" OnSortCommand="Sort_Grid">
		<Columns>
			<asp:TemplateColumn HeaderText="<input title='选中/取消' onclick='Check(this.form)' type='checkbox' name='chkall' id='chkall' />">
				<HeaderStyle Width="20px" />
				<ItemTemplate>
						<input id="id" onclick="checkedEnabledButton(this.form,'id','DelRec')" type="checkbox" value="<%# DataBinder.Eval(Container, "DataItem.id").ToString() %>" name="id" />
					</ItemTemplate>
			</asp:TemplateColumn>
			<asp:TemplateColumn HeaderText="">
				<ItemTemplate>
						<a href="global_editannounce.aspx?id=<%# DataBinder.Eval(Container, "DataItem.id").ToString()%>">编辑</a>
					</ItemTemplate>
			</asp:TemplateColumn>
			<asp:BoundColumn DataField="ID" SortExpression="id" HeaderText="公告ID [递增]" Visible="false" />
			<asp:BoundColumn DataField="poster" SortExpression="poster" HeaderText="发布者用户名" />
			<asp:BoundColumn DataField="title" SortExpression="title" HeaderText="公告标题" ItemStyle-HorizontalAlign="Left"/>
			<asp:BoundColumn DataField="displayorder" SortExpression="displayorder" HeaderText="显示顺序" />
			<asp:BoundColumn DataField="starttime" SortExpression="starttime" HeaderText="起始时间" />
			<asp:BoundColumn DataField="endtime" SortExpression="endtime" HeaderText="结束时间" />
		</Columns>
	</yy:DataGrid>
	<p style="text-align:right;">
		<button type="button" class="ManagerButton" onclick="javascript:window.location.href='global_addannounce.aspx';">
			<img src="../images/add.gif" />添加公告
		</button>&nbsp;&nbsp;
		<yy:Button ID="DelRec" runat="server" Text=" 删 除 " ButtonImgUrl="../images/del.gif" Enabled="false" OnClientClick="if(!confirm('你确认要删除所选的公告吗？')) return false;"></yy:Button>
	</p>
    </form>
    <%=footer%>
</body>
</html>
