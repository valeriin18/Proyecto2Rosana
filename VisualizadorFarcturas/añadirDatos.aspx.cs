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
            string idFactura = txtIdFactura.Text;
            string numFactura = txtNumFactura.Text;
            DateTime fechaFactura = DateTime.Parse(txtFechaFactura.Text);
            string cifCliente = txtCifCliente.Text;
            string nombreApellidos = txtNombreApellidos.Text;
            decimal importe = decimal.Parse(txtImporte.Text);
            decimal importeIVA = decimal.Parse(txtImporteIVA.Text);
            string moneda = txtMoneda.Text;
            DateTime fechaCobro = DateTime.Parse(txtFechaCobro.Text);
            string metodoDePago = txtMetodoDePago.Text;
            string estadoFactura = txtEstadoFactura.Text;
            MySqlCommand cmd = new MySqlCommand("INSERT INTO facturas (IdFactura, NumFactura, FechaDeFactura, CIFCliente, NombreApellidos, Importe, ImporteIVA, Moneda, FechaCobro, MetodoDePago, EstadoFactura) VALUES (@IdFactura, @NumFactura, @FechaDeFactura, @CIFCliente, @NombreApellidos, @Importe, @ImporteIVA, @Moneda, @FechaCobro, @MetodoDePago, @EstadoFactura)", conn);
            cmd.Parameters.AddWithValue("@IdFactura", idFactura);
            cmd.Parameters.AddWithValue("@NumFactura", numFactura);
            cmd.Parameters.AddWithValue("@FechaDeFactura", fechaFactura);
            cmd.Parameters.AddWithValue("@CIFCliente", cifCliente);
            cmd.Parameters.AddWithValue("@NombreApellidos", nombreApellidos);
            cmd.Parameters.AddWithValue("@Importe", importe);
            cmd.Parameters.AddWithValue("@ImporteIVA", importeIVA);
            cmd.Parameters.AddWithValue("@Moneda", moneda);
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
