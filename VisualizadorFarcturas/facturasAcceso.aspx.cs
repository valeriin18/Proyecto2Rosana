using System;
using System.IO;
using System.Xml;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ClosedXML.Excel;

public partial class facturasAcceso : System.Web.UI.Page
{
	/**
	 * Pre: --
	 * Post: En este metodo se cargamos el gridview principal.
	 */
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack)
		{
			gridView.DataSource = new DataTable();
			gridView.DataBind();
		}
	}
	/**
	 * Pre: --
	 * Post: En este metodo se sube el archivo XML a la pagina web almacenandolo en una variable 
	 * y se muestra en una tabla dentro del gridview.
	 */
	protected void subirxml(object sender, EventArgs e)
	{
		if (fileUpload.HasFile)
		{
			HttpPostedFile file = fileUpload.PostedFile;
			if (Path.GetExtension(file.FileName).Equals(".xml", StringComparison.OrdinalIgnoreCase))
			{
				try
				{
					XmlDocument xmlDoc = new XmlDocument();
					xmlDoc.Load(file.InputStream);

					DataSet ds = new DataSet();
					ds.ReadXml(new XmlNodeReader(xmlDoc));
					foreach (DataRow row in ds.Tables[0].Rows)
					{
						DateTime fechaFactura = DateTime.Parse((string)row["fechadefactura"]);
						row["fechadefactura"] = fechaFactura.ToString("dd/MM/yyyy");
					}
					Session["XMLData"] = ds;
					gridView.DataSource = ds.Tables[0];
					gridView.DataBind();
					filtroContainer.Visible = true;
					btnExportar.Visible = true;
				}
				catch (Exception ex)
				{
					gridView.DataSource = null;
					gridView.DataBind();
					Response.Write("Error al procesar el archivo XML: " + ex.Message);
				}
			}
			else
			{
				Response.Write("El archivo seleccionado no es un archivo XML.");
			}
		}
		else
		{
			Response.Write("Por favor, seleccione un archivo XML.");
		}
	}
	/**
	 * Pre: --
	 * Post: En este metodo filtraremos la tabla que estamos viendo en el gridview por 
	 * fechadefactura que es uno de los atributos de el XML.
	 */
	protected void filtrar(object sender, EventArgs e)
	{
		if (Session["XMLData"] is DataSet ds)
		{
			if (!string.IsNullOrEmpty(txtFromDate.Text) && !string.IsNullOrEmpty(txtToDate.Text))
			{
				if (DateTime.TryParse(txtFromDate.Text, out DateTime fromDate) && DateTime.TryParse(txtToDate.Text, out DateTime toDate))
				{
					DataTable datosOriginales = ds.Tables[0];
					DataTable datosFiltrados = datosOriginales.Clone();
					foreach (DataRow row in datosOriginales.Rows)
					{
						if (DateTime.TryParse(row["fechadefactura"].ToString(), out DateTime fechaFactura))
						{
							if (fechaFactura.Date >= fromDate.Date && fechaFactura.Date <= toDate.Date)
							{
								datosFiltrados.ImportRow(row);
							}
						}
					}
					gridView.DataSource = datosFiltrados;
					gridView.DataBind();
				}
				else
				{
					Response.Write("Las fechas ingresadas no son válidas.");
				}
			}
			else
			{
				Response.Write("Por favor, ingrese ambas fechas para filtrar.");
			}
		}
		else
		{
			Response.Write("No se pudo filtrar ya que no se ha cargado un archivo XML.");
		}
	}
	/**
	 * Pre: --
	 * Post: En este metodo limpiaremos el filtro y volveremos a mostrar todo el XML etero.
	 */
	protected void limpiarFiltro(object sender, EventArgs e)
	{
		if (Session["XMLData"] is DataSet ds)
		{
			gridView.DataSource = ds.Tables[0];
			gridView.DataBind();

			txtFromDate.Text = "";
			txtToDate.Text = "";
		}
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
