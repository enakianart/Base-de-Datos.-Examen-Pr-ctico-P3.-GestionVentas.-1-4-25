using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Base_de_Datos.Examen_Práctico_P3.GestionVentas._1_4_25
{
    public partial class FormGestionVentasInicio : Form
    {
        public FormGestionVentasInicio()
        {
            InitializeComponent();
        }

        private void RegistrarClienteBTN_Click(object sender, EventArgs e)
        {
            FormRegistrarCliente formRegistrarCliente = new FormRegistrarCliente();
            formRegistrarCliente.Show();
            this.Hide();
        }

        private void RegistrarVentasBTN_Click(object sender, EventArgs e)
        {
            FormRegistrarVentas formRegistrarVentas = new FormRegistrarVentas();
            formRegistrarVentas.Show();
            this.Hide();
        }

      
    }
}
