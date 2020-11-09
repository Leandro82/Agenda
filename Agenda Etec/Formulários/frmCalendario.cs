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
using System.Threading;
using System.Collections;
using System.Net;
using System.Web;
using System.Xml.Linq;
using System.Net.NetworkInformation;

namespace Agenda_Etec
{
    public partial class frmCalendario : Form
    {
        int codUs;
        string aviso;
        Agenda ag = new Agenda();
        ConectaAgenda ca = new ConectaAgenda();
        ConectaUsuario co = new ConectaUsuario();
        frmProcesso f = new frmProcesso();
        System.Threading.Thread tFormAguarde;
        public frmCalendario(int cod)
        {
            InitializeComponent();
            codUs = cod;
        }

        Button btnDayz;
        Int32 y = 0;
        Int32 x;
        Int32 ndayz, tipo;
        string Dayofweek, CurrentCulture, dia, mes, dtEv;
        private void CarregaFormAguarde()
        {
            AuxClas.processo = "VERIFICANDO CONEXÃO...";
            f.Atualizar();
            f.ShowDialog();
        }

        public static bool Conexao()
        {
            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead("http://www.google.com"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public void Atualiza()
        {
            ag.TipoPesquisa = "Tudo";
            foreach (DataRow evento in ca.BuscaEventos(ag).Rows)
            {
                if (Convert.ToDateTime(evento["dtEv"].ToString()).ToString("dd/MM/yyyy").Substring(3, 2) == mes)
                {
                    int n = dataGridView1.Rows.Add();
                    dataGridView1.Rows[n].Cells[0].Value = Convert.ToDateTime(evento["dtEv"].ToString()).ToString("dd/MM/yyyy");
                    dataGridView1.Rows[n].Cells[1].Value = evento["evento"].ToString();
                    dataGridView1.Rows[n].Cells[2].Value = "Evento da Etec";
                    dataGridView1.Rows[n].Cells[3].Value = Convert.ToDateTime(evento["dtEv"].ToString()).ToString("dd/MM/yyyy").Substring(3, 2) + Convert.ToDateTime(evento["dtEv"].ToString()).ToString("dd/MM/yyyy").ToString().Substring(0, 2);
                }
            }
            dataGridView1.Sort(dataGridView1.Columns[3], ListSortDirection.Ascending);
        }

        public void Evento()
        {
            ag.TipoPesquisa = "Tudo";
            foreach (DataRow evento in ca.BuscaEventos(ag).Rows)
            {
                if (Convert.ToDateTime(evento["dtEv"].ToString()).ToString("dd/MM/yyyy").Substring(3, 2) == mes)
                {
                    int n = dataGridView1.Rows.Add();
                    dataGridView1.Rows[n].Cells[0].Value = Convert.ToDateTime(evento["dtEv"].ToString()).ToString("dd/MM/yyyy");
                    dataGridView1.Rows[n].Cells[1].Value = evento["evento"].ToString();
                    dataGridView1.Rows[n].Cells[2].Value = "Evento da Etec";
                    dataGridView1.Rows[n].Cells[3].Value = Convert.ToDateTime(evento["dtEv"].ToString()).ToString("dd/MM/yyyy").Substring(3, 2) + Convert.ToDateTime(evento["dtEv"].ToString()).ToString("dd/MM/yyyy").ToString().Substring(0, 2);
                }
            }
        }

        public void Feriados()
        {
            try
            {
                dataGridView1.Rows.Clear();
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                ds.ReadXml("https://api.calendario.com.br/?ano=" + textBox1.Text + "&estado=SP&cidade=OSVALDO_CRUZ&token=bGVhbmRyb3JhZGVzY0BnbWFpbC5jb20maGFzaD0xMTQ0MzA2NzY");
                dt = ds.Tables[1];

                if (comboBox1.Text.Length == 1)
                    mes = "0" + comboBox1.Text;
                else
                    mes = comboBox1.Text;

                int cont = dt.Rows.Count;

                foreach (DataRow item in dt.Rows)
                {
                    if (item["type"].ToString() != "Dia Convencional" && item["date"].ToString().Substring(3, 2) == mes)
                    {
                        int n = dataGridView1.Rows.Add();
                        dataGridView1.Rows[n].Cells[0].Value = item["date"].ToString();
                        dataGridView1.Rows[n].Cells[1].Value = item["name"].ToString();
                        dataGridView1.Rows[n].Cells[2].Value = item["type"].ToString();
                        dataGridView1.Rows[n].Cells[3].Value = item["date"].ToString().Substring(3, 2) + item["date"].ToString().Substring(0, 2);
                    }
                }
                Atualiza();
                dataGridView1.Sort(dataGridView1.Columns[3], ListSortDirection.Ascending);
            }
            catch
            {
                string msg = "Verifique o ANO!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
        }
        
        int VerificaDia()
        {
            DateTime time = Convert.ToDateTime(comboBox1.Text + "/01/" + textBox1.Text);
            //péga o dia de inicio da semana para data informada
            Dayofweek = Application.CurrentCulture.Calendar.GetDayOfWeek(time).ToString();
            if (Dayofweek == "Sunday")
            {
                x = 0;
            }
            else if (Dayofweek == "Monday")
            {
                x = 0 + 75;
                ndayz = 1;
            }
            else if (Dayofweek == "Tuesday")
            {
                x = 0 + 150;
                ndayz = 2;
            }
            else if (Dayofweek == "Wednesday")
            {
                x = 0 + 150 + 75;
                ndayz = 3;
            }
            else if (Dayofweek == "Thursday")
            {
                x = 0 + 150 + 150;
                ndayz = 4;
            }
            else if (Dayofweek == "Friday")
            {
                x = 0 + 150 + 150 + 75;
                ndayz = 5;
            }
            else if (Dayofweek == "Saturday")
            {
                x = 0 + 150 + 150 + 150;
                ndayz = 6;
            }
            return x;
        }

        void btnDayzClick(object sender, EventArgs e)
        {
            Button currentbutton = (Button)sender;
            if (currentbutton.Text.Length == 1 && comboBox1.Text.Length == 1)
                dtEv = "0" + currentbutton.Text + "/" + "0" + comboBox1.Text + "/" + textBox1.Text;
            else if (currentbutton.Text.Length == 1 && comboBox1.Text.Length > 1)
                dtEv = "0" + currentbutton.Text + "/" + comboBox1.Text + "/" + textBox1.Text;
            else if (currentbutton.Text.Length > 1 && comboBox1.Text.Length == 1)
                dtEv = currentbutton.Text + "/" + "0" + comboBox1.Text + "/" + textBox1.Text;
            else if (currentbutton.Text.Length > 1 && comboBox1.Text.Length > 1)
                dtEv = currentbutton.Text + "/" + comboBox1.Text + "/" + textBox1.Text;

            string inicial = co.BuscaDataServidor();
            string aux = dtEv;
            DateTime final = DateTime.ParseExact(aux.ToString(), "dd/MM/yyyy", CultureInfo.CurrentCulture);
            TimeSpan dif = final - Convert.ToDateTime(inicial);
            int dias = dif.Days;

            if (dias < 0)
            {
                string msg = "Você só poderá cadastrar um evento a partir da data de hoje!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else
            {
                if (currentbutton.BackColor == Color.Red)
                {
                    aviso = dtEv + " É FERIADO NACIONAL, DESEJA CONTINUAR?";
                    frmDecisao dec = new frmDecisao(aviso, dtEv, codUs);
                    dec.ShowDialog();
                }
                else if (currentbutton.BackColor == Color.Blue)
                {
                    aviso = dtEv + " É FERIADO ESTADUAL, DESEJA CONTINUAR?";
                    frmDecisao dec = new frmDecisao(aviso, dtEv, codUs);
                    dec.ShowDialog();
                }
                else if (currentbutton.BackColor == Color.Magenta)
                {
                    aviso = dtEv + " É FERIADO MUNICIPAL, DESEJA CONTINUAR?";
                    frmDecisao dec = new frmDecisao(aviso, dtEv, codUs);
                    dec.ShowDialog();
                }
                else if (currentbutton.BackColor == Color.Yellow)
                {
                    aviso = dtEv + " PROVAVELMENTE SERÁ FACULTATIVO, DESEJA CONTINUAR?";
                    frmDecisao dec = new frmDecisao(aviso, dtEv, codUs);
                    dec.ShowDialog();
                }
                else
                {
                    frmCadEvento frm = new frmCadEvento(dtEv, codUs,"");
                    frm.Owner = this;
                    frm.ShowDialog();
                }
            }
        }
       
        private void frmCalendario_Load(object sender, EventArgs e)
        {
            if (Conexao() == false)
            {
                string msg = "Verifique sua conexão com a internet!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
                panel1.Enabled = false;
            }
            else if (Conexao() == true)
            {
                CurrentCulture = Application.CurrentCulture.Name;
                //exibe o mes atual
                comboBox1.Text = DateTime.Now.Month.ToString();
                //exibe o nome completo do mes atual
                label2.Text = Application.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(comboBox1.Text)).ToUpper();
                //altera a cultura para evitar data incorreta
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-us");
                //obtem o nume de dias no mes e ano selecionado
                Int32 Dayz = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                //exibe o ano atual no textbox
                textBox1.Text = DateTime.Now.Year.ToString();
                //chama a função 
                VerificaDia();
                Feriados();
                for (Int32 i = 1; i < Dayz + 1; i++)
                {
                    ndayz += 1;
                    if (Convert.ToString(i).Length == 1)
                    {
                        dia = "0" + Convert.ToString(i) + "/" + "0" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString();
                    }
                    else
                    {
                        dia = Convert.ToString(i) + "/" + "0" + Convert.ToString(DateTime.Now.Month.ToString()) + "/" + Convert.ToString(DateTime.Now.Year.ToString());
                    }
                    btnDayz = new Button();
                    btnDayz.Click += new EventHandler(this.btnDayzClick);
                    btnDayz.Name = "B" + i;
                    btnDayz.Text = i.ToString();
                    //btnDayz.BorderStyle = BorderStyle.Fixed3D;
                    for (Int32 j = 0; j < dataGridView1.Rows.Count; j++)
                    {
                        if (dia.TrimEnd() == dataGridView1.Rows[j].Cells[0].Value.ToString().TrimEnd())
                        {
                            if (dataGridView1.Rows[j].Cells[2].Value.ToString() == "Feriado Nacional")
                            {
                                tipo = 1;
                            }
                            else if (dataGridView1.Rows[j].Cells[2].Value.ToString() == "Feriado Estadual")
                            {
                                tipo = 2;
                            }
                            else if (dataGridView1.Rows[j].Cells[2].Value.ToString() == "Feriado Municipal")
                            {
                                tipo = 3;
                            }
                            else if (dataGridView1.Rows[j].Cells[2].Value.ToString() == "Facultativo")
                            {
                                tipo = 4;
                            }
                        }
                    }
                    if (tipo == 1)
                    {
                        btnDayz.BackColor = Color.Red;
                    }
                    else if (tipo == 2)
                    {
                        btnDayz.BackColor = Color.Blue;
                    }
                    else if (tipo == 3)
                    {
                        btnDayz.BackColor = Color.Magenta;
                    }
                    else if (tipo == 4)
                    {
                        btnDayz.BackColor = Color.Yellow;
                    }
                    else if (i == DateTime.Now.Day)
                    {
                        btnDayz.BackColor = Color.Green;
                    }
                    else if (ndayz == 01)
                    {
                        btnDayz.BackColor = Color.LightSalmon;
                    }
                    else
                    {
                        btnDayz.BackColor = Color.Aquamarine;
                    }
                    btnDayz.Font = label1.Font;
                    btnDayz.SetBounds(x, y, 70, 60);
                    x += 75;
                    if (ndayz == 7)
                    {
                        x = 0;
                        ndayz = 0;
                        y += 62;
                    }
                    panel2.Controls.Add(btnDayz);
                    tipo = 0;
                }
                x = 0;
                ndayz = 0;
                y = 0;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 mesAtual, anoAtual;
                anoAtual = Convert.ToInt32(textBox1.Text);
                mesAtual = Convert.ToInt32(comboBox1.Text);
                if (mesAtual == 1)
                {
                    anoAtual -= 1;
                    mesAtual = 12;
                    textBox1.Text = anoAtual.ToString();
                }
                else
                {
                    mesAtual -= 1;
                    comboBox1.Text = mesAtual.ToString();
                }

                comboBox1.Text = mesAtual.ToString();
                if (comboBox1.Text.Length == 1)
                    mes = "0" + comboBox1.Text;
                else
                    mes = comboBox1.Text;

                Feriados();
                //remove all the controls in the panel
                panel2.Controls.Clear();
                Thread.CurrentThread.CurrentCulture = new CultureInfo(CurrentCulture);
                //display the selected month's fullname
                label2.Text = Application.CurrentCulture.DateTimeFormat.GetMonthName(mesAtual).ToUpper();
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-za");
                Int32 Dayz = DateTime.DaysInMonth(Convert.ToInt32(textBox1.Text), Convert.ToInt32(comboBox1.Text));
                VerificaDia();
                for (Int32 i = 1; i < Dayz + 1; i++)
                {
                    ndayz += 1;
                    if (Convert.ToString(i).Length == 1)
                    {
                        dia = "0" + Convert.ToString(i) + "/" + mes + "/" + textBox1.Text;
                    }
                    else
                    {
                        dia = Convert.ToString(i) + "/" + mes + "/" + textBox1.Text;
                    }
                    var btnDayz = new Button();
                    btnDayz.Click += new EventHandler(this.btnDayzClick);
                    btnDayz.Text = i.ToString();
                    for (Int32 j = 0; j < dataGridView1.Rows.Count; j++)
                    {
                        if (dia.TrimEnd() == dataGridView1.Rows[j].Cells[0].Value.ToString().TrimEnd())
                        {
                            if (dataGridView1.Rows[j].Cells[2].Value.ToString() == "Feriado Nacional")
                            {
                                tipo = 1;
                            }
                            else if (dataGridView1.Rows[j].Cells[2].Value.ToString() == "Feriado Estadual")
                            {
                                tipo = 2;
                            }
                            else if (dataGridView1.Rows[j].Cells[2].Value.ToString() == "Feriado Municipal")
                            {
                                tipo = 3;
                            }
                            else if (dataGridView1.Rows[j].Cells[2].Value.ToString() == "Facultativo")
                            {
                                tipo = 4;
                            }
                        }
                    }
                    //btnDayz.BorderStyle = BorderStyle.Fixed3D;
                    Int32 mon = Convert.ToInt32(comboBox1.Text);
                    Int32 year = Convert.ToInt32(textBox1.Text);
                    if (tipo == 1)
                    {
                        btnDayz.BackColor = Color.Red;
                    }
                    else if (tipo == 2)
                    {
                        btnDayz.BackColor = Color.Blue;
                    }
                    else if (tipo == 3)
                    {
                        btnDayz.BackColor = Color.Magenta;
                    }
                    else if (tipo == 4)
                    {
                        btnDayz.BackColor = Color.Yellow;
                    }
                    else if ((i == DateTime.Now.Day) && (mon == DateTime.Now.Month) && (year == DateTime.Now.Year))
                    {
                        //the current day must be highlighted differently
                        btnDayz.BackColor = Color.Green;
                    }
                    else if (ndayz == 01)
                    {
                        btnDayz.BackColor = Color.LightSalmon;
                    }
                    else
                    {
                        //set this color for other days in the selected month
                        btnDayz.BackColor = Color.Aquamarine;
                    }
                    btnDayz.Font = label1.Font;
                    btnDayz.SetBounds(x, y, 70, 60);

                    x += 75;
                    if (ndayz == 7)
                    {
                        x = 0;
                        ndayz = 0;
                        y += 62;
                    }
                    panel2.Controls.Add(btnDayz);
                    tipo = 0;
                }
                x = 0;
                ndayz = 0;
                y = 0;
            }
            catch (FormatException)
            {
                string msg = "Data inválida!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
                textBox1.Focus();
            }
            catch (NullReferenceException)
            {
                string msg = "Data inválida!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
                textBox1.Focus();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((comboBox1.Text == null) || (textBox1.Text == null))
            {
                string msg = "O ano ou o mês estão incorretos!!";
                frmMensagem mg = new frmMensagem(msg);
                mg.ShowDialog();
            }
            else
            {
                try
                {
                    Int32 t = Convert.ToInt32(textBox1.Text);
                    if ((textBox1.Text != "0") || (t < 1))
                    {
                        //remove todos os controles do painel
                        panel2.Controls.Clear();
                        Thread.CurrentThread.CurrentCulture = new CultureInfo(CurrentCulture);
                        //exibe o nome completo do mes selecionado
                        label2.Text = Application.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(comboBox1.Text)).ToUpper();
                        Thread.CurrentThread.CurrentCulture = new CultureInfo("en-us");
                        Int32 Dayz = DateTime.DaysInMonth(Convert.ToInt32(textBox1.Text), Convert.ToInt32(comboBox1.Text));
                        if (comboBox1.Text.Length == 1)
                            mes = "0" + comboBox1.Text;
                        else
                            mes = comboBox1.Text;

                        VerificaDia();
                        Feriados();
                        for (Int32 i = 1; i < Dayz + 1; i++)
                        {
                            ndayz += 1;
                            if (Convert.ToString(i).Length == 1)
                            {
                                dia = "0" + Convert.ToString(i) + "/" + mes + "/" + textBox1.Text;
                            }
                            else
                            {
                                dia = Convert.ToString(i) + "/" + mes + "/" + textBox1.Text;
                            }
                            btnDayz = new Button();
                            btnDayz.Click += new EventHandler(this.btnDayzClick);
                            btnDayz.Text = i.ToString();
                            for (Int32 j = 0; j < dataGridView1.Rows.Count; j++)
                            {
                                if (dia.TrimEnd() == dataGridView1.Rows[j].Cells[0].Value.ToString().TrimEnd())
                                {
                                    if (dataGridView1.Rows[j].Cells[2].Value.ToString() == "Feriado Nacional")
                                    {
                                        tipo = 1;
                                    }
                                    else if (dataGridView1.Rows[j].Cells[2].Value.ToString() == "Feriado Estadual")
                                    {
                                        tipo = 2;
                                    }
                                    else if (dataGridView1.Rows[j].Cells[2].Value.ToString() == "Feriado Municipal")
                                    {
                                        tipo = 3;
                                    }
                                    else if (dataGridView1.Rows[j].Cells[2].Value.ToString() == "Facultativo")
                                    {
                                        tipo = 4;
                                    }
                                }
                            }
                            //btnDayz.BorderStyle = BorderStyle.Fixed3D;
                            Int32 mon = Convert.ToInt32(comboBox1.Text);
                            Int32 year = Convert.ToInt32(textBox1.Text);
                            if (tipo == 1)
                            {
                                btnDayz.BackColor = Color.Red;
                            }
                            else if (tipo == 2)
                            {
                                btnDayz.BackColor = Color.Blue;
                            }
                            else if (tipo == 3)
                            {
                                btnDayz.BackColor = Color.Magenta;
                            }
                            else if (tipo == 4)
                            {
                                btnDayz.BackColor = Color.Yellow;
                            }
                            else if ((i == DateTime.Now.Day) && (mon == DateTime.Now.Month) && (year == DateTime.Now.Year))
                            {
                                //destaca o dia atual com cor diferente
                                btnDayz.BackColor = Color.Green;
                            }
                            else if (ndayz == 01)
                            {
                                btnDayz.BackColor = Color.LightSalmon;
                            }
                            else
                            {
                                //define a cor para outros dias do mes selecionado
                                btnDayz.BackColor = Color.Aquamarine;
                            }
                            btnDayz.Font = label1.Font;
                            btnDayz.SetBounds(x, y, 70, 60);

                            x += 75;
                            if (ndayz == 7)
                            {
                                x = 0;
                                ndayz = 0;
                                y += 62;
                            }
                            panel2.Controls.Add(btnDayz);
                            tipo = 0;
                        }
                        x = 0;
                        ndayz = 0;
                        y = 0;
                    }
                    else
                    {
                        string msg = "O valor deve estar entre 0 e 9999!!";
                        frmMensagem mg = new frmMensagem(msg);
                        mg.ShowDialog();
                        textBox1.Focus();
                    }
                }
                catch (FormatException)
                {
                    string msg = "O ano deve estar entre 0 e 9999!!";
                    frmMensagem mg = new frmMensagem(msg);
                    mg.ShowDialog();
                    textBox1.Focus();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 mesAtual, anoAtual;
                anoAtual = Convert.ToInt32(textBox1.Text);
                mesAtual = Convert.ToInt32(comboBox1.Text);
                if (mesAtual == 12)
                {
                    anoAtual += 1;
                    mesAtual = 1;
                    textBox1.Text = anoAtual.ToString();
                    comboBox1.Text = mesAtual.ToString();
                }
                else
                {
                    mesAtual += 1;
                    comboBox1.Text = mesAtual.ToString();
                }

                if (comboBox1.Text.Length == 1)
                    mes = "0" + comboBox1.Text;
                else
                    mes = comboBox1.Text;
                Feriados();
                //remove todos os controles do painel
                panel2.Controls.Clear();
                Thread.CurrentThread.CurrentCulture = new CultureInfo(CurrentCulture);
                //exibe o nome completo do mes selecionado
                label2.Text = Application.CurrentCulture.DateTimeFormat.GetMonthName(mesAtual).ToUpper();
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-za");
                Int32 Dayz = DateTime.DaysInMonth(Convert.ToInt32(textBox1.Text), Convert.ToInt32(comboBox1.Text));
                VerificaDia();
                for (Int32 i = 1; i < Dayz + 1; i++)
                {
                    ndayz += 1;
                    if (Convert.ToString(i).Length == 1)
                    {
                        dia = "0" + Convert.ToString(i) + "/" + mes + "/" + textBox1.Text;
                    }
                    else
                    {
                        dia = Convert.ToString(i) + "/" + mes + "/" + textBox1.Text;
                    }
                    btnDayz = new Button();
                    btnDayz.Click += new EventHandler(this.btnDayzClick);
                    btnDayz.Text = i.ToString();
                    for (Int32 j = 0; j < dataGridView1.Rows.Count; j++)
                    {
                        if (dia.TrimEnd() == dataGridView1.Rows[j].Cells[0].Value.ToString().TrimEnd())
                        {
                            if (dataGridView1.Rows[j].Cells[2].Value.ToString() == "Feriado Nacional")
                            {
                                tipo = 1;
                            }
                            else if (dataGridView1.Rows[j].Cells[2].Value.ToString() == "Feriado Estadual")
                            {
                                tipo = 2;
                            }
                            else if (dataGridView1.Rows[j].Cells[2].Value.ToString() == "Feriado Municipal")
                            {
                                tipo = 3;
                            }
                            else if (dataGridView1.Rows[j].Cells[2].Value.ToString() == "Facultativo")
                            {
                                tipo = 4;
                            }
                        }
                    }
                    //lblDayz.BorderStyle = BorderStyle.Fixed3D;
                    Int32 mon = Convert.ToInt32(comboBox1.Text);
                    Int32 year = Convert.ToInt32(textBox1.Text);
                    if (tipo == 1)
                    {
                        btnDayz.BackColor = Color.Red;
                    }
                    else if (tipo == 2)
                    {
                        btnDayz.BackColor = Color.Blue;
                    }
                    else if (tipo == 3)
                    {
                        btnDayz.BackColor = Color.Magenta;
                    }
                    else if (tipo == 4)
                    {
                        btnDayz.BackColor = Color.Yellow;
                    }
                    else if ((i == DateTime.Now.Day) && (mon == DateTime.Now.Month) && (year == DateTime.Now.Year))
                    {
                        //o dia atual deve ser destacado com cor diferente
                        btnDayz.BackColor = Color.Green;

                    }
                    else if (ndayz == 01)
                    {
                        btnDayz.BackColor = Color.LightSalmon;
                    }
                    else
                    {
                        //define a cor dos outros dias do mes
                        btnDayz.BackColor = Color.Aquamarine;
                    }
                    btnDayz.Font = label1.Font;
                    btnDayz.SetBounds(x, y, 70, 60);

                    x += 75;
                    if (ndayz == 7)
                    {
                        x = 0;
                        ndayz = 0;
                        y += 62;
                    }
                    panel2.Controls.Add(btnDayz);
                    tipo = 0;
                }
                x = 0;
                ndayz = 0;
                y = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                textBox1.Focus();
            }
        }
    }
}
