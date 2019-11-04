<%@ Page Title="Авторизация" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="modue_integration.Login" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="/Content/MyCss.css" type="text/css" />
    <div class="jumbotron">
        <h1><img src="Files/developing.png" width="100" height="80" align="middle" />Приложение в разработке</h1>
    </div>

    <div class="row">       
         <h2>Авторизация</h2>
            <p>
                <p>Введите логин:</p>
                <asp:TextBox ID="TextBox1" runat="server" ToolTip="Введите логин" CssClass="panel panel-default"></asp:TextBox>
                <p>Введите пароль:</p>
                <asp:TextBox ID="TextBox2" TextMode="Password" runat="server" ToolTip="Введите пароль" CssClass="panel panel-default"></asp:TextBox>
                </br>
                <asp:Label ID="Label1" runat="server" Text="Ошибка авторизации!" Visible="false" ForeColor="Red"></asp:Label>
                </br>
                <asp:Button ID="Button1" runat="server" Text="Получить доступ" CssClass="btn btn-primary" OnClick="Button1_Click" />
            </p>
    </div>
</asp:Content>
