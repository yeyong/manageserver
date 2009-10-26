﻿<%@ Page Language="C#" CodeBehind="global_usergroupgrid.aspx.cs" Inherits="SAS.ManageWeb.ManagePage.global_usergroupgrid" %>
<%@ Register TagPrefix="cc1" Namespace="SAS.Control" Assembly="SAS.Control" %>
<%@ Register Src="../UserControls/PageInfo.ascx" TagName="PageInfo" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <meta name="keywords" content="天狼星,工作室" />
    <meta name="description" content="天狼星工作室综合管理后台" />
    <title>天狼星工作室综合管理后台-用户组列表</title>
    <link href="../styles/datagrid.css" type="text/css" rel="stylesheet" />
    <link href="../styles/dntmanager.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" src="../js/common.js"></script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <uc1:PageInfo ID="info1" runat="server" Icon="information" Text="增加用户组方法有两种:<br />方法1: 进入<a href=&quot;global_addusergroup.aspx&quot;>用户组添加</a>, 增加一个新的用户组,同时编辑该组的其他设置. <br />方法2: 点击下面的相关用户组记录上的 &quot;添加&quot; 链接,系统会用相关用户组的信息作为模板初始化&quot;添加表单&quot;,同时编辑该组的其他设置." />
        <cc1:DataGrid ID="DataGrid1" runat="server" IsFixConlumnControls="true" OnPageIndexChanged="DataGrid_PageIndexChanged" 
            OnSortCommand="Sort_Grid">
            <Columns>
                <asp:TemplateColumn HeaderText="">
                    <itemtemplate>
						<a href="global_editusergroup.aspx?groupid=<%# DataBinder.Eval(Container, "DataItem.ug_id").ToString()%>">编辑</a>
						<%# DataGrid1.LoadSelectedCheckBox(DataBinder.Eval(Container, "DataItem.groupid").ToString())%>
					</itemtemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="">
                    <itemtemplate>
						<a href="global_addusergroup.aspx?groupid=<%# DataBinder.Eval(Container, "DataItem.ug_id").ToString()%>">添加</a>
					</itemtemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="ug_id" SortExpression="ug_id" HeaderText="用户组ID" Visible="false"></asp:BoundColumn>
                <asp:BoundColumn DataField="ug_name" SortExpression="ug_name" HeaderText="名称"></asp:BoundColumn>
                <asp:BoundColumn DataField="creditshigher" SortExpression="creditshigher" HeaderText="积分下限"></asp:BoundColumn>
                <asp:BoundColumn DataField="creditslower" SortExpression="creditslower" HeaderText="积分上限"></asp:BoundColumn>
                <asp:BoundColumn DataField="stars" SortExpression="stars" HeaderText="星星数目" readonly="true"></asp:BoundColumn>
                <asp:BoundColumn DataField="ug_readaccess" SortExpression="ug_readaccess" HeaderText="阅读权限" readonly="true"></asp:BoundColumn>
                <asp:BoundColumn DataField="maxprice" SortExpression="maxprice" HeaderText="主题(附件)最高售价" readonly="true"></asp:BoundColumn>
                <asp:BoundColumn DataField="maxpmnum" SortExpression="maxpmnum" HeaderText="短消息最多条数" readonly="true"></asp:BoundColumn>
                <asp:BoundColumn DataField="maxsigsize" SortExpression="maxsigsize" HeaderText="签名最多字节" readonly="true"></asp:BoundColumn>
                <asp:BoundColumn DataField="maxattachsize" SortExpression="maxattachsize" HeaderText="附件最大尺寸 [单位:字节]" readonly="true"></asp:BoundColumn>
            </Columns>
        </cc1:DataGrid>
        <p style="text-align:right;">
            <cc1:Button ID="EditUserGroup" runat="server" Text=" 提交 " OnClick="EditUserGroup_Click"></cc1:Button>&nbsp;&nbsp;
            <button type="button" class="ManagerButton" id="Button3" onclick="window.location='global_editgroup.aspx';"><img src="../images/arrow_undo.gif"/> 返 回 </button>           
        </p>
    </form>
    <%=footer%>
</body>
</html>
