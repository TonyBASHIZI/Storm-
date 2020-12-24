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
    public partial class formCommisssion : Form
    {
        glossaire glos = new glossaire();
        public formCommisssion()
        {
            InitializeComponent();
        }

        private void formCommisssion_Load(object sender, EventArgs e)
        {
            lbcomm.Text = glos.findCommission();
            label4.Text = "" + int.Parse(lbcomm.Text) * 100 / 100;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtcommi.Text == "")
            {
                MessageBox.Show("Le champ de pourcentage doit etre rempli!!");
            }
            else
            {
                ClassCommission c = new ClassCommission();
                c.Id = 1;
                c.Pourcentage = txtcommi.Text;
                glos.modifierComm(c);
                lbcomm.Text = glos.findCommission();
                label4.Text = "" + int.Parse(lbcomm.Text) * 100 / 100;

            }
            
        }

        private void txtcommi_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < txtcommi.Text.Length; i++)
            {
                if (!char.IsDigit(txtcommi.Text, i))
                {
                    MessageBox.Show("Donnée incorrecte ce champ ne peut que prendre les chiffres entiers!!");
                    txtcommi.Text = "";
                }
                else
                {

                }
            }
        }
    }
}
