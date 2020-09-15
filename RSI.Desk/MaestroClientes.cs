using RSI.Negocio;
using System;
using System.Linq;
using System.Windows.Forms;

namespace RSI.Desk
{
    public partial class MaestroClientes : Form
    {
        private ClienteNegocio clientesNegocio;
        public MaestroClientes()
        {
            InitializeComponent();
            clientesNegocio = new ClienteNegocio();
        }

        private void Clientes_Load(object sender, EventArgs e)
        {
            LlenarGrid();
            var tipoDocumentos = clientesNegocio.ObtenerDocumentos();
            cmbTipoDocumento.DisplayMember = "Descripcion";
            cmbTipoDocumento.ValueMember = "Id";
            cmbTipoDocumento.DataSource = tipoDocumentos.ToArray();
        }

        private void LlenarGrid()
        {
            var clientes = clientesNegocio.ObtenerTodos();
            //var listaClientes = (from cli in clientes
            //                     select new {CLienteId = cli.Id, DocumentoIdentidadId = cli.DocumentoIdentidad.Id, CodigoDocumento = cli.DocumentoIdentidad.Codigo,
            //                         TipoDocumento = cli.DocumentoIdentidad.Descripcion, NúmeroDocumento = cli.NumeroDocumentoIdentidad, Nombre = cli.NombreORazonSocial, Apodo = cli.Apodo,
            //                         FechaNacimiento = cli.FechaNacimiento, Dirección = cli.Direccion,
            //                         Teléfono = cli.Telefono,  Email = cli.Correo, Observación = cli.Observacion}).ToList();
            //dataGridView1.DataSource = listaClientes;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarCampos())
                {
                    var clienteId = int.Parse(txtId.Text == "" ? "-1" : txtId.Text);
                    var documentoId = int.Parse(cmbTipoDocumento.SelectedValue.ToString());
                    clientesNegocio.Guardar(clienteId, documentoId, txtNumeroDocumento.Text, txtNombre.Text, txtApodo.Text, dtpFechaNAcimiento.Value, txtDireccion.Text, txtTelefono.Text, txtCorreo.Text, txtObservacion.Text, Generales.UsuarioLogueado);
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
            if (cmbTipoDocumento.Text == "")
                msg = "El tipo de documento es un campo requerido";
            else if (txtNumeroDocumento.Text == "")
                msg = "El número de documento es un campo requerido";
            else if (txtNombre.Text == "")
                msg = "El nombre es un campo requerido";
            else if (txtApodo.Text == "")
                msg = "El contacto es un campo requerido";
            else if (dtpFechaNAcimiento.Text == "")
                msg = "La fecha de nacimiento es un campo requerido";
            else if (txtDireccion.Text == "")
                msg = "La dirección es un campo requerido";
            else if (txtTelefono.Text == "")
                msg = "El teléfono es un campo requerido";
            if (msg != "")
            {
                MessageBox.Show(msg);
                retorno = true;
            }
            
            return retorno;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtId.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                cmbTipoDocumento.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                txtNumeroDocumento.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                txtNombre.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                txtApodo.Text = dataGridView1.CurrentRow.Cells[6].Value?.ToString() ?? "";
                dtpFechaNAcimiento.Value = (DateTime)(dataGridView1.CurrentRow.Cells[7].Value ?? DateTime.Now);
                txtDireccion.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
                txtTelefono.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
                txtCorreo.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
                txtObservacion.Text = dataGridView1.CurrentRow.Cells[11].Value.ToString();
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
            txtNumeroDocumento.Text = "";
            txtNombre.Text = "";
            txtApodo.Text = "";
            dtpFechaNAcimiento.Value =  DateTime.Now;
            txtDireccion.Text = "";
            txtTelefono.Text = "";
            txtCorreo.Text = "";
            txtObservacion.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
