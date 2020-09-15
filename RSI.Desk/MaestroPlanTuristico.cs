using RSI.Negocio;
using System;
using System.Linq;
using System.Windows.Forms;

namespace RSI.Desk
{
    public partial class MaestroPlanTuristico : Form
    {
        private PlanTuristicoNegocio planTuristicoNegocio;
        public MaestroPlanTuristico()
        {
            InitializeComponent();
            planTuristicoNegocio = new PlanTuristicoNegocio();
        }

        private void MaestroPlanTuristico_Load(object sender, EventArgs e)
        {
            LlenarGrid();
            CargarListasPlanes();
            var tipoDocumentos = planTuristicoNegocio.ObtenerDestinos();
            cmbDestino.DisplayMember = "Descripcion";
            cmbDestino.ValueMember = "Id";
            cmbDestino.DataSource = tipoDocumentos.ToArray();
        }

        private void CargarListasPlanes()
        {
            var listaPlanesBase = planTuristicoNegocio.ObtenerPlanBase();
            cmbPlanBase.DisplayMember = "Descripcion";
            cmbPlanBase.ValueMember = "Id";
            cmbPlanBase.DataSource = listaPlanesBase.ToArray();
        }

        private void LlenarGrid()
        {
            var planes = planTuristicoNegocio.ObtenerTodos();
            var listaPlanes = (from plan in planes
                                 select new {PlanId = plan.Id, DestinoId = plan.DestinoId, CodigoDestino= plan.Destino.Codigo,
                                     Destino = plan.Destino.Descripcion, Código = plan.Codigo,
                                     Descripción = plan.Descripcion, Hotel = plan.Hotel,
                                     plan.CostoAdulto, plan.CostoMenor, plan.CostoInfante, plan.ValorAdulto, plan.ValorMenor, plan.ValorInfante,
                                     Observación = plan.Observacion}).ToList();
            dataGridView1.DataSource = listaPlanes;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {// to do: revisar parametros enviados.
                if (ValidarCampos())
                {
                    var planId = int.Parse(txtId.Text == "" ? "-1" : txtId.Text);
                    var destinoId = int.Parse(cmbDestino.SelectedValue.ToString());
                    var proveedorId = int.Parse(cmbDestino.SelectedValue.ToString());
                    planTuristicoNegocio.Guardar(planId, destinoId, txtCodigo.Text, txtDescripcion.Text,
                        txtHotel.Text, DateTime.Now, DateTime.Now, 0,0,0,0,0,0, proveedorId,
                         txtObservacion.Text, Generales.UsuarioLogueado);
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
             if (txtCodigo.Text == "")
                msg = "El código es un campo requerido";
            else if (cmbDestino.Text == "")
                msg = "El destino es un campo requerido";
            else if(txtDescripcion.Text == "")
                msg = "La descripción es un campo requerido";
            else if(txtHotel.Text == "")
                msg = "El hotel es un campo requerido";
           
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
                cmbDestino.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                txtCodigo.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                txtDescripcion.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                txtHotel.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                txtObservacion.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
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
            txtDescripcion.Text = "";
            txtHotel.Text = "";
            txtCodigo.Text = "";
            txtObservacion.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
