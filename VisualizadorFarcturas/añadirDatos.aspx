<%@ Page Language="C#" AutoEventWireup="true" CodeFile="añadirDatos.aspx.cs" Inherits="añadirDatos" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="Microsoft.Office.Interop.Excel" %>
<!DOCTYPE html>
<link href="StyleSheet.css" rel="stylesheet" />
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AñadirFacturas</title>
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous">
</head>
<body>
    <div id="titulo" class="container">
        <h1>PROGRAMA GESTOR DE FACTURAS</h1>
    </div>
    <form id="form1" runat="server" enctype="multipart/form-data" class="container">
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
                <div class="form-group">
                    <asp:Label runat="server">Importe:</asp:Label>
                    <asp:TextBox ID="txtImporte" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group">
                    <asp:Label runat="server">Importe con IVA:</asp:Label>
                    <asp:TextBox ID="txtImporteIVA" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label runat="server">Moneda:</asp:Label>
                    <asp:TextBox ID="txtMoneda" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label runat="server">Fecha de Cobro:</asp:Label>
                    <asp:TextBox ID="txtFechaCobro" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label runat="server">Método de Pago:</asp:Label>
                    <asp:TextBox ID="txtMetodoPago" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label runat="server">Estado de Factura:</asp:Label>
                    <asp:TextBox ID="txtEstadoFactura" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="form-group">
            <asp:Button ID="btnAnadirDatos" runat="server" Text="Añadir Datos" CssClass="btn btn-primary" />
        </div>
    </form>
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js" integrity="sha384-ApNbgh9B+Y1QKtv3Rn7W3mgPxhU9K/ScQsAP7hUibX39j7fakFPskvXusvfa0b4Q" crossorigin="anonymous"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>
</body>
</html>
