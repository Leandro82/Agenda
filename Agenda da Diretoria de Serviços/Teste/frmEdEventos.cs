using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Teste
{
    public partial class frmEdEventos : Form
    {
        string aux;
        string privilegio;
        int seg, min;
        Variavel va = new Variavel();
        ConectaAgenda co = new ConectaAgenda();
        public void Fechar()
        {
            timer1.Interval = 2000;
            timer1.Start();
        }
        
        public frmEdEventos(int cod, string nome, string data, string dtCad, string local, string horario, string evento, string login, string priv)
        {
            InitializeComponent();
            textBox1.Text = nome;
            dateTimePicker1.Text = data;
            textBox3.Text = local;
            maskedTextBox1.Text = horario;
            textBox2.Text = evento;
            label5.Text = Convert.ToString(cod);
            aux = login;
            privilegio = priv;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (aux != textBox1.Text && privilegio == "")
            {
                Fechar();
                MessageBox.Show("Você não pode alterar um evento de " + textBox1.Text);
            }
            else
            {
                if (textBox1.Text == "" || maskedTextBox1.Text == "" || textBox2.Text == "")
                {
                    Fechar();
                    MessageBox.Show("Não existe evento para editar");
                }
                else
                {
                    va.Codigo = Convert.ToInt32(label5.Text);
                    va.Nome = textBox1.Text;
                    va.Data = Convert.ToDateTime(dateTimePicker1.Text);
                    va.Local = textBox3.Text;
                    va.Horario = maskedTextBox1.Text;
                    va.Evento = textBox2.Text;
                    co.alterarEv(va);
                    Fechar();
                    MessageBox.Show("Evento Alterado");
                    textBox1.Text = "";
                    dateTimePicker1.Text = "";
                    textBox3.Text = "";
                    maskedTextBox1.Text = "";
                    textBox2.Text = "";
                    this.Close();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (aux != textBox1.Text && privilegio == "")
            {
                Fechar();
                MessageBox.Show("Você não pode excluir um evento de " + textBox1.Text);
            }
            else
            {
                if (textBox1.Text == "" || maskedTextBox1.Text == "" || textBox2.Text == "")
                {
                    Fechar();
                    MessageBox.Show("Não existe evento para excluir");
                }
                else
                {
                    va.Codigo = Convert.ToInt32(label5.Text);
                    string message = "Deseja realmente excluir este evento?";
                    string caption = "Confirmar exclusão";
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    DialogResult result;

                    result = MessageBox.Show(message, caption, buttons);

                    if (result == System.Windows.Forms.DialogResult.No)
                    {
                        this.Close();
                    }
                    else
                    {
                        co.excluirEv(va);
                        Fechar();
                        MessageBox.Show("Evento Excluído");
                        textBox1.Text = "";
                        dateTimePicker1.Text = "";
                        textBox3.Text = "";
                        maskedTextBox1.Text = "";
                        textBox2.Text = "";
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            dateTimePicker1.Text = "";
            textBox3.Text = "";
            maskedTextBox1.Text = "";
            textBox2.Text = "";
        }

        private void frmEdEventos_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            seg = 60;
            min = 02;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            seg--;
            if (seg == 0)
            {
                seg = 60;
            }

            if (min == 0 && seg == 60)
            {
                this.Close();
            }

            if (seg == 60)
            {
                toolStripLabel2.Text = min.ToString().PadLeft(2, '0') + ":00";
                min--;
            }
            else
            {
                toolStripLabel2.Text = min.ToString().PadLeft(2, '0') + ":" + seg.ToString().PadLeft(2, '0');
            }

        }
    }
}
