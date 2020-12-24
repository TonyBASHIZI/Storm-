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
using System.Threading;


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
        glossaire glos = new glossaire();

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

                //string co = "Server=" + server + ";UserId=" + uid + ";Port=" + port + ";Password=" + password + ";Database=" + database;
                //con = new MySqlConnection(co);
                //con.Open();


            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public accueil()
        {
            InitializeComponent();
            //Thread t = new Thread(new ThreadStart(ThreadProc));
            //t.Start();
            //for (int i = 0; i < 4; i++)
            //{
            //    MessageBox.Show("Main thread: Do some work.");
            //    Thread.Sleep(0);
            //}
            //t.Join();
        }
        public  void ThreadProc()
        {
            for (int i = 0; i < 10; i++)
            {
                chartPersonNonActif();
                chartCartesPro();
                //chartClActif();
                chartPersonneAct();
                chartCartesActive();
                chartcl();
                chart();
                //chartUser();
                Thread.Sleep(0);
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
        public void chartPersonNonActif()
        {

            try
            {
                InitialiseConnection();
                string q = "SELECT COUNT(*) as per FROM `detail_carte` WHERE montant = 0";

                cmd = new MySqlCommand(q, con);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    chartCarte.Series["Cartes"].Points.AddXY("" + dr.GetString("per"), dr.GetString("per"));
                    //chartCarte.Series["Active"].Points.AddXY("" + dr.GetString("status"), dr.GetString("status"));
                }


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }
        public void chartCartesPro()
        {

            try
            {
                InitialiseConnection();
                string q = "select count(*) as id from carte where status = 'Production' ";

                cmd = new MySqlCommand(q, con);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    chartcarteActive.Series["Carte en Production"].Points.AddXY("" + dr.GetString("id"), dr.GetString("id"));
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public void chartPersonneAct()
        {

            try
            {
                InitialiseConnection();
                string q = "SELECT COUNT(*) as per FROM `detail_carte` WHERE montant > 0 ";

                cmd = new MySqlCommand(q, con);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    chartPersActive.Series["CLActifs"].Points.AddXY("" + dr.GetString("per"), dr.GetString("per"));
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public void chartCartesActive()
        {

            try
            {
                InitialiseConnection();
                string q = "select count(*) as id from carte where status = 'Active' ";

                cmd = new MySqlCommand(q, con);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    chartCarteAct.Series["Actives"].Points.AddXY("" + dr.GetString("id"), dr.GetString("id"));
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        //public void chartUser()
        //{

        //    try
        //    {
        //        InitialiseConnection();
        //        string q = "select count(qte) as id from t_consommation where qte >=10";

        //        cmd = new MySqlCommand(q, con);
        //        dr = cmd.ExecuteReader();
        //        while (dr.Read())
        //        {
        //            chart3.Series["Meilleurs_consommateurs"].Points.AddXY("" + dr.GetString("id"), dr.GetString("id"));
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        var result = MessageBox.Show("Probleme de connexion voulez-vous quitter l'application?", "Exit", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
        //        if (result == DialogResult.OK)
        //        {
        //            Application.Exit();
        //        }
        //        else
        //        {
        //            MessageBox.Show(ex.Message);
        //        }
        //    }

        //}
      

        public void chart()
        {

            try
            {
                InitialiseConnection();
                string q = "select SUM(qte) as QTE from t_consommation ";
                
                cmd = new MySqlCommand(q, con); 
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    chart1.Series["Consommation"].Points.AddXY("" + dr.GetString("QTE"), dr.GetString("QTE"));
                    
                }
                
            }
            catch (Exception ex)
            {
                var result = MessageBox.Show("Probleme de connexion voulez-vous quitter l'application?", "Exit", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    Application.Exit();
                }
                else
                {
                    MessageBox.Show(ex.Message);
                }
                
            }
            
          
        }

        

       
       


        private void accueil_Load(object sender, EventArgs e)
        {
            //Thread t = new Thread(new ThreadStart(ThreadProc));
            //t.Start();
           
            

            chartPersonNonActif();
            chartCartesPro();
            //chartClActif();
            chartPersonneAct();
            chartCartesActive();
            chartcl();
            chart();
            //chartUser();
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

        private void tileItem2_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            //this.Parent.Hide();
            //authentification us = new authentification();
            //us.ShowDialog();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void chartCarte_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //try
            //{
            //    InitialiseConnection();
            //    chartPersonNonActif();
            //    chartCartesPro();
            //    //chartClActif();
            //    chartPersonneAct();
            //    chartCartesActive();
            //    chartcl();
            //    chart();
            //    chartUser();


            //}catch(Exception ex)
            //{
            //    var result = MessageBox.Show("Probleme de connexion voulez-vous quitter l'application?", "Exit", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            //    if (result == DialogResult.OK)
            //    {
            //        Application.Exit();
            //    }
            //    else
            //    {
                   
            //    }
            //}
        }
       

       
    }
}
