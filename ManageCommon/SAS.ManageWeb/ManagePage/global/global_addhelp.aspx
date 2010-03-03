<%@ Page Language="C#" CodeBehind="global_addhelp.aspx.cs" Inherits="SAS.ManageWeb.ManagePage.global_addhelp" %>
<%@ Register TagPrefix="yy" Namespace="SAS.Control" Assembly="SAS.Control"%>
<%@ Register TagPrefix="yy1" TagName="OnlineEditor" Src="../UserControls/onlineeditor.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>添加帮助</title>

    <script type="text/javascript" src="../js/common.js"></script>

    <link href="../styles/dntmanager.css" type="text/css" rel="stylesheet" />

    <meta http-equiv="X-UA-Compatible" content="IE=7" />
</head>
<body>
    <div class="ManagerForm">
        <form id="Form1" runat="server">
        <fieldset>
            <legend style="background: url(../images/icons/legendimg.jpg) no-repeat 6px 50%;">添加帮助</legend>
            <table width="100%">
                <tr>
                    <td class="item_title" colspan="2">
                        帮助标题
                    </td>
                </tr>
                <tr>
                    <td class="vtop rowform">
                        <yy:TextBox ID="title" runat="server" CanBeNull="必填" RequiredFieldType="暂无校验" MaxLength="249" Size="60"></yy:TextBox>
                    </td>
                    <td class="vtop">
                    </td>
                </tr>
                <tr>
                    <td class="item_title" colspan="2">
                        帮助分类
                    </td>
                </tr>
                <tr>
                    <td class="vtop rowform">
                        <yy:DropDownList ID="type" runat="server">
                        </yy:DropDownList>
                    </td>
                    <td class="vtop">
                    </td>
                </tr>
                <tr>
                    <td class="item_title" colspan="2">
                        帮助内容
                    </td>
                </tr>
                <tr>
                    <td class="vtop" colspan="2">
                        <yy1:onlineeditor id="message" runat="server" controlname="message" postminchars="0" postmaxchars="200"></yy1:onlineeditor>
                    </td>
                </tr>
            </table>
            <div style="display: none">
                <tr>
                    <td class="item_title" colspan="2">
                        发布者用户名
                    </td>
                </tr>
                <tr>
                    <td class="vtop rowform">
                        <yy:TextBox ID="poster" runat="server" RequiredFieldType="暂无校验" CanBeNull="必填" MaxLength="20" Enabled="false"></yy:TextBox>
                    </td>
                    <td class="vtop">
                    </td>
                </tr>
            </div>
            <div class="Navbutton">
                <yy:Button ID="Addhelp" runat="server" Text=" 提 交 "></yy:Button>&nbsp;&nbsp;
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
