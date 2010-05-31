<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="global_searchactivity.aspx.cs" Inherits="SAS.ManageWeb.ManagePage.global_searchactivity" %>
<%@ Register TagPrefix="sas" Namespace="SAS.Control" Assembly="SAS.Control" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>查找活动列表</title>
    <link href="../styles/calendar.css" type="text/css" rel="stylesheet" />
    <link href="../styles/dntmanager.css" type="text/css" rel="stylesheet" />
    <link href="../styles/modelpopup.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" src="../js/common.js"></script>
    <script type="text/javascript" src="../js/modalpopup.js"></script>
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
</head>
<body>
    <div class="ManagerForm">
<form id="Form1" method="post" runat="server">
<fieldset>
<legend style="background:url(../images/icons/icon19.jpg) no-repeat 6px 50%;">搜索活动专题</legend>
<table width="100%">
	<tr><td class="item_title">所属类别</td></tr>
	<tr>
		<td class="vtop rowform">
			<sas:dropdowntreelist id="forumid" runat="server"></sas:dropdowntreelist>
		</td>
		<td class="vtop"></td>
	</tr>
	<tr><td class="item_title">活动创建时间范围</td></tr>
	<tr>
		<td class="vtop rowform">
			起始日期:<sas:Calendar id="postdatetimeStart" runat="server" ReadOnly="True" ScriptPath="../js/calendar.js"></sas:Calendar><br />
			结束日期:<sas:Calendar id="postdatetimeEnd" runat="server" ReadOnly="True" ScriptPath="../js/calendar.js"></sas:Calendar>
		</td>
		<td class="vtop"></td>
	</tr>
	<tr><td class="item_title">活动关键字</td></tr>
	<tr>
		<td class="vtop rowform">
			 <sas:TextBox id="keyword" runat="server" RequiredFieldType="暂无校验" Width="100"></sas:TextBox>
		</td>
		<td class="vtop">多关键字中间请用半角逗号"," 分割</td>
	</tr>
	<tr><td class="item_title">是否启用</td></tr>
	<tr>
		<td class="vtop rowform">
			<sas:radiobuttonlist id="attachment" runat="server" RepeatColumns="3">
				<asp:Listitem Value="-1" selected="true">无限制</asp:Listitem>
				<asp:Listitem Value="1">启用</asp:Listitem>
				<asp:Listitem Value="0">禁用</asp:Listitem>
			</sas:radiobuttonlist>
		</td>
		<td class="vtop"></td>
	</tr>
</table>
<sas:Hint id="Hint1" runat="server" HintImageUrl="../images"></sas:Hint>
<div class="Navbutton"><sas:Button id="SaveSearchCondition" runat="server" Text="搜索符合条件主题" ButtonImgUrl="../images/search.gif"></sas:Button></div>
</fieldset>
<div id="topictypes" style="display:none;">
	<tr><td class="item_title" colspan="2">所在分类</td></tr>
	<tr>
		<td class="vtop rowform">
			<sas:dropdownlist id="typeid" runat="server"></sas:dropdownlist>
		</td>
		<td class="vtop"></td>
	</tr>
</div>
</form>
</div>
<%=footer%>
</body>
</html>
