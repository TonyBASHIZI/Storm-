
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
    public partial class formConsommation : Form
    {
        private int inser = 0;

        public int Inser
        {
            get { return inser; }
            set { inser = value; }
        }
        private int update = 0;

        public int Update1
        {
            get { return update; }
            set { update = value; }
        }
        int timcp = 0;
        glossaire glos = new glossaire();
        public formConsommation()
        {
            InitializeComponent();
        }
        public void initialiseChap()
        {
            comboMat.Text = "";
            txtfc.Text = "";
            txtdo.Text = "";
            txtPource.Text = "";
            txtpource0.Text = "";
            txtprix.Text = "";
            txtrs.Text = "";
            txtrs0.Text = "";
            txttel.Text = "";
            txttel0.Text = "";
            txtqte.Text = "";
        }


        private void formConsommation_Load(object sender, EventArgs e)
        {
            
            glos.chargerMat(comboMat);
            labelCl.Text = ""+glos.countCl();
            label12.Text = glos.countConso();
            glos.chargerConso(dataGridView1,dateEdit1.Text);

            
        }

       
      
       

        private void comboMat_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                
                txtrs.Text = glos.findReseax(comboMat.Text);
                txttel.Text = glos.findNumber(txtrs.Text);
                txtrs0.Text = glos.findReseax(txtrs.Text);
                txttel0.Text = glos.findNumber(txtrs0.Text);
                labID.Text = glos.findIDcons(comboMat.Text);

                double pourcen1 = Double.Parse(glos.chargerPourcentN1());
                double pourcen0 = Double.Parse(glos.chargerPourcentN0());
                
           
                    double q = Double.Parse(txtqte.Text);
                    double p = Double.Parse(txtprix.Text);
                     
                    txtfc.Text = "" + q * p;
                    txtdo.Text = "" + Double.Parse(txtfc.Text) / 1600;
                    double p1 = Double.Parse(txtfc.Text);

                    txtPource.Text = "" + p1 * pourcen1 / 100;
                    txtpource0.Text = "" + p1 * pourcen0 / 100;

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            try
            {
                double q = Double.Parse(txtqte.Text);
                double p = Double.Parse(txtprix.Text);

                txtfc.Text = "" + q * p;
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
           


        }

      

        private void formConsommation_Leave(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timcp = 0 ;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
           
        }
        public void calculConsommation(int i)
        {
            try
            {
                comboMat.Text = dataGridView1.Rows[i].Cells["mat"].FormattedValue.ToString();
                txtqte.Text = dataGridView1.Rows[i].Cells["qte"].FormattedValue.ToString();
                txtprix.Text = dataGridView1.Rows[i].Cells["prix"].FormattedValue.ToString();
                try
                {

                    txtrs.Text = glos.findReseax(comboMat.Text);
                    txttel.Text = glos.findNumber(txtrs.Text);
                    
                    labID.Text = glos.findIDcons(comboMat.Text);
                    if (txtrs0.Text == "")
                    {

                    }
                    else
                    {
                        if (glos.findReseax(txtrs.Text) == "")
                        {

                        }
                        else
                        {
                            txtrs0.Text = glos.findReseax(txtrs.Text);
                            txttel0.Text = glos.findNumber(txtrs0.Text);
                        }
                        
                    }
                    
                    double pourcen1 = Double.Parse(glos.chargerPourcentN1());
                    double pourcen0 = Double.Parse(glos.chargerPourcentN0());

                    double q = Double.Parse(txtqte.Text);
                    double p = Double.Parse(txtprix.Text);
                    double taux = Double.Parse(txttaux.Text);

                    txtfc.Text = "" + q * p;
                    txtdo.Text = "" + Double.Parse(txtfc.Text) / taux;
                    double p1 = Double.Parse(txtfc.Text);

                    txtPource.Text = "" + p1 * pourcen1 / taux;
                    txtpource0.Text = "" + p1 * pourcen0 / taux ;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void inserB()
        {
             
            try
            {
                for (int x = 0; x < dataGridView1.Rows.Count; x++)
                {
                    calculConsommation(x);
                    ClassBonus bo = new ClassBonus();
                    bo.Id = int.Parse(labID.Text);
                    bo.Ref_cl = comboMat.Text;
                    bo.Qte = Double.Parse(txtqte.Text);
                    bo.Prix = Double.Parse(txtprix.Text);
                    bo.Reseaux = txtrs.Text;
                    bo.Reseaux0 = txtrs0.Text;
                    bo.Montantfc = Double.Parse(txtfc.Text);
                    bo.Montantdo = Double.Parse(txtdo.Text);
                    bo.Pourcent = Double.Parse(txtPource.Text);
                    bo.Pourcent0 = Double.Parse(txtpource0.Text);
                    bo.Tel = txttel.Text;
                    bo.Tel0 = txttel0.Text;
                    glos.InsertBonus(bo,inser,update,dateEdit1.Text);

                    initialiseChap();
                }
                MessageBox.Show("Le nombre des insertions " + inser + " et des modifications " + update);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            inserB();
     
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            initialiseChap();
        }

       

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            try
            {
                ClassBonus bo = new ClassBonus();
                bo.Ref_cl = comboMat.Text;
                bo.Qte = Double.Parse(txtqte.Text);
                bo.Prix = Double.Parse(txtprix.Text);
                bo.Reseaux = txtrs.Text;
                bo.Reseaux0 = txtrs0.Text;
                bo.Montantfc = Double.Parse(txtfc.Text);
                bo.Montantdo = Double.Parse(txtdo.Text);
                bo.Pourcent = Double.Parse(txtPource.Text);
                bo.Pourcent0 = Double.Parse(txtpource0.Text);
                bo.Tel = txttel.Text;
                bo.Tel0 = txttel0.Text;
                glos.InsertBonus(bo,inser,update,dateEdit1.Text);
                initialiseChap();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                comboMat.Text = dataGridView1.CurrentRow.Cells["mat"].FormattedValue.ToString();
                txtqte.Text = dataGridView1.CurrentRow.Cells["qte"].FormattedValue.ToString();
                txtprix.Text = dataGridView1.CurrentRow.Cells["prix"].FormattedValue.ToString();
                try
                {

                    txtrs.Text = glos.findReseax(comboMat.Text);
                    txttel.Text = glos.findNumber(txtrs.Text);
                    txtrs0.Text = glos.findReseax(txtrs.Text);
                    txttel0.Text = glos.findNumber(txtrs0.Text);
                    labID.Text = glos.findIDcons(comboMat.Text);

                    double pourcen1 = Double.Parse(glos.chargerPourcentN1());
                    double pourcen0 = Double.Parse(glos.chargerPourcentN0());


                    double q = Double.Parse(txtqte.Text);
                    double p = Double.Parse(txtprix.Text);

                    txtfc.Text = "" + q * p;
                    txtdo.Text = "" + Double.Parse(txtfc.Text) / 1600;
                    double p1 = Double.Parse(txtfc.Text);

                    txtPource.Text = "" + p1 * pourcen1 / 100;
                    txtpource0.Text = "" + p1 * pourcen0 / 100;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
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

        private void bindingNavigator1_RefreshItems(object sender, EventArgs e)
        {

        }

        private void dateEdit1_EditValueChanged(object sender, EventArgs e)
        {
            glos.chargerConso(dataGridView1, dateEdit1.Text);
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            try
            {
                consommation cl = new consommation();
                cl.DataSource = StormSoft.classe.glossaire.GetInstance().sortieConsommation();
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
                bonus cl = new bonus();
                cl.DataSource = StormSoft.classe.glossaire.GetInstance().sortieBonus();
                ReportPrintTool printTool = new ReportPrintTool(cl);
                printTool.ShowPreviewDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
