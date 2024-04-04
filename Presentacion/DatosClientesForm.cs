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
            btnLimpiar.BackColor = Color.FromArgb(33, 230, 193);
            btnAceptar.BackColor =  Color.FromArgb(33, 230, 193);

            if (cliente != null)
            {
                txtBoxCuil.Text = cliente.Cuil;
                txtBoxNombreApellido.Text = cliente.NombreYApellido;
                txtBoxDireccion.Text = cliente.Direccion;
                txtBoxLocalidad.Text = cliente.Localidad;
                txtBoxTelefono.Text = cliente.Telefono;
                txtBoxDiasVigencia.Text = cliente.DiasVigencia.ToString();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBoxCuil.Clear();
            txtBoxDiasVigencia.Clear();
            txtBoxDireccion.Clear();
            txtBoxLocalidad.Clear();
            txtBoxNombreApellido.Clear();
            txtBoxTelefono.Clear();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            cliente.Cuil = txtBoxCuil.Text;
            cliente.NombreYApellido = txtBoxNombreApellido.Text;
            cliente.Direccion = txtBoxDireccion.Text;
            cliente.Telefono = txtBoxTelefono.Text;
            cliente.Localidad = txtBoxLocalidad.Text;
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

        private void txtBoxNombreApellido_TextChanged(object sender, EventArgs e)
        {
            txtBoxNombreApellido.Text = txtBoxNombreApellido.Text.ToUpper();
            txtBoxNombreApellido.SelectionStart = txtBoxNombreApellido.Text.Length;
        }

        private void txtBoxDireccion_TextChanged(object sender, EventArgs e)
        {
            txtBoxDireccion.Text = txtBoxDireccion.Text.ToUpper();
            txtBoxDireccion.SelectionStart = txtBoxDireccion.Text.Length;
        }

        private void txtBoxLocalidad_TextChanged(object sender, EventArgs e)
        {
            txtBoxLocalidad.Text = txtBoxLocalidad.Text.ToUpper();
            txtBoxLocalidad.SelectionStart = txtBoxLocalidad.Text.Length;
        }

        private void txtBoxTelefono_TextChanged(object sender, EventArgs e)
        {
            txtBoxTelefono.Text = txtBoxTelefono.Text.ToUpper();
            txtBoxTelefono.SelectionStart = txtBoxTelefono.Text.Length;
        }

        private void txtBoxCuil_TextChanged(object sender, EventArgs e)
        {
            txtBoxCuil.Text = txtBoxCuil.Text.ToUpper();
            txtBoxCuil.SelectionStart = txtBoxCuil.Text.Length;
        }
    }
}
