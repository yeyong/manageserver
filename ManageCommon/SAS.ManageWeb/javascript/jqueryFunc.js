var validEnum =
{
    chinesename: { onshow_str: "请输入姓名", onfocus_str: "姓名仅支持中文", oncorrect_str: "您的输入正确", regexp_str: "chinese", datatype_str: "enum", onerror_str: "请填写中文姓名!", empty_str: "", min_str: "2", max_str: "10", inputonerror_str: "姓名字数为2到4个汉字！", tipid_str: "" },
    nonull: { onshow_str: "*", onfocus_str: "此信息不可为空", oncorrect_str: "您的输入正确", regexp_str: "", datatype_str: "", onerror_str: "", empty_str: "", min_str: "", max_str: "", inputonerror_str: "", tipid_str: "", fun_str: "nonull" },
    catalog: { onshow_str: "以下是您已选的主营行业", onfocus_str: "请选择您公司的主营行业", oncorrect_str: "以下是您已选的主营行业", regexp_str: "", datatype_str: "", onerror_str: "", empty_str: "", min_str: "2", max_str: "", inputonerror_str: "请选择您公司的主营行业", tipid_str: "post_cataid" },
    phone: { onshow_str: "请输入您的固定电话", onfocus_str: "请输入您的固定电话,正确格式xxxx-xxxxxxx", oncorrect_str: "您的输入正确", regexp_str: "tel", datatype_str: "enum", onerror_str: "请正确填写电话格式xxxx-xxxxxxx", empty_str: "", min_str: "", max_str: "", inputonerror_str: "", tipid_str: "" },
    mobile: { onshow_str: "请输入您的手机号", onfocus_str: "正确手机格式13xxxxxxxxx或15xxxxxxxxx", oncorrect_str: "您的输入正确", regexp_str: "mobile", datatype_str: "enum", onerror_str: "正确手机格式13xxxxxxxxx或15xxxxxxxxx", empty_str: "可以为空", min_str: "", max_str: "", inputonerror_str: "", tipid_str: "" },
    fax: { onshow_str: "请输入您的传真号", onfocus_str: "请输入您的传真号,正确格式xxxx-xxxxxxx", oncorrect_str: "您的输入正确", regexp_str: "tel", datatype_str: "enum", onerror_str: "请输入您的传真号xxxx-xxxxxxx", empty_str: "可以为空", min_str: "", max_str: "", inputonerror_str: "", tipid_str: "" },
    email: { onshow_str: "请输入您的电子邮箱信息", onfocus_str: "电子邮箱正确格式xxxx@xxx.xxx", oncorrect_str: "您的输入正确", regexp_str: "email", datatype_str: "enum", onerror_str: "电子邮箱正确格式xxxx@xxx.xxx", empty_str: "可以为空", min_str: "", max_str: "", inputonerror_str: "", tipid_str: "" },
    enweb: { onshow_str: "请输入您的企业网址", onfocus_str: "企业网址正确格式http://xxxx.xxx.xxx", oncorrect_str: "您的输入正确", regexp_str: "url", datatype_str: "enum", onerror_str: "企业网址正确格式http://xxxx.xxx.xxx", empty_str: "可以为空", min_str: "", max_str: "", inputonerror_str: "", tipid_str: "" },
    noemptysign: { onshow_str: "*", onfocus_str: "此信息不可为空", oncorrect_str: "您的输入正确", regexp_str: "notemptyoftd", datatype_str: "enum", onerror_str: "输入内容中不能包含符号和空格", empty_str: "", min_str: "2", max_str: "", inputonerror_str: "此信息内容不能为空！", tipid_str: "" },
    noemptyhtml: { onshow_str: "*", onfocus_str: "此信息不可为空", oncorrect_str: "您的输入正确", regexp_str: "notempty", datatype_str: "enum", onerror_str: "信息中不能包含空格", empty_str: "", min_str: "2", max_str: "", inputonerror_str: "此信息内容不能为空！", tipid_str: "", fun_str: "nohtml" },
    zipcode: { onshow_str: "请输入您的邮编", onfocus_str: "请输入您的邮编", oncorrect_str: "您的输入正确", regexp_str: "zipcode", datatype_str: "enum", onerror_str: "请输入您的邮编", empty_str: "", min_str: "", max_str: "", inputonerror_str: "", tipid_str: "" },
    ennamecheck: { onshow_str: "请输入企业名称", onfocus_str: "企业名称不可为空", oncorrect_str: "您的输入正确", regexp_str: "notemptyoftd", datatype_str: "enum", onerror_str: "输入内容中不能包含符号和空格", empty_str: "", min_str: "2", max_str: "", inputonerror_str: "此信息内容不能为空！", tipid_str: "", ajax_type: "get", ajax_datatype: "json", ajax_button: "nextsub", ajax_url: "../tools/ajax.aspx?t=checkenname", ajax_onerror: "该企业已在申请当中", ajax_wait: "正在对企业名称进行校验，请耐心等待..." },
    builddate: { onshow_str: "请输入日期", onfocus_str: "正确格式xxxx-xx-xx", oncorrect_str: "您的输入正确", regexp_str: "date", datatype_str: "enum", onerror_str: "正确格式xxxx-xx-xx", fun_str: "isdate" },
    money: { onshow_str: "请输入整数", onfocus_str: "请输入整数", oncorrect_str: "您的输入正确", regexp_str: "intege1", datatype_str: "enum", onerror_str: "请输入整数" }
}
//设置form验证
jQuery.fn.FormValidFunc = function(theprifix, groupnum, tipauto) {
    var _this = jQuery(this);
    if (groupnum == "") groupnum = 1;
    if (tipauto == "" || tipauto == null) tipauto = true;
    else tipauto = false;
    jQuery.formValidator.initConfig({ formid: _this.id, autotip: tipauto });
    _this.find("input,select,textarea").each(function() {
        jQuery(this).setValidValue(theprifix);
    });
    _this.submit(function() { return jQuery.formValidator.pageIsValid(groupnum); });
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
            if (typeof venum.fun_str != 'undefined' && venum.fun_str != '') {
                if (venum.fun_str == "nohtml") {
                    jQuery(this).functionValidator({ fun: havhtml });
                }
                if (venum.fun_str == "nonull") {
                    jQuery(this).functionValidator({ fun: isNullStr });
                }
                if (venum.fun_str == "isdate") {
                    jQuery(this).functionValidator({ fun: isDate });
                }
            }
            if (typeof venum.ajax_type != 'undefined' && venum.ajax_type != '') {
                jQuery(this).ajaxValidator({
                    type: venum.ajax_type,
                    url: venum.ajax_url,
                    datatype: venum.ajax_datatype,
                    success: function(data) {
                        if (data == "1") {
                            return true;
                        }
                        else {
                            return false;
                        }
                    },
                    buttons: jQuery("#" + venum.ajax_button),
                    error: function() { alert("服务器没有返回数据，可能服务器忙，请重试"); },
                    onerror: venum.ajax_onerror,
                    onwait: venum.ajax_wait
                });
            }
        }
    }
};
jQuery.fn.ExtendClick = function(fromobj, toobj, tagname) {
    var thefromobj = jQuery("#" + fromobj);
    var thetoobj = jQuery("#" + toobj);
    thefromobj.hide();
    jQuery(this).mouseover(function() {
        thefromobj.show();
    });
    jQuery(this).mouseout(function() {
        thefromobj.hide();
    });
    thefromobj.find(tagname).each(function() {
        jQuery(this).click(function() {
            thetoobj.html(jQuery(this).html());
            thefromobj.hide();
        });
    });
};