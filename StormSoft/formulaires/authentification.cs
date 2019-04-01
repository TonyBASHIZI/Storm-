using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StormSoft.classe;

namespace StormSoft.formulaires
{
    public partial class authentification : Form
    {
        glossaire glos = new glossaire();
        public authentification()
        {
            InitializeComponent();
        }

      

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            
            try
            {
                
                
                if (glos.LoginTest(comboBox1.Text, textBox2.Text) == true)
                {
                    formPrinciapal pc = new formPrinciapal(comboBox1.Text);
                    this.Hide();
                    pc.ShowDialog();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void authentification_Load(object sender, EventArgs e)
        {
            glos.IDusername(comboBox1);
        }
    }
}
