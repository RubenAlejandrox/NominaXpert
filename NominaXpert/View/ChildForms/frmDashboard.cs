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

namespace NominaXpert.View.Forms
{
    public partial class frmDashboard : Form
    {
        public frmDashboard()
        {
            InitializeComponent();  
        }

        private void horaFecha_Tick_1(object sender, EventArgs e)
        {
            lblfecha.Text = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToString("hh:mm:ss");
        }

        private void frmDashboard_Load(object sender, EventArgs e)
        {
            InicializarDashboard();
            CargarEmpleadosActivosYInactivos();
        }

        public void InicializarDashboard()
        {

            lbltext1.Dock = DockStyle.Fill; // El label ocupará todo el espacio disponible
            lbltext1.TextAlign = ContentAlignment.MiddleLeft;
            lbltext1.Text = "Automatiza el cálculo y procesamiento de nóminas con precisión. Gestiona pagos, deducciones y beneficios de manera eficiente.";
            lbltext2.Dock = DockStyle.Fill;
            lbltext2.TextAlign = ContentAlignment.MiddleLeft;
            lbltext2.Text = "Administra la información de tus empleados, seguimiento de días trabajados, informes personales, auditoría y documentación en un solo lugar.";
            lbltext3.Dock = DockStyle.Fill;
            lbltext3.TextAlign = ContentAlignment.MiddleLeft;
            lbltext3.Text = "Genera informes detallados, analiza costos y obtén insights valiosos para la toma de decisiones estratégicas.";
        }

        private void CargarEmpleadosActivosYInactivos()
        {
            try
            {
                EmpleadosDataAccess empleadosDataAccess = new EmpleadosDataAccess();

                // Traemos TODOS los empleados (activos e inactivos)
                List<Empleado> todosLosEmpleados = empleadosDataAccess.ObtenerTodosLosEmpleados(false);

                // Contamos cuántos son activos e inactivos
                int empleadosActivos = todosLosEmpleados.Count(e => e.Estatus == true);
                int empleadosInactivos = todosLosEmpleados.Count(e => e.Estatus == false);

                // Mostrar los resultados en los Labels con el texto solicitado
                lblEmpleadosActivos.Text = $"ACTIVOS: {empleadosActivos}";
                lblEmpleadosInactivos.Text = $"INACTIVOS: {empleadosInactivos}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el número de empleados: " + ex.Message);
            }
        }

    }
}
