using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NominaXpert.Business;

namespace NominaXpert.View.UsersControl
{
    public partial class UC_NominaEditar : UserControl
    {
        public int IdNomina { get; set; }

        public UC_NominaEditar(int idNomina)
        {
            InitializeComponent();
            this.IdNomina = idNomina;
        }

        private void UC_NominaEditar_Load(object sender, EventArgs e)
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
            cBoxEstatusNomina.DataSource = new BindingSource(list_estatus, null);
            cBoxEstatusNomina.DisplayMember = "Value";
            cBoxEstatusNomina.ValueMember = "Key";

            cBoxEstatusNomina.SelectedValue = 1;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
          
        }

        private void btnActualizarCambios_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNoNomina.Text)) //si esto esta vacio, mandamos el sig msj
            {
                MessageBox.Show("El campo de No. de Nómina no puede estar vacío", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                Text = "Generando cambio en el estatus de la Nómina del Empleado...",
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

        private void btnModificar_Click(object sender, EventArgs e)
        {
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
                Text = "Cargando Nómina del Empleado...",
                AutoSize = false, //evita su crecimiento
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill //redimensiona al panel debajo
            };

            mensajeForm.Controls.Add(lblMensaje);
            mensajeForm.Show();

            // Configurar el temporizador para cerrar la ventana después de 2 segundos y cambiar de UC
            System.Timers.Timer timer = new System.Timers.Timer(2000);
            timer.Elapsed += (s, ev) =>
            {
                timer.Stop();
                mensajeForm.Invoke((MethodInvoker)delegate { mensajeForm.Close(); });

                // Ejecutar en el hilo principal
                this.Invoke((MethodInvoker)delegate //asistente solo ve y dile a chef y delega la tarea
                {
                    // Obtener el contenedor padre del UserControl actual
                    Control parent = this.Parent; //Quién es el padre?
                    if (parent != null)
                    {
                        // Remover el UserControl actual
                        parent.Controls.Remove(this);

                        // Crear una nueva instancia de UC_NominaRecibo
                        UC_NominaPercepciones ucRecibo = new UC_NominaPercepciones(this.IdNomina);
                        ucRecibo.Dock = DockStyle.Fill;

                        // Agregar el nuevo UserControl al mismo contenedor
                        parent.Controls.Add(ucRecibo);
                        parent.Controls.SetChildIndex(ucRecibo, 0); // Ponerlo al frente
                    }
                });
            };
            timer.Start();
        }
    }
}
