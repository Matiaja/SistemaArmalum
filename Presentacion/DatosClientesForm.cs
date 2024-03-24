using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidades;

namespace Presentacion
{
    public partial class DatosClientesForm : Form
    {
        public Cliente cliente { get; set; }
        public DatosClientesForm(Cliente cliente)
        {
            InitializeComponent();
            this.cliente = cliente ?? new Cliente();
        }

        private void DatosClientesForm_Load(object sender, EventArgs e)
        {
            if (cliente != null)
            {
                txtBoxCuil.Text = cliente.Cuil;
                txtBoxNombreApellido.Text = cliente.NombreYApellido;
                txtBoxDireccion.Text = cliente.Direccion;
                txtBoxTelefono.Text = cliente.Telefono;
                txtBoxDiasVigencia.Text = cliente.DiasVigencia.ToString();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBoxCuil.Clear();
            txtBoxDiasVigencia.Clear();
            txtBoxDireccion.Clear();
            txtBoxNombreApellido.Clear();
            txtBoxTelefono.Clear();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            cliente.Cuil = txtBoxCuil.Text;
            cliente.NombreYApellido = txtBoxNombreApellido.Text;
            cliente.Direccion = txtBoxDireccion.Text;
            cliente.Telefono = txtBoxTelefono.Text;
            if (string.IsNullOrWhiteSpace(txtBoxDiasVigencia.Text))
            {
                cliente.DiasVigencia = null;
            }
            else
            {
                if (int.TryParse(txtBoxDiasVigencia.Text, out int diasVigencia))
                {
                    cliente.DiasVigencia = diasVigencia;
                }
                else
                {
                    cliente.DiasVigencia = 0;
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
