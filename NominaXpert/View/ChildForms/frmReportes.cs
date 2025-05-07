using NominaXpert.Business;
using NominaXpert.Controller;
using NominaXpert.Model;
using NominaXpert.View.UsersControl;
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
        public int IdNomina { get; set; }

        private readonly NominasController _nominasController;


        public frmReportes()
        {
            InitializeComponent();
            ConfigurarPermisos();
            _nominasController = new NominasController();
        }

        private void frmReportes_Load(object sender, EventArgs e)
        {
            InicializaVentanaCalculoRecibos();
            // Cargar las nóminas automáticamente al abrir el formulario
            CargarNominasPorDefecto();
        }
        public void InicializaVentanaCalculoRecibos()
        {
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
            // 1. Validar si el campo de matrícula está vacío
            if (string.IsNullOrWhiteSpace(txtMatricula.Text)) // Si está vacío, mostramos el siguiente mensaje
            {
                MessageBox.Show("El campo de matrícula no puede estar vacío", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Validar si la matrícula tiene el formato correcto
            if (!EmpleadosNegocio.EsMatriculaValido(txtMatricula.Text.Trim()))
            {
                MessageBox.Show("Matrícula inválida. Verifique el formato.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. Obtener las fechas de inicio y fin seleccionadas
            DateTime fechaInicio = DTPFechaInicioNomina.Value;
            DateTime fechaFin = DTPFechaFinNomina.Value;

            // Validar que la fecha de fin no sea anterior a la fecha de inicio
            if (fechaFin < fechaInicio)
            {
                MessageBox.Show("La fecha de fin no puede ser anterior a la fecha de inicio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 4. Llamar al controlador para obtener las nóminas filtradas por matrícula y fechas
            NominasController nominaController = new NominasController();
            List<NominaConsulta> nominas = nominaController.BuscarNominasPorMatriculaYFechas(txtMatricula.Text.Trim(), fechaInicio, fechaFin);

            // 5. Llenar el DataGridView con las nóminas obtenidas
            if (nominas.Count > 0)
            {
                // Limpiar cualquier dato previo en el DataGridView
                dataGridView1.Rows.Clear();

                // Llenar el DataGridView con los datos de las nóminas
                foreach (var nomina in nominas)
                {
                    dataGridView1.Rows.Add(
                        nomina.IdNomina,
                        nomina.IdEmpleado,
                        nomina.FechaInicio.ToShortDateString(),
                        nomina.FechaFin.ToShortDateString(),
                        nomina.EstadoPago,
                        nomina.MontoTotal,
                        nomina.MontoLetras
                    );
                }

                // Actualizar el total de registros
                lblTotaldeRegistros.Text = $"Total de Registros: {dataGridView1.Rows.Count - 1}";
            }
            else
            {
                MessageBox.Show("No se encontraron nóminas para los filtros aplicados.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        private void ConfigurarPermisos()
        {
            var controller = new UsuariosController();
            btnDatalleNomina.Enabled = controller.TienePermiso("NOM_VIEW");
            btnExportarPDF.Enabled = controller.TienePermiso("EXP_DATS");
            btnEsportarExcel.Enabled = controller.TienePermiso("EXP_DATS");

        }
        private void btnDatalleNomina_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar si se ha seleccionado alguna fila en el DataGridView
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Por favor, seleccione una nómina para ver el detalle.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Obtener el IdNomina de la fila seleccionada
                int idNomina = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id_Nomina"].Value);

                // Navegar a UC_NominaRecibo y pasar el IdNomina
                UC_NominaRecibo ucRecibo = new UC_NominaRecibo(idNomina); // Usamos el idNomina de la fila seleccionada
                ucRecibo.Dock = DockStyle.Fill; // Aseguramos que el UserControl ocupe todo el espacio disponible

                // Obtener el contenedor principal del UserControl actual
                Control parent = this.Parent;
                if (parent != null)
                {
                    // Remover el UserControl actual
                    parent.Controls.Remove(this);

                    // Agregar el nuevo UserControl al mismo contenedor
                    parent.Controls.Add(ucRecibo);
                    parent.Controls.SetChildIndex(ucRecibo, 0); // Ponerlo al frente
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cambiar de vista: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarNominasPorDefecto()
        {
            try
            {
                // Llamar al controlador para obtener las nóminas
                NominasController nominaController = new NominasController();
                List<NominaConsulta> nominas = nominaController.DesplegarNominas(); // Obtener las nóminas sin filtros

                // Llenar el DataGridView con las nóminas
                if (nominas.Count > 0)
                {
                    // Limpiar cualquier dato previo
                    dataGridView1.Rows.Clear();

                    // Configurar el DataGridView para permitir scroll horizontal
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    dataGridView1.AutoResizeColumns();

                    // Llenar el DataGridView con los datos de las nóminas
                    foreach (var nomina in nominas)
                    {
                        dataGridView1.Rows.Add(
                            nomina.IdNomina,
                            nomina.IdEmpleado,
                            nomina.FechaInicio.ToShortDateString(),
                            nomina.FechaFin.ToShortDateString(),
                            nomina.EstadoPago,
                            nomina.MontoTotal,
                            nomina.MontoLetras
                        );
                    }

                    // Actualizar el texto del total de registros
                    lblTotaldeRegistros.Text = $"Total de Registros: {dataGridView1.Rows.Count - 1}";  // Actualizamos el total de registros

                }
                else
                {
                    MessageBox.Show("No se encontraron nóminas.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las nóminas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       
        
    }
}
