<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cookies.aspx.cs" Inherits="modue_integration.Cookies" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="TextBox1" runat="server" Width="304px" CssClass="btn btn-default.disabled" Enabled="False" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>  
           <asp:Button ID="Button1" runat="server" Text="Загрузить исходную диаграмму" CssClass="btn btn-primary" OnClick="Button1_Click" Width="304px" />
            <asp:FileUpload ID="FileUpload1" runat="server" />
            <asp:HiddenField runat="server" ID="hfNameCurrent" ClientIDMode="Static" />
        </div>
    </form>
</body>
</html>
