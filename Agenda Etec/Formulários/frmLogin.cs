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
    public partial class frmLogin : Form
    {
        private bool Logado = false;
        private bool Acesso = false;
        ConectaUsuario co = new ConectaUsuario();
        Usuario us = new Usuario();
        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            label3.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AuxClas.login = textBox1.Text;
            AuxClas.senha = textBox2.Text;
            us.Login = textBox1.Text;
            us.Senha = textBox2.Text;
            bool result = co.VerificaLogin(us);
            bool acesso = co.VerificaAcesso(us);
            Logado = result;
            Acesso = acesso;
            if (result)
            {
                ((frmPrincipal)this.Owner).PosLogado(textBox1.Text, textBox2.Text);
                this.Close();
            }
            else
            {
                label3.Visible = true;
                label3.Text = "Usuário ou senha incorreto!!";
                textBox1.Text = "";
                textBox2.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                AuxClas.login = textBox1.Text;
                AuxClas.senha = textBox2.Text;
                us.Login = textBox1.Text;
                us.Senha = textBox2.Text;
                bool result = co.VerificaLogin(us);
                bool acesso = co.VerificaAcesso(us);
                Logado = result;
                Acesso = acesso;

                if (result)
                {
                    ((frmPrincipal)this.Owner).PosLogado(textBox1.Text, textBox2.Text);
                    this.Close();
                }
                else
                {
                    label3.Visible = true;
                    label3.Text = "Usuário ou senha incorreto!!";
                    textBox1.Text = "";
                    textBox2.Text = "";
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox2.UseSystemPasswordChar == true)
                textBox2.UseSystemPasswordChar = false;
            else if (textBox2.UseSystemPasswordChar == false)
                textBox2.UseSystemPasswordChar = true;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmEsqSenha form = new frmEsqSenha();
            form.ShowDialog();
        }
    }
}
