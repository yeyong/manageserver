/*************************************************分页函数****************************************************/

function ajaxpagination(ajaxfunction, recordcount, pagesize, currentpage, divname) {

    var allcurrentpage = 0;
    var next = 0;
    var pre = 0;
    var startcount = 0;
    var endcount = 0;
    var currentpagestr = '';

    if (currentpage < 1) {
        currentpage = 1;
    }

    //计算总页数
    if (pagesize != 0) {
        allcurrentpage = parseInt((recordcount / pagesize));
        allcurrentpage = ((recordcount % pagesize) != 0 ? allcurrentpage + 1 : allcurrentpage);
        allcurrentpage = (allcurrentpage == 0 ? 1 : allcurrentpage);
    }
    next = currentpage + 1;
    pre = currentpage - 1;

    //中间页起始序号
    startcount = (currentpage + 5) > allcurrentpage ? allcurrentpage - 9 : currentpage - 4;

    //中间页终止序号
    endcount = currentpage < 5 ? 10 : currentpage + 5;

    //为了避免输出的时候产生负数，设置如果小于1就从序号1开始
    if (startcount < 1) {
        startcount = 1;
    }

    //页码+5的可能性就会产生最终输出序号大于总页码，那么就要将其控制在页码数之内
    if (allcurrentpage < endcount) {
        endcount = allcurrentpage;
    }

    if (startcount > 1) {
        currentpagestr += currentpage > 1 ? '<a href="###"  onclick="javascript:' + ajaxfunction + '(' + page_qyid + ',' + pagesize + ', ' + currentpage + ');" title="上一页">上一页</a>' : '';
    }

    //当页码数大于1时, 则显示页码
    if (endcount > 1) {
        //中间页处理, 这个增加时间复杂度，减小空间复杂度
        for (i = startcount; i <= endcount; i++) {
            currentpagestr += currentpage == i ? '<span class="page2">' + i + '</span>' : '<a href="###"  onclick="javascript:' + ajaxfunction + '(' + page_qyid + ',' + pagesize + ', ' + i + ');">' + i + '</a>';
        }
    }

    if (endcount < allcurrentpage) {
        currentpagestr += currentpage != allcurrentpage ? '<a href="###" onclick="javascript:' + ajaxfunction + '(' + page_qyid + ',' + pagesize + ', ' + next + ');" title="下一页">下一页</a>' : '';
    }

    if (allcurrentpage > 1) {
        currentpagestr += ' 共 ' + allcurrentpage + ' 页'; // + recordcount + ' 条记录';
    }

    currentpagestr += ' 到第 <input type="text" name="textfield" class="input2_soout" style="width:25px;" /> 页';
    currentpagestr += ' <input type="button" name="button1" class="agree1" value="确 定" />';
    $(divname).innerHTML = (recordcount == 0) ? '' : currentpagestr;
}
/*************************************************AJAX加载留言列表****************************************************/

function ajaxgetcomment(qyid, pagesize, pageindex) {
    $('commentlist').innerHTML = '加载数据中...';
    comment_page_currentpage = pageindex;
    _sendRequest('tools/ajax.aspx?t=getcompanycomment&qyid=' + qyid + '&pagesize=' + pagesize + '&pageindex=' + pageindex, function(d) {
        try {
            eval('leaveword_callback(' + d + ')');
        } catch (e) { alert(e.message); };
    });
}

function ajaxgetcommentscored(qyid) {
    _sendRequest('tools/ajax.aspx?t=getcompanycommentscored&qyid=' + qyid, function(d) {
        try {
            eval('leavewordmessage_callback(' + d + ')');
        } catch (e) { alert(e.message); };
    });
}

function leavewordmessage_callback(data) {
    $("commentscored").innerHTML = data;
}

function leaveword_callback(data) {
    var leaveword_html = '';

    for (var i in data) {
        leaveword_html += '<li>';
        leaveword_html += '<p class="fswnr2zi">';
        leaveword_html += '<span class="fswnr2zi1">网络评分：<b class="f_f60_24">' + data[i].scored + '</b> 分</span>';
        leaveword_html += '<span class="fswnr2zi2">' + data[i].username + ' 发表于 ' + data[i].commentdate + '</span>';
        leaveword_html += '</p>';
        leaveword_html += '<p class="fswnr2zt">' + data[i].content + '</p>';
        leaveword_html += '</li>';
    }
    //alert(leaveword_html);
    
    $('commentlist').innerHTML = leaveword_html;
    ajaxpagination('ajaxgetcomment', comment_page_recordcount, comment_page_pagesize, comment_page_currentpage, "commentlist_page");
}