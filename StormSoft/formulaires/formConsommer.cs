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
    public partial class formConsommer : Form
    {
        glossaire glos = new glossaire();
        public formConsommer()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void formConsommer_Load(object sender, EventArgs e)
        {
            glos.GetDatas(gridControl1, "id,ref_client,qte,type_cons,date_cons,username", "t_consommation");
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            txtMat.Text = gridView1.GetFocusedRowCellValue("ref_client").ToString();
            dates.Text = gridView1.GetFocusedRowCellValue("date_cons").ToString();
            txtqte.Text = gridView1.GetFocusedRowCellValue("qte").ToString();
            txtType.Text = gridView1.GetFocusedRowCellValue("type_cons").ToString();
            lbID.Text = gridView1.GetFocusedRowCellValue("id").ToString();
            lbStation.Text = gridView1.GetFocusedRowCellValue("username").ToString();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ClassConsommation cons = new ClassConsommation();
            cons.Id =int.Parse(lbID.Text);
            cons.Mat = txtMat.Text;
            cons.Qte = txtqte.Text;
            cons.Typecons = txtType.Text;
            cons.Stat = lbStation.Text;
            glos.Deleteconso(cons);
            glos.GetDatas(gridControl1, "id,ref_client,type_cons,qte,date_cons,username", "t_consommation");
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            txtMat.Text = "";
            txtqte.Text = "";
            txtType.Text = "";
        }

        private void txtqte_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < txtqte.Text.Length; i++)
            {
                if (!char.IsDigit(txtqte.Text, i))
                {
                    MessageBox.Show("Donnée incorrecte ce champ ne peut que prendre les chiffres entiers!!");
                    txtqte.Text = "";
                }
                else
                {

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            formPopup pop = new formPopup(txtMat.Text,lbStation.Text,txtqte.Text,txtType.Text);
            pop.ShowDialog();
        }
    }
}
