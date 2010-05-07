﻿
$.fn.CatalogMoveUp = function(fromid, toid) {
    var fromobj = jQuery("#" + fromid);
    var toobj = jQuery("#" + toid);
    var showstr = "";
    var showid = 0;
    fromobj.find("select").each(function() {
        if ($(this)[0].options.length > 0 && $(this)[0].selectedIndex == -1) {
            alert("请选择完整的行业类别！");
            showstr = "";
            return false;
        }
        if ($(this)[0].selectedIndex == -1) return true;
        showstr += $(this)[0].options[$(this)[0].selectedIndex].text + "/";
        showid = this.value;
    });

    if (toobj[0].options.length < 4 && showstr != "") {
        for (var i = 0; i < toobj[0].options.length; i++) {
            if (toobj[0].options[i].value == showid) {
                alert("该行业类别已添加！");
                return;
            }
        }
        var newitem = new Option(showstr, showid);
        toobj[0].options.add(newitem);
    }
    return this;
};

$.fn.CatalogMoveDown = function(objid) {
    var _obj = jQuery("#" + objid);
    var sindex = _obj[0].selectedIndex;
    if (sindex != -1) _obj[0].options.remove(sindex);
};

$.fn.InitOption = function(defvalue) {
    if (defvalue == "" && defvalue == null) return;
    this.value = "";
    $(this)[0].options.length = 0;
    var cidarray = defvalue.split(",");
    var strarray = new Array();
    for (var index in cidarray) {
        var definfo = "";
        for (var newi in cats) {
            if (cidarray[index] == cats[newi].id) {
                definfo = cats[newi];
            }
        }
        if (definfo != "") strarray[index] = Loadcatadata(definfo.pid, definfo.name);
        var newoption = new Option(strarray[index], cidarray[index]);
        $(this)[0].options.add(newoption);
    }
};

function Loadcatadata(parentid, allstr) {
    var definfos = "";
    if (parentid != 0) {
        for (var i in cats) {
            if (cats[i].id == parentid) definfos = cats[i];
        }
        allstr = Loadcatadata(definfos.pid, definfos.name) + "/" + allstr;
    }
    return allstr;
}

//分类初识化
function initCategory(objid) {
    var _objselect = $("#" + objid);
    var $sel1 = _objselect.find("select").eq(0);
    if (typeof $sel1[0] == "undefined" || $sel1[0] == null) return;
    _objselect.find("select").each(function() {
        $(this)[0].options.length = 0;
    });
    for (var i in cats) {
        if (cats[i].sort == 1) {
            var newopt = new Option(cats[i].name, cats[i].id);
            $sel1[0].options.add(newopt);
        }
    }
}
//加载行业类别
function loadCategory(objid, poses) {
    var _objselect = $("#" + objid);
    var curpos = poses - 1;
    if(curpos< 0)curpos = 0;
    var $sel = _objselect.find("select").eq(curpos);
    var selvalue = $sel[0].value;
    var $sel1 = _objselect.find("select").eq(curpos + 1);
    if ($sel1 == null || typeof $sel1 == "undefined") return;
    
    _objselect.find("select").each(function(index) {
        if (index < poses) return true;
        $(this)[0].options.length = 0;
    });

    for (var i in cats) {
        if (cats[i].pid == selvalue) {
            var newopt = new Option(cats[i].name, cats[i].id);
            $sel1[0].options.add(newopt);
        }
    }
}

//地区级联
jQuery.fn.InitLocation = function() {
    var _this = jQuery(this);
    var _select1 = _this.find("select").eq(0);
    var _select2 = _this.find("select").eq(1);
    var _select3 = _this.find("select").eq(2);

    if (typeof _select1[0] == 'undefined' || _select1 == "") return;

    _select1[0].options.length = 0;
    _select1[0].options.add(new Option("请选择省份", ""));
    for (var i in provinces) {
        var newopt = new Option(provinces[i].provincename, provinces[i].provinceid);
        _select1[0].options.add(newopt);
    }

    _select1.change(function() {
        _select2[0].options.length = 0;
        _select3[0].options.length = 0;
        var svalue = _select1.val();
        _select2[0].options.add(new Option("请选择城市", ""));
        _select3[0].options.add(new Option("请选择地区", ""));
        for (var i2 in citys) {
            if (citys[i2].provinceid == svalue) {
                var cityopt = new Option(citys[i2].cityname, citys[i2].cityid);
                _select2[0].options.add(cityopt);
            }
        }
    }).change();
    _select2.change(function() {
        _select3[0].options.length = 0;
        _select3[0].options.add(new Option("请选择地区", ""));
        var svalue = _select2.val();
        for (var i3 in districts) {
            if (districts[i3].cityid == svalue) {
                var cityopt = new Option(districts[i3].districtname, districts[i3].districtid);
                _select3[0].options.add(cityopt);
            }
        }
    });
    return _this;
};

//地区级联
jQuery.fn.LoadLocation = function(controlObj) {
    var setting = {
        provinceid: 0,
        cityid: 0,
        areaid: 0,
        urlparms: ""    //为空时不进行跳转
    };
    controlObj = controlObj || {};
    jQuery.extend(setting, controlObj);

    var _this = jQuery(this);
    var _select1 = _this.find("select").eq(0);
    var _select2 = _this.find("select").eq(1);
    var _select3 = _this.find("select").eq(2);
    var isjump = false;

    _select1[0].options.length = 0;
    _select2[0].options.length = 0;
    _select3[0].options.length = 0;
    _select1[0].options.add(new Option("请选择省份", "0"));
    _select2[0].options.add(new Option("请选择城市", "0"));
    _select3[0].options.add(new Option("请选择地区", "0"));
    for (var i in provinces) {
        var newopt = new Option(provinces[i].provincename, provinces[i].provinceid);
        _select1[0].options.add(newopt);
    }
    _select1[0].value = setting.provinceid;

    if (setting.provinceid > 0) {
        for (var i2 in citys) {
            if (citys[i2].provinceid == setting.provinceid) {
                var cityopt = new Option(citys[i2].cityname, citys[i2].cityid);
                _select2[0].options.add(cityopt);
            }
        }
        _select2[0].value = setting.cityid;
    }

    if (setting.cityid > 0) {
        for (var i3 in districts) {
            if (districts[i3].cityid == setting.cityid) {
                var cityopt = new Option(districts[i3].districtname, districts[i3].districtid);
                _select3[0].options.add(cityopt);
            }
        }
        _select3[0].value = setting.areaid;
    }

    if (setting.urlparms != "") {

        _select1.change(function() {
            window.location = setting.urlparms.replace("{1}", _select1[0].value).replace("{2}", 0).replace("{3}", 0);
        });
        _select2.change(function() {
            window.location = setting.urlparms.replace("{1}", setting.provinceid).replace("{2}", _select2[0].value).replace("{3}", 0);
        });
        _select3.change(function() {
            window.location = setting.urlparms.replace("{1}", setting.provinceid).replace("{2}", setting.cityid).replace("{3}", _select3[0].value);
        });
    }
    return _this;
}