using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StormSoft.classe;
using ThoughtWorks.QRCode.Codec;
using DevExpress.XtraEditors;
namespace StormSoft.formulaires
{
    public partial class formEnregistrement : Form
    {
        glossaire glos = new glossaire();
        string imglocation = "";
        DataTable dbdataset;
        public formEnregistrement()
        {
            InitializeComponent();
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
        public string getmat()
        {
            Random rnd = new Random();
            int x = rnd.Next(1, 5);
            string mat;

            DateTime da = DateTime.Now;

            string lastid = "" + glos.countCl();

            mat = "M0-" + da.Month + "" + da.Year + "" + "-" + lastid + ""+x;

            return mat;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bindingNavigator1_RefreshItems(object sender, EventArgs e)
        {

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
       
        

        private void pictureBox3_Click(object sender, EventArgs e)
        {
           inserEng();
           
        }
        //public string getmat()
        //{
        //    Random rnd = new Random();
        //    int x = rnd.Next(1, 1500);
        //    string mat;

        //    DateTime da = DateTime.Now;

        //    string lastid = "" + glos.countCl();

        //    mat = "19 NK 01 " + lastid + "" + x;

        //    return mat;
        //}
        public void getMat2()
        {
            Random rnd = new Random();
            int x = rnd.Next(1, 500);
            string mat2;

            DateTime da = DateTime.Now;

            string lastid = "" + glos.countCl();
            txtmat.Text = "PR-00-T-" + da.Day + "" + da.Year + "-" + lastid;
            comboR.Text = "PR-00-T-";
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


            //this.Show();

        }

        private void formEnregistrement_Load(object sender, EventArgs e)
        {

            glos.GetDatasEnregis(gridControl1, "*", "Enregistrement");
            txtmat.Text = getmat();

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
        public void call()
        {
            txtmat.Text = getmat();
            txtID.Text = gridView1.GetFocusedRowCellValue("idCarte").ToString();
            txtadres.Text = gridView1.GetFocusedRowCellValue("adresse").ToString();
            txtprenom.Text = gridView1.GetFocusedRowCellValue("prenom").ToString();

            txtpost.Text = gridView1.GetFocusedRowCellValue("postnom").ToString();
            txtnom.Text =  gridView1.GetFocusedRowCellValue("nom").ToString();
            comboSexe.Text = gridView1.GetFocusedRowCellValue("sexe").ToString();
            txtAffilier.Text =  gridView1.GetFocusedRowCellValue("affiliation").ToString();
            txtphone.Text = gridView1.GetFocusedRowCellValue("telephone").ToString();
            comboR.Text =  gridView1.GetFocusedRowCellValue("reseau").ToString();
            txtpasse.Text =  gridView1.GetFocusedRowCellValue("motdepasse").ToString();
            txtcate.Text = gridView1.GetFocusedRowCellValue("categorie").ToString();
            //comboEtat.Text = dataGridView1.Rows[i].Cells["etatcivil"].FormattedValue.ToString();
            comboCat.Text =  gridView1.GetFocusedRowCellValue("categorie").ToString();
            label5.Text = gridView1.GetFocusedRowCellValue("id").ToString();


            QRCode(pictureBox2, txtmat.Text);

            
        }
        public void inserEng()
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
                    ClassClient cl = new ClassClient();
                    cl.Mat = txtmat.Text;
                    cl.Nom = txtnom.Text;
                    cl.Postnom = txtpost.Text;
                    cl.Prenom = txtprenom.Text;
                    //cl.Refcat = comboCat.Text;
                    cl.Reseaux = comboR.Text;
                    cl.Sexe = comboSexe.Text;
                    cl.Tel = txtphone.Text;
                    //cl.Photo = convertImagePicEdit(pictureEdit1);
                    cl.Qr = convertImageTobyte(pictureBox2);
                    cl.Affiliation = txtAffilier.Text;
                    cl.Adresse = txtadres.Text;
                    cl.Etatcivil = comboEtat.Text;
                    //cl.Age = int.Parse(txtage.Text);
                    cl.Idcarte = txtID.Text;

                    //y = glos.countARB(comboR.Text);
                    glos.InsertClientEnregi(cl);
                    glos.GetDatasEnregis(gridControl1, "*", "Enregistrement");
                    initialise();



                }
                
                }

                // MessageBox.Show("Le nombre des insertions  et des modifications avec succes ");
                //glos.speakeer("FIN DE PROCESSUS");

            
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
       
        private void gridenreg_DoubleClick(object sender, EventArgs e)
        {


           

        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            QRCode(pictureBox2, txtmat.Text);
        }

        //private void simpleButton2_Click(object sender, EventArgs e)
        //{
        //    OpenFileDialog dialog = new OpenFileDialog();
        //    dialog.Filter = "png files(*.png)|*png|jpg files(*.jpg)|*jpg|All files(*.*)|*.*  ";
        //    if (dialog.ShowDialog() == DialogResult.OK)
        //    {
        //        imglocation = dialog.FileName.ToString();
        //        pictureEdit1.Image = Image.FromFile(imglocation);


        //    }
        //}

        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            panel3.BackColor = Color.Red;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            panel3.BackColor = Color.Transparent;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            call();
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            getmat();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            getMat2();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                ClassEnregistrement c = new ClassEnregistrement();
                c.Mat = txtmat.Text;
                c.Ib = int.Parse(label5.Text);
                glos.deleteEnregistrement(c, txtphone.Text);
                glos.GetDatasEnregis(gridControl1, "*", "Enregistrement");
                initialise();
                txtmat.Text = getmat();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            panel5.BackColor = Color.Red;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            panel5.BackColor = Color.Transparent;
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void panel5_MouseLeave(object sender, EventArgs e)
        {
            panel5.BackColor = Color.Transparent;
        }
    }
}
