using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Compi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void ejecutarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var lexico = new Lexico(txtCodigoFuente.Text);
            lexico.EjecutarLexico();

            List<Error> listaErroresLexico = lexico.listaError;

            var lista = new BindingList<Token>(lexico.listaToken);
            dgvLexico.DataSource = null;
            dgvLexico.DataSource = lista;

            dgvErrores.DataSource = null;
            dgvErrores.DataSource = lexico.listaError;

            

            //dataGridView2.DataSource = null;

        }
    }
}
