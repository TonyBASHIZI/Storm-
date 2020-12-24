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
    public partial class formGerantBusiness : Form
    {
        glossaire glos = new glossaire();
        string users = "NULL";
        public formGerantBusiness(string user)
        {
            InitializeComponent();
            users = user;
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            panel1.BackColor = Color.Red;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            panel1.BackColor = Color.Transparent;
        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void formGerantBusiness_Load(object sender, EventArgs e)
        {
            lbuser.Text = "" + users;
            glos.chargerGridBusiness(gridControl1, "*", "consommation_business", lbuser.Text);
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                rapportbusinessGerant j = new rapportbusinessGerant();
                j.DataSource = StormSoft.classe.glossaire.GetInstance().sortieOperationBusDate(txtdate1.Text, users);
                ReportPrintTool printTool = new ReportPrintTool(j);
                printTool.ShowPreviewDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            panel3.BackColor = Color.Red;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            panel3.BackColor = Color.Transparent;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Remplissez Id du client ");
            }
            else
            {
                try
                {
                    rapportbusinessGerant j = new rapportbusinessGerant();
                    j.DataSource = StormSoft.classe.glossaire.GetInstance().sortieOperationBusID(textBox1.Text, users);
                    ReportPrintTool printTool = new ReportPrintTool(j);
                    printTool.ShowPreviewDialog();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            if (dateint2.Text == "" || dteInt1.Text == "")
            {
                MessageBox.Show("Remplissez l'intervalle de date svp et recommencer ");
            }
            else
            {
                MessageBox.Show("Service non disponible pour le moment !\n Contactez IT");
                //try
                //{
                //    rapportbusinessGerant j = new rapportbusinessGerant();
                //    j.DataSource = StormSoft.classe.glossaire.GetInstance().sortieConsommationIntervalFilterBusiness(dteInt1.Text,dateint2.Text, users);
                //    ReportPrintTool printTool = new ReportPrintTool(j);
                //    printTool.ShowPreviewDialog();

                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show(ex.Message);
                //}
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
