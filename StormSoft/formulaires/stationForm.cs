using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StormSoft.report;
using StormSoft.classe;
using DevExpress.XtraReports.UI;
using StormSoft.classe;
using MySql.Data.MySqlClient;

namespace StormSoft.formulaires
{
    public partial class stationForm : Form
    {
        MySqlConnection con = null;
        connexion conx = null;
        MySqlCommand cmd = null;
        MySqlDataAdapter dt = null;
        MySqlDataReader dr = null;
        private string server;
        private string database;
        private string uid;
        private string password;

        private string port;
   

        public void InitialiseConnection()
        {
            try
            {
                server = "192.162.69.136";
                database = "c1db_mangtech";
                uid = "c1user";
                password = "dc2MNReeaVY@";
                port = "3306";
                string co = "Server=" + server + ";UserId=" + uid + ";Port=" + port + ";Password=" + password + ";Database=" + database;
                con = new MySqlConnection(co);
                con.Open();

                //server = "remotemysql.com";
                //database = "QGeXKPIGCu";
                //uid = "QGeXKPIGCu";
                //password = "hq2sgG5qxE";

                //string co = "Data Source=localhost;Initial Catalog=c1db_mangtech; User Id=root; Password=root";
                //con = new MySqlConnection(co);
                //con.Open();


            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        glossaire glos = new glossaire();
        string user;
        public stationForm(string mot)
        {
            InitializeComponent();

            user = mot;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                consommation j = new consommation();
                j.DataSource = StormSoft.classe.glossaire.GetInstance().sortieConsoFilter(textBox1.Text, user);
                ReportPrintTool printTool = new ReportPrintTool(j);
                printTool.ShowPreviewDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            try
            {
                consommation j = new consommation();
                j.DataSource = StormSoft.classe.glossaire.GetInstance().sortieConsommationKeyFilter(txtmat.Text, user);
                ReportPrintTool printTool = new ReportPrintTool(j);
                printTool.ShowPreviewDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void chartcl()
        {

            try
            {
                InitialiseConnection();
                string q = "select count(*) as id from t_client ";

                cmd = new MySqlCommand(q, con);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    chart2.Series["Clients"].Points.AddXY("" + dr.GetString("id"), dr.GetString("id"));
                }


            }
            catch (Exception ex)
            {

                MessageBox.Show("Erreur dans le chargement des données !  ERREUR DE CONNEXION");

            }

        }

        private void stationForm_Load(object sender, EventArgs e)
        {  
            toStation.Text = user;
            chartcl();
            glos.chargerGridStation(gridControl1, " * ", "t_consommation", user);
           
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            try
            {
                consommation j = new consommation();
                j.DataSource = StormSoft.classe.glossaire.GetInstance().sortieConsommationIntervalFilter(textBox2.Text, textBox3.Text, user);
                ReportPrintTool printTool = new ReportPrintTool(j);
                printTool.ShowPreviewDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void separatorControl2_Click(object sender, EventArgs e)
        {

        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void dateEdit2_EditValueChanged(object sender, EventArgs e)
        {
            //total.Text = glos.SUMConsoStatio(user, dateEdit2.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            total.Text = glos.SUMConsoStatio(user, textBox4.Text,user);
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            panel4.BackColor = Color.Green;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            panel4.BackColor = Color.Transparent;
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            panel5.BackColor = Color.Green;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            panel5.BackColor = Color.Transparent;
        }

        private void pictureBox5_MouseEnter(object sender, EventArgs e)
        {
            panel6.BackColor = Color.Green;
        }

        private void pictureBox5_MouseLeave(object sender, EventArgs e)
        {
            panel6.BackColor = Color.Transparent;
        }
    }
}
