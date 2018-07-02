const common = {
    configs: {
        pageSize: 10,
        pageIndex: 1
    },
    confirm: (message, okCallBack) => {
        swal({
            title: "Are you sure?",
            text: "",
            icon: "warning",
            buttons: true,
            dangerMode: true
        }).then((isConfirm) => {
            if (!isConfirm) return;
            okCallBack();
        });
    },
    dateFormatJson: (datetime) => {
        if (datetime == null || datetime === '')
            return '';
        var newdate = new Date(parseInt(datetime.substr(6)));
        var month = newdate.getMonth() + 1;
        var day = newdate.getDate();
        var year = newdate.getFullYear();
        var hh = newdate.getHours();
        var mm = newdate.getMinutes();
        if (month < 10)
            month = "0" + month;
        if (day < 10)
            day = "0" + day;
        if (hh < 10)
            hh = "0" + hh;
        if (mm < 10)
            mm = "0" + mm;
        return day + "/" + month + "/" + year;
    },
    dateTimeFormatJson: (datetime) => {
        if (datetime === null || datetime === "")
            return "";
        var newdate = new Date(parseInt(datetime.substr(6)));
        var month = newdate.getMonth() + 1;
        var day = newdate.getDate();
        var year = newdate.getFullYear();
        var hh = newdate.getHours();
        var mm = newdate.getMinutes();
        var ss = newdate.getSeconds();
        if (month < 10)
            month = `0${month}`;
        if (day < 10)
            day = `0${day}`;
        if (hh < 10)
            hh = `0${hh}`;
        if (mm < 10)
            mm = `0${mm}`;
        if (ss < 10)
            ss = `0${ss}`;
        return `${day}/${month}/${year} ${hh}:${mm}:${ss}`;
    },
    formatNumber: (number, precision) => {
        if (!isFinite(number)) {
            return number.toString();
        }
        var a = number.toFixed(precision).split('.');
        a[0] = a[0].replace(/\d(?=(\d{3})+$)/g, '$&,');
        return a.join('.');
    }
};
$(document).ajaxSend(function(e, xhr, options) {
    if (options.type.toUpperCase() === 'POST' || options.type.toUpperCase() === 'PUT') {
        var token = $("form").find("input[name='__RequestVerificationToken']").val();
        xhr.setRequestHeader("RequestVerificationToken", token);
    }
});