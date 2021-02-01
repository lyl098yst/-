<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebApplication1.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>登录页面</title>
    <link href="../CSS/login.css" rel="stylesheet" />
    <script src = "https://imgcache.qq.com/qcloud/tcbjs/1.3.5/tcb.js" ></script>

    <script  type="text/javascript">
        var env = "";
        window.onload = function () {
            var url = "json/envid.json"
            var request = new XMLHttpRequest();
            request.open("get", url);
            request.send(null);
            request.onload = function () {
                if (request.status == 200) {
                    var json = JSON.parse(request.responseText);                    
                    //console.log(json.envId)
                    env = json.envId
                }       
            }
        }
        function getMix() {
            var code = '';

            for (let i = 1; i <= 4; i++) {
                var temp = Math.floor(Math.random() * 10);
                code = code + '' + temp;
            }
            return code;
        }
       
        function getYzm() {
            var phone = document.getElementById("phone").value
            if (phone == "" || phone.length != 11) {
                alert("手机号格式不对")
                return;
            }
            var code = getMix()
            //console.log("phone", phone);
            //console.log("code", code);
            document.getElementById("yzm").value = code;
            //console.log('env', env);
            const app = tcb.init({
                env: env
            });
            app.auth().signInAnonymously().then(() => {
                //alert("登录云开发成功！");
                app.callFunction({
                    name: 'sendyzm',
                    data: {
                        mobile: phone,
                        nationcode: '86',
                        code: code,
                    },
                }).then((res) => {
                    const result = res.result; //云函数执行结果
                    //console.log("res", res);
                    alert("验证码已发送")
                    countdown()
                });
            });
        }
        function countdown() {
            document.getElementById("sendyzm").disabled = true;
            var countDownNum = 60;
            var timer = setInterval(function () {//这里把setInterval赋值给变量名为timer的变量
                //每隔一秒countDownNum就减一，实现同步
                countDownNum--;
                //然后把countDownNum存进data，好让用户知道时间在倒计着

                document.getElementById("sendyzm").value = countDownNum
                //在倒计时还未到0时，这中间可以做其他的事情，按项目需求来
                if (countDownNum == 0) {
                    //这里特别要注意，计时器是始终一直在走的，如果你的时间为0，那么就要关掉定时器！不然相当耗性能
                    //因为timer是存在data里面的，所以在关掉时，也要在data里取出后再关闭
                    clearInterval(timer);
                    document.getElementById("sendyzm").disabled = false;
                    document.getElementById("sendyzm").value = "获取验证码"
                    //关闭定时器之后，可作其他处理codes go here
                }
            }, 1000)
        }
    </script>
</head>

<body>
    <form id="Login" runat="server" class="login-box">
        <div>
            <h3>请登录</h3>
            <br />
            <div runat="server" id="shouji">
                <div class="textbox">
                    <i class="fa fa-user" aria-hidden="true"></i>手机号：<asp:TextBox  ID="phone" autocomplete="off" runat="server"  onkeypress="if (event.keyCode < 48 || event.keyCode >57) event.returnValue = false;"/>
                </div>
                <input type="button" id="sendyzm" value="获取验证码" onclick="getYzm()" />
                <asp:Label Font-Size="12px" runat="server">*请输入11位手机号</asp:Label><br />
                <div class="textbox">
                    <i class="fa fa-user" aria-hidden="true"></i>验证码<asp:TextBox ID="code" type="password" runat="server"  onkeypress="if (event.keyCode < 48 || event.keyCode >57) event.returnValue = false;"/>
                </div>
                <asp:Button runat="server" ID="button1" Text="用户名登录" OnClick="zhanghao_login"/>
                <asp:Button CssClass="btn" Text="登录" ID="Button3" runat="server" OnClick="login2" />
            </div>
            <div runat="server" id="zhanghao">
                <div class="textbox">
                    <i class="fa fa-user" aria-hidden="true"></i>用户名：<asp:TextBox ID="login_name" autocomplete="off" runat="server" />
                </div>
                <asp:Label Font-Size="12px" runat="server">*请输入以字母开头的用户名</asp:Label><br />
                <div class="textbox">
                    <i class="fa fa-user" aria-hidden="true"></i>密码：<asp:TextBox ID="login_password" type="password" runat="server" />
                </div>
                <asp:Label Font-Size="12px" autocomplete="off" runat="server">*密码必须包含字母数字下划线等</asp:Label><br />
                <asp:Button runat="server" ID="button_change" Text="手机号登录" OnClick="phone_login"/>
                <asp:Button CssClass="btn" Text="登录" ID="hbtn" runat="server" OnClick="login" />
            </div>
            
              

            <asp:TextBox runat="server" ID="yzm" hidden="hidden"></asp:TextBox>
            <asp:Button class="btn" ID="Button2" Text="注册" runat="server" OnClick="go_register" /><br />

        </div>
    </form>
</body>
</html>
