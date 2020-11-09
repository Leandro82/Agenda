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
    public partial class frmMenu : Form
    {
        string user, aux1, priv1, priv2;
        int aux2, seg = 60, min = 5;
        ConectaAgenda co = new ConectaAgenda();
        ConectaUsuario cs = new ConectaUsuario();

        public frmMenu()
        {
            InitializeComponent();
        }

        public void Atualizar()
        {
            user = AuxClas.usuario;
            aux1 = AuxClas.auxiliar;
            priv1 = AuxClas.privilegio;
            toolStripLabel2.Text = AuxClas.usuario;
        }

        private void frmMenu_Load(object sender, EventArgs e)
        {
            Atualizar();

            timer1.Enabled = true;
            seg = 60;
            min = 04;


            if (priv1 == "Ok")
            {
                controleToolStripMenuItem.Visible = true;

            }
            else
            {
                controleToolStripMenuItem.Visible = false;

            }
        }

        private void privilégiosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var peq = new frmPrivilegio();
            if (Application.OpenForms.OfType<frmPrivilegio>().Count() > 0)
            {
                Application.OpenForms[peq.Name].Focus();
            }
            else
            {
                peq.Show();
            }
        }

        private void cadastroDeUsuáriosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var peq = new frmInserirUsuario();
            if (Application.OpenForms.OfType<frmInserirUsuario>().Count() > 0)
            {
                Application.OpenForms[peq.Name].Focus();
            }
            else
            {
                peq.Show();
            }
        }

        private void cadastrarEventoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //var pr = new Principal();
            var log = new frmLogin();
            frmLogin frm = new frmLogin();

            if (Application.OpenForms.OfType<frmLogin>().Count() > 0)
            {
                Application.OpenForms[frm.Name].Close();
            }

            if (controleToolStripMenuItem.Text == null)
            {
                if (Application.OpenForms.OfType<frmLogin>().Count() > 0)
                {
                    Application.OpenForms[log.Name].Focus();
                }
                else
                {
                    log.Show();
                }
            }
            else
            {
                string rec = toolStripLabel2.Text;
                var cad = new Form1(rec);
                if (Application.OpenForms.OfType<Form1>().Count() > 0)
                {

                    Application.OpenForms[cad.Name].Focus();
                }
                else
                {
                    cad.Show();
                }
            }
        }

        private void alterarExcluirEventoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i = 0;
            int cont = cs.Usuario().Rows.Count - 1;
            string usuario = toolStripLabel2.Text;

            frmLogin frm = new frmLogin();
            if (Application.OpenForms.OfType<frmLogin>().Count() > 0)
            {
                Application.OpenForms[frm.Name].Close();
            }

            if (toolStripLabel2.Text == null)
            {
                var log = new frmLogin();

                if (Application.OpenForms.OfType<frmLogin>().Count() > 0)
                {
                    Application.OpenForms[log.Name].Focus();
                }
                else
                {
                    log.Show();
                }
            }
            else
            {
                string rec2 = toolStripLabel2.Text;

                while (i <= cont)
                {
                    if (cs.Usuario().Rows[i]["nome"].ToString() == usuario)
                    {
                        aux2 = i;
                    }
                    i = i + 1;
                }

                if (cs.Usuario().Rows[aux2]["privilegio"].ToString() == "Ok")
                {
                    priv2 = "Ok";
                }
                else
                {
                    priv2 = "";
                }

                var peq = new frmPesqEdEventos(rec2, priv2);
                if (Application.OpenForms.OfType<frmPesqEdEventos>().Count() > 0)
                {
                    Application.OpenForms[peq.Name].Focus();
                }
                else
                {
                    peq.Show();
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            seg--;
                 if (seg == 0)
                {
                    seg = 60;
                }
                 
            if (min == 0 && seg == 60)
                 {
                     this.Close();
                 }
                                  
                if (seg == 60)
                 {
                     toolStripLabel4.Text = min.ToString().PadLeft(2, '0') + ":00";
                     min--;
                }
                 else
                 {
                     toolStripLabel4.Text = min.ToString().PadLeft(2, '0') + ":" + seg.ToString().PadLeft(2, '0');
                }
        }


    }
}
