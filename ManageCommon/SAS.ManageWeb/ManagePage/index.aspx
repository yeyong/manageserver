<%@ Page Language="C#" Inherits="SAS.ManageWeb.ManagePage.index" Codebehind="index.aspx.cs" AutoEventWireup="True" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html dir="ltr" xmlns="http://www.w3.org/1999/xhtml">

<head>
<meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
<meta http-equiv="X-UA-Compatible" content="IE=7" />
<meta name="keywords" content="天狼星,工作室" />
<meta name="description" content="天狼星工作室综合管理后台" />
<title>天狼星工作室综合管理后台</title>
<link href="styles/dntmanager.css" rel="stylesheet" type="text/css" />
</head>

<frameset rows="43,*" frameborder="no" border="0" framespacing="0">
  <frame src="framepage/top.aspx" name="topFrame" scrolling="No" noresize="noresize" id="topFrame" />
  <%if (Request.Params["fromurl"]==null){%>				
			<frame src="framepage/managerbody.aspx"  name="mainFrame" id="mainFrame" onresize="mainFrame.setscreendiv();" scrolling="No" />
  <%}else{%>			
            <frame src="framepage/managerbody.aspx?fromurl=<%=Request.Params["fromurl"]%>"  name="mainFrame" id="mainFrame" onresize="mainFrame.setscreendiv();" scrolling="No" />
  <%}%>
   
</frameset>
</frameset><noframes></noframes>


</html>
