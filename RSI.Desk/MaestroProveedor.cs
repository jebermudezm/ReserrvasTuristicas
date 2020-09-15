using RSI.Negocio;
using System;
using System.Linq;
using System.Windows.Forms;

namespace RSI.Desk
{
    public partial class MaestroProveedor : Form
    {
        private ProveedorNegocio proveedorsNegocio;
        public MaestroProveedor()
        {
            InitializeComponent();
            proveedorsNegocio = new ProveedorNegocio();
        }

        private void MaestroProveedor_Load(object sender, EventArgs e)
        {
            LlenarGrid();
            var tipoDocumentos = proveedorsNegocio.ObtenerDocumentos();
            cmbTipoDocumento.DisplayMember = "Descripcion";
            cmbTipoDocumento.ValueMember = "Id";
            cmbTipoDocumento.DataSource = tipoDocumentos.ToArray();
        }

        private void LlenarGrid()
        {
            var tipoDocumentos = proveedorsNegocio.ObtenerDocumentos();
            var Proveedores = proveedorsNegocio.ObtenerTodos();
            var listaProveedores = (from prov in Proveedores join docs in tipoDocumentos on prov.DocumentoIdentidadId equals docs.Id
                                 select new {ProveedorId = prov.Id, DocumentoIdentidadId = docs.Id, CodigoDocumento = docs.Codigo,
                                     TipoDocumento = docs.Descripcion, NúmeroDocumento = prov.NumeroDocumentoIdentidad,
                                     Nombre = prov.NombreORazonSocial, Contacto = prov.Contacto, Dirección = prov.Direccion,
                                     Teléfono = prov.Telefono,  Email = prov.Correo, Observación = prov.Observacion}).ToList();
            dataGridView1.DataSource = listaProveedores;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarCampos())
                {
                    var ProveedorId = int.Parse(txtId.Text == "" ? "-1" : txtId.Text);
                    var documentoId = int.Parse(cmbTipoDocumento.SelectedValue.ToString());
                    proveedorsNegocio.Guardar(ProveedorId, documentoId, txtNumeroDocumento.Text, txtNombre.Text, txtContacto.Text, txtDireccion.Text, txtTelefono.Text, txtCorreo.Text, txtObservacion.Text, Generales.UsuarioLogueado);
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
            else if(txtNumeroDocumento.Text == "")
                msg = "El número de documento es un campo requerido";
            else if(txtNombre.Text == "")
                msg = "El nombre es un campo requerido";
            else if(txtContacto.Text == "")
                msg = "El contacto es un campo requerido";
            else if(txtDireccion.Text == "")
                msg = "La dirección es un campo requerido";
            else if(txtTelefono.Text == "")
                msg = "El teléfono es un campo requerido";
            if (msg == "")
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
                cmbTipoDocumento.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                txtNumeroDocumento.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                txtNombre.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                txtContacto.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                txtDireccion.Text = dataGridView1.CurrentRow.Cells[7].Value?.ToString() ?? "";
                txtTelefono.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
                txtCorreo.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
                txtObservacion.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
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
            txtDireccion.Text = "";
            txtContacto.Text = "";
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
