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
using System.Data.OleDb;
using ExcelLibrary.CompoundDocumentFormat;
using ExcelLibrary.SpreadSheet;

namespace StormSoft.formulaires
{
    public partial class formExport : Form
    {
        //glossaire glos = new glossaire();
        
        public formExport()
        {
            InitializeComponent();
        }



        private void simpleButton1_Click(object sender, EventArgs e)
        {
            glossaire.GetInstance().chargerConsoStationDate(dataGridView1,comboStation1.Text, datecons.Text);

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            
             glossaire.GetInstance().chargerConsoStationDateInterval(dataGridView1, combostation2.Text, datedebut.Text, datefin.Text);
            

        }

        private void formExport_Load(object sender, EventArgs e)
        {
            //glossaire.GetInstance().getUser(comboStation1);
            //glossaire.GetInstance().getUser(combostation2);

        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Voulez-vous vraiment  exporter Excel", "Fichier Excel", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                glossaire.GetInstance().ExportationExcel(dataGridView1);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.textBox1.Text = openFileDialog1.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string PathConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + textBox1.Text + ";Extended Properties=\"Excel 8.0;HDR=Yes;\";";
            OleDbConnection conn = new OleDbConnection(PathConn);
            OleDbDataAdapter myDataAdapter = new OleDbDataAdapter("Select * from [" + textBox2.Text + "$]", conn);
            DataTable dt = new DataTable();
            myDataAdapter.Fill(dt);
            dataGridView2.DataSource = dt;

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            string matricule = "";
            string typeconso = "";
            string qte = "";
            string prix = "";
            for(int x = 0; x<dataGridView2.Rows.Count; x++)
            {
                matricule = dataGridView2.Rows[x].Cells[0].Value.ToString();
                typeconso = dataGridView2.Rows[x].Cells[1].Value.ToString();
                qte = dataGridView2.Rows[x].Cells[3].Value.ToString();
                prix = dataGridView2.Rows[x].Cells[4].Value.ToString();
               

            }
        }
    }
}
