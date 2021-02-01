<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="文件列表.aspx.cs" Inherits="WebApplication1.上传文件" %>

<!DOCTYPE html >

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>上传文件</title>
</head>
<body>
    <form id="form1" runat="server">

        <div id="UpLoadDiv" runat="Server">
            <h3>文件上传</h3>
            <div>
                <div>
                    <asp:FileUpload ID="UpLoad" runat="server" />
                </div>

                <div>
                    <asp:Button runat="server" ID="button_upload" Text="上传" OnClick="UpLoad1"></asp:Button>
                </div>
            </div>
        </div>
        
        <asp:GridView ID="Files" runat="server" AllowPaging="True" Height="20px" Width="1426px"
            AutoGenerateColumns="False" OnSelectedIndexChanged="Files_SelectedIndexChanged">
            <Columns>
                <asp:BoundField DataField="name" HeaderText="文件名" ReadOnly="True" />
                <asp:BoundField DataField="filename" HeaderText="服务器文件名" ReadOnly="True" />
                <asp:BoundField DataField="num" HeaderText="发送者账号" ReadOnly="True" />
                <asp:BoundField DataField="sendName" HeaderText="发送者姓名" ReadOnly="True" />
                <asp:BoundField DataField="date" HeaderText="发送时间" ReadOnly="True" />
                <asp:CommandField ShowSelectButton="True" SelectText="下载" />
                <asp:CommandField ShowDeleteButton="true" DeleteText="删除" />
            </Columns>
            <HeaderStyle Height="20px" />
            <RowStyle Height="50px" />
        </asp:GridView>


    </form>
</body>
</html>
