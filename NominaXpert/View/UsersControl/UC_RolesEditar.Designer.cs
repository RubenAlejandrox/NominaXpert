namespace NominaXpert.View.UsersControl
{
    partial class UC_RolesEditar
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
            ibtnGuardar = new FontAwesome.Sharp.IconButton();
            panel1 = new Panel();
            lblEditarUsuario = new Label();
            lblEstatus = new Label();
            lblCodigo = new Label();
            txtCodigo = new TextBox();
            cbxEstatus = new ComboBox();
            dtgListaPermiso = new DataGridView();
            Seleccion = new DataGridViewCheckBoxColumn();
            Codigo = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            Descripcion = new DataGridViewTextBoxColumn();
            Estatus = new DataGridViewTextBoxColumn();
            PermisosAsignados = new DataGridViewTextBoxColumn();
            txtDescripcion = new TextBox();
            lblNombre = new Label();
            txtNombreRol = new TextBox();
            label4 = new Label();
            lblSeleccionRol = new Label();
            txtSearchEmpleado = new TextBox();
            iconPIcture = new FontAwesome.Sharp.IconPictureBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtgListaPermiso).BeginInit();
            ((System.ComponentModel.ISupportInitialize)iconPIcture).BeginInit();
            SuspendLayout();
            // 
            // ibtnGuardar
            // 
            ibtnGuardar.BackColor = Color.FromArgb(37, 41, 47);
            ibtnGuardar.FlatStyle = FlatStyle.Flat;
            ibtnGuardar.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ibtnGuardar.ForeColor = Color.FromArgb(12, 215, 253);
            ibtnGuardar.IconChar = FontAwesome.Sharp.IconChar.Save;
            ibtnGuardar.IconColor = Color.FromArgb(12, 215, 253);
            ibtnGuardar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            ibtnGuardar.IconSize = 24;
            ibtnGuardar.Location = new Point(610, 597);
            ibtnGuardar.Name = "ibtnGuardar";
            ibtnGuardar.Size = new Size(196, 62);
            ibtnGuardar.TabIndex = 87;
            ibtnGuardar.Text = "Guardar Cambios";
            ibtnGuardar.TextImageRelation = TextImageRelation.ImageBeforeText;
            ibtnGuardar.UseVisualStyleBackColor = false;
            ibtnGuardar.Click += ibtnGuardar_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(48, 51, 59);
            panel1.Controls.Add(lblEditarUsuario);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1262, 67);
            panel1.TabIndex = 75;
            // 
            // lblEditarUsuario
            // 
            lblEditarUsuario.AutoSize = true;
            lblEditarUsuario.Font = new Font("Corbel", 16F, FontStyle.Bold);
            lblEditarUsuario.ForeColor = Color.FromArgb(12, 215, 253);
            lblEditarUsuario.Location = new Point(36, 17);
            lblEditarUsuario.Name = "lblEditarUsuario";
            lblEditarUsuario.Size = new Size(129, 33);
            lblEditarUsuario.TabIndex = 0;
            lblEditarUsuario.Text = "Editar Rol";
            // 
            // lblEstatus
            // 
            lblEstatus.AutoSize = true;
            lblEstatus.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            lblEstatus.ForeColor = Color.FromArgb(12, 215, 253);
            lblEstatus.Location = new Point(122, 247);
            lblEstatus.Name = "lblEstatus";
            lblEstatus.Size = new Size(74, 23);
            lblEstatus.TabIndex = 103;
            lblEstatus.Text = "Estatus*";
            // 
            // lblCodigo
            // 
            lblCodigo.AutoSize = true;
            lblCodigo.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            lblCodigo.ForeColor = Color.FromArgb(12, 215, 253);
            lblCodigo.Location = new Point(122, 136);
            lblCodigo.Name = "lblCodigo";
            lblCodigo.Size = new Size(76, 23);
            lblCodigo.TabIndex = 104;
            lblCodigo.Text = "Código*";
            // 
            // txtCodigo
            // 
            txtCodigo.BackColor = Color.FromArgb(37, 41, 47);
            txtCodigo.Cursor = Cursors.IBeam;
            txtCodigo.Font = new Font("Century", 10F);
            txtCodigo.ForeColor = Color.FromArgb(12, 215, 253);
            txtCodigo.Location = new Point(301, 132);
            txtCodigo.MaxLength = 50;
            txtCodigo.Name = "txtCodigo";
            txtCodigo.Size = new Size(505, 28);
            txtCodigo.TabIndex = 105;
            // 
            // cbxEstatus
            // 
            cbxEstatus.BackColor = Color.FromArgb(37, 41, 47);
            cbxEstatus.Cursor = Cursors.Hand;
            cbxEstatus.Font = new Font("Century", 10F);
            cbxEstatus.ForeColor = Color.FromArgb(12, 215, 253);
            cbxEstatus.FormattingEnabled = true;
            cbxEstatus.Location = new Point(301, 240);
            cbxEstatus.Name = "cbxEstatus";
            cbxEstatus.Size = new Size(505, 29);
            cbxEstatus.TabIndex = 106;
            // 
            // dtgListaPermiso
            // 
            dtgListaPermiso.BackgroundColor = Color.FromArgb(48, 51, 59);
            dtgListaPermiso.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;
            dtgListaPermiso.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dtgListaPermiso.Columns.AddRange(new DataGridViewColumn[] { Seleccion, Codigo, dataGridViewTextBoxColumn1, Descripcion, Estatus, PermisosAsignados });
            dtgListaPermiso.GridColor = Color.MidnightBlue;
            dtgListaPermiso.Location = new Point(129, 392);
            dtgListaPermiso.Name = "dtgListaPermiso";
            dtgListaPermiso.RowHeadersWidth = 51;
            dtgListaPermiso.Size = new Size(677, 179);
            dtgListaPermiso.TabIndex = 112;
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
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.HeaderText = "Nombre";
            dataGridViewTextBoxColumn1.MinimumWidth = 6;
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.Width = 125;
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
            // txtDescripcion
            // 
            txtDescripcion.BackColor = Color.FromArgb(37, 41, 47);
            txtDescripcion.Cursor = Cursors.IBeam;
            txtDescripcion.Font = new Font("Century", 10F);
            txtDescripcion.ForeColor = Color.FromArgb(12, 215, 253);
            txtDescripcion.Location = new Point(301, 299);
            txtDescripcion.MaxLength = 100;
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new Size(505, 28);
            txtDescripcion.TabIndex = 108;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            lblNombre.ForeColor = Color.FromArgb(12, 215, 253);
            lblNombre.Location = new Point(122, 190);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(84, 23);
            lblNombre.TabIndex = 109;
            lblNombre.Text = "Nombre*";
            // 
            // txtNombreRol
            // 
            txtNombreRol.BackColor = Color.FromArgb(37, 41, 47);
            txtNombreRol.Cursor = Cursors.IBeam;
            txtNombreRol.Font = new Font("Century", 10F);
            txtNombreRol.ForeColor = Color.FromArgb(12, 215, 253);
            txtNombreRol.Location = new Point(301, 186);
            txtNombreRol.MaxLength = 50;
            txtNombreRol.Name = "txtNombreRol";
            txtNombreRol.Size = new Size(505, 28);
            txtNombreRol.TabIndex = 110;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            label4.ForeColor = Color.FromArgb(12, 215, 253);
            label4.Location = new Point(125, 302);
            label4.Name = "label4";
            label4.Size = new Size(111, 23);
            label4.TabIndex = 111;
            label4.Text = "Descripción*";
            // 
            // lblSeleccionRol
            // 
            lblSeleccionRol.AutoSize = true;
            lblSeleccionRol.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            lblSeleccionRol.ForeColor = Color.FromArgb(12, 215, 253);
            lblSeleccionRol.Location = new Point(125, 355);
            lblSeleccionRol.Name = "lblSeleccionRol";
            lblSeleccionRol.Size = new Size(296, 23);
            lblSeleccionRol.TabIndex = 113;
            lblSeleccionRol.Text = "Seleccione los Permisos de este Rol*";
            // 
            // txtSearchEmpleado
            // 
            txtSearchEmpleado.BackColor = Color.FromArgb(37, 41, 47);
            txtSearchEmpleado.BorderStyle = BorderStyle.FixedSingle;
            txtSearchEmpleado.ForeColor = Color.LightGray;
            txtSearchEmpleado.Location = new Point(59, 88);
            txtSearchEmpleado.Name = "txtSearchEmpleado";
            txtSearchEmpleado.Size = new Size(585, 27);
            txtSearchEmpleado.TabIndex = 115;
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
            iconPIcture.Location = new Point(13, 79);
            iconPIcture.Name = "iconPIcture";
            iconPIcture.Size = new Size(40, 36);
            iconPIcture.TabIndex = 114;
            iconPIcture.TabStop = false;
            // 
            // UC_RolesEditar
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(37, 41, 47);
            Controls.Add(txtSearchEmpleado);
            Controls.Add(iconPIcture);
            Controls.Add(lblSeleccionRol);
            Controls.Add(dtgListaPermiso);
            Controls.Add(label4);
            Controls.Add(txtNombreRol);
            Controls.Add(lblNombre);
            Controls.Add(txtDescripcion);
            Controls.Add(cbxEstatus);
            Controls.Add(txtCodigo);
            Controls.Add(lblCodigo);
            Controls.Add(lblEstatus);
            Controls.Add(ibtnGuardar);
            Controls.Add(panel1);
            Name = "UC_RolesEditar";
            Size = new Size(1262, 705);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dtgListaPermiso).EndInit();
            ((System.ComponentModel.ISupportInitialize)iconPIcture).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private FontAwesome.Sharp.IconButton ibtnGuardar;
        private Panel panel1;
        private Label lblEditarUsuario;
        private Label lblEstatus;
        private Label lblCodigo;
        private TextBox txtCodigo;
        private ComboBox cbxEstatus;
        private DataGridView dtgListaPermiso;
        private TextBox txtDescripcion;
        private Label lblNombre;
        private TextBox txtNombreRol;
        private Label label4;
        private Label lblSeleccionRol;
        private TextBox txtSearchEmpleado;
        private FontAwesome.Sharp.IconPictureBox iconPIcture;
        private DataGridViewCheckBoxColumn Seleccion;
        private DataGridViewTextBoxColumn Codigo;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn Descripcion;
        private DataGridViewTextBoxColumn Estatus;
        private DataGridViewTextBoxColumn PermisosAsignados;
    }
}
