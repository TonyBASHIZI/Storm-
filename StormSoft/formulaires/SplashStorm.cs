using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraSplashScreen;

namespace StormSoft.formulaires
{
    public partial class SplashStorm : SplashScreen
    {
        int cmpt = 0;
        public SplashStorm()
        {
            InitializeComponent();
        }

        #region Overrides

        public override void ProcessCommand(Enum cmd, object arg)
        {
            base.ProcessCommand(cmd, arg);
        }

        #endregion

        public enum SplashScreenCommand
        {
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void SplashStorm_Load(object sender, EventArgs e)
        {
            
            

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            cmpt += 1;
            timer1.Start();
            timer1.Enabled = true;
            if (cmpt == 200)
            {
                timer1.Stop();
                timer1.Enabled = false;
                this.Hide();
                authentification auth = new authentification();
                auth.ShowDialog();

            }
        }
    }
}