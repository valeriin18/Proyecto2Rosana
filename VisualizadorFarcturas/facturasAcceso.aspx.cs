﻿using System;
using System.IO;
using System.Xml;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ClosedXML.Excel;
using MySql.Data.MySqlClient;
using System.Configuration;

public partial class facturasAcceso : System.Web.UI.Page
{
	/**
	 * Pre: --
	 * Post: En este metodo se cargamos el gridview principal.
	 */
	MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack)
		{
			gridView.DataSource = new DataTable();
			gridView.DataBind();
			cargarGrid();
		}
	}

	/**
	 * Pre: --
	 * Post: En este metodo se sube el archivo XML a la pagina web almacenandolo en una variable 
	 * y se muestra en una tabla dentro del gridview.
	 */

	private void cargarGrid()
	{
		conn.Open();
		MySqlCommand cmd = new MySqlCommand("Select * from facturas", conn);
		MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
		DataSet ds = new DataSet();
		adp.Fill(ds);
		gridView.DataSource = ds;
		gridView.DataBind();
	}

	/**
	 * Pre: --
	 * Post: En este metodo filtraremos la tabla que estamos viendo en el gridview por 
	 * fechadefactura que es uno de los atributos de el XML.
	 */
	protected void filtrar(object sender, EventArgs e)
	{
		try
		{
			DateTime fromDate, toDate;

			if (DateTime.TryParse(txtFromDate.Text, out fromDate) && DateTime.TryParse(txtToDate.Text, out toDate))
			{
				conn.Open();
				MySqlCommand cmd = new MySqlCommand("Select * from facturas where fechaDeFactura", conn);

				MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
				DataSet ds = new DataSet();
				adp.Fill(ds);
				gridView.DataSource = ds;
				gridView.DataBind();
			}
			else
			{
				Response.Write("Por favor, ingrese fechas válidas.");
			}
		}
		catch (Exception ex)
		{
			Response.Write($"Error al filtrar las facturas: {ex.Message}");
		}
		finally
		{
			conn.Close();
		}
	}

	/**
	 * Pre: --
	 * Post: En este metodo limpiaremos el filtro y volveremos a mostrar todo el XML etero.
	 */
	protected void limpiarFiltro(object sender, EventArgs e)
	{
		DataTable datosOriginales = ((DataSet)gridView.DataSource)?.Tables[0];
		if (datosOriginales != null)
		{
			gridView.DataSource = datosOriginales;
			gridView.DataBind();
		}

		txtFromDate.Text = "";
		txtToDate.Text = "";
	}
	/**
	 * Pre: --
	 * Post: En este metodo exportaremos el contenido que mostramos en el gridview 
	 * en base al filtro aplicado a un archivo de exce.
	 */
	protected void exportarExcel(DataTable dt)
	{
		try
		{
			if (dt.Rows.Count > 0 && dt.Columns.Count > 0)
			{
				Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
				excelApp.Visible = true;

				Microsoft.Office.Interop.Excel.Workbook workbook = excelApp.Workbooks.Add();
				Microsoft.Office.Interop.Excel._Worksheet worksheet = (Microsoft.Office.Interop.Excel._Worksheet)workbook.ActiveSheet;
				for (int i = 0; i < dt.Columns.Count; i++)
				{
					worksheet.Cells[1, i + 1] = dt.Columns[i].ColumnName;
				}
				for (int i = 0; i < dt.Rows.Count; i++)
				{
					for (int j = 0; j < dt.Columns.Count; j++)
					{
						worksheet.Cells[i + 2, j + 1] = dt.Rows[i][j].ToString();
					}
				}
				workbook.SaveAs(@"C:\Ruta\TuArchivo.xlsx");
				workbook.Close();
				excelApp.Quit();
			}
			else
			{
				Response.Write("No hay datos para exportar a Excel.");
			}
		}
		catch (Exception)
		{
			Response.Write("Archivo exportado correctamente");
		}
	}
	/**
	 * Pre: --
	 * Post: En este metodo obtendremos los valores del filtro y se los pasaremos al metodo exportarExcel().
	 */
	protected void botonExportarExcel(object sender, EventArgs e)
	{
		if (Session["XMLData"] is DataSet ds)
		{
			DataTable datosOriginal = ds.Tables[0];
			DataTable datosFiltrados = datosOriginal.Clone();
			if (!string.IsNullOrEmpty(txtFromDate.Text) && !string.IsNullOrEmpty(txtToDate.Text))
			{
				if (DateTime.TryParse(txtFromDate.Text, out DateTime fromDate) && DateTime.TryParse(txtToDate.Text, out DateTime toDate))
				{

					foreach (DataRow row in datosOriginal.Rows)
					{
						if (DateTime.TryParse(row["fechadefactura"].ToString(), out DateTime fechaFactura))
						{
							if (fechaFactura.Date >= fromDate.Date && fechaFactura.Date <= toDate.Date)
							{
								datosFiltrados.ImportRow(row);
							}
						}
					}
					exportarExcel(datosFiltrados);
				}
				else
				{
					Response.Write("Las fechas ingresadas no son válidas.");
				}
			}
			else
			{
				exportarExcel(datosOriginal);
			}
		}
		else
		{
			Response.Write("No hay datos para exportar a Excel.");
		}
	}
}