<%@ Page Language="C#" CodeBehind="global_addcompany.aspx.cs" Inherits="SAS.ManageWeb.ManagePage.global_addcompany"%>
<%@ Register TagPrefix="uc1" TagName="TextareaResize" Src="../UserControls/TextareaResize.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="SAS.Control" Assembly="SAS.Control" %>
<%@ Register TagPrefix="cc3" Namespace="SAS.Control" Assembly="SAS.Control" %>
<%@ Register TagPrefix="uc2" TagName="PageInfo" Src="../UserControls/PageInfo.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
    <title>天狼星工作室综合管理后台-添加商家</title>
    <link href="../styles/datagrid.css" type="text/css" rel="stylesheet" />
    <link href="../styles/tab.css" type="text/css" rel="stylesheet" />
    <link href="../styles/dntmanager.css" type="text/css" rel="stylesheet" />
    <link href="../styles/modelpopup.css" type="text/css" rel="stylesheet" />
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
    <script type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript" src="../js/jQueryFunc.js"></script>
    <script type="text/javascript" src="../js/common.js"></script>
    <script type="text/javascript" src="../js/modalpopup.js"></script>
    <script type="text/javascript" language="javascript">
        jQuery(document).ready(function() {
            jQuery("#areas").ProvinceCity();
        });
    </script>
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
</head>
<body>
    <form id="Form1" method="post" runat="server">
	    <div class="Navbutton" style="width:98%;">
	    <table width="100%">
	    <tr>
        <td>
	    <cc3:TabControl id="TabControl1" SelectionMode="Client" runat="server" TabScriptPath="../js/tabstrip.js" height="100%">
		    <cc3:TabPage Caption="基本信息" ID="tabPage51">
		    <uc2:PageInfo id="info1" runat="server" Icon="Information" Text="添加企业信息，并设置信息相关资料。"/>
		    <table cellspacing="0" cellpadding="4" width="100%" align="center">
            <tr>
                <td class="panelbox" align="left">
                    <table width="100%">
                        <tr>
                            <td style="width: 150px">
                                企业名称:
                            </td>
                            <td>
                                <cc2:TextBox id="qyname" runat="server" CanBeNull="必填" IsReplaceInvertedComma="false" size="40" MaxLength="120">
                                </cc2:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                是否启用:
                            </td>
                            <td>
                                <cc2:RadioButtonList id="status" runat="server" RepeatColumns="2" HintInfo="设置该企业是否启用">
                                    <asp:ListItem Value="1" Selected="True">启用</asp:ListItem>
                                    <asp:ListItem Value="0">不启用</asp:ListItem>
                                </cc2:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                企业法人:
                            </td>
                            <td>
                                <cc2:TextBox id="encorp" runat="server" CanBeNull="可为空" RequiredFieldType="暂无校验" IsReplaceInvertedComma="false" Size="10" MaxLength="20">
                                </cc2:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                主要联系人:
                            </td>
                            <td>
                                <cc2:TextBox id="encontact" runat="server" CanBeNull="可为空" RequiredFieldType="暂无校验" IsReplaceInvertedComma="false" Size="10" MaxLength="20">
                                </cc2:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                固定电话:
                            </td>
                            <td>
                                <cc2:TextBox id="enphone" runat="server" CanBeNull="可为空" RequiredFieldType="固定电话" IsReplaceInvertedComma="false" Size="20">
                                </cc2:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                移动电话:
                            </td>
                            <td>
                                <cc2:TextBox id="enmobile" runat="server" CanBeNull="可为空" RequiredFieldType="移动手机" IsReplaceInvertedComma="false" Size="20">
                                </cc2:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                传真:
                            </td>
                            <td>
                                <cc2:TextBox id="enfax" runat="server" CanBeNull="可为空" RequiredFieldType="固定电话" IsReplaceInvertedComma="false" Size="20">
                                </cc2:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                电子邮箱:
                            </td>
                            <td>
                                <cc2:TextBox id="enemail" runat="server" CanBeNull="可为空" RequiredFieldType="电子邮箱" IsReplaceInvertedComma="false" Size="40">
                                </cc2:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                企业网址:
                            </td>
                            <td>
                                <cc2:TextBox id="enweb" runat="server" CanBeNull="可为空" RequiredFieldType="网页地址" IsReplaceInvertedComma="false" Size="40">
                                </cc2:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>所在城市:</td>
                            <td>
                                <div id="areas"></div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                邮编:
                            </td>
                            <td>
                                <cc2:TextBox id="enpost" runat="server" CanBeNull="可为空" RequiredFieldType="数据校验" IsReplaceInvertedComma="false" Size="10" MaxLength="6">
                                </cc2:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                企业详细地址:
                            </td>
                            <td>
                                <uc1:TextareaResize id="enaddress" runat="server" HintTitle="提示" HintInfo="企业详细地址"
                                    controlname="TabControl1:tabPage51:enaddress" Cols="40" Rows="5">
                                </uc1:TextareaResize>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                企业整体概述:
                            </td>
                            <td>
                                <cc2:TextBox id="endesc" runat="server" CanBeNull="可为空" HintTitle="提示" HintInfo="支持简单HTML" RequiredFieldType="暂无校验" IsReplaceInvertedComma="false" TextMode="MultiLine" width="450px" height="100px">
                                </cc2:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            </table>
			</cc3:TabPage>
			<cc3:TabPage Caption="工商注册信息" ID="tabPage22">
			<uc2:PageInfo id="PageInfo2" runat="server" Icon="Information" Text="请填写在营业执照上的准确注册信息"/>
                <table cellspacing="0" cellpadding="4" width="100%" align="center">
                    <tr>
                        <td class="panelbox" align="left">
                            <table width="100%">
                                <tr>
                                    <td style="width:150px">公司成立时间:</td>
                                    <td>
                                        <cc2:textbox id="enbuilddate" runat="server" RequiredFieldType="日期" IsReplaceInvertedComma="false" Size="20" Text="2009-1-1">
                                        </cc2:textbox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        企业业务类型:
                                    </td>
                                    <td>
                                        <cc2:DropDownList runat="server" ID="enType">
                                            <asp:ListItem Selected="True" Text="生产商" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="代理经销商" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="个人经销商" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="门店" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="原料商" Value="4"></asp:ListItem>
                                            <asp:ListItem Text="分销商" Value="5"></asp:ListItem>
                                            <asp:ListItem Text="服务站" Value="6"></asp:ListItem>
                                            <asp:ListItem Text="其他" Value="7"></asp:ListItem>
                                        </cc2:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        企业经济类型:
                                    </td>
                                    <td>
                                        <cc2:DropDownList runat="server" ID="enco">
                                            <asp:ListItem Selected="True" Text="有限责任公司" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="股份有限公司" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="国营公司" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="集团公司" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="合资企业" Value="4"></asp:ListItem>
                                            <asp:ListItem Text="外企" Value="5"></asp:ListItem>
                                        </cc2:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>注册资本:</td>
                                    <td>
                                        <cc2:textbox id="regcapital" runat="server" HintTitle="提示" HintInfo="请填写在营业执照上的准确注册资本" RequiredFieldType="金额" IsReplaceInvertedComma="false" Size="20">
                                        </cc2:textbox>万元
                                    </td>
                                </tr>
                                <tr>
                                    <td>注册号:</td>
                                    <td>
                                        <cc2:textbox id="regcode" runat="server" HintTitle="提示" HintInfo="请填写在营业执照上的准确注册号" RequiredFieldType="暂无校验" IsReplaceInvertedComma="false" MaxLength="20">
                                        </cc2:textbox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>注册机关:</td>
                                    <td>
                                        <cc2:textbox id="regorgan" runat="server" HintTitle="提示" HintInfo="请填写在营业执照上的准确注册机关" RequiredFieldType="暂无校验" IsReplaceInvertedComma="false" Size="40" MaxLength="20">
                                        </cc2:textbox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>最近年检时间:</td>
                                    <td>
                                        <cc2:textbox id="regyear" runat="server" RequiredFieldType="日期" IsReplaceInvertedComma="false" Size="20" Text="2009-1-1">
                                        </cc2:textbox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>营业期限:</td>
                                    <td>
                                        <cc2:textbox id="Textbox1" runat="server" RequiredFieldType="暂无校验" IsReplaceInvertedComma="false" Size="40">
                                        </cc2:textbox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>注册地址:</td>
                                    <td>
                                        <uc1:TextareaResize id="regaddress" runat="server" HintTitle="提示" HintInfo="请填写在营业执照上的准确注册地址" controlname="TabControl1:tabPage22:regaddress" Cols="40" Rows="5">
                                        </uc1:TextareaResize>
                                    </td>
                                </tr>
                                <tr>
                                    <td>主营商品/经营范围:</td>
                                    <td>
                                        <uc1:TextareaResize id="enmain" runat="server" HintTitle="提示" HintInfo="请填写在营业执照上的准确注册地址" controlname="TabControl1:tabPage22:enmain" Cols="40" Rows="5">
                                        </uc1:TextareaResize>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
			</cc3:TabPage>
			<cc3:TabPage Caption="状态信息" ID="tabPage33">
			    <uc2:PageInfo id="PageInfo1" runat="server" Icon="Information" Text="设置企业在网站上的相关状态"/>
                <table cellspacing="0" cellpadding="4" width="100%" align="center">
                    <tr>
                        <td class="panelbox" align="left">
                            <table width="100%">
                                <tr>
                                    <td style="width: 150px">
                                        审批状态:
                                    </td>
                                    <td>
                                        <cc2:RadioButtonList id="enstatus" runat="server" RepeatColumns="3" HintInfo="设置该企业是否通过审批">                                            
                                            <asp:ListItem Value="1" Text="审批中" Selected="True" onclick="document.getElementById('thereason').style.display = 'none'"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="审批通过" onclick="document.getElementById('thereason').style.display = 'none'"></asp:ListItem>
                                            <asp:ListItem Value="0" Text="审批未通过" onclick="document.getElementById('thereason').style.display = 'block'"></asp:ListItem>
                                        </cc2:RadioButtonList>
                                    </td>
                                </tr>
                                <tr style="display:none" id="thereason">
                                    <td>未通过原因:</td>
                                    <td>
                                        <uc1:TextareaResize id="enreason" runat="server" HintTitle="提示" HintInfo="请详细填写企业为通过审核的原因" controlname="TabControl1:tabPage33:enreason" Cols="40" Rows="5">
                                        </uc1:TextareaResize>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        企业级别:
                                    </td>
                                    <td>
                                        <cc2:RadioButtonList id="enlevels" runat="server" RepeatColumns="4" HintInfo="设置该企业会员级别">
                                            <asp:ListItem Value="0" Text="普通级别" Selected="True"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="会员级别"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="高级会员"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="贵宾会员"></asp:ListItem>
                                        </cc2:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        企业信誉度:
                                    </td>
                                    <td>
                                        <cc2:textbox id="encredit" runat="server" RequiredFieldType="数据校验" IsReplaceInvertedComma="false" Size="10">
                                        </cc2:textbox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
			</cc3:TabPage>
		</cc3:TabControl>
            <div class="Navbutton">
                <cc2:Button ID="UpdateUserGroupInf" runat="server" ValidateForm="true" Text=" 提 交 ">
                </cc2:Button>&nbsp;&nbsp;
                <cc2:Button ID="DeleteUserGroupInf" runat="server" Text=" 删 除 " ButtonImgUrl="../images/del.gif"
                    OnClientClick="if(!confirm('你确认要删除该用户组吗？\n删除后将不能恢复！')) return false;">
                </cc2:Button>&nbsp;&nbsp;
                <button type="button" class="ManagerButton" id="Button1" onclick="window.location='global_editcompany.aspx';">
                    <img src="../images/arrow_undo.gif" />
                    返 回
                </button>
            </div>
			</td>
		    </tr>
		    </table>    		
	    </div>
	    <cc2:Hint id="Hint1" runat="server" HintImageUrl="../images"></cc2:Hint>
	    </form>
	<%=footer%>	
</body>
</html>
