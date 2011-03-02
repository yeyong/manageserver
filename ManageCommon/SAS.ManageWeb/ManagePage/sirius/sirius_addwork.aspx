<%@ Page Language="C#" CodeBehind="sirius_addwork.aspx.cs" Inherits="SAS.ManageWeb.ManagePage.sirius_addwork" %>
<%@ Register TagPrefix="yy" Namespace="SAS.Control" Assembly="SAS.Control"%>
<%@ Register TagPrefix="uc1" TagName="TextareaResize" Src="../UserControls/TextareaResize.ascx" %>
<%@ Register TagPrefix="yy1" TagName="OnlineEditor" Src="../UserControls/onlineeditor.ascx" %>
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
        <legend style="background: url(../images/icons/icon52.jpg) no-repeat 6px 50%;">添加成果信息</legend>
        <table width="100%">
            <tr>
                <td class="item_title" colspan="2">
                    成果标题
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
                    主持团队
                </td>
            </tr>
            <tr>
                <td class="vtop rowform">
                    <yy:DropDownList runat="server" ID="teams"></yy:DropDownList>
                </td>
                <td class="vtop">
                    主持项目的主要团队选择
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
                    成果链接或网址
                </td>
            </tr>
            <tr>
                <td class="vtop rowform">
                    <yy:textbox id="url" runat="server" canbenull="必填" requiredfieldtype="网页地址" width="200"></yy:textbox>
                </td>
                <td class="vtop">
                    格式:http://www.cnzhsy.com
                </td>
            </tr>
            <tr>
                <td class="item_title" colspan="2">
                    成果列表图
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
                    成果列表图背景图
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
				<td class="item_title" colspan="2">成员列表:</td>
			</tr>
            <tr>
                <td class="vtop rowform">
					<uc1:TextareaResize id="moderators" runat="server" controlname="moderators" Cols="40" Rows="5"></uc1:TextareaResize>
				</td>
				<td class="vtop">
				以','进行分割,如:lisi,zhangsan
                </td>
			</tr>
            <tr>
                <td class="item_title" colspan="2">
                    成果简述
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <yy1:OnlineEditor ID="message" runat="server" controlname="message" postminchars="0" postmaxchars="200"></yy1:OnlineEditor>
                </td>
            </tr>
        </table>
        <div class="Navbutton">
            <yy:button id="AddActInfo" runat="server" text=" 提 交 " validateform="true"></yy:button>
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
