using NominaXpert.Utilities;
namespace NominaXpert.View.UsersControl
{
    public partial class UC_UsuariosRolPermiso : UserControl
    {
        public UC_UsuariosRolPermiso()
        {
            InitializeComponent();
            InitializeComponent();
            ConfigurarDataGridView(); 
        }
        private void ConfigurarDataGridView()
        {
            Formas.InitializePanel(panel); // Inicializa el borde izquierdo en el panel

            // Configurar el color de los bordes de las celdas
            dtgListaRoles.GridColor = Color.FromArgb(12, 215, 253); // Bordes internos de las celdas

            // Configurar el estilo del encabezado (fondo blanco y letras personalizadas)
            dtgListaRoles.ColumnHeadersDefaultCellStyle.BackColor = Color.White; // Fondo blanco
            dtgListaRoles.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(12, 215, 253); // Letras color (12, 215, 253)
            dtgListaRoles.EnableHeadersVisualStyles = false; // Deshabilitar estilos visuales para aplicar los colores personalizados

            // Configurar el estilo del cuerpo (fondo oscuro y letras personalizadas)
            dtgListaRoles.DefaultCellStyle.BackColor = Color.FromArgb(37, 41, 47); // Fondo oscuro (15, 15, 15)
            dtgListaRoles.DefaultCellStyle.ForeColor = Color.FromArgb(12, 215, 253); // Letras color (12, 215, 253)
            dtgListaRoles.BackgroundColor = Color.FromArgb(37, 41, 47); // Fondo de la tabla (15, 15, 15)

            // Configurar los bordes del encabezado
            dtgListaRoles.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dtgListaRoles.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(12, 215, 253); // Color del borde del encabezado

            // Configurar los bordes externos del DataGridView
            dtgListaRoles.BorderStyle = BorderStyle.FixedSingle;
            dtgListaRoles.CellBorderStyle = DataGridViewCellBorderStyle.Single;
        }
    }
}
