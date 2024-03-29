using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class AvisoForm : Form
    {
        public AvisoForm()
        {
            InitializeComponent();
        }

        private void AvisoForm_Load(object sender, EventArgs e)
        {
            btnCerrar.BackColor = Color.FromArgb(33, 230, 193);
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
             this.Close();
        }
    }
}
