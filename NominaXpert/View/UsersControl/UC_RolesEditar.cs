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
    public partial class UC_RolesEditar : UserControl
    {
        public UC_RolesEditar()
        {
            InitializeComponent();
            inicializa_UC_Roles_Alta();
        }

        private void inicializa_UC_Roles_Alta()
        {
            PoblaComboEstatus();
        }
        private void PoblaComboEstatus()
        {
            Dictionary<int, string> list_estatus = new Dictionary<int, string>
            {
                //key , value
                {1, "Activo"},
                {0, "Baja"},
                {2, "Baja Temporal"}
            };
            cbxEstatus.DataSource = new BindingSource(list_estatus, null);
            cbxEstatus.DisplayMember = "Value"; //lo que se muestra
            cbxEstatus.ValueMember = "Key"; //lo que se guarda como seleccion
            cbxEstatus.SelectedValue = 1;
        }

        private void ibtnGuardar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show(
                "¿Estás seguro de que deseas guardar los cambios?",
                "Confirmar cambios",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            // Verificar si el usuario hizo clic en "Sí"
            if (resultado == DialogResult.Yes)
            {
                if (GuardarRol())
                {
                    MessageBox.Show("Datos guardados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private bool GuardarRol()
        {
            if (DatosVacios())
            {
                MessageBox.Show("Por favor llene todos los campos", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private void icbtnGuardar_Click(object sender, EventArgs e)
        {
            if (GuardarRol())
            {
                MessageBox.Show("Datos Guardados correctamente.", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private bool DatosVacios()
        {
            if (txtCodigo.Text == "" || txtDescripcion.Text == "" || txtNombreRol.Text == "")
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
