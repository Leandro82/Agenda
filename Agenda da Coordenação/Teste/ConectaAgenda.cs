using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;

namespace Teste
{
    class ConectaAgenda
    {
        public MySqlConnection conexao;
        string caminho = "Persist Security Info=false;SERVER=10.66.122.42;DATABASE=agenda;UID=secac;pwd=secac";

        public void alterarEv(Variavel va)
        {
            try
            {
                MySqlConnection conexao;
                conexao = new MySqlConnection(caminho);
                conexao.Open();
                string alterar = "UPDATE agenda SET nome = '" + va.Nome + "',data = '" + Convert.ToDateTime(va.Data).ToString("yyyy-MM-dd") + "',local = '" + va.Local + "',horario = '" + va.Horario + "',evento = '" + va.Evento + "'WHERE cod = '" + va.Codigo + "'";
                MySqlCommand comando = new MySqlCommand(alterar, conexao);
                comando.ExecuteNonQuery();
                conexao.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de comandos: " + ex.Message);
            }
        }

        public void excluirEv(Variavel va)
        {
            try
            {
                MySqlConnection conexao;
                conexao = new MySqlConnection(caminho);
                conexao.Open();
                string alterar = "DELETE FROM agenda WHERE cod = '" + va.Codigo + "'";
                MySqlCommand comando = new MySqlCommand(alterar, conexao);
                comando.ExecuteNonQuery();
                conexao.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de comandos: " + ex.Message);
            }
        }

        public DataTable Evento()
        {
            conexao = new MySqlConnection(caminho);
            conexao.Open();
            string carrega = "Select * FROM agenda";
            MySqlDataAdapter comandos = new MySqlDataAdapter(carrega, conexao);
            DataTable dt = new System.Data.DataTable();
            comandos.Fill(dt);
            conexao.Close();
            return dt;
        }

        public DataTable Evento1()
        {
            conexao = new MySqlConnection(caminho);
            conexao.Open();
            string carrega = "Select * from agenda where cod = (select MAX(cod) FROM agenda)";
            MySqlDataAdapter comandos = new MySqlDataAdapter(carrega, conexao);
            DataTable dt = new System.Data.DataTable();
            comandos.Fill(dt);
            conexao.Close();
            return dt;
        }

        public DataTable pesqAgenda(Variavel va)
        {
            try
            {
                DataTable dt = new System.Data.DataTable();

                if (va.Auxiliar == "Nome")
                {
                    conexao = new MySqlConnection(caminho);
                    conexao.Open();
                    string carrega = "SELECT cod,nome,dtCad,data,local,horario,evento FROM agenda WHERE evento LIKE '%" + va.Nome + "%'";
                    MySqlDataAdapter comandos = new MySqlDataAdapter(carrega, conexao);
                    comandos.Fill(dt);
                    conexao.Close();
                }
                else if (va.Auxiliar == "Data")
                {
                    conexao = new MySqlConnection(caminho);
                    conexao.Open();
                    string carrega = "SELECT cod,nome,dtCad,data,local,horario,evento FROM agenda WHERE data = '" + Convert.ToDateTime(va.Data).ToString("yyyy/MM/dd") + "'Order By horario";
                    MySqlDataAdapter comandos = new MySqlDataAdapter(carrega, conexao);
                    comandos.Fill(dt);
                    conexao.Close();
                }
                else if (va.Auxiliar == "Intervalo")
                {
                    conexao = new MySqlConnection(caminho);
                    conexao.Open();
                    string carrega = "SELECT * FROM agenda WHERE data BETWEEN'" + Convert.ToDateTime(va.Data).ToString("yyyy/MM/dd") + "'AND'" + Convert.ToDateTime(va.Data2).ToString("yyyy/MM/dd") + "'ORDER BY data, horario";
                    MySqlDataAdapter comandos = new MySqlDataAdapter(carrega, conexao);
                    comandos.Fill(dt);
                    conexao.Close();
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de comandos: " + ex.Message);
            }
        }

        public String BuscaHoraServidor()
        {
            string data;
            using (MySqlConnection cn = new MySqlConnection())
            {
                cn.ConnectionString = caminho;
                try
                {
                    MySqlDataAdapter sda = new MySqlDataAdapter("SELECT CURTIME()", cn);
                    cn.Open();
                    DataTable dt = new System.Data.DataTable();
                    sda.Fill(dt);
                    int cont = dt.Rows.Count;
                    if (cont > 0)
                    {
                        data = dt.Rows[0][0].ToString();
                    }
                    else
                    {
                        data = "";
                    }
                }
                catch (MySqlException e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    cn.Close();
                }
                return data;
            }
        }

        public String BuscaDataServidor()
        {
            string data;
            using (MySqlConnection cn = new MySqlConnection())
            {
                cn.ConnectionString = caminho;
                try
                {
                    MySqlDataAdapter sda = new MySqlDataAdapter("SELECT NOW()", cn);
                    cn.Open();
                    DataTable dt = new System.Data.DataTable();
                    sda.Fill(dt);
                    int cont = dt.Rows.Count;
                    if (cont > 0)
                    {
                        data = dt.Rows[0][0].ToString();
                    }
                    else
                    {
                        data = "";
                    }
                }
                catch (MySqlException e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    cn.Close();
                }
                return data;
            }
        }
    }
}
