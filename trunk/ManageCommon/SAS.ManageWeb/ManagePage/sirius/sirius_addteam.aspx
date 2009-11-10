<%@ Page Language="C#" AutoEventWireup="True" Inherits="SAS.Sirius.Admin.addteam"  CodeBehind="sirius_addteam.aspx.cs"%>

<%@ Register TagPrefix="uc1" TagName="TextareaResize" Src="../UserControls/TextareaResize.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="SAS.Control" Assembly="SAS.Control" %>
<%@ Register TagPrefix="cc3" Namespace="SAS.Control" Assembly="SAS.Control" %>
<%@ Register TagPrefix="uc2" TagName="PageInfo" Src="../UserControls/PageInfo.ascx" %>
<%@ Register TagPrefix="uc3" TagName="OnlineEditor" Src="../UserControls/onlineeditor.ascx" %>
<html>
<head>
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
    <meta name="keywords" content="天狼星,工作室" />
    <meta name="description" content="天狼星工作室综合管理后台" />
    <title>天狼星工作室综合管理后台-添加团队</title>
    <link href="../styles/datagrid.css" type="text/css" rel="stylesheet" />
    <link href="../styles/tab.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
        .td_alternating_item1
        {
            font-size: 12px;
        }
        .td_alternating_item2
        {
            font-size: 12px;
            background-color: #F5F7F8;
        }
    </style>

    <script type="text/javascript" src="../js/common.js"></script>

    <link href="../styles/dntmanager.css" type="text/css" rel="stylesheet" />
    <link href="../styles/modelpopup.css" type="text/css" rel="stylesheet" />

    <script type="text/javascript" src="../js/modalpopup.js"></script>
    <script type="text/javascript" src="../js/ajaxhelper.js"></script>
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
</head>
<body>
   <img id="img_hidden" style="position:absolute;top:-100000px;filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod='image');width:400;height:300"></img>
   <form id="Form1" method="post" runat="server">
	    <div class="Navbutton" style="width:98%;">
	    <table width="100%">
	    <tr>
        <td>	
	    <cc3:TabControl id="TabControl1" SelectionMode="Client" runat="server" TabScriptPath="../js/tabstrip.js" height="100%">
		    <cc3:TabPage Caption="基本信息" ID="tabPage51">
		    <uc2:PageInfo id="info1" runat="server" Icon="Information" Text="根据不同的团队组信息，设置不同的组属性，以突出各团队特色。"></uc2:PageInfo>
		    <table cellspacing="0" cellpadding="4" width="100%" align="center">
            <tr>
                <td class="panelbox" align="left">
                    <table width="100%">
                        <tr>
						    <td style="width: 150px">团队名称:</td>
						    <td>
							    <cc2:TextBox id="name" runat="server" CanBeNull="必填" IsReplaceInvertedComma="false" size="20"  MaxLength="49"></cc2:TextBox>
							</td>
                        </tr>
                        <tr>
						    <td style="width: 90px">是否启用:</td>
						    <td>
						        <cc2:RadioButtonList id="status" runat="server" RepeatColumns="2"  HintInfo="设置该团队是否启用" >
						        <asp:ListItem Value="1" Selected="True">启用</asp:ListItem>
						        <asp:ListItem Value="0" >不启用</asp:ListItem>
						        </cc2:RadioButtonList>
						    </td>
                        </tr>
					    <tr>
						    <td>成员列表:</td>
						    <td>
							    <uc1:TextareaResize id="moderators" runat="server" HintTitle="提示" HintInfo="当前团队成员列表，以&amp;quot;,&amp;quot;进行分割" 
							    controlname="TabControl1:tabPage51:moderators" Cols="40" Rows="5"></uc1:TextareaResize>
							    <br />以','进行分割,如:lisi,zhangsan
							</td>
					    </tr>
                        <tr>
		                    <td>团队图片地址:</td>
		                    <td>
                                <table id="tab1" cellspacing="0" cellpadding="0" width="450">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <b>图片描述:</b>
                                            </td>
                                        </tr>                                        
                                        <tr>
                                            <td>
                                                <cc2:TextBox ID="imgDesc" runat="server" Width="300" Height="50" TextMode="MultiLine"
                                                    IsReplaceInvertedComma="false"></cc2:TextBox>
                                            </td>
                                            <td>
                                                <span id="td1">
                                                    <img src="../images/invalid.gif" width="100" height="75" id="view1"></span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <b>选择图片:</b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <input type="file" id="photo1" onchange="PhotoView(1)" size="50" name="photo1">
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
							</td>
                        </tr>
                        <tr>
		                    <td>本团队SEO关键词:</td>
		                    <td>
			                    <cc2:textbox id="seokeywords" runat="server" HintTitle="提示" HintInfo="设置本团队的SEO关键词,用于搜索引擎优化,放在meta的keyword标签中,多个关键字间请用半角逗号&amp;quot,&amp;quot隔开" RequiredFieldType="暂无校验" width="450" height="100" 
			                    TextMode="MultiLine" IsReplaceInvertedComma="false"></cc2:textbox>
		                    </td>
                        </tr>
                        <tr>
		                    <td>本团队SEO描述:</td>
		                    <td>
			                    <cc2:textbox id="seodescription" runat="server" HintTitle="提示" HintInfo="设置本团队的SEO描述,用于搜索引擎优化,放在meta的description标签中,多个说明文字间请用半角逗号&amp;quot,&amp;quot隔开" RequiredFieldType="暂无校验" width="450" height="100" 
			                    TextMode="MultiLine" IsReplaceInvertedComma="false"></cc2:textbox>
		                    </td>
                        </tr>
                        <tr>
		                    <td >团队简述:</td>
		                    <td>
		                       <cc2:textbox id="teamBio" runat="server" HintTitle="提示" HintInfo="设置本团队基本概述" RequiredFieldType="暂无校验" width="450" height="100" 
			                    TextMode="MultiLine" IsReplaceInvertedComma="false"></cc2:textbox>
		                    </td>
	                    </tr>
                        <tr>
		                    <td>团队意义:</td>
		                    <td>
			                    <cc2:textbox id="content1" runat="server" HintTitle="提示" HintInfo="设置本团队团队意义" RequiredFieldType="暂无校验" width="450" height="100" 
			                    TextMode="MultiLine" IsReplaceInvertedComma="false"></cc2:textbox>
		                    </td>
                        </tr>
                        <tr>
		                    <td >团队工作方向和工作内容:</td>
		                    <td>
		                        <cc2:textbox id="content2" runat="server" HintTitle="提示" HintInfo="设置本团队团队意义" RequiredFieldType="暂无校验" width="450" height="100" 
			                    TextMode="MultiLine" IsReplaceInvertedComma="false"></cc2:textbox>
		                    </td>
	                    </tr>
	                    <tr>
		                    <td >人员职责和基本构成:</td>
		                    <td>
		                        <uc3:OnlineEditor ID="content3" runat="server" controlname="content3" postminchars="0" postmaxchars="2000"></uc3:OnlineEditor>
		                    </td>
	                    </tr>
                        
                    </table>
                </td>
            </tr>
            </table>
			</cc3:TabPage>
			<cc3:TabPage Caption="样式设定" ID="tabPage22">
                <table cellspacing="0" cellpadding="4" width="100%" align="center">
                    <tr>
                        <td class="panelbox" align="left">
                            <table width="100%">
                                <tr>
                                    <td style="width: 150px">
                                        链接地址:
                                    </td>
                                    <td>
                                        <cc2:TextBox ID="teamurl" runat="server" CanBeNull="必填" RequiredFieldType="网页地址" HintInfo="展示平台地址"
                                            IsReplaceInvertedComma="false" Size="20" MaxLength="49"></cc2:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        模板风格:
                                    </td>
                                    <td>
                                        <cc2:DropDownList ID="templateid" runat="server">
                                        </cc2:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
			</cc3:TabPage>
		</cc3:TabControl>		      
		    <div class="Navbutton">
		        <cc2:Button id="SubmitAdd" runat="server" Text=" 添 加 "></cc2:Button>&nbsp;&nbsp;
			    <button onclick="window.location='forum_forumstree.aspx';" id="Button3" class="ManagerButton" type="button"><img src="../images/arrow_undo.gif"/> 返 回 </button>
			</div>
			</td>
		    </tr>
		    </table>    		
	    </div>
	    <cc2:Hint id="Hint1" runat="server" HintImageUrl="../images"></cc2:Hint>
	    </form>
	<%=footer%>	
