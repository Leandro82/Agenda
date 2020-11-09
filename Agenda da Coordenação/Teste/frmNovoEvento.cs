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
    public partial class frmNovoEvento : Form
    {
        int linha;
        ConectaAgenda co = new ConectaAgenda();

        public frmNovoEvento()
        {
            InitializeComponent();
        }

        private void frmNovoEvento_Load(object sender, EventArgs e)
        {
            int aux = co.Evento1().Rows.GetHashCode();
            int cont = co.Evento1().Rows.Count;
            for (int i = 0; i < cont; i++)
            {
                if (co.Evento1().Rows[i]["cod"].GetHashCode() == aux)
                {
                    linha = i;
                }
            }
            textBox1.Text = Convert.ToDateTime(co.Evento1().Rows[linha]["data"].ToString()).ToString("dd/MM/yyyy");
            textBox2.Text = co.Evento1().Rows[linha]["evento"].ToString();
            maskedTextBox1.Text = co.Evento1().Rows[linha]["horario"].ToString();
            textBox3.Text = co.Evento1().Rows[linha]["local"].ToString();
        }
    }
}
