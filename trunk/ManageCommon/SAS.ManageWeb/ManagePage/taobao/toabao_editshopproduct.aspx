<%@ Page Language="C#" CodeBehind="toabao_editshopproduct.aspx.cs" Inherits="SAS.ManageWeb.ManagePage.toabao_editshopproduct" %>
<%@ Register TagPrefix="cc1" Namespace="SAS.Control" Assembly="SAS.Control" %>
<%@ Register TagPrefix="uc1" TagName="AjaxItemList" Src="../UserControls/ajaxtaobaoitems.ascx" %>
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
    function searchitem(objform) {
        var errmsg = "";
        var srate = objform.startrate.value;
        var erate = objform.endrate.value;
        var smoney = objform.startmoney.value;
        var emoney = objform.endmoney.value;
        var scredit = objform.startcredit.value;
        var ecredit = objform.endcredit.value;
        var snums = objform.startnum.value;
        var enums = objform.endnum.value;
        var parems = "";

        if (srate.length > 0 && erate.length > 0) {
            if (!isNumber(srate) || !isNumber(erate)) errmsg += "佣金比率请输入数字格式!\r\n";
            else if (srate > erate) errmsg += "最低佣金比率应低于最高佣金比率\r\n";
            else parems += "&startrate=" + srate + "&endrate=" + erate;
        } else if (((srate.length + erate.length) > 0) && (srate.length == 0 || erate.length == 0)) {
            errmsg += "佣金比率请输入完整或都不输入\r\n";
        }

        if (smoney.length > 0 && emoney.length > 0) {
            if (!isNumber(smoney) || !isNumber(emoney)) errmsg += "销售价格请输入数字格式!\r\n";
            else if (smoney > emoney) errmsg += "最低销售价格应低于最高佣金比率\r\n";
            else parems += "&startmoney=" + smoney + "&endmoney=" + emoney;
        } else if (((smoney.length + emoney.length) > 0) && (smoney.length == 0 || emoney.length == 0)) {
            errmsg += "销售价格请输入完整或都不输入\r\n";
        }

        if (scredit.length > 0 && ecredit.length > 0) {
            if (!isNumber(scredit) || !isNumber(ecredit)) errmsg += "卖家信誉请输入数字格式!\r\n";
            else if (scredit > ecredit) errmsg += "最低卖家信誉应低于最高佣金比率\r\n";
            else parems += "&startcredit=" + scredit + "&endcredit=" + ecredit;
        } else if (((scredit.length + ecredit.length) > 0) && (scredit.length == 0 || ecredit.length == 0)) {
            errmsg += "卖家信誉请输入完整或都不输入\r\n";
        }

        if (snums.length > 0 && enums.length > 0) {
            if (!isNumber(snums) || !isNumber(enums)) errmsg += "最近推广量请输入数字格式!\r\n";
            else if (snums > enums) errmsg += "最低最近推广量应低于最高佣金比率\r\n";
            else parems += "&startnum=" + snums + "&endnum=" + enums;
        } else if (((snums.length + enums.length) > 0) && (snums.length == 0 || enums.length == 0)) {
            errmsg += "最近推广量请输入完整或都不输入\r\n";
        }

        if (errmsg != "") alert(errmsg);
        else {
            AjaxHelper.Updater("../usercontrols/ajaxtaobaoitems.ascx", "taobaoitemlistgrid", "load=true&keyword=" + objform.keyword.value + "&cid=" + $("rcategory").value + parems);
        }
    }

    function selectItem(obj,thevalue) {
        var theobj = getRowObj(obj);
        thevalue = thevalue + "|" + theobj.childNodes[1].innerText + "|" + theobj.childNodes[3].innerText + "|" + theobj.childNodes[1].childNodes[2].childNodes[0].src.replace("_250x250.jpg","");
        
        var thebutton = document.createElement("input");
        thebutton.type="button";
        thebutton.className= "ManagerButton";
        thebutton.value="删除";
        thebutton.onclick = function() {
            delRow(this);
            $("selitems").value = $("selitems").value.replace("," + thevalue, "");
        };
        theobj.firstChild.replaceChild(thebutton, theobj.firstChild.firstChild);
        var copyobj = theobj;
        for (var i = theobj.childNodes.length - 1; i >= 0; i--) {
            if (i == 0 || i == 1 | i == 3) continue;
            theobj.removeChild(copyobj.childNodes[i]);
        }
        
        $("SelectItem").firstChild.appendChild(theobj);
        $("selitems").value = $("selitems").value + "," + thevalue;
    }

    function deleteItem(obj, thevalue) {
        delRow(obj);
        $("selitems").value = $("selitems").value.replace("," + thevalue, "");
    }

    function validate(theForm) {
//        if ($("selitems").value == "") {
//            alert("推荐商品不能为空!");
//            $("selitems").focus();
//            return false;
//        }

        return true;
    }
