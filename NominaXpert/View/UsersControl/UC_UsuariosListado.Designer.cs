namespace NominaXpert.View.UsersControl
{
    partial class UC_UsuariosListado
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
            panelContenedor = new Panel();
            btnSearch = new Button();
            txtSearchEmpleado = new TextBox();
            iconPIcture = new FontAwesome.Sharp.IconPictureBox();
            dtgListaUsuario = new DataGridView();
            Seleccion = new DataGridViewCheckBoxColumn();
            Usuario = new DataGridViewTextBoxColumn();
            Nombre = new DataGridViewTextBoxColumn();
            ApellidoPat = new DataGridViewTextBoxColumn();
            ApellidoMat = new DataGridViewTextBoxColumn();
            Genero = new DataGridViewTextBoxColumn();
            Correo = new DataGridViewTextBoxColumn();
            Estatus = new DataGridViewTextBoxColumn();
            Rol = new DataGridViewTextBoxColumn();
            panel1 = new Panel();
            ibtnEditar = new FontAwesome.Sharp.IconButton();
            ibtnBajaUsuario = new FontAwesome.Sharp.IconButton();
            lblTituloListado = new Label();
            panelContenedor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconPIcture).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dtgListaUsuario).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panelContenedor
            // 
            panelContenedor.Controls.Add(btnSearch);
            panelContenedor.Controls.Add(txtSearchEmpleado);
            panelContenedor.Controls.Add(iconPIcture);
            panelContenedor.Controls.Add(dtgListaUsuario);
            panelContenedor.Controls.Add(panel1);
            panelContenedor.Location = new Point(0, -1);
            panelContenedor.Name = "panelContenedor";
            panelContenedor.Size = new Size(1262, 705);
            panelContenedor.TabIndex = 0;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.Black;
            btnSearch.FlatStyle = FlatStyle.Popup;
            btnSearch.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSearch.ForeColor = Color.White;
            btnSearch.Location = new Point(708, 119);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(94, 29);
            btnSearch.TabIndex = 11;
            btnSearch.Text = "Buscar";
            btnSearch.UseVisualStyleBackColor = false;
            // 
            // txtSearchEmpleado
            // 
            txtSearchEmpleado.BackColor = Color.FromArgb(37, 41, 47);
            txtSearchEmpleado.BorderStyle = BorderStyle.FixedSingle;
            txtSearchEmpleado.ForeColor = Color.LightGray;
            txtSearchEmpleado.Location = new Point(86, 119);
            txtSearchEmpleado.Name = "txtSearchEmpleado";
            txtSearchEmpleado.Size = new Size(585, 27);
            txtSearchEmpleado.TabIndex = 10;
            txtSearchEmpleado.Text = "Buscar usuario";
            // 
            // iconPIcture
            // 
            iconPIcture.BackColor = Color.FromArgb(37, 41, 47);
            iconPIcture.ForeColor = Color.FromArgb(12, 215, 253);
            iconPIcture.IconChar = FontAwesome.Sharp.IconChar.Search;
            iconPIcture.IconColor = Color.FromArgb(12, 215, 253);
            iconPIcture.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPIcture.IconSize = 36;
            iconPIcture.Location = new Point(40, 110);
            iconPIcture.Name = "iconPIcture";
            iconPIcture.Size = new Size(40, 36);
            iconPIcture.TabIndex = 9;
            iconPIcture.TabStop = false;
            // 
            // dtgListaUsuario
            // 
            dtgListaUsuario.BackgroundColor = Color.FromArgb(48, 51, 59);
            dtgListaUsuario.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;
            dtgListaUsuario.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dtgListaUsuario.Columns.AddRange(new DataGridViewColumn[] { Seleccion, Usuario, Nombre, ApellidoPat, ApellidoMat, Genero, Correo, Estatus, Rol });
            dtgListaUsuario.GridColor = Color.MidnightBlue;
            dtgListaUsuario.Location = new Point(40, 176);
            dtgListaUsuario.Name = "dtgListaUsuario";
            dtgListaUsuario.RowHeadersWidth = 51;
            dtgListaUsuario.Size = new Size(1185, 634);
            dtgListaUsuario.TabIndex = 8;
            dtgListaUsuario.CellContentClick += dtgListaUsuario_CellContentClick;
            // 
            // Seleccion
            // 
            Seleccion.HeaderText = "Selección";
            Seleccion.MinimumWidth = 6;
            Seleccion.Name = "Seleccion";
            Seleccion.Width = 125;
            // 
            // Usuario
            // 
            Usuario.HeaderText = "Usuario";
            Usuario.MinimumWidth = 6;
            Usuario.Name = "Usuario";
            Usuario.Width = 125;
            // 
            // Nombre
            // 
            Nombre.HeaderText = "Nombre ";
            Nombre.MinimumWidth = 6;
            Nombre.Name = "Nombre";
            Nombre.Width = 125;
            // 
            // ApellidoPat
            // 
            ApellidoPat.HeaderText = "Apellido Paterno";
            ApellidoPat.MinimumWidth = 6;
            ApellidoPat.Name = "ApellidoPat";
            ApellidoPat.Width = 125;
            // 
            // ApellidoMat
            // 
            ApellidoMat.HeaderText = "Apellido Materno";
            ApellidoMat.MinimumWidth = 6;
            ApellidoMat.Name = "ApellidoMat";
            ApellidoMat.Width = 125;
            // 
            // Genero
            // 
            Genero.HeaderText = "Género";
            Genero.MinimumWidth = 6;
            Genero.Name = "Genero";
            Genero.Width = 125;
            // 
            // Correo
            // 
            Correo.HeaderText = "Correo";
            Correo.MinimumWidth = 6;
            Correo.Name = "Correo";
            Correo.Width = 125;
            // 
            // Estatus
            // 
            Estatus.HeaderText = "Estatus";
            Estatus.MinimumWidth = 6;
            Estatus.Name = "Estatus";
            Estatus.Width = 125;
            // 
            // Rol
            // 
            Rol.HeaderText = "Rol";
            Rol.MinimumWidth = 6;
            Rol.Name = "Rol";
            Rol.Width = 125;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(48, 51, 59);
            panel1.Controls.Add(ibtnEditar);
            panel1.Controls.Add(ibtnBajaUsuario);
            panel1.Controls.Add(lblTituloListado);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1262, 90);
            panel1.TabIndex = 7;
            panel1.Paint += panel1_Paint;
            // 
            // ibtnEditar
            // 
            ibtnEditar.BackColor = Color.FromArgb(37, 41, 47);
            ibtnEditar.FlatStyle = FlatStyle.Flat;
            ibtnEditar.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ibtnEditar.ForeColor = Color.FromArgb(12, 215, 253);
            ibtnEditar.IconChar = FontAwesome.Sharp.IconChar.UserPen;
            ibtnEditar.IconColor = Color.FromArgb(12, 215, 253);
            ibtnEditar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            ibtnEditar.IconSize = 24;
            ibtnEditar.Location = new Point(1029, 36);
            ibtnEditar.Name = "ibtnEditar";
            ibtnEditar.Size = new Size(125, 41);
            ibtnEditar.TabIndex = 94;
            ibtnEditar.Text = "Editar";
            ibtnEditar.TextImageRelation = TextImageRelation.ImageBeforeText;
            ibtnEditar.UseVisualStyleBackColor = false;
            ibtnEditar.Click += ibtnEditar_Click;
            // 
            // ibtnBajaUsuario
            // 
            ibtnBajaUsuario.BackColor = Color.FromArgb(37, 41, 47);
            ibtnBajaUsuario.FlatStyle = FlatStyle.Flat;
            ibtnBajaUsuario.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ibtnBajaUsuario.ForeColor = Color.FromArgb(12, 215, 253);
            ibtnBajaUsuario.IconChar = FontAwesome.Sharp.IconChar.UserXmark;
            ibtnBajaUsuario.IconColor = Color.FromArgb(12, 215, 253);
            ibtnBajaUsuario.IconFont = FontAwesome.Sharp.IconFont.Auto;
            ibtnBajaUsuario.IconSize = 24;
            ibtnBajaUsuario.Location = new Point(872, 36);
            ibtnBajaUsuario.Name = "ibtnBajaUsuario";
            ibtnBajaUsuario.Size = new Size(125, 41);
            ibtnBajaUsuario.TabIndex = 93;
            ibtnBajaUsuario.Text = "Baja";
            ibtnBajaUsuario.TextImageRelation = TextImageRelation.ImageBeforeText;
            ibtnBajaUsuario.UseVisualStyleBackColor = false;
            ibtnBajaUsuario.Click += ibtnBajaUsuario_Click;
            // 
            // lblTituloListado
            // 
            lblTituloListado.AutoSize = true;
            lblTituloListado.Font = new Font("Corbel", 16.2F, FontStyle.Bold);
            lblTituloListado.ForeColor = Color.FromArgb(12, 215, 253);
            lblTituloListado.Location = new Point(40, 37);
            lblTituloListado.Name = "lblTituloListado";
            lblTituloListado.Size = new Size(253, 35);
            lblTituloListado.TabIndex = 9;
            lblTituloListado.Text = "Listado de Usuarios";
            // 
            // UC_UsuariosListado
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(37, 41, 47);
            BackgroundImageLayout = ImageLayout.Center;
            Controls.Add(panelContenedor);
            Name = "UC_UsuariosListado";
            Size = new Size(1262, 705);
            panelContenedor.ResumeLayout(false);
            panelContenedor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)iconPIcture).EndInit();
            ((System.ComponentModel.ISupportInitialize)dtgListaUsuario).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelContenedor;
        private DataGridView dtgListaUsuario;
        private Panel panel1;
        private Label lblTituloListado;
        private Button btnSearch;
        private TextBox txtSearchEmpleado;
        private FontAwesome.Sharp.IconPictureBox iconPIcture;
        private FontAwesome.Sharp.IconButton ibtnBajaUsuario;
        private FontAwesome.Sharp.IconButton ibtnEditar;
        private DataGridViewCheckBoxColumn Seleccion;
        private DataGridViewTextBoxColumn Usuario;
        private DataGridViewTextBoxColumn Nombre;
        private DataGridViewTextBoxColumn ApellidoPat;
        private DataGridViewTextBoxColumn ApellidoMat;
        private DataGridViewTextBoxColumn Genero;
        private DataGridViewTextBoxColumn Correo;
        private DataGridViewTextBoxColumn Estatus;
        private DataGridViewTextBoxColumn Rol;
    }
}
