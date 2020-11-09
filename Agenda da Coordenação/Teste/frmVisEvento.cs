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
    public partial class frmVisEvento : Form
    {
        public frmVisEvento(string nome, string dtCad, string data, string local, string horario, string evento)
        {
            InitializeComponent();
            textBox1.Text = nome;
            dateTimePicker2.Text = data;
            dateTimePicker1.Text = dtCad;
            textBox3.Text = local;
            maskedTextBox1.Text = horario;
            textBox2.Text = evento;

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            TextBox text = new TextBox();
            text.ScrollBars = ScrollBars.Vertical;
        }
    }
}
