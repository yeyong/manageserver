<%@ Page Language="C#" CodeBehind="company_companygrid.aspx.cs" Inherits="SAS.ManageWeb.ManagePage.company_companygrid" %>
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
                                        <sas:RadioButtonList id="enstatus" runat="server" RepeatColumns="4" HintInfo="企业是否通过审核">
                                            <asp:ListItem Value="-1" Text="不限制" Selected="True"></asp:ListItem>                                          
                                            <asp:ListItem Value="1" Text="审核中"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="审核通过"></asp:ListItem>
                                            <asp:ListItem Value="0" Text="审核未通过"></asp:ListItem>
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
                                        </sas:Calendar>
                                        使用注册日期查找<input id="isbuilddatetime" type="checkbox" value="1" name="cins" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>启用状态:</td>
                                    <td>
                                        <sas:RadioButtonList id="envisible" runat="server" RepeatColumns="3" HintInfo="企业是否通过审批">
                                            <asp:ListItem Value="-1" Text="不限制" Selected="True"></asp:ListItem>                                         
                                            <asp:ListItem Value="1" Text="启用"></asp:ListItem>
                                            <asp:ListItem Value="0" Text="未启用"></asp:ListItem>
                                        </sas:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>                    
                    <tr>
                        <td align="center" colspan="2">
                            <sas:Button ID="Search" runat="server" Text="开始搜索"></sas:Button><sas:Button ID="LocationSet" runat="server" Text="区域JSON数据生成"></sas:Button>
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
                                    <%#GetStatusName(DataBinder.Eval(Container, "DataItem.en_status").ToString()) %>
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
