﻿<%@ Page Language="C#" CodeBehind="global_addhelpclass.aspx.cs" Inherits="SAS.ManageWeb.ManagePage.global_addhelpclass" %>
<%@ Register TagPrefix="yy" Namespace="SAS.Control" Assembly="SAS.Control" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>添加帮助类别</title>

    <script type="text/javascript" src="../js/common.js"></script>

    <link href="../styles/dntmanager.css" type="text/css" rel="stylesheet">
    <link href="../styles/modelpopup.css" type="text/css" rel="stylesheet">

    <script type="text/javascript" src="../js/modalpopup.js"></script>

    <meta http-equiv="X-UA-Compatible" content="IE=7" />
</head>
<body>
    <div class="ManagerForm">
<form id="Form1" runat="server" >
<fieldset>
<legend style="background:url(../images/icons/legendimg.jpg) no-repeat 6px 50%;">添加类别</legend>
<table width="100%">
	<tr><td class="item_title" colspan="2">帮助标题</td></tr>
	<tr>
		<td class="vtop" colspan="2">
			<yy:TextBox id="title" runat="server" CanBeNull="必填" RequiredFieldType="暂无校验"  maxlength="249" size="50"></yy:TextBox>
		</td>
	</tr>   			
	<tbody style="display:none">
	<tr><td class="item_title" colspan="2">发布者用户名</td></tr>
	<tr>
		<td class="vtop" colspan="2">
			<yy:TextBox id="poster" runat="server" RequiredFieldType="暂无校验" CanBeNull="必填" MaxLength="20" Enabled="false"></yy:TextBox>
		</td>
	</tr>
	</tbody>
</table>
<div class="Navbutton">
	<yy:Button id="add" runat="server" Text=" 提 交 " OnClick="add_Click" ValidateForm="false"></yy:Button>&nbsp;&nbsp;
	<button type="button" class="ManagerButton" id="Button3" onclick="window.history.back();"><img src="../images/arrow_undo.gif"/> 返 回 </button>
</div>
</fieldset>
</form>
</div>
<%=footer%>
</body>
</html>
