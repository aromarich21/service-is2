<%@ Page Title="Ошибка" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="modue_integration.Error" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="jumbotron">
        <h1><img src="Files/logo.png" width="80" height="80" align="middle" /> Упс, что-то пошло не так...</h1>    
         <p class="lead">
        К сожалению, возникла проблема. Пожалуйста, попробуйте еще раз.
    <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick"></asp:Timer>
    </p>
    </div>
    <p>&nbsp;</p>
</asp:Content>
