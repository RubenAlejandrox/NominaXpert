
namespace NominaXpert.View.UsersControl
{
    public partial class UC_UsuariosBaja : UserControl
    {
        public UC_UsuariosBaja()
        {
            InitializeComponent();
            inicializa_UC_Usuarios_Alta();
        }
        private void inicializa_UC_Usuarios_Alta()
        {
            PoblaComboSeleccionUsuario();
            PoblaComboMotivo();
            dtpFechaBaja.Value = DateTime.Now;
        }
        private void PoblaComboSeleccionUsuario()
        {
            Dictionary<int, string> list_estatus = new Dictionary<int, string> //almacena pares clave/valor
            {
                //key (id), value
                {1, "UAdJacqueline"},
                {0, "UAuRuben"},
                {2, "UEmEvans"}
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
        private void ibtnGuardar_Click_1(object sender, EventArgs e)
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
        private void addUsersControl(UserControl userControl)
        {
            // Limpiar el panel contenedor
            panelContainer.Controls.Clear();
            // Configurar el control de usuario
            userControl.Dock = DockStyle.Fill;
            // Agregar el control de usuario al panel
            panelContainer.Controls.Add(userControl);
        }
        //Si le doy en cancelar me regresa al usuario de control de listado de usuarios 
        private void ibtnCancelar_Click(object sender, EventArgs e)
        {
            UC_UsuariosListado uclistado = new UC_UsuariosListado();
            addUsersControl(uclistado);
        }
        public void CargarDatos(string usuarioSeleccionado)
        {
            // Aquí cargas los datos en los controles del UserControl
            cbxSeleccionUsuario.SelectedValue = ObtenerClaveDeUsuario(usuarioSeleccionado);
        }

        private int ObtenerClaveDeUsuario(string usuarioSeleccionado)
        {
            // Obtener el BindingSource del ComboBox
            var bindingSource = (BindingSource)cbxSeleccionUsuario.DataSource;

            // Obtener la lista de KeyValuePair desde el BindingSource
            var listaUsuarios = bindingSource.List as System.ComponentModel.BindingList<KeyValuePair<int, string>>;

            if (listaUsuarios != null)
            {
                // Buscar la clave correspondiente al nombre del usuario
                foreach (var item in listaUsuarios)
                {
                    if (item.Value == usuarioSeleccionado)
                        return item.Key; // Devuelve la clave asociada al valor
                }
            }
            return -1; // Valor por defecto si no se encuentra
        }
    }
}



