using GsmComm.PduConverter;
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
using System.Management;
using GsmComm.GsmCommunication;
using DevExpress.XtraGrid;

namespace StormSoft.formulaires
{
    public partial class sms : Form
    {
        //glossaire glos = new glossaire();
        private GsmCommMain comm;
        //private GsmCommMain comm;
        private DataTable dt = new DataTable();
        private delegate void SetTextCallback(string text);
        private int port = 17;
        private int baudRate;
        private int timeout;
        classe.sms msg = new classe.sms();

        public sms()
        {
            InitializeComponent();

        }
        public void sendAllSMS(GridControl grid)
        {
            
            if (cbOption.Text == "")
            {
                MessageBox.Show("VEUILLEZ PRECISER L'OPTION D'ENVOI ET LE MESSAGE SVP!!");
            }
            else if (cbOption.Text == "Specifier")
            {
                txtnumber.Visible = true;
                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    DataRow row = gridView1.GetDataRow(i);
                    string number = row["tel"].ToString();
                    //MessageBox.Show("" + number);
                    if (msg.sendlongMsg(number, SMStxt.Text) == true)
                    {
                        Output("Message envoyé a " + number + " \n");
                    }
                    else
                    {
                        MessageBox.Show("MESSAGE NON ENVOYE VERIFIER LE MODEM OU CONTACTER IT");
                    }
                }
                
            }
            else
            {
                MessageBox.Show("Selectonnionez une option svp!!");
                txtnumber.Visible = true;
            }
          
        }
        public string GetAllPorts(ComboBox combo)
        {
            string modems = "";

            try
            {

                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_POTSModem ");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    if ((string)queryObj["Status"] == "OK")
                    {

                        combo.Items.Add(queryObj["AttachedTo"] + " - " + System.Convert.ToString(queryObj["Description"]));
                    }
                    if (combo.Items.Count > 0)
                    {
                        combo.SelectedIndex = 0;
                    }
                }

                return modems;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur s'est produite lors de la requette", "Erreur de" + ex.Message);
                return "";
            }

        }
        private void Output(string text)
        {

            try
            {
                txtOutput.AppendText(text);
                
            }

            catch (Exception)
            {
                MessageBox.Show("Message envoie");

            }

        }
        public void SetData(int port, int baudRate, int timeout)
        {
            pubCon.port = port;
            pubCon.baudRate = baudRate;
            pubCon.timeout = timeout;
        }
        private bool EnterNewSettings()
        {
            int newPort;
            int newBaudRate;
            int newTimeout;

            try
            {
                newPort = int.Parse(portnumber.Text);
            }
            catch (Exception)
            {
                MessageBox.Show(this, "Invalid port number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                comboPort.Focus();
                return false;
            }

            try
            {
                newBaudRate = int.Parse(cboBaudRate.Text);
            }
            catch (Exception)
            {
                MessageBox.Show(this, "Invalid baud rate.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboBaudRate.Focus();
                return false;
            }

            try
            {
                newTimeout = int.Parse(cboTimeout.Text);
            }
            catch (Exception)
            {
                MessageBox.Show(this, "Invalid timeout value.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cboTimeout.Focus();
                return false;
            }

            SetData(newPort, newBaudRate, newTimeout);

            return true;
        }


        private void button4_Click(object sender, EventArgs e)
        {
            if (cbOption.Text =="" || SMStxt.Text =="")
            {
                MessageBox.Show("VEUILLEZ PRECISER L'OPTION D'ENVOI ET LE MESSAGE SVP!!");
            }
            else if (cbOption.Text == "Entrer numero")
            {
                txtnumber.Visible = true;
                
                if (msg.sendlongMsg(txtnumber.Text, SMStxt.Text) == true)
                {
                    Output("Message envoyé a "+txtnumber.Text+" \n");
                }
                else
                {
                    MessageBox.Show("MESSAGE NON ENVOYE VERIFIER LE MODEM OU CONTACTER IT");
                }
            }
            else
            {
                txtnumber.Visible = true;
            }
            

        }

        private void button1_Click(object sender, EventArgs e)
        {

            //MessageBox.Show(this, "Successfully connected to the phone.", "Connection setup", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void sms_Load(object sender, EventArgs e)
        {
            GetAllPorts(comboPort);
            txtnumber.Visible = true;
            glossaire.GetInstance().GetDatas(gridControl1, "*", "affichetelsms");
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!EnterNewSettings())
                return;

            Cursor.Current = Cursors.WaitCursor;
            pubCon.comm = new GsmCommMain(pubCon.port, pubCon.baudRate, pubCon.timeout);
            try
            {
                pubCon.comm.Open();
                while (!pubCon.comm.IsConnected())
                {
                    Cursor.Current = Cursors.Default;
                    if (MessageBox.Show(this, "No phone connected.", "Connection setup\n",
                        MessageBoxButtons.RetryCancel, MessageBoxIcon.Exclamation) == DialogResult.Cancel)
                    {
                        pubCon.comm.Close();
                        return;
                    }
                    Cursor.Current = Cursors.WaitCursor;
                }
                Output("Successfully connected to the phone.\n");
                MessageBox.Show(this, "Successfully connected to the phone.", "Connection setup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                pubCon.comm.Close();
                label_statut.BackColor = Color.Yellow;
                //ControlMsg();

                label_statut.Text = "Connecté";
               
            }
            catch (Exception ex)
            {
                Output("ERREUR : " + ex.Message);
                Output("");
                MessageBox.Show(this, "Connection error: " + ex.Message, "Connection setup", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbOption.Text == "Entrer numero")
            {
                txtnumber.Visible = true;
            }
            else
            {
                txtnumber.Visible = false;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Output("");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (SMStxt.Text == "" || searchControl1.Text == "")
            {
                MessageBox.Show("Le message est vide!");
            }
            else
            {
                DialogResult result = MessageBox.Show("Voulez-vous vraiment envoyer ce message aux clients choisis  ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    sendAllSMS(gridControl1);
                }

                else
                {
                    MessageBox.Show("Opération Annulée !");
                }
               

            }
            
        }

      
    }
}
