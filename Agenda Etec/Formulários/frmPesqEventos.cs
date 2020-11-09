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
    public partial class frmPesqEventos : Form
    {
        ConectaAgenda ca = new ConectaAgenda();
        ConectaUsuario co = new ConectaUsuario();
        Agenda ag = new Agenda();
        DateTime dtEv;
        string evHoje, HoraAux, HoraEv;
        public frmPesqEventos(string hj, string ha, string he)
        {
            InitializeComponent();
            evHoje = hj;
            HoraAux = ha;
            HoraEv = he;
        }

        private void frmPesqEventos_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            dateTimePicker1.Enabled = false;
            dateTimePicker2.Enabled = false;
            dateTimePicker3.Enabled = false;
            textBox1.Enabled = false;
            if (evHoje == "Hoje")
            {
                ag.TipoPesquisa = "Data";
                ag.DataEvento = Convert.ToDateTime(co.BuscaDataServidor().ToString()).ToString("dd/MM/yyyy");
            }
            else if(evHoje == "Agora")
            {
                ag.DataEvento = co.BuscaDataServidor();
                ag.TipoPesquisa = "Horário";
                ag.HoraEvento = HoraEv;
                ag.HoraAuxiliar = HoraAux;
            }
            foreach (DataRow evento in ca.BuscaEventos(ag).Rows)
            {
                int n = dataGridView1.Rows.Add();
                if (n % 2 == 0)
                    dataGridView1.Rows[n].DefaultCellStyle.BackColor = Color.Gray;
                else
                {
                    dataGridView1.Rows[n].DefaultCellStyle.BackColor = Color.LightGray;
                    //dataGridView1.Rows[n].DefaultCellStyle.ForeColor = Color.White;
                }
                dataGridView1.Rows[n].Cells[0].Value = Convert.ToDateTime(evento["dtEv"].ToString()).ToString("dd/MM/yyyy");
                dataGridView1.Rows[n].Cells[1].Value = Convert.ToDateTime(evento["hrEv"].ToString()).ToString("HH:mm");
                dataGridView1.Rows[n].Cells[2].Value = evento["evento"].ToString();
                dataGridView1.Rows[n].Cells[3].Value = evento["localEv"].ToString();
                ag.Codigo = evento["respCadEv"].GetHashCode();
                dataGridView1.Rows[n].Cells[4].Value = ca.RespCadastro(ag).Rows[0][0].ToString();
                dataGridView1.Rows[n].Cells[5].Value = Convert.ToDateTime(evento["dtCad"].ToString()).ToString("dd/MM/yyyy");
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            if(radioButton1.Checked == true)
            {
                textBox1.Clear();
                textBox1.Enabled = false;
                dateTimePicker1.Enabled = true;
                dateTimePicker2.Enabled = false;
                dateTimePicker3.Enabled = false;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            if (radioButton2.Checked == true)
            {
                textBox1.Enabled = true;
                dateTimePicker1.Enabled = false;
                dateTimePicker2.Enabled = false;
                dateTimePicker3.Enabled = false;
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox1.Enabled = false;
            dateTimePicker1.Enabled = false;
            dateTimePicker2.Enabled = true;
            dataGridView1.Rows.Clear();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker3.Enabled = true;
            dateTimePicker3.MinDate = dateTimePicker2.Value;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dtEv = DateTime.ParseExact(dateTimePicker1.Value.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.CurrentCulture);
            ag.DataEvento = Convert.ToString(dtEv);
            ag.TipoPesquisa = "Data";
            dataGridView1.Rows.Clear();
            foreach (DataRow evento in ca.BuscaEventos(ag).Rows)
            {
                int n = dataGridView1.Rows.Add();
                if (n % 2 == 0)
                    dataGridView1.Rows[n].DefaultCellStyle.BackColor = Color.Gray;
                else
                    dataGridView1.Rows[n].DefaultCellStyle.BackColor = Color.LightGray;
                dataGridView1.Rows[n].Cells[0].Value = Convert.ToDateTime(evento["dtEv"].ToString()).ToString("dd/MM/yyyy");
                dataGridView1.Rows[n].Cells[1].Value = Convert.ToDateTime(evento["hrEv"].ToString()).ToString("HH:mm");
                dataGridView1.Rows[n].Cells[2].Value = evento["evento"].ToString();
                dataGridView1.Rows[n].Cells[3].Value = evento["localEv"].ToString();
                ag.Codigo = evento["respCadEv"].GetHashCode();
                dataGridView1.Rows[n].Cells[4].Value = ca.RespCadastro(ag).Rows[0][0].ToString();
                dataGridView1.Rows[n].Cells[5].Value = Convert.ToDateTime(evento["dtCad"].ToString()).ToString("dd/MM/yyyy");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            ag.Evento = textBox1.Text;
            ag.TipoPesquisa = "Evento";
            dataGridView1.Rows.Clear();
            foreach (DataRow evento in ca.BuscaEventos(ag).Rows)
            {
                int n = dataGridView1.Rows.Add();
                if (n % 2 == 0)
                    dataGridView1.Rows[n].DefaultCellStyle.BackColor = Color.Gray;
                else
                    dataGridView1.Rows[n].DefaultCellStyle.BackColor = Color.LightGray;
                dataGridView1.Rows[n].Cells[0].Value = Convert.ToDateTime(evento["dtEv"].ToString()).ToString("dd/MM/yyyy");
                dataGridView1.Rows[n].Cells[1].Value = Convert.ToDateTime(evento["hrEv"].ToString()).ToString("HH:mm");
                dataGridView1.Rows[n].Cells[2].Value = evento["evento"].ToString();
                dataGridView1.Rows[n].Cells[3].Value = evento["localEv"].ToString();
                ag.Codigo = evento["respCadEv"].GetHashCode();
                dataGridView1.Rows[n].Cells[4].Value = ca.RespCadastro(ag).Rows[0][0].ToString();
                dataGridView1.Rows[n].Cells[5].Value = Convert.ToDateTime(evento["dtCad"].ToString()).ToString("dd/MM/yyyy");
            }
        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            ag.DataEvento = dateTimePicker2.Value.ToString("dd/MM/yyyy");
            ag.DataAuxiliar = dateTimePicker3.Value.ToString("dd/MM/yyyy");
            ag.TipoPesquisa = "Intervalo";
            dataGridView1.Rows.Clear();
            foreach (DataRow evento in ca.BuscaEventos(ag).Rows)
            {
                int n = dataGridView1.Rows.Add();
                if (n % 2 == 0)
                    dataGridView1.Rows[n].DefaultCellStyle.BackColor = Color.Gray;
                else
                    dataGridView1.Rows[n].DefaultCellStyle.BackColor = Color.LightGray;
                dataGridView1.Rows[n].Cells[0].Value = Convert.ToDateTime(evento["dtEv"].ToString()).ToString("dd/MM/yyyy");
                dataGridView1.Rows[n].Cells[1].Value = Convert.ToDateTime(evento["hrEv"].ToString()).ToString("HH:mm");
                dataGridView1.Rows[n].Cells[2].Value = evento["evento"].ToString();
                dataGridView1.Rows[n].Cells[3].Value = evento["localEv"].ToString();
                ag.Codigo = evento["respCadEv"].GetHashCode();
                dataGridView1.Rows[n].Cells[4].Value = ca.RespCadastro(ag).Rows[0][0].ToString();
                dataGridView1.Rows[n].Cells[5].Value = Convert.ToDateTime(evento["dtCad"].ToString()).ToString("dd/MM/yyyy");
            }
        }
    }
}
