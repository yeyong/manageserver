<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="SAS.ManageWeb.login" %>
<%@ Import namespace="System.Data" %>
<%@ Import namespace="SAS.Common" %>
<%@ Import namespace="SAS.Logic" %>
<%@ Import namespace="SAS.Entity" %>

<script runat="server">
override protected void OnInit(EventArgs e)
{

	/* 
		This page was created by Studio after 80s Template Engine at 2010-6-22 22:26:40.
		本页面代码由Studio after 80s模板引擎生成于 2010-6-22 22:26:40. 
	*/

	base.OnInit(e);

	templateBuilder.Capacity = 220000;



	if (infloat!=1)
	{

	templateBuilder.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n");
	templateBuilder.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\" >\r\n");
	templateBuilder.Append("<head>\r\n");
	templateBuilder.Append("    <meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\r\n");

	if (pagetitle=="首页")
	{

	templateBuilder.Append("<title>");
	templateBuilder.Append(config.Sitetitle.ToString().Trim());
	templateBuilder.Append(" ");
	templateBuilder.Append(config.Seotitle.ToString().Trim());
	templateBuilder.Append(" - Powered by Studio after 80s</title>\r\n");

	}
	else
	{

	templateBuilder.Append("<title>");
	templateBuilder.Append(pagetitle.ToString());
	templateBuilder.Append(" - ");
	templateBuilder.Append(config.Sitetitle.ToString().Trim());
	templateBuilder.Append(" ");
	templateBuilder.Append(config.Seotitle.ToString().Trim());
	templateBuilder.Append(" - Powered by Studio after 80s</title>\r\n");

	}	//end if
	templateBuilder.Append(meta.ToString());
	templateBuilder.Append("\r\n");
	templateBuilder.Append("<meta name=\"generator\" content=\"SAS 1.0.0\" />\r\n");
	templateBuilder.Append("<meta name=\"author\" content=\"Studio after 80s Team and Studio sirius UI Team\" />\r\n");
	templateBuilder.Append("<meta name=\"copyright\" content=\"2009-2009 Studio after 80s.\" />\r\n");
	templateBuilder.Append("<meta http-equiv=\"x-ua-compatible\" content=\"ie=7\" />\r\n");
	templateBuilder.Append("<link rel=\"icon\" href=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("favicon.ico\" type=\"image/x-icon\" />\r\n");
	templateBuilder.Append("<link rel=\"shortcut icon\" href=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("favicon.ico\" type=\"image/x-icon\" />\r\n");
	templateBuilder.Append("<link rel=\"stylesheet\" href=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/sas.css\" type=\"text/css\" media=\"all\" />\r\n");
	templateBuilder.Append("<link rel=\"stylesheet\" href=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/float.css\" type=\"text/css\" />\r\n");
	templateBuilder.Append(link.ToString());
	templateBuilder.Append("\r\n");
	templateBuilder.Append("<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("javascript/common.js\"></");
	templateBuilder.Append("script>\r\n");
	templateBuilder.Append("<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("javascript/ajax.js\"></");
	templateBuilder.Append("script>\r\n");
	templateBuilder.Append("<script type=\"text/javascript\">\r\n");
	templateBuilder.Append("	var aspxrewrite = ");
	templateBuilder.Append(config.Aspxrewrite.ToString().Trim());
	templateBuilder.Append(";\r\n");
	templateBuilder.Append("	var IMGDIR = '");
	templateBuilder.Append(forumurl.ToString());
	templateBuilder.Append("templates/");
	templateBuilder.Append(templatepath.ToString());
	templateBuilder.Append("/images'\r\n");
	templateBuilder.Append("    var allowfloatwin = ");
	templateBuilder.Append(config.Allowfloatwin.ToString().Trim());
	templateBuilder.Append("\r\n");
	templateBuilder.Append("	var rooturl=\"");
	templateBuilder.Append(rooturl.ToString());
	templateBuilder.Append("\";\r\n");
	templateBuilder.Append("</");
	templateBuilder.Append("script>\r\n");
	templateBuilder.Append(script.ToString());
	templateBuilder.Append("\r\n");
	templateBuilder.Append("</head>\r\n");


	templateBuilder.Append("<body onkeydown=\"if(event.keyCode==27) return false;\">\r\n");
	templateBuilder.Append("<div id=\"append_parent\"></div><div id=\"ajaxwaitid\"></div>\r\n");
	templateBuilder.Append("<div id=\"submenu\">\r\n");
	templateBuilder.Append("	<div class=\"wrap s_clear\">\r\n");
	templateBuilder.Append("	<span class=\"avataonline right\">\r\n");

	if (userid==-1)
	{

	templateBuilder.Append("		<a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("login.aspx\" onClick=\"floatwin('open_login', '");
	templateBuilder.Append(rooturl.ToString());
	templateBuilder.Append("login.aspx', 600, 410);return false;\">[登录]</a>\r\n");
	templateBuilder.Append("		<a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("register.aspx\" onClick=\"floatwin('open_register', '");
	templateBuilder.Append(rooturl.ToString());
	templateBuilder.Append("register.aspx', 600, 410);return false;\">[注册]</a\r\n");
	templateBuilder.Append("		>\r\n");

	}
	else
	{

	templateBuilder.Append("		欢迎<a class=\"drop\" id=\"viewpro\" onMouseOver=\"showMenu(this.id)\">");
	templateBuilder.Append(username.ToString());
	templateBuilder.Append("</a>\r\n");
	templateBuilder.Append("		<span class=\"pipe\">|</span>\r\n");
	templateBuilder.Append("		<a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("logout.aspx?userkey=");
	templateBuilder.Append(userkey.ToString());
	templateBuilder.Append("\">退出</a>\r\n");
	templateBuilder.Append("		<span class=\"pipe\">|</span>\r\n");

	}	//end if

	templateBuilder.Append("	</span>\r\n");

	if (userid!=-1)
	{

	templateBuilder.Append("	<a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("usercpinbox.aspx\" class=\"inbox\" title=\"");
	templateBuilder.Append(oluserinfo.Ol_newpms.ToString().Trim());
	templateBuilder.Append("条新短消息\">收件箱</a>\r\n");

	if (oluserinfo.Ol_newpms>0)
	{

	templateBuilder.Append("<cite>");
	templateBuilder.Append(oluserinfo.Ol_newpms.ToString().Trim());
	templateBuilder.Append("</cite>\r\n");

	}	//end if

	templateBuilder.Append("	<span class=\"pipe\">|</span>\r\n");
	templateBuilder.Append("	<a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("usercpnotice.aspx?filter=all\" class=\"notice\">通知</a>\r\n");

	if (oluserinfo.Ol_newnotices>0)
	{

	templateBuilder.Append("<cite>");
	templateBuilder.Append(oluserinfo.Ol_newnotices.ToString().Trim());
	templateBuilder.Append("</cite>\r\n");

	}	//end if

	templateBuilder.Append("	<span class=\"pipe\">|</span>\r\n");
	templateBuilder.Append("	<a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("usercp.aspx\" class=\"usercp\">用户中心</a>\r\n");
	templateBuilder.Append("	<span class=\"pipe\">|</span>\r\n");

	if (useradminid==1)
	{

	templateBuilder.Append("	<a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("ManagePage/index.aspx\" target=\"_blank\" class=\"systemmanage\">系统设置</a>\r\n");
	templateBuilder.Append("	<span class=\"pipe\">|</span>\r\n");

	}	//end if

	templateBuilder.Append("	<a href=\"javascript:void(0);\" class=\"drop\" id=\"mymenu\" onMouseOver=\"showMenu(this.id, false);\" >我的</a>\r\n");
	templateBuilder.Append("	<ul class=\"popupmenu_popup headermenu_popup\" id=\"mymenu_menu\" style=\"display: none\">\r\n");
	templateBuilder.Append("		<li><a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("mytopics.aspx\">我的主题</a></li>\r\n");
	templateBuilder.Append("		<li><a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("myposts.aspx\">我的帖子</a></li>\r\n");
	templateBuilder.Append("		<li><a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("search.aspx?posterid=current&type=digest\">我的精华</a></li>\r\n");
	templateBuilder.Append("		<li><a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("myattachment.aspx\">我的附件</a></li>\r\n");
	templateBuilder.Append("		<li><a href=\"");
	templateBuilder.Append(forumpath.ToString());
	templateBuilder.Append("usercpsubscribe.aspx\">我的收藏</a></li>\r\n");
	templateBuilder.Append("    </ul>\r\n");

	}	//end if

	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("</div>\r\n");

	}
	else
	{


	Response.Clear();
	Response.ContentType = "Text/XML";
	Response.Expires = 0;
	Response.Cache.SetNoStore();
	
	templateBuilder.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?><root><![CDATA[\r\n");

	}	//end if




	if (infloat!=1)
	{

	templateBuilder.Append("<div id=\"nav\">\r\n");
	templateBuilder.Append("	<div class=\"wrap s_clear\"><a href=\"");
	templateBuilder.Append(config.Siteurl.ToString().Trim());
	templateBuilder.Append("\" class=\"title\">");
	templateBuilder.Append(config.Sitetitle.ToString().Trim());
	templateBuilder.Append("</a> &raquo; 用户登录</div>\r\n");
	templateBuilder.Append("</div>\r\n");

	}	//end if


	if (ispost && !loginsubmit)
	{


	if (infloat==1)
	{


	if (page_err==0)
	{

	templateBuilder.Append("			<script type=\"text/javascript\">\r\n");
	templateBuilder.Append("			$('form1').style.display='none';\r\n");
	templateBuilder.Append("			$('returnmessage').className='';\r\n");
	templateBuilder.Append("			</");
	templateBuilder.Append("script>\r\n");
	templateBuilder.Append("			<div class=\"msgbox\">\r\n");
	templateBuilder.Append("			<h1>Sirius Studio 提示信息</h1>\r\n");
	templateBuilder.Append("			<p>");
	templateBuilder.Append(msgbox_text.ToString());
	templateBuilder.Append("</p>\r\n");

	if (msgbox_url!="")
	{

	templateBuilder.Append("			<h1><a href=\"javascript:;\" onclick=\"location.reload()\">如果长时间没有响应请点击此处</a></h1>\r\n");
	templateBuilder.Append("			<script type=\"text/javascript\">setTimeout('location.reload()', 3000);</");
	templateBuilder.Append("script>\r\n");

	}	//end if

	templateBuilder.Append("			</div>\r\n");

	}
	else
	{

	templateBuilder.Append("			<p>");
	templateBuilder.Append(msgbox_text.ToString());
	templateBuilder.Append("</p>\r\n");

	}	//end if


	}
	else
	{


	if (page_err==0)
	{





	}
	else
	{


	templateBuilder.Append("<div class=\"wrap s_clear\" id=\"wrap\">\r\n");
	templateBuilder.Append("<div class=\"main\">\r\n");
	templateBuilder.Append("	<div class=\"msgbox error_msg\">\r\n");
	templateBuilder.Append("		<h1>出现了");
	templateBuilder.Append(page_err.ToString());
	templateBuilder.Append("个错误</h1>\r\n");
	templateBuilder.Append("		<p>");
	templateBuilder.Append(msgbox_text.ToString());
	templateBuilder.Append("</p>\r\n");
	templateBuilder.Append("		<p class=\"errorback\">\r\n");
	templateBuilder.Append("			<script type=\"text/javascript\">\r\n");
	templateBuilder.Append("				if(");
	templateBuilder.Append(msgbox_showbacklink.ToString());
	templateBuilder.Append(")\r\n");
	templateBuilder.Append("				{\r\n");
	templateBuilder.Append("					document.write(\"<a href=\\\"");
	templateBuilder.Append(msgbox_backlink.ToString());
	templateBuilder.Append("\\\">返回上一步</a> &nbsp; &nbsp;|&nbsp; &nbsp  \");\r\n");
	templateBuilder.Append("				}\r\n");
	templateBuilder.Append("			</");
	templateBuilder.Append("script>\r\n");
	templateBuilder.Append("			<a href=\"forumindex.aspx\">论坛首页</a>\r\n");

	if (usergroupid==7)
	{

	templateBuilder.Append("			 &nbsp; &nbsp|&nbsp; &nbsp; <a href=\"register.aspx\">注册</a>\r\n");

	}	//end if

	templateBuilder.Append("		</p>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("</div>\r\n");
	templateBuilder.Append("</div>\r\n");



	}	//end if


	}	//end if


	}
	else
	{


	if (infloat!=1)
	{

	templateBuilder.Append("<div class=\"wrap s_clear\" id=\"wrap\">\r\n");
	templateBuilder.Append("<div class=\"main login\">\r\n");
	templateBuilder.Append("<div class=\"nojs\">\r\n");
	templateBuilder.Append("<div class=\"float\" id=\"floatlayout_login\" style=\"width: 600px; height: 360px;\"><div>\r\n");
	templateBuilder.Append("<h1>用户登录</h1>\r\n");
	templateBuilder.Append("<form id=\"form1\" name=\"form1\" method=\"post\" \r\n");

	if (loginauth!="")
	{

	templateBuilder.Append("action=\"");
	templateBuilder.Append(rooturl.ToString());
	templateBuilder.Append("login.aspx?loginauth=");
	templateBuilder.Append(loginauth.ToString());
	templateBuilder.Append("&referer=");
	templateBuilder.Append(referer.ToString());
	templateBuilder.Append("\"\r\n");

	}
	else
	{

	templateBuilder.Append("action=\"\"\r\n");

	}	//end if

	templateBuilder.Append(">\r\n");
	templateBuilder.Append("	<div class=\"loginform nolabelform\">\r\n");

	}
	else
	{

	templateBuilder.Append("<div class=\"float\" id=\"floatlayout_login\" style=\"width: 600px; height: 400px;\">\r\n");
	templateBuilder.Append("	<div style=\"width: 1800px\">\r\n");
	templateBuilder.Append("	<div class=\"floatbox floatbox1\">\r\n");
	templateBuilder.Append("	<h3 class=\"float_ctrl\">\r\n");
	templateBuilder.Append("		<span>\r\n");
	templateBuilder.Append("			<a href=\"javascript:;\" class=\"float_close\" onclick=\"floatwin('close_login')\" title=\"关闭\">关闭</a>\r\n");
	templateBuilder.Append("		</span>\r\n");
	templateBuilder.Append("	</h3>\r\n");
	templateBuilder.Append("	<div class=\"gateform\">\r\n");
	templateBuilder.Append("	<h3 id=\"returnmessage\">用户登录</h3>\r\n");
	templateBuilder.Append("	<form id=\"form1\" name=\"form1\" method=\"post\" onsubmit=\"javascript:$('form1').action='");
	templateBuilder.Append(rooturl.ToString());
	templateBuilder.Append("login.aspx?infloat=1&';ajaxpost('form1', 'returnmessage', 'returnmessage', 'onerror');return false;\" action=\"");
	templateBuilder.Append(rooturl.ToString());
	templateBuilder.Append("login.aspx?infloat=1&\">\r\n");
	templateBuilder.Append("	<div class=\"loginform nolabelform\">\r\n");

	}	//end if

	templateBuilder.Append("		<p class=\"selectinput loginpsw\">\r\n");
	templateBuilder.Append("			<label for=\"username\" onclick=\"document.form1.username.focus();\">用户名　：</label>\r\n");
	templateBuilder.Append("			<input type=\"text\" class=\"txt\" tabindex=\"1\" value=\"");
	templateBuilder.Append(postusername.ToString());
	templateBuilder.Append("\" maxlength=\"20\" size=\"25\" autocomplete=\"off\" name=\"username\" id=\"username\"/>\r\n");
	templateBuilder.Append("		</p>\r\n");

	if (loginauth=="")
	{

	templateBuilder.Append("		<p class=\"selectinput loginpsw\">\r\n");
	templateBuilder.Append("		<label for=\"password3\">密　码　：</label>\r\n");
	templateBuilder.Append("		<input type=\"password\" tabindex=\"1\" class=\"txt\" size=\"36\" name=\"password\" id=\"password3\"/>\r\n");
	templateBuilder.Append("		</p>\r\n");

	}	//end if


	if (isseccode)
	{

	templateBuilder.Append("		<div style=\"position: relative;\">\r\n");



	templateBuilder.Append("		</div>\r\n");

	}	//end if


	if (config.Secques==1)
	{

	templateBuilder.Append("		<div class=\"selecttype\">\r\n");
	templateBuilder.Append("		  <script type=\"text/javascript\">\r\n");
	templateBuilder.Append("		  function changequestion()\r\n");
	templateBuilder.Append("		  {\r\n");
	templateBuilder.Append("			  if($('question').value > 0)\r\n");
	templateBuilder.Append("			  {\r\n");
	templateBuilder.Append("				$('answer').style.display='';\r\n");
	templateBuilder.Append("			  } \r\n");
	templateBuilder.Append("			  else \r\n");
	templateBuilder.Append("			  {\r\n");
	templateBuilder.Append("				$('answer').style.display='none';\r\n");
	templateBuilder.Append("			  }\r\n");
	templateBuilder.Append("		  }\r\n");
	templateBuilder.Append("		  </");
	templateBuilder.Append("script>\r\n");
	templateBuilder.Append("			<select style=\"width: 175px; display: none;\" selecti=\"5\" id=\"question\" name=\"question\" change=\"changequestion();\">\r\n");
	templateBuilder.Append("				<option value=\"0\"></option>\r\n");
	templateBuilder.Append("			</select>\r\n");
	templateBuilder.Append("			<a tabindex=\"1\" onclick=\"loadselect_viewmenu(this, 'question', 0, 'floatlayout_login');doane(event)\" onkeyup=\"loadselect_key(this, event, 'question', 'floatlayout_login')\" onmouseout=\"this.blur()\" onmouseover=\"this.focus()\" onblur=\"loadselect_keyinit(event, 2)\" onfocus=\"loadselect_keyinit(event, 1)\" id=\"question_selectinput\" class=\"loadselect\" hidefocus=\"true\" href=\"javascript:;\">安全提问</a>\r\n");
	templateBuilder.Append("			<ul style=\"display: none;\" id=\"question_selectmenu\" class=\"newselect\" onblur=\"loadselect_keyinit(event, 2)\" onfocus=\"loadselect_keyinit(event, 1)\">\r\n");
	templateBuilder.Append("			<li onclick=\"loadselect_liset('question', 0, 'question','0',this.innerHTML, 0)\" k_value=\"0\" k_id=\"question\" class=\"\">安全提问</li>\r\n");
	templateBuilder.Append("			<li onclick=\"loadselect_liset('question', 0, 'question','1',this.innerHTML, 1)\" k_value=\"1\" k_id=\"question\">母亲的名字</li>\r\n");
	templateBuilder.Append("			<li onclick=\"loadselect_liset('question', 0, 'question','2',this.innerHTML, 2)\" k_value=\"2\" k_id=\"question\">爷爷的名字</li>\r\n");
	templateBuilder.Append("			<li onclick=\"loadselect_liset('question', 0, 'question','3',this.innerHTML, 3)\" k_value=\"3\" k_id=\"question\">父亲出生的城市</li>\r\n");
	templateBuilder.Append("			<li onclick=\"loadselect_liset('question', 0, 'question','4',this.innerHTML, 4)\" k_value=\"4\" k_id=\"question\">您其中一位老师的名字</li>\r\n");
	templateBuilder.Append("			<li onclick=\"loadselect_liset('question', 0, 'question','5',this.innerHTML, 5)\" k_value=\"5\" k_id=\"question\" class=\"\">您个人计算机的型号</li>\r\n");
	templateBuilder.Append("			<li onclick=\"loadselect_liset('question', 0, 'question','6',this.innerHTML, 6)\" k_value=\"6\" k_id=\"question\" class=\"current\">您最喜欢的餐馆名称</li>\r\n");
	templateBuilder.Append("			<li onclick=\"loadselect_liset('question', 0, 'question','7',this.innerHTML, 7)\" k_value=\"7\" k_id=\"question\">驾驶执照的最后四位数字</li>\r\n");
	templateBuilder.Append("			</ul>\r\n");
	templateBuilder.Append("		</div>\r\n");
	templateBuilder.Append("		<p>\r\n");
	templateBuilder.Append("			<input type=\"text\" tabindex=\"1\" class=\"txt\" size=\"36\" autocomplete=\"off\" style=\"display: none;\" id=\"answer\" name=\"answer\"/>\r\n");
	templateBuilder.Append("		</p>\r\n");

	}	//end if

	templateBuilder.Append("		<p class=\"logininput loginpsw\"  style=\"display:none\">\r\n");
	templateBuilder.Append("			<label for=\"templateid\">界面风格</label>\r\n");
	templateBuilder.Append("			<select name=\"templateid\" tabindex=\"13\">\r\n");
	templateBuilder.Append("			<option value=\"0\">- 使用默认 -</option>\r\n");
	templateBuilder.Append("				");
	templateBuilder.Append(templatelistboxoptions.ToString());
	templateBuilder.Append("\r\n");
	templateBuilder.Append("			</select>\r\n");
	templateBuilder.Append("		</p>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("	<div class=\"logininfo multinfo\">\r\n");
	templateBuilder.Append("		<h4>没有帐号？\r\n");

	if (infloat==1)
	{

	templateBuilder.Append("			<a href=\"");
	templateBuilder.Append(rooturl.ToString());
	templateBuilder.Append("register.aspx\"  onclick=\"floatwin('close_login');floatwin('open_register', this.href, 600, 400, '600,0');return false;\">立即注册</a>\r\n");

	}
	else
	{

	templateBuilder.Append("			<a href=\"");
	templateBuilder.Append(rooturl.ToString());
	templateBuilder.Append("register.aspx\" tabindex=\"-1\" accesskey=\"r\" title=\"立即注册 (ALT + R)\">立即注册</a>\r\n");

	}	//end if

	templateBuilder.Append("		</h4>\r\n");

	if (infloat!=1)
	{

	templateBuilder.Append("		<p>忘记密码, <a href=\"");
	templateBuilder.Append(rooturl.ToString());
	templateBuilder.Append("getpassword.aspx\" tabindex=\"-1\" accesskey=\"g\" title=\"忘记密码 (ALT + G)\">找回密码</a></p>\r\n");

	}	//end if

	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("	<p class=\"fsubmit\" style=\"margin-bottom:18px;\">\r\n");
	templateBuilder.Append("		<button name=\"login\" type=\"submit\" id=\"login\" tabindex=\"1\" \r\n");

	if (infloat!=1)
	{

	templateBuilder.Append("onclick=\"javascript:window.location.replace('?agree=yes')\"\r\n");

	}	//end if

	templateBuilder.Append(">登录</button>\r\n");
	templateBuilder.Append("		<input type=\"checkbox\" value=\"43200\" tabindex=\"1\" id=\"expires\" name=\"expires\"/>\r\n");
	templateBuilder.Append("		<label for=\"cookietime\">记住我的登录状态</label>\r\n");
	templateBuilder.Append("	</p>\r\n");
	templateBuilder.Append("	<script type=\"text/javascript\">\r\n");
	templateBuilder.Append("		document.getElementById(\"username\").focus();\r\n");
	templateBuilder.Append("	</");
	templateBuilder.Append("script>\r\n");
	templateBuilder.Append("	</form>\r\n");
	templateBuilder.Append("	</div>\r\n");
	templateBuilder.Append("</div>\r\n");
	templateBuilder.Append("</div>\r\n");
	templateBuilder.Append("</div>\r\n");
	templateBuilder.Append("</div>\r\n");

	}	//end if



	if (infloat!=1)
	{

	templateBuilder.Append("Powered by <strong><a href=\"http://www.sirius.org.cn\" target=\"_blank\" title=\"天狼星工作室\">Studio after 80s</a></strong> <em class=\"f_bold\">1.0.0</em>\r\n");



	templateBuilder.Append("</body>\r\n");
	templateBuilder.Append("</html>\r\n");

	}
	else
	{

	templateBuilder.Append("]]></root>\r\n");

	}	//end if




	Response.Write(templateBuilder.ToString());
}
</script>
