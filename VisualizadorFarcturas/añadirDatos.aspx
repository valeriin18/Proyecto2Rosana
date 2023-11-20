<%@ Page Language="C#" AutoEventWireup="true" CodeFile="añadirDatos.aspx.cs" Inherits="añadirDatos" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="Microsoft.Office.Interop.Excel" %>
<!DOCTYPE html>
<link href="StyleSheet.css" rel="stylesheet" />
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AñadirFacturas</title>
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous">
     <style>
        body {
            background-color: #f8f9fa; 
        }
        .card {
            margin-top: 20px;
             margin-left: 10px;
            margin-right: 10px; 
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1); 
            max-width: 800px;
            margin-left: auto;
            margin-right: auto;
        }
    </style>
</head>
<body>
    <div id="titulo" class="container">
        <h1>PROGRAMA GESTOR DE FACTURAS</h1>
    </div>
    <div class="card p-4">
        <form id="form1" runat="server" enctype="multipart/form-data" class="container m-0">
             <div class="row"><h3>NUEVA FACTURA</h3></div>
            <div id="formularioContainer" runat="server" class="row">
                <div class="col-md-6">
                    <div class="form-group">
                        <asp:Label runat="server">ID de Factura:</asp:Label>
                        <asp:TextBox ID="txtIdFactura" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server">Número de Factura:</asp:Label>
                        <asp:TextBox ID="txtNumFactura" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server">Fecha de Factura:</asp:Label>
                        <asp:TextBox ID="txtFechaFactura" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server">CIF Cliente:</asp:Label>
                        <asp:TextBox ID="txtCifCliente" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server">Nombre y Apellidos:</asp:Label>
                        <asp:TextBox ID="txtNombreApellidos" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <asp:Label runat="server">Importe con IVA:</asp:Label>
                        <asp:TextBox ID="txtImporteIVA" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                     <div class="form-group">
                        <asp:Label runat="server">Moneda:</asp:Label>
                        <asp:DropDownList ID="txtMoneda" runat="server" CssClass="form-control">
                            <asp:ListItem Text="€" Value="€" />
                            <asp:ListItem Text="$" Value="$" />
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server">Importe:</asp:Label>
                        <asp:TextBox ID="txtImporte" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server">Método de Pago:</asp:Label>
                        <asp:DropDownList ID="txtMetodoDePago" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Via Oficina Bancaria" Value="Via Oficina Bancaria" />
                            <asp:ListItem Text="Via Telefonica" Value="Via Telefonica" />
                            <asp:ListItem Text="Via Cajero Automatico" Value="Via Cajero Automatico" />
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server">Estado de Factura:</asp:Label>
                        <asp:CheckBox ID="txtEstadoFactura" runat="server" CssClass="form-control"></asp:CheckBox>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <asp:Button ID="btnAnadirDatos" runat="server" OnClick="btnAnadirDatos_Click" Text="Añadir Datos" CssClass="btn btn-primary" />
            </div>
        </form>
    </div>
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js" integrity="sha384-ApNbgh9B+Y1QKtv3Rn7W3mgPxhU9K/ScQsAP7hUibX39j7fakFPskvXusvfa0b4Q" crossorigin="anonymous"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>
</body>
</html>
