﻿<%@ Page Language="C#" CodeBehind="taobao_addCategory.aspx.cs" Inherits="SAS.ManageWeb.ManagePage.taobao_addCategory" %>
<%@ Register Namespace="SAS.Control" Assembly="SAS.Control" TagPrefix="yy"%>
<%@ Register TagPrefix="yy1" TagName="PageInfo" Src="../UserControls/PageInfo.ascx" %>
<%@ Register TagPrefix="yy1" TagName="TaoBaoItemCatTree" Src="../UserControls/taobaoitemcattree.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>添加类别</title>
<link href="../styles/calendar.css" type="text/css" rel="stylesheet" />
<link href="../styles/dntmanager.css" type="text/css" rel="stylesheet" />        
<link href="../styles/modelpopup.css" type="text/css" rel="stylesheet" />
<script type="text/javascript" src="../js/common.js"></script>
<script type="text/javascript" src="../js/modalpopup.js"></script>
<script type="text/javascript">
    function validate(theForm) {
        if ($("cname").value == "") {
            alert("名称不能为空!");
            $("cname").focus();
            return false;
        }
        if ($("displayorder").value == "") {
            alert("显示顺序不能为空不能为空!");
            $("displayorder").focus();
            return false;
        } else if (!isNumber($("displayorder").value)) {
            alert("显示顺序请填写数字格式!");
            $("displayorder").focus();
            return false;
        }
        //		var target = document.getElementsByName("TargetFID");
        //		var checkTarget = false;
        //		for (var i = 0; i < target.length; i++)
        //		{
        //			if (target[i].checked)
        //			{
        //				checkTarget = true;
        //				break;
        //			}
        //		}
        //		if (!checkTarget)
        //		{
        //			alert("未选择相关淘宝类别!");
        //			return false;
        //		}
        return true;
    }
</script>	
<meta http-equiv="X-UA-Compatible" content="IE=7" />
</head>
<body>
    <div class="ManagerForm">
<form id="Form1" method="post" runat="server">
<fieldset>
<legend style="background:url(../images/icons/icon36.jpg) no-repeat 6px 50%;"><%=ltitle%></legend>
<table width="100%">
	<tr><td class="item_title" colspan="2">是否生效</td></tr>
	<tr>
		<td class="vtop rowform">
			 <yy:RadioButtonList id="available" runat="server">
				<asp:Listitem Value="1" Selected="True">生效</asp:Listitem>
				<asp:Listitem Value="0">不生效</asp:Listitem>
			</yy:RadioButtonList>
		</td>
		<td class="vtop"></td>
	</tr>
	<tr><td class="item_title" colspan="2">显示顺序</td></tr>
	<tr>
		<td class="vtop rowform">
			<yy:TextBox id="displayorder" runat="server" CanBeNull="必填" RequiredFieldType="数据校验" Text="0" MultiLine="true" MaxLength="7"></yy:TextBox>
		</td>
		<td class="vtop"></td>
	</tr>
	<tr><td class="item_title" colspan="2">类别名称</td></tr>
	<tr>
		<td class="vtop rowform">
			 <yy:TextBox id="cname" runat="server" RequiredFieldType="暂无校验" CanBeNull="必填" Size="60" MaxLength="50"></yy:TextBox>
		</td>
		<td class="vtop"></td>
	</tr>
	<tbody id="targetForum">
	<tr><td class="item_title" colspan="2">相关淘宝类别</td></tr>
	<tr>
		<td class="vtop" colspan="2">
			<div style="overflow: auto;height: 400px;width:70%;border: 1px double #ccc">
				<yy1:TaoBaoItemCatTree id="TargetFID"  runat="server" HintTitle="提示" HintInfo="设置相关淘宝类别" PageName="advertisement"></yy1:TaoBaoItemCatTree>
			</div>										
		</td>
	</tr>
	</tbody>	
	</table>
	<yy:Hint id="Hint1" runat="server" HintImageUrl="../images"></yy:Hint>
	<div class="Navbutton">
		<yy:Button id="AddCategoryInfo" runat="server" Text=" 提 交 " ValidateForm="true"></yy:Button>&nbsp;&nbsp;
		<button type="button" class="ManagerButton" id="Button3" onclick="window.history.back();"><img src="../images/arrow_undo.gif"/> 返 回 </button>
	</div>
</fieldset>
</form>
</div>
<%=footer%>
</body>
</html>