using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Agenda_Etec
{
    public partial class frmMensagem : Form
    {
        string msg;
        public frmMensagem(string mg)
        {
            InitializeComponent();
            msg = mg;
        }

        public void Fechar()
        {
            timer1.Interval = 5000;
            timer1.Start();
        }

        private void frmMensagem_Load(object sender, EventArgs e)
        {
            Fechar();
            if(msg.Length > 85)
            {
                Font myfont = new Font("Lucida Bright", 10.0f);
                textBox1.Font = myfont;
            }
            textBox1.Text = msg;
            textBox2.Select();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            SendKeys.Send("{ESC}");
            timer1.Stop();
            this.Close();
        }

        private void frmMensagem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 || e.KeyChar == 27)
            {
                this.Close();
            }
        }
    }
}
