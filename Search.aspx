<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="WebApplication1.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" href="CSS/search.css" />
    <link rel="stylesheet" href="CSS/nav.css" />
    <style type="text/css">
        .auto-style1 {
            margin-left: 819;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
            <%Response.Write( "现在有" +Application["UserOnLineCnt"] + "人在线");%>
            <%Response.Write(Session.SessionID);%>
            <div id="Logo" runat="server" style="margin: 0px auto; ">
                <ul id="Func">
                    <li><a href="Search.aspx">首页</a></li>
                    <li><a href="Library.aspx">书库</a></li>
                    <li><a href="Home.aspx">个人中心</a></li>

                </ul>
            </div>
           
            <div>
            <section class="products">
                <div class="container">
                    <h3>校信通图书馆</h3>
                    <p>图书预约 自习抢座</p>
                    <div class="search">
                        <asp:TextBox name="company" ID="book" runat="server" placeholder="请输入书名全称" />
                        <asp:Button ID="search" class="search-btn" runat="server" Text="搜索" OnClick="Search" />

                    </div>

                </div>
            </section>


        </div>
         <div style="margin: 0px auto; right:0px ">
           </div>
            <p>
             <asp:Button runat="server" ID="exit" onclick="Exit" Text="退出" BackColor="#004680" ForeColor="White" Height="42px" Width="998px" CssClass="auto-style1" />
            </p>
    </form>
</body>
</html>
