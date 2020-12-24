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
    public partial class fromPaie : Form
    {
        glossaire glos = new glossaire();
        public fromPaie()
        {
            InitializeComponent();
        }

        private void fromPaie_Load(object sender, EventArgs e)
        {
            glos.chargerPaie1(data1);
            glos.chargerPaie2(data2);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            glos.ExportationExcel(data1);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            glos.ExportationExcel(data2);
        }
    }
}
