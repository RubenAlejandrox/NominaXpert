using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NominaXpert.Controller;
using NominaXpert.Model;

namespace NominaXpert.View.UsersControl
{
    public partial class UC_NominaDeducciones : UserControl
    {
        public int IdNomina { get; set; }

        private readonly DeduccionController _deduccionController; // Controlador de empleados
        private readonly DetalleNominaController _detalleNominaController;
        private readonly Dictionary<int, string> _tiposDeducciones;


        public UC_NominaDeducciones(int idNomina)
        {
            if (idNomina <= 0)
                throw new ArgumentException("La ID de nómina debe ser mayor a 0.");
            InitializeComponent();
            _deduccionController = new DeduccionController();
            _detalleNominaController = new DetalleNominaController();

            _tiposDeducciones = new Dictionary<int, string>
    {
        { 1, "ISR" },
        { 2, "IMSS" },
        { 3, "INFONAVIT" },
        { 4, "FALTAS" },
        { 5, "RETARDOS" },
        { 6, "PENSION ALIMENTICIA" },
        { 7, "OTROS" }
    };

            this.IdNomina = idNomina;
            txtIdNomina.Text = idNomina.ToString();

            PoblaComboTipo();
            CargarDeducciones();
        }
        private void PoblaComboTipo()
        {
            // Asignar el diccionario al combo
            cboTipo.DropDownStyle = ComboBoxStyle.DropDownList;   // Solo permitir seleccionar opciones (no escribir texto)
                                                                  // Asignar el diccionario al combo
            cboTipo.DataSource = new BindingSource(_tiposDeducciones, null);
            cboTipo.DisplayMember = "Value";
            cboTipo.ValueMember = "Key";
            cboTipo.SelectedValue = 1; // ISR por defecto
                                       // Validación adicional (por si el valor 1 no existiera en el diccionario)
            if (cboTipo.SelectedValue == null && cboTipo.Items.Count > 0)
            {
                cboTipo.SelectedIndex = 0; // Por seguridad, seleccionar el primer valor
            }
        }

