﻿<%@ Page Language="C#" CodeBehind="global_advsgrid.aspx.cs" Inherits="SAS.ManageWeb.ManagePage.global_advsgrid" %>
<%@ Register Namespace="SAS.Control" Assembly="SAS.Control" TagPrefix="yy"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>广告列表</title>
    <link href="../styles/datagrid.css" type="text/css" rel="stylesheet" />
    <link href="../styles/dntmanager.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" src="../js/common.js"></script>
    <script type="text/javascript">
        function Check(form) {
            CheckAll(form);
            checkedEnabledButton(form, 'advid', 'DelAds', 'SetAvailable', 'SetUnAvailable')
        }
    </script>
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <yy:DataGrid ID="DataGrid1" runat="server" OnPageIndexChanged="DataGrid_PageIndexChanged" OnSortCommand="Sort_Grid">
        <columns>
		<asp:TemplateColumn HeaderText="<input title='选中/取消' onclick='Check(this.form)' type='checkbox' name='chkall' id='chkall' />">
			<HeaderStyle Width="20px" />
			<ItemTemplate>
					<input id="advid" onclick="checkedEnabledButton(this.form,'advid','DelAds','SetAvailable','SetUnAvailable')" type="checkbox" value="<%# DataBinder.Eval(Container, "DataItem.advid").ToString() %>" name="advid" />							</asp:Label>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="">
			<ItemTemplate>
					<a href="global_editadvs.aspx?advid=<%# DataBinder.Eval(Container, "DataItem.advid").ToString()%>">编辑</a>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:BoundColumn DataField="advid" SortExpression="advid" HeaderText="广告id [递增]"
			Visible="false"></asp:BoundColumn>
		<asp:TemplateColumn HeaderText="是否有效" SortExpression="adavailable">
			<ItemTemplate>
					<%# BoolStr(DataBinder.Eval(Container, "DataItem.adavailable").ToString())%>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:BoundColumn DataField="title" SortExpression="title" HeaderText="广告标题"></asp:BoundColumn>
		<asp:BoundColumn DataField="addisplayorder" SortExpression="addisplayorder" HeaderText="显示顺序">
		</asp:BoundColumn>
		<asp:TemplateColumn HeaderText="广告类型" SortExpression="adtype">
			<ItemTemplate>
					<%# GetAdType(DataBinder.Eval(Container, "DataItem.adtype").ToString())%>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="广告投放范围" SortExpression="targets">
			<ItemTemplate>
					<%# TargetsType(DataBinder.Eval(Container, "DataItem.targets").ToString())%>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="样式" SortExpression="parameters">
			<ItemTemplate>
					<%# ParameterType(DataBinder.Eval(Container, "DataItem.parameters").ToString())%>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="生效时间" SortExpression="starttime">
			<ItemTemplate>
					<%# DataBinder.Eval(Container, "DataItem.starttime").ToString()== "1900-1-1 0:00:00" ? "" : Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.starttime").ToString()).ToShortDateString()%>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="结束时间" SortExpression="endtime">
			<ItemTemplate>
					<%# DataBinder.Eval(Container, "DataItem.endtime").ToString()== "2555-1-1 0:00:00" ? "" : Convert.ToDateTime(DataBinder.Eval(Container, "DataItem.endtime").ToString()).ToShortDateString()%>
			</ItemTemplate>
		</asp:TemplateColumn>
	</columns>
    </yy:DataGrid>
    <p style="text-align: right;">
        <yy:Button ID="DelAds" runat="server" Text="删 除" ButtonImgUrl="../images/del.gif"
            Enabled="false" OnClientClick="if(!confirm('你确认要删除所选的广告吗？')) return false;">
        </yy:Button>&nbsp;&nbsp;
        <yy:Button ID="SetAvailable" runat="server" Text="置为有效" Enabled="false">
        </yy:Button>&nbsp;&nbsp;
        <yy:Button ID="SetUnAvailable" runat="server" Text="置为无效" ButtonImgUrl="../images/invalidation.gif"
            Enabled="false">
        </yy:Button>&nbsp;&nbsp;
        <button type="button" class="ManagerButton" onclick="javascript:window.location.href='global_addadvs.aspx';">
            <img src="../images/add.gif" />添加广告
        </button>
    </p>
    </form>
<%=footer%>
</body>
</html>
