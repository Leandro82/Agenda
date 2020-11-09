using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace Teste
{
    public partial class frmInserirUsuario : Form
    {
        int cont = 0;
        Usuario us = new Usuario();
        ConectaUsuario co = new ConectaUsuario();
        CriaUsuario ca = new CriaUsuario();
        Variavel va = new Variavel();

         public frmInserirUsuario()
        {
            InitializeComponent();
        }

         public void Fechar()
         {
             timer1.Interval = 2000;
             timer1.Start();
         }

        private void button5_Click(object sender, EventArgs e)
        {           
            dataGridView1.Rows.Clear();
            foreach (DataRow item in co.Usuarios().Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["nome"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["login"].ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i = 0;
            int aux = 0;
            cont = co.Usuario().Rows.Count - 1; ;
            

            while (i <= cont)
            {
                if (co.Usuario().Rows[i]["nome"].ToString() == textBox1.Text)
                {
                    aux = 1;
                }
                i = i + 1;
            }

            if (aux == 1)
            {
                Fechar();
                MessageBox.Show("Usuário já cadastrado");
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                label5.Text = "";
            }
            else
            {
                us.Nome = textBox1.Text;
                us.Login = textBox2.Text;
                us.Senha = textBox4.Text;
                us.Acesso = "";

                if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
                {
                    Fechar();
                    MessageBox.Show("Preencha todos os campos");
                }
                else
                {
                    ca.cadastro(us);
                    Fechar();
                    MessageBox.Show("Usuário cadastrado");
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    label5.Text = "";
                }
            }
        }

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

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            textBox1.ReadOnly = true;
            button2.Enabled = true;
            button4.Enabled = true;
            button1.Enabled = false;
            string nome = dataGridView1[0, dataGridView1.CurrentCellAddress.Y].Value.ToString();
            string login = dataGridView1[1, dataGridView1.CurrentCellAddress.Y].Value.ToString();

            textBox1.Text = nome;
            textBox2.Text = login;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            va.Nome = textBox1.Text;

            try
            {
                string message = "Deseja realmente excluir este usuário?";
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
                    co.excluirUs(va);
                    Fechar();
                    MessageBox.Show("Evento Excluído");
                    textBox1.Text = "";
                    textBox1.ReadOnly = false;
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de comandos: " + ex.Message);

            }
            button1.Enabled = true;
            button2.Enabled = false;
            button4.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmInserirUsuario_Load(object sender, EventArgs e)
        {
            label5.Text = "";
            button2.Enabled = false;
            button4.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
                if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
                {
                    Fechar();
                    MessageBox.Show("Preencha todos os campos");
                }
                else
                {
                    us.Nome = textBox1.Text;
                    us.Login = textBox2.Text;
                    us.Senha = textBox4.Text;
                    us.Acesso = "";
                    co.cadastro(us);
                    Fechar();
                    MessageBox.Show("Dados atualizados");
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    label5.Text = "";
                }
                button1.Enabled = true;
                button2.Enabled = false;
                button4.Enabled = false;
                textBox1.ReadOnly = false;
            }

        private void timer1_Tick(object sender, EventArgs e)
        {
            SendKeys.Send("{ESC}");
            timer1.Stop();
        }
       }
    }

