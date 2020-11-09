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
    public partial class frmPrivilegio : Form
    {
        ConectaUsuario co = new ConectaUsuario();
        Variavel va = new Variavel();

        public frmPrivilegio()
        {
            InitializeComponent();
        }

        public void Fechar()
        {
            timer1.Interval = 2000;
            timer1.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            foreach (DataRow item in co.usuariosPriv().Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["nome"].ToString();
                if (item["privilegio"].ToString() == "Ok")
                {
                    dataGridView1.Rows[n].Cells[1].Value = true;
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int cont = 0;
            DataGridViewCheckBoxCell cell;
            foreach (DataGridViewRow linha in dataGridView1.Rows)
            {

                cell = linha.Cells[1] as DataGridViewCheckBoxCell;// linha.Cells["nomeDaColuna"] ou linha.Cells[0]
                bool bChecked = (null != cell && null != cell.Value && true == (bool)cell.Value);
                if (bChecked == true)
                {
                    va.Auxiliar = "True";
                    va.Evento = "Ok";
                    va.Nome = linha.Cells[0].Value.ToString();
                    co.alteraPriv(va);
                }
                else
                {
                    va.Auxiliar = "False";
                    va.Evento = "";
                    va.Nome = linha.Cells[0].Value.ToString();
                    co.alteraPriv(va);
                }
            }
            Fechar();
            MessageBox.Show("Operação realizada com sucesso");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            SendKeys.Send("{ESC}");
            timer1.Stop();
        }
    }
}
