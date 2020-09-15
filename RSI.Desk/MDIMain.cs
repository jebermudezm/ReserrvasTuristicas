using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RSI.Desk
{
    public partial class MDIMain : Form
    {
        private int childFormNumber = 0;

        public MDIMain()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

       

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {

        }

        private void MDIMain_Load(object sender, EventArgs e)
        {
            menuStrip1.Visible = false;
            Login formaLogin = new Login(this);
            formaLogin.MdiParent = this;
            formaLogin.Show();
        }
        public void ActivarMenu()
        {
            menuStrip1.Visible = true;
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var forma = new MaestroClientes();
            forma.MdiParent = this;
            forma.Show();
        }

        private void proveedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var forma = new MaestroProveedor();
            forma.MdiParent = this;
            forma.Show();
        }

        private void destinosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var forma = new MaestroDestino();
            forma.MdiParent = this;
            forma.Show();
        }

        private void planesBaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var forma = new MaestroPlanBase();
            forma.MdiParent = this;
            forma.Show();
        }

        private void conveniosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var forma = new MaestroConvenio();
            forma.MdiParent = this;
            forma.Show();
        }

        private void planesTuristicosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var forma = new MaestroPlanTuristico
            {
                MdiParent = this
            };
            forma.Show();
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var forma = new TestItestSharp
            {
                MdiParent = this
            };
            forma.Show();
        }
    }
}
