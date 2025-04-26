namespace NominaXpert.View.UsersControl
{
    partial class UC_RolesAlta
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
            label1 = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            panel2 = new Panel();
            lblSeleccionRol = new Label();
            dtgListaPermiso = new DataGridView();
            Seleccion = new DataGridViewCheckBoxColumn();
            Codigo = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            Descripcion = new DataGridViewTextBoxColumn();
            Estatus = new DataGridViewTextBoxColumn();
            PermisosAsignados = new DataGridViewTextBoxColumn();
            label4 = new Label();
            txtNombreRol = new TextBox();
            lblNombre = new Label();
            txtDescripcion = new TextBox();
            icbtnGuardar = new FontAwesome.Sharp.IconButton();
            cbxEstatus = new ComboBox();
            txtCodigo = new TextBox();
            lblCodigo = new Label();
            lblEstatus = new Label();
            Nombre = new Label();
            panel1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dtgListaPermiso).BeginInit();
            SuspendLayout();
            // 
            // ibtnGuardar
            // 
            ibtnGuardar.BackColor = Color.FromArgb(37, 41, 47);
            ibtnGuardar.FlatStyle = FlatStyle.Flat;
            ibtnGuardar.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ibtnGuardar.ForeColor = Color.FromArgb(12, 215, 253);
            ibtnGuardar.IconChar = FontAwesome.Sharp.IconChar.Save;
            ibtnGuardar.IconColor = Color.FromArgb(12, 215, 253);
            ibtnGuardar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            ibtnGuardar.IconSize = 24;
            ibtnGuardar.Location = new Point(776, 623);
            ibtnGuardar.Name = "ibtnGuardar";
            ibtnGuardar.Size = new Size(125, 41);
            ibtnGuardar.TabIndex = 105;
            ibtnGuardar.Text = "Guardar";
            ibtnGuardar.TextImageRelation = TextImageRelation.ImageBeforeText;
            ibtnGuardar.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(48, 51, 59);
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1262, 67);
            panel1.TabIndex = 106;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.FromArgb(48, 51, 59);
            label1.Font = new Font("Corbel", 16F, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(12, 215, 253);
            label1.Location = new Point(28, 17);
            label1.Name = "label1";
            label1.Size = new Size(326, 33);
            label1.TabIndex = 0;
            label1.Text = "Formulario de alta de roles";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            tableLayoutPanel1.Controls.Add(panel2, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1262, 705);
            tableLayoutPanel1.TabIndex = 107;
            // 
            // panel2
            // 
            panel2.Controls.Add(lblSeleccionRol);
            panel2.Controls.Add(dtgListaPermiso);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(txtNombreRol);
            panel2.Controls.Add(lblNombre);
            panel2.Controls.Add(txtDescripcion);
            panel2.Controls.Add(icbtnGuardar);
            panel2.Controls.Add(cbxEstatus);
            panel2.Controls.Add(txtCodigo);
            panel2.Controls.Add(lblCodigo);
            panel2.Controls.Add(lblEstatus);
            panel2.Controls.Add(Nombre);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(192, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(877, 699);
            panel2.TabIndex = 0;
            // 
            // lblSeleccionRol
            // 
            lblSeleccionRol.AutoSize = true;
            lblSeleccionRol.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            lblSeleccionRol.ForeColor = Color.FromArgb(12, 215, 253);
            lblSeleccionRol.Location = new Point(85, 320);
            lblSeleccionRol.Name = "lblSeleccionRol";
            lblSeleccionRol.Size = new Size(296, 23);
            lblSeleccionRol.TabIndex = 102;
            lblSeleccionRol.Text = "Seleccione los Permisos de este Rol*";
            // 
            // dtgListaPermiso
            // 
            dtgListaPermiso.BackgroundColor = Color.FromArgb(48, 51, 59);
            dtgListaPermiso.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;
            dtgListaPermiso.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dtgListaPermiso.Columns.AddRange(new DataGridViewColumn[] { Seleccion, Codigo, dataGridViewTextBoxColumn1, Descripcion, Estatus, PermisosAsignados });
            dtgListaPermiso.GridColor = Color.MidnightBlue;
            dtgListaPermiso.Location = new Point(89, 357);
            dtgListaPermiso.Name = "dtgListaPermiso";
            dtgListaPermiso.RowHeadersWidth = 51;
            dtgListaPermiso.Size = new Size(677, 179);
            dtgListaPermiso.TabIndex = 101;
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
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            label4.ForeColor = Color.FromArgb(12, 215, 253);
            label4.Location = new Point(85, 267);
            label4.Name = "label4";
            label4.Size = new Size(111, 23);
            label4.TabIndex = 100;
            label4.Text = "Descripción*";
            // 
            // txtNombreRol
            // 
            txtNombreRol.BackColor = Color.FromArgb(37, 41, 47);
            txtNombreRol.Cursor = Cursors.IBeam;
            txtNombreRol.Font = new Font("Century", 10F);
            txtNombreRol.ForeColor = Color.FromArgb(12, 215, 253);
            txtNombreRol.Location = new Point(261, 151);
            txtNombreRol.MaxLength = 50;
            txtNombreRol.Name = "txtNombreRol";
            txtNombreRol.Size = new Size(505, 28);
            txtNombreRol.TabIndex = 99;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            lblNombre.ForeColor = Color.FromArgb(12, 215, 253);
            lblNombre.Location = new Point(82, 155);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(84, 23);
            lblNombre.TabIndex = 98;
            lblNombre.Text = "Nombre*";
            // 
            // txtDescripcion
            // 
            txtDescripcion.BackColor = Color.FromArgb(37, 41, 47);
            txtDescripcion.Cursor = Cursors.IBeam;
            txtDescripcion.Font = new Font("Century", 10F);
            txtDescripcion.ForeColor = Color.FromArgb(12, 215, 253);
            txtDescripcion.Location = new Point(261, 264);
            txtDescripcion.MaxLength = 100;
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new Size(505, 28);
            txtDescripcion.TabIndex = 97;
            // 
            // icbtnGuardar
            // 
            icbtnGuardar.BackColor = Color.FromArgb(37, 41, 47);
            icbtnGuardar.FlatStyle = FlatStyle.Flat;
            icbtnGuardar.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            icbtnGuardar.ForeColor = Color.FromArgb(12, 215, 253);
            icbtnGuardar.IconChar = FontAwesome.Sharp.IconChar.Save;
            icbtnGuardar.IconColor = Color.FromArgb(12, 215, 253);
            icbtnGuardar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            icbtnGuardar.IconSize = 24;
            icbtnGuardar.Location = new Point(641, 594);
            icbtnGuardar.Name = "icbtnGuardar";
            icbtnGuardar.Size = new Size(125, 50);
            icbtnGuardar.TabIndex = 95;
            icbtnGuardar.Text = "Guardar";
            icbtnGuardar.TextImageRelation = TextImageRelation.ImageBeforeText;
            icbtnGuardar.UseVisualStyleBackColor = false;
            icbtnGuardar.Click += icbtnGuardar_Click;
            // 
            // cbxEstatus
            // 
            cbxEstatus.BackColor = Color.FromArgb(24, 44, 65);
            cbxEstatus.Cursor = Cursors.Hand;
            cbxEstatus.Font = new Font("Century", 10F);
            cbxEstatus.ForeColor = Color.FromArgb(12, 215, 253);
            cbxEstatus.FormattingEnabled = true;
            cbxEstatus.Location = new Point(261, 205);
            cbxEstatus.Name = "cbxEstatus";
            cbxEstatus.Size = new Size(505, 29);
            cbxEstatus.TabIndex = 85;
            // 
            // txtCodigo
            // 
            txtCodigo.BackColor = Color.FromArgb(37, 41, 47);
            txtCodigo.Cursor = Cursors.IBeam;
            txtCodigo.Font = new Font("Century", 10F);
            txtCodigo.ForeColor = Color.FromArgb(12, 215, 253);
            txtCodigo.Location = new Point(261, 97);
            txtCodigo.MaxLength = 50;
            txtCodigo.Name = "txtCodigo";
            txtCodigo.Size = new Size(505, 28);
            txtCodigo.TabIndex = 84;
            // 
            // lblCodigo
            // 
            lblCodigo.AutoSize = true;
            lblCodigo.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            lblCodigo.ForeColor = Color.FromArgb(12, 215, 253);
            lblCodigo.Location = new Point(82, 101);
            lblCodigo.Name = "lblCodigo";
            lblCodigo.Size = new Size(76, 23);
            lblCodigo.TabIndex = 82;
            lblCodigo.Text = "Código*";
            // 
            // lblEstatus
            // 
            lblEstatus.AutoSize = true;
            lblEstatus.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            lblEstatus.ForeColor = Color.FromArgb(12, 215, 253);
            lblEstatus.Location = new Point(82, 212);
            lblEstatus.Name = "lblEstatus";
            lblEstatus.Size = new Size(74, 23);
            lblEstatus.TabIndex = 80;
            lblEstatus.Text = "Estatus*";
            // 
            // Nombre
            // 
            Nombre.AutoSize = true;
            Nombre.Font = new Font("Century", 10F);
            Nombre.ForeColor = Color.FromArgb(12, 215, 253);
            Nombre.Location = new Point(25, 37);
            Nombre.Name = "Nombre";
            Nombre.Size = new Size(83, 21);
            Nombre.TabIndex = 79;
            Nombre.Text = "Nombre*";
            // 
            // UC_RolesAlta
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(37, 41, 47);
            Controls.Add(panel1);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(ibtnGuardar);
            Name = "UC_RolesAlta";
            Size = new Size(1262, 705);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dtgListaPermiso).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private FontAwesome.Sharp.IconButton ibtnGuardar;
        private Panel panel1;
        private Label label1;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel2;
        private Label lblSeleccionRol;
        private DataGridView dtgListaPermiso;
        private Label label4;
        private TextBox txtNombreRol;
        private Label lblNombre;
        private TextBox txtDescripcion;
        private FontAwesome.Sharp.IconButton icbtnGuardar;
        private ComboBox cbxEstatus;
        private TextBox txtCodigo;
        private Label lblCodigo;
        private Label lblEstatus;
        private Label Nombre;
        private DataGridViewCheckBoxColumn Seleccion;
        private DataGridViewTextBoxColumn Codigo;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn Descripcion;
        private DataGridViewTextBoxColumn Estatus;
        private DataGridViewTextBoxColumn PermisosAsignados;
    }
}
