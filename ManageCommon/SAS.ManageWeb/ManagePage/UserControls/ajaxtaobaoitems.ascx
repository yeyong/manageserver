<%@ Control Language="C#" CodeBehind="ajaxtaobaoitems.ascx.cs" Inherits="SAS.ManageWeb.ManagePage.ajaxtaobaoitems" %>
<%@ Import Namespace="SAS.Config" %>
<br />
<div style="width:100%" align="center">
    <table class="table1" cellspacing="0" cellpadding="4" width="100%" align="center">
        <tr>
		<td colspan="2">
		<table class="ntcplist" >
            <tr class="head">
              <td>&nbsp;&nbsp;候选商品列表<%=keyword%></td>
            </tr>
            <tr>
            <td>
             <table class="datalist" cellspacing="0" rules="all" border="1" id="DataGrid1" style="border-collapse:collapse;">
                  <tr class="category">
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;">选择</td>
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;">宝贝名称</td>
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;">卖家昵称</td>
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;">商品价格</td>
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;">淘宝客佣金</td>
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;">佣金比率</td>
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;">30天累计成交量</td>
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;">佣金支出量</td>
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;">信用等级</td>
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;">商品所在地</td>
                  </tr>
                  <%foreach(SAS.Entity.Domain.TaobaokeItem tkiteminfo in taobaoitemlist){ %>
                  <tr class="mouseoutstyle" onmouseover="this.className='mouseoverstyle'" onmouseout="this.className='mouseoutstyle'" style="cursor:hand;">
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;"><input type="button" class="ManagerButton" value="选择" onclick="selectItem(this,'<%=tkiteminfo.NumIid%>');"/></td>
                    <td style="border-color:#EAE9E1;border-width:1px;border-style:solid;">
                        <span id="<%=tkiteminfo.Iid%>" onmouseover="showMenu(this.id,0,0,1,0);" style="font-weight:bold"><%=tkiteminfo.Title%><img src="../images/eye.gif" style="vertical-align:middle" /></span>
                        <div id="<%=tkiteminfo.Iid%>_menu" style="display:none">
					        <img src="<%=tkiteminfo.PicUrl%>_250x250.jpg" onerror="this.src='../../images/common/none.gif'" />
						</div>
                    </td>
                    <td style="border-color:#EAE9E1;border-width:1px;border-style:solid;"><%=tkiteminfo.Nick%></td>
                    <td style="border-color:#EAE9E1;border-width:1px;border-style:solid;"><%=tkiteminfo.Price%></td>
                    <td style="border-color:#EAE9E1;border-width:1px;border-style:solid;"><%=tkiteminfo.Commission%></td>
                    <td style="border-color:#EAE9E1;border-width:1px;border-style:solid;"><%=SAS.Common.TypeConverter.ObjectToFloat(tkiteminfo.CommissionRate)/100%>%</td>
                    <td style="border-color:#EAE9E1;border-width:1px;border-style:solid;"><%=tkiteminfo.Volume%></td>
                    <td style="border-color:#EAE9E1;border-width:1px;border-style:solid;"><%=tkiteminfo.CommissionVolume%></td>
                    <td style="border-color:#EAE9E1;border-width:1px;border-style:solid;"><%=tkiteminfo.SellerCreditScore%></td>
                    <td style="border-color:#EAE9E1;border-width:1px;border-style:solid;"><%=tkiteminfo.ItemLocation%></td>
                  </tr>
                  <%} %>
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