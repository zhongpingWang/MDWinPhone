var Login = {

    //用户登录
    userLogin: function () {
        var data = {};
        data.userName = $("#txtUserName").val();
        if (data.userName) {
            data.userName = data.userName.trim();
        }
        data.userPwd = $("#txtPwd").val();
        if (data.userPwd) {
            data.userPwd = data.userPwd.trim();
        }
        data.type = "getCode";
        window.external.notify(JSON.stringify(data)); 

    },


    loginCallback: function (responseStr) {
    
        alert(responseStr);
    
    },


    //初始化
    init: function () {

        $("#btnLogin").bind("touchend", function (event) {
            Login.userLogin();
            event.stopPropagation();
        });

    }



}


function loginCallback(responseStr) {
    alert(responseStr);

}