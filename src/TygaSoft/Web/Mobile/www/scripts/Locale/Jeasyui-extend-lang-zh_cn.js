$.extend($.fn.validatebox.defaults.rules, {
    select: {
        validator: function (value, param) {
            return value != "-1";
        },
        message: '必选项'
    },
    cfmPsw: {
        validator: function (value, param) {
            return value == $(param[0]).val();
        },
        message: '前后输入密码不正确，请检查'
    },
    psw: {
        validator: function (value, param) {
            var reg = /(([0-9]+)|([a-zA-Z]+)){6,30}/;
            return reg.test(value);
        },
        message: '密码正确格式由数字或字母组成的字符串，且最小6位，最大30位'
    },
    phone: {
        validator: function (value, param) {
            return /^((\d+)|(-)?(\d+)){8,15}$/.test(value);
        },
        message: '请正确输入的手机号或电话号码'
    },
    telPhone: {
        validator: function (value, param) {
            return /^((\d+)-?(\d+)){8,15}$/.test(value);
        },
        message: '请输入正确的电话号码'
    },
    QQ: {
        validator: function (value, param) {
            return /^(\d+){5,15}$/.test(value);
        },
        message: '请输入正确的QQ号码'
    },
    validCode: {
        validator: function (value, param) {
            return /(([0-9]+)|([a-zA-Z]+)){4,4}/.test(value);
        },
        message: '请输入正确的验证码'
    },
    int: {
        validator: function (value, param) {
            return /^\d+$/.test(value);
        },
        message: '请输入数字'
    },
    ne: {
        validator: function (value, param) {
            return (/[0-9a-zA-Z]/.test(value));
        },
        message: '请输入数字或字母组成的字符串'
    },
    float: {
        validator: function (value, param) {
            return /^\d+(\.\d+)?$/.test(value);
        },
        message: '请输入数字或浮点数'
    },
    price: {
        validator: function (value, param) {
            return /(^\d+$)|(^(\d+)\.(\d+){1,2}$)/.test(value);
        },
        message: '请输入正确的金额'
    }
});
