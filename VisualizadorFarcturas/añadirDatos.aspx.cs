using System;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;

public partial class añadirDatos : System.Web.UI.Page
{
    MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btnAnadirDatos_Click(object sender, EventArgs e)
    {
        try
        {
            conn.Open();
            DateTime fechaFactura = DateTime.Parse(txtFechaFactura.Text);
            string cifCliente = txtCifCliente.Text;
            string nombreApellidos = txtNombreApellidos.Text;
            decimal importe = decimal.Parse(txtImporte.Text);
            decimal importeIVA = decimal.Parse(txtImporteIVA.Text);
            string moneda = txtMoneda.Text;
            string metodoDePago = txtMetodoDePago.Text;
            bool estadoFactura = txtEstadoFactura.Checked;
            MySqlCommand cmd = new MySqlCommand("INSERT INTO facturas (FechaDeFactura, CIFCliente, NombreApellidos, Importe, ImporteIVA, Moneda, FechaCobro, MetodoDePago, EstadoFactura) VALUES (@FechaDeFactura, @CIFCliente, @NombreApellidos, @Importe, @ImporteIVA, @Moneda, @FechaCobro, @MetodoDePago, @EstadoFactura)", conn);
            cmd.Parameters.AddWithValue("@FechaDeFactura", fechaFactura);
            cmd.Parameters.AddWithValue("@CIFCliente", cifCliente);
            cmd.Parameters.AddWithValue("@NombreApellidos", nombreApellidos);
            cmd.Parameters.AddWithValue("@Importe", importe);
            cmd.Parameters.AddWithValue("@ImporteIVA", importeIVA);
            cmd.Parameters.AddWithValue("@Moneda", moneda);
            DateTime? fechaCobro = estadoFactura ? DateTime.Now : (DateTime?)null;
            cmd.Parameters.AddWithValue("@FechaCobro", fechaCobro);
            cmd.Parameters.AddWithValue("@MetodoDePago", metodoDePago);
            cmd.Parameters.AddWithValue("@EstadoFactura", estadoFactura);
            cmd.ExecuteNonQuery();
            Response.Write("Datos añadidos correctamente a la base de datos.");
            Response.Redirect("facturasAcceso.aspx");
        }
        catch (Exception ex)
        {
            Response.Write($"Error al añadir datos: {ex.Message}");
        }
        finally
        {
            conn.Close();
        }
    }
}
