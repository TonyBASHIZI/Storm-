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
            if (textBox1.Text == "" || textBox2.Text == "" || comboBox1.Text == "Selectionnez Option" || comboBox1.Text =="")
            {
                MessageBox.Show("Tous les champs doivent etre completé!");

            }
            else
            {
                try
                {

                    if (comboBox1.Text == "STATION")
                    {
                        if (glos.LoginStation(textBox1.Text, textBox2.Text) == true)
                        {
                            formPrinciapal pc = new formPrinciapal(textBox1.Text);
                            this.Hide();
                            pc.ShowDialog();
                        }

                    }
                    else if (comboBox1.Text == "AUTRE")
                    {
                        if (glos.LoginTest(textBox1.Text, textBox2.Text) == true)
                        {
                            formPrinciapal pc = new formPrinciapal(textBox1.Text);
                            this.Hide();
                            pc.ShowDialog();
                        }
                    }
                    else if (comboBox1.Text == "BUSINESS")
                    {
                        if (glos.LoginStation(textBox1.Text, textBox2.Text) == true)
                        {
                            formPrinciapal pc = new formPrinciapal(textBox1.Text);
                            this.Hide();
                            pc.ShowDialog();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Selectionnez une option!!");
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Verifier la connexion avec le serveur!!"+ex.Message);
                }

            }

           
        }
        
        private void authentification_Load(object sender, EventArgs e)
        {
            //glos.IDusername(comboBox1);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
