//分类初识化
function initCategory(objid) {
    var _objselect = $("#" + objid);
    var $sel1 = _objselect.find("select").eq(0);
    _objselect.find("select").each(function() {
        $(this)[0].options.length = 0;
    });
    for (var i in cats) {
        if (cats[i].pid == 0) {
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