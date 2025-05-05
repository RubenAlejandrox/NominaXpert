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
using NominaXpert.Model;
using ControlEscolar.Utilities;
using NLog;

namespace NominaXpert.View.UsersControl
{
    public partial class UC_NominaRecibo : UserControl
    {
        // Propiedad para almacenar el ID de la nómina
        public int IdNomina { get; set; }

        // Logger para la clase
        private static readonly Logger _logger = LoggingManager.GetLogger("NominaXpert.Controller.NominasController");

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
            PoblaCombo();
            this.IdNomina = idNomina;

            _nominasController = new NominasController();
            _bonificacionController = new BonificacionController();
            _deduccionController = new DeduccionController();

            if (idNomina > 0)
                CargarRecibo();
            else
                MessageBox.Show("No se proporcionó una nómina válida.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void PoblaCombo()
        {
            var metodosPago = new Dictionary<int, string>
    {
        { 1, "Transferencia" },
        { 2, "Efectivo" },
        { 3, "Cheque" }
    };

            cboMetodoPago.DataSource = new BindingSource(metodosPago, null);
            cboMetodoPago.DisplayMember = "Value";
            cboMetodoPago.ValueMember = "Key";
            cboMetodoPago.SelectedIndex = 0; // Por defecto Transferencia
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
                lblMontoLetras.Text = NominaNegocio.ConvertirNumeroALetras(totalNeto);

                // --- Validar estado de nómina ---
                if (nomina.EstadoPago == "Pagado")
                {
                    // Deshabilitar botones si ya está pagada
                    btnGenerarNómina.Visible = false;
                    btnRegresar.Visible = false;
                    // btnPDFReciboNomina.Enabled = true;
                }
                else
                {
                    // Habilitar != pagado
                    btnGenerarNómina.Enabled = true;
                    btnRegresar.Enabled = true;
                }

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

        private void btnGenerarNómina_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    PagoController pagoController = new PagoController();

                    // Validar que NO EXISTA PAGO ya para esta nómina
                    if (pagoController.ExistePago(this.IdNomina))
                    {
                        MessageBox.Show("Esta nómina ya fue pagada. No es posible generar otro pago.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    // Validar método de pago
                    if (cboMetodoPago.SelectedItem == null || string.IsNullOrWhiteSpace(cboMetodoPago.Text))
                    {
                        MessageBox.Show("Debe seleccionar un método de pago.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Obtener valores del recibo
                    decimal sueldoBase = decimal.Parse(lblSueldoBase.Text, System.Globalization.NumberStyles.Currency);
                    decimal totalPercepciones = decimal.Parse(lblTotalPercepciones.Text, System.Globalization.NumberStyles.Currency);
                    decimal totalDeducciones = decimal.Parse(lblTotalDeducciones.Text, System.Globalization.NumberStyles.Currency);
                    decimal totalNeto = decimal.Parse(lblTotalNeto.Text, System.Globalization.NumberStyles.Currency);

                    // Crear objeto pago
                    Pago nuevoPago = new Pago
                    {
                        IdNomina = this.IdNomina,
                        FechaPago = DateTime.Now,
                        MontoTotal = totalNeto,
                        MontoLetras = NominaNegocio.ConvertirNumeroALetras(totalNeto),
                        MetodoPago = cboMetodoPago.Text,
                        Referencia = "Pago automático generado desde recibo de nómina"
                    };

                    // Registrar pago en la base de datos
                    bool resultado = pagoController.RegistrarPago(nuevoPago);

                    if (resultado)
                    {
                        // Actualizar estado de nómina a "Pagada"
                        _logger.Info($"UC_NominaRecibo -> Se actualizará el estado de la nómina {this.IdNomina} a Pagado");
                        _nominasController.ActualizarEstadoPago(this.IdNomina, "Pagado");
                        _logger.Info($"UC_NominaRecibo -> Finalizó la actualización de la nómina {this.IdNomina} a Pagado");

                        // Refrescar la información visual del recibo para reflejar "Pagada"
                        CargarRecibo();

                        MessageBox.Show("El pago ha sido registrado correctamente. La nómina ha sido marcada como pagada.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    }
                    else
                    {
                        MessageBox.Show("Ocurrió un error al registrar el pago.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al generar la nómina: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
