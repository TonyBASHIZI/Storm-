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
        MySqlConnection mcon = new MySqlConnection("datasource=localhost;port=3306;username=root;password=");
        MySqlCommand mcd;
        glossaire glos = new glossaire();
        string imglocation = "";
        DataTable dbdataset;
        public newClient()
        {
            InitializeComponent();
           

        }
        public void closeCon()
        {
            if (mcon.State == ConnectionState.Open)
            {
                mcon.Close();
            }
        }
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

        public void openCon()
        {
            if (mcon.State == ConnectionState.Closed)
            {
                mcon.Open();
            }
        }
        public void ExecuteQuery(string q)
        {
            try
            {
                openCon();
                mcd = new MySqlCommand(q, mcon);
                if (mcd.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Query Executed");
                }
                else
                {
                    MessageBox.Show("Query Not Executed");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                closeCon();
                mcd.Dispose();
            }
        }
        

        private void simpleButton2_Click(object sender, EventArgs e)
        
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "png files(*.png)|*png|jpg files(*.jpg)|*jpg|All files(*.*)|*.*  ";
            if(dialog.ShowDialog()==DialogResult.OK)
            {
                imglocation = dialog.FileName.ToString();
                pictureEdit1.Image = Image.FromFile(imglocation);
                

            }

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
            glos.chargerDatacl(dataGridView1);
            glos.chargerDesignRes(comboR);
            txtmat.Text = getmat();
            
          
            
        }
        public string getmat()
        {
            string mat;
             DateTime da =  DateTime.Now;

             string lastid = ""+ glos.countCl();
               
            mat = "M00-M-"+da.Month+""+da.Year+""+ "-"+lastid;

            return mat;
        }

        private void pictureBox2_DoubleClick(object sender, EventArgs e)
        {
        
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            txtmat.Text = dataGridView1.CurrentRow.Cells["matr_client"].FormattedValue.ToString();
            txtnom.Text = dataGridView1.CurrentRow.Cells["nom"].FormattedValue.ToString();
            txtprenom.Text = dataGridView1.CurrentRow.Cells["postnom"].FormattedValue.ToString();
            txtpost.Text = dataGridView1.CurrentRow.Cells["prenom"].FormattedValue.ToString();
            txtadres.Text = dataGridView1.CurrentRow.Cells["adresse"].FormattedValue.ToString();

        }

        private void label18_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] images = null;
                FileStream stream = new FileStream(imglocation, FileMode.Open, FileAccess.Read);
                BinaryReader brs = new BinaryReader(stream);
                images = brs.ReadBytes((int)stream.Length);



                ClassClient cl = new ClassClient();
                cl.Mat = txtmat.Text;
                cl.Nom = txtnom.Text;
                cl.Postnom = txtpost.Text;
                cl.Prenom = txtprenom.Text;
                cl.Refcat = comboCat.Text;
                cl.Reseaux = comboR.Text;
                cl.Sexe = comboSexe.Text;
                cl.Tel = txtphone.Text;
                cl.Photo = images;
                cl.Qr = convertImagePicEdit(pictureEdit1);
                cl.Affiliation = txtAffilier.Text;
                cl.Adresse = txtadres.Text;
                cl.Etatcivil = comboEtat.Text;
                cl.Age = int.Parse(txtage.Text);

                glos.UpdateClient(cl, images, convertImagePicEdit(pictureEdit1));
                glos.chargerclient(dataGridView1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label19_Click(object sender, EventArgs e)
        {
            ClassClient c = new ClassClient();
            c.Mat = txtmat.Text;
            glos.deleteClient(c,txtmat.Text);
            glos.chargerclient(dataGridView1);
        }

       

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            ClassClient c = new ClassClient();
            c.Mat = txtmat.Text;
            glos.deleteClient(c, txtmat.Text);
            glos.chargerclient(dataGridView1);
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            ClassClient c = new ClassClient();
            c.Mat = txtmat.Text;
            glos.deleteClient(c, txtmat.Text);
            glos.chargerclient(dataGridView1);
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
                        byte[] images = null;
                        FileStream stream = new FileStream(imglocation, FileMode.Open, FileAccess.Read);
                        BinaryReader brs = new BinaryReader(stream);
                        images = brs.ReadBytes((int)stream.Length);




                        ClassClient cl = new ClassClient();
                        cl.Mat = txtmat.Text;
                        cl.Nom = txtnom.Text;
                        cl.Postnom = txtpost.Text;
                        cl.Prenom = txtprenom.Text;
                        cl.Refcat = comboCat.Text;
                        cl.Reseaux = comboR.Text;
                        cl.Sexe = comboSexe.Text;
                        cl.Tel = txtphone.Text;
                        cl.Photo = convertImagePicEdit(pictureEdit1);
                        cl.Qr = convertImageTobyte(pictureBox2);
                        cl.Affiliation = txtAffilier.Text;
                        cl.Adresse = txtadres.Text;
                        cl.Etatcivil = comboEtat.Text;
                        cl.Age = int.Parse(txtage.Text);

                        y = glos.countARB(comboR.Text);
                        glos.InsertClient(cl);
                        glos.chargerDatacl(dataGridView1);
                        glos.chargerMat(comboR);
                        

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
            this.Show();

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            try
            {
                ClassClient c = new ClassClient();
                c.Mat = txtmat.Text;
                glos.deleteClient(c, txtmat.Text);
                glos.chargerclient(dataGridView1);

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
            try{
                MessageBox.Show("Cette personne a deja  " + glos.countARB(comboR.Text) + " a son arbre!");
                

            }catch(Exception ex){
                MessageBox.Show(ex.Message);
            }
            
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            this.ShowDialog();
        }

        private void searchControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataView DV = new DataView(dbdataset);
            DV.RowFilter = string.Format("matr_client LIKE '%{0}%'", searchControl1.Text);
            dataGridView1.DataSource = DV;
        }

        private void bindingNavigator1_RefreshItems_1(object sender, EventArgs e)
        {

        }

        private void searchControl1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            glos.seachcl(searchControl1.Text, dataGridView1);
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    listeArbre cl = new listeArbre();
            //    cl.DataSource = StormSoft.classe.glossaire.GetInstance().sortieArbreCl(comboR.Text);
            //    ReportPrintTool printTool = new ReportPrintTool(cl);
            //    printTool.ShowPreviewDialog();

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
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
            DataView DV = new DataView(dbdataset);
            DV.RowFilter = string.Format("matr_client LIKE '%{0}%'", textBox1.Text);
            dataGridView1.DataSource = DV;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        
       
        
    }
}