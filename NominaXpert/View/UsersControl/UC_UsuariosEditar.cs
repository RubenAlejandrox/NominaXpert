
using NominaXpert.Business;

namespace NominaXpert.View.UsersControl
{
    public partial class UC_UsuariosEditar : UserControl
    {
        public UC_UsuariosEditar()
        {
            InitializeComponent();
            inicializa_UC_Usuarios_Edicion();
        }
        private void inicializa_UC_Usuarios_Edicion()
        {
            PoblaComboGenero();
            PoblaComboEstatus();
            PoblaComboRol();
            dtpFechaNacimiento.Value = DateTime.Now;
        }
        public void CargarDatos(string usuario,string nombre, string apellidoPat, string apellidoMat, string genero, string correo, string estatus, string rol)
        {
            // Aquí cargas los datos en los controles del UserControl
            txtNomUsuario.Text = usuario;
            txtNombre.Text = nombre;
            txtApellidoPat.Text = apellidoPat;
            txtApellidoMat.Text = apellidoMat;
            txtCorreo.Text = correo;
            cbxGenero.SelectedValue = ObtenerClaveGenero(genero);
            cbxEstatus.SelectedValue = ObtenerClaveEstatus(estatus);
            cbxRoles.SelectedValue = ObtenerClaveRol(rol);
        }

        private int ObtenerClaveGenero(string genero)
        {
            // Obtener el BindingSource
            var bindingSource = (BindingSource)cbxGenero.DataSource;
            // Obtener la lista de KeyValuePair desde el BindingSource
            var listaGenero = bindingSource.List as System.ComponentModel.BindingList<KeyValuePair<int, string>>;

            if (listaGenero != null)
            {
                // Buscar la clave correspondiente al género
                foreach (var item in listaGenero)
                {
                    if (item.Value == genero)
                        return item.Key;
                }
            }
            return -1; // Valor por defecto si no se encuentra
        }

        private int ObtenerClaveEstatus(string estatus)
        {
            // Obtener el BindingSource
            var bindingSource = (BindingSource)cbxEstatus.DataSource;
            // Obtener la lista de KeyValuePair desde el BindingSource
            var listaEstatus = bindingSource.List as System.ComponentModel.BindingList<KeyValuePair<int, string>>;

            if (listaEstatus != null)
            {
                // Buscar la clave correspondiente al estatus
                foreach (var item in listaEstatus)
                {
                    if (item.Value == estatus)
                        return item.Key;
                }
            }
            return -1; // Valor por defecto si no se encuentra
        }

        private int ObtenerClaveRol(string rol)
        {
            // Obtener el BindingSource
            var bindingSource = (BindingSource)cbxRoles.DataSource;
            // Obtener la lista de KeyValuePair desde el BindingSource
            var listaRoles = bindingSource.List as System.ComponentModel.BindingList<KeyValuePair<int, string>>;

            if (listaRoles != null)
            {
                // Buscar la clave correspondiente al rol
                foreach (var item in listaRoles)
                {
                    if (item.Value == rol)
                        return item.Key;
                }
            }
            return -1; // Valor por defecto si no se encuentra
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

        private void PoblaComboGenero()
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
            cbxGenero.SelectedValue = 0;
        }
        private void PoblaComboRol()
        {
            Dictionary<int, string> list_fecha = new Dictionary<int, string>
            {
                //key , value
                {1, "Administrador"},
                {0, "Empleado"},
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
            if (txtNombre.Text == "" || txtApellidoPat.Text == "" || txtNomUsuario.Text == ""
                || txtContraseña.Text == "" || txtCorreo.Text == "" || txtRfc.Text == "" || txtDireccion.Text == "" || txtApellidoMat.Text == "" || txtCurp.Text == "")
            {
                return true;
            }
            else
            {
                return false;
            }
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

        private void cbxGenero_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



        private void addUsersControl(UserControl userControl)
        {
            // Limpiar el panel contenedor
            panelContainer.Controls.Clear();
            // Configurar el control de usuario
            userControl.Dock = DockStyle.Fill;
            // Agregar el control de usuario al panel
            panelContainer.Controls.Add(userControl);
        }

        private void ibtnGuardar_Click_1(object sender, EventArgs e)
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
                if (GuardarUsuario())
                {
                    MessageBox.Show("Datos guardados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void ibtnCancelar_Click_1(object sender, EventArgs e)
        {
            UC_UsuariosListado uclistado = new UC_UsuariosListado();
            addUsersControl(uclistado);
        }
    }
}










