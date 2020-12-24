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
    public partial class partennaireForm : Form
    {
        glossaire glos = new glossaire();
        ClassPartennaire parte = new ClassPartennaire();

        public partennaireForm()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            parte.Code = txtCode.Text;
            parte.Nom = txtnom.Text;
            parte.Postnom = txtpost.Text;
            parte.Prenom = txtprenom.Text;
            parte.Station = comboStat.Text;
            parte.Telephone = txttel.Text;
            parte.Adresse = txtAdresse.Text;

            glos.insertPartennaire(parte);
            initialise();
            getcode();
         
        }
        public void initialise()
        {
            txtnom.Text = "";
            txtpost.Text = "";
            txtprenom.Text = "";
            txttel.Text = "";
            txtAdresse.Text = "";
            comboStat.Text = "";

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            glos.deletePart(txtCode.Text);
            getcode();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txttel_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboStat_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public void getcode()
        {
            Random rd = new Random();
            int toto = rd.Next(1, 50);

            txtCode.Text = "Parte-" + toto;

        }
        private void partennaireForm_Load(object sender, EventArgs e)
        {
            getcode();
            glos.getUser(comboStat);

        }

        private void panel3_MouseEnter(object sender, EventArgs e)
        {
            panel3.BackColor = Color.Red;
        }

        private void panel3_MouseLeave(object sender, EventArgs e)
        {
            panel3.BackColor = Color.Transparent;
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            try
            {
                partennaire j = new partennaire();
                j.DataSource = StormSoft.classe.glossaire.GetInstance().sortiePartennaire();
                ReportPrintTool printTool = new ReportPrintTool(j);
                printTool.ShowPreviewDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            panel3.BackColor = Color.Red;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            panel3.BackColor = Color.Transparent;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
