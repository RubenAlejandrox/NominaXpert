using NominaXpert.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NominaXpert.View.Forms
{
    public partial class frmReportes : Form
    {
        public frmReportes()
        {
            InitializeComponent();
        }

        private void frmReportes_Load(object sender, EventArgs e)
        {
            InicializaVentanaCalculoRecibos();
        }
        public void InicializaVentanaCalculoRecibos()
        {
            PoblaEstatus();
        }

        private void PoblaEstatus()
        {
            //Crear un diccionario de valores
            Dictionary<int, string> list_estatus = new Dictionary<int, string>
            {
                { 1, "Pendiente" },
                { 2, "Pagado" },
                { 3, "Rechazado" }
            };
            //Asignar el diccionario al combo
            CBoxEstatusNomina.DataSource = new BindingSource(list_estatus, null);
            CBoxEstatusNomina.DisplayMember = "Value";
            CBoxEstatusNomina.ValueMember = "Key";

            CBoxEstatusNomina.SelectedValue = 1;
        }
        private void btnPDFReciboNomina_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMatricula.Text)) //si esto esta vacio, mandamos el sig msj
            {
                MessageBox.Show("El campo de matricula no puede estar vacío", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Crear un formulario de notificación temporal
            Form mensajeForm = new Form
            {
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterScreen,
                Size = new System.Drawing.Size(350, 220),
                Text = "Información del sistema",
                ControlBox = false // Evita que se cierre manualmente
            };

            Label lblMensaje = new Label
            {
                Text = "Generando PDF de la Nómina del Empleado...",
                AutoSize = false,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };

            mensajeForm.Controls.Add(lblMensaje);
            mensajeForm.Show();

            // Configurar el temporizador para cerrar la ventana después de 2 segundos
            System.Timers.Timer timer = new System.Timers.Timer(2000);
            timer.Elapsed += (s, ev) =>
            {
                timer.Stop();
                mensajeForm.Invoke((MethodInvoker)delegate
                {
                    mensajeForm.Close(); // Solo cierra el formulario de mensaje
                });
                timer.Dispose(); // Liberar recursos del Timer
            };
            timer.Start();

        }


        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // 1️ Validar si el campo está vacío
            if (string.IsNullOrWhiteSpace(txtMatricula.Text)) //si esto esta vacio, mandamos el sig msj
            {
                MessageBox.Show("El campo de matricula no puede estar vacío", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2️ Validar si la matrícula tiene el formato correcto
            if (!EmpleadosNegocio.EsMatriculaValido(txtMatricula.Text.Trim()))
            {
                MessageBox.Show("Matrícula inválida. Verifique el formato.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void iconVerNomina_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMatricula.Text)) //si esto esta vacio, mandamos el sig msj
            {
                MessageBox.Show("El campo de matricula no puede estar vacío", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Crear un formulario de notificación temporal
            Form mensajeForm = new Form
            {
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterScreen,
                Size = new System.Drawing.Size(350, 220),
                Text = "Información del sistema",
                ControlBox = false // Evita que se cierre manualmente
            };

            Label lblMensaje = new Label
            {
                Text = "Generando Excel de la Nómina...",
                AutoSize = false,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };

            mensajeForm.Controls.Add(lblMensaje);
            mensajeForm.Show();

            // Configurar el temporizador para cerrar la ventana después de 2 segundos
            System.Timers.Timer timer = new System.Timers.Timer(2000);
            timer.Elapsed += (s, ev) =>
            {
                timer.Stop();
                mensajeForm.Invoke((MethodInvoker)delegate
                {
                    mensajeForm.Close(); // Solo cierra el formulario de mensaje
                });
                timer.Dispose(); // Liberar recursos del Timer
            };
            timer.Start();
        }
    }
}
