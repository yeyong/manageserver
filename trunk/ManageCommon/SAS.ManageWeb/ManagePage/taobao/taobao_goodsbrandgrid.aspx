<%@ Page Language="C#" CodeBehind="taobao_goodsbrandgrid.aspx.cs" Inherits="SAS.ManageWeb.ManagePage.taobao_goodsbrandgrid" %>
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
    <script type="text/javascript">
        function Check(form, checked) {
            CheckByName(form, 'enid', checked);
            checkedEnabledButton(form, 'enid', 'ENStart', 'ENPause');
        }
    </script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <div class="ManagerForm">
            <fieldset>
                <legend style="background: url(../images/icons/icon32.jpg) no-repeat 6px 50%;">搜索品牌</legend>
                <asp:Panel ID="searchtable" runat="server" Visible="true">
                <table cellspacing="0" cellpadding="4" width="100%" align="center">
                    <tr>
                        <td  class="panelbox" width="50%" align="left">
                            <table width="100%">
                                <tr>
                                    <td style="width: 80px">品牌名称:</td>
                                    <td>
                                        <sas:TextBox ID="brandname" runat="server" RequiredFieldType="暂无校验" Size="40"></sas:TextBox>&nbsp;
                                        同时查找品牌别名<input id="islike" type="checkbox" value="1" name="cins" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>启用状态:</td>
                                    <td>
                                        <sas:RadioButtonList id="enstatus" runat="server" RepeatColumns="3" HintInfo="品牌是否启用">
                                            <asp:ListItem Value="-1" Text="全部" Selected=True></asp:ListItem>
                                            <asp:ListItem Value="1" Text="启用中"></asp:ListItem>
                                            <asp:ListItem Value="0" Text="暂停中"></asp:ListItem>
                                        </sas:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td  class="panelbox" width="50%" align="right">
                            <table width="100%">
                                <tr>
                                    <td>相关类别:</td>
                                    <td>
                                        <sas:DropDownTreeList runat="server" ID="relateclass" />
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
        <table width="100%">
            <tr>
                <td>
                    <sas:DataGrid ID="DataGrid1" runat="server" OnPageIndexChanged="DataGrid_PageIndexChanged" OnSortCommand="Sort_Grid" PageSize="15">
                        <Columns>
                            <asp:TemplateColumn HeaderText="<input title='选中/取消' onclick='Check(this.form,this.checked)' type='checkbox' name='chkall' id='chkall' />">
                                <HeaderStyle Width="20px" />
                                <ItemTemplate>
						            <input id="enid" type="checkbox" onclick="checkedEnabledButton(form, 'enid', 'ENStart', 'ENPause')" value="<%# DataBinder.Eval(Container, "DataItem.id").ToString()%>"	name="enid">
						        </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="">
                                <ItemTemplate>
                                    <a href="taobao_editgoodsbrand.aspx?id=<%# DataBinder.Eval(Container, "DataItem.id").ToString()%>">
                                        编辑</a>
                                    <%# DataGrid1.LoadSelectedCheckBox(DataBinder.Eval(Container, "DataItem.id").ToString())%>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="品牌Logo">
                                <ItemTemplate>
                                    <img src="<%# DataBinder.Eval(Container, "DataItem.logo").ToString()%>" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="bname" SortExpression="bname" HeaderText="品牌名称" ReadOnly="true"></asp:BoundColumn>
                            <asp:BoundColumn DataField="spell" SortExpression="spell" HeaderText="品牌别名" ReadOnly="true"></asp:BoundColumn>
                            <asp:BoundColumn DataField="cname" SortExpression="cname" HeaderText="关联类别" ReadOnly="true"></asp:BoundColumn>
                            <asp:BoundColumn DataField="website" SortExpression="website" HeaderText="官方网址" ReadOnly="true"></asp:BoundColumn>
                            <asp:BoundColumn DataField="bcompany" SortExpression="bcompany" HeaderText="官方公司" ReadOnly="true"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="开启状态" SortExpression="status">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.status").ToString() == "0" ? "暂停" : "开启"%>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </sas:DataGrid>
                </td>
            </tr>
        </table>
        <p style="text-align:right;">
            <table style="float:right">
                <tr>
                    <td><sas:Button ID="ENStart" runat="server" Text=" 开 启 " designtimedragdrop="247" Enabled="false"></sas:Button>&nbsp;&nbsp;</td>
                    <td><sas:Button ID="ENPause" runat="server" Text=" 暂 停 " ButtonImgUrl="../images/del.gif" Enabled="false"></sas:Button>&nbsp;&nbsp;</td>
                </tr>
            </table>
        </p>
        <sas:Hint id="Hint1" runat="server" HintImageUrl="../images"></sas:Hint>
    </form>
    <%=footer%>	
</body>
</html>