﻿<%@ Page Language="C#" CodeBehind="taobao_addshoprecommend.aspx.cs" Inherits="SAS.ManageWeb.ManagePage.taobao_addshoprecommend" %>
<%@ Register TagPrefix="cc1" Namespace="SAS.Control" Assembly="SAS.Control" %>
<%@ Register TagPrefix="uc1" TagName="AjaxShopList" Src="../UserControls/ajaxtaobaoshops.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<title>无标题页</title>
<meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
<link href="../styles/calendar.css" type="text/css" rel="stylesheet" />
<link href="../styles/datagrid.css" type="text/css" rel="stylesheet" />	
<link href="../styles/dntmanager.css" type="text/css" rel="stylesheet" />
<link href="../styles/modelpopup.css" type="text/css" rel="stylesheet" />	
<script type="text/javascript" src="../js/common.js"></script>
<script type="text/javascript" src="../js/AjaxHelper.js"></script>
<script type="text/javascript" src="../js/calendar.js"></script>
<script type="text/javascript" src="../js/modalpopup.js"></script>
<script type="text/javascript" src="../../javascript/common.js"></script>
<script type="text/javascript">
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
            AjaxHelper.Updater("../usercontrols/ajaxtaobaoshops.ascx", "taobaoshoplistgrid", "load=true&display=0&ordercolumn=" + Form1.Select1.value + "&ordertype=" + ordertype + parems);
        }
    }

    function selectItem(obj,thevalue) {
        var theobj = getRowObj(obj);
        var thebutton = document.createElement("input");
        thebutton.type="button";
        thebutton.className= "ManagerButton";
        thebutton.value="删除";
        thebutton.onclick = function() {
            delRow(this);
            $("selitems").value = $("selitems").value.replace("," + thevalue, "");
        };
        theobj.firstChild.replaceChild(thebutton, theobj.firstChild.firstChild);
        $("SelectItem").firstChild.appendChild(theobj);
        $("selitems").value = $("selitems").value + "," + thevalue;
    }

    function validate(theForm) {
        if ($("rtitle").value == "") {
            alert("推荐标题不能为空!");
            $("rtitle").focus();
            return false;
        }
        if ($("selitems").value == "") {
            alert("推荐商品不能为空!");
            $("selitems").focus();
            return false;
        }
        return true;
    }
</script>
<meta http-equiv="X-UA-Compatible" content="IE=7" />
</head>
<body>
<form id="Form1" runat="server">
<div class="ManagerForm">
<fieldset>
<legend style="background:url(../images/icons/legendimg.jpg) no-repeat 6px 50%;">添加店铺推荐</legend>
    <table cellspacing="0" cellpadding="4" width="100%" align="center">
        <tr>
            <td class="item_title" colspan="2">
                推荐标题
            </td>
        </tr>
        <tr>
            <td class="vtop rowform">
                <cc1:TextBox ID="rtitle" runat="server" RequiredFieldType="暂无校验" CanBeNull="必填" Size="100"
                    MaxLength="150"></cc1:TextBox>
            </td>
            <td class="vtop">
            </td>
        </tr>
        <tr>
            <td class="item_title" colspan="2">
                相关频道
            </td>
        </tr>
        <tr>
            <td class="vtop rowform">
               <cc1:DropDownList runat="server" ID="rchanel"></cc1:DropDownList>
            </td>
            <td class="vtop">
            </td>
        </tr>
        <tr>
            <td class="item_title" colspan="2">
                相关类别
            </td>
        </tr>
        <tr>
            <td class="vtop rowform">
                <cc1:DropDownTreeList runat="server" ID="rcategory" />
            </td>
            <td class="vtop">
            </td>
        </tr>
       <tr>
		<td colspan="2">
		<table class="ntcplist">
		    <tr class="head">
              <td>&nbsp;&nbsp;已选店铺列表</td>
            </tr>
            <tr>
            <td>
             <table class="datalist" cellspacing="0" rules="all" border="1" id="SelectItem" style="border-collapse:collapse;">
                  <tr class="category">
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;">选择</td>
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;">店铺图标</td>
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;">店铺名称</td>
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;">店铺卖家</td>
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;">佣金比率</td>
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;">商家类型</td>
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;">是否参加消保</td>
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;">信用等级</td>
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;">信用总分</td>
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;">评价总条数</td>
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;">好评总条数</td>
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;">商品描述评分</td>
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;">服务态度评分</td>
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;">发货速度评分</td>
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;">详细地址</td>
                  </tr>
              </table>
              </td>
            </tr>
          </table>
          </td>
          </tr>
    </table>
    <div class="Navbutton">
		<cc1:Button id="AddRecommendInfo" runat="server" Text=" 添 加 " ValidateForm="true"></cc1:Button>&nbsp;&nbsp;<input type="hidden" name="selitems" id="selitems"/>
		<button type="button" class="ManagerButton" id="Button3" onclick="window.history.back();"><img src="../images/arrow_undo.gif"/> 返 回 </button>
	</div>
</fieldset>
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
<cc1:Hint id="Hint1" runat="server" HintImageUrl="../images"></cc1:Hint>
</div>
<div id="taobaoshoplistgrid"><uc1:AjaxShopList id="AjaxShopList1" runat="server"></uc1:AjaxShopList></div>
</form>
<%=footer%>
</body>
</html>