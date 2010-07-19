<%@ Control Language="C#" CodeBehind="ajaxtaobaoshops.ascx.cs" Inherits="SAS.ManageWeb.ManagePage.ajaxtaobaoshops" %>
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
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;">选择</td>
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;">店铺图标</td>
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;">店铺名称</td>
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;">店铺卖家</td>
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;">佣金比率</td>
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;">商家类型</td>
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;">是否参加消保</td>
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;">信用等级</td>
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;">信用总分</td>
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;">评价总条数</td>
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;">好评总条数</td>
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;">商品描述评分</td>
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;">服务态度评分</td>
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;">发货速度评分</td>
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;">详细地址</td>
                  </tr>               
                  <%foreach (SAS.Entity.ShopDetailInfo tbshopinfo in shoplist)
                    { %>
                  <tr class="mouseoutstyle" onmouseover="this.className='mouseoverstyle'" onmouseout="this.className='mouseoutstyle'" style="cursor:hand;">
                    <td nowrap="nowrap" style="border-color:#EAE9E1;border-width:1px;border-style:solid;"><%if(dmode==DisplayMode.SearchMode){%><input type="button" class="ManagerButton" value="选择" onclick="selectItem(this,'<%=tbshopinfo.sid%>');"/><%}else if(dmode==DisplayMode.ManageMode){%><input type="checkbox" value="<%=tbshopinfo.sid%>" name="shopsids"/><%}%></td>
                    <td style="border-color:#EAE9E1;border-width:1px;border-style:solid;"><img width="82px" height="82px" src="http://logo.taobao.com/shop-logo<%=tbshopinfo.pic_path%>"/></td>
                    <td style="border-color:#EAE9E1;border-width:1px;border-style:solid;"><%=tbshopinfo.title%></td>
                    <td style="border-color:#EAE9E1;border-width:1px;border-style:solid;"><%=tbshopinfo.nick%></td>
                    <td style="border-color:#EAE9E1;border-width:1px;border-style:solid;"><%=SAS.Common.TypeConverter.ObjectToFloat(tbshopinfo.commission_rate)%>%</td>
                    <td style="border-color:#EAE9E1;border-width:1px;border-style:solid;"><%=tbshopinfo.shop_type%></td>                    
                    <td style="border-color:#EAE9E1;border-width:1px;border-style:solid;"><img src='<%=tbshopinfo.consumer_protection?"../images/state2.gif":"../images/state2.gif"%>'/></td>
                    <td style="border-color:#EAE9E1;border-width:1px;border-style:solid;"><%=tbshopinfo.shop_level%></td> 
                    <td style="border-color:#EAE9E1;border-width:1px;border-style:solid;"><%=tbshopinfo.shop_score%></td> 
                    <td style="border-color:#EAE9E1;border-width:1px;border-style:solid;"><%=tbshopinfo.total_num%></td> 
                    <td style="border-color:#EAE9E1;border-width:1px;border-style:solid;"><%=tbshopinfo.good_num%></td> 
                    <td style="border-color:#EAE9E1;border-width:1px;border-style:solid;"><%=tbshopinfo.item_score%></td> 
                    <td style="border-color:#EAE9E1;border-width:1px;border-style:solid;"><%=tbshopinfo.service_score%></td> 
                    <td style="border-color:#EAE9E1;border-width:1px;border-style:solid;"><%=tbshopinfo.delivery_score%></td> 
                    <td style="border-color:#EAE9E1;border-width:1px;border-style:solid;"><%=tbshopinfo.shop_country+tbshopinfo.shop_province+tbshopinfo.shop_city%></td> 
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