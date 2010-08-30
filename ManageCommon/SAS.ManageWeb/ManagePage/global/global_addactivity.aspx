<%@ Page Language="C#" CodeBehind="global_addactivity.aspx.cs" Inherits="SAS.ManageWeb.ManagePage.global_addactivity" %>
<%@ Register TagPrefix="sas" Namespace="SAS.Control" Assembly="SAS.Control" %>
<%@ Register TagPrefix="sas1" TagName="TextareaResize" Src="../UserControls/TextareaResize.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>新建活动</title>
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
	var n = 0;
	function displayHTML(obj) {
		window.open(obj, 'popup', 'toolbar = no, status = no, scrollbars=yes');
	}	
		
	function HighlightAll(obj) {
		obj.focus();
		obj.select();
		if (document.getElementById("templatenew_posttextarea")) {
			obj.createTextRange().execCommand("Copy");
			window.status = '将模板内容复制到剪贴板';
			setTimeout("window.status=''", 1800);
		}
	}

	function findInPage(obj, str) {
	    try {
	        var txt, i, found;
	        if (str == "") {
	            return false;
	        }
	        if (document.layers) {
	            if (!obj.find(str)) {
	                while (obj.find(str, false, true)) {
	                    n++;
	                }
	            } else {
	                n++;
	            }
	            if (n == 0) {
	                alert("未找到指定字串. ");
	            }
	        }
	        if (document.getElementById("templatenew_posttextarea")) {
	            txt = obj.createTextRange();
	            for (i = 0; i <= n && (found = txt.findText(str)) != false; i++) {
	                txt.moveStart('character', 1);
	                txt.moveEnd('textedit');
	            }
	            if (found) {
	                txt.moveStart('character', -1);
	                txt.findText(str);
	                txt.select();
	                txt.scrollIntoView();
	                n++;
	            } else {
	                if (n > 0) {
	                    n = 0;
	                    findInPage(str);
	                } else {
	                    alert("未找到指定字串. ");
	                }
	            }
	        }
	        return false;
	    }
	    catch (error) {
	        alert("已经到达文件尾！");
	    }
	}

	function validate(theForm) {
	    if ($("act_title").value == "") {
	        alert("活动标题不能为空!");
	        $("act_title").focus();
	        return false;
	    }

	    if ($("templatenew").value == "") {
	        alert("广告内容不能为空!");
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
        <legend style="background:url(../images/icons/icon36.jpg) no-repeat 6px 50%;">添加活动</legend>
        <table width="100%">
            <tr>
                <td class="item_title" colspan="2">活动类型</td>
            </tr>
            <tr>
                <td class="vtop rowform">
                    <sas:dropdownlist id="typeid" runat="server"></sas:dropdownlist>
                </td>
                <td class="vtop">
                </td>
            </tr>
            <tr>
                <td class="item_title" colspan="2">活动标题</td>
            </tr>
            <tr>
                <td class="vtop rowform">
                    <sas:TextBox id="act_title" runat="server" CanBeNull="必填" RequiredFieldType="暂无校验" MaxLength="100" HintInfo="设置活动标题，最多50个字" Size="50"></sas:TextBox>
                </td>
                <td class="vtop">
                </td>
            </tr>
            <tr>
                <td class="item_title" colspan="2">活动图片</td>
            </tr>
            <tr>
                <td class="vtop rowform">
                    <sas:TextBox id="act_rssimg" runat="server" CanBeNull="可为空" RequiredFieldType="暂无校验" MaxLength="100" HintInfo="设置活动标题，最多50个字" Size="60"></sas:TextBox>
                </td>
                <td class="vtop">
                </td>
            </tr>
            <tr>
                <td class="item_title" colspan="2">活动时间范围</td>
            </tr>
            <tr>
                <td class="vtop rowform">
                    起始日期:<sas:Calendar ID="postdatetimeStart" runat="server" ReadOnly="True" ScriptPath="../js/calendar.js" HintInfo="设置活动开始时间"></sas:Calendar>
                </td>
                <td class="vtop">
                    结束日期:<sas:Calendar ID="postdatetimeEnd" runat="server" ReadOnly="True" ScriptPath="../js/calendar.js" HintInfo="设置活动结束时间"></sas:Calendar>
                </td>
            </tr>
            <tr>
                <td class="item_title">活动样式</td>
                <td class="item_title">活动脚本</td>
            </tr>
            <tr>
                <td class="vtop rowform">
                    <sas1:textarearesize id="act_style" controlname="act_style" runat="server" rows="10" cols="50" HintInfo="设置页面style样式"></sas1:textarearesize>
                </td>
                <td class="vtop">
                    <sas1:textarearesize id="act_script" controlname="act_script" runat="server" rows="10" cols="50" HintInfo="设置页面特效脚本"></sas1:textarearesize>
                </td>
            </tr>
            <tr>
                <td class="item_title" colspan="2">活动内容</td>
            </tr>
            <tr>
                <td class="vtop rowform" colspan="2">
                    <sas:colorpicker id="ColorPicker1" runat="server" readonly="True"></sas:colorpicker>
                    <sas1:textarearesize id="templatenew" runat="server" controlname="templatenew" is_replace="false" rows="20" cols="120" HintInfo="设置页面内容"></sas1:textarearesize>
                </td>
            </tr>
            <tr>
                <td class="vtop rowform">
                    <input name="search" type="text" accesskey="t" size="20" onchange="n=0;" />&nbsp;
                    <button type="button" class="ManagerButton" accesskey="f" onclick="findInPage(this.form.templatenew_posttextarea, this.form.search.value)">
                        <img src="../images/search.gif" />搜索
                    </button>
                    &nbsp;&nbsp;
                    <button type="button" class="ManagerButton" accesskey="c" onclick="HighlightAll(this.form.templatenew_posttextarea,'')">
                        <img src="../images/topictype.gif" />复制
                    </button>
                </td>
                <td class="vtop">
                </td>
            </tr>
            <tr>
                <td class="item_title" colspan="2">SEO优化标题</td>
            </tr>
            <tr>
                <td class="vtop rowform">
                    <sas:TextBox id="seotitle" runat="server" RequiredFieldType="暂无校验" MaxLength="100" HintInfo="设置SEO标题，最多50个字"></sas:TextBox>
                </td>
                <td class="vtop">
                </td>
            </tr>
            <tr>
                <td class="item_title" colspan="2">SEO优化关键字</td>
            </tr>
            <tr>
                <td class="vtop rowform">
                    <sas:TextBox id="seokeyword" runat="server" Width="420px" Height="60px" TextMode="MultiLine" RequiredFieldType="暂无校验" MaxLength="60" HintInfo="设置SEO优化关键字，注意多个关键字用英文逗号隔开，最多60个字符"></sas:TextBox>
                </td>
                <td class="vtop">
                </td>
            </tr>
            <tr>
                <td class="item_title" colspan="2">SEO优化内容</td>
            </tr>
            <tr>
                <td class="vtop rowform">
                    <sas:TextBox id="seodesc" runat="server" Width="420px" Height="120px" TextMode="MultiLine" RequiredFieldType="暂无校验" MaxLength="120" HintInfo="设置SEO优化内容，最多120个字"></sas:TextBox>
                </td>
                <td class="vtop">
                </td>
            </tr>
            <tr>
                <td class="item_title" colspan="2">
                    是否启用
                </td>
            </tr>
            <tr>
                <td class="vtop rowform">
                    <sas:RadioButtonList ID="act_status" runat="server" RepeatColumns="2">
                        <asp:ListItem Value="1" Selected="true">启用</asp:ListItem>
                        <asp:ListItem Value="0">禁用</asp:ListItem>
                    </sas:RadioButtonList>
                </td>
                <td class="vtop">
                </td>
            </tr>
        </table>
        </fieldset>
        <sas:Hint id="Hint1" runat="server" HintImageUrl="../images"></sas:Hint>
	    <div class="Navbutton">
		    <sas:Button id="AddActInfo" runat="server" Text=" 提 交 " ValidateForm="true"></sas:Button>&nbsp;&nbsp;
            <button class="ManagerButton" type="button" onclick="javascript:window.location.href='global_searchactivity.aspx';">
                <img src="../images/arrow_undo.gif" />
                返 回
            </button>
        </div>
        </form>
    </div>
    <%=footer%>
</body>
</html>
