<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="修改用户角色.aspx.cs" Inherits="WebApplication1.ChangeRole" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" href="CSS/nav.css" />
</head>
<body>
    <form id="form1" runat="server">
      
        <div class="search">
            <asp:Label ID="Label1" runat="server" Font-Names="微软雅黑" Font-Size="XX-Large" Height="50px" Text="请点击选择切换对应账号的角色" Width="631px"></asp:Label>
            <br />
            <br />
            &nbsp;&nbsp;&nbsp;
            <asp:TextBox name="company" ID="userId" runat="server" placeholder="请输入编号进行查找" Height="39px" Width="249px" />
            &nbsp;&nbsp;
            <asp:Button ID="search" class="search-btn" runat="server" Text="搜索" OnClick="Search" BackColor="#FF9933" ForeColor="Black" Height="43px" Width="82px" />

            <asp:Button ID="search0" class="search-btn" runat="server" Text="首页" OnClick="back" BackColor="#FF9933" ForeColor="Black" Height="43px" Width="82px" />

            <br />
            <br />

            <asp:GridView ID="role" runat="server" Height="236px"  Width="1347px" HorizontalAlign="Center" 
                OnSelectedIndexChanged="role_SelectedIndexChanged"
                OnPageIndexChanging= "role_PageIndexChanging" AllowPaging="True" ClientIDMode="Static" PageSize="5" AutoGenerateColumns="False">
                <HeaderStyle BackColor="#CCFFFF" Font-Bold="True" Font-Size="X-Large" Height="50px" />
                <RowStyle BackColor="#CCFFFF" BorderColor="#99FF99" Font-Bold="True" Font-Overline="False" Font-Size="Larger" 
                    ForeColor="Black" Height="50px" HorizontalAlign="Center" />
                <AlternatingRowStyle BackColor="#CCFFFF" />
                <Columns>
                    <asp:BoundField DataField="num" HeaderText="账号" ReadOnly="True" />
                    <asp:BoundField DataField="name" HeaderText="用户名" ReadOnly="True" />
                    <asp:BoundField DataField="email" HeaderText="邮箱" ReadOnly="True" />
                    <asp:BoundField DataField="phone" HeaderText="手机" ReadOnly="True" />
                    <asp:BoundField DataField="roleid" HeaderText="角色" ReadOnly="True" />

                    <asp:CommandField ShowSelectButton="True"  SelectText="修改角色"/>
                </Columns>
            </asp:GridView>

        </div>
       

    </form>
</body>
</html>
