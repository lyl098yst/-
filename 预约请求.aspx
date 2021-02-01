<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="预约请求.aspx.cs" Inherits="WebApplication1.预约请求" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>        
            &nbsp;&nbsp;&nbsp;
            <asp:TextBox name="reserve" ID="isbn" runat="server" placeholder="请输入编号进行查找" Height="39px" Width="249px" />
            &nbsp;&nbsp;
            <asp:Button ID="search" class="search-btn" runat="server" Text="搜索" OnClick="Search" BackColor="#FF9933" ForeColor="Black" Height="43px" Width="82px" />

            &nbsp;
            <asp:Button ID="homePage" class="search-btn" runat="server" Text="首页" OnClick="back" BackColor="#FF9933" ForeColor="Black" Height="43px" Width="82px" />

            <asp:GridView ID="reserve" runat="server" Height="236px" Width="1347px" HorizontalAlign="Center" OnSelectedIndexChanged="role_SelectedIndexChanged" AllowPaging="True"  OnPageIndexChanging="reserve_PageIndexChanging" AutoGenerateColumns="False" PageSize="4">
                <HeaderStyle BackColor="#CCFFFF" Font-Bold="True" Font-Size="X-Large" Height="50px" />
                <RowStyle BackColor="#CCFFFF" BorderColor="#99FF99" Font-Bold="True" Font-Overline="False" Font-Size="Larger" ForeColor="Black" Height="50px" HorizontalAlign="Center" />

                <AlternatingRowStyle BackColor="#CCFFFF" />           
                <Columns>   
                    <asp:BoundField DataField="num" HeaderText="学号" ReadOnly="True" />
                    <asp:BoundField DataField="ISBN" HeaderText="ISBN" ReadOnly="True" />
                    <asp:BoundField DataField="book_name" HeaderText="书名" ReadOnly="True" />
                    <asp:BoundField DataField="author" HeaderText="作者" ReadOnly="True" />
                    <asp:BoundField DataField="time" HeaderText="预约时间" ReadOnly="True" />
                    <asp:BoundField DataField="library" HeaderText="馆藏地点" ReadOnly="True" />
                    <asp:BoundField DataField="status" HeaderText="预约状态" ReadOnly="True" />                    
                    <asp:BoundField DataField="which" HeaderText="书序号" ReadOnly="True" />
                    
                    
                    <asp:ImageField ShowHeader="True" DataImageUrlField="url" HeaderText="图">
                        <ControlStyle Height="130px" Width="130px" />
                    </asp:ImageField>
                    <asp:CommandField  ShowSelectButton="True" SelectText="取消预约" />                    
                </Columns>

            </asp:GridView>
            
        </div>
    </form>
</body>
</html>
