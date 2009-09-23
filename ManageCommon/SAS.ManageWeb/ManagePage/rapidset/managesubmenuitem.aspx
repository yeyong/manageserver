<%@ Page Language="C#" CodeBehind="managesubmenuitem.aspx.cs" Inherits="SAS.ManageWeb.ManagePage.rapidset.managesubmenuitem" %>
<%@ Register TagPrefix="cc1" Namespace="SAS.Control" Assembly="SAS.Control" %>
<%@ Register Src="../UserControls/PageInfo.ascx" TagName="PageInfo" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <meta name="keywords" content="天狼星,工作室" />
    <meta name="description" content="天狼星工作室综合管理后台" />
    <title>天狼星工作室综合管理后台</title>
     <link href="../styles/datagrid.css" type="text/css" rel="stylesheet" />
    <link href="../styles/dntmanager.css" type="text/css" rel="stylesheet" />        
    <link href="../styles/modelpopup.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" src="../js/modalpopup.js"></script>
    <script type="text/javascript" src="../js/common.js"></script>
    <script type="text/javascript">
        function newMenu()
        {
            document.getElementById("opt").innerHTML = "新建子菜单项";
            document.getElementById("id").value = "-1";
            document.getElementById("menutitle").value = "";
            document.getElementById("link").value = "";
            BOX_show('neworeditsubmenuitem');
        }
        function editMenu(id,menutitle,link)
        {
            document.getElementById("opt").innerHTML = "编辑子菜单项";
            document.getElementById("id").value = id;
            document.getElementById("menutitle").value = menutitle;
            document.getElementById("link").value = link;
            BOX_show('neworeditsubmenuitem');
        }
        function chkSubmit()
        {
            if(document.getElementById("menutitle").value == "")
            {
                alert("子菜单项名称不能为空！");
                document.getElementById("menutitle").focus();
                return false;
            }
            if(document.getElementById("link").value == "")
            {
                alert("子菜单项链接不能为空！");
                document.getElementById("link").focus();
                return false;
            }
            document.getElementById("form1").submit();
            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
