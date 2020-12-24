using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;

namespace StormSoft.formulaires
{
    public partial class formPrinciapal : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private newClient childForm = null;
        private int childFormNumber = 1;

        public formPrinciapal(string str_value)
        {
            InitializeComponent();
            donnerAcces(str_value);
            LblConnectedUser.Caption = str_value;
            use.Text = str_value;
        }
        public formPrinciapal()
        {
            InitializeComponent();
        }
        private void ShowNewForm(object sender, EventArgs e)
        {
            formPrinciapal childForm = new formPrinciapal();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }
        public void donnerAcces(string niveau)
        {
            if (niveau == "Administrateur")
            {
                barButtonItem3.Enabled = true;
                barButtonItem9.Enabled = true;
                barButtonItem10.Enabled = true;
                barButtonItem12.Enabled = true;
                barButtonItem15.Enabled = true;
                barButtonItem16.Enabled = true;
                barButtonItem20.Enabled = true;
                barButtonItem1.Enabled = true;
                barButtonItem11.Enabled = true;
                barButtonItem2.Enabled = true;
                barButtonItem14.Enabled = true;
                barButtonItem5.Enabled = true;
                barButtonItem6.Enabled = true;
                barButtonItem7.Enabled = true;
                barButtonItem8.Enabled = true;
                barButtonItem21.Enabled = true;
                barButtonItem24.Enabled = true;
                barButtonItem24.Enabled = true;
                barButtonItem23.Enabled = true;


            }
           
            else
            {
                
                barButtonItem9.Enabled = false;
                barButtonItem10.Enabled = false;
                barButtonItem12.Enabled = false;
                barButtonItem15.Enabled = false;
                barButtonItem16.Enabled = false;
                barButtonItem20.Enabled = true;
                barButtonItem1.Enabled = false;
                barButtonItem11.Enabled = false;
                barButtonItem22.Enabled = false;
                barButtonItem19.Enabled = false;
                barButtonItem5.Enabled = false;
                barButtonItem6.Enabled = false;
                barButtonItem21.Enabled = false;
                barButtonItem24.Enabled = false;
                barButtonItem17.Enabled = false;
                barButtonItem25.Enabled = false; 
                barButtonItem23.Enabled = false;
            }


        }
        private void formPrinciapal_Load(object sender, EventArgs e)
        {
            string link = use.Text;

            formAccueil p = new formAccueil();
            p.MdiParent = this;
            p.Show();
            
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            newClient n = new newClient();
            n.MdiParent = this;
            n.Show();
               
            
        }

        private void ribbon_Click(object sender, EventArgs e)
        {
           
        }

        private void formPrinciapal_FormClosing(object sender, FormClosingEventArgs e)
        {
            var result = MessageBox.Show("Voulez-vous quitter l'application?", "Exit", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                Application.Exit();
            }
            else
            {
                e.Cancel = true;
            }
           // Application.Exit();
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {

            formNewParenage c = new formNewParenage();
            
            c.ShowDialog();

        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            formConsommation n = new formConsommation();
            n.MdiParent = this;
            n.Show();
        }

        private void barButtonItem10_ItemClick(object sender, ItemClickEventArgs e)
        {
            loginAdmin us = new loginAdmin();
            us.ShowDialog();
        }

        private void barButtonItem13_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Hide();
            authentification us = new authentification();
            us.ShowDialog();
        }

        private void barButtonItem12_ItemClick(object sender, ItemClickEventArgs e)
        {
            loginAdmin pp = new loginAdmin();
            pp.ShowDialog();

        }

        private void barButtonItem9_ItemClick(object sender, ItemClickEventArgs e)
        {
            sms n = new sms();
            n.MdiParent = this;
            n.Show();
        }

        private void barButtonItem6_ItemClick(object sender, ItemClickEventArgs e)
        {
            formRecherche rech = new formRecherche();
            rech.MdiParent = this;
            rech.Show();
        }

        private void barButtonItem15_ItemClick(object sender, ItemClickEventArgs e)
        {
            loginAdmin pp = new loginAdmin();
            pp.ShowDialog();
        }

        private void barButtonItem16_ItemClick(object sender, ItemClickEventArgs e)
        {
            partennaireForm p = new partennaireForm();
            p.MdiParent = this;
            p.Show();
        }

        private void barButtonItem11_ItemClick(object sender, ItemClickEventArgs e)
        {
            formExport p = new formExport();
            p.MdiParent = this;
            p.Show();
        }

        private void barButtonItem17_ItemClick(object sender, ItemClickEventArgs e)
        {
            accueil p = new accueil();
            p.MdiParent = this;
            p.Show();
        }

        private void barButtonItem18_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Hide();
            authentification us = new authentification();
            us.ShowDialog();
        }

        private void barButtonItem19_ItemClick(object sender, ItemClickEventArgs e)
        {
            formBusiness p = new formBusiness();
            p.MdiParent = this;
            p.Show();
        }

        private void barButtonItem20_ItemClick(object sender, ItemClickEventArgs e)
        {
            string link = use.Text;

            stationForm p = new stationForm(use.Text);
            p.MdiParent = this;
            p.Show();

        }

        private void barButtonItem21_ItemClick(object sender, ItemClickEventArgs e)
        {
            formExportSuivis p = new formExportSuivis();
            p.MdiParent = this;
            p.Show();

        }

        private void barButtonItem22_ItemClick(object sender, ItemClickEventArgs e)
        {
            formBalance p = new formBalance();
            p.MdiParent = this;
            p.Show();
        }

        private void barButtonItem23_ItemClick(object sender, ItemClickEventArgs e)
        {
            string link = use.Text;

            stationForm p = new stationForm(use.Text);
            p.MdiParent = this;
            p.Show();
        }

        private void barButtonItem24_ItemClick(object sender, ItemClickEventArgs e)
        {
            loginAdmin pp = new loginAdmin();
            pp.ShowDialog();
           
        }

        private void barButtonItem25_ItemClick(object sender, ItemClickEventArgs e)
        {
            formRetrait p = new formRetrait();
            p.MdiParent = this;
            p.Show();
        }

        private void barButtonItem27_ItemClick(object sender, ItemClickEventArgs e)
        {
            string link = use.Text;

            formGerantBusiness p = new formGerantBusiness(use.Text);
            p.MdiParent = this;
            p.Show();
        }
    }
}