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
    public partial class frmPesqEventos : Form
    {
        string auxData;
        string auxVis;
        ConectaAgenda co = new ConectaAgenda();
        Variavel va = new Variavel();

        int cont;
        public frmPesqEventos(string data, string vis)
        {
            InitializeComponent();
            auxData = data;
            auxVis = vis;
        }

        public void Fechar()
        {
            timer1.Interval = 2000;
            timer1.Start();
        }

        private void Data()
        {
            this.dateTimePicker1.Text = co.BuscaDataServidor();
            this.dateTimePicker2.Text = co.BuscaDataServidor();
            this.dateTimePicker3.Text = co.BuscaDataServidor();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            //int cod = dataGridView1[0, dataGridView1.CurrentCellAddress.Y].Value.GetHashCode();
            string nome = dataGridView1[0, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            string dtCad = dataGridView1[1, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            string data = dataGridView1[2, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            string local = dataGridView1[3, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            string horario = dataGridView1[4, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            string evento = dataGridView1[5, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            var form = new frmVisEvento(nome,dtCad, data, local, horario, evento);
            form.Show();
            form.TopMost = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == false && radioButton2.Checked == false && radioButton3.Checked == false)
            {
                MessageBox.Show("Por favor escolha um parâmetro para pesquisa");
            }
            else
            {
                if (radioButton2.Checked == true)
                {
                    va.Auxiliar = "Nome";
                    va.Nome = textBox1.Text;
                }
                else if (radioButton1.Checked == true)
                {
                    va.Auxiliar = "Data";
                    va.Data = Convert.ToDateTime(dateTimePicker1.Text);
                }
                else if (radioButton3.Checked == true)
                {
                    va.Auxiliar = "Intervalo";
                    va.Data = Convert.ToDateTime(dateTimePicker2.Text);
                    va.Data2 = Convert.ToDateTime(dateTimePicker3.Text);
                }

                cont = co.pesqAgenda(va).Rows.Count;
                if (cont == 0)
                {
                    dataGridView1.Rows.Clear();
                    Fechar();
                    MessageBox.Show("Não existe dados para essa pesquisa");
                    textBox1.Text = "";
                }
                else
                {
                    dataGridView1.Rows.Clear();
                    foreach (DataRow item in co.pesqAgenda(va).Rows)
                    {
                        int n = dataGridView1.Rows.Add();
                        //dataGridView1.Rows[n].Cells[0].Value = item["cod"].GetHashCode();
                        dataGridView1.Rows[n].Cells[0].Value = item["nome"].ToString();
                        dataGridView1.Rows[n].Cells[1].Value = Convert.ToDateTime(item["dtCad"].ToString()).ToString("dd/MM/yyyy");
                        dataGridView1.Rows[n].Cells[2].Value = Convert.ToDateTime(item["data"].ToString()).ToString("dd/MM/yyyy");
                        dataGridView1.Rows[n].Cells[3].Value = item["local"].ToString();
                        dataGridView1.Rows[n].Cells[4].Value = item["horario"].ToString();
                        dataGridView1.Rows[n].Cells[5].Value = item["evento"].ToString();
                    }
                }
            }
        }


        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                textBox1.Enabled = true;
                textBox1.Focus();
                dateTimePicker1.Enabled = false;
                dateTimePicker2.Enabled = false;
                dateTimePicker3.Enabled = false;
            }
            else if (radioButton1.Checked == true)
            {
                dateTimePicker1.Enabled = true;
                textBox1.Text = "";
                textBox1.Enabled = false;
                dateTimePicker2.Enabled = false;
                dateTimePicker3.Enabled = false;
            }
            else if (radioButton3.Checked == true)
            {
                dateTimePicker1.Enabled = false;
                textBox1.Text = "";
                textBox1.Enabled = false;
                dateTimePicker2.Enabled = true;
                dateTimePicker3.Enabled = true;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                textBox1.Enabled = true;
                dateTimePicker1.Enabled = false;
                dateTimePicker2.Enabled = false;
                dateTimePicker3.Enabled = false;
            }
            else if (radioButton1.Checked == true)
            {
                dateTimePicker1.Enabled = true;
                textBox1.Text = "";
                textBox1.Enabled = false;
                dateTimePicker2.Enabled = false;
                dateTimePicker3.Enabled = false;
            }
            else if(radioButton3.Checked == true)
            {
                dateTimePicker1.Enabled = false;
                textBox1.Text = "";
                textBox1.Enabled = false;
                dateTimePicker2.Enabled = true;
                dateTimePicker3.Enabled = true;
            }
        }

        private void frmPesqEventos_Load(object sender, EventArgs e)
        {
            Data();
            textBox1.Enabled = false;
            dateTimePicker1.Enabled = false;
            dateTimePicker2.Enabled = false;
            dateTimePicker3.Enabled = false;

            if (auxVis == "Ok")
            {
                va.Auxiliar = "Data";
                va.Data = Convert.ToDateTime(dateTimePicker1.Text);
                dataGridView1.Rows.Clear();
                foreach (DataRow item in co.pesqAgenda(va).Rows)
                {
                    int n = dataGridView1.Rows.Add();
                    dataGridView1.Rows[n].Cells[0].Value = item["nome"].ToString();
                    dataGridView1.Rows[n].Cells[1].Value = Convert.ToDateTime(item["dtCad"].ToString()).ToString("dd/MM/yyyy");
                    dataGridView1.Rows[n].Cells[2].Value = Convert.ToDateTime(item["data"].ToString()).ToString("dd/MM/yyyy");
                    dataGridView1.Rows[n].Cells[3].Value = item["local"].ToString();
                    dataGridView1.Rows[n].Cells[4].Value = item["horario"].ToString();
                    dataGridView1.Rows[n].Cells[5].Value = item["evento"].ToString();
                }
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                textBox1.Enabled = true;
                dateTimePicker1.Enabled = false;
                dateTimePicker2.Enabled = false;
                dateTimePicker3.Enabled = false;
            }
            else if (radioButton1.Checked == true)
            {
                dateTimePicker1.Enabled = true;
                textBox1.Text = "";
                textBox1.Enabled = false;
                dateTimePicker2.Enabled = false;
                dateTimePicker3.Enabled = false;
            }
            else if (radioButton3.Checked == true)
            {
                dateTimePicker1.Enabled = false;
                textBox1.Text = "";
                textBox1.Enabled = false;
                dateTimePicker2.Enabled = true;
                dateTimePicker3.Enabled = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            SendKeys.Send("{ESC}");
            timer1.Stop();
        }
    }
}
