using StormSoft.classe;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StormSoft.formulaires
{
    public partial class formNewParenage : Form
    {
        glossaire glos = new glossaire();
        ClassArbre arb = new ClassArbre();
        public formNewParenage()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public string getCodeArbre(string mat)
        {
            string code;
           

            code = mat+ "-AR";

            return code;
        }

        private void formNewParenage_Load(object sender, EventArgs e)
        {
            glos.chargerMatAr(comboCode);
            glos.chargerArbre(dataGridView1);

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try {
                arb.CodeArbre = comboCode.Text;
                arb.DesignArbre = codereseaux.Text;
                glos.insertArbre(arb);
                glos.chargerArbre(dataGridView1);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            codereseaux.Text = getCodeArbre(comboCode.Text);
        }

        private void searchControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            glos.seachArbre(dataGridView1, searchControl1.Text);
        }

        private void searchControl1_Enter(object sender, EventArgs e)
        {
           
        }
    }
}

