<%@ Page Language="C#" CodeBehind="global_sysadminusergroupgrid.aspx.cs" Inherits="SAS.ManageWeb.ManagePage.global_sysadminusergroupgrid" %>
<%@ Register TagPrefix="cc1" Namespace="SAS.Control" Assembly="SAS.Control" %>
<%@ Register Src="../UserControls/PageInfo.ascx" TagName="PageInfo" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <meta name="keywords" content="天狼星,工作室" />
    <meta name="description" content="天狼星工作室综合管理后台" />
    <title>天狼星工作室综合管理后台-系统组列表</title>
    <link href="../styles/datagrid.css" type="text/css" rel="stylesheet" />
    <link href="../styles/dntmanager.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" src="../js/common.js"></script>
</head>
<body>
   <form id="Form1" method="post" runat="server">
        <uc1:PageInfo ID="info1" runat="server" Icon="information" Text="<ul><li>Discuz!NT 管理组包括管理员、超级版主、版主以及关联了这三个组的用户组. </ul><ul><li>增加管理组方法有两种:<br />方法1:<ul><li>进入<a href=&quot;global_addadminusergroup.aspx&quot;>管理组添加</a>, 增加一个新的管理组;<li>并在管理权限设置页中选择一个 &quot;关联管理组&quot;　,同时编辑该组的其他设置. </ul></ul><ul>方法2:<ul><li>点击下面的相关管理组记录上的 &quot;添加&quot; 链接,系统会用相关管理组的信息作为模板初始化&quot;添加表单&quot;,同时编辑该组的其他设置. </ul></ul>" />
        <table class="table1" cellspacing="0" cellpadding="4" width="100%" align="center">
            <tr>
                <td class="panelbox">
                    <table width="100%">
                        <tr><td><b>说明: </b></td></tr>
                     </table>
                </td>  
            </tr>
            <tr>
                <td class="panelbox">
                    <table width="100%">
                        <tr><td><ul>"系统组"为系统初始化时数据库中自带的数据信息,管理员可以对相关的系统组进行编辑,但无法删除该系统组.</ul></td></tr>
                    </table>
                </td>
            </tr>
        </table>
        <cc1:DataGrid ID="DataGrid1" runat="server" OnPageIndexChanged="DataGrid_PageIndexChanged" OnSortCommand="Sort_Grid">
            <Columns>
                <asp:TemplateColumn HeaderText="">
                    <ItemTemplate>
						<a href="<%# GetLink(DataBinder.Eval(Container, "DataItem.ug_pg_id").ToString(),DataBinder.Eval(Container, "DataItem.ug_id").ToString())%>">编辑</a>
					</ItemTemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="ug_id" SortExpression="ug_id" HeaderText="用户组ID" Visible="false"></asp:BoundColumn>
                <asp:BoundColumn DataField="ug_name" SortExpression="ug_name" HeaderText="名称"></asp:BoundColumn>
                <asp:BoundColumn DataField="stars" SortExpression="stars" HeaderText="星星数目"></asp:BoundColumn>
                <asp:BoundColumn DataField="ug_readaccess" SortExpression="ug_readaccess" HeaderText="阅读权限"></asp:BoundColumn>
                <asp:BoundColumn DataField="maxpmnum" SortExpression="maxpmnum" HeaderText="短消息最多条数"></asp:BoundColumn>
                <asp:BoundColumn DataField="ug_maxsigsize" SortExpression="ug_maxsigsize" HeaderText="签名最多字节"></asp:BoundColumn>
                <asp:BoundColumn DataField="ug_maxattachsize" SortExpression="ug_maxattachsize" HeaderText="附件最大尺寸 [单位:字节]"></asp:BoundColumn>
            </Columns>
        </cc1:DataGrid>
        <p style="text-align:right;">
            <button type="button" class="ManagerButton" id="Button3" onclick="window.history.back();"><img src="../images/arrow_undo.gif"/> 返 回 </button>           
        </p>
    </form>
    <%=footer%>
</body>
</html>
