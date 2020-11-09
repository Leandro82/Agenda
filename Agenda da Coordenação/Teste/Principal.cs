using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data.OleDb;
using System.Deployment.Application;

namespace Teste
{
    public partial class Principal : Form
    {
        string aux1, nt;
        string priv1;
        string user = "";
        string visualizar = "Ok";
        int seg, pos, d;
        ConectaAgenda co = new ConectaAgenda();
        ConectaUsuario cs = new ConectaUsuario();
        DateTime dataHoje;

        public Principal()
        {
            InitializeComponent();
        }

        public Timer ti = new Timer();

        public void Fechar()
        {
            timer3.Interval = 5000;
            timer3.Start();
        }

        public void Atualizar()
        {
            user = AuxClas.usuario;
            aux1 = AuxClas.auxiliar;
            priv1 = AuxClas.privilegio;
            nt = AuxClas.icone;
        }

        private void Data()
        {
            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-US");
            var horaf = Convert.ToDateTime("12:00 PM");
            string hora = co.BuscaHoraServidor();
            string horateste = String.Format(ci, "{0:hh:mm tt}", Convert.ToDateTime(hora));
            var horain = Convert.ToDateTime(horateste);
            toolStripLabel2.Text = Convert.ToString(horain);
        }

        private void pesquisarEventoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var peq = new frmPesqEventos("","");
            if (Application.OpenForms.OfType<frmPesqEventos>().Count() > 0)
            {
                Application.OpenForms[peq.Name].Focus();
            }
            else
            {
                peq.Show();
            }
        }


         private void Principal_Load(object sender, EventArgs e)
        {
            int i = 0;
            int data = 0;
            int cont = co.Evento().Rows.Count - 1;
            int cont1 = cs.Usuario().Rows.Count - 1;
            d = co.Evento().Rows.Count - 2;
            Data();
            Copias();
            timer2.Enabled = true;
            timer2.Start();
            seg = 180;
            
            Atualizar();

            toolStripLabel5.Text = DateTime.Now.ToShortDateString();

            if (nt == "Ok")
            {
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(10000, "Atenção", "Agenda Etec já está em uso, para restaurar clique duas vezes sobre o ícone", ToolTipIcon.Info);
            }
             
            
             if (aux1 == "aux")
            {

            }
            else
            {
                while (i <= cont)
                {
                    if (co.BuscaDataServidor() == co.Evento().Rows[i]["data"].ToString())
                    {
                        data = data + 1;
                    }
                    i = i + 1;
                }

                if (nt == "Ok")
                {
                }
                else
                {
                    if (data >= 1)
                    {
                        string message = "Temos " + data + " eventos hoje, deseja ver?";
                        string caption = "Aviso";
                        MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                        DialogResult result;

                        result = MessageBox.Show(message, caption, buttons);

                        if (result == System.Windows.Forms.DialogResult.No)
                        {

                        }
                        else
                        {
                            var peq = new frmPesqEventos(co.BuscaDataServidor(), visualizar);
                            if (Application.OpenForms.OfType<frmPesqEventos>().Count() > 0)
                            {
                                Application.OpenForms[peq.Name].Focus();
                            }
                            else
                            {
                                peq.Show();
                                peq.TopMost = true;
                            }
                        }
                    }
                }
            }
        }

