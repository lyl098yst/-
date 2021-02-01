<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="WebApplication1.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>注册页面</title>
    <link href="../CSS/regist.css" rel="stylesheet" />
    
    <script src = "https://imgcache.qq.com/qcloud/tcbjs/1.3.5/tcb.js" ></script>

    <script type="text/javascript">
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
            console.log("phone", phone);
            console.log("code", code);
            document.getElementById("yzm").value = code;
            const app = tcb.init({
                env: env // 此处填入您的环境ID
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
                        console.log("res", res);
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
    <style type="text/css">
        .auto-style1 {
            width: 274px;
        }
        .auto-style2 {
            width: 204px;
        }
        .auto-style4 {
            width: 224px;
        }
        .auto-style6 {
            font-size: 12px;
            width: 222px;
        }
        .auto-style7 {
            width: 222px;
        }
        .auto-style8 {
            width: 289px;
        }
        .auto-style9 {
            width: 1148px; /*设置宽度，方便使其居中*/
            margin: 40px auto auto auto; /*上右下左*/
        }
    </style>
</head>
<body>


    <form id="form3" runat="server" method="post" class="auto-style9">

        <h2 class="action_type">用户注册</h2>
        <table>
          
            <tr>
                <td class="auto-style7">
                    <label class="first"><span>*</span>用户名</label>
                </td>

                <td>
                    <asp:TextBox ID="name" CssClass="in-1" AutoCompleteType="Disabled" runat="server" AutoPostBack="true" OnTextChanged="getNum" />
                </td>
                <td class="auto-style1">
                    <asp:Label Font-Size="12px" runat="server">*请输入以字母开头的用户名</asp:Label>
                </td>
                <td class="auto-style2">
                    <asp:Label ID="judgeName" runat="server"></asp:Label>
                </td>
                <td class="auto-style8">
                    <asp:RequiredFieldValidator ID="mustInput_name" runat="Server"
                        ControlToValidate="name"
                        SetFocusOnError="true"
                        ErrorMessage="用户名必须输入"
                        ForeColor="#ff0000" 
                        Display="dynamic"> 
                    </asp:RequiredFieldValidator>

                </td>
                <td class="auto-style4">
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                        ControlToValidate="name"
                        ValidationExpression="([a-z]|[A-Z])+\w*"
                        ErrorMessage="用户名格式错误"
                        ForeColor="#ff0000" 
                        Display="dynamic">
                    </asp:RegularExpressionValidator>
                </td>

            </tr>

            <tr>
                
                <td class="auto-style7">
                    <label class="first"><span>*</span>姓名</label>
                </td>
                <td>
                    <asp:TextBox ID="name_real" CssClass="in-1"  runat="server" AutoCompleteType="Disabled"/>
                </td>
                <td class="auto-style1">
                    <asp:Label Font-Size="12px" runat="server">*请输入真实姓名</asp:Label>
                </td>
                <td class="auto-style2">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="Server"
                        ControlToValidate="name_real"
                        SetFocusOnError="true"
                        ErrorMessage="姓名必须输入"
                        ForeColor="#ff0000" 
                        Display="dynamic">
                    </asp:RequiredFieldValidator>
                </td>

            </tr>
                        <tr>
                <td class="auto-style7">
                    <label><span>*</span>手机</label>
                </td>
                <td>
                    <asp:TextBox CssClass="in-1" ID="phone" runat="server" AutoPostBack="true" OnTextChanged="getPhone"  onkeypress="if (event.keyCode < 48 || event.keyCode >57) event.returnValue = false;"/>
                </td>
                <td class="auto-style1">
                    <input type="button" id="sendyzm" value="获取验证码" onclick="getYzm()" />
                </td>
                 <td class="auto-style2">
                    <asp:Label ID="judge_phone" runat="server"  onkeypress="if (event.keyCode < 48 || event.keyCode >57) event.returnValue = false;"></asp:Label>
                </td>
                <td class="auto-style8">
                    <asp:RequiredFieldValidator ID="must_input_phone" runat="Server"
                        ControlToValidate="phone"
                        SetFocusOnError="true"
                        ErrorMessage="手机号必须输入"
                        ForeColor="#ff0000" 
                        Display="dynamic">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="auto-style4">
                    <asp:RegularExpressionValidator ID="phone_format"
                        runat="server"
                        ControlToValidate="phone"
                        ErrorMessage="手机号码位数不正确"
                        EnableClientScript="true"
                        ForeColor="#ff0000" 
                        ValidationExpression="\d{11}"
                        Display="dynamic">
                    </asp:RegularExpressionValidator>

                </td>
            </tr>
           <tr>
               <td class="auto-style7">
                   <asp:TextBox runat="server" ID="yzm" hidden="hidden"></asp:TextBox>
               </td>
           </tr>
             <tr>
                <td class="auto-style7">
                    <label><span>*</span>验证码</label>
                </td>
                <td>
                    <asp:TextBox CssClass="in-1" ID="confirm" runat="server" AutoCompleteType="Disabled"/>
                </td>
                 <td class="auto-style1">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="Server"
                        ControlToValidate="confirm"
                        SetFocusOnError="true"
                        ForeColor="#ff0000" 
                        ErrorMessage="验证码必须输入"
                        Display="dynamic">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="auto-style2">
                    <asp:CompareValidator ID="CompareValidator1" runat="server"
                        ControlToValidate="yzm"
                        ControlToCompare="confirm"
                        Type="String"
                        EnableClientScript="true"
                        Operator="Equal"
                        ErrorMessage="验证码错误"
                        ForeColor="#ff0000" 
                        Display="dynamic">
                    </asp:CompareValidator>
                </td>
            </tr>

            <tr>
                <td class="auto-style7">
                    <label><span>*</span>密&#160;&#160;&#160;&#160;码</label>
                </td>
                <td>
                    <asp:TextBox ID="password" CssClass="in-1" TextMode="password" runat="server" />
                </td>
                <td class="auto-style1">
                    <asp:Label Font-Size="12px" runat="server">*密码为包含字母数字下划线的6-20为字符</asp:Label>
                </td>
                <td class="auto-style2">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="Server"
                        ControlToValidate="password"
                        SetFocusOnError="true"
                        ForeColor="#ff0000" 
                        ErrorMessage="密码必须输入"
                        Display="dynamic">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="auto-style8">
                    <asp:RegularExpressionValidator ID="password_format" runat="server"
                        ControlToValidate="password"
                        ForeColor="#ff0000" 
                        ErrorMessage="请输入合法密码"
                        EnableClientScript="true"
                        ValidationExpression="^(?![0-9]+$)(?![a-z]+$)(?![A-Z]+$)(?!([^(0-9a-zA-Z)])+$).{6,20}$"
                        Display="dynamic">
                    </asp:RegularExpressionValidator>
                </td>
            </tr>

            <tr>
                <td class="auto-style7">
                    <label><span>*</span>确认密码</label>
                </td>
                <td>
                    <asp:TextBox CssClass="in-1" ID="password_confirm" runat="server" TextMode="Password" />
                </td>
                <td class="auto-style1">
                    <asp:CompareValidator ID="equal_password" runat="server"
                        ControlToValidate="password"
                        ControlToCompare="password_confirm"
                        Type="String"
                        EnableClientScript="true"
                        Operator="Equal"
                        ForeColor="#ff0000" 
                        ErrorMessage="两次密码不一致"
                        Display="dynamic">
                    </asp:CompareValidator>
                </td>
            </tr>


            <tr>
                <td class="auto-style7">
                    <label><span>*</span>Email</label>
                </td>
                <td>
                    <asp:TextBox CssClass="in-1" ID="email" runat="server" />
                </td>
                <td class="auto-style1">
                    <asp:RequiredFieldValidator ID="mustInput_email" runat="Server"
                        ControlToValidate="email"
                        SetFocusOnError="true"
                        ErrorMessage="邮箱必须输入"
                        ForeColor="#ff0000" 
                        Display="dynamic">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="auto-style2">
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2"
                        runat="server"
                        ControlToValidate="email"
                        ErrorMessage="邮箱格式错误"
                        ForeColor="#ff0000" 
                        EnableClientScript="true"
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                        Display="dynamic">
                    </asp:RegularExpressionValidator>
                </td>
            </tr>


            <asp:ValidationSummary ID="valsRegister" runat="server" ShowMessageBox="true" ShowSummary="false" />

            <tr>
                <td class="auto-style7"></td>
                <td>
                    <asp:Button ID="btn" runat="server"  OnClick="regist" Text="点击注册" />
                </td>
            </tr>

            <tr >
                <td class="auto-style6">
                    &nbsp;加<span>*</span>的为必填项目
                </td>
            </tr>
            <tr>
                <td  class="auto-style6">
                    <a href="Login.aspx" style="color:white">已经注册，去登陆</a>
                </td>
                
            </tr>
        </table>
    </form>



</body>
</html>
