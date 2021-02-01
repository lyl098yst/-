<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="借阅历史.aspx.cs" Inherits="WebApplication1.借阅历史" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="History" runat="server" AutoGenerateColumns="False" Height="378px" Width="1295px">
                <Columns>
                    <asp:BoundField DataField="ISBN" HeaderText="ISBN号" ReadOnly="True" />
                    <asp:BoundField DataField="book_name" HeaderText="书名" ReadOnly="True" />
                    <asp:BoundField DataField="author" HeaderText="作者" ReadOnly="True" />
                    <asp:BoundField DataField="library" HeaderText="图书馆" ReadOnly="True" />
                    <asp:BoundField DataField="borrow_date" HeaderText="借书日期" ReadOnly="True" />
                    <asp:BoundField DataField="expire_date" HeaderText="过期日期" ReadOnly="True" />                
                </Columns>
                <HeaderStyle Height="30px" />
                <RowStyle Height="30px" />
            </asp:GridView>
        </div>
    </form>
</body>
</html>
