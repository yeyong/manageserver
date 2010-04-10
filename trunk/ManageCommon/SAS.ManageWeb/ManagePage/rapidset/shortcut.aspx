<%@ Page Language="C#" CodeBehind="shortcut.aspx.cs" Inherits="SAS.ManageWeb.ManagePage.shortcut" %>
<%@ Register TagPrefix="yy" Namespace="SAS.Control" Assembly="SAS.Control" %>
<%@ Import NameSpace="SAS.Common"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>shortcut</title>
    <link href="../styles/dntmanager.css" type="text/css" rel="stylesheet" />
    <link href="../styles/modelpopup.css" type="text/css" rel="stylesheet" />

    <script type="text/javascript" src="../js/common.js"></script>

    <script type="text/javascript" src="../js/modalpopup.js"></script>

    <script type="text/javascript">
        var currentid = 0;
        var bar = 0;
        var filenameliststr = '<%=filenamelist%>';
        var filenamelist = new Array();
        filenamelist = filenameliststr.split('|');

        function runstatic() {
            if (filenamelist[currentid] != "") {
                document.getElementById('Layer5').innerHTML = '<br /><table><tr><td valign=top><img border=\"0\" src=\"../images/ajax_loading.gif\"  /></td><td valign=middle style=\"font-size: 14px;\" >正在更新' + filenamelist[currentid] + '.htm模板, 请稍等...<BR /></td></tr></table><BR />';
                document.getElementById('Layer5').style.witdh = '350';
                document.getElementById('success').style.witdh = '400';
                document.getElementById('success').style.display = "block";
                getReturn('createtemplate.aspx?type=single&path=' + document.getElementById('Templatepath').value + '&filename=' + filenamelist[currentid]);
                currentid++;
            }
            else {
                document.getElementById('Layer5').innerHTML = "<br />模板更新成功, 请稍等...";
                document.getElementById('success').style.display = "block";
                count();
                document.getElementById('Form1').submit();
            }
        }

        function count() {
            bar = bar + 2;
            if (bar < 99) { setTimeout("count()", 100); }
            else { document.getElementById('success').style.display = "none"; }
        }

        function run() {
            bar = 0;
            document.getElementById('Layer5').innerHTML = "<BR /><table><tr><td valign=top><img border=\"0\" src=\"../images/ajax_loading.gif\"  /></td><td valign=middle style=\"font-size: 14px;\" >正在提交数据, 请稍等...<BR /></td></tr></table><BR />";
            document.getElementById('success').style.display = "block";
            setInterval('runstatic()', 5000); //每次提交时间为6秒
        }

        function validateform(theform) {
            document.getElementById('Form1').submit();
            return true;
        }

        function validate(theform) {
            if (document.getElementById('createtype').checked) {
                run();
                return false;
            }
            else {
                return true;
            }
        }    
    </script>
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <table cellspacing="0" cellpadding="4" width="100%" align="center">
        <tr>
            <td class="panelbox">
                <table width="100%">
                    <tr>
                        <td style="width: 120px">
                            编辑用户:
                        </td>
                        <td style="width: 120px">
                            <yy:TextBox ID="Username" runat="server" RequiredFieldType="暂无校验" Width="200"></yy:TextBox>
                        </td>
                        <td>
                            <yy:Button ID="EditUser" runat="server" Text="提 交"></yy:Button>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            编辑栏目:
                        </td>
                        <td>
                            <yy:DropDownTreeList ID="forumid" runat="server"></yy:DropDownTreeList>
                        </td>
                        <td>
                            <yy:Button ID="EditForum" runat="server" Text="提 交"></yy:Button>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            编辑用户组:
                        </td>
                        <td>
                            <yy:DropDownList ID="Usergroupid" runat="server">
                            </yy:DropDownList>
                        </td>
                        <td>
                            <yy:Button ID="EditUserGroup" runat="server" Text="提 交"></yy:Button>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            生成模板:
                        </td>
                        <td>
                            <yy:DropDownList ID="Templatepath" runat="server">
                            </yy:DropDownList>
                            <input type="checkbox" id="createtype" name="createtype">降低CPU占用
                        </td>
                        <td>
                            <yy:Button ID="CreateTemplate" runat="server" Text="提 交" ValidateForm="true"></yy:Button>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:LinkButton ID="UpdateCache" CssClass="ManagerButton" runat="server" Text="<span>更新缓存</span>"></asp:LinkButton>
                <asp:LinkButton ID="UpdateForumStatistics" CssClass="ManagerButton" runat="server"
                    Text="<span>更新站点统计</span>"></asp:LinkButton>
            </td>
        </tr>
    </table>
    <fieldset style="overflow: hidden; zoom: 1; margin-top: 10px; background-color: #f5f7f8;
        padding: 10px">
        <legend style="padding: 0pt 10px; margin-left: 25px; font-size: 13px; color: #000;">
            系统信息</legend>
        <table width="100%">
            <tr>
                <td>
                    服务器名称:
                </td>
                <td align="left">
                    <%=Server.MachineName%>
                </td>
                <td>
                    服务器操作系统:
                </td>
                <td align="left">
                    <%=Environment.OSVersion.ToString()%>
                </td>
            </tr>
            <tr>
                <td>
                    服务器IIS版本:
                </td>
                <td align="left">
                    <%=Request.ServerVariables["SERVER_SOFTWARE"] %>
                </td>
                <td>
                    .NET解释引擎版本:
                </td>
                <td align="left">
                    .NET CLR
                    <%=Environment.Version.Major %>.<%=Environment.Version.Minor %>.<%=Environment.Version.Build %>.<%=Environment.Version.Revision %>
                </td>
            </tr>
        </table>
    </fieldset>
    <div id="BOX_overlay" style="background: #000; position: absolute; z-index: 100;
        filter: alpha(opacity=50); -moz-opacity: 0.6; opacity: 0.6;">
    </div>
    <div id="setting" />
    </form>
    <%=footer%>
</body>
</html>
