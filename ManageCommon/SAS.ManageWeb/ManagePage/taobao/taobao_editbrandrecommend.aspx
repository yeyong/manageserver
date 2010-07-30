<%@ Page Language="C#" CodeBehind="taobao_editbrandrecommend.aspx.cs" Inherits="SAS.ManageWeb.ManagePage.taobao_editbrandrecommend" %>
<%@ Register TagPrefix="cc1" Namespace="SAS.Control" Assembly="SAS.Control" %>
<%@ Register TagPrefix="uc1" TagName="AjaxBrandList" Src="../UserControls/ajaxbrandlist.ascx" %>
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
    function searchbrand() {
        var errmsg = "";
        var bname = Form1.brandname.value;
        var rclass = $('relateclass').value;

        if (errmsg != "") alert(errmsg);
        else {
            AjaxHelper.Updater("../usercontrols/ajaxbrandlist.ascx", "taobaobrandlistgrid", "load=true&brandname=" + bname + "&relateclass=" + rclass);
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

    function deleteItem(obj, thevalue) {
        delRow(obj);
        $("selitems").value = $("selitems").value.replace("," + thevalue, "");
    }

    function validate(theForm) {
        if ($("rtitle").value == "") {
            alert("推荐标题不能为空!");
            $("rtitle").focus();
            return false;
        }
        if ($("selitems").value == "") {
            alert("推荐品牌不能为空!");
            //$("selitems").focus();
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
<legend style="background:url(../images/icons/legendimg.jpg) no-repeat 6px 50%;">添加品牌推荐</legend>
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
              <td>&nbsp;&nbsp;已选品牌列表</td>
            </tr>
            <tr>
            <td>
             <table class="datalist" cellspacing="0" rules="all" border="1" id="SelectItem" style="border-collapse:collapse;">
                  <tr class="category">
                  <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;">操作</td>
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;">品牌logo</td>
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;">品牌名称</td>
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;">品牌别名</td>
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;">关联类别</td>
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;">品牌排序</td>
                  </tr>
                   <%
                       foreach (System.Data.DataRow dr in branddatatable.Rows)
                       { 
                   %>
                  <tr class="mouseoutstyle" onmouseover="this.className='mouseoverstyle'" onmouseout="this.className='mouseoutstyle'" style="cursor:hand;">
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;"><input type="button" class="ManagerButton" value="删除" onclick="deleteItem(this,'<%=dr["id"]%>');"/></td>
                    <td style="border-color:#EAE9E1;border-width:1px;border-style:solid;"><img width="82px" height="82px" src="<%=dr["logo"]%>"/></td>
                    <td style="border-color:#EAE9E1;border-width:1px;border-style:solid;"><%=dr["bname"]%></td>
                    <td style="border-color:#EAE9E1;border-width:1px;border-style:solid;"><%=dr["spell"]%></td>
                    <td style="border-color:#EAE9E1;border-width:1px;border-style:solid;"><%=dr["cname"]%></td>
                    <td style="border-color:#EAE9E1;border-width:1px;border-style:solid;"><%=dr["order"]%></td>
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
		<cc1:Button id="EditRecommendInfo" runat="server" Text=" 修 改 " ValidateForm="true"></cc1:Button>&nbsp;&nbsp;<input type="hidden" name="selitems" id="selitems" value="<%=selectitems%>"/>
		<button type="button" class="ManagerButton" id="Button3" onclick="window.history.back();"><img src="../images/arrow_undo.gif"/> 返 回 </button>
	</div>
</fieldset>
<fieldset>
<legend style="background: url(&quot;../images/icons/icon32.jpg&quot;) no-repeat scroll 6px 50% transparent;">
            搜索相关品牌</legend>
        <table cellspacing="0" cellpadding="4" width="100%" align="center">
            <tr>
                <td class="panelbox" width="50%" align="left">
                    <table width="100%">
                        <tr>
                            <td style="width: 80px">
                                品牌名称别名:
                            </td>
                            <td>
                                <input type="text" name="brandname" size="30" class="FormBase" onfocus="this.className='FormFocus';"
                                    onblur="this.className='FormBase';" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="panelbox" width="50%" align="right">
                    <table width="100%">
                        <tr>
                            <td style="width: 80px">
                                相关类别:
                            </td>
                            <td>
                                <select name="relateclass">
                                 <option value="0">请选择相关类别</option>
                                <%
                                    foreach (System.Data.DataRow dr in therootclass.Select("[parentid] = 0"))
                                    {
                                %>
                                <option value="<%=dr["cid"]%>"><%=dr["name"]%></option>
                                <%
                                    }
                                %>
                                </select>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div class="Navbutton">
            <input type="button" value="开始搜索" class="ManagerButton" onclick="searchbrand()" />
        </div>
</fieldset>
<cc1:Hint id="Hint1" runat="server" HintImageUrl="../images"></cc1:Hint>
</div>
<div id="taobaobrandlistgrid"><uc1:AjaxBrandList id="AjaxBrandList1" runat="server"></uc1:AjaxBrandList></div>
</form>
<%=footer%>
</body>
</html>