using System;
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
			Session["DatosOriginales"] = null;
			gridView.DataSource = new DataTable();
			gridView.DataBind();
			cargarGrid();
		}
	}

	/**
	 * Pre: --
	 * Post: En este metodo se hace una select de todo el contenido de la base de datos con la que 
	 * se ha realizado la conexion y se carga en el gridview.
	 */
	private void cargarGrid()
	{
		conn.Open();
		MySqlCommand cmd = new MySqlCommand("Select idFactura as Num, fechaDeFactura as 'F. Factura', cifCliente as CIF, NombreApellidos as Cliente, importe as Importe, importeIVA as '+IVA', moneda as 'Moneda', fechaCobro as 'F. Cobro', metodoDePago as 'Método pago', estadoFactura as Estado from facturas", conn);
		MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
		DataSet ds = new DataSet();
		adp.Fill(ds);
		// Modificar formato de los datos en el DataSet
		foreach (DataRow row in ds.Tables[0].Rows)
		{
			for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
			{
				// Alineación y formato para números
				if (ds.Tables[0].Columns[i].DataType == typeof(decimal) ||
					ds.Tables[0].Columns[i].DataType == typeof(double) ||
					ds.Tables[0].Columns[i].DataType == typeof(float))
				{
					if (decimal.TryParse(row[i].ToString(), out decimal number))
					{
						row[i] = number.ToString("#,##0.00");
					}
				}
				// Alineación y formato para DateTime
				else if (ds.Tables[0].Columns[i].DataType == typeof(DateTime))
				{
					if (DateTime.TryParse(row[i].ToString(), out DateTime date))
					{
						row[i] = date.ToString("dd/MM/yyyy HH:mm");
					}
				}
				// Alineación y formato para strings
				else if (ds.Tables[0].Columns[i].DataType == typeof(string))
				{
					row[i] = row[i].ToString().ToLower();
				}

			}
		}

		gridView.DataSource = ds;
		gridView.DataBind();
		Session["DatosOriginales"] = ds.Tables[0];
		conn.Close();
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
				MySqlCommand cmd = new MySqlCommand("Select idFactura as Num, fechaDeFactura as 'F. Factura', cifCliente as CIF, NombreApellidos as Cliente, importe as Importe, importeIVA as '+IVA', moneda as 'Moneda', fechaCobro as 'F. Cobro', metodoDePago as 'Método pago', estadoFactura as Estado from facturas where fechaDeFactura BETWEEN @fromDate AND @toDate", conn);
				cmd.Parameters.AddWithValue("@fromDate", fromDate);
				cmd.Parameters.AddWithValue("@toDate", toDate);
				MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
				DataSet ds = new DataSet();
				adp.Fill(ds);
				// Modificar formato de los datos en el DataSet
				foreach (DataRow row in ds.Tables[0].Rows)
				{
					for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
					{
						// Alineación y formato para números
						if (ds.Tables[0].Columns[i].DataType == typeof(decimal) ||
							ds.Tables[0].Columns[i].DataType == typeof(double) ||
							ds.Tables[0].Columns[i].DataType == typeof(float))
						{
							if (decimal.TryParse(row[i].ToString(), out decimal number))
							{
								row[i] = number.ToString("#,##0.00");
							}
						}
						// Alineación y formato para strings
						else if (ds.Tables[0].Columns[i].DataType == typeof(string))
						{
							row[i] = row[i].ToString().ToLower();
						}
					}
				}
				gridView.DataSource = ds;
				gridView.DataBind();
				Session["DatosOriginales"] = ds.Tables[0];
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
		DataTable datosOriginales = Session["DatosOriginales"] as DataTable;
		if (datosOriginales != null)
		{
			gridView.DataSource = datosOriginales;
			gridView.DataBind();
		}

		txtFromDate.Text = "";
		txtToDate.Text = "";
		cargarGrid();
	}

    /**
	 * Pre: --
	 * Post: Este método permite editar las facturas en el gridview
	 */
    protected void gridView_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gridView.EditIndex = e.NewEditIndex;
        cargarGrid();
    }

	/**
	 * Pre: --
	 * Post: Este método permite personalizar ciertas partes del gridview
	 */
	protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			string estado = DataBinder.Eval(e.Row.DataItem, "Estado").ToString();

			// Modifica el estilo dependiendo del valor de "Estado"
			if (estado == "0")
			{
				e.Row.Cells[10].Text = "Pendiente";
				e.Row.Cells[10].CssClass = "estado-pendiente";

				LinkButton btnEditar = e.Row.Cells[0].Controls[0] as LinkButton;

				if (btnEditar != null)
				{
					btnEditar.Text = "Pagar";
				}
			}
			else if (estado == "1")
			{
				e.Row.Cells[10].Text = "Pagada";
				e.Row.Cells[10].CssClass = "estado-pagada";

				// Oculta el botón de edición si el estado es "Pendiente"
				LinkButton btnEditar = e.Row.Cells[0].Controls[0] as LinkButton;

				if (btnEditar != null)
				{
					btnEditar.Visible = false;
					btnEditar.Text = "Pagar";
				}

			}
		}
	}

	/**
	 * Pre: --
	 * Post: Usamos este método para actualizar las facturas cuando queremos pasarlas de pendientes a pagadas
	 */
	protected void gridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
	{
		try
		{
			// Obtener el ID de la fila que se está actualizando desde DataKeys
			int idFactura = Convert.ToInt32(gridView.DataKeys[e.RowIndex].Value);

			string nuevoFechaDeFactura = e.NewValues["F. Factura"]?.ToString();
			string nuevoCifCliente = e.NewValues["CIF"]?.ToString();
			string nuevoNombreApellidos = e.NewValues["Cliente"]?.ToString();
			decimal nuevoImporte = Convert.ToDecimal(e.NewValues["Importe"]);
			decimal nuevoImporteIVA = Convert.ToDecimal(e.NewValues["+IVA"]);
			string nuevaMoneda = e.NewValues["Moneda"]?.ToString();
			string nuevaFechaCobro = e.NewValues["F. Cobro"]?.ToString();
			string nuevoMetodoDePago = e.NewValues["Método pago"]?.ToString();

			// Aquí debes escribir la lógica para actualizar la base de datos
			conn.Open();
			MySqlCommand cmd = new MySqlCommand("UPDATE facturas SET " +
				"fechaDeFactura = str_to_date(@nuevoFechaDeFactura, '%m/%d/%Y %H:%i:%s'), " +
				"cifCliente = @nuevoCifCliente, " +
				"NombreApellidos = @nuevoNombreApellidos, " +
				"importe = @nuevoImporte, " +
				"importeIVA = @nuevoImporteIVA, " +
				"moneda = @nuevaMoneda, " +
				"fechaCobro = NOW(), " +
				"metodoDePago = @nuevoMetodoDePago, " +
				"estadoFactura = 1 " +
				"WHERE idFactura = @idFactura", conn);

			cmd.Parameters.AddWithValue("@NuevoFechaDeFactura", nuevoFechaDeFactura);
			cmd.Parameters.AddWithValue("@NuevoCifCliente", nuevoCifCliente);
			cmd.Parameters.AddWithValue("@NuevoNombreApellidos", nuevoNombreApellidos);
			cmd.Parameters.AddWithValue("@NuevoImporte", nuevoImporte);
			cmd.Parameters.AddWithValue("@NuevoImporteIVA", nuevoImporteIVA);
			cmd.Parameters.AddWithValue("@NuevaMoneda", nuevaMoneda);
			cmd.Parameters.AddWithValue("@NuevaFechaCobro", nuevaFechaCobro);
			cmd.Parameters.AddWithValue("@NuevoMetodoDePago", nuevoMetodoDePago);
			cmd.Parameters.AddWithValue("@IdFactura", idFactura);

			cmd.ExecuteNonQuery();
			conn.Close();

			gridView.EditIndex = -1;
			cargarGrid();
		}
		catch (Exception ex)
		{
			Response.Write($"Error al actualizar la fila: {ex.Message}");
		}
		finally
		{
			conn.Close();
		}
	}

	/**
	 * Pre: --
	 * Post: Este método nos permite eliminar facturas
	 */
	protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
	{
		try
		{
			int idFactura = Convert.ToInt32(gridView.DataKeys[e.RowIndex].Value);

			conn.Open();
			MySqlCommand cmd = new MySqlCommand("DELETE FROM facturas WHERE idFactura = @idFactura", conn);
			cmd.Parameters.AddWithValue("@IdFactura", idFactura);
			cmd.ExecuteNonQuery();
			conn.Close();

			cargarGrid();
		}
		catch (Exception ex)
		{
			Response.Write($"Error al eliminar la fila: {ex.Message}");
		}
	}

	/**
	 * Pre: --
	 * Post: Este método cancela la edición de una factura
	 */
	protected void gridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
	{
		gridView.EditIndex = -1;
		cargarGrid();
	}

	/**
	 * Pre: --
	 * Post: Este método nos lleva a la pantalla donde añadimos nuevas facturas
	 */
	protected void botonAnadirDatos(object sender, EventArgs e)
	{
		Response.Redirect("añadirDatos.aspx");
	}
}
