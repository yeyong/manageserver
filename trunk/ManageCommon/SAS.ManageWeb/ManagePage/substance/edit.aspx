<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Page Language="C#" %>
<html dir="ltr" xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
<meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
<meta http-equiv="X-UA-Compatible" content="IE=7" />
<meta name="description" content="网站简介" />
<meta name="keywords" content="" />
<title>列表</title>
<link href="../style/comm.css" type="text/css" rel="stylesheet" />
<link href="../style/admin.css" type="text/css" rel="stylesheet" />
<script src="../scripts/jquery.js" type="text/javascript"></script>
</head>

<body>

<form id="form1" runat="server">

<div class="xinrt">
	<div class="percot">
		<ul class="pert">
			<li class="pertli1">基本信息</li>
			<li class="pertli2">基本信息</li>
		</ul>
		<div class="pert2">
			<table width="100%" border="0" cellspacing="0" cellpadding="0" class="edtb">
				<tr>
					<td style="width:15%" class="edtd1">
					<span class="edtu">
					<img alt="" title="可以对当前分类进行修改" src="../images/admin-gif/access-71.gif" /></span>
					当前分类：
					</td>
					<td style="width:35%" class="edtd2">
					电脑网络 &gt; 显示器 &gt; 液晶　
					<input type="button" name="button2" class="an2" value="编辑" />
					</td>
					<td style="width:15%" class="edtd1">&nbsp;</td>
					<td style="width:35%" class="edtd2">&nbsp;</td>
				</tr>
				<tr>
					<td style="width:15%" class="edtd1">商品名称：</td>
					<td style="width:35%" class="edtd2">
					<input type="text" name="textfield" class="input2" style="width:180px;" />
					<span class="zi1">*</span>
					</td>
					<td style="width:15%" class="edtd1">商品简称：</td>
					<td style="width:35%" class="edtd2">
					<input type="text" name="textfield" class="input2" style="width:180px;" />
					<span class="zi1">*</span>
					</td>
				</tr>
				<tr>
					<td style="width:15%" class="edtd1">
					<span class="edtu"><img alt="" title="" src="../images/admin-gif/access-71.gif" /></span>
					商品编号：
					</td>
					<td style="width:35%" class="edtd2">
					<input type="text" name="textfield" value="000000" class="input2" style="width:180px;" />
					<input type="button" name="button2" class="an2" value="编辑" />
					</td>
					<td style="width:15%" class="edtd1">&nbsp;</td>
					<td style="width:35%" class="edtd2">&nbsp;</td>
				</tr>
				<tr>
					<td style="width:15%" class="edtd1">
					<span class="edtu"><img alt="" title="" src="../images/admin-gif/access-71.gif" /></span>
					市场价格：
					</td>
					<td style="width:35%" class="edtd2">
					<input type="text" name="textfield" class="input2" style="width:130px;" />
					<span class="zi1">*</span>
					</td>
					<td style="width:15%" class="edtd1">
					<span class="edtu"><img alt="" title="" src="../images/admin-gif/access-71.gif" /></span>
					商城价格：
					</td>
					<td style="width:35%" class="edtd2">
					<input type="text" name="textfield" class="input2" style="width:130px;" />
					<span class="zi1">*</span>
					</td>
				</tr>
				<tr>
					<td style="width:15%" class="edtd1">
					<span class="edtu"><img alt="" title="" src="../images/admin-gif/access-71.gif" /></span>
					商品品牌：
					</td>
					<td style="width:35%" class="edtd2">
					<select name="select">
						<option>请选择</option>
						<option>品牌名</option>
					</select>
					</td>
					<td style="width:15%" class="edtd1">&nbsp;</td>
					<td style="width:35%" class="edtd2">&nbsp;</td>
				</tr>
				<tr>
					<td style="width:15%" class="edtd1">
					<span class="edtu"><img alt="" title="" src="../images/admin-gif/access-71.gif" /></span>
					库存警告：
					</td>
					<td style="width:35%" class="edtd2">
					<input type="radio" name="RadioGroup1" value="是" /> 是
					<input type="radio" name="RadioGroup1" value="否" /> 否
					</td>
					<td style="width:15%" class="edtd1">&nbsp;</td>
					<td style="width:35%" class="edtd2">&nbsp;</td>
				</tr>
				<tr>
					<td style="width:15%" class="edtd1">
					库存：
					</td>
					<td style="width:35%" class="edtd2">
					<input type="text" name="textfield" class="input2" style="width:180px;" />
					<span class="zi1">*</span>
					</td>
					<td style="width:15%" class="edtd1">&nbsp;</td>
					<td style="width:35%" class="edtd2">&nbsp;</td>
				</tr>
				<tr>
					<td style="width:15%" class="edtd1">
					<span class="edtu"><img alt="" title="" src="../images/admin-gif/access-71.gif" /></span>
					商品重量：
					</td>
					<td style="width:35%" class="edtd2">
					<input type="text" name="textfield" class="input2" style="width:180px;" />
					<span class="zi1">*</span>
					</td>
					<td style="width:15%" class="edtd1">&nbsp;</td>
					<td style="width:35%" class="edtd2">&nbsp;</td>
				</tr>
				<tr>
					<td style="width:15%" class="edtd1">
					商品属性：
					</td>
					<td style="width:35%" class="edtd2">
					<input name="" type="checkbox" value="" /> 上架　
					<input name="" type="checkbox" value="" /> 特价　
					<input name="" type="checkbox" value="" /> 新品　
					<input name="" type="checkbox" value="" /> 推荐　
					<input name="" type="checkbox" value="" /> 热点　
					</td>
					<td style="width:15%" class="edtd1">&nbsp;</td>
					<td style="width:35%" class="edtd2">&nbsp;</td>
				</tr>
				<tr>
					<td style="width:15%" class="edtd1">
					商品标签：
					</td>
					<td style="width:35%" class="edtd2">
					<input type="text" name="textfield" class="input2" style="width:180px;" />
					</td>
					<td style="width:15%" class="edtd1">&nbsp;</td>
					<td style="width:35%" class="edtd2">&nbsp;</td>
				</tr>
				<tr>
					<td style="width:15%" class="edtd1">
					<span class="edtu"><img alt="" title="" src="../images/admin-gif/access-71.gif" /></span>
					虚拟商品：
					</td>
					<td style="width:35%" class="edtd2">
					<input type="radio" name="RadioGroup1" value="是" /> 是
					<input type="radio" name="RadioGroup1" value="否" /> 否
					</td>
					<td style="width:15%" class="edtd1">&nbsp;</td>
					<td style="width:35%" class="edtd2">&nbsp;</td>
				</tr>
				<tr>
					<td style="width:15%" class="edtd1">
					<span class="edtu"><img alt="" title="" src="../images/admin-gif/access-71.gif" /></span>
					是否换购商品：
					</td>
					<td style="width:35%" class="edtd2">
					<input type="radio" name="RadioGroup1" value="是" /> 是
					<input type="radio" name="RadioGroup1" value="否" /> 否
					</td>
					<td style="width:15%" class="edtd1">&nbsp;</td>
					<td style="width:35%" class="edtd2">&nbsp;</td>
				</tr>
				<tr>
					<td style="width:15%" class="edtd1">
					是否积分兑换：
					</td>
					<td style="width:35%" class="edtd2">
					<input type="radio" name="RadioGroup1" value="是" /> 是
					<input type="radio" name="RadioGroup1" value="否" /> 否
					</td>
					<td style="width:15%" class="edtd1">&nbsp;</td>
					<td style="width:35%" class="edtd2">&nbsp;</td>
				</tr>
				<tr>
					<td style="width:15%" class="edtd1">
					商品排序：
					</td>
					<td style="width:35%" class="edtd2">
					<input type="text" name="textfield" class="input2" style="width:180px;" />
					</td>
					<td style="width:15%" class="edtd1">&nbsp;</td>
					<td style="width:35%" class="edtd2">&nbsp;</td>
				</tr>
				<tr>
					<td style="width:15%" class="edtd1">
					商品浏览次数：
					</td>
					<td style="width:35%" class="edtd2">
					<input type="text" name="textfield" class="input2" style="width:180px;" />
					</td>
					<td style="width:15%" class="edtd1">&nbsp;</td>
					<td style="width:35%" class="edtd2">&nbsp;</td>
				</tr>
				<tr>
					<td style="width:15%" class="edtd1">
					<span class="edtu"><img alt="" title="" src="../images/admin-gif/access-71.gif" /></span>
					赠送积分：
					</td>
					<td style="width:35%" class="edtd2">
					<input type="text" name="textfield" class="input2" style="width:180px;" />
					</td>
					<td style="width:15%" class="edtd1">&nbsp;</td>
					<td style="width:35%" class="edtd2">&nbsp;</td>
				</tr>
				<tr>
					<td style="width:15%" class="edtd1">
					<span class="edtu"><img alt="" title="" src="../images/admin-gif/access-71.gif" /></span>
					起购数量：
					</td>
					<td style="width:35%" class="edtd2">
					<input type="text" name="textfield" class="input2" style="width:180px;" />
					</td>
					<td style="width:15%" class="edtd1">&nbsp;</td>
					<td style="width:35%" class="edtd2">&nbsp;</td>
				</tr>
				<tr>
					<td style="width:15%" class="edtd1">
					是否为商品配件：
					</td>
					<td style="width:35%" class="edtd2">
					<input type="radio" name="RadioGroup1" value="是" /> 是
					<input type="radio" name="RadioGroup1" value="否" /> 否
					</td>
					<td style="width:15%" class="edtd1">&nbsp;</td>
					<td style="width:35%" class="edtd2">&nbsp;</td>
				</tr>
			</table>
			<div class="edbt">
				<input type="button" name="button2" class="an4" value="保存并返回列表" />
				<input type="button" name="button2" class="an4" value="保存并添加相似商品" />
			</div>
			
		</div>
	</div>

</div>

</form>

</body>

</html>
