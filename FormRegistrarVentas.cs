using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Base_de_Datos.Examen_Práctico_P3.GestionVentas._1_4_25
{
    public partial class FormRegistrarVentas : Form
    {
        private string connectionString = "Data Source=ENZOACER\\SQLEXPRESS;Initial Catalog=GestionVentas;Integrated Security=True;";

        private int? ventaID = null; // Para almacenar el ID de la venta después de registrarla

        public FormRegistrarVentas()
        {
            InitializeComponent();
            // **No deshabilitar la sección de detalles al cargar el formulario**
        }

        private void EnviarVentaBTN_Click(object sender, EventArgs e)
        {
            if (int.TryParse(IDclienteTXT.Text, out int idCliente) &&
                DateTime.TryParse(FechaVentaTP.Value.ToString(), out DateTime fechaVenta) &&
                MetodoPagoComboBox.SelectedValue != null && int.TryParse(MetodoPagoComboBox.SelectedValue.ToString(), out int idMetodoPago) &&
                !string.IsNullOrEmpty(EstadoPagoTXT.Text) &&
                !string.IsNullOrEmpty(NoFacturaTXT.Text) &&
                DateTime.TryParse(FechaEntregaTP.Value.ToString(), out DateTime fechaEntrega) &&
                DateTime.TryParse(FechaRetiroTP.Value.ToString(), out DateTime fechaRetiro) &&
                decimal.TryParse(TotalTXT.Text, out decimal total))
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand();
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "RegistrarVenta";

                        // Parámetros de la venta principal (ENTRADA)
                        command.Parameters.AddWithValue("@IDcliente", idCliente);
                        command.Parameters.AddWithValue("@FechaVenta", fechaVenta);
                        command.Parameters.AddWithValue("@IDmetodoPago", idMetodoPago);
                        command.Parameters.AddWithValue("@EstadoPago", EstadoPagoTXT.Text);
                        command.Parameters.AddWithValue("@NoFactura", NoFacturaTXT.Text);
                        command.Parameters.AddWithValue("@FechaEntrega", fechaEntrega);
                        command.Parameters.AddWithValue("@FechaRetiro", fechaRetiro);
                        command.Parameters.AddWithValue("@Total", total);

                        // **Ejecutar el procedimiento y obtener el resultado escalar (el ID de la venta)**
                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int ventaIdGenerada))
                        {
                            ventaID = ventaIdGenerada;
                            IDventaTXT.Text = ventaID.ToString(); // Mostrar el ID de la venta

                            MessageBox.Show($"Venta principal registrada exitosamente. ID de Venta: {ventaID}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Habilitar la sección de detalles y/o enfocar el primer campo
                            IDproductoTXT.Enabled = true;
                            CantidadTXT.Enabled = true;
                            PrecioUnitarioTXT.Enabled = true;
                            EnviarDetalleVentaBTN.Enabled = true;
                            IDproductoTXT.Focus();

                            // Deshabilitar la edición de la venta principal (opcional)
                            IDclienteTXT.Enabled = false;
                            FechaVentaTP.Enabled = false;
                            MetodoPagoComboBox.Enabled = false;
                            EstadoPagoTXT.Enabled = false;
                            NoFacturaTXT.Enabled = false;
                            FechaEntregaTP.Enabled = false;
                            FechaRetiroTP.Enabled = false;
                            TotalTXT.Enabled = false;
                            EnviarVentaBTN.Enabled = false;
                        }
                        else
                        {
                            MessageBox.Show("Error al obtener el ID de la venta registrada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            ventaID = null;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al registrar la venta principal: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ventaID = null; // Resetear el ID de venta en caso de error
                    }
                    finally
                    {
                        if (connection.State == ConnectionState.Open)
                        {
                            connection.Close();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, complete todos los campos de la venta principal correctamente.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void EnviarDetalleVentaBTN_Click_1(object sender, EventArgs e)
        {
            if (ventaID.HasValue)
            {
                if (int.TryParse(IDproductoTXT.Text, out int idProducto) &&
                    int.TryParse(CantidadTXT.Text, out int cantidad) &&
                    decimal.TryParse(PrecioUnitarioTXT.Text, out decimal precioUnitario))
                {
                    decimal subtotal = cantidad * precioUnitario;

                    string connectionString = this.connectionString;
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlTransaction transaction = null; // Para la transacción
                        try
                        {
                            connection.Open();
                            transaction = connection.BeginTransaction(); // Iniciar transacción
                            SqlCommand command = new SqlCommand("INSERT INTO DetalleVentas (IDventa, IDproducto, Cantidad, PrecioUnitario, Subtotal) VALUES (@IDventa, @IDproducto, @Cantidad, @PrecioUnitario, @Subtotal)", connection, transaction);
                            command.Parameters.AddWithValue("@IDventa", ventaID.Value);
                            command.Parameters.AddWithValue("@IDproducto", idProducto);
                            command.Parameters.AddWithValue("@Cantidad", cantidad);
                            command.Parameters.AddWithValue("@PrecioUnitario", precioUnitario);
                            command.Parameters.AddWithValue("@Subtotal", subtotal);

                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Detalle de venta agregado.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // Actualizar el total en la tabla Venta
                                SqlCommand updateCommand = new SqlCommand("UPDATE Ventas SET Total = (SELECT SUM(Subtotal) FROM DetalleVentas WHERE IDventa = @IDventa) WHERE IDventa = @IDventa", connection, transaction);
                                updateCommand.Parameters.AddWithValue("@IDventa", ventaID.Value);
                                updateCommand.ExecuteNonQuery();

                                transaction.Commit(); // Confirmar la transacción

                                // Limpiar campos del detalle para el siguiente producto
                                IDproductoTXT.Clear();
                                CantidadTXT.Clear();
                                PrecioUnitarioTXT.Clear();
                                SubtotalTXT.Clear(); // Si lo tienes y quieres limpiarlo
                                IDproductoTXT.Focus();

                                // **Opcional: Recargar el Total en el formulario**
                                CargarTotalVenta();
                            }
                            else
                            {
                                transaction.Rollback(); // Revertir si falla la inserción del detalle
                                MessageBox.Show("No se pudo agregar el detalle de venta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch (Exception ex)
                        {
                            if (transaction != null) transaction.Rollback(); // Revertir en caso de error
                            MessageBox.Show($"Error al agregar el detalle de venta: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            if (connection.State == ConnectionState.Open)
                            {
                                connection.Close();
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Por favor, ingrese valores válidos para el detalle del producto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Primero debe registrar la venta principal.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LimpiarFormulario()
        {
            ventaID = null;
            IDventaTXT.Clear();
            IDclienteTXT.Clear();
            FechaVentaTP.Value = DateTime.Now;
            MetodoPagoComboBox.SelectedIndex = -1; EstadoPagoTXT.Clear();
            NoFacturaTXT.Clear();
            FechaEntregaTP.Value = DateTime.Now;
            FechaRetiroTP.Value = DateTime.Now;
            TotalTXT.Clear();
            IDproductoTXT.Clear();
            CantidadTXT.Clear();
            PrecioUnitarioTXT.Clear();
            SubtotalTXT.Clear();

            // Habilitar la sección de venta principal y la de detalles al limpiar
            IDclienteTXT.Enabled = true;
            FechaVentaTP.Enabled = true;
            MetodoPagoComboBox.Enabled = true;
            EstadoPagoTXT.Enabled = true;
            NoFacturaTXT.Enabled = true;
            FechaEntregaTP.Enabled = true;
            FechaRetiroTP.Enabled = true;
            TotalTXT.Enabled = true;
            EnviarVentaBTN.Enabled = true;

            IDproductoTXT.Enabled = true; // Habilitar al limpiar
            CantidadTXT.Enabled = true;    // Habilitar al limpiar
            PrecioUnitarioTXT.Enabled = true; // Habilitar al limpiar
            EnviarDetalleVentaBTN.Enabled = true; // Habilitar al limpiar
        }

        private void FormRegistrarVentas_Load(object sender, EventArgs e)
        {
            CargarMetodosPago();
            // **No deshabilitar la sección de detalles al cargar el formulario**
        }

        private void CargarMetodosPago()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("SELECT IDmetodoPago, MetodoPago FROM MetodosPago", connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable metodosPagoTable = new DataTable();
                    adapter.Fill(metodosPagoTable);

                    // Asignar la tabla de datos al ComboBox
                    MetodoPagoComboBox.DataSource = metodosPagoTable;

                    // INDICA QUÉ COLUMNA MOSTRAR AL USUARIO
                    MetodoPagoComboBox.DisplayMember = "MetodoPago";

                    // INDICA QUÉ COLUMNA UTILIZAR COMO VALOR INTERNO
                    MetodoPagoComboBox.ValueMember = "IDmetodoPago";

                    // Opcional: Agregar un ítem por defecto
                    MetodoPagoComboBox.SelectedIndex = -1; // Para que no haya nada seleccionado por defecto
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al cargar los métodos de pago: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void RegistrarClienteBTN_Click(object sender, EventArgs e)
        {
            FormRegistrarCliente formRegistrarCliente = new FormRegistrarCliente();
            formRegistrarCliente.Show();
            this.Hide();
        }

        private void CargarTotalVenta()
        {
            if (ventaID.HasValue)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand("SELECT Total FROM Ventas WHERE IDventa = @IDventa", connection);
                        command.Parameters.AddWithValue("@IDventa", ventaID.Value);
                        object result = command.ExecuteScalar();
                        if (result != null && decimal.TryParse(result.ToString(), out decimal total))
                        {
                            TotalTXT.Text = total.ToString("N2"); // Formatear a 2 decimales
                        }
                        else
                        {
                            TotalTXT.Text = "0.00";
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al cargar el total de la venta: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        TotalTXT.Text = "ERROR";
                    }
                }
            }
            else
            {
                TotalTXT.Text = "0.00";
            }
        }
    }
}