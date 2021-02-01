<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="添加书籍.aspx.cs" Inherits="WebApplication1.添加书籍" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
             <asp:FileUpload ID="fileUpload" runat="server" />             <asp:Button ID="search" class="search-btn" runat="server" Text="上传" OnClick="upload" BackColor="#FF9933" ForeColor="Black" Height="43px" Width="82px" />

            <asp:Button ID="search0" class="search-btn" runat="server" Text="首页" OnClick="confirm" BackColor="#FF9933" ForeColor="Black" Height="43px" Width="82px" />

             <br />
        </div>
        <asp:GridView ID="addbook" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="addbook_SelectedIndexChanged">
                <Columns>   
                    <asp:BoundField DataField="ISBN" HeaderText="ISBN" ReadOnly="True" />
                    <asp:BoundField DataField="remain" HeaderText="剩余量" ReadOnly="True" />
                    <asp:BoundField DataField="name" HeaderText="书名" ReadOnly="True" />
                    <asp:BoundField DataField="author" HeaderText="作者" ReadOnly="True" />
                    <asp:BoundField DataField="description" HeaderText="描述" ReadOnly="True" />
                    <asp:BoundField DataField="language" HeaderText="语言" ReadOnly="True" />
                    <asp:BoundField DataField="publisher" HeaderText="出版社" ReadOnly="True" />
                    <asp:BoundField DataField="date" HeaderText="出版日期" ReadOnly="True" />
                    <asp:BoundField DataField="library" HeaderText="馆藏地" ReadOnly="True" />
                    <asp:BoundField DataField="place" HeaderText="楼层" ReadOnly="True" />
                    <asp:BoundField DataField="label" HeaderText="索书号" ReadOnly="True" />
                    <asp:BoundField DataField="type" HeaderText="类型" ReadOnly="True" />
                    <asp:BoundField DataField="price" HeaderText="价格" ReadOnly="True" />
                    <asp:CommandField ShowSelectButton="true" SelectText="查看该图书" />
                  
                </Columns>
        </asp:GridView>
    </form>
</body>
</html>
