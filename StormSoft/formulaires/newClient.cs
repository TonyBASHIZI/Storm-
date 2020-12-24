using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Text;
using MySql.Data.MySqlClient;
using System.IO;
using StormSoft.classe;
using ThoughtWorks.QRCode.Codec;
using StormSoft.report;
using DevExpress.XtraReports.UI;

namespace StormSoft.formulaires
{
    
    
    public partial class newClient : DevExpress.XtraEditors.XtraForm
    {
        //MySqlConnection mcon = new MySqlConnection("datasource=localhost;port=3306;username=root;password=");
        //MySqlCommand mcd;
        glossaire glos = new glossaire();
        string imglocation = "";
        //DataTable dbdataset;
        public newClient()
        {
            InitializeComponent();
           

        }
        //public void closeCon()
        //{
        //    if (mcon.State == ConnectionState.Open)
        //    {
        //        mcon.Close();
        //    }
        //}
        void QRCode(PictureBox pic_box, string data)
        {

            try
            {
                var objQRCode = new QRCodeEncoder();
                Image imgImage;
                objQRCode.QRCodeEncodeMode = ThoughtWorks.QRCode.Codec.QRCodeEncoder.ENCODE_MODE.BYTE;
                objQRCode.QRCodeScale = 7;
                objQRCode.QRCodeVersion = 4;
                objQRCode.QRCodeErrorCorrect = ThoughtWorks.QRCode.Codec.QRCodeEncoder.ERROR_CORRECTION.L;
                imgImage = objQRCode.Encode(data);
                pic_box.Image = imgImage;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //public void openCon()
        //{
        //    if (mcon.State == ConnectionState.Closed)
        //    {
        //        mcon.Open();
        //    }
        //}
        //public void ExecuteQuery(string q)
        //{
        //    try
        //    {
        //        openCon();
        //        mcd = new MySqlCommand(q, mcon);
        //        if (mcd.ExecuteNonQuery() == 1)
        //        {
        //            MessageBox.Show("Query Executed");
        //        }
        //        else
        //        {
        //            MessageBox.Show("Query Not Executed");
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //    finally
        //    {
        //        closeCon();
        //        mcd.Dispose();
        //    }
        //}
        

        private void simpleButton2_Click(object sender, EventArgs e)
        
        {
            //OpenFileDialog dialog = new OpenFileDialog();
            //dialog.Filter = "png files(*.png)|*png|jpg files(*.jpg)|*jpg|All files(*.*)|*.*  ";
            //if(dialog.ShowDialog()==DialogResult.OK)
            //{
            //    imglocation = dialog.FileName.ToString();
            //    pictureEdit1.Image = Image.FromFile(imglocation);
                

            //}

        }
        private Byte[] convertImageTobyte(PictureBox pic)
        {
            MemoryStream ms = new MemoryStream();
            Bitmap bmpImage = new Bitmap(pic.Image);
            Byte[] bytImage;
            bmpImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            bytImage = ms.ToArray();
            ms.Close();
            return bytImage;
        }
        private Byte[] convertImagePicEdit(PictureEdit pic)
        {
            MemoryStream ms = new MemoryStream();
            Bitmap bmpImage = new Bitmap(pic.Image);
            Byte[] bytImage;
            bmpImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            bytImage = ms.ToArray();
            ms.Close();
            return bytImage;
        }

       

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            ClassCategorie cat = new ClassCategorie();
            cat.Designcat = comboBox1.Text;
            glos.InsertCat(cat);
            //string q = "insert into station_db.tcategorie (designcat) values('" + comboBox1.Text + "')";
            //ExecuteQuery(q);
            
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            QRCode(pictureBox2, txtmat.Text);
        }
      

        private void newClient_Load(object sender, EventArgs e)
        {
            //glos.chargerclient(dataGridView1);
            glos.GetDatas(gridControl1, "id,matr_client,nom,postnom,prenom,tel,adresse,reseaux,affiliation,id_carte", "t_client");
            //glos.chargerDesignRes(comboR);
            txtmat.Text = getmat();
            searchControl2.Properties.DataSource = glossaire.GetInstance().GetDataList("id_carte", "carte", "status", "Active");
            comboR.Text = "Mag-01-";
            
          
            
        }
        public string getmat()
        {
            Random rnd = new Random();
            int x = rnd.Next(1, 5000);
            
          
            string mat;

            DateTime da = DateTime.Now;

            string lastid = "" + glos.countCl();

            mat = "Mag-" + da.Month +"-" + lastid + "-" + x;
            comboR.Text = "Mag-01-";

            return mat;
        }

        private void pictureBox2_DoubleClick(object sender, EventArgs e)
        {
        
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            txtmat.Text = gridView1.GetFocusedRowCellValue("matr_client").ToString();
            txtnom.Text = gridView1.GetFocusedRowCellValue("nom").ToString();
            txtprenom.Text = gridView1.GetFocusedRowCellValue("postnom").ToString();
            txtpost.Text = gridView1.GetFocusedRowCellValue("prenom").ToString();
            txtadres.Text = gridView1.GetFocusedRowCellValue("adresse").ToString();
            txtAffilier.Text = gridView1.GetFocusedRowCellValue("affiliation").ToString();
            txtphone.Text = gridView1.GetFocusedRowCellValue("tel").ToString();
            comboR.Text = gridView1.GetFocusedRowCellValue("reseaux").ToString();

        }

        private void label18_Click(object sender, EventArgs e)
        {
           
        }

        private void label19_Click(object sender, EventArgs e)
        {
           
        }

       

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            
        }




