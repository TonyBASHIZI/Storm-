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
    public partial class formPrixPourcentage : Form
    {
        glossaire glos = new glossaire();
        public formPrixPourcentage()
        {
            InitializeComponent();
        }

        private void formPrixPourcentage_Load(object sender, EventArgs e)
        {
            
            txtPdies.Text = glos.chargerPrixDie();
            txtpEs.Text = glos.chargerPrixE();
            txtPetro.Text = glos.chargerPrixPetro();
            
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            try
            {
                ClassPourcentage p = new ClassPourcentage();
                p.Niveau1 =int.Parse(textBox2.Text);
                p.Niveau0 = int.Parse(textBox1.Text);
                p.Id = int.Parse(labelID.Text);
                p.Categorie = comboBox1.Text;
                p.Date_fin =DateTime.Parse(dateTimePicker1.Text);

                glos.updatedPourcent(p);
                textBox2.Text = glos.chargerPourcentN1(comboBox1.Text);
                textBox1.Text = glos.chargerPourcentN0(comboBox1.Text);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {


            MessageBox.Show("Service non disponible!");
            //ClassPrix clp = new ClassPrix();
            //clp.PrixE = Double.Parse(txtpEs.Text);
            //clp.PrixD = Double.Parse(txtPdies.Text);
            //clp.PrixP = Double.Parse(txtPetro.Text);

           // glos.updatedPDiesel(clp);
            //glos.updatedPEss(clp);
           // glos.updatedPPetro(clp);

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            labelID.Text = glos.chargerID(comboBox1.Text);
            textBox2.Text = glos.chargerPourcentN1(comboBox1.Text);
            textBox1.Text = glos.chargerPourcentN0(comboBox1.Text);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            for (int i = 0; i < textBox1.Text.Length; i++)
            {
                if (!char.IsDigit(textBox1.Text, i))
                {
                    MessageBox.Show("Donnée incorrecte ce champ ne peut que prendre les chiffres entiers!!");
                    textBox1.Text = "";
                }
                else
                {

                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < textBox2.Text.Length; i++)
            {
                if (!char.IsDigit(textBox2.Text, i))
                {
                    MessageBox.Show("Donnée incorrecte ce champ ne peut que prendre les chiffres entiers!!");
                    textBox2.Text = "";
                }
                else
                {

                }
            }
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {

            panel2.BackColor = Color.Green;
            
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            panel2.BackColor = Color.Transparent;
        }
    }
}
