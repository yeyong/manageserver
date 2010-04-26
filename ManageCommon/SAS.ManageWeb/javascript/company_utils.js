function isUndefined(variable) {
    return typeof variable == 'undefined' ? true : false;
}

function trim(str) {
    return (str + '').replace(/(\s+)$/g, '').replace(/^\s+/g, '');
}

function search(theform) {
    var svalue = trim(theform.searchcontent.value);
    var stype = theform.searchtype.value;
    var stype2 = theform.searchclass.value;
    if (svalue == "") return false;
    window.location = "zshy-" + stype2 + "-0-0-0-" + stype + "-0-0-" + encodeURIComponent(svalue).replace("'","%27") + ".html";
    return false;
}