<%@ Page Language="C#" CodeBehind="global_usergrid.aspx.cs" Inherits="SAS.ManageWeb.ManagePage.global_usergrid" %>
<%@ Register TagPrefix="cc1" Namespace="SAS.Control" Assembly="SAS.Control" %>
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
        function Check(form,checked)
        {
            CheckByName(form,'uid',checked);
            checkedEnabledButton(form,'uid','StopTalk','DeleteUser')
        }
    </script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <div class="ManagerForm">
            <fieldset>
                <legend style="background: url(../images/icons/icon32.jpg) no-repeat 6px 50%;">搜索用户</legend>
                <asp:Panel ID="searchtable" runat="server" Visible="true">
                <table cellspacing="0" cellpadding="4" width="100%" align="center">
                    <tr>
                        <td  class="panelbox" width="50%" align="left">
                            <table width="100%">
                                <tr>
                                    <td style="width: 80px">用 户 名:</td>
                                    <td>
                                        <cc1:TextBox ID="Username" runat="server" RequiredFieldType="暂无校验" Width="100"></cc1:TextBox>&nbsp;
                                        模糊查找<input id="islike" type="checkbox" value="1" name="cins" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>用 户 组:</td>
                                    <td>
                                        <cc1:DropDownList ID="UserGroup" runat="server">
                                        </cc1:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>用户ID (UID):</td>
                                    <td>
                                        <cc1:TextBox ID="uid" runat="server" RequiredFieldType="暂无校验" Width="100"></cc1:TextBox>&nbsp;格式:1,2,3
                                        <asp:RegularExpressionValidator ID="homephone" runat="SERVER" ControlToValidate="uid"
                                            ErrorMessage="输入错误" ValidationExpression="^([1-9]*,)*[0-9]*$">
                                        </asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>用户积分:</td>
                                    <td>
                                        大于或等于:<cc1:TextBox ID="credits_start" runat="server" RequiredFieldType="数据校验" Size="8" MaxLength="9"></cc1:TextBox><br />
                                        小于或等于:<cc1:TextBox ID="credits_end" runat="server" RequiredFieldType="数据校验" Size="8" MaxLength="9"></cc1:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>用户发帖数:</td>
                                    <td>
                                        大于或等于:<cc1:TextBox ID="posts" runat="server" RequiredFieldType="数据校验" Width="80"></cc1:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td  class="panelbox" width="50%" align="right">
                            <table width="100%">
                                <tr>
                                    <td style="width: 80px">昵 称:</td>
                                    <td>
                                        <cc1:TextBox ID="nickname" runat="server" RequiredFieldType="暂无校验" Width="150"></cc1:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Email 包含:</td>
                                    <td>
                                        <cc1:TextBox ID="email" runat="server" RequiredFieldType="暂无校验" Width="150"></cc1:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>注册日期:</td>
                                    <td>
                                        从&nbsp;<cc1:Calendar ID="joindateStart" runat="server" ReadOnly="False" ScriptPath="../js/calendar.js">
                                        </cc1:Calendar>
                                        到&nbsp;<cc1:Calendar ID="joindateEnd" runat="server" ReadOnly="False" ScriptPath="../js/calendar.js">
                                        </cc1:Calendar><br />
                                        使用日期查找<input id="ispostdatetime" type="checkbox" value="1" name="cins" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>最后登陆IP:</td>
                                    <td>
                                        <cc1:TextBox ID="lastip" runat="server" RequiredFieldType="IP地址" Width="150"></cc1:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>用户精华帖数:</td>
                                    <td>
                                        大于或等于:<cc1:TextBox ID="digestposts" runat="server" RequiredFieldType="数据校验" Width="80"></cc1:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>                    
                    <tr>
                        <td align="center" colspan="2">
                            <cc1:Button ID="Search" runat="server" Text="开始搜索"></cc1:Button>
                        </td>
                    </tr>
                </table>
                </asp:Panel>
                <cc1:Button ID="ResetSearchTable" runat="server" Text="重设搜索条件" Visible="False"></cc1:Button>
            </fieldset>
        </div>
        <br />
        <table width="100%">
            <tr>
                <td>
                    <cc1:DataGrid ID="DataGrid1" runat="server" OnPageIndexChanged="DataGrid_PageIndexChanged" align="center">
                        <Columns>
                            <asp:TemplateColumn HeaderText="<input title='选中/取消' onclick='Check(this.form,this.checked)' type='checkbox' name='chkall' id='chkall' />">
                                <HeaderStyle Width="20px" />
                                <ItemTemplate>
						            <%# DataBinder.Eval(Container, "DataItem.ps_id").ToString() != "1" ? "<input id=\"uid\" onclick=\"checkedEnabledButton(this.form,'uid','StopTalk','DeleteUser')\" type=\"checkbox\" value=\"" + DataBinder.Eval(Container, "DataItem.ps_id").ToString() + "\"	name=\"uid\">" : ""%>
						        </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="">
                                <ItemTemplate>
							        <a href="#" onclick="javascript:window.location.href='global_edituser.aspx?uid=<%# DataBinder.Eval(Container, "DataItem.ps_id").ToString()%>&condition=<%=ViewState["condition"]==null?"":SAS.Common.Utils.HtmlEncode(ViewState["condition"].ToString().Replace("'","~^").Replace("%","~$"))%>';">编辑</a>
						        </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="">
                                <ItemTemplate>
					    	        <a href="#" onclick="javascript:window.location.href='global_givemedals.aspx?uid=<%# DataBinder.Eval(Container, "DataItem.ps_id").ToString()%>&condition=<%=ViewState["condition"]==null?"":SAS.Common.Utils.HtmlEncode(ViewState["condition"].ToString().Replace("'","~^").Replace("%","~$"))%>';">勋章</a>
						        </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="ps_id" HeaderText="用户ID" Visible="false"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="用户名">
                                <ItemTemplate>
							        <a href="../../userinfo-<%# DataBinder.Eval(Container, "DataItem.ps_id")%>.aspx" target="_blank"><%# DataBinder.Eval(Container, "DataItem.ps_name")%></a>
						        </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="头像">
                                <ItemTemplate>
							        <img src="../../tools/avatar.aspx?uid=<%# DataBinder.Eval(Container, "DataItem.ps_id")%>&size=small" onerror="this.onerror=null;this.src='../../templates/default/images/noavatar_small.gif';" />
						        </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="ug_name" HeaderText="所属组"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ps_nickName" HeaderText="昵称"></asp:BoundColumn>
                            <%--<asp:BoundColumn DataField="posts" HeaderText="发帖数"></asp:BoundColumn>--%>
                            <asp:BoundColumn DataField="ps_createDate" HeaderText="注册时间" DataFormatString="{0:yyyy-MM-dd}"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ps_credits" HeaderText="积分"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ps_email" HeaderText="邮箱"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="最后活动/上次访问时间">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.ps_lastactivity")%><br /><%# DataBinder.Eval(Container, "DataItem.ps_lastLogin")%>
						        </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </cc1:DataGrid>
                </td>
            </tr>
        </table>
        <p style="text-align:right;">
            <table style="float:right">
                <tr>
                    <td><cc1:Button ID="StopTalk" runat="server" Text=" 禁 言 " designtimedragdrop="247" Enabled="false"></cc1:Button>&nbsp;&nbsp;</td>
                    <td><cc1:Button ID="DeleteUser" runat="server" Text=" 删 除 " ButtonImgUrl="../images/del.gif" Enabled="false"></cc1:Button>&nbsp;&nbsp;</td>
                    <td>
                        <cc1:CheckBoxList ID="deltype" runat="server" RepeatColumns="1" RepeatLayout="flow">
                            <asp:ListItem Value="1">删除但保留该用户所发帖子</asp:ListItem>
                            <asp:ListItem Value="2">删除但保留该用户已发送的短消息</asp:ListItem>
                        </cc1:CheckBoxList>
                    </td>
                </tr>
            </table>              
        </p>
    </form>
    <%=footer%>
</body>
</html>
