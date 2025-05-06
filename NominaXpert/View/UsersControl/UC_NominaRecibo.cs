using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using NominaXpert.Controller;
using NominaXpert.Business;
using NominaXpert.Model;
using ControlEscolar.Utilities;
using NLog;

using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Kernel.Font;
using iText.Layout.Properties;
using iText.Kernel.Exceptions;
using iText.IO.Font.Constants;

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
                lblHorasTrabajadas.Text = nomina.HorasTrabajadas.ToString();

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
                    lblMetodoPago.Visible = false;
                    cboMetodoPago.Visible = false;
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

            try
            {
                // Mostrar el cuadro de diálogo para guardar el archivo
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "PDF Files (*.pdf)|*.pdf", // Filtro para archivos PDF
                    Title = "Guardar Recibo de Nómina",
                    FileName = "ReciboNomina_" + IdNomina + ".pdf" // Nombre predeterminado
                };

                // Si el usuario selecciona una ubicación y un nombre, se genera el PDF
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string path = saveFileDialog.FileName; // Ruta seleccionada por el usuario

                    // Crear un escritor para el PDF
                    using (PdfWriter writer = new PdfWriter(path))
                    {
                        // Crear un documento PDF
                        using (PdfDocument pdf = new PdfDocument(writer))
                        {
                            Document document = new Document(pdf);

                            // Usar una fuente estándar sin necesidad de especificar un archivo TTF
                            PdfFont boldFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
                            PdfFont regularFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

                            // Título del documento
                            document.Add(new Paragraph("Recibo de Nómina - NominaXpert")
                                .SetTextAlignment(TextAlignment.CENTER)
                                .SetFont(boldFont) // Usamos la fuente en negrita
                                .SetFontSize(18)
                                .SetMarginTop(10));

                            // Salto de línea
                            document.Add(new Paragraph("\n"));

                            // Información de la nómina
                            document.Add(new Paragraph("Empleado: " + lblNombreEmpleado.Text).SetFont(regularFont));
                            document.Add(new Paragraph("Departamento: " + lblDepartamento.Text).SetFont(regularFont));
                            document.Add(new Paragraph("RFC: " + lblRFC.Text).SetFont(regularFont));
                            document.Add(new Paragraph("ID: " + lblIdEmpleado.Text).SetFont(regularFont));
                            document.Add(new Paragraph("Fecha Inicio: " + lblFechaInicio.Text).SetFont(regularFont));
                            document.Add(new Paragraph("Fecha Fin: " + lblFechaFin.Text).SetFont(regularFont));
                            document.Add(new Paragraph("Estado de Pago: " + lblEstado.Text).SetFont(regularFont));
                            document.Add(new Paragraph("\n"));

                            // Obtener las bonificaciones y deducciones
                            var percepciones = _bonificacionController.ObtenerBonificacionesPorNomina(IdNomina);
                            var deducciones = _deduccionController.ObtenerDeduccionesPorNomina(IdNomina);

                            // Agregar las tablas de percepciones
                            if (percepciones.Any())
                            {
                                document.Add(new Paragraph("Percepciones")
                                    .SetFont(boldFont) // Usamos la fuente en negrita
                                    .SetFontSize(14));

                                Table table = new Table(2);
                                table.AddHeaderCell("Concepto");
                                table.AddHeaderCell("Monto");

                                foreach (var percepcion in percepciones)
                                {
                                    table.AddCell(percepcion.IdTipo.ToString()); // Puedes mostrar el tipo si es necesario
                                    table.AddCell(percepcion.Monto.ToString("C"));
                                }

                                document.Add(table);
                            }

                            document.Add(new Paragraph("\n"));

                            // Agregar las tablas de deducciones
                            if (deducciones.Any())
                            {
                                document.Add(new Paragraph("Deducciones")
                                    .SetFont(boldFont) // Usamos la fuente en negrita
                                    .SetFontSize(14));

                                Table tableDeducciones = new Table(2);
                                tableDeducciones.AddHeaderCell("Concepto");
                                tableDeducciones.AddHeaderCell("Monto");

                                foreach (var deduccion in deducciones)
                                {
                                    tableDeducciones.AddCell(deduccion.IdTipo.ToString()); // Puedes mostrar el tipo si es necesario
                                    tableDeducciones.AddCell(deduccion.Monto.ToString("C"));
                                }

                                document.Add(tableDeducciones);
                            }

                            document.Add(new Paragraph("\n"));

                            // Cálculos de totales
                            decimal totalPercepciones = percepciones.Sum(p => p.Monto);
                            decimal totalDeducciones = deducciones.Sum(d => d.Monto);
                            decimal sueldoBase = decimal.Parse(lblSueldoBase.Text, System.Globalization.NumberStyles.Currency);
                            decimal totalNeto = (sueldoBase + totalPercepciones) - totalDeducciones;

                            document.Add(new Paragraph($"Total Percepciones: {totalPercepciones:C}").SetFont(regularFont));
                            document.Add(new Paragraph($"Total Deducciones: {totalDeducciones:C}").SetFont(regularFont));
                            document.Add(new Paragraph($"Sueldo Base: {sueldoBase:C}").SetFont(regularFont));
                            document.Add(new Paragraph($"Total Neto: {totalNeto:C}").SetFont(regularFont));
                            document.Add(new Paragraph($"Monto en Letras: {NominaNegocio.ConvertirNumeroALetras(totalNeto)}").SetFont(regularFont));

                            // Cerrar el documento
                            document.Close();
                        }
                    }

                    MessageBox.Show("El recibo de nómina ha sido generado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (PdfException pdfEx)
            {
                MessageBox.Show($"Error al generar el PDF: {pdfEx.Message}", "PDF Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inesperado: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

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
