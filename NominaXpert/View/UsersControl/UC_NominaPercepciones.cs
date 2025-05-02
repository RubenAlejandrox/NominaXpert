using NominaXpert.Controller;
using NominaXpert.Data;
using NominaXpert.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NominaXpert.View.UsersControl
{
    public partial class UC_NominaPercepciones : UserControl
    {

        // Propiedad pública para almacenar la ID de la nómina
        public int IdNomina { get; set; }

        private readonly BonificacionController _bonificacionController; // Controlador de empleados


        public UC_NominaPercepciones()
        {
            InitializeComponent();
            _bonificacionController = new BonificacionController();
        }

        private void UC_NominaPercepciones_Load(object sender, EventArgs e)
        {
            
        }

        public void InicializaVentana()
        {
            PoblaComboTipo();

        }

        public void CargarBonificaciones()
        {
            // Asegúrate de que el valor de la ID de la nómina es correcto
            int idNomina = 0;
            if (int.TryParse(txtIdNomina.Text, out idNomina))
            {
                this.IdNomina = idNomina;
            }
            else
            {
                MessageBox.Show("La ID de la nómina es inválida o no ha sido pasada correctamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (idNomina <= 0)
            {
                MessageBox.Show("La ID de la nómina es inválida o no ha sido pasada correctamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // No continuar si la ID de la nómina no es válida
            }

            // Usar esa ID para cargar las bonificaciones asociadas a la nómina
            try
            {
                List<Bonificacion> bonificaciones = _bonificacionController.ObtenerBonificacionesPorNomina(idNomina);

                // Verificar si no se obtuvieron bonificaciones
                if (bonificaciones.Count == 0)
                {
                    MessageBox.Show("No se encontraron bonificaciones asociadas a esta nómina.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Poblar el DataGridView con las bonificaciones obtenidas
                foreach (var bonificacion in bonificaciones)
                {
                    DataGridViewPercepciones.Rows.Add(bonificacion.IdNomina, bonificacion.IdTipo, bonificacion.Monto);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las bonificaciones: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PoblaComboTipo()
        {
            //Crear un diccionario de valores
            Dictionary<int, string> list_estatus = new Dictionary<int, string>
            {
                { 1, "Horas Extras" },
                { 2, "Comisión" },
                { 3, "Incentivo" },
                { 4, "Asistencia" },
                { 5, "Puntualidad" },
                { 6, "Retroactivo" },
                { 7, "Otros" }
            };
            //Asignar el diccionario al combo
            cboTipo.DataSource = new BindingSource(list_estatus, null);
            cboTipo.DisplayMember = "Value";
            cboTipo.ValueMember = "Key";

            cboTipo.SelectedValue = 1;
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
                Text = "Guardando Percepciones...",
                AutoSize = false, // Evita que se expanda
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
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

                        // Crear una nueva instancia de UC_NominaDeducciones
                        UC_NominaDeducciones ucRecibo = new UC_NominaDeducciones();
                        ucRecibo.Dock = DockStyle.Fill;

                        // Agregar el nuevo UserControl al mismo contenedor
                        parent.Controls.Add(ucRecibo);
                        parent.Controls.SetChildIndex(ucRecibo, 0); // Ponerlo al frente
                    }
                });
            };
            timer.Start();
        }

        private void LimpiarCampos()
        {
            txtMonto.Clear();
            cboTipo.SelectedValue = 1; // Establecer el valor por defecto del ComboBox
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

            // Crear la bonificación
            var bonificacion = new Bonificacion
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
                // Llamamos al controlador para registrar la bonificación
                _bonificacionController.RegistrarBonificacion(bonificacion);

                // Guardamos el detalle de la nómina
                var detalleNomina = new DetalleNomina
                {
                    IdNomina = this.IdNomina,  // Usamos la misma ID de la nómina
                    Descripcion = "Movimiento que suma al salario del empleado",  // Descripción fija
                    Monto = monto,  // Monto de la bonificación
                    Tipo = "Ingreso"  // Tipo fijo como "Ingreso"
                };

                // Registrar el detalle de la nómina
                var detalleNominaController = new DetalleNominaController();
                detalleNominaController.RegistrarDetalleNomina(detalleNomina);

                // Si el proceso fue exitoso, se muestra un mensaje y se limpia el formulario
                MessageBox.Show("Bonificación guardada correctamente y detalle registrado.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Limpiar los campos después de guardar
                LimpiarCampos();

                // Liberar el siguiente UserControl (en este caso, se podría redirigir a otro UC)
                Control parent = this.Parent;
                if (parent != null)
                {
                    parent.Controls.Remove(this);  // Remover el UC actual

                    // Crear una nueva instancia del siguiente UserControl
                    UC_NominaDeducciones ucDeducciones = new UC_NominaDeducciones();
                    ucDeducciones.Dock = DockStyle.Fill;

                    // Agregar el nuevo UserControl al contenedor
                    parent.Controls.Add(ucDeducciones);
                    parent.Controls.SetChildIndex(ucDeducciones, 0);  // Ponerlo al frente
                }
            }
            catch (Exception ex)
            {
                // En caso de error, mostramos el mensaje correspondiente
                MessageBox.Show($"Error al guardar la bonificación: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
            if (DataGridViewPercepciones.SelectedRows.Count > 0)
            {
                var row = DataGridViewPercepciones.SelectedRows[0];
                var idBonificacion = Convert.ToInt32(row.Cells["Id_nomina"].Value); // Obtener ID de la bonificación

                // Confirmar la eliminación
                var result = MessageBox.Show("¿Estás seguro de que deseas eliminar esta bonificación?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        // Llamamos al controlador para eliminar la bonificación
                        var rowsAffected = _bonificacionController.EliminarBonificacion(idBonificacion);
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Bonificación eliminada correctamente.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CargarBonificaciones(); // Recargar las bonificaciones
                        }
                        else
                        {
                            MessageBox.Show("No se pudo eliminar la bonificación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al eliminar la bonificación: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona una bonificación para eliminar.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            // Verificar si se ha seleccionado una fila
            if (DataGridViewPercepciones.SelectedRows.Count > 0)
            {
                var row = DataGridViewPercepciones.SelectedRows[0];
                var idBonificacion = Convert.ToInt32(row.Cells["Id_nomina"].Value); // Obtener ID de la bonificación
                var tipo = row.Cells["Tipo"].Value.ToString();
                var monto = Convert.ToDecimal(row.Cells["Monto"].Value);

                // Cargar los datos de la bonificación seleccionada en los campos
                cboTipo.SelectedItem = tipo;  // Puede necesitar mapeo para la selección correcta
                txtMonto.Text = monto.ToString();

                // Crear la bonificación para la actualización
                var bonificacion = new Bonificacion
                {
                    Id = idBonificacion,
                    IdNomina = this.IdNomina,  // Usamos la ID de la nómina pasada
                    IdTipo = Convert.ToInt32(cboTipo.SelectedValue),
                    Monto = Convert.ToDecimal(txtMonto.Text)
                };

                try
                {
                    // Llamamos al controlador para actualizar la bonificación
                    var rowsAffected = _bonificacionController.ActualizarBonificacion(bonificacion);
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Bonificación actualizada correctamente.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarBonificaciones(); // Recargar las bonificaciones
                        LimpiarCampos(); // Limpiar los campos después de modificar
                    }
                    else
                    {
                        MessageBox.Show("No se pudo actualizar la bonificación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al actualizar la bonificación: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona una bonificación para modificar.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void UC_NominaPercepciones_Load_1(object sender, EventArgs e)
        {
            InicializaVentana();
            CargarBonificaciones();
        }
    }
}