        private void bindingNavigator1_RefreshItems(object sender, EventArgs e)
        {

        }
        private void txtAffilier_TextChanged(object sender, EventArgs e)
        {

        }

       

       

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            try
            {
                int r = glos.countARB(comboR.Text);
                if (r == 10)
                {

                    MessageBox.Show("Le nombre d'enfant est deja atteint pour cette persconne!!");
                }
                else
                {
                    int y;

                    try
                    {
                        //byte[] images = null;
                        //FileStream stream = new FileStream(imglocation, FileMode.Open, FileAccess.Read);
                        //BinaryReader brs = new BinaryReader(stream);
                        //images = brs.ReadBytes((int)stream.Length);

                        ClassClient cl = new ClassClient();
                        cl.Mat = txtmat.Text;
                        cl.Nom = txtnom.Text;
                        cl.Postnom = txtpost.Text;
                        cl.Prenom = txtprenom.Text;
                        cl.Refcat = comboCat.Text;
                        cl.Reseaux = comboR.Text;
                        cl.Sexe = comboSexe.Text;
                        cl.Tel = txtphone.Text;
                        //cl.Photo = convertImagePicEdit(pictureEdit1);
                        cl.Qr = convertImageTobyte(pictureBox2);
                        cl.Affiliation = txtAffilier.Text;
                        cl.Adresse = txtadres.Text;
                        cl.Etatcivil = comboEtat.Text;
                       // cl.Age = int.Parse(txtage.Text);
                        cl.Id = searchControl2.Text;
                        
                        //y = glos.countARB(comboR.Text);
                        glos.InsertClient(cl);
                        glos.GetDatas(gridControl1, "id,matr_client,nom,postnom,prenom,tel,adresse,reseaux,affiliation,id_carte", "t_client");
                        //glos.chargerMat(comboR);
                        initialise();
                        

                    }
                    catch (Exception ax)
                    {
                        MessageBox.Show(ax.Message);
                    }

        }
                     

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           

        }
       
