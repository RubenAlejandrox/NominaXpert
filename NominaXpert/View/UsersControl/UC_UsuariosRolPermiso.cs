using NominaXpert.Controller;
using System.Data;
using NominaXpert.Utilities;
using NominaXpert.Model;
namespace NominaXpert.View.UsersControl
{
    public partial class UC_UsuariosRolPermiso : UserControl
    {
        public UC_UsuariosRolPermiso()
        {
            InitializeComponent();
            CargarRoles();
        }


        private void dtgRoles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void CargarRoles()
        {
            try
            {
                RolesController controller = new RolesController();
                var roles = controller.ObtenerTodosLosRoles();

                dtgRoles.DataSource = null;

                if (roles.Count == 0)
                {
                    MessageBox.Show("No se encontraron roles registrados.",
                                    "Información",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    return;
                }

                DataTable dt = new DataTable();
                dt.Columns.Add("Id", typeof(int));
                dt.Columns.Add("Nombre", typeof(string));
                dt.Columns.Add("Descripcion", typeof(string));
                dt.Columns.Add("Estatus", typeof(string));
                dt.Columns.Add("Permisos", typeof(string));

                foreach (var rol in roles)
                {
                    dt.Rows.Add(
                        rol.Id,
                        rol.Nombre,
                        rol.Descripcion,
                        rol.Estatus ? "Activo" : "Inactivo",
                        string.Join(", ", rol.Permisos.Select(p => p.Descripcion))
                    );
                }

                dtgRoles.DataSource = dt;
                ConfigurarDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar roles: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ConfigurarDataGridView()
        {
            if (dtgRoles.Columns.Count == 0) return;

            // Ajustes generales
            dtgRoles.AllowUserToAddRows = false;
            dtgRoles.AllowUserToDeleteRows = false;
            dtgRoles.ReadOnly = true;

            // Configurar columnas
            var columnas = new[] {
        new { Name = "id", Width = 50, Visible = false },
        new { Name = "Nombre", Width = 120, Visible = true },
        new { Name = "Descripcion", Width = 200, Visible = true },
        new { Name = "Estatus", Width = 80, Visible = true },
        new { Name = "Permisos", Width = 300, Visible = true }
    };

            foreach (var col in columnas)
            {
                if (dtgRoles.Columns.Contains(col.Name))
                {
                    dtgRoles.Columns[col.Name].Width = col.Width;
                    dtgRoles.Columns[col.Name].Visible = col.Visible;
                }
            }

            // Estilo de cabeceras
            dtgRoles.EnableHeadersVisualStyles = false;
            dtgRoles.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dtgRoles.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dtgRoles.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);

            // Alternar colores de filas
            dtgRoles.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;

            // Selección completa de fila
            dtgRoles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
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

        private void dtgRoles_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ibtnEditar_Click(object sender, EventArgs e)
        {
            if (dtgRoles.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecciona un rol para editar", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Obtener el ID del rol desde la fila seleccionada
            int idRol = Convert.ToInt32(dtgRoles.SelectedRows[0].Cells["Id"].Value);

            // Obtener el rol completo (con permisos incluidos)
            RolesController controller = new RolesController();
            Rol rol = controller.ObtenerTodosLosRoles().FirstOrDefault(r => r.Id == idRol);

            if (rol != null)
            {
                // Crear el UserControl de edición
                UC_RolesAlta ucEditar = new UC_RolesAlta();

                // Mostrarlo en el contenedor
                addUsersControl(ucEditar);

                // Cargar los datos en el formulario
                ucEditar.CargarRolParaEditar(rol);
            }
        }

        private void ibtnBajaUsuario_Click(object sender, EventArgs e)
        {

        }
    }
}
