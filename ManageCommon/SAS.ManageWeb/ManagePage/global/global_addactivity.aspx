<%@ Page Language="C#" CodeBehind="global_addactivity.aspx.cs" Inherits="SAS.ManageWeb.ManagePage.global_addactivity" %>
<%@ Register TagPrefix="sas" Namespace="SAS.Control" Assembly="SAS.Control" %>
<%@ Register TagPrefix="sas1" TagName="TextareaResize" Src="../UserControls/TextareaResize.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title></title>
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
    </script>

    <script type="text/javascript" src="../js/common.js"></script>

    <meta http-equiv="X-UA-Compatible" content="IE=7" />
</head>
<body>
    <div class="ManagerForm">
        <form id="Form1" method="post" runat="server">
        <table width="100%">
            <tr>
                <td class="item_title" colspan="2">
                    活动标题
                </td>
            </tr>
            <tr>
                <td class="vtop rowform">
                    <sas:TextBox id="act_title" runat="server" CanBeNull="必填" RequiredFieldType="暂无校验" MaxLength="20"></sas:TextBox>
                </td>
                <td class="vtop">
                </td>
            </tr>
            <tr>
                <td class="item_title">
                    活动样式
                </td>
                <td class="item_title">
                    活动脚本
                </td>
            </tr>
            <tr>
                <td class="vtop rowform">
                    <sas1:textarearesize id="act_style" runat="server" rows="10" cols="40"></sas1:textarearesize>
                </td>
                <td class="vtop">
                    <sas1:textarearesize id="act_script" runat="server" rows="10" cols="40"></sas1:textarearesize>
                </td>
            </tr>
            <tr>
                <td class="item_title" colspan="2">
                    活动内容
                </td>
            </tr>
            <tr>
                <td class="vtop rowform" colspan="2">
                    <sas:colorpicker id="ColorPicker1" runat="server" readonly="True"></sas:colorpicker>
                    <sas1:textarearesize id="templatenew" runat="server" controlname="templatenew" is_replace="false" rows="20" cols="120"></sas1:textarearesize>
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
        </table>
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
</body>
</html>
