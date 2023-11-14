<%@ Page Language="C#" AutoEventWireup="true" CodeFile="facturasAcceso.aspx.cs" Inherits="facturasAcceso" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="Microsoft.Office.Interop.Excel" %>
<!DOCTYPE html>
<link href="StyleSheet.css" rel="stylesheet" />
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Facturas</title>
</head>
<body>
	<div id="titulo"><h1>PROGRAMA GESTOR DE FACTURAS</h1></div>
	<form id="form1" runat="server" enctype="multipart/form-data">
		<asp:FileUpload ID="fileUpload" runat="server" />
		<asp:Button ID="btnLoad" runat="server" Text="Mostrar" OnClick="subirxml" />
		<div id="filtroContainer" runat="server" visible="false">
			<hr/>
			<asp:Label ID="lblFromDate" runat="server" Text="Desde:"></asp:Label>
			<asp:TextBox ID="txtFromDate" runat="server" TextMode="Date"></asp:TextBox>
			<asp:Label ID="lblToDate" runat="server" Text="Hasta:"></asp:Label>
			<asp:TextBox ID="txtToDate" runat="server" TextMode="Date"></asp:TextBox>
			<asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" OnClick="filtrar" />
			<asp:Button ID="btnLimpiar" runat="server" Text="Borrar Filtros" OnClick="limpiarFiltro" />
			<hr/>
		</div>
		<asp:GridView ID="gridView" runat="server" CssClass="styled-table" AutoGenerateColumns="true">
		</asp:GridView>
		<div ID="btnExportar" runat="server" visible="false">
			<hr/>
			<asp:Button runat="server" Text="Exportar a Excel" OnClick="botonExportarExcel"/>
		</div>
	</form>
</body>
</html>