</script>
<meta http-equiv="X-UA-Compatible" content="IE=7" />
</head>
<body>
<form id="Form1" runat="server">
<div class="ManagerForm">
<fieldset>
<legend style="background:url(../images/icons/legendimg.jpg) no-repeat 6px 50%;">编辑商品推荐</legend>
    <table cellspacing="0" cellpadding="4" width="100%" align="center">
       <tr>
		<td colspan="2">
		<table class="ntcplist">
		    <tr class="head">
              <td>&nbsp;&nbsp;已选商品列表</td>
            </tr>
            <tr>
            <td>
             <table class="datalist" cellspacing="0" rules="all" border="1" id="SelectItem" style="border-collapse:collapse;">
                  <tr class="category">
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;">选择</td>
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;">宝贝名称</td>
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;">商品价格</td>
                  </tr>
                  <%
                      foreach (string str in selectitems.Split(','))
                      {
                          if (str == "") continue;
                          string[] substr = str.Split('|');
                          if (substr.Length < 4) continue;
                  %>
                  <tr class="mouseoutstyle" onmouseover="this.className='mouseoverstyle'" onmouseout="this.className='mouseoutstyle'" style="cursor:hand;">
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;"><input type="button" class="ManagerButton" value="删除" onclick="deleteItem(this,'<%=SAS.Common.Utils.RTrim(str)%>');"/></td>
                    <td style="border-color:#EAE9E1;border-width:1px;border-style:solid;">
                        <span id="a<%=substr[0]%>" onmouseover="showMenu(this.id,0,0,1,0);" style="font-weight:bold"><%=substr[1]%><img src="../images/eye.gif" style="vertical-align:middle" /></span>
                        <div id="a<%=substr[0]%>_menu" style="display:none">
					        <img src="<%=substr[3]%>_250x250.jpg" onerror="this.src='../../images/common/none.gif'" />
						</div>
                    </td>
                    <td style="border-color:#EAE9E1;border-width:1px;border-style:solid;"><%=substr[2]%></td>
                  </tr>
                  <%
                      }
                  %>
              </table>
              </td>
            </tr>
          </table>
          </td>
          </tr>
    </table>
    <div class="Navbutton">
		<cc1:Button id="EditRecommendInfo" runat="server" Text=" 确 定 " ValidateForm="true"></cc1:Button>&nbsp;&nbsp;<input type="hidden" name="selitems" id="selitems" value="<%=SAS.Common.Utils.RTrim(selectitems)%>"/>
		<button type="button" class="ManagerButton" id="Button3" onclick="window.history.back();"><img src="../images/arrow_undo.gif"/> 返 回 </button>
	</div>
</fieldset>
<fieldset>
<legend style="background:url(../images/icons/legendimg.jpg) no-repeat 6px 50%;">搜索商品</legend>
    <table cellspacing="0" cellpadding="4" width="100%" align="center">
        <tr>
            <td class="panelbox" width="50%" align="left">
                <table width="100%">
                    <tr>
                        <td style="width: 80px">
                            商品关键字:
                        </td>
                        <td>
                           <input type="text" name="keyword" size="30" class="FormBase" onfocus="this.className='FormFocus';" onblur="this.className='FormBase';"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            相关类别:
                        </td>
                        <td>
                            <select name="rcategory">
                                <option value="0">请选择类别</option>
                                <%
                                    foreach (SAS.Entity.Domain.ItemCat iteminfo in icatlist)
                                    {
                                      %>
                                      <option value="<%=iteminfo.Cid%>"><%=iteminfo.Name%></option>
                                      <%
                                    }
                                         %>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            佣金比率:
                        </td>
                        <td>
                            从&nbsp;<input type="text" name="startrate" size="10" class="FormBase" onfocus="this.className='FormFocus';" onblur="this.className='FormBase';"/>
                            到&nbsp;<input type="text" name="endrate" size="10" class="FormBase" onfocus="this.className='FormFocus';" onblur="this.className='FormBase';"/>
                        </td>
                    </tr>
                </table>
            </td>
            <td class="panelbox" width="50%" align="right">
                <table width="100%">
                    <tr>
                        <td>
                            商品价格:
                        </td>
                        <td>
                            从&nbsp;<input type="text" name="startmoney" size="10" class="FormBase" onfocus="this.className='FormFocus';" onblur="this.className='FormBase';"/>
                            到&nbsp;<input type="text" name="endmoney" size="10" class="FormBase" onfocus="this.className='FormFocus';" onblur="this.className='FormBase';"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            卖家信誉度:
                        </td>
                        <td>
                            从&nbsp;<input type="text" name="startcredit" size="10" class="FormBase" onfocus="this.className='FormFocus';" onblur="this.className='FormBase';"/>
                            到&nbsp;<input type="text" name="endcredit" size="10" class="FormBase" onfocus="this.className='FormFocus';" onblur="this.className='FormBase';"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            累计推广量:
                        </td>
                        <td>
                            从&nbsp;<input type="text" name="startnum" size="10" class="FormBase" onfocus="this.className='FormFocus';" onblur="this.className='FormBase';"/>
                            到&nbsp;<input type="text" name="endnum" size="10" class="FormBase" onfocus="this.className='FormFocus';" onblur="this.className='FormBase';"/>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
<div class="Navbutton"><input type="button" class="ManagerButton" value="查找符合条件的商品" onclick="searchitem(this.form)"/></div>	
</fieldset>
<cc1:Hint id="Hint1" runat="server" HintImageUrl="../images"></cc1:Hint>
</div>
<div id="taobaoitemlistgrid"><uc1:AjaxItemList id="AjaxItemList1" runat="server"></uc1:AjaxItemList></div>
</form>
<%=footer%>
</body>
</html>
