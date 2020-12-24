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
using StormSoft.report;
using DevExpress.XtraReports.UI;

namespace StormSoft.formulaires
{
    public partial class formRapportStation : Form
    {
        glossaire glos = new glossaire();
        public formRapportStation()
        {
            InitializeComponent();
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            panel1.BackColor = Color.Red;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            panel1.BackColor = Color.Transparent;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (txtStation.Text == "" || txtDate.Text == "" || cmbType.Text == "")
            {
                MessageBox.Show("Remplissez tous les champs svp!!");
            }
            else
            {
                //creation etat ici demain!
                rapportFiltreType j = new rapportFiltreType();
                j.DataSource = glos.sortieConsoStationType(txtDate.Text, cmbType.Text, txtStation.Text);
                ReportPrintTool printTool = new ReportPrintTool(j);
                printTool.ShowPreviewDialog();
               
            }
            
        }

        private void formRapportStation_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (station.Text == "" || date1.Text == "" || date2.Text == "" || type.Text=="")
            {
                MessageBox.Show("Remplissez tous les champs svp!!");
            }
            else
            {
                //creation etat ici demain!
                rapportFiltreType j = new rapportFiltreType();
                j.DataSource = glos.sortieConsoStationTypeInterval(date1.Text, date2.Text, type.Text, station.Text);
                ReportPrintTool printTool = new ReportPrintTool(j);
                printTool.ShowPreviewDialog();

            }
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            panel2.BackColor = Color.Red;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            panel2.BackColor = Color.Transparent;
        }
    }
}
