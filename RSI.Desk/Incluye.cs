using RSI.Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RSI.Desk
{
    public partial class Incluye : Form
    {
        int idPlan;
        IncluyeNegocio incluyeNegocio;
        public Incluye(int id)
        {
            idPlan = id;
            incluyeNegocio = new IncluyeNegocio();
            InitializeComponent();
        }

        private void Incluye_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            var listaInclye = incluyeNegocio.ObtenerPorPlan(idPlan);
            int i = 0;
            foreach (var item in listaInclye)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = item.Id;
                dataGridView1.Rows[i].Cells[1].Value = item.Codigo;
                dataGridView1.Rows[i].Cells[2].Value = item.Descripcion;
                dataGridView1.Rows[i].Cells[3].Value = item.Observacion;
                dataGridView1.Rows[i].Cells[4].Value = "Ver Detalle";
                    i++;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    var id = int.Parse(dataGridView1.Rows[i].Cells[0].Value?.ToString() ?? "-1");
                    var codigo = dataGridView1.Rows[i].Cells[1].Value?.ToString() ?? "";
                    var descripcion = dataGridView1.Rows[i].Cells[2].Value?.ToString() ?? "";
                    var observacion = dataGridView1.Rows[i]?.Cells[3].Value?.ToString() ?? "";
                    if (codigo != "" && descripcion != "" && observacion != "")
                    {
                        var idincluye = incluyeNegocio.Guardar(id, codigo, descripcion, observacion, idPlan, Generales.UsuarioLogueado);
                    }

                }
                MessageBox.Show("El detalle se guardó con exito.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió el siguiente error: {ex.Message}.");
            }
            
            
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {

            dataGridView1.Rows.Add();
        }

        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            MessageBox.Show(e.RowIndex.ToString());
        }
    }
}