</body>
<script>
    function PhotoView(layer) {
        var file = $("photo" + layer).value;
        if (file != "") {
            var patn = /\.jpg$|\.jpeg$|\.gif$|\.png$/i;
            if (!patn.test(file)) {
                clearFileInput($("photo" + layer));
                alert("相册只允许jpg、jpeg、gif或png格式的图片!");
                return;
            }
            if (document.all) //IE执行
            {
                insertImage(layer);
            }
        }
        else {
            $("view" + layer).src = "../images/invalid.gif";

        }
    }
    
    function insertImage(id) {
        var localimgpreview = '';
        var path = $('photo' + id).value;
        var ext = path.lastIndexOf('.') == -1 ? '' : path.substr(path.lastIndexOf('.') + 1, path.length).toLowerCase();
        var re = new RegExp("(^|\\s|,)" + ext + "($|\\s|,)", "ig");
        var localfile = $('photo' + id).value.substr($('photo' + id).value.replace(/\\/g, '/').lastIndexOf('/') + 1);

        if (path == '') {
            return;
        }

        var err = false;
        $('img_hidden').alt = id;
        try {
            $('img_hidden').filters.item("DXImageTransform.Microsoft.AlphaImageLoader").sizingMethod = 'image';
        }
        catch (e){ err = true; }
        try {
            $('img_hidden').filters.item("DXImageTransform.Microsoft.AlphaImageLoader").src = $('photo' + id).value;
        }
        catch (e) {
            alert('无效的图片文件。');
            delAttach(id);

            err = true;

            return;
        }
        var wh = { 'w': $('img_hidden').offsetWidth, 'h': $('img_hidden').offsetHeight };
        if (wh['w'] > 100) {
            wh['h'] *= 100 / wh['w'];
            wh['w'] = 100;
        }
        if (wh['h'] > 100) {
            wh['w'] *= 100 / wh['h'];
            wh['h'] = 100;
        }
        $('img_hidden').style.width = wh['w'];
        $('img_hidden').style.height = wh['h'];
        try {
            $('img_hidden').filters.item("DXImageTransform.Microsoft.AlphaImageLoader").sizingMethod = 'scale';
        }
        catch (e) {
        }
        if (err == true) {
            $('img_hidden').src = $('photo' + id).value;
        }
        div = document.createElement('div');
        $('td' + id).removeChild($('td' + id).children(0));
        $('td' + id).appendChild(div);
        div.innerHTML = '<img style="filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod=\'scale\',src=\'' + $('photo' + id).value + '\');width:' + wh['w'] + ';height:' + wh['h'] + '" src=\'../images/none.gif\' border="0" id="view' + id + '" aid="view' + id + '" alt="" />';
    }
    
    function clearFileInput(file) {
        var form = document.createElement('form');
        document.body.appendChild(form);
        var pos = file.nextSibling;
        form.appendChild(file);
        form.reset();
        pos.parentNode.insertBefore(file, pos);
        document.body.removeChild(form);
    }
</script>
</html>