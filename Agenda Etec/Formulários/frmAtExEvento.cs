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
    public partial class frmAtExEvento : Form
    {
        ConectaAgenda ca = new ConectaAgenda();
        ConectaUsuario co = new ConectaUsuario();
        Agenda ag = new Agenda();
        DateTime dtEv;
        string hrCad, dtCad;
        int codEv, respEv;
        public frmAtExEvento(int resp)
        {
            InitializeComponent();
            respEv = resp;
        }

        public void LimparComponentes()
        {
            dateTimePicker1.Text = co.BuscaDataServidor();
            textBox1.Clear();
            dataGridView1.Rows.Clear();
            dateTimePicker2.Text = co.BuscaDataServidor();
            maskedTextBox2.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
           if(radioButton1.Checked == true)
            {
                dateTimePicker1.Enabled = true;
                textBox1.Enabled = false;
                textBox1.Clear();
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                dateTimePicker1.Enabled = false;
                textBox1.Enabled = true;
                textBox1.Clear();
            }
        }

        private void frmAtExEvento_Load(object sender, EventArgs e)
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
                dataGridView1.Rows[n].Cells[0].Value = evento["codEv"].GetHashCode();
                dataGridView1.Rows[n].Cells[1].Value = Convert.ToDateTime(evento["dtEv"].ToString()).ToString("dd/MM/yyyy");
                dataGridView1.Rows[n].Cells[2].Value = Convert.ToDateTime(evento["hrEv"].ToString()).ToString("HH:mm");
                dataGridView1.Rows[n].Cells[3].Value = evento["evento"].ToString();
                dataGridView1.Rows[n].Cells[4].Value = evento["localEv"].ToString();
                ag.Codigo = evento["respCadEv"].GetHashCode();
                dataGridView1.Rows[n].Cells[5].Value = ca.RespCadastro(ag).Rows[0][0].ToString();
                dataGridView1.Rows[n].Cells[6].Value = Convert.ToDateTime(evento["dtCad"].ToString()).ToString("dd/MM/yyyy");
                dataGridView1.Rows[n].Cells[7].Value = Convert.ToDateTime(evento["hrCad"].ToString()).ToString("HH:mm");
            }
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
                dataGridView1.Rows[n].Cells[0].Value = evento["codEv"].GetHashCode();
                dataGridView1.Rows[n].Cells[1].Value = Convert.ToDateTime(evento["dtEv"].ToString()).ToString("dd/MM/yyyy");
                dataGridView1.Rows[n].Cells[2].Value = Convert.ToDateTime(evento["hrEv"].ToString()).ToString("HH:mm");
                dataGridView1.Rows[n].Cells[3].Value = evento["evento"].ToString();
                dataGridView1.Rows[n].Cells[4].Value = evento["localEv"].ToString();
                ag.Codigo = evento["respCadEv"].GetHashCode();
                dataGridView1.Rows[n].Cells[5].Value = ca.RespCadastro(ag).Rows[0][0].ToString();
                dataGridView1.Rows[n].Cells[6].Value = Convert.ToDateTime(evento["dtCad"].ToString()).ToString("dd/MM/yyyy");
                dataGridView1.Rows[n].Cells[7].Value = Convert.ToDateTime(evento["hrCad"].ToString()).ToString("HH:mm");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!maskedTextBox2.MaskCompleted)
            {
                string msg = "Informe o horário do evento!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
                maskedTextBox2.Focus();
            }
            else if (textBox2.Text == "")
            {
                string msg = "Informe o local do evento!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
                textBox2.Focus();
            }
            else if (textBox3.Text == "")
            {
                string msg = "Informe que evento irá acontecer!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
                textBox3.Focus();
            }
            else
            {
                dtEv = DateTime.ParseExact(dateTimePicker2.Value.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.CurrentCulture);
                ag.Codigo = codEv;
                ag.DataEvento = Convert.ToString(dtEv);
                ag.HoraEvento = maskedTextBox2.Text;
                ag.Local = textBox2.Text;
                ag.Evento = textBox3.Text;
                ag.RepCadastroEvento = respEv;
                ag.DataCadastro = co.BuscaDataServidor().ToString();
                ag.HoraCadastro = co.BuscaHoraServidor().ToString();
                ca.AtualizarEvento(ag);
                string msg = "Evento atualizado!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
                LimparComponentes();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ag.Codigo = codEv;
            ca.ExcluirEvento(ag);
            string msg = "Evento excluído!!";
            frmMensagem mg = new frmMensagem(msg);
            mg.ShowDialog();
            LimparComponentes();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LimparComponentes();
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
                dataGridView1.Rows[n].Cells[0].Value = evento["codEv"].GetHashCode();
                dataGridView1.Rows[n].Cells[1].Value = Convert.ToDateTime(evento["dtEv"].ToString()).ToString("dd/MM/yyyy");
                dataGridView1.Rows[n].Cells[2].Value = Convert.ToDateTime(evento["hrEv"].ToString()).ToString("HH:mm");
                dataGridView1.Rows[n].Cells[3].Value = evento["evento"].ToString();
                dataGridView1.Rows[n].Cells[4].Value = evento["localEv"].ToString();
                ag.Codigo = evento["respCadEv"].GetHashCode();
                dataGridView1.Rows[n].Cells[5].Value = ca.RespCadastro(ag).Rows[0][0].ToString();
                dataGridView1.Rows[n].Cells[6].Value = Convert.ToDateTime(evento["dtCad"].ToString()).ToString("dd/MM/yyyy");
                dataGridView1.Rows[n].Cells[7].Value = Convert.ToDateTime(evento["hrCad"].ToString()).ToString("HH:mm");
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            codEv = dataGridView1[0, dataGridView1.CurrentCellAddress.Y].Value.GetHashCode();
            dateTimePicker2.Text = dataGridView1[1, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            maskedTextBox2.Text = dataGridView1[2, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            textBox3.Text = dataGridView1[3, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            textBox2.Text = dataGridView1[4, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            dtCad = dataGridView1[6, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            hrCad = dataGridView1[7, dataGridView1.CurrentCellAddress.Y].Value.ToString();
        }
    }
}
