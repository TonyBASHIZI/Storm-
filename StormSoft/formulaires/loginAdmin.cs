using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StormSoft.formulaires
{
    public partial class loginAdmin : Form
    {
        public loginAdmin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox1.Text == "")
            {
                MessageBox.Show("Remplissez tous les champs svp!!");
            }
            else if (textBox1.Text == "Administrateur" && textBox2.Text == "niveauAdmin@@")
            {
                textBox1.Text ="";
                textBox2.Text = "";
                loginAdmin lo = new loginAdmin();
                lo.Hide();
                formPrixPourcentage p = new formPrixPourcentage();
                p.ShowDialog();

            }
            else if (textBox1.Text == "Niveau4" && textBox2.Text == "niveau4@")
            {
                textBox1.Text = "";
                textBox2.Text = "";
                loginAdmin lo = new loginAdmin();
                lo.Hide();
                formConsommer p = new formConsommer();
                p.ShowDialog();

            }
            else if (textBox1.Text == "Niveau3" && textBox2.Text == "54321@")
            {
                textBox1.Text = "";
                textBox2.Text = "";
                loginAdmin lo = new loginAdmin();
                lo.Hide();
                formUsers p = new formUsers();
                p.ShowDialog();

            }
            else if (textBox1.Text == "Niveau5" && textBox2.Text == "5991@")
            {
                textBox1.Text = "";
                textBox2.Text = "";
                loginAdmin lo = new loginAdmin();
                lo.Hide();
                V ag = new V();
                ag.ShowDialog();
            }
            else if (textBox1.Text == "Niveau6" && textBox2.Text == "202@")
            {
                textBox1.Text = "";
                textBox2.Text = "";
                loginAdmin lo = new loginAdmin();
                lo.Hide();
                formCommisssion ag = new formCommisssion();
                ag.ShowDialog();
            }


            
        }
    }
}
