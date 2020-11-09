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
    public partial class Form1 : Form
    {

        //SqlConnection conexao;
        string aux;
        int seg = 60, min = 5;
        ConectaAgenda co = new ConectaAgenda();
        public Form1(string rec)
        {
            InitializeComponent();
            aux = rec;
        }

        public void Fechar()
        {
            timer2.Interval = 2000;
            timer2.Start();
        }

        private void Data()
        {
            this.dateTimePicker1.Text = co.BuscaDataServidor();
            this.dateTimePicker2.Text = co.BuscaDataServidor();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Variavel va = new Variavel();
            Conecta co = new Conecta();
        
            if (maskedTextBox1.Text == "" || textBox2.Text == "")
            {
                Fechar();
                MessageBox.Show("Por favor, preencher todos os campos");
            }
            else
            {
                //MessageBox.Show(us);
                va.Nome = aux;
                va.Data = Convert.ToDateTime(dateTimePicker1.Text);
                va.DtCadastro = Convert.ToDateTime(dateTimePicker2.Text);
                va.Local = textBox1.Text;
                va.Horario = maskedTextBox1.Text;
                va.Evento = textBox2.Text;
                co.cadastro(va);
                Fechar();
                MessageBox.Show("Evento cadastrado");

                maskedTextBox1.Text = "";
                textBox1.Text = "";
                textBox2.Text = "";
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Text = "";
            maskedTextBox1.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var cad = new frmPesqEventos("","");
            if (Application.OpenForms.OfType<frmPesqEventos>().Count() > 0)
            {
                Application.OpenForms[cad.Name].Focus();
            }
            else
            {
                cad.Show();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Data();
            timer1.Enabled = true;
            seg = 60;
            min = 04;
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
                toolStripLabel4.Text = min.ToString().PadLeft(2, '0') + ":00";
                min--;
            }
            else
            {
                toolStripLabel4.Text = min.ToString().PadLeft(2, '0') + ":" + seg.ToString().PadLeft(2, '0');
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            SendKeys.Send("{ESC}");
            timer2.Stop();
        }

    }
}
