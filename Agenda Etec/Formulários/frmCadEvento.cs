using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace Agenda_Etec
{
    public partial class frmCadEvento : Form
    {
        Agenda ag = new Agenda();
        ConectaAgenda ca = new ConectaAgenda();
        ConectaUsuario co = new ConectaUsuario();
        Usuario us = new Usuario();
        string data, nome, acao, dtEvAux;
        DateTime dtEv;
        int codEv, codRespCad, codLog;

        private void frmCadEvento_Load(object sender, EventArgs e)
        {
            label5.Text = data;
        }

        public frmCadEvento(string dt, int cod, string ac)
        {
            InitializeComponent();
            data = dt;
            codLog = cod;
            acao = ac;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            maskedTextBox1.Clear();
            textBox1.Clear();
            textBox2.Clear();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (!maskedTextBox1.MaskCompleted)
            {
                string msg = "Informe o horário do evento!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
                maskedTextBox1.Focus();
            }
            else if (textBox1.Text == "")
            {
                string msg = "Informe o local do evento!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
                textBox1.Focus();
            }
            else if (textBox2.Text == "")
            {
                string msg = "Informe que evento irá acontecer!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
                textBox2.Focus();
            }
            else
            {
                dtEv = DateTime.ParseExact(label5.Text.ToString(), "dd/MM/yyyy", CultureInfo.CurrentCulture);
                ag.DataCadastro = co.BuscaDataServidor();
                ag.HoraCadastro = co.BuscaHoraServidor().ToString();
                ag.DataEvento = Convert.ToString(dtEv);
                ag.HoraEvento = maskedTextBox1.Text;
                ag.Local = textBox1.Text;
                ag.Evento = textBox2.Text;
                ag.RepCadastroEvento = codLog;
                ca.Cadastro(ag);
                if (Application.OpenForms.OfType<frmCalendario>().Count() == 1 && acao == "Feriado")
                {
                    string msg = "Evento cadastrado, porém, o Calendário está aberto. Devido ao feriado é preciso fechá-lo e abri-lo novamente para ver o evento cadastrado!!";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                    maskedTextBox1.Clear();
                    textBox1.Clear();
                    textBox2.Clear();
                }
                else
                {
                    ((frmCalendario)this.Owner).Feriados();
                    string msg = "Evento cadastrado!!";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                    maskedTextBox1.Clear();
                    textBox1.Clear();
                    textBox2.Clear();
                }
            }
        }
    }
}
