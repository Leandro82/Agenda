using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Agenda_Etec
{
    public partial class frmCadUsuario : Form
    {
        Usuario us = new Usuario();
        ConectaUsuario cs = new ConectaUsuario();
        int codUs = 0;
        public frmCadUsuario()
        {
            InitializeComponent();
        }

        public void LimpaComponentes()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Enabled = true;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;
            checkBox7.Checked = false;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                string msg = "Informe o nome completo!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else if (textBox2.Text == "")
            {
                string msg = "Informe um E-mail válido!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else if (checkBox1.Checked == false && checkBox2.Checked == false && checkBox3.Checked == false && checkBox4.Checked == false && checkBox5.Checked == false && checkBox6.Checked == false && checkBox7.Checked == false)
            {
                string msg = "Escolha uma função!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else if (textBox3.Text == "")
            {
                string msg = "Informe um Login para acesso ao sistema!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else if (textBox4.Text == "")
            {
                string msg = "Informe uma senha para acesso ao sistema!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else if (radioButton1.Checked == false && radioButton2.Checked == false)
            {
                string msg = "Informe a situação do usuário na escola!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else
            {
                us.Nome = textBox1.Text;
                us.Email = textBox2.Text;
                if (checkBox1.Checked == true)
                {
                    us.Profissao1 = "Secretaria Acadêmica";
                    us.Profissao2 = "";
                }
                else if (checkBox2.Checked == true)
                {
                    us.Profissao1 = "Diretoria de Serviços";
                    us.Profissao2 = "";
                }
                else if (checkBox3.Checked == true)
                {
                    us.Profissao1 = "Professor";
                    us.Profissao2 = "Coordenador de Curso";
                }
                else if (checkBox5.Checked == true)
                {
                    us.Profissao1 = "Professor";
                    us.Profissao2 = "Coordenador Pedagógico";
                }
                else if (checkBox6.Checked == true)
                {
                    us.Profissao1 = "Professor";
                    us.Profissao2 = "Orientador Educacional";
                }
                else if (checkBox4.Checked == true)
                {
                    us.Profissao1 = "Professor";
                    us.Profissao2 = "";
                }
                else if (checkBox7.Checked == true)
                {
                    us.Profissao1 = "Diretor";
                    us.Profissao2 = "";
                }
                us.Login = textBox3.Text;
                us.Senha = textBox4.Text;
                if (radioButton1.Checked == true)
                    us.Situacao = "Ativo";
                else if (radioButton2.Checked == true)
                    us.Situacao = "Inativo";
                us.Acesso = "Não";

                cs.Cadastro(us);
                string msg = "Usuário cadastrado!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
                LimpaComponentes();
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            us.Nome = textBox5.Text;

            dataGridView1.Rows.Clear();
            foreach(DataRow usuario in cs.SelecionaUsuario(us).Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = usuario["codUs"].GetHashCode();
                dataGridView1.Rows[n].Cells[1].Value = usuario["nome"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = usuario["email"].ToString();
                dataGridView1.Rows[n].Cells[3].Value = usuario["funcao"].ToString();
                dataGridView1.Rows[n].Cells[4].Value = usuario["coordenacao"].ToString();
                dataGridView1.Rows[n].Cells[5].Value = usuario["situacao"].ToString();
                dataGridView1.Rows[n].Cells[6].Value = usuario["login"].ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                string msg = "Informe o nome completo!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else if (textBox2.Text == "")
            {
                string msg = "Informe um E-mail válido!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else if (checkBox1.Checked == false && checkBox2.Checked == false && checkBox3.Checked == false && checkBox4.Checked == false && checkBox5.Checked == false && checkBox6.Checked == false && checkBox7.Checked == false)
            {
                string msg = "Escolha uma função!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else if (radioButton1.Checked == false && radioButton2.Checked == false)
            {
                string msg = "Informe a situação do usuário na escola!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else
            {
                us.Nome = textBox1.Text;
                us.Email = textBox2.Text;
                if (checkBox1.Checked == true)
                {
                    us.Profissao1 = "Secretaria Acadêmica";
                    us.Profissao2 = "";
                }
                else if (checkBox2.Checked == true)
                {
                    us.Profissao1 = "Diretoria de Serviços";
                    us.Profissao2 = "";
                }
                else if (checkBox3.Checked == true)
                {
                    us.Profissao1 = "Professor";
                    us.Profissao2 = "Coordenador de Curso";
                }
                else if (checkBox5.Checked == true)
                {
                    us.Profissao1 = "Professor";
                    us.Profissao2 = "Coordenador Pedagógico";
                }
                else if (checkBox6.Checked == true)
                {
                    us.Profissao1 = "Professor";
                    us.Profissao2 = "Orientador Educacional";
                }
                else if (checkBox4.Checked == true)
                {
                    us.Profissao1 = "Professor";
                    us.Profissao2 = "";
                }
                else if (checkBox7.Checked == true)
                {
                    us.Profissao1 = "Diretor";
                    us.Profissao2 = "";
                }
                us.Login = textBox3.Text;
                us.Senha = textBox4.Text;
                if (radioButton1.Checked == true)
                    us.Situacao = "Ativo";
                else if (radioButton2.Checked == true)
                    us.Situacao = "Inativo";
                us.Codigo = codUs;

                cs.AtualizarDadosPessoais(us);
                string msg = "Cadastrado atualizado!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
                LimpaComponentes();
                button1.Enabled = true;
                button5.Visible = false;
                textBox5.Text = "";
                dataGridView1.Rows.Clear();
            }
        }

        private void frmCadUsuario_Load(object sender, EventArgs e)
        {
            button5.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                string msg = "Informe um Login!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else if (textBox4.Text == "")
            {
                string msg = "Informe uma Senha!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else
            {
                us.Codigo = codUs;
                us.Login = textBox3.Text;
                us.Senha = textBox4.Text;
                us.Acesso = "Não";
                cs.AtualizarAcesso(us);
                string msg = "Login e Senha atualizados!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
                LimpaComponentes();
                textBox5.Text = "";
                dataGridView1.Rows.Clear();
                button1.Enabled = true;
                button5.Visible = false;
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox5.Checked == true)
            {
                checkBox4.Enabled = false;
                checkBox4.Checked = true;
            }
            else if(checkBox5.Checked == false)
            {
                checkBox4.Enabled = true;
                checkBox4.Checked = false;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                checkBox4.Enabled = false;
                checkBox4.Checked = true;
            }
            else if (checkBox3.Checked == false)
            {
                checkBox4.Enabled = true;
                checkBox4.Checked = false;
            }
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked == true)
            {
                checkBox4.Enabled = false;
                checkBox4.Checked = true;
            }
            else if (checkBox6.Checked == false)
            {
                checkBox4.Enabled = true;
                checkBox4.Checked = false;
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                string msg = "Informe um usuário para editar ou excluir!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
                textBox5.Focus();
            }
            else
            {
                button1.Enabled = false;
                button5.Visible = true;
                LimpaComponentes();
                codUs = dataGridView1[0, dataGridView1.CurrentCellAddress.Y].Value.GetHashCode();
                textBox1.Text = dataGridView1[1, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                textBox2.Text = dataGridView1[2, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                if (dataGridView1[3, dataGridView1.CurrentCellAddress.Y].Value.ToString() == "Secretaria Acadêmica")
                    checkBox1.Checked = true;
                else if (dataGridView1[3, dataGridView1.CurrentCellAddress.Y].Value.ToString() == "Diretoria de Serviços")
                    checkBox2.Checked = true;
                else if (dataGridView1[4, dataGridView1.CurrentCellAddress.Y].Value.ToString() == "Coordenador de Curso")
                    checkBox3.Checked = true;
                else if (dataGridView1[4, dataGridView1.CurrentCellAddress.Y].Value.ToString() == "Coordenador Pedagógico")
                    checkBox5.Checked = true;
                else if (dataGridView1[4, dataGridView1.CurrentCellAddress.Y].Value.ToString() == "Orientador Educacional")
                    checkBox6.Checked = true;
                else if (dataGridView1[3, dataGridView1.CurrentCellAddress.Y].Value.ToString() == "Professor")
                    checkBox4.Checked = true;
                else if (dataGridView1[3, dataGridView1.CurrentCellAddress.Y].Value.ToString() == "Diretor")
                    checkBox7.Checked = true;
                textBox3.Text = dataGridView1[6, dataGridView1.CurrentCellAddress.Y].Value.ToString();
                if (dataGridView1[5, dataGridView1.CurrentCellAddress.Y].Value.ToString() == "Ativo")
                    radioButton1.Checked = true;
                else if (dataGridView1[5, dataGridView1.CurrentCellAddress.Y].Value.ToString() == "Inativo")
                    radioButton2.Checked = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (codUs == 0)
            {
                string msg = "Informe um cadastro para excluir!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else
            {
                us.Codigo = codUs;
                cs.ExcluirUsuario(us);
                string msg = "Cadastro excluído!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
                LimpaComponentes();
                button1.Enabled = true;
                button5.Visible = false;
                textBox5.Text = "";
                dataGridView1.Rows.Clear();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LimpaComponentes();
            button1.Enabled = true;
            button5.Visible = false;
            textBox5.Text = "";
            dataGridView1.Rows.Clear();
        }
    }
}
