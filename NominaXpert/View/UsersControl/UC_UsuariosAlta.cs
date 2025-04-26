using NominaXpert.Business;

namespace NominaXpert.View.UsersControl
{
    public partial class UC_UsuariosAlta : UserControl
    {
        public UC_UsuariosAlta()
        {
            InitializeComponent();
            inicializa_UC_Usuarios_Alta();
        }
        private void inicializa_UC_Usuarios_Alta()
        {
            PoblaComboTipoFecha();
            PoblaComboEstatus();
            PoblaComboRol();
            dtpFechaNacimiento.Value = DateTime.Now;
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

        private void PoblaComboTipoFecha()
        {
            Dictionary<int, string> list_fecha = new Dictionary<int, string>
            {
                //key , value
                {1, "Hombre"},
                {0, "Mujer"}
            };
            cbxGenero.DataSource = new BindingSource(list_fecha, null);
            cbxGenero.DisplayMember = "Value"; //lo que se muestra
            cbxGenero.ValueMember = "Key"; //lo que se guarda como seleccion
            cbxGenero.SelectedValue = 1;
        }
        private void PoblaComboRol()
        {
            Dictionary<int, string> list_fecha = new Dictionary<int, string>
            {
                //key , value
                {1, "Administrador"},
                {0, "Recursos Humanos (RH)"},
                {2, "Auditor"}
            };
            cbxRoles.DataSource = new BindingSource(list_fecha, null);
            cbxRoles.DisplayMember = "Value"; //lo que se muestra
            cbxRoles.ValueMember = "Key"; //lo que se guarda como seleccion
            cbxRoles.SelectedValue = 1;
        }

        private bool GuardarUsuario()
        {
            if (DatosVacios())
            {
                MessageBox.Show("Por favor llene todos los campos", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!DatosValidos())
            {
                return false;
            }
            return true;
        }
        private bool DatosVacios()
        {
            if (txtNombre.Text == "" || txtApellido.Text == "" || txtNomUsuario.Text == ""
                || txtContraseña.Text == "" || txtCorreo.Text == "" || txtRfc.Text == "" || txtDireccion.Text == "" || txtApellidoMaterno.Text == "" || txtCurp.Text == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void ibtnGuardar_Click_1(object sender, EventArgs e)
        {
            if (GuardarUsuario())
            {
                MessageBox.Show("Datos Guardados correctamente.", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private bool DatosValidos()
        {
            if (!PersonasNegocio.EsCorreoValido(txtCorreo.Text.Trim()))
            {
                MessageBox.Show("Correo inválido.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!PersonasNegocio.EsCURPValido(txtCurp.Text.Trim()))
            {
                MessageBox.Show("CURP inválida.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!PersonasNegocio.EsRFCValido(txtRfc.Text.Trim()))
            {
                MessageBox.Show("RFC inválido.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        
    }
}










