namespace NominaXpert.View.UsersControl
{
    partial class UC_UsuariosRolPermiso
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            panel = new Panel();
            btnSearch = new Button();
            txtSearchEmpleado = new TextBox();
            iconPIcture = new FontAwesome.Sharp.IconPictureBox();
            dtgListaRoles = new DataGridView();
            Seleccion = new DataGridViewCheckBoxColumn();
            Codigo = new DataGridViewTextBoxColumn();
            Nombre = new DataGridViewTextBoxColumn();
            Descripcion = new DataGridViewTextBoxColumn();
            Estatus = new DataGridViewTextBoxColumn();
            PermisosAsignados = new DataGridViewTextBoxColumn();
            panel2 = new Panel();
            label1 = new Label();
            panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconPIcture).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dtgListaRoles).BeginInit();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel
            // 
            panel.Controls.Add(btnSearch);
            panel.Controls.Add(txtSearchEmpleado);
            panel.Controls.Add(iconPIcture);
            panel.Controls.Add(dtgListaRoles);
            panel.Controls.Add(panel2);
            panel.Dock = DockStyle.Fill;
            panel.Location = new Point(0, 0);
            panel.Name = "panel";
            panel.Size = new Size(1262, 705);
            panel.TabIndex = 0;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.Black;
            btnSearch.FlatStyle = FlatStyle.Popup;
            btnSearch.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSearch.ForeColor = Color.White;
            btnSearch.Location = new Point(720, 169);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(94, 29);
            btnSearch.TabIndex = 37;
            btnSearch.Text = "Buscar";
            btnSearch.UseVisualStyleBackColor = false;
            // 
            // txtSearchEmpleado
            // 
            txtSearchEmpleado.BackColor = Color.FromArgb(37, 41, 47);
            txtSearchEmpleado.BorderStyle = BorderStyle.FixedSingle;
            txtSearchEmpleado.ForeColor = Color.LightGray;
            txtSearchEmpleado.Location = new Point(98, 169);
            txtSearchEmpleado.Name = "txtSearchEmpleado";
            txtSearchEmpleado.Size = new Size(585, 27);
            txtSearchEmpleado.TabIndex = 36;
            txtSearchEmpleado.Text = "Buscar rol";
            // 
            // iconPIcture
            // 
            iconPIcture.BackColor = Color.FromArgb(37, 41, 47);
            iconPIcture.ForeColor = Color.FromArgb(12, 215, 253);
            iconPIcture.IconChar = FontAwesome.Sharp.IconChar.Search;
            iconPIcture.IconColor = Color.FromArgb(12, 215, 253);
            iconPIcture.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPIcture.IconSize = 36;
            iconPIcture.Location = new Point(52, 160);
            iconPIcture.Name = "iconPIcture";
            iconPIcture.Size = new Size(40, 36);
            iconPIcture.TabIndex = 35;
            iconPIcture.TabStop = false;
            // 
            // dtgListaRoles
            // 
            dtgListaRoles.BackgroundColor = Color.FromArgb(48, 51, 59);
            dtgListaRoles.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;
            dtgListaRoles.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dtgListaRoles.Columns.AddRange(new DataGridViewColumn[] { Seleccion, Codigo, Nombre, Descripcion, Estatus, PermisosAsignados });
            dtgListaRoles.GridColor = Color.MidnightBlue;
            dtgListaRoles.Location = new Point(52, 204);
            dtgListaRoles.Name = "dtgListaRoles";
            dtgListaRoles.RowHeadersWidth = 51;
            dtgListaRoles.Size = new Size(799, 341);
            dtgListaRoles.TabIndex = 34;
            // 
            // Seleccion
            // 
            Seleccion.HeaderText = "Selección";
            Seleccion.MinimumWidth = 6;
            Seleccion.Name = "Seleccion";
            Seleccion.Width = 125;
            // 
            // Codigo
            // 
            Codigo.HeaderText = "Código";
            Codigo.MinimumWidth = 6;
            Codigo.Name = "Codigo";
            Codigo.Width = 125;
            // 
            // Nombre
            // 
            Nombre.HeaderText = "Nombre";
            Nombre.MinimumWidth = 6;
            Nombre.Name = "Nombre";
            Nombre.Width = 125;
            // 
            // Descripcion
            // 
            Descripcion.HeaderText = "Descripción";
            Descripcion.MinimumWidth = 6;
            Descripcion.Name = "Descripcion";
            Descripcion.Width = 125;
            // 
            // Estatus
            // 
            Estatus.HeaderText = "Estatus";
            Estatus.MinimumWidth = 6;
            Estatus.Name = "Estatus";
            Estatus.Width = 125;
            // 
            // PermisosAsignados
            // 
            PermisosAsignados.HeaderText = "Permisos Asignados";
            PermisosAsignados.MinimumWidth = 6;
            PermisosAsignados.Name = "PermisosAsignados";
            PermisosAsignados.Width = 125;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(48, 51, 59);
            panel2.Controls.Add(label1);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(1262, 84);
            panel2.TabIndex = 33;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Corbel", 16.2F, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(12, 245, 253);
            label1.Location = new Point(52, 28);
            label1.Name = "label1";
            label1.Size = new Size(81, 35);
            label1.TabIndex = 0;
            label1.Text = "Roles";
            // 
            // UC_UsuariosRolPermiso
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(37, 41, 47);
            Controls.Add(panel);
            ForeColor = Color.FromArgb(12, 215, 253);
            Name = "UC_UsuariosRolPermiso";
            Size = new Size(1262, 705);
            panel.ResumeLayout(false);
            panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)iconPIcture).EndInit();
            ((System.ComponentModel.ISupportInitialize)dtgListaRoles).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel;
        private Button btnSearch;
        private TextBox txtSearchEmpleado;
        private FontAwesome.Sharp.IconPictureBox iconPIcture;
        private DataGridView dtgListaRoles;
        private Panel panel2;
        private Label label1;
        private DataGridViewCheckBoxColumn Seleccion;
        private DataGridViewTextBoxColumn Codigo;
        private DataGridViewTextBoxColumn Nombre;
        private DataGridViewTextBoxColumn Descripcion;
        private DataGridViewTextBoxColumn Estatus;
        private DataGridViewTextBoxColumn PermisosAsignados;
    }
}
