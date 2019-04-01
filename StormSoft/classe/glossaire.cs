using System;
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
        public void chargerDatacl(DataGridView data){
            
            try
            {
                InitialiseConnection();
                string q = "select matr_client,nom,postnom,prenom,tel,age,adresse,reseaux,affiliation,photo from t_client";
                cmd = new MySqlCommand(q, con);
                dt = new MySqlDataAdapter(cmd);
                DataTable table = new DataTable();
                data.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                data.RowTemplate.Height = 120;
                data.AllowUserToAddRows = false;

                dt.Fill(table);
                data.DataSource = table;

                DataGridViewImageColumn imgcolum = new DataGridViewImageColumn();
                imgcolum = (DataGridViewImageColumn)data.Columns[9];
                imgcolum.ImageLayout = DataGridViewImageCellLayout.Stretch;
                dt.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public void deleteClient(ClassClient client, string code)
        {
            try
            {


                InitialiseConnection();
                //if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                string qrdelete = "delete from t_client where matr_client = @matr_client ";
                cmd = new MySqlCommand(qrdelete, con);
                cmd.Parameters.Add(new MySqlParameter("@matr_client", client.Mat));

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

        public void seachcl(string champ, DataGridView data)
        {
            try
            {
                InitialiseConnection();
                string q = "select matr_client,nom,postnom,prenom,tel,age,adresse,reseaux,affiliation,photo from t_client where matr_client = '"+champ+"'";
                cmd = new MySqlCommand(q, con);
                dt = new MySqlDataAdapter(cmd);
                DataTable table = new DataTable();
                data.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                data.RowTemplate.Height = 120;
                data.AllowUserToAddRows = false;

                dt.Fill(table);
                data.DataSource = table;

                DataGridViewImageColumn imgcolum = new DataGridViewImageColumn();
                imgcolum = (DataGridViewImageColumn)data.Columns[9];
                imgcolum.ImageLayout = DataGridViewImageCellLayout.Stretch;
                dt.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

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
        public void chargerMat(ComboBox data)
        {
            try
            {
                InitialiseConnection();
              
                string req = "select ref_client from t_consommation ";
                cmd = new MySqlCommand(req, con);

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    data.Items.Add(dr["ref_client"].ToString());

                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {

            }
           
            
        }
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
            try
            {
                InitialiseConnection();
                //if (!con.State.ToString().ToLower().Equals("open"))
                //{
                //    con.Open();

                //}


                string req = "select ref_client from t_arbre ";
                cmd = new MySqlCommand(req, con);

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    data.Items.Add(dr["ref_client"].ToString());



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
                    res = "";
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
        public string chargerPourcentN0()
        {
            InitialiseConnection();
            string n1 = "";
            try
            {
                string q = "select niv2 from pourcentage ";
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
        public string chargerPourcentN1()
        {
            InitialiseConnection();
            string n = "";
            try
            {
                string q = "select niv1 from pourcentage ";
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
        public String countConso()
        {
            string c = "";
            try
            {

                InitialiseConnection();
        
                string q = "select count(*) as nb from t_consommation ";
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
                    MessageBox.Show("Echec de Connexion");
                    b = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return b;
        }
        public void InsertClient(ClassClient client)
        {

           
            try
            {
                InitialiseConnection();

             
                string qInsert = "INSERT INTO t_client (matr_client,nom,postnom,prenom,adresse,etatcivil,affiliation,reseaux,photo,tel,sexe,age,qrcode,refcat) VALUES (@matr_client,@nom,@postnom,@prenom,@adresse,@etatcivil,@affiliation,@reseaux,@photo,@tel,@sexe,@age,@qrcode,@refcat)";
                cmd = new MySqlCommand(qInsert, con);
                cmd.Parameters.Add(new MySqlParameter("@matr_client", client.Mat));
                cmd.Parameters.Add(new MySqlParameter("@nom", client.Nom));
                cmd.Parameters.Add(new MySqlParameter("@adresse", client.Adresse));
                cmd.Parameters.Add(new MySqlParameter("@tel", client.Tel));
                cmd.Parameters.Add(new MySqlParameter("@photo", client.Photo));
                cmd.Parameters.Add(new MySqlParameter("@postnom", client.Postnom));
                cmd.Parameters.Add(new MySqlParameter("@prenom", client.Prenom));
                cmd.Parameters.Add(new MySqlParameter("@sexe", client.Sexe));
                cmd.Parameters.Add(new MySqlParameter("@reseaux", client.Reseaux));
                cmd.Parameters.Add(new MySqlParameter("@affiliation", client.Affiliation));
                cmd.Parameters.Add(new MySqlParameter("@etatcivil", client.Etatcivil));
                cmd.Parameters.Add(new MySqlParameter("@qrcode", client.Qr));
                cmd.Parameters.Add(new MySqlParameter("@age", client.Age));
                cmd.Parameters.Add(new MySqlParameter("@refcat", client.Refcat));


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
        public void updatedPourcent(ClassPourcentage p)
        {
            
            InitialiseConnection();
            try
            {
                string q = "UPDATE  pourcentage SET niv1=@niv1,niv2=@niv2,created_at=@created_at where id = 1";
                cmd = new MySqlCommand(q, con);
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
        public void updatedPEss(ClassPrix p)
        {

            InitialiseConnection();
            try
            {
                string q = "UPDATE  price SET prix=@prix  where typeconso = 'Essence'";
                cmd = new MySqlCommand(q, con);
                cmd.Parameters.Add(new MySqlParameter("@prix", p.PrixE));
                DialogResult result = MessageBox.Show("Voulez-vous vraiment modifier le prix Essence ?", "Modification", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

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
        public void updatedPPetro(ClassPrix p)
        {

            InitialiseConnection();
            try
            {
                string q = "UPDATE  price SET prix=@prix  where typeconso = 'Petrole'";
                cmd = new MySqlCommand(q, con);
                cmd.Parameters.Add(new MySqlParameter("@prix", p.PrixP));
                DialogResult result = MessageBox.Show("Voulez-vous vraiment modifier le prix Petrole ?", "Modification", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

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
        public void updatedPDiesel(ClassPrix p)
        {

            InitialiseConnection();
            try
            {
                string q = "UPDATE  price SET prix=@prix  where typeconso = 'Diesel'";
                cmd = new MySqlCommand(q, con);
                cmd.Parameters.Add(new MySqlParameter("@prix", p.PrixD));
                DialogResult result = MessageBox.Show("Voulez-vous vraiment modifier le prix Diesel ?", "Modification", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

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
        public void InsertBonus(ClassBonus bonus,int x, int y,string date_c)
        {
           
           
            try
            {

                InitialiseConnection();
                string re = "insert into t_bonus (ref_client,qte,prix,pourcentage,montantfc,montantdo,reseaux,reseaux0,pourcentage0,tel0,tel) VALUES(@ref_client,@qte,@prix,@pourcentage,@montantfc,@montantdo,@reseaux,@reseaux0,@pourcentage0,@tel0,@tel)";
                cmd = new MySqlCommand(re, con);
                //cmd.Parameters.Add(new MySqlParameter("@id", bonus.Id));
                cmd.Parameters.Add(new MySqlParameter("@ref_client", bonus.Ref_cl));
                cmd.Parameters.Add(new MySqlParameter("@qte", bonus.Qte));
                cmd.Parameters.Add(new MySqlParameter("@prix", bonus.Prix));
                cmd.Parameters.Add(new MySqlParameter("@pourcentage", bonus.Pourcent));
                cmd.Parameters.Add(new MySqlParameter("@montantfc", bonus.Montantfc));
                cmd.Parameters.Add(new MySqlParameter("@montantdo", bonus.Montantdo));
                cmd.Parameters.Add(new MySqlParameter("@reseaux", bonus.Reseaux));
                cmd.Parameters.Add(new MySqlParameter("@reseaux0", bonus.Reseaux0));
                cmd.Parameters.Add(new MySqlParameter("@pourcentage0", bonus.Qte));
                cmd.Parameters.Add(new MySqlParameter("@tel", bonus.Tel));
                cmd.Parameters.Add(new MySqlParameter("@tel0", bonus.Tel0));


                DialogResult result = MessageBox.Show("Voulez-vous vraiment enregistrer cette operation ?", "Insertion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        x++;
                        etatCons(0, bonus.Ref_cl, date_c);
                        y++;
                        
                    }
                }
                else
                {
                    MessageBox.Show("Opération Annulée!");
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("Le nombre des insertions " + x + " et des modifications " + y);
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
        public DataSet sortieCl()
        {
           
            try
            {
                InitialiseConnection();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();

                cmd = new MySqlCommand("select matr_client,nom,postnom,prenom,tel,age,adresse,reseaux,affiliation,photo,qrcode from t_client ", con);
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
        public DataSet sortieArbreCl(string mat)
        {

            try
            {
                InitialiseConnection();
                if (!con.State.ToString().ToLower().Equals("open")) con.Open();

                cmd = new MySqlCommand("select * from t_client where reseaux = '"+mat+"' ", con);
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
        public void UpdateClient(ClassClient client, byte[] img, byte[] qr)
        {
            try
            {
                InitialiseConnection();

                if (!con.State.ToString().ToLower().Equals("open")) con.Open();
                string qupdate = "UPDATE tclient SET nom=@nom,postnom=@postnom,adresse=@adresse,prenom=@prenom,tel=@tel,reseaux=@reseaux,affiliation=@affiliation,sexe=@sexe,age=@age,etatCivil=@etatCivil,qrcode=@qrcode,refcat=@refcat,photo=@photo WHERE matr_client = @matr_client";
                cmd = new MySqlCommand(qupdate, con);
                cmd.Parameters.Add(new MySqlParameter("@matr_client", client.Mat));
                cmd.Parameters.Add(new MySqlParameter("@nom", client.Nom));
                cmd.Parameters.Add(new MySqlParameter("@adresse", client.Adresse));
                cmd.Parameters.Add(new MySqlParameter("@tel", client.Tel));
                cmd.Parameters.Add(new MySqlParameter("@photo", img));
                cmd.Parameters.Add(new MySqlParameter("@postnom", client.Postnom));
                cmd.Parameters.Add(new MySqlParameter("@prenom", client.Prenom));
                cmd.Parameters.Add(new MySqlParameter("@sexe", client.Sexe));
                cmd.Parameters.Add(new MySqlParameter("@reseaux", client.Reseaux));
                cmd.Parameters.Add(new MySqlParameter("@affiliation", client.Affiliation));
                cmd.Parameters.Add(new MySqlParameter("@etatcivil", client.Etatcivil));
                cmd.Parameters.Add(new MySqlParameter("@qrcode", qr));
                cmd.Parameters.Add(new MySqlParameter("@age", client.Age));
                cmd.Parameters.Add(new MySqlParameter("@refcat", client.Refcat));


                DialogResult result = MessageBox.Show("Voulez-vous vraiment modifier  ?", "Modifier", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Modification avec succes!!");
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
    }
    


}

