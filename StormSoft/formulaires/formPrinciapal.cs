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
            label3.Text = str_value;
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

            }
            else if (niveau == "UtilisateurA")
            {
                barButtonItem3.Enabled = false;
                barButtonItem9.Enabled = false;
                barButtonItem10.Enabled = false;
                barButtonItem12.Enabled = false;
            }


        }
        private void formPrinciapal_Load(object sender, EventArgs e)
        {
            accueil ac = new accueil();
            ac.MdiParent = this;
            ac.Show();


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
            Application.Exit();
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
            formUsers us = new formUsers();
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
            formPrixPourcentage pp = new formPrixPourcentage();
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
    }
}