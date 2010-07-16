<%@ Page Language="C#" CodeBehind="taobao_collectionshop.aspx.cs" Inherits="SAS.ManageWeb.ManagePage.taobao_collectionshop" %>
<%@ Register TagPrefix="cc1" Namespace="SAS.Control" Assembly="SAS.Control" %>
<%@ Register TagPrefix="uc1" TagName="AjaxShopList" Src="../UserControls/ajaxtaobaoshops.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
<title>店铺收集</title>
<link href="../styles/gridStyle.css" type="text/css" rel="stylesheet" />
<link href="../styles/calendar.css" type="text/css" rel="stylesheet" />
<link href="../styles/datagrid.css" type="text/css" rel="stylesheet" />		
<script type="text/javascript" src="../js/common.js"></script>
<script language="JavaScript" type="text/javascript" src="../../javascript/ajax.js"></script>
<script type="text/javascript" src="../js/AjaxHelper.js"></script>
<script type="text/javascript" src="../js/calendar.js"></script>
<link href="../styles/dntmanager.css" type="text/css" rel="stylesheet" />
<script type="text/javascript" src="../js/draglist.js"></script>
<link href="../styles/draglist.css" type="text/css" rel="stylesheet" />
<script type="text/javascript">
	function validate(theform)
	{
		var idColl = $("dom0").getElementsByTagName("input");
		var idlist = "";
		for(i = 0 ; i < idColl.length ; i++)
		{
			if(idlist=="")
			{
			   idlist = idColl[i].value;
			}
			else
			{
			   idlist = idlist + "," + idColl[i].value;
			}
		}
		$("forumtopicstatus").value = idlist;
		return true;
	}


	function collectionshopinfo()
	{
	    var nick = $('nick').value;

	    _sendRequest('../company/global_ajaxcall.aspx?opname=collectionshop', collectionshopinfo_callback, false, 'nick=' + nick);
	}
	function collectionshopinfo_callback(doc) {
	    if (doc == "0") {
	        $('adderror').innerHTML = '店铺不存在';
	    }
	    else if (doc == "2") {
	        $('adderror').innerHTML = '添加店铺成功';
	    } else {
	        $('adderror').innerHTML = '更新店铺成功' + doc;
	    }
	    $('nick').value = '';
	}

   function checkformid()
   {
		if($('forumid').value!='' && !isNumber($('forumid').value))
		{
		alert("版块ID必须为数字");
		return false;
		}
	}
</script>
<meta http-equiv="X-UA-Compatible" content="IE=7" />
</head>
<body >

<form id="Form1" runat="server">
<div class="ManagerForm" id="searchcondition">
<fieldset>
<legend style="background: url(&quot;../images/icons/icon32.jpg&quot;) no-repeat scroll 6px 50% transparent;">搜索相关店铺</legend>
<div id="searchtable">
	<table width="100%">
	<tbody>
		<tr>
			<td>
			<span style="padding-right:4px;">排序方式</span>
			<select name="showtype" id="showtype" style="margin-right:8px;">
				<option value="shop_level">按信誉度排序</option>
				<option value="commission_rate">按佣金比率排序</option>
			</select>
			<span style="padding-right:4px;">店铺名称</span>
			<input name="forumid" type="text"  id="forumid" style="margin-right:8px;" value="" size="4"/>
			<input name="search" type="submit" value="开始搜索" class="ManagerButton" onClick="return  checkformid();"/>
			</td>
		</tr>
	</tbody>
	</table>
</div>
</fieldset>
</div>
<div id="taobaoshoplistgrid" style="width:98%;margin:0 auto;"><uc1:AjaxShopList id="AjaxTopicInfo1" runat="server"></uc1:AjaxShopList></div>

<br />
<div class="ManagerForm">
<fieldset>
<legend style="background: url(&quot;../images/icons/icon32.jpg&quot;) no-repeat scroll 6px 50% transparent;">直接添加店铺</legend>
<div id="addhottopics">
	<table width="100%">
	<tbody>
		<tr>
			<td><span style="padding-right:4px;">店铺卖家昵称:</span>
			<input name="nick" type="text" id="nick" style="margin-right:8px;"><input name="button" type="button" id="dsfsdafsa" onClick="collectionshopinfo()" value="添加店铺"  class="ManagerButton"><span style="color:#FF0000;padding-left:8px;" id="adderror"></span>
			</td>
		</tr>
	</tbody>
	</table>
</div>
</fieldset>
</div>
</form>
<%=footer%>
</body>
</html>