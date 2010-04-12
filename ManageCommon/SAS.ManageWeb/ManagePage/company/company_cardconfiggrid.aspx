<%@ Page Language="C#" CodeBehind="company_cardconfiggrid.aspx.cs" Inherits="SAS.ManageWeb.ManagePage.company_cardconfiggrid" %>
<%@ Register Namespace="SAS.Control" Assembly="SAS.Control" TagPrefix="yy"%>
<%@ Register Src="../UserControls/PageInfo.ascx" TagPrefix="yy1" TagName="PageInfo" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>导航菜单管理</title>
    <link href="../styles/datagrid.css" type="text/css" rel="stylesheet" />
    <link href="../styles/dntmanager.css" type="text/css" rel="stylesheet" />
    <link href="../styles/modelpopup.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" src="../js/modalpopup.js"></script>
    <script type="text/javascript" src="../js/common.js"></script>
    <script type="text/javascript">
        function newMenu() {
            $("opt").innerHTML = "新建名片配置";
            $("id").value = "0";
            $("mode").value = "new";
            $("ccname").value = "";
            $("tid").options[0].selected = true;
            $("hasflash").checked = false;
            $("hasimage").checked = false;
            $("hasjs").checked = false;
            $("hassilverlight").checked = false;
            $("showparams").value = "";
            BOX_show('neworeditmainmenu');
        }
        function editMenu(menuid) {
            $("opt").innerHTML = "编辑名片配置";
            for (var i = 0; i < nav.length; i++) {
                if (nav[i]["id"] == menuid) {
                    $("id").value = nav[i]["id"];
                    $("mode").value = "edit";
                    $("ccname").value = nav[i]["ccname"];
                    $("tid").value = nav[i]["tid"];
                    if (nav[i]["hasflash"] == "1") $("hasflash").checked = true;
                    if (nav[i]["hasimage"] == "1") $("hasimage").checked = true;
                    if (nav[i]["hasjs"] == "1") $("hasjs").checked = true;
                    if (nav[i]["hassilverlight"] == "1") $("hassilverlight").checked = true;
                    $("showparams").value = nav[i]["showparams"];
                    BOX_show('neworeditmainmenu');
                    return;
                }
            }
            alert("名片配置不存在！");
        }
        function chkSubmit() {
            if ($("ccname").value == "") {
                alert("名片配置名称不能为空！");
                $("ccname").focus();
                return false;
            }
            $("form1").submit();
            return true;
        }
    </script>
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
</head>
<body>
    <form id="form1" runat="server">
	<yy1:PageInfo ID="info1" runat="server" Icon="information" Text="<li>主菜单项必须在其下没有子菜单时才可删除!</li>" />
	<yy:datagrid id="DataGrid1" runat="server" IsFixConlumnControls="true" TableHeaderName="配置文件管理">
	   <Columns>
		<asp:BoundColumn DataField="ccname" HeaderText="配置名"><ItemStyle HorizontalAlign="Center" /></asp:BoundColumn>
		<asp:BoundColumn DataField="name" HeaderText="模板名称" ReadOnly="true"><ItemStyle HorizontalAlign="Center" /></asp:BoundColumn>
		<asp:TemplateColumn HeaderText="是否支持flash">
			<ItemTemplate>
				<%# DataBinder.Eval(Container, "DataItem.hasflash").ToString() == "0" ? "不支持" : "支持"%>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="是否支持图片">
			<ItemTemplate>
				<%# DataBinder.Eval(Container, "DataItem.hasimage").ToString() == "0" ? "不支持" : "支持"%>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="是否支持js">
			<ItemTemplate>
				<%# DataBinder.Eval(Container, "DataItem.hasjs").ToString() == "0" ? "不支持" : "支持"%>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="是否支持silverlight">
			<ItemTemplate>
				<%# DataBinder.Eval(Container, "DataItem.hassilverlight").ToString() == "0" ? "不支持" : "支持"%>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:BoundColumn DataField="createdate" HeaderText="创建时间" ReadOnly="true"><ItemStyle HorizontalAlign="Center" /></asp:BoundColumn>
		<asp:BoundColumn DataField="vailddate" HeaderText="有效时间"><ItemStyle HorizontalAlign="Center" /></asp:BoundColumn>
		<asp:TemplateColumn HeaderText="操作">
			<ItemTemplate>
			    <input id="keyid" type="hidden" value="<%# DataBinder.Eval(Container, "DataItem.id").ToString() %>" name="keyid"/>
				<a href="javascript:;" onclick="editMenu('<%# DataBinder.Eval(Container, "DataItem.id").ToString() %>');">编辑</a>&nbsp;
				<a href="?mode=del&id=<%# DataBinder.Eval(Container, "DataItem.id").ToString() %>" onclick="return confirm('确认要将该菜单项删除吗?');">删除</a>
			</ItemTemplate>
		</asp:TemplateColumn>
	  </Columns>
	</yy:datagrid>
	<p style="text-align:right;">
	    <yy:Button id="saveNav" runat="server" Text="保存" OnClick="saveNav_Click"></yy:Button>
		<button type="button" class="ManagerButton" id="Button2" onclick="newMenu();"><img src="../images/add.gif"/> 新 建 </button>
	</p>
	<div id="BOX_overlay" style="background: #000; position: absolute; z-index:100; filter:alpha(opacity=50);-moz-opacity: 0.6;opacity: 0.6;"></div>
	<div id="neworeditmainmenu" style="display: none; background :#fff; padding:10px; border:1px solid #999; width:400px;">
		<div class="ManagerForm">
			<fieldset>
			<legend id="opt" style="background:url(../images/icons/icon53.jpg) no-repeat 6px 50%;">新建名片配置</legend>
			<table cellspacing="0" cellPadding="4" class="tabledatagrid" width="80%">
				<tr>
					<td width="30%" height="30px">
						配置名称:
						<input type="hidden" id="id" name="id" value="0" />
						<input type="hidden" id="mode" name="mode" value="" />
					</td>
					<td width="70%"><input id="ccname"  name="ccname" type="text" maxlength="50" size="30"class="FormBase" onfocus="this.className='FormFocus';" onblur="this.className='FormBase';" /></td>
				</tr>
				<tr>
					<td height="30px">模板选择:</td>
					<td>
						<select name="tid" id="tid">
						    <%
                                foreach (System.Data.DataRow dr in cardtemplist.Rows)
                                {
                            %>
                            <option value="<%=dr["id"]%>"><%=dr["name"]%></option>
                            <%
                                }
                            %>
						</select>
					</td>
				</tr>
				<tr>
					<td height="30px">支持模式:</td>
                    <td>
                        <input type="checkbox" name="hasflash" id="hasflash" value="1"/>支持flash
                        <input type="checkbox" name="hasimage" id="hasimage" value="1"/>支持图片<br />
                        <input type="checkbox" name="hasjs" id="hasjs" value="1"/>支持js
                        <input type="checkbox" name="hassilverlight" id="hassilverlight" value="1"/>支持silverlight
                    </td>
				</tr>
				<tr>
				    <td height="30px">参数设置:</td>
				    <td>
				        <input type="text" name="showparams" id="showparams" />
				    </td>
				</tr>
				<tr>
				    <td height="30px">有效时间:</td>
				    <td>
				        <input type="text" name="vailddate" id="vailddate" value="2010-12-30"/>
				    </td>
				</tr>
				<tr>
					<td colspan="2" height="30px" align="center">
						<button type="button" class="ManagerButton" id="AddNewRec" onclick="chkSubmit();"><img src="../images/add.gif"/> 提 交 </button>&nbsp;&nbsp;
						<button type="button" class="ManagerButton" id="Button1" onclick="BOX_remove('neworeditmainmenu');"><img src="../images/state1.gif"/> 取 消 </button>
					</td>
				</tr>
			</table>
			</fieldset>
		</div>
	</div>
</form>
<div id="setting" />
<%=footer%>
</body>
</html>

