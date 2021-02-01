<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BookDetail.aspx.cs" Inherits="WebApplication1.BookDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>修改图书信息</title>
    
</head>
<body>
    <form id="form1" runat="server" style=" text-align: right; padding-left: 500px;">
        &nbsp;&nbsp;&nbsp;

        <asp:Table runat="server" Height="600px" Width="600px">

            <asp:TableRow>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell HorizontalAlign="left">
                    <asp:Image ID="pic" runat="server" Height="130" Width="100px" />
                </asp:TableCell>
                <asp:TableCell>
                    
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow HorizontalAlign="Right">
                <asp:TableCell>
                    <asp:Label ID="Label1" runat="server" Text="ISBN号"></asp:Label>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="left">
                    <asp:TextBox disabled="disabled" width="250px"  ID="ISBN" runat="server"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow HorizontalAlign="Right">
                <asp:TableCell>
                    <asp:Label ID="Label3"  runat="server" Text="作者"></asp:Label>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="left">
                    <asp:TextBox ID="author" Width="250px" runat="server"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow HorizontalAlign="Right">
                <asp:TableCell>
                    <asp:Label ID="Label2" runat="server" Text="书名"></asp:Label>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="left">
                    <asp:TextBox ID="book_name" Width="250px"  runat="server"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow HorizontalAlign="Right">
                <asp:TableCell>
                    <asp:Label ID="Label4" runat="server" Text="描述"></asp:Label>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="left">
                    <asp:TextBox ID="description" Columns="50" TextMode="MultiLine" style="width:370px;word-wrap:break-word;height:100px;word-break:break-all" runat="server"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow HorizontalAlign="Right">
                <asp:TableCell>
                    <asp:Label ID="Label8" runat="server" Text="语言"></asp:Label>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="left">
                    <asp:TextBox ID="language" Width="250px"  runat="server"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow HorizontalAlign="Right">
                <asp:TableCell>
                    <asp:Label ID="Label9" runat="server" Text="出版社"></asp:Label>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="left">
                    <asp:TextBox ID="publisher" Width="250px"  runat="server"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow HorizontalAlign="Right">
                <asp:TableCell>
                    <asp:Label ID="Label10" runat="server" Text="出版日期"></asp:Label>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="left">
                    <asp:TextBox ID="date"  Width="250px" runat="server"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow HorizontalAlign="Right">
                <asp:TableCell>
                    <asp:Label ID="Label5" runat="server" Text="剩余量"></asp:Label>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="left">
                    <asp:TextBox ID="remain" Width="250px" disabled="disabled"   runat="server"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow HorizontalAlign="Right">
                <asp:TableCell>
                    <asp:Label ID="Label6" runat="server" Text="类型"></asp:Label>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="left">
                    <asp:TextBox ID="type"  Width="250px" runat="server"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow HorizontalAlign="Right">
                <asp:TableCell>
                    <asp:Label ID="Label7" runat="server" Text="价格"></asp:Label>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="left">
                    <asp:TextBox ID="price" Width="250px"  runat="server"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow HorizontalAlign="Right">
                <asp:TableCell>
                    <asp:Label ID="Label11" runat="server" Text="馆藏地"></asp:Label>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="left">
                    <asp:TextBox ID="library"  Width="250px" runat="server"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow HorizontalAlign="Right">
                <asp:TableCell>
                    <asp:Label ID="Label12" runat="server" Text="楼层"></asp:Label>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="left">
                    <asp:TextBox ID="place"  Width="250px" runat="server"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow HorizontalAlign="Right">
                <asp:TableCell>
                    <asp:Label ID="Label13" runat="server" Text="索书号"></asp:Label>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="left">
                    <asp:TextBox ID="label"  Width="250px" runat="server"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow HorizontalAlign="Right">
                <asp:TableCell>
                   <asp:Button runat="server" OnClick="update" Text="保存"/>
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Right">
                    <asp:Button runat="server" OnClick="delete" Text="删除"/>
                </asp:TableCell>
                <asp:TableCell></asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:Button ID="Button1" runat="server" Text="返回" OnClick="Exit" />
    </form>
</body>
</html>


