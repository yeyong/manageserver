<%@ Page Language="C#" CodeBehind="sirius_searchwork.aspx.cs" Inherits="SAS.ManageWeb.ManagePage.sirius_searchwork" %>
<%@ Register TagPrefix="sas" Namespace="SAS.Control" Assembly="SAS.Control" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>查找成果列表</title>
    <link href="../styles/calendar.css" type="text/css" rel="stylesheet" />
    <link href="../styles/dntmanager.css" type="text/css" rel="stylesheet" />
    <link href="../styles/modelpopup.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" src="../js/common.js"></script>
    <script type="text/javascript" src="../js/modalpopup.js"></script>
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
</head>
<body>
    <div class="ManagerForm">
        <form id="Form1" method="post" runat="server">
        <fieldset>
            <legend style="background: url(../images/icons/icon19.jpg) no-repeat 6px 50%;">搜索成果</legend>
            <table width="100%">
                <tr>
                    <td class="item_title" colspan="2">
                        主持团队选择
                    </td>
                </tr>
                <tr>
                    <td class="vtop rowform">
                        <sas:DropDownList ID="teamsel" runat="server">
                        </sas:DropDownList>
                    </td>
                    <td class="vtop">
                    </td>
                </tr>
            </table>
            <sas:Hint ID="Hint1" runat="server" HintImageUrl="../images"></sas:Hint>
            <div class="Navbutton">
                <sas:Button ID="SaveSearchCondition" runat="server" Text="搜索符合条件成果" ButtonImgUrl="../images/search.gif">
                </sas:Button>
                <button type="button" class="ManagerButton" onclick="javascript:window.location.href='sirius_addwork.aspx';">
                    <img src="../images/add.gif" />添加团队成果</button>
            </div>
        </fieldset>
        <div id="topictypes" style="display: none;">
        </div>
        </form>
    </div>
    <%=footer%>
</body>
</html>
