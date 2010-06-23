function isUndefined(variable) {
    return typeof variable == 'undefined' ? true : false;
}

function trim(str) {
    return (str + '').replace(/(\s+)$/g, '').replace(/^\s+/g, '');
}

function search(theform) {
    var svalue = trim(theform.searchcontent.value);
    var stype = 0;
    var stype2 = 0;
    if (!isUndefined(theform.searchtype)) stype = theform.searchtype.value;
    if (!isUndefined(theform.searchclass)) stype2 = theform.searchclass.value;
    if (svalue == "") return false;
    window.location = "zshy-" + stype2 + "-0-0-0-" + stype + "-0-0-" + encodeURIComponent(svalue).replace("'","%27") + ".html";
    return false;
}

function getsearchvalue(thevalue) {
    var svalue = trim(thevalue);
    svalue = svalue.replace("'", "%27");
    return encodeURIComponent(svalue);
}