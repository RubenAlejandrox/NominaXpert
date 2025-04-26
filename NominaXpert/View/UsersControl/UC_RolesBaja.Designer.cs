namespace NominaXpert.View.UsersControl
{
    partial class UC_RolesBaja
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
            iconPIcture = new FontAwesome.Sharp.IconPictureBox();
            btnSearch = new Button();
            txtSearchEmpleado = new TextBox();
            dtpFechaBaja = new Utilities.NominaDatePicker();
            panel1 = new Panel();
            lblTituloBaja = new Label();
            cbxMotivoBaja = new ComboBox();
            lblMotivoBaja = new Label();
            lblSeleccionUsuario = new Label();
            cbxSeleccionUsuario = new ComboBox();
            label10 = new Label();
            txtDetallesBaja = new TextBox();
            lblNotas = new Label();
            ibtnGuardar = new FontAwesome.Sharp.IconButton();
            ((System.ComponentModel.ISupportInitialize)iconPIcture).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // iconPIcture
            // 
            iconPIcture.BackColor = Color.FromArgb(37, 41, 47);
            iconPIcture.ForeColor = Color.FromArgb(12, 215, 253);
            iconPIcture.IconChar = FontAwesome.Sharp.IconChar.Search;
            iconPIcture.IconColor = Color.FromArgb(12, 215, 253);
            iconPIcture.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPIcture.IconSize = 36;
            iconPIcture.Location = new Point(59, 165);
            iconPIcture.Name = "iconPIcture";
            iconPIcture.Size = new Size(40, 36);
            iconPIcture.TabIndex = 123;
            iconPIcture.TabStop = false;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.Black;
            btnSearch.FlatStyle = FlatStyle.Popup;
            btnSearch.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSearch.ForeColor = Color.White;
            btnSearch.Location = new Point(727, 174);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(94, 29);
            btnSearch.TabIndex = 122;
            btnSearch.Text = "Buscar";
            btnSearch.UseVisualStyleBackColor = false;
            // 
            // txtSearchEmpleado
            // 
            txtSearchEmpleado.BackColor = Color.FromArgb(37, 41, 47);
            txtSearchEmpleado.BorderStyle = BorderStyle.FixedSingle;
            txtSearchEmpleado.ForeColor = Color.LightGray;
            txtSearchEmpleado.Location = new Point(105, 174);
            txtSearchEmpleado.Name = "txtSearchEmpleado";
            txtSearchEmpleado.Size = new Size(585, 27);
            txtSearchEmpleado.TabIndex = 121;
            txtSearchEmpleado.Text = "Buscar empleado";
            // 
            // dtpFechaBaja
            // 
            dtpFechaBaja.BorderColor = Color.FromArgb(12, 215, 253);
            dtpFechaBaja.BorderSize = 2;
            dtpFechaBaja.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dtpFechaBaja.Format = DateTimePickerFormat.Short;
            dtpFechaBaja.Location = new Point(213, 470);
            dtpFechaBaja.MinimumSize = new Size(0, 35);
            dtpFechaBaja.Name = "dtpFechaBaja";
            dtpFechaBaja.Size = new Size(147, 35);
            dtpFechaBaja.SkinColor = Color.FromArgb(48, 51, 59);
            dtpFechaBaja.TabIndex = 120;
            dtpFechaBaja.TextColor = Color.FromArgb(12, 215, 253);
            dtpFechaBaja.Value = new DateTime(2025, 3, 12, 1, 10, 15, 0);
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(48, 51, 59);
            panel1.Controls.Add(lblTituloBaja);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1262, 85);
            panel1.TabIndex = 111;
            // 
            // lblTituloBaja
            // 
            lblTituloBaja.AutoSize = true;
            lblTituloBaja.Font = new Font("Corbel", 16F, FontStyle.Bold);
            lblTituloBaja.ForeColor = Color.FromArgb(12, 215, 253);
            lblTituloBaja.Location = new Point(34, 28);
            lblTituloBaja.Name = "lblTituloBaja";
            lblTituloBaja.Size = new Size(171, 33);
            lblTituloBaja.TabIndex = 0;
            lblTituloBaja.Text = "Baja de Roles";
            // 
            // cbxMotivoBaja
            // 
            cbxMotivoBaja.BackColor = Color.FromArgb(37, 41, 47);
            cbxMotivoBaja.Cursor = Cursors.Hand;
            cbxMotivoBaja.Font = new Font("Century", 10F);
            cbxMotivoBaja.ForeColor = Color.FromArgb(12, 215, 253);
            cbxMotivoBaja.FormattingEnabled = true;
            cbxMotivoBaja.Location = new Point(213, 352);
            cbxMotivoBaja.Name = "cbxMotivoBaja";
            cbxMotivoBaja.Size = new Size(688, 29);
            cbxMotivoBaja.TabIndex = 118;
            // 
            // lblMotivoBaja
            // 
            lblMotivoBaja.AutoSize = true;
            lblMotivoBaja.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            lblMotivoBaja.ForeColor = Color.FromArgb(12, 215, 253);
            lblMotivoBaja.Location = new Point(213, 328);
            lblMotivoBaja.Name = "lblMotivoBaja";
            lblMotivoBaja.Size = new Size(114, 23);
            lblMotivoBaja.TabIndex = 112;
            lblMotivoBaja.Text = "Motivo Baja*";
            // 
            // lblSeleccionUsuario
            // 
            lblSeleccionUsuario.AutoSize = true;
            lblSeleccionUsuario.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            lblSeleccionUsuario.ForeColor = Color.FromArgb(12, 215, 253);
            lblSeleccionUsuario.Location = new Point(213, 220);
            lblSeleccionUsuario.Name = "lblSeleccionUsuario";
            lblSeleccionUsuario.Size = new Size(139, 23);
            lblSeleccionUsuario.TabIndex = 113;
            lblSeleccionUsuario.Text = "Seleccionar Rol*";
            // 
            // cbxSeleccionUsuario
            // 
            cbxSeleccionUsuario.BackColor = Color.FromArgb(37, 41, 47);
            cbxSeleccionUsuario.Cursor = Cursors.Hand;
            cbxSeleccionUsuario.Font = new Font("Century", 10F);
            cbxSeleccionUsuario.ForeColor = Color.FromArgb(12, 215, 253);
            cbxSeleccionUsuario.FormattingEnabled = true;
            cbxSeleccionUsuario.Location = new Point(213, 246);
            cbxSeleccionUsuario.Name = "cbxSeleccionUsuario";
            cbxSeleccionUsuario.Size = new Size(688, 29);
            cbxSeleccionUsuario.TabIndex = 117;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            label10.ForeColor = Color.FromArgb(12, 215, 253);
            label10.Location = new Point(213, 446);
            label10.Name = "label10";
            label10.Size = new Size(102, 23);
            label10.TabIndex = 114;
            label10.Text = "Fecha Baja*";
            // 
            // txtDetallesBaja
            // 
            txtDetallesBaja.BackColor = Color.FromArgb(37, 41, 47);
            txtDetallesBaja.Cursor = Cursors.IBeam;
            txtDetallesBaja.Font = new Font("Century", 10F);
            txtDetallesBaja.ForeColor = Color.FromArgb(12, 215, 253);
            txtDetallesBaja.Location = new Point(213, 572);
            txtDetallesBaja.MaxLength = 100;
            txtDetallesBaja.Name = "txtDetallesBaja";
            txtDetallesBaja.Size = new Size(688, 28);
            txtDetallesBaja.TabIndex = 116;
            // 
            // lblNotas
            // 
            lblNotas.AutoSize = true;
            lblNotas.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            lblNotas.ForeColor = Color.FromArgb(12, 215, 253);
            lblNotas.Location = new Point(213, 548);
            lblNotas.Name = "lblNotas";
            lblNotas.Size = new Size(121, 23);
            lblNotas.TabIndex = 115;
            lblNotas.Text = "Detalles Baja*";
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
            ibtnGuardar.Location = new Point(776, 635);
            ibtnGuardar.Name = "ibtnGuardar";
            ibtnGuardar.Size = new Size(125, 41);
            ibtnGuardar.TabIndex = 119;
            ibtnGuardar.Text = "Guardar";
            ibtnGuardar.TextImageRelation = TextImageRelation.ImageBeforeText;
            ibtnGuardar.UseVisualStyleBackColor = false;
            ibtnGuardar.Click += ibtnGuardar_Click;
            // 
            // UC_RolesBaja
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(37, 41, 47);
            Controls.Add(iconPIcture);
            Controls.Add(btnSearch);
            Controls.Add(txtSearchEmpleado);
            Controls.Add(dtpFechaBaja);
            Controls.Add(ibtnGuardar);
            Controls.Add(panel1);
            Controls.Add(cbxMotivoBaja);
            Controls.Add(lblMotivoBaja);
            Controls.Add(lblSeleccionUsuario);
            Controls.Add(cbxSeleccionUsuario);
            Controls.Add(label10);
            Controls.Add(txtDetallesBaja);
            Controls.Add(lblNotas);
            Name = "UC_RolesBaja";
            Size = new Size(1262, 705);
            ((System.ComponentModel.ISupportInitialize)iconPIcture).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FontAwesome.Sharp.IconPictureBox iconPIcture;
        private Button btnSearch;
        private TextBox txtSearchEmpleado;
        private Utilities.NominaDatePicker dtpFechaBaja;
        private Panel panel1;
        private Label lblTituloBaja;
        private ComboBox cbxMotivoBaja;
        private Label lblMotivoBaja;
        private Label lblSeleccionUsuario;
        private ComboBox cbxSeleccionUsuario;
        private Label label10;
        private TextBox txtDetallesBaja;
        private Label lblNotas;
        private FontAwesome.Sharp.IconButton ibtnGuardar;
    }
}
