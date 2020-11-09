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
    public partial class frmPrincipal : Form
    {
        TabPage tab = null;
        ConectaUsuario co = new ConectaUsuario();
        ConectaAgenda ca = new ConectaAgenda();
        Agenda ag = new Agenda();
        Usuario us = new Usuario();
        int min, seg, codUs, minuto, segundo, hora, aux;
        string evHoje,HoraAux, MinAux, HoraEv, nt;
        public frmPrincipal()
        {
            InitializeComponent();
        }

        public int VerificaFormAberto(string fm)
        {
            int quant = 0;
            int cont = tabControl1.TabPages.Count;
            for(Int32 i = 0; i < cont; i++)
            {
                if (tabControl1.TabPages[i].Text == fm)
                    quant = 1;
            }
            return quant;
        }
        public void Atualizar()
        {
            nt = AuxClas.icone;
        }
     
        public void PosLogado(string login, string senha)
        {
            us.Login = login;
            us.Senha = senha;
            if (co.PrimeiroAcesso(us).Rows[0][4].ToString() == "Não")
            {
                frmDadosLogin frm = new frmDadosLogin(co.PrimeiroAcesso(us).Rows[0][0].GetHashCode(), co.PrimeiroAcesso(us).Rows[0][1].ToString(), login);
                frm.Show();
                frm.Owner = this;
            }
            else
            {
                toolStripLabel1.Visible = true;
                toolStripLabel2.Visible = true;
                toolStripLabel6.Visible = true;
                toolStripLabel7.Visible = true;
                toolStripLabel2.Text = co.PrimeiroAcesso(us).Rows[0][1].ToString();
                codUs = co.PrimeiroAcesso(us).Rows[0][0].GetHashCode();
                cadastrosToolStripMenuItem.Visible = true;
                if (co.PrimeiroAcesso(us).Rows[0][5].ToString() != "Secretaria Acadêmica" && co.PrimeiroAcesso(us).Rows[0][5].ToString() != "Diretoria de Serviços")
                    usuárioToolStripMenuItem.Visible = false;
                else
                    usuárioToolStripMenuItem.Visible = true;
                fazerLoginToolStripMenuItem.Text = "Deslogar/Sair";
                min = 10;
                seg = 60;
                timer1.Start();
            }
        }

        public void PrimeiroAcesso(string login, string senha)
        {
            us.Login = login;
            us.Senha = senha;
            toolStripLabel1.Visible = true;
            toolStripLabel2.Visible = true;
            toolStripLabel6.Visible = true;
            toolStripLabel7.Visible = true;
            toolStripLabel2.Text = co.PrimeiroAcesso(us).Rows[0][1].ToString();
            codUs = co.PrimeiroAcesso(us).Rows[0][0].GetHashCode();
            cadastrosToolStripMenuItem.Visible = true;
            if (co.PrimeiroAcesso(us).Rows[0][5].ToString() != "Secretaria Acadêmica" && co.PrimeiroAcesso(us).Rows[0][5].ToString() != "Diretoria de Serviços")
                usuárioToolStripMenuItem.Visible = false;
            else
                usuárioToolStripMenuItem.Visible = true;
            fazerLoginToolStripMenuItem.Text = "Deslogar/Sair";
            min = 10;
            seg = 60;
            timer1.Start();
        }
        public void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.ShowInTaskbar = true;
            notifyIcon1.Visible = false;
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
                hora = Convert.ToInt32(Convert.ToDateTime(co.BuscaHoraServidor().ToString()).ToString("HH"));
                minuto = Convert.ToInt32(Convert.ToDateTime(co.BuscaHoraServidor().ToString()).ToString("mm"));
                segundo = Convert.ToInt32(Convert.ToDateTime(co.BuscaHoraServidor().ToString()).ToString("ss"));
                HoraAux = Convert.ToString(hora) + ":" + Convert.ToString(minuto) + ":00";
                timer2.Start();
                DateTime data = Convert.ToDateTime(co.BuscaDataServidor());
                toolStripLabel5.Text = Convert.ToString(data.ToString("dd/MM/yyyy"));
                toolStripLabel1.Visible = false;
                toolStripLabel2.Visible = false;
                toolStripLabel6.Visible = false;
                toolStripLabel7.Visible = false;
                cadastrosToolStripMenuItem.Visible = false;
                notifyIcon1.BalloonTipText = "o Agenda Etec está oculto, caso queira usá-lo, é só procurar abaixo em MOSTRAR ÍCONES OCULTOS.";
                notifyIcon1.BalloonTipTitle = "Aviso";
                notifyIcon1.Visible = false;
                ag.TipoPesquisa = "Data";
                ag.DataEvento = Convert.ToDateTime(co.BuscaDataServidor().ToString()).ToString("dd/MM/yyyy");
            if (ca.BuscaEventos(ag).Rows.Count >= 1)
            {
                DialogResult escolha = MessageBox.Show("Deseja ver?", "Teremos " + ca.BuscaEventos(ag).Rows.Count + " evento(s) hoje", MessageBoxButtons.YesNo);
                if (escolha == DialogResult.Yes)
                {
                    evHoje = "Hoje";
                    var peq = new frmPesqEventos(evHoje, "", "");
                    peq.Owner = this;
                    peq.MdiParent = this;
                    peq.Dock = DockStyle.Fill;
                    peq.Show();
                    tabControl1.TabPages.Add(peq.Text);
                    this.tabControl1.SelectedIndex = tabControl1.TabPages.Count - 1;
                    this.MdiChildren[tabControl1.SelectedIndex].Activate();
                }
            }
        }

        private void frmPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (tabControl1.TabPages.Count >= 1)
            {
                if (tabControl1.TabPages[tabControl1.SelectedIndex].Text == this.MdiChildren[tabControl1.SelectedIndex].Text)
                {
                    this.MdiChildren[tabControl1.SelectedIndex].Activate();
                }
            }
        }

        private void frmPrincipal_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(10000);
                this.ShowInTaskbar = false;
            }
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            {
                toolTip1.SetToolTip(tabControl1, "Dê dois cliques para fechar");
            }
        }

        private void FrmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            switch (e.CloseReason)
            {
                case CloseReason.UserClosing:
                    {
                        e.Cancel = true;
                        MessageBox.Show("Clique em FECHAR na barra de menu");
                        break;
                    }
                case CloseReason.WindowsShutDown:
                    {
                        // Windows está sendo desligado
                        break;
                    }
            }
        }

        private void tabControl1_DoubleClick(object sender, EventArgs e)
        {
            if (tabControl1.TabPages.Count >= 1)
            {
                this.MdiChildren[tabControl1.SelectedIndex].Close();
                this.tabControl1.TabPages.Remove(tabControl1.SelectedTab);
            }
        }

        private void fecharToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int tabPage = tabControl1.TabPages.Count;
            for (Int32 i = 0; i < tabPage; i++)
            {
                this.MdiChildren[tabControl1.SelectedIndex].Close();
                this.tabControl1.TabPages.Remove(tabControl1.SelectedTab);
            }
            this.WindowState = FormWindowState.Minimized;
        }

        private void restaurarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.ShowInTaskbar = true;
            notifyIcon1.Visible = false;
        }

        private void fazerLoginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fazerLoginToolStripMenuItem.Text == "Deslogar/Sair")
            {
                timer1.Stop();
                fazerLoginToolStripMenuItem.Text = "Fazer Login";
                cadastrosToolStripMenuItem.Visible = false;
                toolStripLabel1.Visible = false;
                toolStripLabel2.Text = "";
                toolStripLabel2.Visible = false;
                toolStripLabel6.Visible = false;
                toolStripLabel7.Visible = false;
                int tabPage = tabControl1.TabPages.Count;
                for (Int32 i = 0; i < tabPage; i++)
                {
                    this.MdiChildren[tabControl1.SelectedIndex].Close();
                    this.tabControl1.TabPages.Remove(tabControl1.SelectedTab);
                }
            }
            else
            {
                var peq = new frmLogin();
                if (Application.OpenForms.OfType<frmLogin>().Count() > 0)
                {
                    Application.OpenForms[peq.Name].Focus();
                }
                else
                {
                    peq.Owner = this;
                    peq.Show();
                }
            }
        }

        private void cadastrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var peq = new frmCalendario(codUs);
            if (VerificaFormAberto(peq.Text) == 0)
            {
                peq.Owner = this;
                peq.Dock = DockStyle.Fill;
                peq.WindowState = FormWindowState.Maximized;
                peq.MdiParent = this;
                peq.Show();
                tabControl1.TabPages.Add(peq.Text);
                this.tabControl1.SelectedIndex = tabControl1.TabPages.Count - 1;
                this.MdiChildren[tabControl1.SelectedIndex].Activate();
            }
            else
            {
                string msg = "O Formulário " + peq.Text + " já está aberto";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
        }

        private void atualizarExcluirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var peq = new frmAtExEvento(codUs);
            if (VerificaFormAberto(peq.Text) == 0)
            {
                peq.Owner = this;
                peq.Dock = DockStyle.Fill;
                peq.WindowState = FormWindowState.Maximized;
                peq.MdiParent = this;
                peq.Show();
                tabControl1.TabPages.Add(peq.Text);
                this.tabControl1.SelectedIndex = tabControl1.TabPages.Count - 1;
                this.MdiChildren[tabControl1.SelectedIndex].Activate();
            }
            else
            {
                string msg = "O Formulário " + peq.Text + " já está aberto";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            segundo += 1;
            if(segundo == 60)
            {
                segundo = 0;
                minuto += 1;
                if (minuto == 60)
                {
                    minuto = 0;
                    hora += 1;
                    if (hora == 24)
                        hora = 0;
                }
                if (Convert.ToString(Convert.ToDateTime(co.BuscaHoraServidor()).ToString("mm")).Length == 2)
                    MinAux = Convert.ToString(Convert.ToDateTime(co.BuscaHoraServidor()).ToString("mm")).Substring(1, 1);
                if (Convert.ToInt32(MinAux) == 0 || Convert.ToInt32(MinAux) == 5)
                {
                    ag.DataEvento = co.BuscaDataServidor();
                    ag.TipoPesquisa = "Horário";
                    ag.HoraEvento = Convert.ToString(hora) + ":" + Convert.ToString(minuto) + ":00";
                    HoraEv = ag.HoraEvento;
                    ag.HoraAuxiliar = HoraAux;
                    evHoje = "Agora";
                    var peq = new frmPesqEventos(evHoje, HoraAux, HoraEv);
                    if (ca.BuscaEventos(ag).Rows.Count >= 1)
                    {
                        DialogResult escolha = MessageBox.Show("Deseja ver?", "Temos " + ca.BuscaEventos(ag).Rows.Count + " evento(s) acontecendo neste momento.", MessageBoxButtons.YesNo);
                        if (escolha == DialogResult.Yes)
                        {
                            this.WindowState = FormWindowState.Maximized;
                            int cont = tabControl1.TabPages.Count;
                            for (Int32 i = 0; i < cont; i++)
                            {
                                if (this.tabControl1.TabPages[i].Text == "Consultar Agenda")
                                {
                                    this.tabControl1.SelectedIndex = i;
                                }
                            }
                            peq.Owner = this;
                            peq.Dock = DockStyle.Fill;
                            peq.WindowState = FormWindowState.Maximized;
                            peq.MdiParent = this;
                            peq.Show();
                            tabControl1.TabPages.Add(peq.Text);
                            this.tabControl1.SelectedIndex = tabControl1.TabPages.Count - 1;
                            //this.MdiChildren[tabControl1.SelectedIndex].Activate();
                        }
                    }
                    HoraAux = Convert.ToString(hora) + ":" + Convert.ToString(minuto + 1) + ":00";
                }
            }
        }

        private void consultarAgendaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var peq = new frmPesqEventos("", "", "");

            if (VerificaFormAberto(peq.Text) == 0)
            {
                peq.Owner = this;
                peq.Dock = DockStyle.Fill;
                peq.WindowState = FormWindowState.Maximized;
                peq.MdiParent = this;
                peq.Show();
                tabControl1.TabPages.Add(peq.Text);
                this.tabControl1.SelectedIndex = tabControl1.TabPages.Count - 1;
                this.MdiChildren[tabControl1.SelectedIndex].Activate();
            }
            else
            {
                string msg = "O Formulário "+peq.Text + " já está aberto";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }

        }

        private void usuárioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var peq = new frmCadUsuario();
            if (VerificaFormAberto(peq.Text) == 0)
            {
                peq.Owner = this;
                peq.Dock = DockStyle.Fill;
                peq.WindowState = FormWindowState.Maximized;
                peq.MdiParent = this;
                peq.Show();
                tabControl1.TabPages.Add(peq.Text);
                this.tabControl1.SelectedIndex = tabControl1.TabPages.Count - 1;
                this.MdiChildren[tabControl1.SelectedIndex].Activate();
            }
            else
            {
                string msg = "O Formulário " + peq.Text + " já está aberto";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        { 
            if (seg == 0 || seg == 60)
            {
                seg = 60;
                if (seg == 60)
                {
                    toolStripLabel7.Text = Convert.ToString(min) + ":00";
                    if (min == 0)
                    {
                        timer1.Stop();
                        fazerLoginToolStripMenuItem.Text = "Fazer Login";
                        cadastrosToolStripMenuItem.Visible = false;
                        toolStripLabel1.Visible = false;
                        toolStripLabel2.Text = "";
                        toolStripLabel2.Visible = false;
                        toolStripLabel6.Visible = false;
                        toolStripLabel7.Visible = false;
                        int tabPage = tabControl1.TabPages.Count;
                        for (Int32 i = 0; i < tabPage; i++)
                        {
                            this.MdiChildren[tabControl1.SelectedIndex].Close();
                            this.tabControl1.TabPages.Remove(tabControl1.SelectedTab);
                        }
                    }
                }
                min -= 1;
            }
            else
            {
                if(Convert.ToString(seg).Length == 1)
                    toolStripLabel7.Text = Convert.ToString(min) + ":0" + Convert.ToString(seg);
                else
                    toolStripLabel7.Text = Convert.ToString(min) + ":" + Convert.ToString(seg);
            }
            seg -= 1;
        }
    }
}
