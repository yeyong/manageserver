<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Page Language="C#" %>
<html dir="ltr" xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
<meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
<meta http-equiv="X-UA-Compatible" content="IE=7" />
<meta name="description" content="网站简介" />
<meta name="keywords" content="" />
<title>内容</title>
<link href="../style/comm.css" type="text/css" rel="stylesheet" />
<link href="../style/admin.css" type="text/css" rel="stylesheet" />
<script language="javascript">
    function ShowThis(id){
        var MenuList = document.getElementsByTagName("div");
        if(document.getElementById("sort_" + id)){
            if(document.getElementById("sort_" + id).style.display == "none"){
                document.getElementById("sort_" + id).style.display = "block";
                if(document.getElementById("sort_" + id + "_img_1")) document.getElementById("sort_" + id + "_img_1").src = "../images/arrow1b.gif";
                if(document.getElementById("sort_" + id + "_img_2")) document.getElementById("sort_" + id + "_img_2").src = "../images/minus.gif";
                if(document.getElementById("cate_sort_" + id)) document.getElementById("cate_sort_" + id).className = "cate1b adline";
            }else{
                document.getElementById("sort_" + id).style.display = "none";
                if(document.getElementById("sort_" + id + "_img_1")) document.getElementById("sort_" + id + "_img_1").src = "../images/arrow1.gif";
                if(document.getElementById("sort_" + id + "_img_2")) document.getElementById("sort_" + id + "_img_2").src = "../images/plus.gif";
                if(document.getElementById("cate_sort_" + id)) document.getElementById("cate_sort_" + id).className = "cate1 adline";
            }
        }
        
        for(var j = 0 ; j < MenuList.length ; j++){
            if(MenuList[j].className == "sort" && MenuList[j].id != "sort_" + id){
                MenuList[j].style.display = "none";
                if(document.getElementById(MenuList[j].id + "_img_1")) document.getElementById(MenuList[j].id + "_img_1").src = "../images/arrow1.gif";
                if(document.getElementById(MenuList[j].id + "_img_2")) document.getElementById(MenuList[j].id + "_img_2").src = "../images/plus.gif";
                if(document.getElementById("cate_" + MenuList[j].id)) document.getElementById("cate_" + MenuList[j].id).className = "cate1 adline";
            }
        }
        
        RecordID(id);
    }
    
    function FormatStatus(){
        if(document.getElementById("sort_" + defaultid)){
            document.getElementById("sort_" + defaultid).style.display = "block";
            document.getElementById("sort_" + defaultid + "_img_1").src = "../images/arrow1b.gif";
            document.getElementById("sort_" + defaultid + "_img_2").src = "../images/minus.gif";
            if(document.getElementById("cate_sort_" + defaultid)) document.getElementById("cate_sort_" + defaultid).className = "cate1b adbg";
        }
    }
    
    function RecordID(id){
        if(document.getElementById("sid")) document.getElementById("sid").value = id;
    }
    
	function leftToggler()
	{
	var L=document.getElementById("left"); // 变量：L代表 id="left" 的标记
	var R=document.getElementById("right"); // 变量：R代表 id="right" 的标记
	if (L.className=="lt") // 判断：如果 id="left" 的class值 等于left的话，将执行下面{}里面的内容
		{
			L.className="lt1"; // 给 id="left" 的标记 加上class：left1
		}
	else // 判断：如果 id="left" 的class值 不等于left的话，将执行下面{}里面的内容
		{
			L.className="lt"; // 给 id="left" 的标记 加上class：left
			R.className="rt"; // 给 id="right" 的标记 加上class：right
		}
	}
    
    </script>
</head>

<body>

