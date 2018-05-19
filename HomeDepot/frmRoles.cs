using CN;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HomeDepot
{
    public partial class frmRoles : Form
    {
        public frmRoles()
        {
            InitializeComponent();
        }

        private void dgvRoles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmRoles_Load(object sender, EventArgs e)
        {
            cargarDatos();
        }

        private void cargarDatos()
        {
            dgvRoles.DataSource = Rol.traerTodos();
        }

        private void limpiarDatos()
        {
            txtNombre.Clear();
            txtDescripcion.Text = "";
            txtIdRol.Text = "0";
        }


        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text;
            string descripcion = txtDescripcion.Text;

            int idRol;

            int.TryParse(txtIdRol.Text, out idRol);

            bool esActivo = chkEsActivo.Checked;

            try
            {
                Rol objetoRol = new Rol(nombre, idRol, descripcion, esActivo);
                objetoRol.guardar();
                MessageBox.Show("Proceso Exitoso!");
                cargarDatos();
                limpiarDatos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void dgvRoles_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (e.Row.Index < 0)
            {
                return;
            }
            string nombre = e.Row.Cells["nombre"].Value.ToString();

            string descripcion = "";
            if (e.Row.Cells["descripcion"].Value != null)
            {
                descripcion = e.Row.Cells["descripcion"].Value.ToString();
            }

            string idRol = e.Row.Cells["idRol"].Value.ToString();
            string esActivo = e.Row.Cells["esActivo"].Value.ToString();
            //bool esActivo = Convert.ToBoolean(strEsActivo);

            txtIdRol.Text = idRol;
            txtNombre.Text = nombre;
            txtDescripcion.Text = descripcion;
            chkEsActivo.Checked = (esActivo == "True");

        }
    }
}
