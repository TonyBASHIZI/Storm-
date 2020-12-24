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
    public partial class formUsers : Form
    {
        glossaire glos = new glossaire();

        public formUsers()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ClassUsers use = new ClassUsers();
            use.Username = textBox1.Text;
            use.Password = textBox2.Text;
            use.Nom = textBox3.Text;
            use.Email = textBox4.Text;
            glos.insertUsers(use);


        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (textBox5.Text == "")
            {
                MessageBox.Show("Remplissez le champ pour ID!!");
            }
            else
            {
                glos.deleteuser(textBox5.Text);
            }
            
        }

        private void formUsers_Load(object sender, EventArgs e)
        {
            //glos.chargerusers(dataGridView1);
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            try
            {
                users cl = new users();
                cl.DataSource = StormSoft.classe.glossaire.GetInstance().sortieUsers();
                ReportPrintTool printTool = new ReportPrintTool(cl);
                printTool.ShowPreviewDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            panel2.BackColor = Color.Red;

        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            panel2.BackColor = Color.Transparent;
        }

        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            panel3.BackColor = Color.Red;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            panel2.BackColor = Color.Transparent;
        }

        private void pictureBox4_MouseEnter(object sender, EventArgs e)
        {
            panel2.BackColor = Color.Red;
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            panel2.BackColor = Color.Transparent;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
