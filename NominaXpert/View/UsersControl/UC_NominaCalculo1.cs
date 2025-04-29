using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NominaXpert.Business;
using NominaXpert.Controller;
using NominaXpert.Utilities;

namespace NominaXpert.View.UsersControl
{
    public partial class UC_NominaCalculo1 : UserControl
    {
        public UC_NominaCalculo1()
        {
            InitializeComponent();
        }

        private void UC_NominaAlta_Load(object sender, EventArgs e)
        {
            InicializaVentanaCalculoRecibos();
        }

        public void InicializaVentanaCalculoRecibos()
        {
           
        }
      


        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // 1️ Validar si el campo está vacío
            if (string.IsNullOrWhiteSpace(txtMatricula.Text))
            {
                MessageBox.Show("El campo de matrícula no puede estar vacío.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2️ Validar si la matrícula tiene el formato correcto
            if (!EmpleadosNegocio.EsMatriculaValido(txtMatricula.Text.Trim()))
            {
                MessageBox.Show("Matrícula inválida. Verifique el formato.", "Información del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var controller = new EmpleadosController();
                var (nombre, sueldo) = controller.BuscarEmpleadoPorMatricula(txtMatricula.Text.Trim());

                // Mostrar datos en la UI
                txtNombreEmpleado.Text = nombre;
                txtSueldoBase.Text = sueldo.ToString("C");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       

        private void btnCalcularVerNomina_Click(object sender, EventArgs e)
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
                Text = "Generando Nómina del Empleado...",
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
                        UC_NominaPercepciones ucRecibo = new UC_NominaPercepciones();
                        ucRecibo.Dock = DockStyle.Fill;

                        // Agregar el nuevo UserControl al mismo contenedor
                        parent.Controls.Add(ucRecibo);
                        parent.Controls.SetChildIndex(ucRecibo, 0); // Ponerlo al frente
                    }
                });
            };
            timer.Start();

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        //private void btnCalcularVerNomina_Click(object sender, EventArgs e)
        //{
        //    UC_NominaRecibo ucr = new UC_NominaRecibo();
        //    Utilities.Formas.ActivateButton(sender, Formas.RGBColors.ChangeColor);
        //    ucr.Show();
        //}
    }
}
