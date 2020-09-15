using RSI.Negocio;
using System;
using System.Linq;
using System.Windows.Forms;

namespace RSI.Desk
{
    public partial class MaestroConvenio : Form
    {
        private ConvenioNegocio  convenioNegocio;
        public MaestroConvenio()
        {
            InitializeComponent();
            convenioNegocio = new ConvenioNegocio();
        }

        private void MaestroProveedor_Load(object sender, EventArgs e)
        {
            LlenarGrid();
        }

        private void LlenarGrid()
        {
            var convenios = convenioNegocio.ObtenerTodos();
            var listaConvenios = (from conv in convenios
                                 select new {DestinoId = conv.Id, Convenio = conv.Nombre, Descruento = conv.Descuento,
                                     Observación = conv.Observacion}).ToList();
            dataGridView1.DataSource = listaConvenios;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarCampos())
                {
                    var convenioId = int.Parse(txtId.Text == "" ? "-1" : txtId.Text);
                    convenioNegocio.Guardar(convenioId, txtNombre.Text, double.Parse(txtDescuento.Text), txtObservacion.Text, Generales.UsuarioLogueado);
                    LlenarGrid();
                    LimpiarControles();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Ocurrió el siguiente error: {ex.Message}");
            }
            
        }

        private bool ValidarCampos()
        {
            var retorno = true;
            var msg = "";
            if(txtNombre.Text == "")
                msg = "El nombre es un campo requerido";
            else if(txtDescuento.Text == "")
                msg = "El descuento es un campo requerido";
            else if (double.Parse(txtDescuento.Text) < 0 && double.Parse(txtDescuento.Text) > 100)
                msg = "El porcentaje de descuento debe ser un numero entre 0 y 100.";
            if (msg != "")
            {
                MessageBox.Show(msg);
                retorno = false;
            }
            
            return retorno;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtId.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                txtNombre.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                txtDescuento.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                txtObservacion.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Ocurrió el siguiente error: {ex.Message}");
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                LimpiarControles();
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Ocurrió el siguiente error: {ex.Message}");
            }
            
        }

        private void LimpiarControles()
        {
            txtId.Text = "";
            txtNombre.Text = "";
            txtDescuento.Text = "";
            txtObservacion.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
