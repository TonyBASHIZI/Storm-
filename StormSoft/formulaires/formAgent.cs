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

namespace StormSoft.formulaires
{
    public partial class V : Form
    {
        ClassAgent ag = new  ClassAgent();
        public V()
        {
            InitializeComponent();
        }
        public  string getMat()
        {
            Random rd = new Random();
            int x = rd.Next(1, 500);
            string cle = "MAG-AG-" + x;

            return cle;
        }
        public void initialise()
        {

            textBox1.Text = "" + getMat();
            lbId.Text = "--";
            textBox2.Text = "";
            textBox3.Text = "";
        }

        private void formAgent_Load(object sender, EventArgs e)
        {

            textBox1.Text = "" + getMat();
            glossaire.GetInstance().GetDatas(gridControl1, "id,ref_matricule,pwd,solde","devise");
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

            ag.Matricule = textBox1.Text;
            ag.Noms = textBox2.Text;
            ag.Mot_de_passe = textBox3.Text;

            if (ag.Matricule == "" || ag.Noms == "" || ag.Mot_de_passe == "")
            {
                MessageBox.Show("Veuillez remplir tous les champs svp !!");

            }
            else
            {
                glossaire.GetInstance().insertAgent(ag);
                initialise();
                glossaire.GetInstance().GetDatas(gridControl1, "id,ref_matricule,nom,pwd,solde", "devise");
                
            }

        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            
            textBox1.Text = gridView1.GetFocusedRowCellValue("ref_matricule").ToString();
            lbId.Text = gridView1.GetFocusedRowCellValue("id").ToString();
            //textBox3.Text = gridView1.GetFocusedRowCellValue("pwd").ToString();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            ClassAgent ag = new ClassAgent();

            if (lbId.Text == "--")
            {
                MessageBox.Show("Veuillez remplir tous les champs svp !!");

            }
            else
            {
               
                ag.Id = int.Parse(lbId.Text);
                glossaire.GetInstance().deleteAgent(ag);
                initialise();
                glossaire.GetInstance().GetDatas(gridControl1, "id,ref_matricule,nom,pwd,solde", "devise");
                

            }

        }
    }
}
