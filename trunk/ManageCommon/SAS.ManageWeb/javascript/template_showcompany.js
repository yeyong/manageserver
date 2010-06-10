/*************************************************分页函数****************************************************/

function ajaxpagination(ajaxfunction, recordcount, pagesize, currentpage, divname) {

    var allcurrentpage = 0;
    var next = 0;
    var pre = 0;
    var startcount = 0;
    var endcount = 0;
    var currentpagestr = '<BR />';

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
        currentpagestr += currentpage > 1 ? '&nbsp;&nbsp;<a href="###"  onclick="javascript:' + ajaxfunction + '(' + page_qyid + ',' + pagesize + ', ' + currentpage + ');" title="上一页">上一页</a>' : '';
    }

    //当页码数大于1时, 则显示页码
    if (endcount > 1) {
        //中间页处理, 这个增加时间复杂度，减小空间复杂度
        for (i = startcount; i <= endcount; i++) {
            currentpagestr += currentpage == i ? '&nbsp;' + i + '' : '&nbsp;<a href="###"  onclick="javascript:' + ajaxfunction + '(' + page_qyid + ',' + pagesize + ', ' + i + ');">' + i + '</a>';
        }
    }

    if (endcount < allcurrentpage) {
        currentpagestr += currentpage != allcurrentpage ? '&nbsp;&nbsp;<a href="###" onclick="javascript:' + ajaxfunction + '(' + page_qyid + ',' + pagesize + ', ' + next + ');" title="下一页">下一页</a>&nbsp;&nbsp;' : '';
    }

    if (endcount > 1) {
        currentpagestr += "&nbsp; &nbsp;";
    }

    if (allcurrentpage > 1) {
        currentpagestr += currentpage + ' / ' + allcurrentpage + ' 页'; // + recordcount + ' 条记录';
    }

    $(divname).innerHTML = (recordcount == 0) ? '' : currentpagestr;
}
/*************************************************AJAX加载留言列表****************************************************/

function ajaxgetcomment(qyid, pagesize, pageindex) {
    $('commentlist').innerHTML = '加载数据中...';
    leaveword_page_currentpage = pageindex;
    _sendRequest('tools/ajax.aspx?t=getcompanycomment&qyid=' + qyid + '&pagesize=' + pagesize + '&pageindex=' + pageindex, function(d) {
        try {
            eval('leaveword_callback(' + d + ')');
        } catch (e) { };
    });
}

function ajaxgetleavewordbyid(leavewordid) {
    _sendRequest('tools/ajax.aspx?t=getgoodsleavewordbyid&leavewordid=' + leavewordid, function(d) {
        try {
            eval('leavewordmessage_callback(' + d + ')');
        } catch (e) { };
    });
}

function leavewordmessage_callback(data) {
    if (data[0].id > 0) {
        $("message").value = data[0].message.replace(/<br \/>/g, "\r\n");
        $("leavewordid").value = data[0].id;
        $("postleaveword").value = "edit";
    }
    else {
        alert('当前留言不存在或已被删除!');
    }
}


function leaveword_callback(data) {
    var leaveword_html = '';
    leaveword_html += '<li>';

    for (var i in data) {
        
    }
    //alert(leaveword_html);

    leaveword_html += '</li>';
    $('commentlist').innerHTML = leaveword_html;
    ajaxpagination('ajaxgetcomment', comment_page_recordcount, comment_page_pagesize, comment_page_currentpage, "commentlist_page");
}