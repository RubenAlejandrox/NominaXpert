using System.Data;
using NominaXpert.Controller;
using NominaXpert.Model;
namespace NominaXpert.View.UsersControl
{
    public partial class UC_UsuariosListado : UserControl
    {

        public UC_UsuariosListado()
        {
            InitializeComponent();
            ConfigurarDataGridView();
            CargarUsuarios(); // Llama al método al cargar el UserControl
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
        private void ibtnEditar_Click(object sender, EventArgs e)
        {
            if (dgvListadoUsuario.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecciona un usuario para editar", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int idUsuario = Convert.ToInt32(dgvListadoUsuario.SelectedRows[0].Cells["id"].Value);

            UC_UsuariosAlta ucEditar = new UC_UsuariosAlta(); // constructor vacío
            addUsersControl(ucEditar);
            ucEditar.ObtenerDetalleUsuario(idUsuario); // método correcto
        }

        private void ibtnBajaUsuario_Click(object sender, EventArgs e)
        {

            UC_UsuariosBaja ucBaja = new UC_UsuariosBaja();
            addUsersControl(ucBaja);

        }
        private void btnEditar_Click_2(object sender, EventArgs e)
        {
            // Obtener la fila seleccionada

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }


        private void dtgListadoUsuario_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void CargarUsuarios()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                UsuariosController usuariosController = new UsuariosController();

                // Obtener todos los usuarios
                List<Usuario> usuarios = usuariosController.ObtenerUsuarios(soloActivos: false);

                dgvListadoUsuario.DataSource = null;

                if (usuarios.Count == 0)
                {
                    MessageBox.Show("No se encontraron usuarios registrados",
                                  "Información",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Information);
                    return;
                }

                // Crear DataTable con estructura completa
                DataTable dt = new DataTable();
                dt.Columns.Add("id", typeof(int));
                dt.Columns.Add("nombre_usuario", typeof(string));
                dt.Columns.Add("nombre_completo", typeof(string));
                dt.Columns.Add("correo", typeof(string));
                dt.Columns.Add("telefono", typeof(string));
                dt.Columns.Add("direccion", typeof(string));
                dt.Columns.Add("curp", typeof(string));
                dt.Columns.Add("rfc", typeof(string));
                dt.Columns.Add("fecha_nacimiento", typeof(DateTime));
                dt.Columns.Add("id_rol", typeof(int));
                dt.Columns.Add("nombre_rol", typeof(string)); // Columna para nombre del rol
                dt.Columns.Add("estatus", typeof(bool));
                dt.Columns.Add("contraseña", typeof(string));

                foreach (Usuario usuario in usuarios)
                {
                    dt.Rows.Add(
                        usuario.Id,
                        usuario.Nombre_Usuario,
                        usuario.DatosPersonales?.NombreCompleto ?? "N/A",
                        usuario.DatosPersonales?.Correo ?? "N/A",
                        usuario.DatosPersonales?.Telefono ?? "N/A",
                        usuario.DatosPersonales?.Direccion ?? "N/A",
                        usuario.DatosPersonales?.Curp ?? "N/A",
                        usuario.DatosPersonales?.Rfc ?? "N/A",
                        usuario.DatosPersonales?.FechaNacimiento ?? DateTime.MinValue,
                        usuario.IdRol,
                        usuario.Rol?.Nombre ?? "Desconocido", // Nombre del rol
                        usuario.Estatus,
                        "*******" // No mostrar contraseña real
                    );
                }

                dgvListadoUsuario.DataSource = dt;
                ConfigurarDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar usuarios: {ex.Message}",
                              "Error",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void ConfigurarDataGridView()
        {
            if (dgvListadoUsuario.Columns.Count == 0) return;

            // Ajustes generales
            dgvListadoUsuario.AllowUserToAddRows = false;
            dgvListadoUsuario.AllowUserToDeleteRows = false;
            dgvListadoUsuario.ReadOnly = true;

            // Configurar columnas
            var columnas = new[] {
        new { Name = "id", Width = 50, Visible = false },
        new { Name = "nombre_usuario", Width = 120, Visible = true },
        new { Name = "nombre_completo", Width = 200, Visible = true },
        new { Name = "correo", Width = 180, Visible = true },
        new { Name = "telefono", Width = 100, Visible = true },
        new { Name = "direccion", Width = 150, Visible = true },
        new { Name = "curp", Width = 120, Visible = true },
        new { Name = "rfc", Width = 120, Visible = true },
        new { Name = "fecha_nacimiento", Width = 100, Visible = true },
        new { Name = "id_rol", Width = 70, Visible = false }, // Ocultamos el ID del rol
        new { Name = "nombre_rol", Width = 150, Visible = true }, // Mostramos el nombre
        new { Name = "estatus", Width = 80, Visible = true },
        new { Name = "contraseña", Width = 80, Visible = false }
    };

            foreach (var col in columnas)
            {
                if (dgvListadoUsuario.Columns.Contains(col.Name))
                {
                    dgvListadoUsuario.Columns[col.Name].Width = col.Width;
                    dgvListadoUsuario.Columns[col.Name].Visible = col.Visible;
                }
            }

            // Formato para fechas
            dgvListadoUsuario.Columns["fecha_nacimiento"].DefaultCellStyle.Format = "dd/MM/yyyy";

            // Estilo de cabeceras
            dgvListadoUsuario.EnableHeadersVisualStyles = false;
            dgvListadoUsuario.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dgvListadoUsuario.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvListadoUsuario.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);

            // Alternar colores de filas
            dgvListadoUsuario.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;

            // Selección completa de fila
            dgvListadoUsuario.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void iconPIcture_Click(object sender, EventArgs e)
        {

        }
    }
}




