$.extend($.fn.validatebox.defaults.rules, {
    select: {
        validator: function (value, param) {
            return value != "-1";
        },
        message: 'This field is required.'
    },
    cfmPsw: {
        validator: function (value, param) {
            return value == $(param[0]).val();
        },
        message: 'Before and after the input password is incorrect, please check.'
    },
    psw: {
        validator: function (value, param) {
            var reg = /(([0-9]+)|([a-zA-Z]+)){6,30}/;
            return reg.test(value);
        },
        message: 'A string of digits or letters, with a minimum of 6 bits and a maximum of 30 bits.'
    },
    phone: {
        validator: function (value, param) {
            return /^((\d+)|(-)?(\d+)){8,15}$/.test(value);
        },
        message: 'Please enter the phone number or phone number.'
    },
    telPhone: {
        validator: function (value, param) {
            return /^((\d+)-?(\d+)){8,15}$/.test(value);
        },
        message: 'Please enter the correct number.'
    },
    QQ: {
        validator: function (value, param) {
            return /^(\d+){5,15}$/.test(value);
        },
        message: 'Please enter the correct QQ number.'
    },
    validCode: {
        validator: function (value, param) {
            return /(([0-9]+)|([a-zA-Z]+)){4,4}/.test(value);
        },
        message: 'Please enter the correct verification code.'
    },
    int: {
        validator: function (value, param) {
            return /^\d+$/.test(value);
        },
        message: 'Please enter a number'
    }
});
