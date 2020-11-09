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
    public partial class frmLogin : Form
    {

        public frmLogin()
        {
            InitializeComponent();
        }

        Principal pr = new Principal();
        frmMenu mn = new frmMenu();
        ConectaUsuario co = new ConectaUsuario();
        public void Fechar()
        {
            timer1.Interval = 2000;
            timer1.Start();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int i = 0;
            string aux = "aux";
            int rec1 = 100000;
            int rec2 = 100001;

            int cont = co.Usuario().Rows.Count;

            while (i < cont && rec1 != rec2)
            {
                if (textBox1.Text == co.Usuario().Rows[i]["login"].ToString())
                {
                    rec1 = i;
                }


                if (textBox2.Text == co.Usuario().Rows[i]["senha"].ToString())
                {
                    rec2 = i;
                }

                i = i + 1;
            }

            if (rec1 == 100000)
            {
                Fechar();
                MessageBox.Show("Usuário não cadastrado");
                textBox1.Focus();
            }
            else
            {
                if (rec1 != rec2)
                {
                    Fechar();
                    MessageBox.Show("Usuário ou senha inválido");
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox1.Focus();
                }
                else if (co.Usuario().Rows[rec1]["acesso"].ToString() == "")
                {
                    Form2 f2 = new Form2();
                    AuxClas.login = co.Usuario().Rows[rec1]["login"].ToString();
                    AuxClas.usuario = co.Usuario().Rows[rec1]["nome"].ToString();

                    if (co.Usuario().Rows[rec1]["privilegio"].ToString() == "Ok")
                    {
                        AuxClas.privilegio = "Ok";
                    }
                    else
                    {
                        AuxClas.privilegio = "";
                    }
                    f2.Show();
                    f2.Atualizar();
                    this.Hide();
                }
                else
                {
                    AuxClas.usuario = co.Usuario().Rows[rec1]["nome"].ToString();
                    if (co.Usuario().Rows[rec1]["privilegio"].ToString() == "Ok")
                    {
                        AuxClas.privilegio = "Ok";
                    }

                    else
                    {
                        AuxClas.privilegio = "";
                    }
                        AuxClas.auxiliar = aux;
                        

                        mn.Show();
                        mn.Atualizar();
                    
                    
                    this.Hide();
                }
            }
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            SendKeys.Send("{ESC}");
            timer1.Stop();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                int i = 0;
                string aux = "aux";
                int rec1 = 100000;
                int rec2 = 100001;

                int cont = co.Usuario().Rows.Count;

                while (i < cont && rec1 != rec2)
                {
                    if (textBox1.Text == co.Usuario().Rows[i]["login"].ToString())
                    {
                        rec1 = i;
                    }


                    if (textBox2.Text == co.Usuario().Rows[i]["senha"].ToString())
                    {
                        rec2 = i;
                    }

                    i = i + 1;
                }

                if (rec1 == 100000)
                {
                    Fechar();
                    MessageBox.Show("Usuário não cadastrado");
                    textBox1.Focus();
                }
                else
                {
                    if (rec1 != rec2)
                    {
                        Fechar();
                        MessageBox.Show("Usuário ou senha inválido");
                        textBox1.Clear();
                        textBox2.Clear();
                        textBox1.Focus();
                    }
                    else if (co.Usuario().Rows[rec1]["acesso"].ToString() == "")
                    {
                        Form2 f2 = new Form2();
                        AuxClas.login = co.Usuario().Rows[rec1]["login"].ToString();
                        AuxClas.usuario = co.Usuario().Rows[rec1]["nome"].ToString();

                        if (co.Usuario().Rows[rec1]["privilegio"].ToString() == "Ok")
                        {
                            AuxClas.privilegio = "Ok";
                        }
                        else
                        {
                            AuxClas.privilegio = "";
                        }
                        f2.Show();
                        f2.Atualizar();
                        this.Hide();
                    }
                    else
                    {
                        AuxClas.usuario = co.Usuario().Rows[rec1]["nome"].ToString();
                        if (co.Usuario().Rows[rec1]["privilegio"].ToString() == "Ok")
                        {
                            AuxClas.privilegio = "Ok";
                        }

                        else
                        {
                            AuxClas.privilegio = "";
                        }
                        AuxClas.auxiliar = aux;


                        mn.Show();
                        mn.Atualizar();


                        this.Hide();
                    }
                }
            }
        }
    }
}
