using System.Data;
using NominaXpert.Business;
using NominaXpert.Controller;
using NominaXpert.Model;

namespace NominaXpert.View.UsersControl
{
    public partial class UC_UsuariosAlta : UserControl
    {
        private bool _modoEdicion = false;

        private int _idUsuarioEditar = 0;

        public UC_UsuariosAlta(int idUsuario) : this() // llama al constructor por defecto
        {
            _idUsuarioEditar = idUsuario;
            _modoEdicion = true;

        }
        public void ObtenerDetalleUsuario(int idUsuario)
        {
            UsuariosController controller = new UsuariosController();
            Usuario usuario = controller.ObtenerDetalleUsuario(idUsuario);

            if (usuario != null)
            {
                _idUsuarioEditar = usuario.Id;
                txtNomUsuario.Text = usuario.Nombre_Usuario;
                txtContraseña.Text = usuario.Contrasena;
                txtNombre.Text = usuario.DatosPersonales?.NombreCompleto ?? "";
                txtCorreo.Text = usuario.DatosPersonales?.Correo ?? "";
                txtTelefono.Text = usuario.DatosPersonales?.Telefono ?? "";
                txtDireccion.Text = usuario.DatosPersonales?.Direccion ?? "";
                txtCurp.Text = usuario.DatosPersonales?.Curp ?? "";
                txtRfc.Text = usuario.DatosPersonales?.Rfc ?? "";
                dtpFechaNacimiento.Value = usuario.DatosPersonales?.FechaNacimiento ?? DateTime.Now;
                cbxEstatus.SelectedValue = usuario.Estatus ? 1 : 0;
                cbxRoles.SelectedValue = usuario.IdRol;

                EstablecerModoEdicion(true); // 🟢 Cambiar botón y modo
            }
        }


        private void EstablecerModoEdicion(bool edicion)
        {
            _modoEdicion = edicion;
            ibtnGuardar.Text = edicion ? "Actualizar" : "Guardar";
            //.Text = edicion ? "Editar Usuario" : "Nuevo Usuario"; // si usas un label
        }

        public UC_UsuariosAlta()
        {
            InitializeComponent();
            inicializa_UC_Usuarios_Alta();
        }
        private void inicializa_UC_Usuarios_Alta()
        {
            PoblaComboEstatus();
            PoblaComboRol();
            ConfigurarPermisos();
            dtpFechaNacimiento.Value = DateTime.Now;
        }

        private void UC_UsuariosAlta_Load(object sender, EventArgs e)
        {
            if (!_modoEdicion)
                EstablecerModoEdicion(false);
        }