<form id="form1" runat="server">

	<div class="cot">
			<div class="lt" id="left">
				<div class="lttit">
				<p class="lttit2 adzt1">功能菜单</p>
				</div>
				<div class="ltso">
				<input type="text" name="textfield" class="input2" style="width:120px;" />
				<input type="button" name="button2" class="an2" value="查询" />
				</div>
				<div>
                    <center>
                        <div id="cate_sort_244" class="cate1 adline">
                            <div class="cate2">
                                <a href="javascript:ShowThis(244);"><img id="sort_244_img_1" src="../images/arrow1.gif" alt="" /></a> 
                                <a href="javascript:ShowThis(244);">列表管理</a>
                            </div>
                            <div class="cate3">
                                <a href="javascript:ShowThis(244);"><img id="sort_244_img_2" src="../images/plus.gif" style="cursor: pointer;" alt="" /></a>
                            </div>
                        </div>
                        <div id="sort_244" class="sort" style="display: none;">
                            <ul>
                                <li>
                                   <img src="../images/arrow2.gif" style="vertical-align: middle" alt="" />
                                   <a href="../substance/list.aspx" target="main">列表管理</a>
                                </li>
                            </ul>
                        </div>
                    </center>
                
                    <center>
                        <div id="cate_sort_250" class="cate1 adline">
                            <div class="cate2">
                                <a href="javascript:ShowThis(250);"><img id="sort_250_img_1" src="../images/arrow1.gif" alt="" /></a> 
                                <a href="javascript:ShowThis(250);">添加管理</a>
                            </div>
                            <div class="cate3">
                                <a href="javascript:ShowThis(250);"><img id="sort_250_img_2" src="../images/plus.gif" style="cursor: pointer;" alt="" /></a>
                            </div>
                        </div>
                        <div id="sort_250" class="sort" style="display: none;">
                            <ul>
                                <li>
                                   <img src="../images/arrow2.gif" style="vertical-align: middle" alt="" />
                                   <a href="../substance/edit.aspx" target="main">编辑管理</a>
                                </li>
                                <li>
                                    <img src="../images/arrow2.gif" style="vertical-align: middle" alt="" />
                                    <a href="../substance/edit.aspx" target="main">编辑管理</a>
                                </li>
                            </ul>
                        </div>
                    </center>
                
                    <center>
                        <div id="cate_sort_31" class="cate1 adline">
                            <div class="cate2">
                                <a href="javascript:ShowThis(31);"><img id="sort_31_img_1" src="../images/arrow1.gif" alt="" /></a>
                                <a href="javascript:ShowThis(31);">链接管理</a>
                            </div>
                            <div class="cate3">
                                <a href="javascript:ShowThis(31);"><img id="sort_31_img_2" src="../images/plus.gif" style="cursor: pointer;" alt="" /></a>
                            </div>
                        </div>
                        <div id="sort_31" class="sort" style="display: none;">
                            <ul>
                               <li>
                                  <img src="../images/arrow2.gif" style="vertical-align: middle" alt="" />
                                  <a href="#" target="main">图片链管理</a>
                               </li>
							   <li>
                                   <img src="../images/arrow2.gif" style="vertical-align: middle" alt="" />
                                   <a href="#" target="main">文字链管理</a>
                               </li>
                            </ul>
                        </div>
                    </center>
                
                    <center>
                        <div id="cate_sort_65" class="cate1 adline">
                            <div class="cate2">
                                <a href="javascript:ShowThis(65);"><img id="sort_65_img_1" src="../images/arrow1.gif" alt="" /></a>
                                <a href="javascript:ShowThis(65);">帐户管理</a>
                            </div>
                            <div class="cate3">
                                <a href="javascript:ShowThis(65);"><img id="sort_65_img_2" src="../images/plus.gif" style="cursor: pointer;" alt="" /></a>
                            </div>
                        </div>
                        <div id="sort_65" class="sort" style="display: none;">
                            <ul>
                               <li>
                                   <img src="../images/arrow2.gif" style="vertical-align: middle" alt="" />
                                   <a href="#" target="main">帐户信息管理</a>
                               </li>
                            </ul>
                        </div>
                    </center>
	        </div>
			</div>
			
			<div class="rt1">
				<a href="javascript:leftToggler()" title="开启或关闭侧边栏"><span class="rt1tu1"></span></a>
			</div>
			
			<div class="rt" id="right">
				<p class="site">您当前的位置：信息管理 &gt; 公告管理</p>
				<iframe id="main" name="main" frameborder="0" scrolling="auto" src="../substance/show.aspx" width="100%" height="96%" style="z-index:2;"></iframe>
			</div>
			
	</div>
	
	
</form>

</body>
</html>
