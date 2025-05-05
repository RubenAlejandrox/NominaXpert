using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NominaXpert.Controller;
using NominaXpert.Business;

namespace NominaXpert.View.UsersControl
{
    public partial class UC_NominaRecibo : UserControl
    {
        // Propiedad para almacenar el ID de la nómina
        public int IdNomina { get; set; }

        // Controladores para las diferentes secciones
        private readonly NominasController _nominasController;
        private readonly BonificacionController _bonificacionController;
        private readonly DeduccionController _deduccionController;

        /// <summary>
        /// Constructor de la clase UC_NominaRecibo.
        /// </summary>
        /// <param name="idNomina"></param>
        public UC_NominaRecibo(int idNomina)
        {
            InitializeComponent();
            this.IdNomina = idNomina;

            _nominasController = new NominasController();
            _bonificacionController = new BonificacionController();
            _deduccionController = new DeduccionController();

            if (idNomina > 0)
                CargarRecibo();
            else
                MessageBox.Show("No se proporcionó una nómina válida.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Método para cargar los datos del recibo de nómina
        /// </summary>
        private void CargarRecibo()
        {
            try
            {
                // Obtener nómina
                var nomina = _nominasController.BuscarNominaPorId(IdNomina);

                if (nomina == null)
                {
                    MessageBox.Show("No se encontró la nómina.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Mostrar datos de empleado
                lblNombreEmpleado.Text = nomina.NombreEmpleado;
                lblDepartamento.Text = nomina.DepartamentoEmpleado;
                lblRFC.Text = nomina.RfcEmpleado;
                lblIdEmpleado.Text = nomina.IdEmpleado.ToString();

                // Periodo de nómina
                lblFechaInicio.Text = nomina.FechaInicio.ToShortDateString();
                lblFechaFin.Text = nomina.FechaFin.ToShortDateString();
                lblEstado.Text = nomina.EstadoPago;

                // Obtener percepciones y deducciones
                var percepciones = _bonificacionController.ObtenerBonificacionesPorNomina(IdNomina);
                var deducciones = _deduccionController.ObtenerDeduccionesPorNomina(IdNomina);

                decimal totalPercepciones = percepciones.Sum(p => p.Monto);
                decimal totalDeducciones = deducciones.Sum(d => d.Monto);

                // Mostrar cálculos
                lblTotalPercepciones.Text = totalPercepciones.ToString("C");
                lblTotalDeducciones.Text = totalDeducciones.ToString("C");
                lblSueldoBase.Text = nomina.SueldoBase.ToString("C");

                decimal totalNeto = (nomina.SueldoBase + totalPercepciones) - totalDeducciones;
                lblTotalNeto.Text = totalNeto.ToString("C");

                // Monto en letras
                //lblMontoLetras.Text = NominaNegocio.ConvertirNumeroALetras(totalNeto);
                lblMontoLetras.Text = NominaNegocio.ConvertirNumeroALetras(totalNeto);
                //lblMontoLetras.Text = "MONTOLetras temporal";

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el recibo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Carga el recibo de nómina y lo muestra en el control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPDFReciboNomina_Click(object sender, EventArgs e)
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
                Text = "Generando Excel de la Nómina del Empleado...",
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

        private void btnRegresar_Click(object sender, EventArgs e)
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
                Text = "Regresando a Deducciones...",
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
                        UC_NominaDeducciones ucRecibo = new UC_NominaDeducciones(this.IdNomina);
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
