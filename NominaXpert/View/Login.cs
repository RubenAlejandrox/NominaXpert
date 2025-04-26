using NominaXpert.Business;

namespace NominaXpert.View.Forms
{
    public partial class Login : Form
    {
        private bool showPassword = false;

        public Login()
        {
            InitializeComponent();
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close(); //Cerrar cuando se hace clic en cancelar
        }
        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            // 1️ Validar si los campos están vacíos
            if (string.IsNullOrWhiteSpace(txtCorreo.Text))
            {
                MessageBox.Show("El campo de usuario no puede estar vacío...", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtConstraseña.Text))
            {
                MessageBox.Show("El campo de contraseña no puede estar vacío...", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2️ Validar formato del correo
            if (!UsuarioNegocios.EsCorreoValido(txtCorreo.Text))
            {
                MessageBox.Show("El campo de correo no tiene el formato correcto...", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // 3 Verificar credenciales y obtener rol
            string rol = UsuarioNegocios.ObtenerRol(txtCorreo.Text, txtConstraseña.Text);

            if (rol != null)
            {
                // Guardar el rol en la propiedad estática de UsuarioNegocios
                UsuarioNegocios.Rol = rol;

                //Indicar que el login fue exitoso y cerrar este formulario
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void iconEye_Click(object sender, EventArgs e)
        {
            showPassword = !showPassword;
            if (showPassword)
            {
                txtConstraseña.PasswordChar = '\0';
           
            }
            else
            {
                txtConstraseña.PasswordChar = '*';
              
            }
        }
    }
}
