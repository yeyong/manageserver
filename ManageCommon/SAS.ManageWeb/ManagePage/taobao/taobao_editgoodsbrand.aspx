﻿<%@ Page Language="C#" CodeBehind="taobao_editgoodsbrand.aspx.cs" Inherits="SAS.ManageWeb.ManagePage.taobao_editgoodsbrand" %>
<%@ Register TagPrefix="sas" Namespace="SAS.Control" Assembly="SAS.Control" %>
<%@ Register TagPrefix="sas1" TagName="TextareaResize" Src="../UserControls/TextareaResize.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>新建品牌</title>
    <link href="../styles/calendar.css" type="text/css" rel="stylesheet" />
    <link href="../styles/dntmanager.css" type="text/css" rel="stylesheet" />
    <link href="../styles/colorpicker.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
        .img
        {
            border: 0px;
            align: bottom;
            position: relative;
            top: 0.5%;
        }
    </style>

    <script type="text/javascript">
	
	function validate(theForm) {
	    if ($("brandname").value == "") {
	        alert("品牌名称不能为空!");
	        $("brandname").focus();
	        return false;
	    }
	    return true;
	}
    </script>

    <script type="text/javascript" src="../js/common.js"></script>

    <meta http-equiv="X-UA-Compatible" content="IE=7" />
</head>
<body>
    <div class="ManagerForm">
        <form id="Form1" method="post" runat="server">
        <fieldset>
        <legend style="background:url(../images/icons/icon36.jpg) no-repeat 6px 50%;">创建品牌</legend>
        <table width="100%">
            <tr>
                <td class="item_title">所属类别</td>
            </tr>
            <tr>
                <td class="vtop rowform">
                    <sas:DropDownTreeList id="brandclass" runat="server"></sas:DropDownTreeList>
                </td>
            </tr>
            <tr>
                <td class="item_title">品牌名称</td>
            </tr>
            <tr>
                <td class="vtop rowform">
                    <sas:TextBox id="brandname" runat="server" CanBeNull="必填" RequiredFieldType="暂无校验" MaxLength="20" HintInfo="设置品牌名称，最多20个字"></sas:TextBox>
                </td>
            </tr>
            <tr>
                <td class="item_title">品牌别名</td>
            </tr>
            <tr>
                <td class="vtop rowform">
                    <sas:TextBox id="brandspell" runat="server" CanBeNull="必填" RequiredFieldType="暂无校验" MaxLength="20" HintInfo="品牌英文名、缩略名或其他前台显示名称,最多20个字"></sas:TextBox>
                </td>
            </tr>
            <tr>
                <td class="item_title">品牌Logo</td>
            </tr>
            <tr>
                <td class="vtop rowform">
                    <sas:TextBox id="brandlogo" runat="server" CanBeNull="必填" RequiredFieldType="暂无校验" HintInfo="品牌图片地址"></sas:TextBox>
                </td>
            </tr>
            <tr>
                <td class="item_title">品牌图片</td>
            </tr>
            <tr>
                <td class="vtop rowform">
                    <sas:TextBox id="brandimg" runat="server" RequiredFieldType="暂无校验" HintInfo="品牌详细页大图片地址"></sas:TextBox>
                </td>
            </tr>
            <tr>
                <td class="item_title">品牌官方网址</td>
            </tr>
            <tr>
                <td class="vtop rowform">
                    <sas:TextBox id="brandwebsite" runat="server" RequiredFieldType="网页地址" MaxLength="20" HintInfo="品牌官方网址"></sas:TextBox>
                </td>
            </tr>
            <tr>
                <td class="item_title">品牌官方公司</td>
            </tr>
            <tr>
                <td class="vtop rowform">
                    <sas:TextBox id="brandcompany" runat="server" RequiredFieldType="暂无校验" MaxLength="20" HintInfo="品牌官方公司"></sas:TextBox>
                </td>
            </tr>
            <tr>
                <td class="item_title">SEO优化关键字</td>
            </tr>
            <tr>
                <td class="vtop rowform">
                    <sas:TextBox id="seokeyword" runat="server" Width="420px" Height="60px" TextMode="MultiLine" RequiredFieldType="暂无校验" MaxLength="60" HintInfo="设置SEO优化关键字，注意多个关键字用英文逗号隔开，最多60个字符"></sas:TextBox>
                </td>
            </tr>
            <tr>
                <td class="item_title">品牌简述</td>
            </tr>
            <tr>
                <td class="vtop rowform">
                    <sas:TextBox id="brandshortdesc" runat="server" Width="420px" Height="120px" TextMode="MultiLine" RequiredFieldType="暂无校验" MaxLength="120" HintInfo="设置品牌简述内容，最多120个字"></sas:TextBox>
                </td>
            </tr>
             <tr>
                <td class="item_title">品牌详述</td>
            </tr>
            <tr>
                <td class="vtop rowform">
                    <sas1:TextareaResize id="branddesc" runat="server" HintTitle="提示" HintInfo="品牌详细描述，支持简单HTML代码" controlname="branddesc" Cols="80" Rows="10"></sas1:TextareaResize>
                </td>
            </tr>
            <tr>
                <td class="item_title">品牌顺序</td>
            </tr>
            <tr>
                <td class="vtop rowform">
                    <sas:TextBox id="brandorder" runat="server" CanBeNull="必填" RequiredFieldType="数据校验" MaxLength="10" HintInfo="品牌顺序中 0 为推荐品牌，会优先显示在推荐页面上"></sas:TextBox>
                </td>
            </tr>
            <tr>
                <td class="item_title">
                    是否启用
                </td>
            </tr>
            <tr>
                <td class="vtop rowform">
                    <sas:RadioButtonList ID="brandstatus" runat="server" RepeatColumns="2">
                        <asp:ListItem Value="1" Selected="true">启用</asp:ListItem>
                        <asp:ListItem Value="0">禁用</asp:ListItem>
                    </sas:RadioButtonList>
                </td>
            </tr>
        </table>
        </fieldset>
        <sas:Hint id="Hint1" runat="server" HintImageUrl="../images"></sas:Hint>
	    <div class="Navbutton">
		    <sas:Button id="UpdateBrandInfo" runat="server" Text=" 提 交 " ValidateForm="true"></sas:Button>&nbsp;&nbsp;
            <button class="ManagerButton" type="button" onclick="javascript:window.location.href='taobao_goodsbrandgrid.aspx';">
                <img src="../images/arrow_undo.gif" />
                返 回
            </button>
        </div>
        </form>
    </div>
    <%=footer%>
</body>
</html>