        private void testeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 peq = new Form2();
            if (Application.OpenForms.OfType<Form2>().Count() > 0)
            {
                Application.OpenForms[peq.Name].Focus();
            }
            else
            {
                peq.Show();
            }
        }

        private void privilégiosToolStripMenuItem1_Click(object sender, EventArgs e)
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

        private void fazerLoginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLogin frm = new frmLogin();
            
            if (Application.OpenForms.OfType<frmLogin>().Count() > 0)
            {
                Application.OpenForms[frm.Name].Close();
            }

            frm.Show();
        }

        
        private void pesquisarEventoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var peq = new frmPesqEventos("", "");
            if (Application.OpenForms.OfType<frmPesqEventos>().Count() > 0)
            {
                Application.OpenForms[peq.Name].Focus();
            }
            else
            {
                peq.Show();
            }
        }

        private void fazerLoginToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (notifyIcon1.Visible == false)
            {
                notifyIcon1.Visible = false;
            }
            var log = new frmLogin();

            if (Application.OpenForms.OfType<frmLogin>().Count() > 0)
            {
                Application.OpenForms[log.Name].Close();
                if (Application.OpenForms.OfType<frmLogin>().Count() == 0)
                {
                    log.Show();
                }
            }
            else
            {
                log.Show();
            }
        }

        
        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.Visible = true;
            notifyIcon1.Visible = false; 
            this.WindowState = FormWindowState.Maximized;
            this.ShowInTaskbar = true;        
        }

        private void restaurarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Visible = true;
            this.WindowState = FormWindowState.Maximized;
            this.ShowInTaskbar = true;
            notifyIcon1.Visible = false;
        }

        private void sairToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Principal_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            switch (e.CloseReason)
            {
                case CloseReason.UserClosing:
                    {
                        e.Cancel = true;
                        Fechar();
                        MessageBox.Show("Clique em SAIR na barra de menu");
                        break;
                    }
                case CloseReason.WindowsShutDown:
                    {
                        // Windows está sendo desligado
                        break;
                    }
            }
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            toolStripLabel2.Text = Convert.ToDateTime(co.BuscaHoraServidor()).ToLongTimeString();
            /*SendKeys.Send("{ESC}");
            timer1.Stop();*/
        }

        private void Principal_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                this.Hide();
                notifyIcon1.Visible = true;
            }

        }

        public void timer2_Tick(object sender, EventArgs e)
        {
            int quant = 0;
            int i = 0, j = 0,  data1 = 0, hor = 0;
            
            label1.Text = Convert.ToString(seg);
            

            seg--;
            if (seg == 0)
            {
                int cont = co.Evento().Rows.Count - 1;
                if (cont - d >= 2)
                {
                    string message = "Um novo evento foi cadastrado, deseja ver?";
                        string caption = "Aviso";
                        MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                        DialogResult result;

                        result = MessageBox.Show(message, caption, buttons);

                        if (result == System.Windows.Forms.DialogResult.No)
                        {

                        }
                        else
                        {
                            frmNovoEvento ne = new frmNovoEvento();
                            ne.ShowDialog();
                        }
                    d = cont - 1;
                }

                while (i <= cont)
                {
                    if (co.BuscaDataServidor() == co.Evento().Rows[i]["data"].ToString())
                    {
                        data1 = 1;
                    }
                    if (DateTime.Parse(toolStripLabel2.Text).ToString("HH:mm") == co.Evento().Rows[i]["horario"].ToString() && Convert.ToString(co.BuscaDataServidor()) == co.Evento().Rows[i]["data"].ToString())
                    {
                        hor = 1;
                        pos = i;
                        j = j + 1;
                        quant = quant + 1;
                    }
                    i = i + 1;
                }
                seg = 180;
            }
            
            if (data1 == 1 && hor == 1)
            {
               string message = "Temos " + quant + " evento(s) acontecendo agora, deseja ver?";
                        string caption = "Aviso";
                        MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                        DialogResult result;

                        result = MessageBox.Show(message, caption, buttons);

                        if (result == System.Windows.Forms.DialogResult.No)
                        {

                        }
                        else
                        {
                            frm_Acontece fa = new frm_Acontece(quant, pos);
                            fa.Show();
                        }
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            SendKeys.Send("{ESC}");
            timer3.Stop();
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.Visible = false;
            this.ShowInTaskbar = false;
            notifyIcon1.Visible = true;
        }

        private void fazerLoginToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (notifyIcon1.Visible == false)
            {
                notifyIcon1.Visible = false;
            }
            var log = new frmLogin();

            if (Application.OpenForms.OfType<frmLogin>().Count() > 0)
            {
                Application.OpenForms[log.Name].Close();
                if (Application.OpenForms.OfType<frmLogin>().Count() == 0)
                {
                    log.Show();
                }
            }
            else
            {
                log.Show();
            }
        }

        public void Copias()
        {
            copias.Interval = 600000;
            copias.Start();
        }

        private void copias_Tick(object sender, EventArgs e)
        {
            Conecta co = new Conecta();
            int cont = co.AvisoLib().Rows.Count;
            if (cont >= 1)
            {
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(100000, "Atenção Coordenador(a)", "Existe(m) Requisição(es) para ser(em) liberada(s)", ToolTipIcon.Info);
            }
        }

    }
}
