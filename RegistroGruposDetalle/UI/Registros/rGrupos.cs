using RegistroGruposDetalle.BLL;
using RegistroGruposDetalle.DAL;
using RegistroGruposDetalle.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RegistroGruposDetalle.UI.Registros
{
    public partial class rGrupos : Form
    {
        public rGrupos()
        {
            InitializeComponent();
            LlenarComboBox();  
        }

        private void LlenarCampos(Grupos grupo)
        {
            GrupoIdnumericUpDown.Value = grupo.GrupoId;
            fechaDateTimePicker.Value = grupo.Fecha;
            NombretextBox.Text = grupo.Descripcion;

            //Cargar el detalle al Grid
            DetalleDataGridView.DataSource = grupo.Detalle;

            //Ocultar columnas
            DetalleDataGridView.Columns["Id"].Visible = false;
            DetalleDataGridView.Columns["PersonaId"].Visible = false;
        }

        private Grupos LlenaClase()
        {
            Grupos grupo = new Grupos();

            grupo.GrupoId = Convert.ToInt32(GrupoIdnumericUpDown.Value);
            grupo.Fecha = fechaDateTimePicker.Value;

            //Agregar cada linea del Grid al detalle
            foreach (DataGridViewRow item in DetalleDataGridView.Rows)
            {
                grupo.AgregarDetalle(
                    ToInt(item.Cells["Id"].Value),
                    ToInt(item.Cells["GrupoId"].Value),
                    ToInt(item.Cells["PersonaId"].Value),
                    item.Cells["Cargo"].ToString()
                  );
            }
            return grupo;
        }

        private void LlenarComboBox()
        {
            Repositorio<Personas> repositorio = new Repositorio<Personas>(new Contexto());
            PersonacomboBox.DataSource = repositorio.GetList(c => true);
            PersonacomboBox.ValueMember = "PersonaId";
            PersonacomboBox.DisplayMember = "Nombres";
        }

        private bool validar()
        {
            bool validar = false;

            if (DetalleDataGridView.RowCount == 0)
            {
                errorProvider1.SetError(DetalleDataGridView,
                    "Es obligatorio seleccionar las personas");
                validar = true;
            }

            return validar;
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(GrupoIdnumericUpDown.Value);
            Grupos grupo = GruposBLL.Buscar(id);

            if (grupo != null)
            {
                LlenarCampos(grupo);
            }
            else
                MessageBox.Show("No se encontro!", "Fallo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            GrupoIdnumericUpDown.Value = 0;
            NombretextBox.Clear();
            fechaDateTimePicker.Value = DateTime.Now;
            CargotextBox.Clear();

            DetalleDataGridView.DataSource = null;
            errorProvider1.Clear();
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            Grupos grupo;
            bool Paso = false;

            if (validar())
            {
                MessageBox.Show("Favor revisar todos los campos", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            grupo = LlenaClase();

            //Determinar si es Guardar o Modificar
            if (GrupoIdnumericUpDown.Value == 0)
                Paso = GruposBLL.Guardar(grupo);
            else
                //validar que exista.
                Paso = GruposBLL.Modificar(grupo);

            //Informar el resultado
            if (Paso)
            {
                BtnNuevo.PerformClick();
                MessageBox.Show("Guardado!!", "Exito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("No se pudo guardar!!", "Fallo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(GrupoIdnumericUpDown.Value);

            //validar que exista
            if (GruposBLL.Eliminar(id))
                MessageBox.Show("Eliminado!!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("No se pudo eliminar!!", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private int ToInt(object valor)
        {
            int retorno = 0;

            int.TryParse(valor.ToString(), out retorno);

            return retorno;
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            List<GruposDetalle> detalle = new List<GruposDetalle>();

            if (DetalleDataGridView.DataSource != null)
            {
                detalle = (List<GruposDetalle>)DetalleDataGridView.DataSource;
            }

            //Agregar un nuevo detalle con los datos introducidos.
            detalle.Add(
                new GruposDetalle(
                    id: 0,
                    grupoId: (int)GrupoIdnumericUpDown.Value,
                    personaId: (int)PersonacomboBox.SelectedValue,
                    cargo: (string)CargotextBox.Text
                ));

            //Cargar el detalle al Grid
            DetalleDataGridView.DataSource = null;
            DetalleDataGridView.DataSource = detalle;
        }

        private void BtnRemover_Click(object sender, EventArgs e)
        {
            if (DetalleDataGridView.Rows.Count > 0
                && DetalleDataGridView.CurrentRow != null)
            {
                //convertir el grid en la lista
                List<GruposDetalle> detalle
                    = (List<GruposDetalle>)DetalleDataGridView.DataSource;

                //remover la fila
                detalle.RemoveAt(DetalleDataGridView.CurrentRow.Index);

                // Cargar el detalle al Grid
                DetalleDataGridView.DataSource = null;
                DetalleDataGridView.DataSource = detalle;
            }
        }
    }
}
