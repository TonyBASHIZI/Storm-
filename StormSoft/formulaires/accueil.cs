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
using MySql.Data.MySqlClient;
using DevExpress.Charts.Model;


namespace StormSoft.formulaires
{
    public partial class accueil : Form
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

                server = "remotemysql.com";
                database = "QGeXKPIGCu";
                uid = "QGeXKPIGCu";
                password = "hq2sgG5qxE";

                string co = "Server=" + server + ";UserId=" + uid + ";Port=" + port + ";Password=" + password + ";Database=" + database;
                con = new MySqlConnection(co);
                con.Open();


            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public accueil()
        {
            InitializeComponent();
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
                MessageBox.Show(ex.Message);
            }
            
        }

        public void chart()
        {

            try
            {
                InitialiseConnection();
                string q = "select count(*) as id from t_consommation ";
                
                cmd = new MySqlCommand(q, con); 
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    chart1.Series["Consommation"].Points.AddXY("" + dr.GetString("id"), dr.GetString("id"));
                    
                }
                


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            //chart1.Series["Quantite"].Points.AddXY("Tony", 600);
            //chart1.Series["Quantite"].Points.AddXY("Peter", 100);
            //chart1.Series["Quantite"].Points.AddXY("Jose", 400);
        }

        

       
       


        private void accueil_Load(object sender, EventArgs e)
        {
            //label1.Text = glos.countCl();
           //glos.chartcl(chartControl1);
            //label2.Text = glos.countConso();
            chartcl();
            chart();     
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void tileControl2_Click(object sender, EventArgs e)
        {

        }

        private void chart2_Click(object sender, EventArgs e)
        {

        }
       

       
    }
}
