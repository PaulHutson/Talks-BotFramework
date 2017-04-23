<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="BotFramework_WebTest.index" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Application Running
        </div>
        <div>
            <asp:Button runat="server" ID="btn_PingEVERYONE" Text="Ping all connected clients" OnClick="btn_PingEVERYONE_Click" />
        </div>
    </form>
</body>
</html>
