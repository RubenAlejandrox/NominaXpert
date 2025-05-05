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
using NominaXpert.Controller;

namespace NominaXpert.View.UsersControl
{
    public partial class UC_NominaEditar : UserControl
    {
        /// <summary>
        /// ID de la nómina a editar y el ID del empleado
        /// </summary>
        public int IdNomina { get; set; }
        public int IdEmpleado { get; set; }

        public UC_NominaEditar(int idNomina)
        {
            InitializeComponent();

            this.IdNomina = idNomina;

            if (idNomina > 0)
            {
                txtNoNomina.Text = idNomina.ToString();
            }
        }

        /// <summary>
        /// Método que se ejecuta al cargar el UserControl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UC_NominaEditar_Load(object sender, EventArgs e)
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

        /// <summary>
        /// Método para buscar un empleado por nómina
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Validar si el campo nomina está vacío
            if (string.IsNullOrWhiteSpace(txtNoNomina.Text))
            {
                MessageBox.Show("El campo de Nómina no puede estar vacío.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar si el campo Nómina es valido 
            if (!int.TryParse(txtNoNomina.Text.Trim(), out int idNomina))
            {
                MessageBox.Show("El No. de Nómina debe ser un número válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                var nominaController = new NominasController();
                var nomina = nominaController.BuscarNominaPorId(idNomina);

                if (nomina == null)
                {
                    MessageBox.Show("No se encontró la nómina.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                txtNombreEmpleado.Text = nomina.NombreEmpleado;
                txtEstadoDePago.Text = nomina.EstadoPago;

                // Asignar el ID de la nómina y el ID del empleado
                this.IdNomina = nomina.IdNomina;
                this.IdEmpleado = nomina.IdEmpleado;

                // Seleccionar el estado actual en el combo
                cBoxEstatusNomina.SelectedIndex = cBoxEstatusNomina.FindStringExact(nomina.EstadoPago);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar la nómina: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnActualizarCambios_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNoNomina.Text)) //si esto esta vacio, mandamos el sig msj
            {
                MessageBox.Show("El campo de No. de Nómina no puede estar vacío", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (this.IdNomina <= 0)
            {
                MessageBox.Show("Debes buscar primero la nómina para actualizar su estado.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Validar si el campo Nómina es valido
            string nuevoEstado = cBoxEstatusNomina.Text;

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
                    // Realizar actualización
                    var nominaController = new NominasController();
                    var resultado = nominaController.ActualizarEstadoPago(this.IdNomina, nuevoEstado);

                    if (resultado > 0)
                    {
                        MessageBox.Show("Estado de pago actualizado correctamente.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtEstadoDePago.Text = nuevoEstado;
                    }
                    else
                    {
                        MessageBox.Show("No se pudo actualizar el estado de pago.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                });
                timer.Dispose();// Liberar recursos del Timer
            };
            timer.Start();

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {

            if (this.IdNomina <= 0)
            {
                MessageBox.Show("Debes buscar primero la nómina antes de modificar percepciones y deducciones.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                mensajeForm.Invoke((MethodInvoker)delegate
                {
                    // Cerrar el formulario de mensaje
                    mensajeForm.Close();
                    //});

                    //// Ejecutar en el hilo principal
                    //this.Invoke((MethodInvoker)delegate //asistente solo ve y dile a chef y delega la tarea
                    //{
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
                //};
                timer.Dispose();
            };
            timer.Start();
        }

        private void btnVisualizarNomina_Click(object sender, EventArgs e)
        {
            // Verificar si la nómina es válida
            if (this.IdNomina <= 0)
            {
                MessageBox.Show("Debes buscar primero una nómina para visualizar el recibo.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Mostrar un mensaje temporal
            Form mensajeForm = new Form
            {
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterScreen,
                Size = new System.Drawing.Size(350, 220),
                Text = "Cargando Recibo",
                ControlBox = false // Evita cerrar manualmente
            };

            Label lblMensaje = new Label
            {
                Text = "Generando vista previa del recibo de nómina...",
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
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
                this.Invoke((MethodInvoker)delegate
                {
                    // Obtener el contenedor padre del UserControl actual
                    Control parent = this.Parent;

                    if (parent != null)
                    {
                        // Remover el UserControl actual
                        parent.Controls.Remove(this);

                        // Crear una nueva instancia de UC_NominaRecibo pasándole la ID de nómina
                        UC_NominaRecibo recibo = new UC_NominaRecibo(this.IdNomina);
                        recibo.Dock = DockStyle.Fill;

                        // Agregar el nuevo UserControl al contenedor
                        parent.Controls.Add(recibo);
                        parent.Controls.SetChildIndex(recibo, 0); // Poner al frente
                    }
                });
            };

            timer.Start();
        }
    }
}
