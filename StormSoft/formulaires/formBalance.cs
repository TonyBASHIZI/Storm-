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
    public partial class formBalance : Form
    {
        public formBalance()
        {
            InitializeComponent();
        }

        private void formBalance_Load(object sender, EventArgs e)
        {
            lbBalance.Text =  glossaire.GetInstance().getBalance();
            glossaire.GetInstance().GetDatas(gridControl1, "*", "detail_carte");
        }
    }
}
