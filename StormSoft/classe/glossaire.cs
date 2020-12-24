﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using StormSoft.classe;
using System.Reflection;
using DevExpress.XtraCharts;
using System.Data.SqlClient;
using DevExpress.Charts.Model;
using System.Speech;
using System.Speech.Synthesis;
using DevExpress.XtraGrid;

namespace StormSoft.classe
{

    class glossaire
    {
        MySqlConnection con = null;
        connexion conx = null;
        MySqlCommand cmd = null;
        MySqlDataAdapter dt = null;
        MySqlDataReader dr = null;
        MySqlDataAdapter adpr = null;
        DataSet dste;
        private string server;
        private string database;
        private string uid;
        private string password;

        private string port;

        private static glossaire glos = null;


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

                //string co = "Data Source=localhost;Initial Catalog=c1db_mangtech; User Id=root; Password=root;";
                //con = new MySqlConnection(co);
                //con.Open();
               
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show("Erreur lors de la connexion avec la base \n des données! verifier la connexion internet \n si le message continue alors contacter IT");
                
            }

        }
       
        public void chargerGrid(GridControl grid, string field, string table)
        {
            InitialiseConnection();

            try
            {
                using (cmd = con.CreateCommand())
                {
                    cmd.CommandText = " SELECT " + field + " FROM " + table + "";
                    dt = new MySqlDataAdapter((MySqlCommand)cmd);
                    DataSet ds = new DataSet();
                    dt.Fill(ds);
                    grid.DataSource = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }
        public void chargerGridStation(GridControl grid, string field, string table, string station)
        {
            InitialiseConnection();

            try
            {
               
                using (cmd = con.CreateCommand())
                {
                    //cmd.CommandText = " SELECT " + field + " FROM " + table + " where username = '"+ station +"' GROUP BY id DESC limit 1500 ";
                    cmd.CommandText = " SELECT " + field + " FROM " + table + " WHERE username ='"+station+"' GROUP BY id DESC limit 1500 ;";
                    dt = new MySqlDataAdapter((MySqlCommand)cmd);
                    DataSet ds = new DataSet();
                    dt.Fill(ds);
                    grid.DataSource = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }
        public void chargerGridBusiness(GridControl grid, string field, string table, string bus)
        {
            InitialiseConnection();

            try
            {

                using (cmd = con.CreateCommand())
                {
                    //cmd.CommandText = " SELECT " + field + " FROM " + table + " where username = '"+ station +"' GROUP BY id DESC limit 1500 ";
                    cmd.CommandText = " SELECT " + field + " FROM " + table + " WHERE ref_business ='" + bus + "' GROUP BY id DESC limit 2500 ;";
                    dt = new MySqlDataAdapter((MySqlCommand)cmd);
                    DataSet ds = new DataSet();
                    dt.Fill(ds);
                    grid.DataSource = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }
        public static glossaire GetInstance()
        {
            if (glos == null)
                glos = new glossaire();

            return glos;
        }
        public DataTable LoadGrid(string tableName)
        {
            InitialiseConnection();

           // if (!con.State.ToString().ToLower().Equals("open")) con.Open();
            DataTable table = new DataTable();
            dt = new MySqlDataAdapter("select * from " + tableName + "", con);
            dt.Fill(table);
            con.Close();

            return table;
        }
        //public void chargerDatacl(DataGridView data){
            
        //    try
        //    {
        //        InitialiseConnection();
        //        string q = "select matr_client,nom,postnom,prenom,tel,age,adresse,reseaux,affiliation from t_client ORDER BY id DESC limit 50";
        //        cmd = new MySqlCommand(q, con);
        //        dt = new MySqlDataAdapter(cmd);
        //        DataTable table = new DataTable();
        //        data.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        //        data.RowTemplate.Height = 50;
        //        data.AllowUserToAddRows = false;

        //        dt.Fill(table);
        //        data.DataSource = table;

        //       // DataGridViewImageColumn imgcolum = new DataGridViewImageColumn();
        //        //imgcolum = (DataGridViewImageColumn)data.Columns[8];
        //        //imgcolum.ImageLayout = DataGridViewImageCellLayout.Stretch;
        //        dt.Dispose();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }

        //}
        public void getEnregistreent(DataGridView data)
        {

            try
            {
                InitialiseConnection();
                string q = "select * from Enregistrement where etat=1  ORDER BY id DESC ";
                cmd = new MySqlCommand(q, con);
                dt = new MySqlDataAdapter(cmd);
                DataTable table = new DataTable();
                data.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                data.RowTemplate.Height = 50;
                data.AllowUserToAddRows = false;

                dt.Fill(table);
                data.DataSource = table;

                // DataGridViewImageColumn imgcolum = new DataGridViewImageColumn();
                //imgcolum = (DataGridViewImageColumn)data.Columns[8];
                //imgcolum.ImageLayout = DataGridViewImageCellLayout.Stretch;
                dt.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public void deleteClient(ClassClient client)
        {
            try
            {


                InitialiseConnection();
                //if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                string qrdelete = "delete from t_client where matr_client=@matr_client";
                cmd = new MySqlCommand(qrdelete, con);
                cmd.Parameters.Add(new MySqlParameter("@matr_client", client.Mat));

                DialogResult result = MessageBox.Show("Voulez-vous vraiment supprimer cette personne ?", "Suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Suppression avec succes!!");
                        ClassCarte c = new ClassCarte();
                        c.Idcarte = client.Id;
                        c.Statut = "Active";
                        glos.updateStatut(c);
                        glos.deleteCompte(client.Id);


                    }
                }

                else
                {
                    MessageBox.Show("Opération Annulée !");
                }

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {

                MessageBox.Show(ex.Message);

            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }

        }
        public void deleteEnregistrement(ClassEnregistrement client, string code)
        {
            try
            {


                InitialiseConnection();
                //if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                string qrdelete = "delete from Enregistrement where id = @id ";
                cmd = new MySqlCommand(qrdelete, con);
                cmd.Parameters.Add(new MySqlParameter("@id", client.Ib));

                DialogResult result = MessageBox.Show("Voulez-vous vraiment supprimer cette personne ?", "Suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Suppression avec succes!!");

                    }
                }

                else
                {
                    MessageBox.Show("Opération Annulée !");
                }

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {

                MessageBox.Show(ex.Message);

            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }

        }

        /// Attribue les paramatres dans les requêtes de la base de données
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="length"></param>
        /// <param name="paramValue"></param>

        private static void SetParameter(MySqlCommand cmd, string name, DbType type, int length, object paramValue)
        {

            IDbDataParameter a = cmd.CreateParameter();
            a.ParameterName = name;
            a.Size = length;
            a.DbType = type;

            if (paramValue == null)
            {
                if (!a.IsNullable)
                {
                    a.DbType = DbType.String;
                }
                a.Value = DBNull.Value;
            }
            else
                a.Value = paramValue;

            cmd.Parameters.Add(a);
        }
        public void chargerclient(DataGridView data)
        {
            try
            {
                InitialiseConnection();
              
                string req = "select matr_client,nom,postnom,prenom,tel,age,adresse,reseaux,affiliation from t_client";
                cmd = new MySqlCommand(req, con);

                dr = cmd.ExecuteReader();
                data.Rows.Clear();

                while (dr.Read())
                {
                    data.Rows.Add(dr["matr_client"].ToString(), dr["nom"].ToString(), dr["postnom"].ToString(), dr["prenom"].ToString(), dr["tel"].ToString(), dr["age"].ToString(), dr["adresse"].ToString(), dr["reseaux"].ToString(), dr["affiliation"].ToString());

                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }
            

        }
        public void  chargerPaie1(DataGridView data)
        {
            try
            {
                InitialiseConnection();

                string req = "SELECT `matr_client`, `nom`, `postnom`,t_client.`tel`,SUM(pourcentage) As Total FROM `t_client`,`t_bonus` WHERE `matr_client` = t_bonus.`ref_client` and etats = 0 GROUP by `matr_client`";
                cmd = new MySqlCommand(req, con);

                dr = cmd.ExecuteReader();
                data.Rows.Clear();

                while (dr.Read())
                {
                    data.Rows.Add(dr["matr_client"].ToString(), dr["nom"].ToString(), dr["postnom"].ToString(), dr["tel"].ToString(),dr["total"].ToString());

                }

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }
            

        }
        public void chargerPaie2(DataGridView data)
        {
            try
            {
                InitialiseConnection();

                string req = "SELECT `matr_client`, `nom`, `postnom`,t_client.`tel`,SUM(pourcentage0) As Total FROM `t_client`,`t_bonus` WHERE `matr_client` = t_bonus.`reseaux0` and etats = 0  GROUP by `matr_client`";
                cmd = new MySqlCommand(req, con);

                dr = cmd.ExecuteReader();
                data.Rows.Clear();

                while (dr.Read())
                {
                    data.Rows.Add(dr["matr_client"].ToString(), dr["nom"].ToString(), dr["postnom"].ToString(), dr["tel"].ToString(), dr["total"].ToString());

                }

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }


        }
        public void insertPartennaire(ClassPartennaire par)
        {
            try {
                InitialiseConnection();
                string q = "insert into partennaire(nom,postnom,prenom,adresse,tel,station) values(@nom,@postnom,@prenom,@adresse,@tel,@station)";
                cmd = new MySqlCommand(q, con);
                cmd.Parameters.Add(new MySqlParameter("@nom", par.Nom));
                cmd.Parameters.Add(new MySqlParameter("@postnom", par.Postnom));
                cmd.Parameters.Add(new MySqlParameter("@prenom", par.Prenom));
                cmd.Parameters.Add(new MySqlParameter("@adresse", par.Adresse));
                cmd.Parameters.Add(new MySqlParameter("@tel", par.Telephone));
                cmd.Parameters.Add(new MySqlParameter("@station", par.Station));

                if (cmd.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Insertion avec succes");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }

        }
        public void insertMessagerie(ClsMessagerieInsert cb)
        {

            try
            {
                InitialiseConnection();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                string q = "insert into tMessagerie(NumeroTutaire,CorpsMessage,EtatSms,Utilisateur) values (@NumeroTutaire,@CorpsMessage,@DateEnvoie,@EtatSms,@Utilisateur)";
                //cmd = new MySqlCommand("insert into tMessagerie values (@NumeroTutaire,@CorpsMessage,@DateEnvoie,@EtatSms,@Utilisateur)", con);
                cmd = new MySqlCommand(q, con);
                cmd.Parameters.Add(new MySqlParameter("NumeroTutaire", cb.Numero1));
                cmd.Parameters.Add(new MySqlParameter("CorpsMessage", cb.MessateTexte1));
                cmd.Parameters.Add(new MySqlParameter("DateEnvoie", cb.DateEvoie1));
                cmd.Parameters.Add(new MySqlParameter("EtatSms", cb.EtatSms1));
                cmd.Parameters.Add(new MySqlParameter("Utilisateur", cb.Utilisateur1));

                cmd.ExecuteNonQuery();
                con.Close();
                //MessageBox.Show("Envoie du message pris en charge par le serveur !!!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public void insertConsommatio(ClassConsommationImport cons)
        {

            try
            {
               
                InitialiseConnection();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                string q = "insert into t_consommation(ref_client,type_cons,qte,prix,username) values (@ref_client,@type_cons,@qte,@prix,@username)";
                //cmd = new MySqlCommand("insert into tMessagerie values (@NumeroTutaire,@CorpsMessage,@DateEnvoie,@EtatSms,@Utilisateur)", con);
                cmd = new MySqlCommand(q, con);
                cmd.Parameters.Add(new MySqlParameter("ref_client", cons.Mat));
                cmd.Parameters.Add(new MySqlParameter("type_cons", cons.Type));
                cmd.Parameters.Add(new MySqlParameter("qte", cons.Prix));
                cmd.Parameters.Add(new MySqlParameter("prix", cons.Qte));
                cmd.Parameters.Add(new MySqlParameter("username", cons.User));

                cmd.ExecuteNonQuery();
                MessageBox.Show("Insertion avec succes de la consommation !!!");
                con.Close();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public void deletePart(string key)
        {
            try
            {
                InitialiseConnection();
                string q = "delete form partennaire where code = '"+key+"'";
                cmd = new MySqlCommand(q, con);
                
                if (cmd.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Suppression avec succes");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }

        }
        public void ExportationExcel(DataGridView Grid) {
            try
            {
                Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
                Microsoft.Office.Interop.Excel._Worksheet worksheet = null;

                worksheet = workbook.Sheets["Sheet1"];
                worksheet = workbook.ActiveSheet;
                worksheet.Name = "MANGEXCEL";

                for (int i = 1; i < Grid.Columns.Count + 1; i++)
                {
                    worksheet.Cells[1, i] = Grid.Columns[i - 1].HeaderText;
                }

                for (int i = 0; i < Grid.Rows.Count; i++)
                {
                    for (int j = 0; j < Grid.Columns.Count; j++)
                    {
                        worksheet.Cells[i + 2, j + 1] = Grid.Rows[i].Cells[j].Value.ToString();
                    }
                }
                var saveFileDialoge = new SaveFileDialog();
                saveFileDialoge.FileName = "output";
                saveFileDialoge.DefaultExt = ".xlsx";
                if (saveFileDialoge.ShowDialog() == DialogResult.OK)
                {
                    workbook.SaveAs(saveFileDialoge.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                }
                app.Quit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



        }
        


        //public void seachcl(string champ, DataGridView data)
        //{
        //    try
        //    {
        //        InitialiseConnection();
        //        string q = "select matr_client,nom,postnom,prenom,tel,age,adresse,reseaux,affiliation,photo from t_client where matr_client = '" + champ + "' or nom = '" + champ + "' or postnom = '" + champ + "'";
        //        cmd = new MySqlCommand(q, con);
        //        dt = new MySqlDataAdapter(cmd);
        //        DataTable table = new DataTable();
        //        data.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        //        data.RowTemplate.Height = 120;
        //        data.AllowUserToAddRows = false;

        //        dt.Fill(table);
        //        data.DataSource = table;

        //        DataGridViewImageColumn imgcolum = new DataGridViewImageColumn();
        //        imgcolum = (DataGridViewImageColumn)data.Columns[9];
        //        imgcolum.ImageLayout = DataGridViewImageCellLayout.Stretch;
        //        dt.Dispose();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }

        //}
        public string getBalance()
        {
            string bal = "0";
            try
            {
                InitialiseConnection();

                string req = "select SUM(montant) as total from detail_carte ";
                cmd = new MySqlCommand(req, con);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                  bal = dr["total"].ToString();

                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }

            return bal;
        }
        public void chargerArbre(DataGridView data)
        {
            try
            {
                InitialiseConnection();
               

                string req = "select * from t_arbre ";
                cmd = new MySqlCommand(req, con);
                data.Rows.Clear();
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    data.Rows.Add(dr["ref_client"].ToString(), dr["designreseaux"].ToString());

                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
           

        }

        public void seachArbre(DataGridView data, string champ)
        {

            InitialiseConnection();


            string req = "select * from t_arbre where ref_client = '" + champ + "' ";
            cmd = new MySqlCommand(req, con);
            data.Rows.Clear();
            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                data.Rows.Add(dr["ref_client"].ToString(), dr["designreseaux"].ToString());

            }
            else
            {
                data.Rows.Clear();
                while (dr.Read())
                {
                    data.Rows.Add(dr["ref_client"].ToString(), dr["designreseaux"].ToString());
                }
            }

        }
        public void chargerConso(DataGridView data, string date_leo)
        {
            try
            {
                InitialiseConnection();

                string q = "select *  from t_consommation where Date_Format(date_cons,'%d/%m/%Y') = '" + date_leo + "' and etat = 1 ";
                cmd = new MySqlCommand(q, con);
                data.Rows.Clear();
                dr = cmd.ExecuteReader();
                
                while (dr.Read())
                {
                    data.Rows.Add(dr["ref_client"].ToString(), dr["type_cons"].ToString(), dr["qte"].ToString(), dr["prix"].ToString(), dr["date_cons"].ToString());

                }


            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                con.Close();

            }
        }
        public void chargerConsoStationDate(DataGridView data, string station, string date_leo)
        {
            try
            {
                InitialiseConnection();

                string q = "select *  from t_consommation where Date_Format(date_cons,'%d/%m/%Y') = '" + date_leo + "' and username = '"+station+"' ";
                cmd = new MySqlCommand(q, con);
                data.Rows.Clear();
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    data.Rows.Add(dr["ref_client"].ToString(), dr["type_cons"].ToString(), dr["qte"].ToString(), dr["prix"].ToString(), dr["date_cons"].ToString());

                }


            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                con.Close();

            }
        }

        public void chargerPersonneNonActive(DataGridView data)
        {
            try
            {
                InitialiseConnection();

                string q = "SELECT matricule,nom,tel,id_carte,montant FROM `detail_carte`,`t_client` WHERE matricule = matr_client and montant = 0";
                cmd = new MySqlCommand(q, con);
                data.Rows.Clear();
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    data.Rows.Add(dr["matricule"].ToString(), dr["nom"].ToString(), dr["tel"].ToString(), dr["id_carte"].ToString(), dr["montant"].ToString());

                }


            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                con.Close();

            }
        }
        public void chargerPersonneSansCarte(DataGridView data)
        {
            try
            {
                InitialiseConnection();

                string q = "SELECT matr_client,nom,tel,id_carte FROM `t_client` WHERE id_carte IS NULL";
                cmd = new MySqlCommand(q, con);
                data.Rows.Clear();
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    data.Rows.Add(dr["matr_client"].ToString(), dr["nom"].ToString(), dr["tel"].ToString(), dr["id_carte"].ToString());

                }


            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                con.Close();

            }
        }
        public void chargerConsoStationDateInterval(DataGridView data, string station, string date1, string date2)
        {
            try
            {
                InitialiseConnection();

                string q = "select *  from t_consommation where username = '" + station + "' and Date_Format(date_cons,'%d/%m/%Y') between '" + date1 + "' and  '" + date2 + "' ";
                cmd = new MySqlCommand(q, con);
                data.Rows.Clear();
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    data.Rows.Add(dr["ref_client"].ToString(), dr["type_cons"].ToString(), dr["qte"].ToString(), dr["prix"].ToString(), dr["date_cons"].ToString());

                }


            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                con.Close();

            }
        }
        public void chargerConsoAvantage(DataGridView data)
        {
            try
            {
                InitialiseConnection();

                string q = " SELECT ref_client,nom,tel,qte FROM `t_consommation`,`t_client` WHERE qte >= 10 and ref_client = matr_client GROUP BY ref_client ";
                cmd = new MySqlCommand(q, con);
                data.Rows.Clear();
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    data.Rows.Add(dr["ref_client"].ToString(), dr["nom"].ToString(), dr["tel"].ToString(), dr["qte"].ToString());

                }


            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                con.Close();

            }
        }

        public void speakeer(string mot)
        {
            SpeechSynthesizer ob = new SpeechSynthesizer();
            ob.Speak(mot);
 
        }
        //public void chargerMat(ComboBox data)
        //{
        //    try
        //    {
        //        InitialiseConnection();
              
        //        string req = "select ref_client from t_consommation ";
        //        cmd = new MySqlCommand(req, con);

        //        dr = cmd.ExecuteReader();

        //        while (dr.Read())
        //        {
        //            data.Items.Add(dr["ref_client"].ToString());

        //        }
        //    }
        //    catch (MySql.Data.MySqlClient.MySqlException ex)
        //    {

        //    }
           
            
        //}
       
        public void chargerMatAr(ComboBox data)
        {
            try
            {
                InitialiseConnection();

                string req = "select matr_client from  t_client";
                cmd = new MySqlCommand(req, con);

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    data.Items.Add(dr["matr_client"].ToString());

                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
        public void IDusername(ComboBox combo)
        {
            try
            {
                InitialiseConnection();
                string q = "select username from retrofit_users";
                cmd = new MySqlCommand(q, con);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    combo.Items.Add(dr["username"].ToString());

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void getUser(ComboBox combo)
        {
            try
            {
                InitialiseConnection();
                string q = "select username from t_consommation";
                cmd = new MySqlCommand(q, con);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    combo.Items.Add(dr["username"].ToString());

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public int countARB(string data)
        {
            int x = 0;
            InitialiseConnection();
          
            string req = "select count(*) as nb from t_client where reseaux ='"+data+"'";
            cmd = new MySqlCommand(req, con);

            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                
                x += int.Parse(dr.GetString("nb"));


            }
            
            return x;
        }
        public void chargerDesignRes(ComboBox data)
        {
            //try
            //{
            //    InitialiseConnection();
            //    //if (!con.State.ToString().ToLower().Equals("open"))
            //    //{
            //    //    con.Open();

            //    //}


            //    string req = "select ref_client from t_arbre ";
            //    cmd = new MySqlCommand(req, con);

            //    dr = cmd.ExecuteReader();

            //    while (dr.Read())
            //    {
            //        data.Items.Add(dr["ref_client"].ToString());



            //    }


            //}
            //catch (MySql.Data.MySqlClient.MySqlException ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //finally
            //{
            //    cmd.Dispose();
            //    con.Close();
            //}

        }
        public string chargerPrixE()
        {
            InitialiseConnection();
            string prixE = "0";
            try
            {

                string q = "select prix from price where typeconso = 'Essence' ";
                cmd = new MySqlCommand(q,con);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    prixE = dr.GetString("prix");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }

            return prixE;
        }
        public string chargerPrixPetro()
        {
            InitialiseConnection();
            string prixP = "0";
            try
            {

                string q = "select prix from price where typeconso = 'Petrole' ";
                cmd = new MySqlCommand(q, con);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    prixP = dr.GetString("prix");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }

            return prixP;
        }
        public string chargerPrixDie()
        {
            InitialiseConnection();
            string prixD = "0";
            try
            {

                string q = "select prix from price where typeconso = 'Diesel' ";
                cmd = new MySqlCommand(q, con);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    prixD = dr.GetString("prix");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }

            return prixD;
        }
        public string chargerID(string cate)
        {
            InitialiseConnection();
            string prixD = "0";
            try
            {

                string q = "select id from pourcentage where categorie = '"+cate+"' ";
                cmd = new MySqlCommand(q, con);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    prixD = dr.GetString("id");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }

            return prixD;
        }
      
        
        public String findReseax(string code)
        {
            string res = "";
            try
            {
                InitialiseConnection();
               
                string q = "select reseaux from t_client where matr_client = '" + code + "'";
                cmd = new MySqlCommand(q, con);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {

                    res = dr.GetString("reseaux").ToString();
                }
                else
                {
                    res = "Null";
                }

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }

            return res;
        }

        public String findNumber(string num)
        {
            string n = "";
            try
            {
                InitialiseConnection();
                

                string q = "select tel from t_client where matr_client = '" + num + "'";
                cmd = new MySqlCommand(q, con);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    n = dr.GetString("tel");
                    return n;
                }
                else
                {
                    
                    return "";
                }

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }

            return n;
        }
        public string chargerPourcentN0(string cate)
        {
            InitialiseConnection();
            string n1 = "";
            try
            {
                string q = "select niv2 from pourcentage where categorie = '"+cate+"'";
                cmd = new MySqlCommand(q, con);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    n1 = dr.GetString("niv2");

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }
            return n1; 
        }
        public string getSoldeMat(string mat)
        {
            InitialiseConnection();
            string n1 = "";
            try
            {
                string q = "select montant from detail_carte where matricule = '" + mat + "'";
                cmd = new MySqlCommand(q, con);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    n1 = dr.GetString("montant");

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }
            return n1;
        }
        public string chargerPourcentN1(string cate)
        {
            InitialiseConnection();
            string n = "";
            try
            {
                string q = "select niv1 from pourcentage where categorie = '"+cate+"'";
                cmd = new MySqlCommand(q, con);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    n = dr.GetString("niv1");

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }
            return n;
        }
        public string soldeBusiness(string mat)
        {
            InitialiseConnection();
            string n = "";
            try
            {
                string q = "select solde from agent_business where  ref_matricule = '" + mat + "'";
                cmd = new MySqlCommand(q, con);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    n = dr.GetString("solde");

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }
            return n;
        }
        public void GetDatas(GridControl grid, string field, string table)
        {
            InitialiseConnection();

            try
            {
                using (cmd = con.CreateCommand())
                {
                    cmd.CommandText = " SELECT " + field + " FROM " + table + " ORDER BY id DESC limit 2000 ";
                    dt = new MySqlDataAdapter((MySqlCommand)cmd);
                    DataSet ds = new DataSet();
                    dt.Fill(ds);
                    grid.DataSource = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }
        public void GetRetraits(GridControl grid, string field, string table)
        {
            InitialiseConnection();

            try
            {
                using (cmd = con.CreateCommand())
                {
                    cmd.CommandText = " SELECT " + field + " FROM " + table + " ORDER BY date DESC limit 1000 ";
                    dt = new MySqlDataAdapter((MySqlCommand)cmd);
                    DataSet ds = new DataSet();
                    dt.Fill(ds);
                    grid.DataSource = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }
        public void GetDatasNext(GridControl grid, string field, string table, int n)
        {
            InitialiseConnection();

            try
            {
                using (cmd = con.CreateCommand())
                {
                    cmd.CommandText = " SELECT " + field + " FROM " + table + " where id > " + n + " ORDER BY id DESC  limit "+(n+1000)+"";
                    dt = new MySqlDataAdapter((MySqlCommand)cmd);
                    DataSet ds = new DataSet();
                    dt.Fill(ds);
                    grid.DataSource = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }
        public void GetDatasEnregis(GridControl grid, string field, string table)
        {
            InitialiseConnection();

            try
            {
                using (cmd = con.CreateCommand())
                {
                    cmd.CommandText = " SELECT " + field + " FROM " + table + " where statut = 1 ORDER BY id DESC ";
                    dt = new MySqlDataAdapter((MySqlCommand)cmd);
                    DataSet ds = new DataSet();
                    dt.Fill(ds);
                    grid.DataSource = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }
        public bool Deleteconso(ClassConsommation cons)
        {
            bool bol = false;
            try
            {
                InitialiseConnection();
                string q = "delete from t_consommation where id=@id";
                cmd = new MySqlCommand(q, con);
                cmd.Parameters.Add(new MySqlParameter("@id", cons.Id));
                DialogResult result = MessageBox.Show("Voulez-vous vraiment  cette operation ?", "suprimession", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        cmd.Dispose();
                        con.Close();
                        MessageBox.Show("Suppression consommation effectuee avec succes!!");
                        bol = true;
                        UpdateMontant(cons.Mat, cons.Qte, cons.Typecons,cons.Stat);
                       
                    }
                }

                else
                {
                    MessageBox.Show("Opération Annulée !");
                    bol = false;
                }


            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }

            return bol;
        }
        public string MontantCarte(string num)
        {
            string n = "";
            try
            {
                InitialiseConnection();
                string q = "select montant from detail_carte  where matricule = '" + num + "'";
                cmd = new MySqlCommand(q, con);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    n = dr.GetString("montant");
                    return n;
                }
                else
                {
                    MessageBox.Show("Pas de compte a ce matricule!!");
                    return "";
                }

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }

            return n;
        }
        public void UpdateMontant(string mat,string qte,string t,string stat)
        {
            string montant = MontantCarte(mat);
            int qnew = int.Parse(qte) * getNiveau(t,stat);
            int m = int.Parse(montant) - qnew;
            MessageBox.Show(""+mat+" "+montant+" " +qnew+" "+stat);
            try
            {
                InitialiseConnection();
                string q = "update detail_carte set montant=@montant where matricule=@matricule";
                cmd = new MySqlCommand(q, con);

                cmd.Parameters.Add(new MySqlParameter("@montant", m));
                cmd.Parameters.Add(new MySqlParameter("@matricule", mat));

                MessageBox.Show("Mis a jour du compte effectue avec success");

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }


        }
        public int getNiveau(string type, string station)
        {
            int n = 0;
            try
            {
                InitialiseConnection();
                string q = "select niv1 from pourcentage  where categorie = '" + type + "' and ref_station='"+station+"'";
                cmd = new MySqlCommand(q, con);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    n = int.Parse(dr.GetString("niv1"));
                    return n;
                }
                else
                {
                    MessageBox.Show("Erreur de traitement");
                    return 0;
                }

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }

            return n;
        }
        public String findIDcons(string num)
        {
            string n = "";
            try
            {
                InitialiseConnection();
                string q = "select id from t_consommation  where ref_client = '" + num + "'";
                cmd = new MySqlCommand(q, con);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    n = dr.GetString("id");
                    return n;
                }
                else
                {
                    MessageBox.Show("Cette matricule n'a pas d'identifiant dans la consommation");
                    return "";
                }
                    
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }

            return n;
        }
        public String findCommission()
        {
            string n = "";
            try
            {
                InitialiseConnection();
                string q = "select pourcentage from commission";
                cmd = new MySqlCommand(q, con);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    n = dr.GetString("pourcentage");
                    return n;
                }
                else
                {
                    MessageBox.Show("Introuvable");
                    return "";
                }

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }

            return n;
        }
        public void modifierComm(ClassCommission commi)
        {
            try
            {
                InitialiseConnection();
                string q = "update commission set pourcentage=@pourcentage where id=@id";
                cmd = new MySqlCommand(q, con);
                cmd.Parameters.Add(new MySqlParameter("@id", commi.Id));
                cmd.Parameters.Add(new MySqlParameter("@pourcentage", commi.Pourcentage));

                DialogResult result = MessageBox.Show("Voulez-vous vraiment modifier la commission par '"+commi.Pourcentage+"'% ?", "Modification", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Modification effectuée avec succes!!");
                    }
                }

                else
                {
                    MessageBox.Show("Opération Annulée !");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur pendant de traitement!\n"+ex.Message);
            }
            finally
            {
                cmd.Dispose();
                con.Close();

            }
        }
        public void insertUsers(ClassUsers use)
        {
            try {

                InitialiseConnection();

                string q = "insert into retrofit_users(name,email,password,username) VALUES(@name,@email,@password,@username)";
                cmd = new MySqlCommand(q, con);
                cmd.Parameters.Add(new MySqlParameter("@name", use.Username));
                cmd.Parameters.Add(new MySqlParameter("@email", use.Email));
                cmd.Parameters.Add(new MySqlParameter("@password", use.Password));
                cmd.Parameters.Add(new MySqlParameter("@username", use.Nom));
                DialogResult result = MessageBox.Show("Voulez-vous vraiment enregistrer cette operation ?", "Inserer", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Enregistrement avec succes!!");
                    }
                }

                else
                {
                    MessageBox.Show("Opération Annulée !");
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                con.Close();

            }
           


        }
        public void chargerusers(DataGridView data)
        {
            try
            {
                InitialiseConnection();
                
                string q = "select * from retrofit_users ";
                cmd = new MySqlCommand(q, con);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    data.Rows.Add(dr["name"].ToString(), dr["email"].ToString(), dr["password"].ToString(), dr["username"].ToString());
                }

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }   
        public int countCl()
        {
            string c = "";
            int ret = 0;
            try
            {

                InitialiseConnection();
                
                string q = "select count(*) as nbcl from t_client ";
                cmd = new MySqlCommand(q, con);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    c = dr.GetString("nbcl");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }
            ret = int.Parse(c);

            return ret + 1;
        }
        //public String countConso()
        //{
        //    string c = "";
        //    try
        //    {

        //        InitialiseConnection();
        
        //        string q = "select count(*) as nb from t_consommation ";
        //        cmd = new MySqlCommand(q, con);
        //        dr = cmd.ExecuteReader();
        //        if (dr.Read())
        //        {
        //            c = dr.GetString("nb");
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //    finally
        //    {
        //        cmd.Dispose();
        //        con.Close();
        //    }

            
        //    return c;
        //}
        public String SUMConso()
        {
            string c = "";
            try
            {

                InitialiseConnection();

                string q = "select SUM(qte) as nb from t_consommation ";
                cmd = new MySqlCommand(q, con);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    c = dr.GetString("nb");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }


            return c;
        }
        public String SUMConsoStatio(string key, string data, string user)
        {
            string c = "0";
            try
            {

                InitialiseConnection();

                //string q = "select SUM(qte) as nb from t_consommation where username = '" + key + "' AND  Date_Format(date_cons,'%d/%m/%Y') = '" + data + "'  ";
                string q = "select SUM(qte) as nb,username from t_consommation WHERE Date_Format(date_cons,'%d/%m/%Y')='"+data+"' AND username='"+user+"';";
                cmd = new MySqlCommand(q, con);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    c = dr.GetString("nb");
                }
                else
                {
                    c = "0";
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }


            return c;
        }
       


       

      
        string n;
        public Boolean LoginTest(string username, string password)
        {
            Boolean b = false;

            try
            {
                InitialiseConnection();

                cmd = new MySqlCommand("SELECT username, password FROM retrofit_users where username ='" + username + "' AND password = '" + password + "'", con);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    b = true;
                }

                if (b == true)
                {
                    //MessageBox.Show("La connection a reussie !");
                    b = true;
                    
                }
                else
                {
                    MessageBox.Show("Mot de passe ou utilisateur n'est pas trouvé!");
                    b = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return b;
        }
        public Boolean LoginStation(string username, string password)
        {
            Boolean b = false;

            try
            {
                InitialiseConnection();

                cmd = new MySqlCommand("SELECT username, password FROM users where username ='" + username + "' AND password = '" + password + "'", con);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    b = true;
                }

                if (b == true)
                {
                    //MessageBox.Show("La connection a reussie !");
                    b = true;

                }
                else
                {
                    MessageBox.Show("Les informations données sont pas correctes");
                    b = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }  
            return b;
        }
        public void 
            InsertClient(ClassClient client)
        {

           
            try
            {
                InitialiseConnection();


                string qInsert = "INSERT INTO t_client (matr_client,nom,postnom,prenom,adresse,etatcivil,affiliation,reseaux,tel,sexe,qrcode,refcat,id_carte) VALUES (@matr_client,@nom,@postnom,@prenom,@adresse,@etatcivil,@affiliation,@reseaux,@tel,@sexe,@qrcode,@refcat,@id_carte)";
                cmd = new MySqlCommand(qInsert, con);
                cmd.Parameters.Add(new MySqlParameter("@matr_client", client.Mat));
                cmd.Parameters.Add(new MySqlParameter("@nom", client.Nom));
                cmd.Parameters.Add(new MySqlParameter("@adresse", client.Adresse));
                cmd.Parameters.Add(new MySqlParameter("@tel", client.Tel));
                //cmd.Parameters.Add(new MySqlParameter("@photo", client.Photo));
                cmd.Parameters.Add(new MySqlParameter("@postnom", client.Postnom));
                cmd.Parameters.Add(new MySqlParameter("@prenom", client.Prenom));
                cmd.Parameters.Add(new MySqlParameter("@sexe", client.Sexe));
                cmd.Parameters.Add(new MySqlParameter("@reseaux", client.Reseaux));
                cmd.Parameters.Add(new MySqlParameter("@affiliation", client.Affiliation));
                cmd.Parameters.Add(new MySqlParameter("@etatcivil", client.Etatcivil));
                cmd.Parameters.Add(new MySqlParameter("@qrcode", client.Qr));
                //cmd.Parameters.Add(new MySqlParameter("@age", client.Age));
                cmd.Parameters.Add(new MySqlParameter("@refcat", client.Refcat));
                cmd.Parameters.Add(new MySqlParameter("@id_carte", client.Id));

                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Enregistrement avec succes!!");
                        //updatedStatutEnre(client.Idcarte);

                    }
                 else
                 {
                    MessageBox.Show("Opération a echouer !");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           



        }
        public void  InsertClientEnregi(ClassClient client)
        {

           
            try
            {
                InitialiseConnection();


                string qInsert = "INSERT INTO t_client (matr_client,nom,postnom,prenom,adresse,etatcivil,affiliation,reseaux,tel,sexe,qrcode,refcat,id_carte) VALUES (@matr_client,@nom,@postnom,@prenom,@adresse,@etatcivil,@affiliation,@reseaux,@tel,@sexe,@qrcode,@refcat,@id_carte)";
                cmd = new MySqlCommand(qInsert, con);
                cmd.Parameters.Add(new MySqlParameter("@matr_client", client.Mat));
                cmd.Parameters.Add(new MySqlParameter("@nom", client.Nom));
                cmd.Parameters.Add(new MySqlParameter("@adresse", client.Adresse));
                cmd.Parameters.Add(new MySqlParameter("@tel", client.Tel));
                //cmd.Parameters.Add(new MySqlParameter("@photo", client.Photo));
                cmd.Parameters.Add(new MySqlParameter("@postnom", client.Postnom));
                cmd.Parameters.Add(new MySqlParameter("@prenom", client.Prenom));
                cmd.Parameters.Add(new MySqlParameter("@sexe", client.Sexe));
                cmd.Parameters.Add(new MySqlParameter("@reseaux", client.Reseaux));
                cmd.Parameters.Add(new MySqlParameter("@affiliation", client.Affiliation));
                cmd.Parameters.Add(new MySqlParameter("@etatcivil", client.Etatcivil));
                cmd.Parameters.Add(new MySqlParameter("@qrcode", client.Qr));
                cmd.Parameters.Add(new MySqlParameter("@refcat", client.Refcat));
                cmd.Parameters.Add(new MySqlParameter("@id_carte", client.Idcarte));

                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Enregistrement avec succes!!");
                        updatedStatutEnre(client.Idcarte);

                    }
                 else
                 {
                    MessageBox.Show("Opération a echouer !");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           



        }
       
        public void updatedPourcen(ClassPourcentage p)
        {

            InitialiseConnection();
            try
            {
                string q = "UPDATE  pourcentage SET niv1=@niv1,niv2=@niv2,created_at=@created_at where id = 1";
                cmd = new MySqlCommand(q, con);
                cmd.Parameters.Add(new MySqlParameter("@niv1", p.Niveau1));
                cmd.Parameters.Add(new MySqlParameter("@niv2", p.Niveau0));
                cmd.Parameters.Add(new MySqlParameter("@created_at", p.Date_fin));

                DialogResult result = MessageBox.Show("Voulez-vous vraiment modifier le  pourcentage ?", "Modification", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Enregistrement avec succes!!");

                    }
                }

                else
                {
                    MessageBox.Show("Opération Annulée !");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void updatedStatutEnre(string key)
        {

            InitialiseConnection();
            try
            {

                string q = "UPDATE  Enregistrement SET statut=@statut  where idCarte = '" + key + "'";
                cmd = new MySqlCommand(q, con);

                cmd.Parameters.Add(new MySqlParameter("@statut", 0));

                if (cmd.ExecuteNonQuery() == 1)
                {

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void updatedPourcent(ClassPourcentage p)
        {
            
            InitialiseConnection();
            try
            {
                string q = "UPDATE  pourcentage SET niv1=@niv1,niv2=@niv2,created_at=@created_at where id=@id";
                cmd = new MySqlCommand(q, con);
                cmd.Parameters.Add(new MySqlParameter("@id", p.Id));
                cmd.Parameters.Add(new MySqlParameter("@niv1",p.Niveau1));
                cmd.Parameters.Add(new MySqlParameter("@niv2",p.Niveau0 ));
                cmd.Parameters.Add(new MySqlParameter("@created_at", p.Date_fin));

                DialogResult result = MessageBox.Show("Voulez-vous vraiment modifier le  pourcentage ?", "Modification", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Enregistrement avec succes!!");

                    }
                }

                else
                {
                    MessageBox.Show("Opération Annulée !");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //public void updatedPEss(ClassPrix p)
        //{

        //    InitialiseConnection();
        //    try
        //    {
        //        string q = "UPDATE  price SET prix=@prix  where typeconso = 'Essence'";
        //        cmd = new MySqlCommand(q, con);
        //        cmd.Parameters.Add(new MySqlParameter("@prix", p.PrixE));
        //        DialogResult result = MessageBox.Show("Voulez-vous vraiment modifier le prix Essence ?", "Modification", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        //        if (result == DialogResult.Yes)
        //        {
        //            if (cmd.ExecuteNonQuery() == 1)
        //            {
        //                MessageBox.Show("Enregistrement avec succes!!");

        //            }
        //        }

        //        else
        //        {
        //            MessageBox.Show("Opération Annulée !");
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}
        //public void updatedPPetro(ClassPrix p)
        //{

        //    InitialiseConnection();
        //    try
        //    {
        //        string q = "UPDATE  price SET prix=@prix  where typeconso = 'Petrole'";
        //        cmd = new MySqlCommand(q, con);
        //        cmd.Parameters.Add(new MySqlParameter("@prix", p.PrixP));
        //        DialogResult result = MessageBox.Show("Voulez-vous vraiment modifier le prix Petrole ?", "Modification", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        //        if (result == DialogResult.Yes)
        //        {
        //            if (cmd.ExecuteNonQuery() == 1)
        //            {
        //                MessageBox.Show("Enregistrement avec succes!!");

        //            }
        //        }

        //        else
        //        {
        //            MessageBox.Show("Opération Annulée !");
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}
        //public void updatedPDiesel(ClassPrix p)
        //{

        //    InitialiseConnection();
        //    try
        //    {
        //        string q = "UPDATE  price SET prix=@prix  where typeconso = 'Diesel'";
        //        cmd = new MySqlCommand(q, con);
        //        cmd.Parameters.Add(new MySqlParameter("@prix", p.PrixD));
        //        DialogResult result = MessageBox.Show("Voulez-vous vraiment modifier le prix Diesel ?", "Modification", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        //        if (result == DialogResult.Yes)
        //        {
        //            if (cmd.ExecuteNonQuery() == 1)
        //            {
        //                MessageBox.Show("Enregistrement avec succes!!");

        //            }
        //        }

        //        else
        //        {
        //            MessageBox.Show("Opération Annulée !");
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}
        public void insertArbre(ClassArbre arbre)
        {
            InitialiseConnection();
            try
            {
                string qInsert = "INSERT INTO t_arbre (designreseaux,ref_client) VALUES (@designreseaux,@ref_client)";
                cmd = new MySqlCommand(qInsert, con);
                cmd.Parameters.Add(new MySqlParameter("@ref_client", arbre.CodeArbre));
                cmd.Parameters.Add(new MySqlParameter("@designreseaux", arbre.DesignArbre));

                DialogResult result = MessageBox.Show("Voulez-vous vraiment enregistrer cette operation ?", "Suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Enregistrement avec succes!!");

                    }
                }

                else
                {
                    MessageBox.Show("Opération Annulée !");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }
        }
        public void InsertCat(ClassCategorie cat)
        {
            try
            {

                InitialiseConnection();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();

                string q = "INSERT INTO t_categorie(designcat) VALUES (@designcat)";
                cmd = new MySqlCommand(q, con);
                cmd.Parameters.Add(new MySqlParameter("@designcat", cat.Designcat));


                DialogResult result = MessageBox.Show("Voulez-vous vraiment enregistrer cette operation ?", "Suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Enregistrement avec succes!!");
                    }
                }
                else
                {
                    MessageBox.Show("Opération Annulée!");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }
        }
        public void etatCons(int champ,string key, string date_leo)
        {
            
            try
            {
                InitialiseConnection();
                string q = "update t_consommation set etat=@etat where ref_client = '" + key + "' and Date_Format(date_cons,'%d/%m/%Y') = '" + date_leo + "'";
                cmd = new MySqlCommand(q, con);
                cmd.Parameters.Add(new MySqlParameter("@etat",champ));
                dr = cmd.ExecuteReader();
                //MessageBox.Show("Etat modifier");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }

           
        }
        public void InsertBonus(ClassBonus bonus,string date_c)
        {
           
           
            try
            {

                InitialiseConnection();
                string re = "insert into t_bonus (ref_client,qte,prix,pourcentage,montantfc,montantdo,reseaux0,pourcentage0,tel0,tel) VALUES(@ref_client,@qte,@prix,@pourcentage,@montantfc,@montantdo,@reseaux0,@pourcentage0,@tel0,@tel)";
                cmd = new MySqlCommand(re, con);
                //cmd.Parameters.Add(new MySqlParameter("@id", bonus.Id));
                cmd.Parameters.Add(new MySqlParameter("@ref_client", bonus.Ref_cl));
                cmd.Parameters.Add(new MySqlParameter("@qte", bonus.Qte));
                cmd.Parameters.Add(new MySqlParameter("@prix", bonus.Prix));
                cmd.Parameters.Add(new MySqlParameter("@pourcentage", bonus.Pourcent));
                cmd.Parameters.Add(new MySqlParameter("@montantfc", bonus.Montantfc));
                cmd.Parameters.Add(new MySqlParameter("@montantdo", bonus.Montantdo));
            //    cmd.Parameters.Add(new MySqlParameter("@reseaux", bonus.Reseaux));
                cmd.Parameters.Add(new MySqlParameter("@reseaux0", bonus.Reseaux0));
                cmd.Parameters.Add(new MySqlParameter("@pourcentage0", bonus.Pourcent0));
                cmd.Parameters.Add(new MySqlParameter("@tel", bonus.Tel));
                cmd.Parameters.Add(new MySqlParameter("@tel0", bonus.Tel0));


                if (cmd.ExecuteNonQuery() == 1)
                {
                   
                    etatCons(0, bonus.Ref_cl, date_c);
                   

                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
           // MessageBox.Show("Le nombre des insertions " + x + " et des modifications " + y);
        }
        public DataSet sortieCARTE(string key)
        {

            try
            {
                InitialiseConnection();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();

                cmd = new MySqlCommand("select matr_client,nom,tel,qrcode from t_client where matr_client = '"+key+"' ", con);
                adpr = new MySqlDataAdapter(cmd);
                dste = new DataSet();
                adpr.Fill(dste, "t_client");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return dste;
        }
        public DataSet sortieConsoStationType(string date, string type, string station)
        {
            try
            {
                InitialiseConnection();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();

                cmd = new MySqlCommand("select * from t_consommation where  Date_Format(date_cons,'%d/%m/%Y') = '" + date + "' and type_cons ='" + type + "' and username='" + station + "' ", con);
                adpr = new MySqlDataAdapter(cmd);
                dste = new DataSet();
                adpr.Fill(dste, "t_consommation");
               

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return dste;
        }
        public DataSet sortieRetraitDates(string date1, string date2)
        {
            try
            {
                InitialiseConnection();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();

                cmd = new MySqlCommand("select * from retraitsall where Date_Format(DATE,'%d/%m/%Y') between '" + date1 + "' and '" + date2 + "' ", con);
                adpr = new MySqlDataAdapter(cmd);
                dste = new DataSet();
                adpr.Fill(dste, "retraitsall");


            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de connexion avec le server lors de traitements \n ou format de la date (JJ/MM/AA)"+ex.Message);
            }

            return dste;
        }
        public DataSet sortieConsoStationTypeInterval(string date1,string date2, string type, string station)
        {
            try
            {
                InitialiseConnection();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();

                cmd = new MySqlCommand("select * from t_consommation where  Date_Format(date_cons,'%d/%m/%Y') between '" + date1 + "' and '" + date2 + "' and type_cons ='" + type + "' and username='" + station + "' ", con);
                adpr = new MySqlDataAdapter(cmd);
                dste = new DataSet();
                adpr.Fill(dste, "t_consommation");


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return dste;
        }

        public void updatePasse(string mat,string passe ){

            try{
                InitialiseConnection();

                string q = "update detail_carte set mot_de_passe=@mot_de_passe where matricule=@matricule";
                cmd = new MySqlCommand(q,con);
                cmd.Parameters.Add(new MySqlParameter("@mot_de_passe", passe));
                cmd.Parameters.Add(new MySqlParameter("@matricule", mat));
                
                DialogResult result = MessageBox.Show("Voulez-vous vraiment modifier le mot de passe ?", "Modification", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Modification effectuée avec succes!!");

                    }
                }

                else
                {
                    MessageBox.Show("Opération Annulée !");
                }

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        
        }
        public DataSet sortieCl()
        {
           
            try
            {
                InitialiseConnection();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();

                cmd = new MySqlCommand("select matr_client,nom,postnom,prenom,tel,adresse,reseaux,affiliation,qrcode,id_carte from t_client ", con);
                adpr = new MySqlDataAdapter(cmd);
                dste = new DataSet();
                adpr.Fill(dste, "t_client");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return dste;
        }
        public DataSet sortiePartennaire()
        {

            try
            {
                InitialiseConnection();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();

                cmd = new MySqlCommand("select * from partennaire ", con);
                adpr = new MySqlDataAdapter(cmd);
                dste = new DataSet();
                adpr.Fill(dste, "partennaire");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return dste;
        }
        public DataSet sortieConsommation()
        {

            try
            {
                InitialiseConnection();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new MySqlCommand("select * from t_consommation ", con);
                adpr = new MySqlDataAdapter(cmd);
                dste = new DataSet();
                adpr.Fill(dste, "t_consommation");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return dste;
        }
        public DataSet sortieBonus()
        {

            try
            {
                InitialiseConnection();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new MySqlCommand("select * from t_bonus ", con);
                adpr = new MySqlDataAdapter(cmd);
                dste = new DataSet();
                adpr.Fill(dste, "t_bonus");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return dste;
        }
        public DataSet sortieUsers()
        {

            try
            {
                InitialiseConnection();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new MySqlCommand("select * from retrofit_users ", con);
                adpr = new MySqlDataAdapter(cmd);
                dste = new DataSet();
                adpr.Fill(dste, "retrofit_users");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return dste;
        }
        public void insertAgent(ClassAgent ag)
        {
            try
            {
                InitialiseConnection();
                string q = "insert into devise(ref_matricule,nom,pwd) values(@ref_matricule,@nom,@pwd)";
                cmd = new MySqlCommand(q, con);
                cmd.Parameters.Add(new MySqlParameter("@ref_matricule", ag.Matricule));
                cmd.Parameters.Add(new MySqlParameter("@nom", ag.Noms));
                cmd.Parameters.Add(new MySqlParameter("@pwd", ag.Mot_de_passe));

                if (cmd.ExecuteNonQuery() == 1)
                {

                    MessageBox.Show("Creation compte agent effectué");

                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public void deleteAgent(ClassAgent ag)
        {
            try
            {
                InitialiseConnection();
                string q = "delete from devise where id=@id";
                cmd = new MySqlCommand(q,con);

                cmd.Parameters.Add(new MySqlParameter("@id", ag.Id));


                DialogResult result = MessageBox.Show("Voulez-vous vraiment  cet agent  ?", "Suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Suppression avec succes!!");

                    }
                }

                else
                {
                    MessageBox.Show("Opération Annulée !");
                }
            }catch(Exception ex)
            {


                MessageBox.Show("Erreur lors de la suppression!! verifier la connexion on contactait IT");
            }
        }
        public void deleteuser(string key)
        {
            try
            {
                InitialiseConnection();
                string qrdelete = "delete from retrofit_users  where id = @id ";
                cmd = new MySqlCommand(qrdelete, con);
                cmd.Parameters.Add(new MySqlParameter("@id", key));

                DialogResult result = MessageBox.Show("Voulez-vous vraiment  cet utilisateur admin ?", "Suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Suppression avec succes!!");

                    }
                }

                else
                {
                    MessageBox.Show("Opération Annulée !");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Cet ID n'est pas trouvé "+ex.Message);
            }
        }
        public DataSet sortieConsommationKey(string key)
        {

            try
            {
                InitialiseConnection();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new MySqlCommand("select * from t_consommation where ref_client = '"+key+"' ", con);
                adpr = new MySqlDataAdapter(cmd);
                dste = new DataSet();
                adpr.Fill(dste, "t_consommation");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return dste;
        }
        public DataSet sortieConsommationKeyFilter(string key, string station)
        {

            try
            {
                InitialiseConnection();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                //cmd = new MySqlCommand("select * from t_consommation where ref_client = '" + key + "' AND username = '"+ station +"' ", con);
                cmd = new MySqlCommand("select * from t_consommation where ref_client = '" + key + "' and username ='"+station+"' ", con);
                adpr = new MySqlDataAdapter(cmd);
                dste = new DataSet();
                adpr.Fill(dste, "t_consommation");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return dste;
        }
        public DataSet sortieConsommationDate(string date_leo)
        {

            try
            {
                InitialiseConnection();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new MySqlCommand("select * from t_consommation where Date_Format(date_cons,'%d/%m/%Y') = '" + date_leo + "' ", con);
                adpr = new MySqlDataAdapter(cmd);
                dste = new DataSet();
                adpr.Fill(dste, "t_consommation");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return dste;
        }
        public DataSet sortieOperationDateBus(string date_leo)
        {

            try
            {
                InitialiseConnection();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new MySqlCommand("select * from consommation_business where Date_Format(created_at,'%d/%m/%Y') = '" + date_leo + "' ", con);
                adpr = new MySqlDataAdapter(cmd);
                dste = new DataSet();
                adpr.Fill(dste, "consommation_business");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return dste;
        }
        public DataSet sortieOperationBusDate(string date_leo, string bus)
        {

            try
            {
                InitialiseConnection();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new MySqlCommand("select * from consommation_business where Date_Format(created_at,'%d/%m/%Y') = '" + date_leo + "' AND ref_business = '"+bus+"' ", con);
                adpr = new MySqlDataAdapter(cmd);
                dste = new DataSet();
                adpr.Fill(dste, "consommation_business");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return dste;
        }
        public DataSet sortieOperationBusID(string id,string bus)
        {

            try
            {
                InitialiseConnection();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new MySqlCommand("select * from consommation_business where ref_client = '" + id + "' AND ref_business = '" + bus + "' ", con);
                adpr = new MySqlDataAdapter(cmd);
                dste = new DataSet();
                adpr.Fill(dste, "consommation_business");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return dste;
        }
        public DataSet sortieConsoFilter(string date_leo, string station)
        {

            try
            {
                InitialiseConnection();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                //cmd = new MySqlCommand("select * from t_consommation where Date_Format(date_cons,'%d/%m/%Y') = '" + date_leo + "' AND username = '" + station + "' ", con);
                cmd = new MySqlCommand("select * from t_consommation where Date_Format(date_cons,'%d/%m/%Y') = '" + date_leo + "' and username = '"+station+"'", con);
                adpr = new MySqlDataAdapter(cmd);
                dste = new DataSet();
                adpr.Fill(dste, "t_consommation");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return dste;
        }
        public DataSet sortieConsommationInterval(string date1, string date2)
        {

            try
            {
                InitialiseConnection();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new MySqlCommand("select * from t_consommation where Date_Format(date_cons,'%d/%m/%Y') between '"+date1+"' and '"+date2+"' ", con);
                adpr = new MySqlDataAdapter(cmd);
                dste = new DataSet();
                adpr.Fill(dste, "t_consommation");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return dste;
        }
        public DataSet sortieConsommationIntervalFilter(string date1, string date2, string station)
        {

            try
            {
                InitialiseConnection();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                //cmd = new MySqlCommand("select * from t_consommation where Date_Format(date_cons,'%d/%m/%Y') between '" + date1 + "' and '" + date2 + "'  and  username = '" + station + "' ", con);
                cmd = new MySqlCommand("select * from t_consommation where Date_Format(date_cons,'%d/%m/%Y') between '" + date1 + "' and '" + date2 + "' and username='"+station+"' ", con);
                adpr = new MySqlDataAdapter(cmd);
                dste = new DataSet();
                adpr.Fill(dste, "t_consommation");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return dste;
        }
        public DataSet sortieConsommationIntervalFilterBusiness(string date1, string date2, string business)
        {

            try
            {
                InitialiseConnection();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                //cmd = new MySqlCommand("select * from t_consommation where Date_Format(date_cons,'%d/%m/%Y') between '" + date1 + "' and '" + date2 + "'  and  username = '" + station + "' ", con);
                cmd = new MySqlCommand("select * from consommation_business where Date_Format(created_at,'%d/%m/%Y') between '" + date1 + "' and '" + date2 + "' ", con);
                adpr = new MySqlDataAdapter(cmd);
                dste = new DataSet();
                adpr.Fill(dste, "consommation_business");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return dste;
        }
        public List<string> GetDataList(string field, string table, string where = "", string value = "")
        {
            List<string> list = new List<string>();

            InitialiseConnection();

            try
            {
                using (cmd = con.CreateCommand())
                {
                    if (where == "" && value == "")
                    {
                        cmd.CommandText = " SELECT " + field + " FROM " + table;
                    }
                    else
                    {
                        cmd.CommandText = " SELECT " + field + " FROM " + table + " WHERE " + where + " = '" + value + "'";
                    }

                    dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        list.Add((dr[field]).ToString());
                    }

                    return list;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dr.Dispose();
            }
        }
        public void updateStatut(ClassCarte carte)
        {
            try
            {

                InitialiseConnection();
                string q = "update carte set status=@status where id_carte=@id_carte";

                cmd = new MySqlCommand(q, con);
                cmd.Parameters.Add(new MySqlParameter("@id_carte", carte.Idcarte));
                cmd.Parameters.Add(new MySqlParameter("@status", carte.Statut));
                if (cmd.ExecuteNonQuery() == 1)
                {
                   // MessageBox.Show("Carte en Production");
                }

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }
        }
        public void deleteCompte(string nfc)
        {
            try
            {

                InitialiseConnection();
                string q = "delete from detail_carte where ref_nfc=@ref_nfc";

                cmd = new MySqlCommand(q, con);
                cmd.Parameters.Add(new MySqlParameter("@ref_nfc",nfc));
               
                if (cmd.ExecuteNonQuery() == 1)
                {
                    // MessageBox.Show("Carte en Production");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }
        }
        public DataSet sortieAvantage()
        {

            try
            {
                InitialiseConnection();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new MySqlCommand("select * from t_consommation where qte >=15 ", con);
                adpr = new MySqlDataAdapter(cmd);
                dste = new DataSet();
                adpr.Fill(dste, "t_consommation");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return dste;
        }
        public DataSet sortieConsommationDateUser(string user, string date2)
        {

            try
            {
                InitialiseConnection();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new MySqlCommand("select * from t_consommation where Date_Format(date_cons,'%d/%m/%Y') = '" + date2 + "' and username ='" + user + "' ", con);
                adpr = new MySqlDataAdapter(cmd);
                dste = new DataSet();
                adpr.Fill(dste, "t_consommation");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return dste;
        }
        public DataSet sortieConsommationIntervallUser(string user, string date1, string date2 )
        {

            try
            {
                InitialiseConnection();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new MySqlCommand("select * from t_consommation where Date_Format(date_cons,'%d/%m/%Y') = '" + date2 + "' and username ='" + user + "' ", con);
                adpr = new MySqlDataAdapter(cmd);
                dste = new DataSet();
                adpr.Fill(dste, "t_consommation");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return dste;
        }

        public DataSet sortieConsommationMat(string code)
        {

            try
            {
                InitialiseConnection();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                cmd = new MySqlCommand("select * from t_consommation where username = '" + code + "' ", con);
                adpr = new MySqlDataAdapter(cmd);
                dste = new DataSet();
                adpr.Fill(dste, "t_consommation");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return dste;
        }
        public DataSet sortieArbreCl(string mat)
        {

            try
            {
                InitialiseConnection();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();

                cmd = new MySqlCommand("select * from t_client where reseaux = '" + mat + "' ", con);
                adpr = new MySqlDataAdapter(cmd);
                dste = new DataSet();
                adpr.Fill(dste, "t_client");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return dste;
        }
        public void UpdateClient(ClassClient client)
        {
            try
            {
                InitialiseConnection();

                //if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                string qupdate = "UPDATE t_client SET nom=@nom,postnom=@postnom,adresse=@adresse,prenom=@prenom,tel=@tel,reseaux=@reseaux,affiliation=@affiliation,sexe=@sexe,etatCivil=@etatCivil,refcat=@refcat, id_carte=@id_carte WHERE id=@id";
                cmd = new MySqlCommand(qupdate, con);

                //cmd.Parameters.Add(new MySqlParameter("@matr_client", client.Id));
                cmd.Parameters.Add(new MySqlParameter("@id", client.Ib));
                cmd.Parameters.Add(new MySqlParameter("@nom", client.Nom));
                cmd.Parameters.Add(new MySqlParameter("@adresse", client.Adresse));
                cmd.Parameters.Add(new MySqlParameter("@tel", client.Tel));
                cmd.Parameters.Add(new MySqlParameter("@postnom", client.Postnom));
                cmd.Parameters.Add(new MySqlParameter("@prenom", client.Prenom));
                cmd.Parameters.Add(new MySqlParameter("@sexe", client.Sexe));
                cmd.Parameters.Add(new MySqlParameter("@reseaux", client.Reseaux));
                cmd.Parameters.Add(new MySqlParameter("@affiliation", client.Affiliation));
                cmd.Parameters.Add(new MySqlParameter("@etatcivil", client.Etatcivil));
                //cmd.Parameters.Add(new MySqlParameter("@qrcode", client.Qr));
                //cmd.Parameters.Add(new MySqlParameter("@age", client.Age));
                cmd.Parameters.Add(new MySqlParameter("@refcat", client.Refcat));
                cmd.Parameters.Add(new MySqlParameter("@id_carte", client.Id));

                DialogResult result = MessageBox.Show("Voulez-vous vraiment modifier  ?", "Modification", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        cmd.Dispose();
                        con.Close();
                        MessageBox.Show("Modification avec succes!!");
                        ClassCarte c = new ClassCarte();
                        c.Idcarte = client.Id;
                        c.Statut = "Production";
                        glos.updateStatut(c);

                    }
                }

                else
                {
                    MessageBox.Show("Opération Annulée !");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
    


}
 
           
