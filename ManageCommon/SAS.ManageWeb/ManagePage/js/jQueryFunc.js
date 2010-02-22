﻿/**
* jQuery :  jquery方法放置区域(引用common.js)
* @author   yeyong
* @example  
* @params   暂无
*/

//城市联动方法
$.fn.ProvinceCity = function(defaultvalue) {
    var _self = this;
    //插入3个空的下拉框
    _self.append("<select id=\"province\" name=\"province\"></select>");
    _self.append("<select id=\"city\" name=\"city\"></select>");
    _self.append("<select id=\"district\" name=\"district\"></select>");
    //分别获取3个下拉框
    var $sel1 = _self.find("select").eq(0);
    var $sel2 = _self.find("select").eq(1);
    var $sel3 = _self.find("select").eq(2);
    //默认省级下拉
    var pageurl = "../company/global_ajaxcall.aspx?";

    if (defaultvalue != "" && typeof defaultvalue != "undefined") {
        var strArray = defaultvalue.split(',');
        var result1 = getReturn(pageurl + "opname=area1&defvalue=" + strArray[2]);
        $sel1.append(result1);
        var result2 = getReturn(pageurl + "opname=area2&parentcode=" + strArray[2] + "&defvalue=" + strArray[1]);
        $sel2.append(result2);
        var result3 = getReturn(pageurl + "opname=area3&parentcode=" + strArray[1] + "&defvalue=" + strArray[0]);
        $sel3.append(result3);
    } else {
        var result = getReturn(pageurl + "opname=area1");
        $sel2.attr("style", "display:none");
        $sel3.attr("style", "display:none");
        $sel1.append(result);
    }

    //省级联动 控制
    var index1 = "";
    $sel1.change(function() {
        //清空其它2个下拉框
        $sel2[0].options.length = 0;
        $sel3[0].options.length = 0;
        index1 = this.value;
        if (index1 == 0) {	//当选择的为 “请选择” 时
            $sel2[0].options.length = 0;
            $sel3[0].options.length = 0;
            $sel2.attr("style", "display:none");
            $sel3.attr("style", "display:none");
        } else {
            var result2 = getReturn(pageurl + "opname=area2&parentcode=" + index1);
            $sel2.append(result2);
            $sel3[0].options.length = 0;
            $sel2.attr("style", "display:block");
            $sel3.attr("style", "display:none");
        }
    });
    //1级城市联动 控制
    var index2 = "";
    $sel2.change(function() {
        $sel3[0].options.length = 0;
        index2 = this.value;
        if (index2 == 0) {
            $sel3[0].options.length = 0;
            $sel3.attr("style", "display:none");
        } else {
            var result3 = getReturn(pageurl + "opname=area3&parentcode=" + index2);
            $sel3.append(result3);
            $sel3.attr("style", "display:block");
        }
    });
    return _self;
};

$.fn.CatalogList = function() {
    var _self = this;
    //插入3个空的下拉框
    _self.append("<select id=\"cata1\" name=\"cata1\" size=\"8\" style=\"width: 120px;\"></select>");
    _self.append("<select id=\"cata2\" name=\"cata2\" size=\"8\" style=\"width: 120px;\"></select>");
    _self.append("<select id=\"cata3\" name=\"cata3\" size=\"8\" style=\"width: 120px;\"></select>");
    _self.append("<select id=\"cata4\" name=\"cata4\" size=\"8\" style=\"width: 120px;\"></select>");
    //分别获取3个下拉框
    var $sel1 = _self.find("select").eq(0);
    var $sel2 = _self.find("select").eq(1);
    var $sel3 = _self.find("select").eq(2);
    var $sel4 = _self.find("select").eq(3);
    //默认下拉
    var pageurl = "../company/global_ajaxcall.aspx?";
    var result = getReturn(pageurl + "opname=catalog");
    $sel1.append(result);

    _self.find("select").each(function(index) {
        this.change(function() {
            var curindex = index;
            while (curindex < 4) {
                _self.find("select").eq(curindex).options.length = 0;
                curindex++;
            }
            var results = getReturn(pageurl + "opname=catalog&parentid=" + this.value);
            _self.find("select").eq(index + 1).append(results);
        });
    });

};