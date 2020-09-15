using RSI.Negocio;
using System;
using System.Linq;
using System.Windows.Forms;

namespace RSI.Desk
{
    public partial class MaestroDestino : Form
    {
        private DestinoNegocio destinosNegocio;
        public MaestroDestino()
        {
            InitializeComponent();
            destinosNegocio = new DestinoNegocio();
        }

        private void MaestroProveedor_Load(object sender, EventArgs e)
        {
            LlenarGrid();
        }

        private void LlenarGrid()
        {
            var destinos = destinosNegocio.ObtenerTodos();
            var listaDestinos = (from dest in destinos
                                 select new {DestinoId = dest.Id, Código = dest.Codigo, Descripción = dest.Descripcion,
                                     Observación = dest.Observacion}).ToList();
            dataGridView1.DataSource = listaDestinos;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarCampos())
                {
                    var destinoId = int.Parse(txtId.Text == "" ? "-1" : txtId.Text);
                    destinosNegocio.Guardar(destinoId, txtCodigo.Text, txtdescripcion.Text, txtObservacion.Text, Generales.UsuarioLogueado);
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
            if(txtCodigo.Text == "")
                msg = "El código es un campo requerido";
            else if(txtdescripcion.Text == "")
                msg = "La descripción es un campo requerido";
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
                txtCodigo.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                txtdescripcion.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
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
            txtCodigo.Text = "";
            txtdescripcion.Text = "";
            txtObservacion.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
