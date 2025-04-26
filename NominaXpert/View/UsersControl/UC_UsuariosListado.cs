using System.Data;
namespace NominaXpert.View.UsersControl
{
    public partial class UC_UsuariosListado : UserControl
    {
        public UC_UsuariosListado()
        {
            InitializeComponent();
            ConfigurarDataGridView(); //    this.Load += Form1_Load; // Vincular el evento Load
            dtgListaUsuario.Columns.Add("Seleccion", "Seleccion"); // Columna de checkboxes
            dtgListaUsuario.Columns["Seleccion"].ValueType = typeof(bool); // Tipo de dato booleano
            dtgListaUsuario.Columns.Add("Nombre", "Nombre");
            dtgListaUsuario.Columns.Add("ApellidoPat", "Apellido Paterno");
            dtgListaUsuario.Columns.Add("ApellidoMat", "Apellido Materno");
            dtgListaUsuario.Columns.Add("Genero", "Género");
            dtgListaUsuario.Columns.Add("Correo", "Correo");
            dtgListaUsuario.Columns.Add("Estatus", "Estatus");
            dtgListaUsuario.Columns.Add("Rol", "Rol");
            dtgListaUsuario.Columns.Add("Usuario", "Usuario");
            // Agregar datos de ejemplo
            dtgListaUsuario.Rows.Add(false, "UAuRuben", "Rubén", "Nolasco", "Ruiz", "Hombre", "audi@gmail.com", "Activo", "Auditor");
            dtgListaUsuario.Rows.Add(false, "UAdJacqueline", "Jacquelin", "Escobar", "Espinoza", "Mujer", "admin@gmail.com", "Activo", "Administrador");
            dtgListaUsuario.Rows.Add(false, "UEmEvans", "Evans", "Martinez", "Santana", "Mujer", "emp@gmail.com", "Activo", "RH");
        }




        private void addUsersControl(UserControl userControl)
        {
            // Limpiar el panel contenedor
            panelContenedor.Controls.Clear();
            // Configurar el control de usuario
            userControl.Dock = DockStyle.Fill;
            // Agregar el control de usuario al panel
            panelContenedor.Controls.Add(userControl);
        }

        private void btnEditar_Click_2(object sender, EventArgs e)
        {
            // Obtener la fila seleccionada
            var filaSeleccionada = dtgListaUsuario.Rows.Cast<DataGridViewRow>()
                .FirstOrDefault(row => Convert.ToBoolean(row.Cells["Seleccion"].Value));

            if (filaSeleccionada != null)
            {
                // Obtener los datos de la fila seleccionada
                string apellidoPat = filaSeleccionada.Cells["ApellidoPat"].Value.ToString();
                string apellidoMat = filaSeleccionada.Cells["ApellidoMat"].Value.ToString();
                string nombre = filaSeleccionada.Cells["Nombre"].Value.ToString();
                string genero = filaSeleccionada.Cells["Genero"].Value.ToString();
                string correo = filaSeleccionada.Cells["Correo"].Value.ToString();
                string estatus = filaSeleccionada.Cells["Estatus"].Value.ToString();
                string rol = filaSeleccionada.Cells["Rol"].Value.ToString();
                string usuario = filaSeleccionada.Cells["Usuario"].Value.ToString();
                // Cargar el UserControl de edición
                UC_UsuariosEditar ucEditar = new UC_UsuariosEditar();
                ucEditar.CargarDatos(usuario, nombre, apellidoPat, apellidoMat, genero, correo, estatus, rol);

                addUsersControl(ucEditar);
            }
            else
            {
                MessageBox.Show("No se puede realizar esta opción.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dtgListaUsuario_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ConfigurarDataGridView()
        {
            // Configurar el color de los bordes de las celdas
            dtgListaUsuario.GridColor = Color.FromArgb(12, 215, 253); // Bordes internos de las celdas

            // Configurar el estilo del encabezado (fondo blanco y letras personalizadas)
            dtgListaUsuario.ColumnHeadersDefaultCellStyle.BackColor = Color.White; // Fondo blanco
            dtgListaUsuario.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(12, 215, 253); // Letras color (12, 215, 253)
            dtgListaUsuario.EnableHeadersVisualStyles = false; // Deshabilitar estilos visuales para aplicar los colores personalizados

            // Configurar el estilo del cuerpo (fondo oscuro y letras personalizadas)
            dtgListaUsuario.DefaultCellStyle.BackColor = Color.FromArgb(37, 41, 47); // Fondo oscuro (15, 15, 15)
            dtgListaUsuario.DefaultCellStyle.ForeColor = Color.FromArgb(12, 215, 253); // Letras color (12, 215, 253)
            dtgListaUsuario.BackgroundColor = Color.FromArgb(37, 41, 47); // Fondo de la tabla (15, 15, 15)

            // Configurar los bordes del encabezado
            dtgListaUsuario.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dtgListaUsuario.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(12, 215, 253); // Color del borde del encabezado

            // Configurar los bordes externos del DataGridView
            dtgListaUsuario.BorderStyle = BorderStyle.FixedSingle;
            dtgListaUsuario.CellBorderStyle = DataGridViewCellBorderStyle.Single;
        }

        private void ibtnBajaUsuario_Click(object sender, EventArgs e)
        {
            // Obtener la fila seleccionada
            var filaSeleccionada = dtgListaUsuario.Rows.Cast<DataGridViewRow>()
                .FirstOrDefault(row => Convert.ToBoolean(row.Cells["Seleccion"].Value));

            if (filaSeleccionada != null)
            {
                string usuario = filaSeleccionada.Cells["Usuario"].Value.ToString();
                // Cargar el UserControl de edición
                UC_UsuariosBaja ucBaja = new UC_UsuariosBaja();
                ucBaja.CargarDatos(usuario);
                addUsersControl(ucBaja);
            }
            else
            {
                MessageBox.Show("No se puede realizar esta opción.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ibtnEditar_Click(object sender, EventArgs e)
        {
            // Obtener la fila seleccionada
            var filaSeleccionada = dtgListaUsuario.Rows.Cast<DataGridViewRow>()
                .FirstOrDefault(row => Convert.ToBoolean(row.Cells["Seleccion"].Value));

            if (filaSeleccionada != null)
            {
                // Obtener los datos de la fila seleccionada
                string apellidoPat = filaSeleccionada.Cells["ApellidoPat"].Value.ToString();
                string apellidoMat = filaSeleccionada.Cells["ApellidoMat"].Value.ToString();
                string nombre = filaSeleccionada.Cells["Nombre"].Value.ToString();
                string genero = filaSeleccionada.Cells["Genero"].Value.ToString();
                string correo = filaSeleccionada.Cells["Correo"].Value.ToString();
                string estatus = filaSeleccionada.Cells["Estatus"].Value.ToString();
                string rol = filaSeleccionada.Cells["Rol"].Value.ToString();
                string usuario = filaSeleccionada.Cells["Usuario"].Value.ToString();
                // Cargar el UserControl de edición
                UC_UsuariosEditar ucEditar = new UC_UsuariosEditar();
                ucEditar.CargarDatos(usuario, nombre, apellidoPat, apellidoMat, genero, correo, estatus, rol);

                addUsersControl(ucEditar);
            }
            else
            {
                MessageBox.Show("No se puede realizar esta opción.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}









