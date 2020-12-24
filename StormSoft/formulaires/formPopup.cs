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
    public partial class formPopup : Form
    {
        glossaire glos = new glossaire();
        public formPopup(string mat, string station, string qte, string carbu)
        {
            InitializeComponent();
            lbmat.Text = mat;
            lbstation.Text = station;
            lbqte.Text = qte;
            lbcARBU.Text = carbu;
        }

        private void formPopup_Load(object sender, EventArgs e)
        {
            int pource = 0;
            lbesse.Text = glos.chargerPourcentN1(lbstation.Text);
            lbmaz.Text = glos.chargerPourcentN0(lbstation.Text);
            txtSoldAvant.Text = glos.getSoldeMat(lbmat.Text);
            lbmat.Text = glos.getSoldeMat(lbmat.Text);
            int qteCon = int.Parse(lbqte.Text);
            if (lbcARBU.Text == lbesse.Text)
            {
                 pource = int.Parse(lbesse.Text);
            }
            else if (lbcARBU.Text == lbmaz.Text)
            {
                 pource = int.Parse(lbesse.Text);
            }
            int montant = qteCon * pource;
            int montantAvant = int.Parse(txtSoldAvant.Text);

            txtsoldApres.Text =""+(montantAvant - montant);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
