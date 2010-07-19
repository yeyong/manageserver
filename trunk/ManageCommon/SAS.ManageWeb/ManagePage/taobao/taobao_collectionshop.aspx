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
	function collectionshopinfo()
	{
	    var nick = $('nick').value;
	    if (confirm("抓取店铺信息需要耗费一定的时间,确认要继续吗？")) {
	        _sendRequest('../company/global_ajaxcall.aspx?opname=collectionshop', collectionshopinfo_callback, false, 'nick=' + nick);
	        $('success').style.display = 'block';
	        $('Layer5').innerHTML = '<BR /><table><tr><td valign=top><img border=0 src=../images/ajax_loading.gif  /></td><td valign=middle style=font-size:14px;>正在抓取店铺信息, <BR />请稍等...<BR /></td></tr></table><BR />';
	    }
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
	    AjaxHelper.Updater("../usercontrols/ajaxtaobaoshops.ascx", "taobaoshoplistgrid", "display=1");
	    $('success').style.display = 'none';
	}

	function searchshop() {
	    var errmsg = "";
	    var shoptitle = Form1.shoptitle.value;
	    var shopnick = Form1.shopnick.value;
	    var province = Form1.province.value;
	    var city = Form1.city.value;
	    var startscore = Form1.startscore.value;
	    var endscore = Form1.endscore.value;
	    var startcredit = Form1.startcredit.value;
	    var endcredit = Form1.endcredit.value;
	    var startrate = Form1.startrate.value;
	    var endrate = Form1.endrate.value;
	    var ordertype = "";
	    var parems = "";

	    
	    //alert(shoptitle);

	    if (startscore != "" && endscore != "") {
	        if (!isNumber(startscore) || !isNumber(endscore)) errmsg += "店铺总评分请输入数字！";
	        else if (startscore > endscore) errmsg += "店铺最小评分应小于最大评分，请仔细检查！";
	        else parems += "&startscore=" + startscore + "&endscore=" + endscore;
	    } else if ((startscore.length == 0 || endscore.length == 0) && (startscore.length + endscore.length) > 0) {
	        errmsg += "店铺评分区间请填写完整或清空！";
	    }

	    if (startcredit != "" && endcredit != "") {
	        if (!isNumber(startcredit) || !isNumber(endcredit)) errmsg += "店铺信誉度请输入数字！";
	        else if (startcredit > endcredit) errmsg += "店铺最小誉度应小于最大誉度，请仔细检查！";
	        else parems += "&startcredit=" + startcredit + "&endcredit=" + endcredit;
	    } else if ((startcredit.length == 0 || endcredit.length == 0) && (startcredit.length + endcredit.length) > 0) {
	        errmsg += "店铺誉度区间请填写完整或清空！";
	    }

	    if (startrate != "" && endrate != "") {
	        if (!isNumber(startrate) || !isNumber(endrate)) errmsg += "店铺佣金请输入数字！";
	        else if (startrate > endrate) errmsg += "店铺最小佣金应小于最大佣金，请仔细检查！";
	        else parems += "&startrate=" + startrate + "&endrate=" + endrate;
	    } else if ((startrate.length == 0 || endrate.length == 0) && (startrate.length + endrate.length) > 0) {
	        errmsg += "店铺佣金区间请填写完整或清空！";
	    }

	    if (Form1.descorder.checked) {
	        ordertype = "desc";
	    }
        parems += "&shoptitle=" + shoptitle + "&shopnick=" + shopnick + "&province=" + province + "&city=" + city;
	    if (errmsg != "") alert(errmsg);
	    else {
	        AjaxHelper.Updater("../usercontrols/ajaxtaobaoshops.ascx", "taobaoshoplistgrid", "load=true&display=1&ordercolumn=" + Form1.Select1.value + "&ordertype=" + ordertype + parems);
	    }
	}
</script>
<meta http-equiv="X-UA-Compatible" content="IE=7" />
</head>
<body >

<form id="Form1" runat="server">

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
<div class="ManagerForm" id="searchcondition">
    <fieldset>
        <legend style="background: url(&quot;../images/icons/icon32.jpg&quot;) no-repeat scroll 6px 50% transparent;">
            搜索相关店铺</legend>
        <table cellspacing="0" cellpadding="4" width="100%" align="center">
            <tr>
                <td class="panelbox" width="50%" align="left">
                    <table width="100%">
                        <tr>
                            <td style="width: 80px">
                                店铺名称标题:
                            </td>
                            <td>
                                <input type="text" name="shoptitle" size="30" class="FormBase" onfocus="this.className='FormFocus';"
                                    onblur="this.className='FormBase';" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 80px">
                                店铺卖家:
                            </td>
                            <td>
                                <input type="text" name="shopnick" size="20" class="FormBase" onfocus="this.className='FormFocus';"
                                    onblur="this.className='FormBase';" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 80px">
                                店铺地区:
                            </td>
                            <td>
                                省<input type="text" name="province" size="10" class="FormBase" onfocus="this.className='FormFocus';"
                                    onblur="this.className='FormBase';" />
                                市<input type="text" name="city" size="10" class="FormBase" onfocus="this.className='FormFocus';"
                                    onblur="this.className='FormBase';" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="panelbox" width="50%" align="right">
                    <table width="100%">
                        <tr>
                            <td>
                                店铺总评分区间:
                            </td>
                            <td>
                                从&nbsp;<input type="text" name="startscore" size="10" class="FormBase" onfocus="this.className='FormFocus';"
                                    onblur="this.className='FormBase';" />
                                到&nbsp;<input type="text" name="endscore" size="10" class="FormBase" onfocus="this.className='FormFocus';"
                                    onblur="this.className='FormBase';" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                店铺信誉区间:
                            </td>
                            <td>
                                从&nbsp;<input type="text" name="startcredit" size="10" class="FormBase" onfocus="this.className='FormFocus';"
                                    onblur="this.className='FormBase';" />
                                到&nbsp;<input type="text" name="endcredit" size="10" class="FormBase" onfocus="this.className='FormFocus';"
                                    onblur="this.className='FormBase';" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                店铺推广佣金率:
                            </td>
                            <td>
                                从&nbsp;<input type="text" name="startrate" size="10" class="FormBase" onfocus="this.className='FormFocus';"
                                    onblur="this.className='FormBase';" />
                                到&nbsp;<input type="text" name="endrate" size="10" class="FormBase" onfocus="this.className='FormFocus';"
                                    onblur="this.className='FormBase';" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div class="Navbutton">
            <span style="padding-right: 4px;">排序方式</span>
            <select name="showtype" id="Select1" style="margin-right: 8px;">
                <option value="shop_level">按信誉度排序</option>
                <option value="commission_rate">按佣金比率排序</option>
            </select>
            <input type="radio" name="theorder" id="descorder" class="txt" checked="checked" />倒序
            <input type="radio" name="theorder" id="ascorder" class="txt" />正序
            <input type="button" value="开始搜索" class="ManagerButton" onclick="searchshop()" />
        </div>
    </fieldset>
</div>
<div id="taobaoshoplistgrid" style="width:98%;margin:0 auto;"><uc1:AjaxShopList id="AjaxShopList1" runat="server" Display="ManageMode"></uc1:AjaxShopList></div>
</form>
<%=footer%>
</body>
</html>