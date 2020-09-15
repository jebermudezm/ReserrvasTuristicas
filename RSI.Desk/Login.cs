using RSI.Negocio;
using System;
using System.Windows.Forms;

namespace RSI.Desk
{
    public partial class Login : Form
    {
        private MDIMain mdiMain;
        public Login(MDIMain mDIMain)
        {
            this.mdiMain = mDIMain;
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Debes digitar el usuario.");
                return;
            }
            if (textBox2.Text == "")
            {
                MessageBox.Show("Debes digitar la Contraseña.");
                return;
            }
            var loginNegocio = new LoginNegocio();
            var usuario = loginNegocio.ObtenerUsuario(textBox1.Text, textBox2.Text);
            if (usuario == null)
            {
                MessageBox.Show("El usuario y contraseña digitados no son válidos.");
                return;
            }
            Generales.UsuarioLogueado = usuario;
            mdiMain.ActivarMenu();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
