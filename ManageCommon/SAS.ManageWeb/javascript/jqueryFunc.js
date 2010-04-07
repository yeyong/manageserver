var validEnum =
{
    chinesename: { onshow_str: "请输入姓名", onfocus_str: "姓名仅支持中文", oncorrect_str: "您的输入正确", regexp_str: "chinese", datatype_str: "enum", onerror_str: "请填写中文姓名!", empty_str: "", min_str: "2", max_str: "10", inputonerror_str: "姓名字数为2到4个汉字！", tipid_str: "" },
    nonull: { onshow_str: "*", onfocus_str: "此信息不可为空", oncorrect_str: "您的输入正确", regexp_str: "", datatype_str: "", onerror_str: "", empty_str: "", min_str: "2", max_str: "", inputonerror_str: "此信息内容不能为空！", tipid_str: "" }

    //    chinesename: "请输入姓名|姓名仅支持中文|您的输入正确|chinese|enum|请填写中文姓名!||2|10|姓名字数为2到4个汉字！|",
    //    intphone: "请输入您的联系电话|正确格式0571-2111122或13546681111|你的输入正确|phonemobile|enum|请正确填写电话格式||11|13|电话请填写11-13位数字|",
    //    brandinfo: "请输入您希望代理的品牌|请输入您希望代理的品牌|您的输入正确|notemptyoftd|enum|品牌仅能输入英文、汉字和数字||1||该项不可为空，请您填写！|",
    //    idcard: "身份证信息|请正确填写您的身份证信息|你的输入正确|idcard|enum|请正确填写您的身份证信息|身份证信息可以为空||||",
    //    email: "电子邮箱信息|电子邮箱正确格式为servise@wumeiwang.com|你的输入正确|email|enum|请正确填写电子邮箱信息|||||",
    //    qq: "QQ信息|请正确填写您的QQ信息|你的输入正确|qq|enum|请正确填写您的QQ信息|QQ信息可以为空||||",
    //    location: "请您完整选择所在区域|请您完整选择所在区域|您的选择正确|||||1||请您完整选择所在区域！|locid",
    //    fax: "传真信息|正确格式0571-2111122|你的输入正确|tel|enum|请正确填写传真号|传真信息可以为空|11|13|电话请填写11-13位数字|",
    //    nonull:"必填信息|此信息不可为空|您的输入正确|||||2||此信息内容不能为空！|",
    //    msn: "msn信息|请正确填写您的msn信息|你的输入正确|email|enum|请正确填写您的msn信息|msn信息可以为空||||",
    //    mobile:"请输入您的手机号|正确格式如：13546681111|你的输入正确|mobile|enum|请正确填写电话格式|||||"
}
//设置form验证
jQuery.fn.FormValidFunc = function(theprifix, groupnum) {
    var _this = jQuery(this);
    if (groupnum == "") groupnum = 1;
    jQuery.formValidator.initConfig({ formid: _this.id, autotip: true, automodify: true });
    _this.find("input,select,textarea").each(function() {
        jQuery(this).setValidValue(theprifix);
    });
    _this.onsubmit = function() { jQuery.formValidator.pageIsValid(groupnum); };
};
//设置验证值
jQuery.fn.setValidValue = function(theprifix) {
    var theobj = jQuery(this).attr("class").split(' ')[0];
    if (theobj.indexOf(theprifix) != -1) {
        var regobj = theobj.replace(theprifix, "");
        var venum = eval("validEnum." + regobj);
        if (typeof venum != 'undefined' && venum != "") {
            jQuery(this).formValidator({ empty: venum.empty_str, onshow: venum.onshow_str, onfocus: venum.onfocus_str, oncorrect: venum.oncorrect_str, onempty: venum.empty_str, tipid: venum.tipid_str });

            if (venum.regexp_str != "") {
                jQuery(this).regexValidator({ regexp: venum.regexp_str, datatype: venum.datatype_str, onerror: venum.onerror_str });
            }
            if (venum.min_str != "") {
                if (venum.max_str == "") jQuery(this).inputValidator({ min: venum.min_str, onerror: venum.inputonerror_str });
                else jQuery(this).inputValidator({ min: venum.min_str, max: venum.max_str, onerror: venum.inputonerror_str });
            }
        }
    }
};