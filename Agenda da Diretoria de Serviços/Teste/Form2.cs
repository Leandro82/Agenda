using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Teste
{
    public partial class Form2 : Form
    {

        string privilegio;

        public Form2()
        {
            InitializeComponent();
        }

        frmMenu cad = new frmMenu();

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text != textBox4.Text)
            {
                label5.Text = "Senhas não batem";
                label5.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                label5.Text = "OK";
                label5.ForeColor = System.Drawing.Color.Green;
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            label5.Text = "";
        }

        public void Atualizar()
        {
            textBox1.Text = AuxClas.usuario;
            textBox2.Text = AuxClas.login;
            privilegio = AuxClas.privilegio;
        }

        public void Fechar()
        {
            timer1.Interval = 2000;
            timer1.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Usuario us = new Usuario();
            ConectaUsuario co = new ConectaUsuario();

            us.Nome = textBox1.Text;
            us.Login = textBox2.Text;
            us.Acesso = "Ok";
            us.Senha = textBox4.Text;
            co.cadastro(us);
            Fechar();
            MessageBox.Show("Senha alterada");

            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
            {
                Fechar();
                MessageBox.Show("Por favor, preencher todos os campos");
            }
            else
            {
                AuxClas.auxiliar = "aux";
                AuxClas.usuario = textBox1.Text;
                AuxClas.privilegio = privilegio;


                cad.Show();
                cad.Atualizar();
                this.Hide();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            SendKeys.Send("{ESC}");
            timer1.Stop();
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Usuario us = new Usuario();
                ConectaUsuario co = new ConectaUsuario();

                us.Nome = textBox1.Text;
                us.Login = textBox2.Text;
                us.Acesso = "Ok";
                us.Senha = textBox4.Text;
                co.cadastro(us);
                Fechar();
                MessageBox.Show("Senha alterada");

                if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
                {
                    Fechar();
                    MessageBox.Show("Por favor, preencher todos os campos");
                }
                else
                {
                    AuxClas.auxiliar = "aux";
                    AuxClas.usuario = textBox1.Text;
                    AuxClas.privilegio = privilegio;


                    cad.Show();
                    cad.Atualizar();
                    this.Hide();
                }
            }
        }
    }
}
