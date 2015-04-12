  
//封装StringBuilder
function StringBuilder() { this._string_ = new Array(); }
StringBuilder.prototype.Append = function (str) { this._string_.push(str); }
StringBuilder.prototype.toString = function () { return this._string_.join(""); }
StringBuilder.prototype.AppendFormat = function () {
    if (arguments.length > 1) {
        var TString = arguments[0];
        if (arguments[1] instanceof Array) {
            for (var i = 0, iLen = arguments[1].length; i < iLen; i++) {
                var jIndex = i;
                var re = eval("/\\{" + jIndex + "\\}/g;");
                TString = TString.replace(re, arguments[1][i]);
            }
        } else {
            for (var i = 1, iLen = arguments.length; i < iLen; i++) {
                var jIndex = i - 1;
                var re = eval("/\\{" + jIndex + "\\}/g;");
                TString = TString.replace(re, arguments[i]);
            }
        }
        this.Append(TString);
    } else if (arguments.length == 1) {
        this.Append(arguments[0]);
    }
};

//trim去掉字符串两边的指定字符，默认去空格
String.prototype.Trim = function (str) { if (!str) { str = '\\s'; } else { if (str == '\\') { str = '\\\\'; } else if (str == ',' || str == '|' || str == ';' || str == '-') { str = '\\' + str; } else { str = '\\s'; } } eval('var reg=/(^' + str + '+)|(' + str + '+$)/g;'); return this.replace(reg, ''); };
String.prototype.trim = function (str) { return this.Trim(str); };
//判断一个字符串是否为NULL或者空字符串
String.prototype.isNull = function () { return this == null || this.trim().length == 0; }
String.prototype.equals = function (str) { return this == str; }
String.prototype.contains = function (str) { if (str) return this.indexOf(str) != -1; else return false; }
//字符串截取，后面加入...
String.prototype.interceptString = function (len) {
    if (this.length > len) {
        return this.substring(0, len - 1) + "...";
    }
    else {
        return this;
    }
}
//获得一个字符串的字节数
String.prototype.countLength = function () { var strLength = 0; for (var i = 0; i < this.length; i++) { if (this.charAt(i) > '~') strLength += 2; else strLength += 1; } return strLength; }
//根据指定的字节数截取字符串
String.prototype.cutString = function (cutLength) { if (!cutLength) { cutLength = this.countLength(); } var strLength = 0; var cutStr = ""; if (cutLength > this.countLength()) { cutStr = this; } else { for (var i = 0; i < this.length; i++) { if (this.charAt(i) > '~') { strLength += 2; } else { strLength += 1; } if (strLength >= cutLength) { cutStr = this.substring(0, i + 1); break; } } } return cutStr; };
//去掉所有的html标记
String.prototype.cutHTML = function () {
    return this.replace(/<[^>]+>/g, "");
};
//html 转义
String.prototype.SpecialCharacters = function () {
    return this.replace(/"/g, "&quot;").replace(/'/g, "&acute;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
}
//去除script标签 ,Raven Added
String.prototype.CutScript = function () {
    return this.replace(/<script>/gi, "&lt;script&gt;").replace(/<\/script>/gi, "&lt;/script&gt;");
}

String.prototype.cutStringWithHtml = function (len, rows) {
    var str = new StringBuilder();
    var strLength = 0;
    var isA = false;
    var isPic = false;
    var isBr = false;
    var brCount = 0;
    for (var i = 0; i < this.length; i++) {
        var letter = this.substring(i, i + 1);
        var nextLetter = this.substring(i + 1, i + 2);
        var nextnextLetter = this.substring(i + 2, i + 3);

        if (letter == "<" && nextLetter == "a") {//a标签包含
            isA = true;
        } else if (letter == "<" && nextLetter == "b" && nextnextLetter == "r") {//换行符
            isBr = true;
            brCount++;
        } else if (letter == "<") {//图片
            isPic = true;
        }

        if (brCount == Number(rows)) {
            break;
        }
        str.Append(letter);
        if (!isA && !isPic && !isBr) {
            if (this.charAt(i) > '~')
                strLength += 2;
            else strLength += 1;
        }

        if (isPic) {
            if (letter == ">") {
                isPic = false;
            } else {
                continue;
            }

        }
        if (isA) {
            if (letter == ">" && this.substring(i - 1, i) == "a") {
                isA = false;
            } else {
                continue;
            }
        }

        if (isBr) {
            if (letter == ">" && this.substring(i - 1, i) == "r") {
                isBr = false;
            } else {
                continue;
            }
        }

        if (strLength >= len) {
            break;
        }
    }
    return str.toString();
}
//日期格式化
Date.prototype.format = function (format) {
    var o = {
        "M+": this.getMonth() + 1,
        "d+": this.getDate(),
        "h+": this.getHours(),
        "m+": this.getMinutes(),
        "s+": this.getSeconds(),
        "q+": Math.floor((this.getMonth() + 3) / 3),
        "S": this.getMilliseconds()
    }

    if (/(y+)/.test(format)) {
        format = format.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    }

    for (var k in o) {
        if (new RegExp("(" + k + ")").test(format)) {
            format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
        }
    }
    return format;
}
//日期加减
Date.prototype.addSomeDay = function (n) {
    var uom = new Date(this - 0 + n * 86400000);
    uom = (uom.getMonth() + 1) + "/" + uom.getDate() + "/" + uom.getFullYear();
    return new Date(uom);
}
//分钟加减
Date.prototype.addSomeMinutes = function (m) {
    var currentDate = new Date(this);
    currentDate.setTime(currentDate.getTime() + parseInt(m) * 60 * 1000);
    return currentDate;
}
//月份加减
Date.isLeapYear = function (year) {
    return (((year % 4 === 0) && (year % 100 !== 0)) || (year % 400 === 0));
};
Date.getDaysInMonth = function (year, month) {
    return [31, (Date.isLeapYear(year) ? 29 : 28), 31, 30, 31, 30, 31, 31, 30, 31, 30, 31][month];
};
Date.prototype.isLeapYear = function () {
    var y = this.getFullYear();
    return (((y % 4 === 0) && (y % 100 !== 0)) || (y % 400 === 0));
};
Date.prototype.getDaysInMonth = function () {
    return Date.getDaysInMonth(this.getFullYear(), this.getMonth());
};
Date.prototype.addMonths = function (value) {
    var n = this.getDate();
    this.setDate(1);
    this.setMonth(this.getMonth() + value);
    this.setDate(Math.min(n, this.getDaysInMonth()));
    return this;
};


var File = typeof File == 'undefined' ? {} : File;
//获取后缀名
File.GetExt = function (fileName) {
    var t = fileName.split(".");
    return t[t.length - 1];
}
File.isPicture = function (fileExt) {
    var fileExts = [".jpg", ".gif", ".png", ".jpeg", ".bmp"];
    if (fileExt) {
        fileExt = fileExt.toLowerCase();
        return fileExts.contains(fileExt);
        //return fileExt == ".jpg" || fileExt == '.gif' || fileExt == '.png' || fileExt == 'jpeg';
    }
    return false;
}
File.isDocument = function (fileExt) {
    var fileExts = [".doc", ".docx", ".ppt", ".pot", ".pps", ".pptx", ".xls", ".xlsx", ".pdf", ".txt"];
    if (fileExt) {
        fileExt = fileExt.toLowerCase();
        return fileExts.contains(fileExt);
        //return fileExt == ".doc" || fileExt == ".docx" || fileExt == ".ppt" || fileExt == ".pot" || fileExt == ".pps" || fileExt == ".pptx" || fileExt == ".xls" || fileExt == ".xlsx" || fileExt == ".pdf" || fileExt == ".txt";
    }
    return false;
}
File.isCompress = function (fileExt) {
    var fileExts = [".zip", ".rar", ".7z", ".mm", ".vsd"];
    if (fileExt) {
        fileExt = fileExt.toLowerCase();
        return fileExts.contains(fileExt);
        //return fileExt == ".zip" || fileExt == ".rar" || fileExt == ".7z";
    }
    return false;
}

//关于链接的操作命名空间
var Link = {};
//把一个字符串变成链接
Link.Filter = function (str) {
    var urlReg = /http(s)?:\/\/([\w-]+\.)+[\w-]+(\/[\w- .\/?%&=])?[^ <>\[\]*(){}\u4E00-\u9FA5]+/gi;   //lio 2012-4-25 eidt   //         /^[\u4e00-\u9fa5\w]+$/;\u4E00-\u9FA5
    return str.replace(urlReg, function (m) { return '<a target="_blank" href="' + m + '">' + m + '</a>'; });
}

//验证一个字符串是否包含特殊字符
RegExp.isContainSpecial = function (str) {
    var containSpecial = RegExp(/[(\,)(\\)(\/)(\:)(\*)(\')(\?)(\\\)(\<)(\>)(\|)]+/);
    return (containSpecial.test(str));
}
//验证一个字符串时候是email
RegExp.isEmail = function (str) {
    var emailReg = /^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)*\.[\w-]+$/i;
    return emailReg.test(str);
}
//验证一个字符串是否是
RegExp.isUrl = function (str) {
    var patrn = /^http(s)?:\/\/[A-Za-z0-9\-]+\.[A-Za-z0-9\-]+[\/=\?%\-&_~`@[\]\:+!]*([^<>])*$/;
    return patrn.exec(str);
}
//验证一个字符串是否是电话或传真
RegExp.isTel = function (str) {
    var pattern = /^[+]?((\d){3,4}([ ]|[-]))?((\d){7,8})(([ ]|[-])(\d){1,12})?$/;
    return pattern.exec(str);
}
//验证一个字符串是否是手机号码
RegExp.isMobile = function (str) {
    var patrn = /^(1[3-8]{1})\d{9}$/;
    return patrn.exec(str);

}
//验证一个字符串是否是传真号
RegExp.isFax = function (str) {
    var patrn = /^[+]{0,1}(\d){1,3}[ ]?([-]?((\d)|[ ]){1,12})+$/;
    return patrn.exec(str);

}
//验证一个字符串是否为外国手机号码
RegExp.isElseMobile = function (str) {
    var patrn = /^\d{5}\d*$/;
    return patrn.exec(str);
}
//验证一个字符串是否是汉字
RegExp.isZHCN = function (str) {
    var p = /^[\u4e00-\u9fa5\w]+$/;
    return p.exec(str);
}
//验证一个字符串是否是数字
RegExp.isNum = function (str) {
    var p = /^\d+$/;
    return p.exec(str);
}
//验证一个字符串是否是纯英文
RegExp.isEnglish = function (str) {
    var p = /^[a-zA-Z., ]+$/;
    return p.exec(str);
}
// 判断是否为对象类型
RegExp.isObject = function (obj) {
    return (typeof obj == 'object') && obj.constructor == Object;
}
//验证字符串是否不包含特殊字符 返回bool
RegExp.isUnSymbols = function (str) {
    var p = /^[A-Za-z0-9\u0391-\uFFE5 \.,()，。（）]+$/;
    return p.exec(str);
}
//将一个字符串用给定的字符变成数组，
String.prototype.toArray = function (str) {
    if (this.indexOf(str) != -1) {
        return this.split(str);
    }
    else {
        if (this != '') {
            return [this.toString()];
        }
        else {
            return [];
        }
    }
};
//根据数据取得再数组中的索引
Array.prototype.getIndex = function (obj) {
    for (var i = 0; i < this.length; i++) {
        if (obj == this[i] || obj.equals(this[i])) {
            return i;
        }
    }
    return -1;
}
//移除数组中的某元素
Array.prototype.remove = function (obj) {
    for (var i = 0; i < this.length; i++) {
        if (obj.equals(this[i])) {
            this.splice(i, 1);
            break;
        }
    }
    return this;
};
//判断元素是否在数组中
Array.prototype.contains = function (obj) {
    for (var i = 0; i < this.length; i++) {
        if (obj == this[i] || obj.equals(this[i])) {
            return true;
        }
    }
    return false;
};

Array.prototype.findAll = function (fn) {
    var arr = [];
    for (var i = 0, len = this.length; i < len; i++) {
        var o = this[i];
        if (fn.call(o, o)) {
            arr.push(o);
        }
    }
    return arr;
};
Array.prototype.find = function (fn) {
    var arr = [];
    for (var i = 0, len = this.length; i < len; i++) {
        var o = this[i];
        if (fn.call(o, o)) {
            arr.push(o);
            break;
        }
    }
    return arr;
};

var enterClick = false;
//重写的alert对IE6的处理，随着滚动条的下拉而移动
window.onscroll = function () {
    if ($.browser.msie && $.browser.version == '6.0') {
        var scrollH = document.documentElement.scrollTop || window.pageYOffset || document.body.scrollTop;
        var clientH = document.documentElement.clientHeight || window.innerHeight || document.body.clientHeight;
        $("#alertDialog").css("top", (clientH / 4) - ($("#alertDialog").height() / 2) + scrollH);
    }
}
//文本框获取焦点
function TextBoxFocus(txtBox, txtValue, lang) {
    if (lang)
        txtValue = md_lang[lang];
    if ($(txtBox).val() == txtValue) {
        $(txtBox).removeClass("Gray_c").addClass("Black");
        $(txtBox).val("");
    } else if (txtValue == 'undefined') {
        $(txtBox).removeClass("Gray_c").addClass("Black");
    }
}
//文本框失去焦点
function TextBoxUnFocus(txtBox, txtValue, lang) {
    if (lang)
        txtValue = md_lang[lang];
    if ($(txtBox).val() == "") {
        $(txtBox).removeClass("Black").addClass("Gray_c");
        $(txtBox).val(txtValue);
    }
}
//计算坐标 Left Top
function getPos2(el, sProp) {
    var iPos = 0;
    while (el != null) {
        iPos += el["offset" + sProp];
        el = el.offsetParent;
    }
    return iPos;
}
//填充下拉框
function FillSelectOption(selectObj, value, text) {
    var option = document.createElement('option');
    option.value = value;
    option.text = text;
    option.innerText = text;
    //$(option).attr("title",$.trim(text));
    selectObj.appendChild(option);
}
//多选框全选控制1
function AllCheckedByName(cbx, cbxName) {
    var cbxObjs = document.getElementsByName(cbxName);
    if (cbxObjs != null) {
        for (var i = 0; i < cbxObjs.length; i++) {
            var cbxObj = cbxObjs[i];
            cbxObj.checked = cbx.checked;
        }
    }
}
//多选框全选控制2
function AllCheckedByBoolFlag(flag, cbxName) {
    var cbxObjs = document.getElementsByName(cbxName);
    if (cbxObjs != null) {
        for (var i = 0; i < cbxObjs.length; i++) {
            var cbxObj = cbxObjs[i];
            cbxObj.checked = flag;
        }
    }
}
//Cookies 操作
//写入
function setCookie(name, value) {
    var nextyear = new Date();
    nextyear.setFullYear(nextyear.getFullYear() + 10);
    var expireDate = nextyear.toGMTString();
    if (document.domain.indexOf('.mingdao.com') == -1) {
        document.cookie = name + "=" + escape(value) + ";expires=" + expireDate + ";path=/";
    } else {
        document.cookie = name + "=" + escape(value) + ";expires=" + expireDate + ";path=/;domain=.mingdao.com";
    }
}
//读取
function getCookie(name) {
    var arr = document.cookie.match(new RegExp("(^| )" + name + "=([^;]*)(;|$)"));
    if (arr != null) {
        return unescape(arr[2]);
    }
    return null;
}
//删除
function delCookie(name) {
    var exp = new Date();
    exp.setTime(exp.getTime() - 10000);
    if (getCookie(name) == null) {
        return;
    }
    var cval = document.cookie.match(new RegExp("(^| )" + name + "=([^;]*)(;|$)"))[2];
    if (cval != null) {
        if (document.domain.indexOf('.mingdao.com') == -1) {
            document.cookie = name + "=" + cval + ";expires=" + exp.toGMTString() + ";path=/";
        } else {
            document.cookie = name + "=" + cval + ";expires=" + exp.toGMTString() + ";path=/;domain=.mingdao.com";
        }

    }
} 
 
var triggers, curTimeout;
var alert = function (msg, type, autoClose, callback) {
    if (triggers && curTimeout) {
        clearTimeout(curTimeout);
        $("#alertDialog").remove();
    }
    var dialogHTML = new StringBuilder();
    dialogHTML.Append("<span id='alertDialog'");
    dialogHTML.Append("><span></span><a href='javascript:void(0);' class='close'>×</a></span>");
    $(document.body).append(dialogHTML.toString());
    /**alert x事件**/
    if (typeof (callback) != "undefined") {
        if (callback) {
            $("#alertDialog .close").click(function () {
                callback.close();
            });
        }
    } else {
        $("#alertDialog .close").click(function () {
            triggers.remove(); clearTimeout(curTimeout);
        });
    }
    $("#alertDialog").removeClass();
    if (type) {
        if (type == 2) {
            $("#alertDialog").addClass("errorDialog");
        }
        else if (type == 3) {
            $("#alertDialog").addClass("warningDialog");
        } else if (type == 4) {
            $("#alertDialog").addClass("warningErrorDialog");
        }
    }
    $("#alertDialog span").html(msg);
    var clientW = document.documentElement.clientWidth;
    var clientH = document.documentElement.clientHeight || window.innerHeight || document.body.clientHeight;
    var scrollH = document.documentElement.scrollTop || window.pageYOffset || document.body.scrollTop;
    $("#alertDialog").css({
        top: "30%",
        left: ($(window).width() - $("#alertDialog").width()) / 2 + "px"
    });

    triggers = $("#alertDialog");
    if (typeof (autoClose) != "undefined") {
        if (autoClose) {
            curTimeout = setTimeout("triggers.remove();clearTimeout(curTimeout);", 3000);
        }
    } else {
        curTimeout = setTimeout("triggers.remove();clearTimeout(curTimeout);", 3000);
    }


}
function CreateTimeSpan(dateStr) {
    var dateTime = new Date();

    var date = dateStr.split(" ")[0];
    var year = date.split("-")[0];
    var month = date.split("-")[1] - 1;
    var day = date.split("-")[2];

    var time = dateStr.split(" ")[1];
    var hour = time.split(":")[0];
    var minute = time.split(":")[1];
    var second = time.split(":")[2];

    dateTime.setFullYear(year);
    dateTime.setMonth(month);
    dateTime.setDate(day);
    dateTime.setHours(hour);
    dateTime.setMinutes(minute);
    dateTime.setSeconds(second);

    var now = new Date();

    var today = new Date();
    today.setFullYear(now.getFullYear());
    today.setMonth(now.getMonth());
    today.setDate(now.getDate());
    today.setHours(0);
    today.setMinutes(0);
    today.setSeconds(0);

    var milliseconds = 0;
    var timeSpanStr;
    if (dateTime - today >= 0) {
        milliseconds = now - dateTime;
        if (milliseconds < 1000 && milliseconds < 60000) {
            timeSpanStr = md_lang.common_phrase_justnow;
        } else if (milliseconds > 1000 && milliseconds < 60000) {
            timeSpanStr = Math.floor(milliseconds / 1000) + md_lang.myfeed_updates_time_second;
        } else if (milliseconds > 60000 && milliseconds < 3600000) {
            timeSpanStr = Math.floor(milliseconds / 60000) + md_lang.myfeed_updates_time_minute;
        } else {
            timeSpanStr = md_lang.common_phrase_today + " " + hour + ":" + minute;
        }
    }
    else {
        milliseconds = today - dateTime;
        if (milliseconds < 86400000) {
            timeSpanStr = md_lang.common_phrase_yesterday + " " + hour + ":" + minute;
        } else if (milliseconds > 86400000 && milliseconds < 31536000000) {
            timeSpanStr = (month + 1) + md_lang.PM_date_month + day + md_lang.PM_date_day + " " + hour + ":" + minute;
        } else if (milliseconds > 31536000000) {
            timeSpanStr = year + md_lang.myaccount_profile_job_year + (month + 1) + md_lang.PM_date_month + day + md_lang.PM_date_day + " " + hour + ":" + minute;
        }
    }
    return timeSpanStr;
}
 


 

 

//统一非空提醒
var glintInterval = new Object();
glintInterval.curInterval = null;
glintInterval.count = 0;
function NullTextbox(divID) {
    if ($(divID + " > *").length) {
        var bgColor = $(divID).css("backgroundColor");
        $(divID + " > *").css("backgroundColor", bgColor);
    }
    if (glintInterval.curInterval) {
        clearInterval(glintInterval.curInterval);
        glintInterval.curInterval = null;
    }
    glintInterval.curInterval = setInterval("glint('" + divID + "')", 300);
}
function glint(divID) {
    glintInterval.count++;
    try {
        $(divID + ',' + divID + ' > *,' + divID + ' :text').animate({
            backgroundColor: '#ffcccc'
        }, 180).animate({
            backgroundColor: '#ffffff'
        }, 180);
    } catch (e) { }
    if (glintInterval.count % 2 == 0) {
        clearInterval(glintInterval.curInterval);
        glintInterval.curInterval = null;
    }
}

function CountLength(str) {
    var strLength = 0;
    for (var i = 0; i < str.length; i++) {
        if (str.charAt(i) > '~') {
            strLength += 2;
        }
        else {
            strLength += 1;
        }
    }
    return strLength;
}
function ReNumString(num) {
    num = parseInt(num);
    var reStr;
    if (num > 0 && num < 10) {
        reStr = num.toString();
    }
    else if (num >= 10 && num < 100) {
        reStr = "9+";
    }
    else if (num >= 100) {
        reStr = "99+";
    }

    return reStr;
}
 

function removeHTMLTag(str) {
    str = str.replace(/<\/?[^>]*>/g, ''); //去除HTML tag
    str = str.replace(/[ | ]*\n/g, '\n'); //去除行尾空白
    str = str.replace(/\n[\s| | ]*\r/g, '\n'); //去除多余空行
    str = str.replace(/&nbsp;/ig, '' +
        ''); //去掉&nbsp;
    return str;
}


 
 
 
 

//关闭窗口
function CloseWebPage() {
    if (navigator.userAgent.indexOf("MSIE") > 0) {
        if (navigator.userAgent.indexOf("MSIE 6.0") > 0) {
            window.opener = null;
            window.close();
        } else {
            window.open('', '_top');
            window.top.close();
        }
    }
    else if (navigator.userAgent.indexOf("Firefox") > 0) {
        window.location.href = 'about:blank ';
    } else {
        window.opener = null;
        window.open('', '_self', '');
        window.close();
    }
}
