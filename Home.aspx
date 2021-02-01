<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="WebApplication1.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../CSS/nav.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            float: left;
            width: 46%;
            height: 625px;
        }
        .auto-style2 {
            float: left;
            width: 98%;
            height: 300px;
        }
        .auto-style3 {
            width: 293px;
            height: 49px;
        }
        .auto-style4 {
            width: 278px;
            height: 257px;
            margin-top: 0px;
        }
        .auto-style5 {
            width: 100%;
        }
    </style>
</head>

<body>

    <form id="form2" runat="server">
        <div style="margin: 0px auto; " class="auto-style5">
            <div style="float: left; width: 20%; height: 200px;">
                <div style="float: left; width: 100%; height: 50px;">
                    <h3 class="auto-style3">权限管理</h3>
                </div>
                <div class="auto-style2">
                    <asp:TreeView ID="TreeView1" runat="server" Font-Size="X-Large" Height="288px"  Width="242px">
                    </asp:TreeView>
                </div>
            </div>

            <div class="auto-style1">
                <asp:Table runat="server" Height="624px" Width="790px">

                    <asp:TableRow HorizontalAlign="Right">
                        <asp:TableCell>
                            <asp:Label ID="Label1" runat="server" Text="用户名"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell HorizontalAlign="left">
                            <asp:TextBox width="250px" Height="50px" Font-Size="24px"  disabled="disabled" ID="TextBox0" runat="server"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow HorizontalAlign="Right">
                        <asp:TableCell>
                            <asp:Label ID="Label5" runat="server" Text="真实姓名"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell HorizontalAlign="left">
                            <asp:TextBox width="250px" Height="50px" Font-Size="24px"  disabled="disabled" ID="TextBox4" runat="server"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow HorizontalAlign="Right">
                        <asp:TableCell>
                            <asp:Label ID="Label2" runat="server" Text="手机号"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell HorizontalAlign="left">
                            <asp:TextBox width="250px" Height="50px" Font-Size="24px"  disabled="disabled" ID="TextBox1" runat="server"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow HorizontalAlign="Right">
                        <asp:TableCell>
                            <asp:Label ID="Label3" runat="server" Text="邮箱"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell HorizontalAlign="left">
                            <asp:TextBox  disabled="disabled" Height="50px" Font-Size="24px"  width="250px" ID="TextBox2" runat="server"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow HorizontalAlign="Right">
                        <asp:TableCell>
                            <asp:Label ID="Label4" runat="server" Text="角色"></asp:Label>
                        </asp:TableCell>
                        <asp:TableCell HorizontalAlign="left">
                            <asp:TextBox  width="250px" Height="50px" Font-Size="24px"  disabled="disabled" ID="TextBox3" runat="server"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>

                </asp:Table>
            </div>

        </div>
    </form>
</body>
</html>
