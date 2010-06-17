<%@ Page Language="C#" CodeBehind="global_commentedit.aspx.cs" Inherits="SAS.ManageWeb.ManagePage.commentedit" %>
<%@ Register TagPrefix="sas" Namespace="SAS.Control" Assembly="SAS.Control" %>
<%@ Register Src="../UserControls/PageInfo.ascx" TagName="PageInfo" TagPrefix="yy1" %>
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
    <script type="text/javascript">
        function Check(form, checked) {
            CheckByName(form, 'commid', checked);
            checkedEnabledButton(form, 'commid', 'ENPause');
        }
    </script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <yy1:PageInfo ID="PageInfo1" runat="server" Icon="information" Text="" />
        <table width="100%">
            <tr>
                <td>
                    <sas:DataGrid ID="DataGrid1" runat="server" OnPageIndexChanged="DataGrid_PageIndexChanged" OnSortCommand="Sort_Grid" PageSize="15">
                        <Columns>
                            <asp:TemplateColumn HeaderText="<input title='选中/取消' onclick='Check(this.form,this.checked)' type='checkbox' name='chkall' id='chkall' />">
                                <HeaderStyle Width="20px" />
                                <ItemTemplate>
						            <input id="commid" type="checkbox" onclick="checkedEnabledButton(form, 'commid', 'ENPause')" value="<%# DataBinder.Eval(Container, "DataItem.commentid").ToString()%>" name="commid">
						        </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="username" SortExpression="username" HeaderText="用户名" ReadOnly="true"></asp:BoundColumn>
                            <asp:BoundColumn DataField="userip" SortExpression="userip" HeaderText="用户IP" ReadOnly="true"></asp:BoundColumn>
                            <asp:BoundColumn DataField="commentdate" SortExpression="commentdate" HeaderText="评论时间" ReadOnly="true"></asp:BoundColumn>
                            <asp:BoundColumn DataField="scored" SortExpression="scored" HeaderText="打分" ReadOnly="true"></asp:BoundColumn>
                            <asp:BoundColumn DataField="content" SortExpression="content" HeaderText="评论内容" ReadOnly="true" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                        </Columns>
                    </sas:DataGrid>
                </td>
            </tr>
        </table>
        <p style="text-align:right;">
            <table style="float:right">
                <tr>
                    <td><sas:Button ID="ENPause" runat="server" Text=" 删 除 " ButtonImgUrl="../images/del.gif" Enabled="false"></sas:Button>&nbsp;&nbsp;</td>
                    <td><button type="button" class="ManagerButton" id="Button3" onclick="window.history.back();"><img src="../images/arrow_undo.gif"/> 返 回 </button></td>
                </tr>
            </table>
        </p>
        <sas:Hint id="Hint1" runat="server" HintImageUrl="../images"></sas:Hint>
    </form>
    <%=footer%>	
</body>
</html>