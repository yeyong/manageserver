<%@ Page Language="C#" CodeBehind="global_activitygrid.aspx.cs" Inherits="SAS.ManageWeb.ManagePage.global_activitygrid" %>
<%@ Register TagPrefix="sas" Namespace="SAS.Control" Assembly="SAS.Control" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <meta name="keywords" content="天狼星,工作室" />
    <meta name="description" content="天狼星工作室综合管理后台" />
    <title>天狼星工作室综合管理后台</title>
    <link href="../styles/datagrid.css" type="text/css" rel="stylesheet" />
    <link href="../styles/dntmanager.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" src="../js/common.js"></script>

    <script type="text/javascript">
        function check(browser) {
            document.forms[0].operation.value = browser;
        }

        function CheckAll(form) {
            for (var i = 0; i < form.elements.length; i++) {
                var e = form.elements[i];
                if (e.name == 'id')
                    e.checked = form.chkall.checked;
            }
        }

        function SH_SelectOne(obj) {
            if (obj.checked == false) {
                document.getElementById('chkall').checked = obj.chcked;

            }
        }

        function Check(form) {
            CheckAll(form);
            checkedEnabledButton(form, 'id', 'SetActInfo')
        }
    </script>

    <meta http-equiv="X-UA-Compatible" content="IE=7" />
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <div class="ManagerForm">
<fieldset>
<legend style="background:url(../images/icons/legendimg.jpg) no-repeat 6px 50%;">操作主题</legend>
<table cellspacing="0" cellpadding="4" width="100%" align="center">
	<tr>
		<td class="panelbox" colspan="2">
			<table width="100%">
				<tr>
					<td style="width:100px"><input type="radio" name="operation" value="movetype" onClick="check(this.value)" checked />批量移动到</td>
					<td><sas:dropdownlist id="typeid" runat="server"></sas:dropdownlist></td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td class="panelbox" align="left" width="50%">
			<table width="100%">
				<tr>
					<td style="width:100px">
					    <input type="radio" name="operation" value="allenabled" onClick="check(this.value)" />批量启用					    
					</td>
					<td><input type="radio" name="operation" value="allunabled" onClick="check(this.value)" />批量禁用</td>
				</tr>
			</table>
		</td>
		<td class="panelbox" align="right" width="50%">
			<table width="100%">
				<tr>
					<td style="width:110px"><input type="radio" name="operation" value="deleteact" onClick="check(this.value)" />批量删除活动</td>
					<td>&nbsp;</td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td align="center" colspan="2"><sas:Button id="SetActInfo" runat="server" Text=" 提 交 " Enabled="false"></sas:Button></td>
	</tr>
</table>
</fieldset>
</div>
        <sas:datagrid id="DataGrid1" runat="server" OnPageIndexChanged="DataGrid_PageIndexChanged" OnSortCommand="Sort_Grid" PageSize="15">
	<Columns>
		<asp:TemplateColumn HeaderText="<input title='选中/取消' onclick='Check(this.form)' type='checkbox' name='chkall' id='chkall' />">
			<HeaderStyle Width="20px" />
			<ItemTemplate>
				<input id="id" onclick="checkedEnabledButton(this.form,'id','SetActInfo')" type="checkbox" value="<%# DataBinder.Eval(Container, "DataItem.id").ToString() %>" name="id" />
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="活动标题">
			<ItemTemplate>
				<a href="global_editactivity.aspx?id=<%# DataBinder.Eval(Container, "DataItem.id").ToString() %>">
					<%# DataBinder.Eval(Container, "DataItem.atitle").ToString() %>
				</a>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="活动类型">
			<itemtemplate>
				<%# GetActivityType(DataBinder.Eval(Container, "DataItem.atype").ToString())%>
			</itemtemplate>
		</asp:TemplateColumn>
		<asp:BoundColumn DataField="begintime" SortExpression="begintime" HeaderText="活动开始时间" ></asp:BoundColumn>
		<asp:BoundColumn DataField="endtime" SortExpression="endtime" HeaderText="活动结束时间"></asp:BoundColumn>
		<asp:BoundColumn DataField="createdate" SortExpression="createdate" HeaderText="活动创建时间" ></asp:BoundColumn>
		<asp:TemplateColumn HeaderText="开启状态">
			<ItemTemplate>
				<%# BoolStr(DataBinder.Eval(Container, "DataItem.enabled").ToString())%>
			</ItemTemplate>
		</asp:TemplateColumn>
	</Columns>
</sas:datagrid>
    </form>
    <%=footer%>	
</body>
</html>
