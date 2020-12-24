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
    public partial class formRetrait : Form
    {
        glossaire glos = new glossaire();

        public formRetrait()
        {
            InitializeComponent();
        }

        private void formRetrait_Load(object sender, EventArgs e)
        {
            glossaire.GetInstance().GetRetraits(gridControl1, "*", "retraitsall");
        }

        private void searchControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Remplissez tous les champs svp!!");
            }
            else
            {
                //creation etat ici demain!
                retraitDate j = new retraitDate();
                j.DataSource = glos.sortieRetraitDates(textBox1.Text, textBox2.Text);
                ReportPrintTool printTool = new ReportPrintTool(j);
                printTool.ShowPreviewDialog();

            }
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            panel1.BackColor = Color.Red;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            panel1.BackColor = Color.Transparent;
        }
    }
}
