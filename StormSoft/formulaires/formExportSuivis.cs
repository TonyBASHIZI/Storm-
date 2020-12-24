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
    public partial class formExportSuivis : Form
    {
        glossaire glos = new glossaire();


        public formExportSuivis()
        {
            InitializeComponent();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton1_MouseEnter(object sender, EventArgs e)
        {
            panel4.BackColor = Color.Silver;
        }

        private void simpleButton1_MouseLeave(object sender, EventArgs e)
        {
            panel4.BackColor = Color.Transparent;
        }

        private void simpleButton2_MouseEnter(object sender, EventArgs e)
        {
            panel3.BackColor = Color.Silver;

        }

        private void simpleButton2_MouseLeave(object sender, EventArgs e)
        {
            panel3.BackColor = Color.Transparent;

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Voulez-vous vraiment  exporter Excel", "Fichier Excel", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                glossaire.GetInstance().ExportationExcel(dataNonActif);
            }
        }

        private void formExportSuivis_Load(object sender, EventArgs e)
        {
            glossaire.GetInstance().chargerPersonneNonActive(dataNonActif);
            glossaire.GetInstance().chargerPersonneSansCarte(dataPersSansCarte);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Voulez-vous vraiment  exporter Excel", "Fichier Excel", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                glossaire.GetInstance().ExportationExcel(dataNonActif);
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
