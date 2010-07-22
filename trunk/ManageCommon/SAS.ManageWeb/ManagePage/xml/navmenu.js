var toptabmenu = [
{'id':'1','title':'全 局','mainmenulist':'1,2','mainmenuidlist':'1010,1020','defaulturl':'rapidset/shortcut.aspx','system':'1'},{'id':'2','title':'栏 目','mainmenulist':'3,4,5','mainmenuidlist':'2010,2020,2030','defaulturl':'forum/forum_forumstree.aspx','system':'1'},{'id':'3','title':'用 户','mainmenulist':'6,7,15','mainmenuidlist':'3010,3020,7120','defaulturl':'global/global_usergrid.aspx','system':'1'},{'id':'4','title':'帖 子','mainmenulist':'8,9','mainmenuidlist':'4010,4020','defaulturl':'forum/forum_auditpost.aspx','system':'1'},{'id':'5','title':'企 业','mainmenulist':'16,17,18','mainmenuidlist':'7220,7320,7420','defaulturl':'company/company_companygrid.aspx','system':'2'},{'id':'6','title':'其 他','mainmenulist':'11,12','mainmenuidlist':'6010,6020','defaulturl':'global/global_announcegrid.aspx','system':'1'},{'id':'7','title':'工 具','mainmenulist':'13,14','mainmenuidlist':'7010,7020','defaulturl':'global/global_cachemanage.aspx','system':'1'}
];
var mainmenu = [
{'id':'1','menuid':'1010','menutitle':'常规选项'},{'id':'2','menuid':'1020','menutitle':'界面风格'},{'id':'3','menuid':'2010','menutitle':'版块设置'},{'id':'4','menuid':'2020','menutitle':'论坛维护'},{'id':'5','menuid':'2030','menutitle':'论坛聚合'},{'id':'6','menuid':'3010','menutitle':'用户管理'},{'id':'7','menuid':'3020','menutitle':'分组与级别'},{'id':'8','menuid':'4010','menutitle':'审核管理'},{'id':'9','menuid':'4020','menutitle':'帖子相关'},{'id':'11','menuid':'6010','menutitle':'其它设置'},{'id':'12','menuid':'6020','menutitle':'运行记录'},{'id':'13','menuid':'7010','menutitle':'数据库'},{'id':'14','menuid':'7020','menutitle':'系统工具'},{'id':'15','menuid':'7120','menutitle':'团队管理'},{'id':'16','menuid':'7220','menutitle':'企业管理'},{'id':'17','menuid':'7320','menutitle':'行业管理'},{'id':'18','menuid':'7420','menutitle':'淘之购推荐管理'}
];
var submenu = [
{'menuparentid':'1010','menutitle':'基本设置','link':'global/global_baseset.aspx','frameid':'main'},{'menuparentid':'1010','menutitle':'注册与访问控制','link':'global/global_registerandvisit.aspx','frameid':'main'},{'menuparentid':'1010','menutitle':'搜索引擎优化','link':'global/global_searchengine.aspx','frameid':'main'},{'menuparentid':'1010','menutitle':'积分设置','link':'global/global_scoreset.aspx','frameid':'main'},{'menuparentid':'1010','menutitle':'安全控制','link':'global/global_safecontrol.aspx','frameid':'main'},{'menuparentid':'1010','menutitle':'时间段设置','link':'global/global_timespan.aspx','frameid':'main'},{'menuparentid':'1010','menutitle':'附件设置','link':'global/global_attach.aspx','frameid':'main'},{'menuparentid':'1010','menutitle':'论坛功能','link':'forum/forum_option.aspx','frameid':'main'},{'menuparentid':'1010','menutitle':'用户权限','link':'forum/forum_userrights.aspx','frameid':'main'},{'menuparentid':'1010','menutitle':'邮箱设置','link':'global/global_emailconfig.aspx','frameid':'main'},{'menuparentid':'1020','menutitle':'界面与显示方式','link':'global/global_uiandshowstyle.aspx','frameid':'main'},{'menuparentid':'1020','menutitle':'模板管理','link':'global/global_templatesgrid.aspx','frameid':'main'},{'menuparentid':'2010','menutitle':'版块管理','link':'forum/forum_forumstree.aspx','frameid':'main'},{'menuparentid':'2010','menutitle':'版块添加','link':'forum/forum_addforums.aspx','frameid':'main'},{'menuparentid':'2010','menutitle':'合并版块','link':'forum/forum_forumcombination.aspx','frameid':'main'},{'menuparentid':'2010','menutitle':'主题分类','link':'forum/forum_topictypesgrid.aspx','frameid':'main'},{'menuparentid':'2020','menutitle':'管理附件','link':'forum/forum_searchattchment.aspx','frameid':'main'},{'menuparentid':'2020','menutitle':'批量主题管理','link':'forum/forum_seachtopic.aspx','frameid':'main'},{'menuparentid':'2020','menutitle':'批量删帖','link':'forum/forum_searchpost.aspx','frameid':'main'},{'menuparentid':'2020','menutitle':'清理短消息','link':'forum/forum_searchsm.aspx','frameid':'main'},{'menuparentid':'2020','menutitle':'论坛数据维护','link':'forum/forum_updateforumstatic.aspx','frameid':'main'},{'menuparentid':'2020','menutitle':'远程附件设置','link':'global/global_ftpsetting.aspx?ftptype=ForumAttach','frameid':'main'},{'menuparentid':'2020','menutitle':'分表设置','link':'global/global_detachtable.aspx','frameid':'main'},{'menuparentid':'2030','menutitle':'论坛数据','link':'aggregation/aggregation_forumaggset.aspx','frameid':'main'},{'menuparentid':'2030','menutitle':'轮显图片','link':'aggregation/aggregation_rotatepic.aspx?pagename=website','frameid':'main'},{'menuparentid':'2030','menutitle':'推荐版块','link':'aggregation/aggregation_recommendforums.aspx','frameid':'main'},{'menuparentid':'3010','menutitle':'添加用户','link':'global/global_adduser.aspx','frameid':'main'},{'menuparentid':'3010','menutitle':'编辑用户','link':'global/global_usergrid.aspx','frameid':'main'},{'menuparentid':'3010','menutitle':'审核新用户','link':'forum/forum_audituser.aspx','frameid':'main'},{'menuparentid':'3010','menutitle':'合并用户','link':'global/global_combinationuser.aspx','frameid':'main'},{'menuparentid':'3010','menutitle':'批量邮件发送','link':'global/global_usergroupsendemail.aspx','frameid':'main'},{'menuparentid':'3010','menutitle':'批量短消息发送','link':'global/global_sendsmtogroup.aspx','frameid':'main'},{'menuparentid':'3020','menutitle':'添加用户组','link':'global/global_addgroup.aspx','frameid':'main'},{'menuparentid':'3020','menutitle':'编辑用户组','link':'global/global_editgroup.aspx','frameid':'main'},{'menuparentid':'3020','menutitle':'合并用户组','link':'global/global_combinationusergroup.aspx','frameid':'main'},{'menuparentid':'4010','menutitle':'审核主题','link':'forum/forum_auditnewtopic.aspx','frameid':'main'},{'menuparentid':'4010','menutitle':'审核帖子','link':'forum/forum_auditpost.aspx','frameid':'main'},{'menuparentid':'4010','menutitle':'主题回收站','link':'forum/forum_auditingtopic.aspx','frameid':'main'},{'menuparentid':'4020','menutitle':'Discuz!NT代码','link':'forum/forum_bbcodegrid.aspx','frameid':'main'},{'menuparentid':'4020','menutitle':'表情管理','link':'forum/forum_smiliemanage.aspx','frameid':'main'},{'menuparentid':'4020','menutitle':'附件类型尺寸','link':'forum/forum_attachtypesgrid.aspx','frameid':'main'},{'menuparentid':'4020','menutitle':'鉴定管理','link':'forum/forum_identifymanage.aspx','frameid':'main'},{'menuparentid':'4020','menutitle':'附件类型管理','link':'forum/forum_attchemnttypes.aspx','frameid':'main'},{'menuparentid':'4020','menutitle':'标签管理','link':'forum/forum_tagmanage.aspx','frameid':'main'},{'menuparentid':'6010','menutitle':'整站公告','link':'global/global_announcegrid.aspx','frameid':'main'},{'menuparentid':'6010','menutitle':'整站帮助','link':'global/global_helplist.aspx','frameid':'main'},{'menuparentid':'6010','menutitle':'在线列表定制','link':'global/global_onlinelistgrid.aspx','frameid':'main'},{'menuparentid':'6010','menutitle':'广告管理','link':'global/global_advsgrid.aspx','frameid':'main'},{'menuparentid':'6010','menutitle':'友情链接列表','link':'global/global_forumlinksgrid.aspx','frameid':'main'},{'menuparentid':'6010','menutitle':'勋章列表','link':'global/global_medalgrid.aspx','frameid':'main'},{'menuparentid':'6010','menutitle':'词语过滤','link':'global/global_wordgrid.aspx','frameid':'main'},{'menuparentid':'6010','menutitle':'计划任务','link':'global/global_schedulemanage.aspx','frameid':'main'},{'menuparentid':'6020','menutitle':'后台日志','link':'forum/forum_adminvisitloggrid.aspx','frameid':'main'},{'menuparentid':'6020','menutitle':'管理日志','link':'forum/forum_moderatormanagelog.aspx','frameid':'main'},{'menuparentid':'6020','menutitle':'评分日志','link':'forum/forum_ratescorelog.aspx','frameid':'main'},{'menuparentid':'6020','menutitle':'勋章授予日志','link':'forum/forum_medalsloggird.aspx','frameid':'main'},{'menuparentid':'6020','menutitle':'积分交易日志','link':'forum/forum_paymentloggrid.aspx','frameid':'main'},{'menuparentid':'7010','menutitle':'运行指定的SQL语句','link':'global/global_runsql.aspx','frameid':'main'},{'menuparentid':'7010','menutitle':'数据库日志管理','link':'global/global_logandshrinkdb.aspx','frameid':'main'},{'menuparentid':'7010','menutitle':'数据库信息备份','link':'global/global_backupandrestore.aspx','frameid':'main'},{'menuparentid':'7020','menutitle':'更新缓存','link':'global/global_cachemanage.aspx','frameid':'main'},{'menuparentid':'7020','menutitle':'手动调整版块','link':'forum/forum_forumsgrid.aspx','frameid':'main'},{'menuparentid':'7020','menutitle':'推广插件设置','link':'plugin/creditstrategy.aspx','frameid':'main'},{'menuparentid':'6010','menutitle':'公共消息管理','link':'global/global_announceprivatemessage.aspx','frameid':'main'},{'menuparentid':'1020','menutitle':'导航菜单管理','link':'global/global_navigationmanage.aspx','frameid':'main'},{'menuparentid':'7120','menutitle':'增加团队','link':'sirius/sirius_addteam.aspx','frameid':'main'},{'menuparentid':'7120','menutitle':'编辑团队','link':'sirius/sirius_manageteam.aspx','frameid':'main'},{'menuparentid':'7220','menutitle':'企业信息添加','link':'company/global_addcompany.aspx','frameid':'main'},{'menuparentid':'7220','menutitle':'企业信息管理','link':'company/company_companygrid.aspx','frameid':'main'},{'menuparentid':'7320','menutitle':'行业类别管理','link':'company/company_cataloggrid.aspx','frameid':'main'},{'menuparentid':'7220','menutitle':'企业名片模板','link':'company/company_cardtemplategrid.aspx','frameid':'main'},{'menuparentid':'7220','menutitle':'企业名片配置','link':'company/company_cardconfiggrid.aspx','frameid':'main'},{'menuparentid':'6010','menutitle':'活动管理','link':'global/global_searchactivity.aspx','frameid':'main'},{'menuparentid':'7320','menutitle':'商品类别管理','link':'taobao/taobao_categorygrid.aspx','frameid':'main'},{'menuparentid':'7420','menutitle':'编辑推荐信息','link':'taobao/taobao_editrecommend.aspx','frameid':'main'},{'menuparentid':'7420','menutitle':'添加推荐信息','link':'taobao/taobao_addrecommend.aspx','frameid':'main'},{'menuparentid':'7420','menutitle':'店铺推广','link':'taobao/taobao_collectionshop.aspx','frameid':'main'},{'menuparentid':'7320','menutitle':'品牌创建','link':'taobao/taobao_addgoodsbrand.aspx','frameid':'main'},{'menuparentid':'7320','menutitle':'品牌管理','link':'taobao/taobao_goodsbrandgrid.aspx','frameid':'main'}
];
var shortcut = []