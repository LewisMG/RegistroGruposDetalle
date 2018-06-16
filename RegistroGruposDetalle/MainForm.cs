using RegistroGruposDetalle.UI.Consultas;
using RegistroGruposDetalle.UI.Registros;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RegistroGruposDetalle
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void GruposToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rGrupos registro = new rGrupos();
            registro.MdiParent = this;
            registro.Show();
        }

        private void personasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rPersonas registrar = new rPersonas();
            registrar.MdiParent = this;
            registrar.Show();
        }

        private void personasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            cPersonas consultar = new cPersonas();
            consultar.MdiParent = this;
            consultar.Show();
        }

        private void GruposToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            cGrupos consultar = new cGrupos();
            consultar.MdiParent = this;
            consultar.Show();
        }

        private void PersonastoolStripButton_Click(object sender, EventArgs e)
        {
            rPersonas registrar = new rPersonas();
            registrar.MdiParent = this;
            registrar.Show();
        }

        private void GrupotoolStripButton_Click(object sender, EventArgs e)
        {
            rGrupos registro = new rGrupos();
            registro.MdiParent = this;
            registro.Show();
        }
    }
}
