<%@ Page Title="Приложение" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="module.aspx.cs" Inherits="modue_integration.module" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
     <script type="text/javascript" language="javascript">  
     </script>
    
    <div class="jumbotron">
        <h1><img src="Files/logo.png" width="80" height="80" align="middle" /> Приступим к соединению?</h1>
    </div>
    <div class="row">
        <div>
        <div class="col-md-4">
            <h2>Шаг 1</h2>
            <p>
                Для работы необходимо выбрать файл <*.is2>.
            </p>
            <p>
                <asp:FileUpload ID="FileUpload1" CssClass="panel panel-default" runat="server" accept=".is2"/> 
                <asp:TextBox ID="TextBox3" runat="server" Width="304px" CssClass="btn btn-default.disabled" Enabled="False" OnTextChanged="TextBox3_TextChanged"></asp:TextBox>          
            </p>
        </div>
        <div class="col-md-4">
            <h2>Шаг 2</h2>
                <p>
                    После выбора файла загрузите исходную диаграмму, кликнув на кнопку
                </p>
                <p>
                    <asp:Button ID="Button1" runat="server" Text="Загрузить исходную диаграмму" CssClass="btn btn-primary" OnClick="Button1_Click" Width="304px" /> 
                    <asp:TextBox ID="TextBox1" runat="server" Width="304px" CssClass="btn btn-default.disabled" Enabled="False" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>                            
                </p>
        </div>
        <div class="col-md-4">
            <h2>Шаг 3</h2>
                <p>
                    Снова выберите файл и загрузите импортируемую диаграмму
                </p>
                <p>                                
                    <asp:Button ID="Button2" runat="server" Text="Загрузить импортируемую диаграмму" CssClass="btn btn-primary" OnClick="Button2_Click" onclientclick="Button2_Click" Width="279px" />
                    <asp:TextBox ID="TextBox2" runat="server" Width="304px" CssClass="btn btn-default.disabled" Enabled="False" OnTextChanged="TextBox2_TextChanged"></asp:TextBox>            
                </p>
        </div>
        <div class="col-md-4">
            <h2>Шаг 4</h2>
                <p>
                    Выберите элемент, к которой присоединятся элементы интегрируемой диаграммы
                </p>
                <p>
                    <asp:DropDownList Visible="false" ID="DropDownList1" runat="server" CssClass="thumbnail navbar-default"></asp:DropDownList>                              
                    <asp:Button Visible="false" ID="Button4" runat="server" Text="Выбрать" CssClass="btn btn-primary" OnClick="Button4_Click" onclientclick="Button2_Click" Width="279px" /> 
                </p>
        </div>
        </div>
        <br />
        <asp:Button Visible="true" ID="Button3" runat="server" Text="Проверка данных" CssClass="btn btn-primary" OnClick="Button3_Click" Width="279px" />
        <asp:TextBox Visible="false" ID="TextBox4" runat="server" Width="304px" CssClass="btn btn-default.disabled" Enabled="False" OnTextChanged="TextBox4_TextChanged"></asp:TextBox>
        <asp:HiddenField runat="server" ID="hfNameCurrent" ClientIDMode="Static" />
        <asp:HiddenField runat="server" ID="hfNameIntegration" ClientIDMode="Static" />
        <asp:HiddenField runat="server" ID="hfDiagrammCurrent" ClientIDMode="Static" />
        <asp:HiddenField runat="server" ID="hfDiagrammIntegration" ClientIDMode="Static" />
        <asp:HiddenField runat="server" ID="pointCurrentError" ClientIDMode="Static"/>
        <asp:HiddenField runat="server" ID="pointIntegrationError" ClientIDMode="Static"/>
        <asp:HiddenField runat="server" ID="countCurElement" ClientIDMode="Static" />
        <asp:HiddenField runat="server" ID="countIntgrElement" ClientIDMode="Static" />
        <asp:HiddenField runat="server" ID="countResultElement" ClientIDMode="Static" />
        <asp:HiddenField runat="server" ID="countIntgrLinks" ClientIDMode="Static" />      
        <asp:HiddenField runat="server" ID="countCurrentLinks" ClientIDMode="Static" />
    </div>
</asp:Content>





