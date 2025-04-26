namespace NominaXpert.View.UsersControl
{
    partial class UC_UsuariosAlta
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
            panel1 = new Panel();
            label1 = new Label();
            panel3 = new Panel();
            txtRfc = new TextBox();
            lblRFC = new Label();
            txtDireccion = new TextBox();
            lblDireccion = new Label();
            txtNomUsuario = new TextBox();
            txtContraseña = new TextBox();
            label9 = new Label();
            label7 = new Label();
            label2 = new Label();
            label3 = new Label();
            cbxRoles = new ComboBox();
            lblRol = new Label();
            cbxEstatus = new ComboBox();
            label6 = new Label();
            ibtnGuardar = new FontAwesome.Sharp.IconButton();
            panel2 = new Panel();
            txtCurp = new TextBox();
            lblCurp = new Label();
            txtApellidoMaterno = new TextBox();
            lblApellidoMaterno = new Label();
            txtCorreo = new TextBox();
            lblCorreo = new Label();
            dtpFechaNacimiento = new DateTimePicker();
            cbxGenero = new ComboBox();
            txtApellido = new TextBox();
            txtNombre = new TextBox();
            lblApellidoParterno = new Label();
            label10 = new Label();
            label5 = new Label();
            Nombre = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            panel1.SuspendLayout();
            panel3.SuspendLayout();
            panel2.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(48, 51, 59);
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1262, 67);
            panel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.FromArgb(48, 51, 59);
            label1.Font = new Font("Corbel", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(12, 215, 253);
            label1.Location = new Point(20, 13);
            label1.Name = "label1";
            label1.Size = new Size(380, 35);
            label1.TabIndex = 0;
            label1.Text = "Formulario de alta de usuarios";
            // 
            // panel3
            // 
            panel3.Controls.Add(txtRfc);
            panel3.Controls.Add(lblRFC);
            panel3.Controls.Add(txtDireccion);
            panel3.Controls.Add(lblDireccion);
            panel3.Controls.Add(txtNomUsuario);
            panel3.Controls.Add(txtContraseña);
            panel3.Controls.Add(label9);
            panel3.Controls.Add(label7);
            panel3.Controls.Add(label2);
            panel3.Controls.Add(label3);
            panel3.Controls.Add(cbxRoles);
            panel3.Controls.Add(lblRol);
            panel3.Controls.Add(cbxEstatus);
            panel3.Controls.Add(label6);
            panel3.Controls.Add(ibtnGuardar);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(633, 3);
            panel3.Name = "panel3";
            panel3.Size = new Size(498, 584);
            panel3.TabIndex = 1;
            panel3.Paint += panel3_Paint;
            // 
            // txtRfc
            // 
            txtRfc.BackColor = Color.FromArgb(37, 41, 47);
            txtRfc.Cursor = Cursors.IBeam;
            txtRfc.Font = new Font("Century", 10F);
            txtRfc.ForeColor = Color.FromArgb(12, 215, 253);
            txtRfc.Location = new Point(190, 33);
            txtRfc.MaxLength = 13;
            txtRfc.Name = "txtRfc";
            txtRfc.Size = new Size(239, 28);
            txtRfc.TabIndex = 107;
            // 
            // lblRFC
            // 
            lblRFC.AutoSize = true;
            lblRFC.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            lblRFC.ForeColor = Color.FromArgb(12, 215, 253);
            lblRFC.Location = new Point(43, 40);
            lblRFC.Name = "lblRFC";
            lblRFC.Size = new Size(49, 23);
            lblRFC.TabIndex = 106;
            lblRFC.Text = "RFC*";
            // 
            // txtDireccion
            // 
            txtDireccion.BackColor = Color.FromArgb(37, 41, 47);
            txtDireccion.Cursor = Cursors.IBeam;
            txtDireccion.Font = new Font("Century", 10F);
            txtDireccion.ForeColor = Color.FromArgb(12, 215, 253);
            txtDireccion.Location = new Point(190, 81);
            txtDireccion.MaxLength = 100;
            txtDireccion.Name = "txtDireccion";
            txtDireccion.Size = new Size(239, 28);
            txtDireccion.TabIndex = 105;
            // 
            // lblDireccion
            // 
            lblDireccion.AutoSize = true;
            lblDireccion.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            lblDireccion.ForeColor = Color.FromArgb(12, 215, 253);
            lblDireccion.Location = new Point(43, 88);
            lblDireccion.Name = "lblDireccion";
            lblDireccion.Size = new Size(93, 23);
            lblDireccion.TabIndex = 104;
            lblDireccion.Text = "Dirección*";
            // 
            // txtNomUsuario
            // 
            txtNomUsuario.BackColor = Color.FromArgb(37, 41, 47);
            txtNomUsuario.Cursor = Cursors.IBeam;
            txtNomUsuario.Font = new Font("Century", 10F);
            txtNomUsuario.ForeColor = Color.FromArgb(12, 215, 253);
            txtNomUsuario.Location = new Point(190, 224);
            txtNomUsuario.MaxLength = 50;
            txtNomUsuario.Name = "txtNomUsuario";
            txtNomUsuario.Size = new Size(239, 28);
            txtNomUsuario.TabIndex = 101;
            // 
            // txtContraseña
            // 
            txtContraseña.BackColor = Color.FromArgb(37, 41, 47);
            txtContraseña.Cursor = Cursors.IBeam;
            txtContraseña.Font = new Font("Century", 10F);
            txtContraseña.ForeColor = Color.FromArgb(12, 215, 253);
            txtContraseña.Location = new Point(190, 271);
            txtContraseña.MaxLength = 100;
            txtContraseña.Name = "txtContraseña";
            txtContraseña.PasswordChar = '*';
            txtContraseña.Size = new Size(239, 28);
            txtContraseña.TabIndex = 100;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            label9.ForeColor = Color.FromArgb(12, 215, 253);
            label9.Location = new Point(43, 274);
            label9.Name = "label9";
            label9.Size = new Size(107, 23);
            label9.TabIndex = 99;
            label9.Text = "Contraseña*";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            label7.ForeColor = Color.FromArgb(12, 215, 253);
            label7.Location = new Point(43, 231);
            label7.Name = "label7";
            label7.Size = new Size(78, 23);
            label7.TabIndex = 98;
            label7.Text = "Usuario*";
            // 
            // label2
            // 
            label2.Location = new Point(0, 0);
            label2.Name = "label2";
            label2.Size = new Size(100, 23);
            label2.TabIndex = 102;
            // 
            // label3
            // 
            label3.Location = new Point(0, 0);
            label3.Name = "label3";
            label3.Size = new Size(100, 23);
            label3.TabIndex = 103;
            // 
            // cbxRoles
            // 
            cbxRoles.BackColor = Color.FromArgb(37, 41, 47);
            cbxRoles.Font = new Font("Century", 10F);
            cbxRoles.ForeColor = Color.FromArgb(12, 215, 253);
            cbxRoles.FormattingEnabled = true;
            cbxRoles.Location = new Point(190, 178);
            cbxRoles.Name = "cbxRoles";
            cbxRoles.Size = new Size(239, 29);
            cbxRoles.TabIndex = 89;
            // 
            // lblRol
            // 
            lblRol.AutoSize = true;
            lblRol.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            lblRol.ForeColor = Color.FromArgb(12, 215, 253);
            lblRol.Location = new Point(43, 181);
            lblRol.Name = "lblRol";
            lblRol.Size = new Size(44, 23);
            lblRol.TabIndex = 88;
            lblRol.Text = "Rol*";
            // 
            // cbxEstatus
            // 
            cbxEstatus.BackColor = Color.FromArgb(37, 41, 47);
            cbxEstatus.Font = new Font("Century", 10F);
            cbxEstatus.ForeColor = Color.FromArgb(12, 215, 253);
            cbxEstatus.FormattingEnabled = true;
            cbxEstatus.Location = new Point(190, 130);
            cbxEstatus.Name = "cbxEstatus";
            cbxEstatus.Size = new Size(239, 29);
            cbxEstatus.TabIndex = 86;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            label6.ForeColor = Color.FromArgb(12, 215, 253);
            label6.Location = new Point(43, 130);
            label6.Name = "label6";
            label6.Size = new Size(74, 23);
            label6.TabIndex = 81;
            label6.Text = "Estatus*";
            // 
            // ibtnGuardar
            // 
            ibtnGuardar.BackColor = Color.FromArgb(37, 41, 47);
            ibtnGuardar.FlatAppearance.BorderSize = 2;
            ibtnGuardar.FlatStyle = FlatStyle.Flat;
            ibtnGuardar.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            ibtnGuardar.ForeColor = Color.FromArgb(12, 215, 253);
            ibtnGuardar.IconChar = FontAwesome.Sharp.IconChar.Save;
            ibtnGuardar.IconColor = Color.FromArgb(12, 215, 253);
            ibtnGuardar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            ibtnGuardar.IconSize = 24;
            ibtnGuardar.Location = new Point(304, 421);
            ibtnGuardar.Name = "ibtnGuardar";
            ibtnGuardar.Size = new Size(125, 50);
            ibtnGuardar.TabIndex = 95;
            ibtnGuardar.Text = "Guardar";
            ibtnGuardar.TextImageRelation = TextImageRelation.ImageBeforeText;
            ibtnGuardar.UseVisualStyleBackColor = false;
            ibtnGuardar.Click += ibtnGuardar_Click_1;
            // 
            // panel2
            // 
            panel2.Controls.Add(txtCurp);
            panel2.Controls.Add(lblCurp);
            panel2.Controls.Add(txtApellidoMaterno);
            panel2.Controls.Add(lblApellidoMaterno);
            panel2.Controls.Add(txtCorreo);
            panel2.Controls.Add(lblCorreo);
            panel2.Controls.Add(dtpFechaNacimiento);
            panel2.Controls.Add(cbxGenero);
            panel2.Controls.Add(txtApellido);
            panel2.Controls.Add(txtNombre);
            panel2.Controls.Add(lblApellidoParterno);
            panel2.Controls.Add(label10);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(Nombre);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(129, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(498, 584);
            panel2.TabIndex = 0;
            // 
            // txtCurp
            // 
            txtCurp.BackColor = Color.FromArgb(37, 41, 47);
            txtCurp.Cursor = Cursors.IBeam;
            txtCurp.Font = new Font("Century", 10F);
            txtCurp.ForeColor = Color.FromArgb(12, 215, 253);
            txtCurp.Location = new Point(204, 322);
            txtCurp.MaxLength = 18;
            txtCurp.Name = "txtCurp";
            txtCurp.Size = new Size(238, 28);
            txtCurp.TabIndex = 101;
            // 
            // lblCurp
            // 
            lblCurp.AutoSize = true;
            lblCurp.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            lblCurp.ForeColor = Color.FromArgb(12, 215, 253);
            lblCurp.Location = new Point(25, 326);
            lblCurp.Name = "lblCurp";
            lblCurp.Size = new Size(62, 23);
            lblCurp.TabIndex = 100;
            lblCurp.Text = "CURP*";
            // 
            // txtApellidoMaterno
            // 
            txtApellidoMaterno.BackColor = Color.FromArgb(37, 41, 47);
            txtApellidoMaterno.Cursor = Cursors.IBeam;
            txtApellidoMaterno.Font = new Font("Century", 10F);
            txtApellidoMaterno.ForeColor = Color.FromArgb(12, 215, 253);
            txtApellidoMaterno.Location = new Point(204, 127);
            txtApellidoMaterno.MaxLength = 50;
            txtApellidoMaterno.Name = "txtApellidoMaterno";
            txtApellidoMaterno.Size = new Size(238, 28);
            txtApellidoMaterno.TabIndex = 99;
            // 
            // lblApellidoMaterno
            // 
            lblApellidoMaterno.AutoSize = true;
            lblApellidoMaterno.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            lblApellidoMaterno.ForeColor = Color.FromArgb(12, 215, 253);
            lblApellidoMaterno.Location = new Point(25, 131);
            lblApellidoMaterno.Name = "lblApellidoMaterno";
            lblApellidoMaterno.Size = new Size(159, 23);
            lblApellidoMaterno.TabIndex = 98;
            lblApellidoMaterno.Text = "Apellido Materno*";
            // 
            // txtCorreo
            // 
            txtCorreo.BackColor = Color.FromArgb(37, 41, 47);
            txtCorreo.Cursor = Cursors.IBeam;
            txtCorreo.Font = new Font("Century", 10F);
            txtCorreo.ForeColor = Color.FromArgb(12, 215, 253);
            txtCorreo.Location = new Point(204, 274);
            txtCorreo.MaxLength = 320;
            txtCorreo.Name = "txtCorreo";
            txtCorreo.Size = new Size(238, 28);
            txtCorreo.TabIndex = 97;
            // 
            // lblCorreo
            // 
            lblCorreo.AutoSize = true;
            lblCorreo.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            lblCorreo.ForeColor = Color.FromArgb(12, 215, 253);
            lblCorreo.Location = new Point(25, 278);
            lblCorreo.Name = "lblCorreo";
            lblCorreo.Size = new Size(72, 23);
            lblCorreo.TabIndex = 96;
            lblCorreo.Text = "Correo*";
            // 
            // dtpFechaNacimiento
            // 
            dtpFechaNacimiento.CalendarForeColor = Color.FromArgb(12, 215, 253);
            dtpFechaNacimiento.CalendarMonthBackground = Color.FromArgb(24, 44, 65);
            dtpFechaNacimiento.CalendarTitleBackColor = Color.FromArgb(12, 215, 253);
            dtpFechaNacimiento.CalendarTitleForeColor = Color.FromArgb(12, 215, 253);
            dtpFechaNacimiento.CalendarTrailingForeColor = Color.FromArgb(12, 215, 253);
            dtpFechaNacimiento.Format = DateTimePickerFormat.Short;
            dtpFechaNacimiento.Location = new Point(204, 222);
            dtpFechaNacimiento.Name = "dtpFechaNacimiento";
            dtpFechaNacimiento.Size = new Size(238, 27);
            dtpFechaNacimiento.TabIndex = 86;
            // 
            // cbxGenero
            // 
            cbxGenero.BackColor = Color.FromArgb(37, 41, 47);
            cbxGenero.Cursor = Cursors.Hand;
            cbxGenero.Font = new Font("Century", 10F);
            cbxGenero.ForeColor = Color.FromArgb(12, 215, 253);
            cbxGenero.FormattingEnabled = true;
            cbxGenero.Location = new Point(204, 170);
            cbxGenero.Name = "cbxGenero";
            cbxGenero.Size = new Size(238, 29);
            cbxGenero.TabIndex = 85;
            // 
            // txtApellido
            // 
            txtApellido.BackColor = Color.FromArgb(37, 41, 47);
            txtApellido.Cursor = Cursors.IBeam;
            txtApellido.Font = new Font("Century", 10F);
            txtApellido.ForeColor = Color.FromArgb(12, 215, 253);
            txtApellido.Location = new Point(204, 80);
            txtApellido.MaxLength = 50;
            txtApellido.Name = "txtApellido";
            txtApellido.Size = new Size(238, 28);
            txtApellido.TabIndex = 84;
            // 
            // txtNombre
            // 
            txtNombre.BackColor = Color.FromArgb(37, 41, 47);
            txtNombre.Cursor = Cursors.IBeam;
            txtNombre.Font = new Font("Century", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtNombre.ForeColor = Color.FromArgb(12, 215, 253);
            txtNombre.Location = new Point(204, 33);
            txtNombre.MaxLength = 50;
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(238, 26);
            txtNombre.TabIndex = 83;
            // 
            // lblApellidoParterno
            // 
            lblApellidoParterno.AutoSize = true;
            lblApellidoParterno.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            lblApellidoParterno.ForeColor = Color.FromArgb(12, 215, 253);
            lblApellidoParterno.Location = new Point(25, 84);
            lblApellidoParterno.Name = "lblApellidoParterno";
            lblApellidoParterno.Size = new Size(153, 23);
            lblApellidoParterno.TabIndex = 82;
            lblApellidoParterno.Text = "Apellido Paterno*";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            label10.ForeColor = Color.FromArgb(12, 215, 253);
            label10.Location = new Point(25, 222);
            label10.Name = "label10";
            label10.Size = new Size(160, 23);
            label10.TabIndex = 81;
            label10.Text = "Fecha Nacimiento*";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            label5.ForeColor = Color.FromArgb(12, 215, 253);
            label5.Location = new Point(25, 177);
            label5.Name = "label5";
            label5.Size = new Size(75, 23);
            label5.TabIndex = 80;
            label5.Text = "Género*";
            // 
            // Nombre
            // 
            Nombre.AutoSize = true;
            Nombre.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            Nombre.ForeColor = Color.FromArgb(12, 215, 253);
            Nombre.Location = new Point(25, 37);
            Nombre.Name = "Nombre";
            Nombre.Size = new Size(84, 23);
            Nombre.TabIndex = 79;
            Nombre.Text = "Nombre*";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 4;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.Controls.Add(panel2, 1, 0);
            tableLayoutPanel1.Controls.Add(panel3, 2, 0);
            tableLayoutPanel1.Dock = DockStyle.Top;
            tableLayoutPanel1.Location = new Point(0, 67);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1262, 590);
            tableLayoutPanel1.TabIndex = 1;
            tableLayoutPanel1.Paint += tableLayoutPanel1_Paint;
            // 
            // UC_UsuariosAlta
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(37, 41, 47);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(panel1);
            ForeColor = Color.FromArgb(12, 215, 253);
            Name = "UC_UsuariosAlta";
            Size = new Size(1262, 705);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private Panel panel3;
        private TextBox txtRfc;
        private Label lblRFC;
        private TextBox txtDireccion;
        private Label lblDireccion;
        private TextBox txtNomUsuario;
        private TextBox txtContraseña;
        private Label label9;
        private Label label7;
        private Label label2;
        private Label label3;
        private ComboBox cbxRoles;
        private Label lblRol;
        private ComboBox cbxEstatus;
        private Label label6;
        private FontAwesome.Sharp.IconButton ibtnGuardar;
        private Panel panel2;
        private TextBox txtApellidoMaterno;
        private Label lblApellidoMaterno;
        private TextBox txtCorreo;
        private Label lblCorreo;
        private DateTimePicker dtpFechaNacimiento;
        private ComboBox cbxGenero;
        private TextBox txtApellido;
        private TextBox txtNombre;
        private Label lblApellidoParterno;
        private Label label10;
        private Label label5;
        private Label Nombre;
        private TableLayoutPanel tableLayoutPanel1;
        private TextBox txtCurp;
        private Label lblCurp;
    }
}