        private void ConfigurarPermisos()
        {
            var controller = new UsuariosController();

            ibtnGuardar.Enabled = controller.TienePermiso("USR_ADD");
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


        private void PoblaComboRol()
        {
            Dictionary<int, string> list_fecha = new Dictionary<int, string>
            {
                //key , value
                {1, "Administrador"},
                {3, "Lector"},
                {2, "Operador"},
                {4, "Seguridad"},
                {5, "Autorizador"}
            };
            cbxRoles.DataSource = new BindingSource(list_fecha, null);
            cbxRoles.DisplayMember = "Value"; //lo que se muestra
            cbxRoles.ValueMember = "Key"; //lo que se guarda como seleccion
            cbxRoles.SelectedValue = 1;
        }

        private bool DatosVacios()
        {
            if (txtNombre.Text == "" || txtDireccion.Text == "" || txtTelefono.Text == "" || txtNomUsuario.Text == ""
                || txtContraseña.Text == "" || txtCorreo.Text == "" || txtRfc.Text == "" || txtDireccion.Text == "" || txtRfc.Text == "" || txtCurp.Text == "")
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
            if (_modoEdicion)
            {
                ConfigurarPermisos();
                ActualizarUsuario();
            } // ya debes tener este
            else
                GuardarUsuario(); // tu método normal
        }

        private void ReiniciarFormulario()
        {
            txtNomUsuario.Clear();
            txtContraseña.Clear();
            txtNombre.Clear();
            txtCorreo.Clear();
            txtTelefono.Clear();
            txtDireccion.Clear();
            txtCurp.Clear();
            txtRfc.Clear();
            dtpFechaNacimiento.Value = DateTime.Now.AddYears(-18);
            cbxEstatus.SelectedIndex = 0;
            cbxRoles.SelectedIndex = 0;

            _modoEdicion = false;
            _idUsuarioEditar = 0;
            EstablecerModoEdicion(false); // 🔄 Regresar texto a Guardar
        }



        private void GuardarUsuario()
        {
            if (DatosVacios())
            {
                MessageBox.Show("Por favor llene todos los campos", "Información del sistema",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!DatosValidos())
            {
                return;
            }

            try
            {
                Persona persona = new Persona(
                    txtNombre.Text.Trim(),
                    txtCorreo.Text.Trim(),
                    txtTelefono.Text.Trim(),
                    txtCurp.Text.Trim())
                {
                    FechaNacimiento = dtpFechaNacimiento.Value,
                    Direccion = txtDireccion.Text.Trim(),
                    Rfc = txtRfc.Text.Trim()
                };

                Usuario estudiante = new Usuario
                {
                    Nombre_Usuario = txtNomUsuario.Text.Trim(),
                    Contrasena = txtContraseña.Text.Trim(),
                    IdRol = (int)cbxRoles.SelectedValue,
                    Estatus = Convert.ToInt32(cbxEstatus.SelectedValue) == 1, // Conversión correcta
                    DatosPersonales = persona
                };

                UsuariosController usuariosController = new UsuariosController();
                var (idUsuario, mensaje) = usuariosController.RegistrarUsuario(estudiante);

                if (idUsuario > 0) // Cambiado a idUsuario para coincidir con la desestructuración
                {
                    MessageBox.Show(mensaje, "Información del sistema",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarFormulario();
                }
                else
                {
                    MessageBox.Show(mensaje, "Información del sistema",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    //switch (idUsuario)
                    //{
                    //    case -2:
                    //        txtCurp.Focus();
                    //        txtCurp.SelectAll();
                    //        break;
                    //    case -3:
                    //        txtNoControl.Focus();
                    //        txtNoControl.SelectAll();
                    //        break;
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo completar el registro. Error: " + ex.Message,
                              "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void LimpiarFormulario()
        {
            txtNombre.Clear();
            txtRfc.Clear();
            txtNomUsuario.Clear();
            txtContraseña.Clear();
            txtCorreo.Clear();
            txtTelefono.Clear();
            txtCurp.Clear();
            txtDireccion.Clear();
            cbxEstatus.ResetText();
            cbxRoles.ResetText();
            dtpFechaNacimiento.Value = DateTime.Now.AddYears(-18); // Valor por defecto
            cbxEstatus.SelectedValue = 1; // Activo por defecto
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ActualizarUsuario()
        {
            if (DatosVacios())
            {
                MessageBox.Show("Por favor llena todos los campos.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Persona persona = new Persona
            {
                Id = _idUsuarioEditar,
                NombreCompleto = txtNombre.Text.Trim(),
                Correo = txtCorreo.Text.Trim(),
                Telefono = txtTelefono.Text.Trim(),
                Curp = txtCurp.Text.Trim(),
                Rfc = txtRfc.Text.Trim(),
                Direccion = txtDireccion.Text.Trim(),
                FechaNacimiento = dtpFechaNacimiento.Value,
                Estatus = Convert.ToInt32(cbxEstatus.SelectedValue) == 1
            };

            Usuario usuario = new Usuario
            {
                Id = _idUsuarioEditar,
                Nombre_Usuario = txtNomUsuario.Text.Trim(),
                Contrasena = txtContraseña.Text.Trim(),
                IdRol = Convert.ToInt32(cbxRoles.SelectedValue),
                Estatus = Convert.ToInt32(cbxEstatus.SelectedValue) == 1,
                DatosPersonales = persona
            };

            UsuariosController controller = new UsuariosController();
            var (exito, mensaje) = controller.ActualizarUsuario(usuario);

            MessageBox.Show(mensaje, exito ? "Éxito" : "Error",
                MessageBoxButtons.OK,
                exito ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

            if (exito)
                ReiniciarFormulario();
        }

        private void txtCorreo_TextChanged(object sender, EventArgs e)
        {

        }
    }
}













