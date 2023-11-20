﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="facturasAcceso.aspx.cs" Inherits="facturasAcceso" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="Microsoft.Office.Interop.Excel" %>
<!DOCTYPE html>
<link href="StyleSheet.css" rel="stylesheet" />
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Facturas</title>
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous">
</head>
<body class="container mt-4">
    <div id="titulo" class="mb-4">
        <h1>PROGRAMA GESTOR DE FACTURAS</h1>
    </div>
    <form id="form1" runat="server" enctype="multipart/form-data">
        <div id="filtroContainer" runat="server" class="mb-4">
            <div class="btn-group mb-2">
                <asp:Button runat="server" Text="Añadir Datos" OnClick="botonAnadirDatos" CssClass="btn btn-primary" />
            </div>
            <hr />
            <div class="form-row align-items-center">
                <div class="col-md-2">
                    <asp:Label ID="lblFromDate" runat="server" Text="Desde:"></asp:Label>
                    <asp:TextBox ID="txtFromDate" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="lblToDate" runat="server" Text="Hasta:"></asp:Label>
                    <asp:TextBox ID="txtToDate" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" OnClick="filtrar" CssClass="btn btn-info btn-block mt-2" />
                </div>
                <div class="col-md-2">
                    <asp:Button ID="btnLimpiar" runat="server" Text="Borrar Filtros" OnClick="limpiarFiltro" CssClass="btn btn-light btn-block mt-2" />
                </div>
            </div>
            <hr />
        </div>
        <asp:GridView ID="gridView" runat="server" DataKeyNames="idFactura" CssClass="table table-bordered table-striped"
            AutoGenerateEditButton="True" OnRowEditing="gridView_RowEditing"
            OnRowUpdating="gridView_RowUpdating" OnRowCancelingEdit="gridView_RowCancelingEdit" >
        </asp:GridView>

        <div ID="btnExportar" runat="server" class="mt-4">
            <hr />
            <asp:Button runat="server" Text="Exportar a Excel" OnClick="botonExportarExcel" CssClass="btn btn-success" />
        </div>
    </form>
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js" integrity="sha384-ApNbgh9B+Y1QKtv3Rn7W3mgPxhU9K/ScQsAP7hUibX39j7fakFPskvXusvfa0b4Q" crossorigin="anonymous"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>
</body>
</html>
