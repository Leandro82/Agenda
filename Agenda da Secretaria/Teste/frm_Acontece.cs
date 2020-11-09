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
    public partial class frm_Acontece : Form
    {
        int quant1;
        int pos1;
        int mudar = 0;
        int[] posicao = new int[10];
        DateTime dataHoje = DateTime.Today;

        public frm_Acontece(int quant, int pos)
        {
            InitializeComponent();
            quant1 = quant;
            pos1 = pos;
        }

        private DataTable Evento()
        {
            Conecta cl = new Conecta();
            MySqlConnection con = new MySqlConnection("Persist Security Info=false;SERVER=10.66.123.200;DATABASE=agenda;UID=secac;pwd=secac");
            string vSQL = "Select * FROM agenda";
            MySqlDataAdapter vDataAdapter = new MySqlDataAdapter(vSQL, con);
            DataTable vTable = new DataTable();
            vDataAdapter.Fill(vTable);
            return vTable;
        }

        private void frm_Acontece_Load(object sender, EventArgs e)
        {
            int cont = Evento().Rows.Count - 1;
            int i = 0,j = 0;

            button1.Enabled = false;
            label1.Text = "Evento(s) de "+Convert.ToString(quant1);
            textBox1.Text = "1";

            while (i <= cont)
            {
                if (Evento().Rows[i]["horario"].ToString() == Evento().Rows[pos1]["horario"].ToString() && Convert.ToString(dataHoje) == Evento().Rows[i]["data"].ToString())
                {
                    posicao[j] = i;
                    j = j + 1;
                }
                i = i + 1;
            }


            textBox2.Text = Evento().Rows[posicao[0]]["evento"].ToString();
            textBox3.Text = Evento().Rows[posicao[0]]["local"].ToString();

            if (quant1 == 1)
            {
                button1.Enabled = false;
                button2.Enabled = false;
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            int cont;
            button1.Enabled = true;
            cont = Convert.ToInt32(textBox1.Text) + 1;
            textBox1.Text = Convert.ToString(cont);
            if (Convert.ToString(cont) == textBox1.Text)
            {
                button2.Enabled = false;
            }

            mudar = mudar + 1;
            textBox2.Text = Evento().Rows[posicao[mudar]]["evento"].ToString();
            textBox3.Text = Evento().Rows[posicao[mudar]]["local"].ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int cont;
            cont = Convert.ToInt32(textBox1.Text) - 1;
            textBox1.Text = Convert.ToString(cont);

            if (textBox1.Text == "1")
            {
                button1.Enabled = false;
                button2.Enabled = true;
            }

            mudar = mudar - 1;
            textBox2.Text = Evento().Rows[posicao[mudar]]["evento"].ToString();
            textBox3.Text = Evento().Rows[posicao[mudar]]["local"].ToString();
        }


        }
        
        
    }
