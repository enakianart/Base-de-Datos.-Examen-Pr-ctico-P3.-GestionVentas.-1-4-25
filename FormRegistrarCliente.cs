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
    public partial class FormRegistrarCliente : Form
    {
        private string connectionString = "Data Source=ENZOACER\\SQLEXPRESS;Initial Catalog=GestionVentas;Integrated Security=True;";
        public FormRegistrarCliente()
        {
            InitializeComponent();
        }

        private void EnviarBTN_Click(object sender, EventArgs e)
        {

            string nombre = NombreTXT.Text;
            string apellido = ApellidoTXT.Text;
            string correo = CorreoTXT.Text;
            string telefono = TelefonoTXT.Text;

            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellido) || string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(telefono))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    //  insertar un nuevo cliente
                    string query = "INSERT INTO Clientes (Nombre, Apellido, Correo, Telefono) VALUES (@Nombre, @Apellido, @Correo, @Telefono)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Nombre", nombre);
                        command.Parameters.AddWithValue("@Apellido", apellido);
                        command.Parameters.AddWithValue("@Correo", correo);
                        command.Parameters.AddWithValue("@Telefono", telefono);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Cliente registrado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            NombreTXT.Clear();
                            ApellidoTXT.Clear();
                            CorreoTXT.Clear();
                            TelefonoTXT.Clear();
                        }
                        else
                        {
                            MessageBox.Show("No se pudo registrar el cliente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al conectar o insertar en la base de datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void RegistrarVentasBTN_Click(object sender, EventArgs e)
        {
            FormRegistrarVentas formRegistrarVentas = new FormRegistrarVentas();
            formRegistrarVentas.Show();
            this.Hide();
        }


        
    }
}