        public void CargarDeducciones()
        {
            if (this.IdNomina <= 0)
            {
                MessageBox.Show("ID de nómina no válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                dataGridViewDeducciones.Rows.Clear();
                var bonificaciones = _deduccionController.ObtenerDeduccionesPorNomina(this.IdNomina);

                foreach (var b in bonificaciones)
                {
                    dataGridViewDeducciones.Rows.Add(
                        b.Id,
                        b.IdNomina,
                        _tiposDeducciones[b.IdTipo],
                        b.Monto
                    );
                }

                //if (bonificaciones.Count == 0)
                //{
                //    MessageBox.Show("No hay percepciones registradas.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar percepciones: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarCampos()
        {
            txtMonto.Clear();
            cboTipo.SelectedValue = 1;// Establecer el valor por defecto del ComboBox
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
                Text = "Regresando a Percepciones...",
                AutoSize = false, //evita su crecimiento
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill //redimensiona al panel debajo
            };

            mensajeForm.Controls.Add(lblMensaje);
            mensajeForm.Show();

            // Configurar el temporizador para cerrar la ventana después de 2 segundos y cambiar de UC
            System.Timers.Timer timer = new System.Timers.Timer(1000);
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

        private void btnSiguiente_Click(object sender, EventArgs e)
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
                Text = "Generando Recibo de Nómina del Empleado...",
                AutoSize = false, //evita su crecimiento
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill //redimensiona al panel debajo
            };

            mensajeForm.Controls.Add(lblMensaje);
            mensajeForm.Show();

            // Configurar el temporizador para cerrar la ventana después de 2 segundos y cambiar de UC
            System.Timers.Timer timer = new System.Timers.Timer(1000);
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
                        UC_NominaRecibo ucRecibo = new UC_NominaRecibo();
                        ucRecibo.Dock = DockStyle.Fill;

                        // Agregar el nuevo UserControl al mismo contenedor
                        parent.Controls.Add(ucRecibo);
                        parent.Controls.SetChildIndex(ucRecibo, 0); // Ponerlo al frente
                    }
                });
            };
            timer.Start();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Validación de campos
            if (string.IsNullOrWhiteSpace(txtMonto.Text))
            {
                MessageBox.Show("El monto no puede estar vacío.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validación de que el monto tenga un máximo de 2 decimales
            if (!decimal.TryParse(txtMonto.Text, out decimal monto) || monto != Math.Round(monto, 2))
            {
                MessageBox.Show("El monto debe tener un máximo de 2 decimales.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validación de que el monto sea mayor a 0
            if (monto <= 0)
            {
                MessageBox.Show("El monto debe ser mayor a 0.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Crear la deducción
            var deduccion = new Deduccion
            {
                IdNomina = this.IdNomina, // Usamos la ID de la nómina pasada desde el otro UC
                IdTipo = Convert.ToInt32(cboTipo.SelectedValue),
                Monto = monto
            };

            // Configurar un ProgressBar para mostrar que el proceso está en curso
            ProgressBar progressBar = new ProgressBar();
            progressBar.Style = ProgressBarStyle.Marquee;
            progressBar.Dock = DockStyle.Fill;
            this.Controls.Add(progressBar);
            progressBar.BringToFront();
            this.Cursor = Cursors.WaitCursor; // Cambiar el cursor a un "wait" mientras se realiza el proceso

            try
            {
                // Llamamos al controlador para registrar la deducción
                _deduccionController.RegistrarDeduccion(deduccion);

                // Guardamos el detalle de la nómina
                var detalleNomina = new DetalleNomina
                {
                    IdNomina = this.IdNomina,  // Usamos la misma ID de la nómina
                    Descripcion = "Movimiento que resta al salario del empleado",  // Descripción fija
                    Monto = monto,  // Monto de la deducción
                    Tipo = "Deducción"  // Tipo fijo como "Deduccion"
                };

                // Registrar el detalle de la nómina
                _detalleNominaController.RegistrarDetalleNomina(detalleNomina);

                // Si el proceso fue exitoso, se muestra un mensaje y se limpia el formulario
                MessageBox.Show("Deducción guardada correctamente y detalle registrado.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Limpiar los campos después de guardar
                LimpiarCampos();
                CargarDeducciones();
            }
            catch (Exception ex)
            {
                // En caso de error, mostramos el mensaje correspondiente
                MessageBox.Show($"Error al guardar la Deducción: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // En caso de error, verificar si las tablas fueron llenadas correctamente
                MessageBox.Show("No se pudieron insertar los registros en las tablas. Verifica la base de datos. Contacta al administrador", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                // Limpiar el ProgressBar y restablecer el cursor
                progressBar.Visible = false;
                this.Cursor = Cursors.Default;
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Verificar si se ha seleccionado una fila
            if (dataGridViewDeducciones.SelectedRows.Count > 0)
            {
                var row = dataGridViewDeducciones.SelectedRows[0];
                var idDeduccion = Convert.ToInt32(row.Cells["id"].Value);  // Obtener ID de la deducción

                // Confirmar la eliminación
                var result = MessageBox.Show("¿Estás seguro de que deseas eliminar esta deducción?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        // Llamar controlador para eliminar la deducción
                        var rowsAffected = _deduccionController.EliminarDeduccion(idDeduccion, IdNomina);
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Deducción eliminada correctamente.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CargarDeducciones(); // Recargar las deducciones
                        }
                        else
                        {
                            MessageBox.Show("No se pudo eliminar la deducción.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al eliminar la deducción: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona una deducción para eliminar.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {

            // Verificar si se ha seleccionado una fila
            if (dataGridViewDeducciones.SelectedRows.Count > 0)
            {
                var row = dataGridViewDeducciones.SelectedRows[0];

                // Verificar que las celdas tengan valores
                if (row.Cells["id"].Value != null && row.Cells["Tipo"].Value != null && row.Cells["Monto"].Value != null)
                {
                    var idDeduccion = Convert.ToInt32(row.Cells["id"].Value);  // Obtener ID de la deducción
                    var tipo = row.Cells["Tipo"].Value.ToString();
                    var monto = Convert.ToDecimal(row.Cells["Monto"].Value);

                    // Mostrar los valores antes de proceder
                    MessageBox.Show($"Modificando deducción: ID: {idDeduccion}, Tipo: {tipo}, Monto: {monto}");


                    // Cargar los datos de la deducción seleccionada en los campos
                    cboTipo.SelectedItem = tipo;  // Puede necesitar mapeo para la selección correcta
                    txtMonto.Text = monto.ToString();

                    // Crear la deducción para la actualización
                    var deduccion = new Deduccion
                    {
                        Id = idDeduccion,
                        IdNomina = this.IdNomina,  // Usamos la ID de la nómina pasada
                        IdTipo = Convert.ToInt32(cboTipo.SelectedValue),
                        Monto = Convert.ToDecimal(txtMonto.Text)
                    };

                    try
                    {
                        // Llamamos al controlador para actualizar la dedicción
                        var rowsAffected = _deduccionController.ActualizarDeduccion(deduccion);
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Deducción actualizada correctamente.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CargarDeducciones(); // Recargar las deducciones
                            LimpiarCampos(); // Limpiar los campos después de modificar
                        }
                        else
                        {
                            MessageBox.Show("No se pudo actualizar la deducción.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al actualizar la deducción: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Algunas celdas están vacías. Por favor, verifique los datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona una deducción para modificar.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
