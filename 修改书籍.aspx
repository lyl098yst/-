<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="修改书籍.aspx.cs" Inherits="WebApplication1.修改书籍" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            
        </div>
        <asp:TextBox ID="isbn" runat="server"></asp:TextBox>
        <asp:Button ID="button1" runat="server" Text="搜索isbn" OnClick="detail" />
       
        <asp:Button ID="button2" runat="server" Text="首页" OnClick="back" />
       
    </form>
</body>
</html>
