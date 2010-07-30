<%@ Control Language="C#" CodeBehind="ajaxbrandlist.ascx.cs" Inherits="SAS.ManageWeb.ManagePage.ajaxbrandlist" %>
<%@ Import Namespace="SAS.Config" %>
<br />
<div style="width:100%" align="center">
    <table class="table1" cellspacing="0" cellpadding="4" width="100%" align="center">
        <tr>
		<td colspan="2">
		<table class="ntcplist" >
            <tr class="head">
              <td>&nbsp;&nbsp;候选店铺列表</td>
            </tr>
            <tr>
            <td>
             <table class="datalist" cellspacing="0" rules="all" border="1" id="DataGrid1" style="border-collapse:collapse;">
                  <tr class="category">
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;">操作</td>
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;">品牌logo</td>
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;">品牌名称</td>
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;">品牌别名</td>
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;">关联类别</td>
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;">品牌排序</td>
                  </tr>               
                  <%foreach (System.Data.DataRow dr in brandlist.Select("status=1"))
                    { 
                        %>
                  <tr class="mouseoutstyle" onmouseover="this.className='mouseoverstyle'" onmouseout="this.className='mouseoutstyle'" style="cursor:hand;">
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;"><input type="button" class="ManagerButton" value="操作" onclick="selectItem(this,'<%=dr["id"]%>');"/></td>
                    <td style="border-color:#EAE9E1;border-width:1px;border-style:solid;"><img width="82px" height="82px" src="<%=dr["logo"]%>"/></td>
                    <td style="border-color:#EAE9E1;border-width:1px;border-style:solid;"><%=dr["bname"]%></td>
                    <td style="border-color:#EAE9E1;border-width:1px;border-style:solid;"><%=dr["spell"]%></td>
                    <td style="border-color:#EAE9E1;border-width:1px;border-style:solid;"><%=dr["cname"]%></td>
                    <td style="border-color:#EAE9E1;border-width:1px;border-style:solid;"><%=dr["order"]%></td>
                  </tr>
                  <%
                    } 
                  %>
                  <tr>
	                <td align="left" valign="bottom" colspan="7" style="border-width:0px;"><%=pagelink %></td>
                  </tr>
              </table>
              </td>
            </tr>
          </table>
          </td>
          </tr>
          </table>
</div>