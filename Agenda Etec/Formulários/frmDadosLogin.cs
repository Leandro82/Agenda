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
    public partial class frmDadosLogin : Form
    {
        int codUs;
        string nome, login;
        Usuario us = new Usuario();
        ConectaUsuario cs = new ConectaUsuario();
        public frmDadosLogin(int cd, string nm, string lg)
        {
            InitializeComponent();
            codUs = cd;
            nome = nm;
            login = lg;
        }

        private void frmDadosLogin_Load(object sender, EventArgs e)
        {
            textBox1.Text = nome;
            textBox2.Text = login;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                string msg = "Informe um Login!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else if (textBox3.Text == "")
            {
                string msg = "Informe uma Senha!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else
            {
                us.Codigo = codUs;
                us.Login = textBox2.Text;
                us.Senha = textBox3.Text;
                us.Acesso = "Sim";
                cs.AtualizarAcesso(us);
                string msg = "Dados alterados!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
                ((frmPrincipal)this.Owner).PrimeiroAcesso(textBox2.Text, textBox3.Text);
                this.Close();
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (textBox2.Text == "")
                {
                    string msg = "Informe um Login!!";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else if (textBox3.Text == "")
                {
                    string msg = "Informe uma Senha!!";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                }
                else
                {
                    us.Codigo = codUs;
                    us.Login = textBox2.Text;
                    us.Senha = textBox3.Text;
                    us.Acesso = "Sim";
                    cs.AtualizarAcesso(us);
                    string msg = "Dados alterados!!";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                    ((frmPrincipal)this.Owner).PrimeiroAcesso(textBox2.Text, textBox3.Text);
                    this.Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox3.UseSystemPasswordChar == true)
                textBox3.UseSystemPasswordChar = false;
            else if (textBox3.UseSystemPasswordChar == false)
                textBox3.UseSystemPasswordChar = true;
        }
    }
}
