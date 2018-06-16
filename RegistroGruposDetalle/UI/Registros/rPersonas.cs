using RegistroGruposDetalle.BLL;
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
    public partial class rPersonas : Form
    {
        public rPersonas()
        {
            InitializeComponent();
        }

        private void CedulamaskedTextBox_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
        
        private Personas LlenaClase()
        {
            Personas persona = new Personas();

            persona.PersonaId = Convert.ToInt32(PersonaIdnumericUpDown.Value);
            persona.Fecha = FechadateTimePicker.Value;
            persona.Nombres = NombretextBox.Text;
            persona.Cedula = CedulamaskedTextBox.Text;
            persona.Direccion = DirecciontextBox.Text;
            persona.Telefono = TelefonomaskedTextBox.Text;

            return persona;
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(PersonaIdnumericUpDown.Value);
            Personas persona = PersonasBLL.Buscar(id);

            if (persona != null)
            {
                FechadateTimePicker.Value = persona.Fecha;
                NombretextBox.Text = persona.Nombres;
                CedulamaskedTextBox.Text = persona.Cedula;
                DirecciontextBox.Text = persona.Direccion;
                TelefonomaskedTextBox.Text = persona.Telefono;
            }
            else
                MessageBox.Show("No se encontro!", "Fallo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            PersonaIdnumericUpDown.Value = 0;
            FechadateTimePicker.Value = DateTime.Now;
            NombretextBox.Clear();
            CedulamaskedTextBox.Clear();
            DirecciontextBox.Clear();
            TelefonomaskedTextBox.Clear();
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            Personas persona;
            bool Paso = false;


            persona = LlenaClase();

            //Determinar si es Guardar o Modificar
            if (PersonaIdnumericUpDown.Value == 0)
                Paso = PersonasBLL.Guardar(persona);
            else
                //todo: validar que exista.
                Paso = PersonasBLL.Modificar(persona);

            //Informar el resultado
            if (Paso)
                MessageBox.Show("Guardado!!", "Exito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("No se pudo guardar!!", "Fallo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(PersonaIdnumericUpDown.Value);

            //todo: validar que exista
            if (BLL.PersonasBLL.Eliminar(id))
                MessageBox.Show("Eliminado!!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("No se pudo eliminar!!", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private bool Validar()
        {
            bool HayErrores = false;

            if (String.IsNullOrWhiteSpace(TelefonomaskedTextBox.Text))
            {
                errorProvider1.SetError(TelefonomaskedTextBox,
                    "No debes dejar el telefono vacio");
                HayErrores = true;
            }
            if (String.IsNullOrWhiteSpace(NombretextBox.Text))
            {
                errorProvider1.SetError(NombretextBox,
                     "No debes dejar el nombre vacio");
                HayErrores = true;
            }
            if (String.IsNullOrWhiteSpace(DirecciontextBox.Text))
            {

                errorProvider1.SetError(DirecciontextBox,
                    "No debes dejar la direccion vacia");
                HayErrores = true;
            }
            if (String.IsNullOrWhiteSpace(CedulamaskedTextBox.Text))
            {

                errorProvider1.SetError(CedulamaskedTextBox,
                    "No debes dejar la cedula vacia");
                HayErrores = true;
            }

            return HayErrores;
        }
    }
}
