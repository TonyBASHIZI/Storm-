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
            textBox2.Text = glos.chargerPourcentN1();
            textBox1.Text = glos.chargerPourcentN0();
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
                p.Date_fin =DateTime.Parse(dateTimePicker1.Text);

                glos.updatedPourcent(p);
                textBox2.Text = glos.chargerPourcentN1();
                textBox1.Text = glos.chargerPourcentN0();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            ClassPrix clp = new ClassPrix();
            clp.PrixE = Double.Parse(txtpEs.Text);
            clp.PrixD = Double.Parse(txtPdies.Text);
            clp.PrixP = Double.Parse(txtPetro.Text);

            glos.updatedPDiesel(clp);
            glos.updatedPEss(clp);
            glos.updatedPPetro(clp);

        }
    }
}
