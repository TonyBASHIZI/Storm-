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
    public partial class formRecherche : Form
    {
        glossaire glos = new glossaire();
        public formRecherche()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            try
            {
                consommation  j = new consommation();
                 j.DataSource = StormSoft.classe.glossaire.GetInstance().sortieConsommationKey(txtmat.Text);
                ReportPrintTool printTool = new ReportPrintTool(j);
                printTool.ShowPreviewDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                consommation j = new consommation();
                j.DataSource = StormSoft.classe.glossaire.GetInstance().sortieConsommationDate(dateEdit1.Text);
                ReportPrintTool printTool = new ReportPrintTool(j);
                printTool.ShowPreviewDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            try
            {
                consommatioStation j = new consommatioStation();
                j.DataSource = StormSoft.classe.glossaire.GetInstance().sortieConsommationMat(comboBox1.Text);
                ReportPrintTool printTool = new ReportPrintTool(j);
                printTool.ShowPreviewDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void formRecherche_Load(object sender, EventArgs e)
        {
            //glos.getUser(comboBox1);
            //glos.getUser(comboBox2);
            //textBox1.Text = glos.SUMConso();
         // textBox2.Text = glos.SUMConsoStatio("Alpha");
           // labC.Text = glos.SUMConsoStatio("Charli");
            //labB.Text = glos.SUMConsoStatio("Beta");
            //labD.Text = glos.SUMConsoStatio("Delta");
            //glos.chargerConsoAvantage(dataAvantage);


        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            try
            {
                consommation j = new consommation();
                j.DataSource = StormSoft.classe.glossaire.GetInstance().sortieConsommationInterval(dateEdit3.Text,dateEdit4.Text);
                ReportPrintTool printTool = new ReportPrintTool(j);
                printTool.ShowPreviewDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            try
            {
                consommatioStation j = new consommatioStation();
                j.DataSource = StormSoft.classe.glossaire.GetInstance().sortieConsommationDateUser(comboBox2.Text, dateEdit2.Text);
                ReportPrintTool printTool = new ReportPrintTool(j);
                printTool.ShowPreviewDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void labtotal_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            try
            {
                Avantage j = new Avantage();
                j.DataSource = StormSoft.classe.glossaire.GetInstance().sortieAvantage();
                ReportPrintTool printTool = new ReportPrintTool(j);
                printTool.ShowPreviewDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Service non disponible pour l'instant");
           // glos.ExportationExcel(dataAvantage);
        }

        private void groupBox6_Enter(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            try
            {
                consommatioStation j = new consommatioStation();
                j.DataSource = StormSoft.classe.glossaire.GetInstance().sortieConsommationIntervalFilter(dateEdit6.Text, dateEdit5.Text, comboBox3.Text);
                ReportPrintTool printTool = new ReportPrintTool(j);
                printTool.ShowPreviewDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            loginAdmin cons = new loginAdmin();
            cons.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            formRapportStation st = new formRapportStation();
            st.ShowDialog();
        }

        private void pictureBox7_MouseEnter(object sender, EventArgs e)
        {
            panel2.BackColor = Color.Red;
        }

        private void pictureBox7_MouseLeave(object sender, EventArgs e)
        {
            panel2.BackColor = Color.Transparent;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void dataAvantage_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            glos.chargerConsoAvantage(dataAvantage);
            textBox1.Text = glos.SUMConso();
        }

        private void pictureBox5_MouseEnter(object sender, EventArgs e)
        {
            panel3.BackColor = Color.Green;
        }

        private void pictureBox5_MouseLeave(object sender, EventArgs e)
        {
            panel3.BackColor = Color.Transparent;
        }

        private void pictureBox4_MouseEnter(object sender, EventArgs e)
        {
            panel4.BackColor = Color.Green;
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            panel4.BackColor = Color.Transparent;
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            panel5.BackColor = Color.Green;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            panel5.BackColor = Color.Transparent;
        }
    }
}
