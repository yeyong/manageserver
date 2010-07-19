<%@ Page Language="C#" CodeBehind="taobao_addtopicrecommend.aspx.cs" Inherits="SAS.ManageWeb.ManagePage.taobao_addtopicrecommend" %>
<%@ Register TagPrefix="cc1" Namespace="SAS.Control" Assembly="SAS.Control" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1">
<title>无标题页</title>
<meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
<link href="../styles/calendar.css" type="text/css" rel="stylesheet" />
<link href="../styles/datagrid.css" type="text/css" rel="stylesheet" />	
<link href="../styles/dntmanager.css" type="text/css" rel="stylesheet" />
<link href="../styles/modelpopup.css" type="text/css" rel="stylesheet" />	
<script type="text/javascript" src="../js/common.js"></script>
<script type="text/javascript" src="../js/modalpopup.js"></script>
<script type="text/javascript">
    function selectItem(obj,thevalue) {
        var theobj = getRowObj(obj);
        var thebutton = document.createElement("input");
        thebutton.type="button";
        thebutton.className= "ManagerButton";
        thebutton.value="删除";
        thebutton.onclick = function() {
            delRow(this);
            $("selitems").value = $("selitems").value.replace("," + thevalue, "");
            alert($("selitems").value);
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
            alert("推荐不能为空!");
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
<legend style="background:url(../images/icons/legendimg.jpg) no-repeat 6px 50%;">添加活动推荐</legend>
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
              <td>&nbsp;&nbsp;已选活动列表</td>
            </tr>
            <tr>
            <td>
             <table class="datalist" cellspacing="0" rules="all" border="1" id="SelectItem" style="border-collapse:collapse;">
                  <tr class="category">
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;">选择</td>
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;">活动ID</td>
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;">活动标题</td>
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;">活动类型</td>
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;">活动图片</td>
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;">排列顺序</td>
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
<legend style="background:url(../images/icons/legendimg.jpg) no-repeat 6px 50%;">增加活动</legend>
    <table cellspacing="0" cellpadding="4" width="100%" align="center">
        <tr>
            <td class="panelbox" width="50%" align="left">
                <table width="100%">
                    <tr>
                        <td style="width: 80px">
                            活动ID:
                        </td>
                        <td>
                           <input type="text" name="actid" size="20" class="FormBase" onfocus="this.className='FormFocus';" onblur="this.className='FormBase';"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            排列顺序:
                        </td>
                        <td>
                            <input type="text" name="actorder" size="8" class="FormBase" onfocus="this.className='FormFocus';" onblur="this.className='FormBase';"/>
                        </td>
                    </tr>
                </table>
            </td>
            <td class="panelbox" width="50%" align="right">
                <table width="100%">
                    <tr>
                        <td>
                            活动标题:
                        </td>
                        <td>
                            <input type="text" name="acttitle" size="40" class="FormBase" onfocus="this.className='FormFocus';" onblur="this.className='FormBase';"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            活动图片:
                        </td>
                        <td>
                            <input type="text" name="actpic" size="40" class="FormBase" onfocus="this.className='FormFocus';" onblur="this.className='FormBase';"/>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
<div class="Navbutton"><input type="button" class="ManagerButton" value="增加该活动" onclick="searchitem(this.form)"/><input type="button" class="ManagerButton" value="预览该活动" onclick="searchitem(this.form)"/></div>	
</fieldset>
<cc1:Hint id="Hint1" runat="server" HintImageUrl="../images"></cc1:Hint>
</div>
<div id="taobaoitemlistgrid"></div>
</form>
<%=footer%>
</body>
</html>