        public void initialise()
        {
            txtmat.Text = getmat();
            txtnom.Text = "";
            txtnom.Text = "";
            txtprenom.Text = "";
            txtadres.Text = "";
            txtAffilier.Text = "";
            //txtage.Text = "";
            txtphone.Text = "";
            txtpost.Text = "";
            searchControl2.Properties.DataSource = glossaire.GetInstance().GetDataList("id_carte", "carte", "status", "Active");
            comboR.Text = "Mag-01-";
            

            //this.Show();

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

            try
            {
                if(txtmat.Text=="")
                {
                    MessageBox.Show("Vous devez selectionner un client dans la table client en bas!");
                }
                else
                {
                    ClassClient c = new ClassClient();
                    c.Mat = txtmat.Text;
                    c.Id = txtIdcarte.Text;
                    glos.deleteClient(c);
                    glos.GetDatas(gridControl1, "id,matr_client,nom,postnom,prenom,tel,adresse,reseaux,affiliation,id_carte", "t_client");
                    initialise();
                }
              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

       

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void rbParrain_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            try
            {
                client cl = new client();
                cl.DataSource = StormSoft.classe.glossaire.GetInstance().sortieCl();
                ReportPrintTool printTool = new ReportPrintTool(cl);
                printTool.ShowPreviewDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void comboR_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try{
            //    MessageBox.Show("Cette personne a deja  " + glos.countARB(comboR.Text) + " a son arbre!");
                

            //}catch(Exception ex){
            //    MessageBox.Show(ex.Message);
            //}
            
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            this.ShowDialog();
        }

        private void searchControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DataView DV = new DataView(dbdataset);
            //DV.RowFilter = string.Format("matr_client LIKE '%{0}%'", searchControl1.Text);
           // dataGridView1.DataSource = DV;
        }

        private void bindingNavigator1_RefreshItems_1(object sender, EventArgs e)
        {

        }

        private void searchControl1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            //glos.seachcl(searchControl1.Text, data);
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            try
            {
                arbre cl = new arbre();
                cl.DataSource = StormSoft.classe.glossaire.GetInstance().sortieArbreCl(comboR.Text);
                ReportPrintTool printTool = new ReportPrintTool(cl);
                printTool.ShowPreviewDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            try
            {
                carte cl = new carte();
                cl.DataSource = StormSoft.classe.glossaire.GetInstance().sortieCARTE(txtmat.Text);
                ReportPrintTool printTool = new ReportPrintTool(cl);
                printTool.ShowPreviewDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //DataView DV = new DataView(dbdataset);
            //DV.RowFilter = string.Format("matr_client LIKE '%{0}%'", textBox1.Text);
            //dataGridView1.DataSource = DV;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            try
            {
                //byte[] images = null;
                //FileStream stream = new FileStream(imglocation, FileMode.Open, FileAccess.Read);
                //BinaryReader brs = new BinaryReader(stream);
                //images = brs.ReadBytes((int)stream.Length);

                ClassClient cl = new ClassClient();
                cl.Mat = txtmat.Text;
                cl.Nom = txtnom.Text;
                cl.Ib =int.Parse(txtIdcl.Text);
                cl.Id = searchControl2.Text;
                cl.Postnom = txtpost.Text;
                cl.Prenom = txtprenom.Text;
                cl.Refcat = comboCat.Text;
                cl.Reseaux = comboR.Text;
                cl.Sexe = comboSexe.Text;
                cl.Tel = txtphone.Text;
                //cl.Photo = convertImagePicEdit(pictureEdit1);
                cl.Qr = convertImageTobyte(pictureBox2);
                cl.Affiliation = txtAffilier.Text;
                cl.Adresse = txtadres.Text;
                cl.Etatcivil = comboEtat.Text;
               // cl.Age = int.Parse(txtage.Text);

                glos.UpdateClient(cl);
                glos.GetDatas(gridControl1, "id,matr_client,nom,postnom,prenom,tel,adresse,reseaux,affiliation,id_carte", "t_client");
               // glos.chargerMat(comboR);
                initialise();


            }
            catch (Exception ax)
            {
                MessageBox.Show(ax.Message);
            }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            try
            {
                glos.speakeer("Cette personne a deja  " + glos.countARB(comboR.Text) + " a son arbre!");
                MessageBox.Show("Cette personne a deja  " + glos.countARB(comboR.Text) + " a son arbre!");


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            panel5.BackColor = Color.Lime;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            panel5.BackColor = Color.Transparent;
        }
        public void getMat2()
        {
            Random rnd = new Random();
            int x = rnd.Next(1, 500);
            string mat2;

            DateTime da = DateTime.Now;

            string lastid = "" + glos.countCl();
            txtmat.Text = "Mag-02-" + da.Day + "" + da.Year + "-" + lastid;
            comboR.Text = "Mag-02-";
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            getMat2();

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
           txtmat.Text =  getmat();
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            txtmat.Text = getmat();
        }

        private void panel5_MouseEnter(object sender, EventArgs e)
        {
            
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void simpleButton3_Click_1(object sender, EventArgs e)
        {
            //glos.GetDatasNext(gridControl1, "id,matr_client,nom,postnom,prenom,tel,adresse,reseaux,affiliation,id_carte", "t_client", 1000);
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            glos.GetDatas(gridControl1, "id,matr_client,nom,postnom,prenom,tel,adresse,reseaux,affiliation,id_carte", "t_client");
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            glos.GetDatasNext(gridControl1, "id,matr_client,nom,postnom,prenom,tel,adresse,reseaux,affiliation,id_carte", "t_client", 5000);
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
            
            txtmat.Text = gridView1.GetFocusedRowCellValue("matr_client").ToString();
            txtadres.Text = gridView1.GetFocusedRowCellValue("adresse").ToString();
            txtprenom.Text = gridView1.GetFocusedRowCellValue("prenom").ToString();
            txtIdcl.Text = gridView1.GetFocusedRowCellValue("id").ToString();
            txtpost.Text = gridView1.GetFocusedRowCellValue("postnom").ToString();
            txtnom.Text = gridView1.GetFocusedRowCellValue("nom").ToString();
            txtAffilier.Text = gridView1.GetFocusedRowCellValue("affiliation").ToString();
            txtphone.Text = gridView1.GetFocusedRowCellValue("tel").ToString();
            comboR.Text = gridView1.GetFocusedRowCellValue("reseaux").ToString();
            txtIdcarte.Text = gridView1.GetFocusedRowCellValue("id_carte").ToString();
            searchControl2.Text = gridView1.GetFocusedRowCellValue("id_carte").ToString();
            //searchControl2.Text = "toto";
            //txtIdcarte.Text = gridView1.GetFocusedRowCellValue("id_carte").ToString();
            //comboSexe.Text = gridView1.GetFocusedRowCellValue("sexe").ToString();
            //comboEtat.Text = gridView1.GetFocusedRowCellValue("etatcivil").ToString();
            //txtmat.Text = getmat();
            
            

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
          

            
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                byte[] images = null;
                FileStream stream = new FileStream(imglocation, FileMode.Open, FileAccess.Read);
                BinaryReader brs = new BinaryReader(stream);
                images = brs.ReadBytes((int)stream.Length);




                ClassClient cl = new ClassClient();
                cl.Idcarte = txtIdcl.Text;
                cl.Mat = txtmat.Text;
                cl.Nom = txtnom.Text;
                cl.Postnom = txtpost.Text;
                cl.Prenom = txtprenom.Text;
                cl.Refcat = comboCat.Text;
                cl.Reseaux = comboR.Text;
                cl.Sexe = comboSexe.Text;
                cl.Tel = txtphone.Text;
                //cl.Photo = convertImagePicEdit(pictureEdit1);
                cl.Qr = convertImageTobyte(pictureBox2);
                cl.Affiliation = txtAffilier.Text;
                cl.Adresse = txtadres.Text;
                cl.Etatcivil = comboEtat.Text;
                //cl.Age = int.Parse(txtage.Text);

                glos.UpdateClient(cl);
                glos.GetDatas(gridControl1, "id,matr_client,nom,postnom,prenom,tel,adresse,reseaux,affiliation,id_carte", "t_client");
                // glos.chargerMat(comboR);
                initialise();


            }
            catch (Exception ax)
            {
                MessageBox.Show(ax.Message);
            }
        }

        private void comboEtat_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox4_MouseEnter(object sender, EventArgs e)
        {
            panel9.BackColor = Color.Red;
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            panel9.BackColor = Color.Transparent;
        }

        private void pictureBox5_MouseEnter(object sender, EventArgs e)
        {
            panel10.BackColor = Color.Red;
        }

        private void panel10_MouseLeave(object sender, EventArgs e)
        {
            panel10.BackColor = Color.Transparent;
        }

        private void searchControl2_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void searchControl2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            formMotdePasse mot = new formMotdePasse();
            mot.ShowDialog();
        }

        private void pictureBox5_MouseLeave(object sender, EventArgs e)
        {
            panel10.BackColor = Color.Transparent;
        }

        
       
        
    }
}