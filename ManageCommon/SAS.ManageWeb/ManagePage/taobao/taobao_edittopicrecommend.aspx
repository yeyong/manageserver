<%@ Page Language="C#" CodeBehind="taobao_edittopicrecommend.aspx.cs" Inherits="SAS.ManageWeb.ManagePage.taobao_edittopicrecommend" %>
<%@ Register TagPrefix="cc1" Namespace="SAS.Control" Assembly="SAS.Control" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1">
<title>修改专题活动推荐</title>
<meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
<link href="../styles/calendar.css" type="text/css" rel="stylesheet" />
<link href="../styles/datagrid.css" type="text/css" rel="stylesheet" />	
<link href="../styles/dntmanager.css" type="text/css" rel="stylesheet" />
<link href="../styles/modelpopup.css" type="text/css" rel="stylesheet" />	
<script type="text/javascript" src="../js/common.js"></script>
<script type="text/javascript" src="../js/modalpopup.js"></script>
<script type="text/javascript">
    function validateact() {
        var msgbox = "";
        if (Form1.acttitle.value == "") {
            msgbox += "活动标题不能为空\r\n";
            Form1.acttitle.focus();
        }
        if (Form1.actid.value == "") {
            msgbox += "活动ID不能为空\r\n";
            Form1.actid.focus();
        } else if (!isNumber(Form1.actid.value)) {
            msgbox += "活动ID必须为数字\r\n";
            Form1.actid.focus();
        }
        if (Form1.actorder.value == "") {
            msgbox += "活动排序顺序不能为空\r\n";
            Form1.actorder.focus();
        } else if (!isNumber(Form1.actorder.value)) {
            msgbox += "活动排序顺序必须为数字\r\n";
            Form1.actorder.focus();
        }
        if (Form1.actpic.value == "") {
            msgbox += "活动图片不能为空\r\n";
            Form1.actpic.focus();
        }
        if (msgbox != "") {
            alert(msgbox);
            return false;
        }
        return true;        
    }

    function addact() {
        if (!validateact()) return;
    
        var addvalue = Form1.actid.value + "|" + Form1.acttitle.value + "|" + $('acttype').value + "|" + Form1.actorder.value + "|" + Form1.actpic.value;
        var thevalue = [Form1.actid.value, Form1.acttitle.value, $('acttype').value, Form1.actorder.value];
        var trobj = document.createElement("tr");
        trobj.className = "mouseoutstyle";
        trobj.setAttribute("style", "cursor:hand;");
        trobj.onmouseover = function() {
            this.className = 'mouseoverstyle';
        };
        trobj.onmouseout = function() {
            this.className = 'mouseoutstyle';
        };

        var tdobj1 = document.createElement("td");
        tdobj1.setAttribute("style", "border-color:#EAE9E1;border-width:1px;border-style:solid;");
        var inputobj = document.createElement("input");
        inputobj.type = "button";
        inputobj.className = "ManagerButton";
        inputobj.value = "删除";
        inputobj.onclick = function() {
            delRow(this);
            $("selitems").value = $("selitems").value.replace("," + addvalue, "");
        };

        tdobj1.appendChild(inputobj);
        trobj.appendChild(tdobj1);

        var tdobj2 = document.createElement("td");
        tdobj2.setAttribute("style", "border-color:#EAE9E1;border-width:1px;border-style:solid;");
        tdobj2.innerHTML = "<img src=\"" + Form1.actpic.value + "\"/>";
        trobj.appendChild(tdobj2);

        for (var i = 0; i < thevalue.length; i++) {
            var tdobj0 = document.createElement("td");
            tdobj0.setAttribute("style", "border-color:#EAE9E1;border-width:1px;border-style:solid;");
            tdobj0.innerHTML = thevalue[i];
            trobj.appendChild(tdobj0);
        }

        $("SelectItem").firstChild.appendChild(trobj);
        $("selitems").value = $("selitems").value + "," + addvalue;
    }

    function del(obj,thevalue) {
        delRow(obj);
        $("selitems").value = $("selitems").value.replace("," + thevalue, "");
    }

    function watchtopic() {
        if (!validateact()) return;
        $("taobaoitemlistgrid").style.display = "block";
        var typeurl = "";
        if ($('acttype').value == "1") typeurl = "http://haibao.huoban.taobao.com/tms/topic.php?pid=<%=taobaouserid%>&eventid=";
        else if ($('acttype').value == "2") typeurl = "http://zhuti.huoban.taobao.com/event.php?pid=<%=taobaouserid%>&eventid=";
        $("lookact").src = typeurl + Form1.actid.value;
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
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;">活动图片</td>
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;">活动ID</td>
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;">活动标题</td>
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;">活动类型</td>                    
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;">排列顺序</td>
                  </tr>
                  <%
                      foreach (string topicstr in rinfo.ccontent.Split(','))
                      {
                       %>
                  <tr class="mouseoutstyle" onmouseover="this.className='mouseoverstyle'" onmouseout="this.className='mouseoutstyle'" style="cursor:hand;">
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;"><input type="button" class="ManagerButton" value="删除" onclick="del(this,'<%=topicstr%>');"/></td>
                    <td style="border-color:#EAE9E1;border-width:1px;border-style:solid;"><img src="<%=topicstr.Split('|')[4]%>" /></td>
                    <td style="border-color:#EAE9E1;border-width:1px;border-style:solid;"><%=topicstr.Split('|')[0]%></td>
                    <td style="border-color:#EAE9E1;border-width:1px;border-style:solid;"><%=topicstr.Split('|')[1]%></td>
                    <td style="border-color:#EAE9E1;border-width:1px;border-style:solid;"><%=topicstr.Split('|')[2]%></td>
                    <td style="border-color:#EAE9E1;border-width:1px;border-style:solid;"><%=topicstr.Split('|')[3]%></td>
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
                            <input type="text" name="actorder" size="8" class="FormBase" onfocus="this.className='FormFocus';" onblur="this.className='FormBase';" value="0"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            活动类型:
                        </td>
                        <td>
                            <select name="acttype">
                                <option value="1" selected>普通型</option>
                                <option value="2">图片型</option>
                            </select>
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
<div class="Navbutton"><input type="button" class="ManagerButton" value="增加该活动" onclick="addact()"/><input type="button" class="ManagerButton" value="预览该活动" onclick="watchtopic()"/></div>	
</fieldset>
<cc1:Hint id="Hint1" runat="server" HintImageUrl="../images"></cc1:Hint>
</div>
<div id="taobaoitemlistgrid" style="display:none"><iframe id="lookact" scrolling="auto" width="100%" height="300px"></iframe></div>
</form>
<%=footer%>
</body>
</html>
