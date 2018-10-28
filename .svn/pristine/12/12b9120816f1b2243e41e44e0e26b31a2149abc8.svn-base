var Common = {
    AppName:'/asset',
    GetRequestQueryStr: function () {
        var hashMap = {};
        var href = window.location.href;
        var queryStr = href.substr(href.indexOf('?') + 1);
        var arr = queryStr.split("&");
        var len = arr.length;
        //var sJson = "";
        if (len > 0) {
            for (var i = 0; i < len; i++) {
                //if(i>0) sJson+=','
                var item = arr[i];
                var itemArr = item.split("=");
                if (itemArr.length > 1) hashMap[itemArr[0]] = $.trim(itemArr[1]);
                else hashMap[itemArr[0]] = "";
            }
        }
        return hashMap;
    },
    GetRequestQueryStrByKey: function (key) {
        var arr = Common.GetRequestQueryStr();
        return arr[key];
    },
    EnumMenuName: {
        UCMenuParentName: '首页'
    }
}