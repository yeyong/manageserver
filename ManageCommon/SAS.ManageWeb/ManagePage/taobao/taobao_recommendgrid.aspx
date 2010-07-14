<%@ Page Language="C#" CodeBehind="taobao_recommendgrid.aspx.cs" Inherits="SAS.ManageWeb.ManagePage.taobao_recommendgrid" %>
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
                <legend style="background: url(../images/icons/icon32.jpg) no-repeat 6px 50%;">搜索<%=rtypestr%>推荐信息</legend>
                <asp:Panel ID="searchtable" runat="server" Visible="true">
                <table cellspacing="0" cellpadding="4" width="100%" align="center">
                    <tr>
                        <td  class="panelbox" width="50%" align="left">
                            <table width="100%">
                                <tr>
                                    <td style="width: 80px"><%=rtypestr%>推荐标题:</td>
                                    <td>
                                        <sas:TextBox ID="enname" runat="server" RequiredFieldType="暂无校验" Size="40"></sas:TextBox>&nbsp;
                                        模糊查找<input id="islike" type="checkbox" value="1" name="cins" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>相关频道:</td>
                                    <td>
                                        <sas:DropDownList runat="server" ID="rchanel">
                                        </sas:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>相关类别:</td>
                                    <td>
                                        <sas:DropDownTreeList runat="server" ID="rcategory" />
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
                                        </sas:Calendar>
                                        使用创建日期查找<input id="isbuilddatetime" type="checkbox" value="1" name="cins" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>更新日期:</td>
                                    <td>
                                        从&nbsp;<sas:Calendar ID="updatedateStart" runat="server" ReadOnly="False" ScriptPath="../js/calendar.js">
                                        </sas:Calendar>
                                        到&nbsp;<sas:Calendar ID="updatedateEnd" runat="server" ReadOnly="False" ScriptPath="../js/calendar.js">
                                        </sas:Calendar>
                                        使用更新日期查找<input id="isupdatedatetime" type="checkbox" value="1" name="cins" runat="server" />
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
						            <input id="enid" type="checkbox" onclick="checkedEnabledButton(form, 'enid', 'ENStart', 'ENPause')" value="<%# DataBinder.Eval(Container, "DataItem.en_id").ToString()%>"	name="enid">
						        </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="">
                                <ItemTemplate>
                                    <a href="company_companyedit.aspx?enid=<%# DataBinder.Eval(Container, "DataItem.en_id").ToString()%>">
                                        编辑</a>
                                    <%# DataGrid1.LoadSelectedCheckBox(DataBinder.Eval(Container, "DataItem.en_id").ToString())%>
                                    <a href="../global/global_commentedit.aspx?objid=<%# DataBinder.Eval(Container, "DataItem.en_id").ToString()%>">
                                        评论</a>
                                    <%# DataGrid1.LoadSelectedCheckBox(DataBinder.Eval(Container, "DataItem.en_id").ToString())%>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="en_name" SortExpression="en_name" HeaderText="名称" ReadOnly="true"></asp:BoundColumn>
                            <asp:BoundColumn DataField="en_update" SortExpression="en_update" HeaderText="最近更新时间" ReadOnly="true"></asp:BoundColumn>
                            <asp:BoundColumn DataField="en_createdate" SortExpression="en_createdate" HeaderText="信息创建时间" ReadOnly="true"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="开启状态" SortExpression="en_visble">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.en_visble").ToString() == "0" ? "暂停":"开启"%>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="开启状态" SortExpression="en_status">
                                <ItemTemplate>
                                    
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </sas:DataGrid>
                </td>
            </tr>
        </table>
        <sas:Hint id="Hint1" runat="server" HintImageUrl="../images"></sas:Hint>
    </form>
    <%=footer%>	
</body>
</html>
