<%@ Page Language="C#" CodeBehind="sirius_editactivity.aspx.cs" Inherits="SAS.ManageWeb.ManagePage.sirius_editactivity" %>
<%@ Register TagPrefix="yy" Namespace="SAS.Control" Assembly="SAS.Control"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
    <meta name="keywords" content="天狼星,工作室" />
    <meta name="description" content="天狼星工作室综合管理后台" />
    <title>天狼星工作室综合管理后台-添加团队</title>
    <script type="text/javascript" src="../js/common.js"></script>
    <link href="../styles/dntmanager.css" type="text/css" rel="stylesheet" />        
    <link href="../styles/modelpopup.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" src="../js/modalpopup.js"></script>
    <script type="text/javascript">
        function add_act() {
            var finalObj = document.getElementById("selitems");
            var addObj = document.getElementById("insert_pic");
            var Obj = document.getElementById("pics");
            if (addObj.value == '') return;
            var trobj = document.createElement("tr");
            var tdobj = document.createElement("td");
            tdobj.setAttribute("align", "center");
            tdobj.setAttribute("nowrap", "nowrap");
            tdobj.innerHTML = "<img src=\"" + addObj.value + "\"/><br/>";
            var inputobj = document.createElement("input");
            inputobj.type = "button";
            inputobj.className = "ManagerButton";
            inputobj.value = "删除";
            inputobj.onclick = function() {
                delRow(this);
                finalObj.value = finalObj.value.replace(addObj.value, "").replace(",,", ",");
            };
            tdobj.appendChild(inputobj);
            trobj.appendChild(tdobj);
            Obj.firstChild.appendChild(trobj);
            finalObj.value = finalObj.value == "" ? addObj.value : finalObj.value + "," + addObj.value;
            addObj.value = "";
        }

        function del_act(obj, thevalue) {
            var finalObj = document.getElementById("selitems");
            delRow(obj);
            finalObj.value = finalObj.value.replace(thevalue, "").replace(",,", ",");
        }
    
        function validate(theform) {
            if (document.getElementById("title").value == "") {
                alert("标题不能为空");
                document.getElementById("title").focus();
                return false;
            }
            return true;
        }
    </script>
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
</head>
<body>
<div class="ManagerForm">
    <form id="Form1" runat="server" onsubmit="return validate(this);">
    <fieldset>
        <legend style="background: url(../images/icons/icon52.jpg) no-repeat 6px 50%;">添加活动信息</legend>
        <table width="100%">
            <tr>
                <td class="item_title" colspan="2">
                    活动标题
                </td>
            </tr>
            <tr>
                <td class="vtop rowform">
                    <yy:textbox id="title" runat="server" canbenull="必填" requiredfieldtype="暂无校验" maxlength="249" size="60"></yy:textbox>
                </td>
                <td class="vtop">
                </td>
            </tr>
            <tr>
                <td class="item_title" colspan="2">
                    发起团队
                </td>
            </tr>
            <tr>
                <td class="vtop rowform">
                    <yy:DropDownList runat="server" ID="teams"></yy:DropDownList>
                </td>
                <td class="vtop">
                    发起活动的主要团队选择
                </td>
            </tr>
            <tr>
                <td class="item_title" colspan="2">
                    起始时间
                </td>
            </tr>
            <tr>
                <td class="vtop rowform">
                    <yy:textbox id="starttime" runat="server" canbenull="必填" requiredfieldtype="日期" width="200"></yy:textbox>
                </td>
                <td class="vtop">
                    格式:2005-5-5
                </td>
            </tr>
            <tr>
                <td class="item_title" colspan="2">
                    结束时间
                </td>
            </tr>
            <tr>
                <td class="vtop rowform">
                    <yy:textbox id="endtime" runat="server" canbenull="必填" requiredfieldtype="日期" width="200"></yy:textbox>
                </td>
                <td class="vtop">
                    格式:2005-5-5
                </td>
            </tr>
            <tr>
                <td class="item_title" colspan="2">
                    活动列表图
                </td>
            </tr>
            <tr>
                <td class="vtop rowform">
                    <yy:textbox id="listpic" runat="server" canbenull="必填" requiredfieldtype="暂无校验" maxlength="249" size="60"></yy:textbox>
                </td>
                <td class="vtop">
                </td>
            </tr>
            <tr>
                <td class="item_title" colspan="2">
                    活动列表图背景图
                </td>
            </tr>
            <tr>
                <td class="vtop rowform">
                    <yy:textbox id="listbak" runat="server" canbenull="必填" requiredfieldtype="暂无校验" maxlength="249" size="60"></yy:textbox>
                </td>
                <td class="vtop">
                </td>
            </tr>
            <tr>
                <td class="item_title" colspan="2">
                    活动简述
                </td>
            </tr>
            <tr>
                <td class="vtop rowform">
                    <yy:textbox id="shortdesc" runat="server" canbenull="必填" requiredfieldtype="暂无校验" width="450" height="100" TextMode="MultiLine" IsReplaceInvertedComma="false"></yy:textbox>
                </td>
                <td class="vtop">
                </td>
            </tr>
            <tr>
                <td class="item_title" colspan="2">
                    活动图组
                </td>
            </tr>
            <tr>
                <td class="item_title" colspan="2">
                    <div style="overflow:auto;height:300px">
                    <table class="datagridStyles" cellspacing="0" cellpadding="3" border="0" id="pics">
                    <%if (!string.IsNullOrEmpty(picsliststr))
                      {%>
                    <%foreach (string imgstr in picsliststr.Split(','))
                      {%>
                    <tr>
                        <td align="center" nowrap="nowrap"><img src="<%=imgstr%>"/><br/><input type="button" value="删除" class="ManagerButton" onclick="del_act(this,'<%=imgstr%>')"/></td>
                    </tr>
                    <%}
                      }%>
                    </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="item_title" colspan="2">
                    增加图片
                </td>
            </tr>
            <tr>
                <td class="vtop rowform">
                    <input type="text" id="insert_pic"  size="60" class="FormBase" onfocus="this.className='FormFocus';" onblur="this.className='FormBase';"/> <input type="button" value="增加" class="ManagerButton" onclick="add_act()"/>
                </td>
                <td class="vtop">
                </td>
            </tr>
        </table>
        <div class="Navbutton">
            <yy:button id="UpActInfo" runat="server" text=" 提 交 " validateform="true"></yy:button><input type="hidden" name="selitems" id="selitems" value="<%=picsliststr%>"/>
            &nbsp;&nbsp;
            <button type="button" class="ManagerButton" id="Button3" onclick="window.history.back();">
                <img src="../images/arrow_undo.gif" />
                返 回
            </button>
        </div>
    </fieldset>
    </form>
    </div>
    <%=footer%>
</body>
</html>