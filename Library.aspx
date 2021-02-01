<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Library.aspx.cs" Inherits="WebApplication1.Library" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" href="CSS/nav.css" />
    
</head>
<body>
    <form id="form1" runat="server">
        
        <div style="float: right; width: 100%;">
            <div class="search">
                <asp:TextBox name="company" ID="book" runat="server" placeholder="请输入书名全称" />
                <asp:Button ID="search" class="search-btn" runat="server" Text="搜索" OnClick="Search" />

                <asp:Button ID="search0" class="search-btn" runat="server" Text="首页" OnClick="back" />

            </div>
            <div>
                <asp:Button ID="Button1" class="button" runat="server" Text="上一页" OnClick="LastPage" />
                <asp:Button ID="Button3" class="button" runat="server" Text="下一页" OnClick="NextPage" />
                <asp:Label runat="server" ID="current"></asp:Label>
                /
                <asp:Label runat="server" ID="count"></asp:Label>
            </div>
            <div>
                <asp:Table ID="MyTable" runat="server">
                    <asp:TableHeaderRow>
                        <asp:TableCell>
                                <p style="width:100px;padding-left:30px" class="overline">剩余量</ p>
                        </asp:TableCell>
                        <asp:TableCell>
                                <p style="width:100px;padding:10px" class="overline">书名</ p>
                        </asp:TableCell>
                        <asp:TableCell>
                                <p style="width:120px">图书封面</p>
                        </asp:TableCell>
                        <asp:TableCell>
                                <p style="width:500px;padding:40px" class="overline">图书描述</ p>
                        </asp:TableCell>
                        <asp:TableCell>
                                <p style="width:200px;padding:10px" class="overline">图书作者</ p>
                        </asp:TableCell>
                        <asp:TableCell>
                                <p style="width:100px;padding:10px" class="overline">图书馆藏地</ p>
                        </asp:TableCell>
                        <asp:TableCell>
                                <p style="width:100px;padding:10px" class="overline">图书索书号</ p>
                        </asp:TableCell>
                    </asp:TableHeaderRow>
                </asp:Table>
            </div>

        </div>
    </form>
</body>
</html>
