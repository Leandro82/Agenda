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
    public partial class frmDecisao : Form
    {
        string aviso, data, acao;
        int codLog;

        public frmDecisao(string av, string dt, int cod)
        {
            InitializeComponent();
            aviso = av;
            data = dt;
            codLog = cod;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmDecisao_Load(object sender, EventArgs e)
        {
            label1.Text = aviso;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            acao = "Feriado";
            frmCadEvento frm = new frmCadEvento(data, codLog, acao);
            frm.ShowDialog();
            this.Close();
        }
    }
}
