using FontAwesome.Sharp;
using NominaXpert.View.UsersControl;

namespace NominaXpert.View.Forms
{
    public partial class frmSeguridadUsuarios : Form
    {
        public frmSeguridadUsuarios()
        {
            InitializeComponent();
            CargarUserControlInicial();
            CrearBordeBoton();
        }
        //Variable tipo iconButton que almacena un botón activo
        private IconButton currentBtn;

        private Panel leftBorderBtn;
        private struct RGBColors
        {
            public static Color changeColor = Color.FromArgb(12, 215, 253);
        }

        private void CrearBordeBoton()
        {
            leftBorderBtn = new Panel(); //inicializar el panel
            leftBorderBtn.Size = new Size(100, 3); // Tamaño predeterminado
            leftBorderBtn.Visible = false;
            this.Controls.Add(leftBorderBtn); // Agregar el panel al formulario
        }
        private void ActivateButton(object senderBtn, Color color)
        {
            if (senderBtn != null)
            {
                DisableButton();//Restablecimiento de colores 
                //Button
                currentBtn = (IconButton)senderBtn;
                currentBtn.BackColor = Color.FromArgb(59, 62, 68);
                currentBtn.ForeColor = color;
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                currentBtn.IconColor = color;
                currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentBtn.ImageAlign = ContentAlignment.MiddleRight;
                //Left border button
                leftBorderBtn.BackColor = color;
                leftBorderBtn.Size = new Size(currentBtn.Width, 3); // Ancho del botón, altura de 3px
                leftBorderBtn.Location = new Point(currentBtn.Location.X, currentBtn.Location.Y + currentBtn.Height);
                leftBorderBtn.Visible = true;
                leftBorderBtn.BringToFront();

            }
        }
        //restablece el estilo del botón que estaba activo antes de que se hiciera clic en otro botón
        private void DisableButton()
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = Color.FromArgb(48, 51, 59);
                currentBtn.ForeColor = Color.Gainsboro;
                currentBtn.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn.IconColor = Color.FromArgb(12, 215, 253);
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }
        //Carga un control de usuario inicial, cuando se abre por primera vez
        private void CargarUserControlInicial()
        {
            UC_UsuariosAlta uc = new UC_UsuariosAlta();
            addUsersControl(uc); // Usamos el mismo método que para los botones
        }
        //Agrega un control de usuario
        private void addUsersControl(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill; //El control se ajusta para llenar el espacio disponible
            panelContainer.Controls.Clear(); //limpia
            panelContainer.Controls.Add(userControl); //agrega el control de usuario
            userControl.BringToFront(); //lo lleva al frente para que sea visible
        }
        //Creación de control de usuario
        private void btnAltaBar_Click(object sender, EventArgs e)
        {
            UC_UsuariosAlta uc = new UC_UsuariosAlta();
            ActivateButton(sender, RGBColors.changeColor);
            addUsersControl(uc);
        }

        private void btnListadoBar_Click(object sender, EventArgs e)
        {
            UC_UsuariosListado uc = new UC_UsuariosListado();
            ActivateButton(sender, RGBColors.changeColor);
            addUsersControl(uc);
        }
        private void btnBajaBar_Click(object sender, EventArgs e)
        {

            UC_UsuariosBaja uc = new UC_UsuariosBaja();
            ActivateButton(sender, RGBColors.changeColor);
            addUsersControl(uc);
        }

        private void btnRolesPermisosBar_Click(object sender, EventArgs e)
        {
            UC_UsuariosRolPermiso uc = new UC_UsuariosRolPermiso();
            ActivateButton(sender, RGBColors.changeColor);
            addUsersControl(uc);
        }

        private void ibtnAltaRol_Click(object sender, EventArgs e)
        {
            UC_RolesAlta uc = new UC_RolesAlta();
            ActivateButton(sender, RGBColors.changeColor);
            addUsersControl(uc);
        }

        private void ibtnEdicionRol_Click(object sender, EventArgs e)
        {
            UC_RolesEditar uc = new UC_RolesEditar();
            ActivateButton(sender, RGBColors.changeColor);
            addUsersControl(uc);
        }

        private void ibtnBajaRoles_Click(object sender, EventArgs e)
        {
            UC_RolesBaja uc = new UC_RolesBaja();
            ActivateButton(sender, RGBColors.changeColor);
            addUsersControl(uc);
        }
    }
}
