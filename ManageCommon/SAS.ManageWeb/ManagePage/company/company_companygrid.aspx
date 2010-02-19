<%@ Page Language="C#" CodeBehind="company_companygrid.aspx.cs" Inherits="SAS.ManageWeb.ManagePage.company.company_companygrid" %>
<%@ Register TagPrefix="sas" Namespace="SAS.Control" Assembly="SAS.Control" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <meta name="keywords" content="天狼星,工作室" />
    <meta name="description" content="天狼星工作室综合管理后台" />
    <title>天狼星工作室综合管理后台</title>
    <link href="../styles/datagrid.css" type="text/css" rel="stylesheet" />
    <link href="../styles/calendar.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" src="../js/common.js"></script>
    <link href="../styles/dntmanager.css" type="text/css" rel="stylesheet" />
    <link href="../styles/modelpopup.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" src="../js/modalpopup.js"></script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <div class="ManagerForm">
            <fieldset>
                <legend style="background: url(../images/icons/icon32.jpg) no-repeat 6px 50%;">搜索企业</legend>
                <asp:Panel ID="searchtable" runat="server" Visible="true">
                <table cellspacing="0" cellpadding="4" width="100%" align="center">
                    <tr>
                        <td  class="panelbox" width="50%" align="left">
                            <table width="100%">
                                <tr>
                                    <td style="width: 80px">企业名称:</td>
                                    <td>
                                        <sas:TextBox ID="enname" runat="server" RequiredFieldType="暂无校验" Size="40"></sas:TextBox>&nbsp;
                                        模糊查找<input id="islike" type="checkbox" value="1" name="cins" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>审核状态:</td>
                                    <td>
                                        <sas:RadioButtonList id="enstatus" runat="server" RepeatColumns="3" HintInfo="企业是否通过审批">                                            
                                            <asp:ListItem Value="1" Text="审批中" Selected="True"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="审批通过"></asp:ListItem>
                                            <asp:ListItem Value="0" Text="审批未通过"></asp:ListItem>
                                        </sas:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td  class="panelbox" width="50%" align="right">
                            <table width="100%">
                                <tr>
                                    <td>创建日期:</td>
                                    <td>
                                        从&nbsp;<sas:Calendar ID="joindateStart" runat="server" ReadOnly="False" ScriptPath="../js/calendar.js">
                                        </sas:Calendar>
                                        到&nbsp;<sas:Calendar ID="joindateEnd" runat="server" ReadOnly="False" ScriptPath="../js/calendar.js">
                                        </sas:Calendar><br />
                                        使用日期查找<input id="ispostdatetime" type="checkbox" value="1" name="cins" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>                    
                    <tr>
                        <td align="center" colspan="2">
                            <sas:Button ID="Search" runat="server" Text="开始搜索"></sas:Button>
                        </td>
                    </tr>
                </table>
                </asp:Panel>
                <sas:Button ID="ResetSearchTable" runat="server" Text="重设搜索条件" Visible="False"></sas:Button>
            </fieldset>
        </div>
        <sas:Hint id="Hint1" runat="server" HintImageUrl="../images"></sas:Hint>
    </form>
    <%=footer%>	
</body>
</html>
