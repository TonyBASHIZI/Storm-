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
    public partial class formMotdePasse : Form
    {
        public formMotdePasse()
        {
            InitializeComponent();
        }

        private void formMotdePasse_Load(object sender, EventArgs e)
        {
            glossaire.GetInstance().GetDatas(gridControl1, "id,matricule,mot_de_passe", "detail_carte");
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            textBox1.Text = gridView1.GetFocusedRowCellValue("matricule").ToString();
            textBox2.Text = gridView1.GetFocusedRowCellValue("mot_de_passe").ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("Veuillez remplir tous les champs svp!!!");
            }
            else
            {
                glossaire.GetInstance().updatePasse(textBox1.Text, textBox3.Text);
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                glossaire.GetInstance().GetDatas(gridControl1, "id,matricule,mot_de_passe", "detail_carte");
            }
            

        }
    }
}
