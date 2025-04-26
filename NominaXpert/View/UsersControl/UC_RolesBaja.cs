using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NominaXpert.View.UsersControl
{
    public partial class UC_RolesBaja : UserControl
    {
        public UC_RolesBaja()
        {
            InitializeComponent();
            PoblaComboSeleccionRol();
            PoblaComboMotivo();
            dtpFechaBaja.Value = DateTime.Now;
        }
        private void PoblaComboSeleccionRol()
        {
            Dictionary<int, string> list_estatus = new Dictionary<int, string> //almacena pares clave/valor
            {
                //key (id), value
                {1, "Administrador"},
                {0, "Recursos Humanos (RH)"},
                {2, "Auditor"}
            };
            cbxSeleccionUsuario.DataSource = new BindingSource(list_estatus, null);
            cbxSeleccionUsuario.DisplayMember = "Value"; //lo que se muestra
            cbxSeleccionUsuario.ValueMember = "Key"; //lo que se guarda como seleccion
            cbxSeleccionUsuario.SelectedValue = 1; // Valor por defecto
        }
        private void PoblaComboMotivo()
        {
            Dictionary<int, string> list_estatus = new Dictionary<int, string>
            {
                //key , value
                {1, "Motivo 1"},
                {0, "Motivo 2"},
                {2, "Motivo 3"}
            };
            cbxMotivoBaja.DataSource = new BindingSource(list_estatus, null);
            cbxMotivoBaja.DisplayMember = "Value"; //lo que se muestra
            cbxMotivoBaja.ValueMember = "Key"; //lo que se guarda como seleccion
            cbxMotivoBaja.SelectedValue = 1;
        }
        private void ibtnGuardar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show(
                "¿Estás seguro de que deseas dar de baja?",
                "Confirmar cambios",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            // Verificar si el usuario hizo clic en "Sí"
            if (resultado == DialogResult.Yes)
            {
                if (RealizarBaja())
                {
                    MessageBox.Show("Usuario dado de baja correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private bool RealizarBaja()
        {
            if (DatosVacios())
            {
                MessageBox.Show("Por favor llene todos los campos", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private bool DatosVacios()
        {
            if (txtDetallesBaja.Text == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
