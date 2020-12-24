using DevExpress.XtraReports.UI;
using StormSoft.report;
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
    public partial class formBusiness : Form
    {
        glossaire glos = new glossaire();
        public formBusiness()
        {
            InitializeComponent();
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            panel5.BackColor = Color.Red;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            panel5.BackColor = Color.Transparent;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (txtdate1.Text == "")
            {
                MessageBox.Show("Replissez bien la date!!");
            }
            else
            {
                try
                {
                    rapportBusiness j = new rapportBusiness();
                    j.DataSource = StormSoft.classe.glossaire.GetInstance().sortieOperationDateBus(txtdate1.Text);
                    ReportPrintTool printTool = new ReportPrintTool(j);
                    printTool.ShowPreviewDialog();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (txtdate2.Text == "" || cb1.Text == "")
            {
                MessageBox.Show("Replissez bien les deux champs!!");
            }
            else
            {
                try
                {
                    rapportBusiness j = new rapportBusiness();
                    j.DataSource = StormSoft.classe.glossaire.GetInstance().sortieOperationBusDate(txtdate2.Text, cb1.Text);
                    ReportPrintTool printTool = new ReportPrintTool(j);
                    printTool.ShowPreviewDialog();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
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

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            

            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtcode.Text == "")
            {
                MessageBox.Show("Remplir le code du compte business");

            }
            else
            {
                lbsolde.Text = "" + glos.soldeBusiness(txtcode.Text);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtcodeBus.Text == "")
            {
                MessageBox.Show("Remplissez tous les champs");
            }
            else
            {
                glos.chargerGridBusiness(gridControl1, "*", "consommation_business", txtcodeBus.Text);
            }
            
        }

        private void groupBox7_Enter(object sender, EventArgs e)
        {

        }

        private void formBusiness_Load(object sender, EventArgs e)
        {

        }
    }
}
