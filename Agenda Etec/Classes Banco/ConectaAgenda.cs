using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace Agenda_Etec
{
    class ConectaAgenda
    {
        public MySqlConnection conexao;

        public string Endereco()
        {
            StringConexao str = new StringConexao();
            return str.Endereco();
        }

        public void Cadastro(Agenda ag)
        {
            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string inserir = "INSERT INTO agenda(dtCad, hrCad, dtEv, hrEv, localEv, evento, respCadEv)VALUES('" + Convert.ToDateTime(ag.DataCadastro).ToString("yyyy-MM-dd") + "','" + ag.HoraCadastro + "','" + Convert.ToDateTime(ag.DataEvento).ToString("yyyy-MM-dd") + "','" + ag.HoraEvento + "','" + ag.Local + "','" + ag.Evento + "', '" + ag.RepCadastroEvento + "')";
                MySqlCommand comandos = new MySqlCommand(inserir, conexao);
                comandos.ExecuteNonQuery();
                conexao.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de comandos: " + ex.Message);
            }
        }

        public void AtualizarEvento(Agenda ag)
        {
            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string alterar = "UPDATE agenda SET dtEv='" + Convert.ToDateTime(ag.DataEvento).ToString("yyyy-MM-dd") + "',hrEv='" + ag.HoraEvento + "',localEv = '" + ag.Local + "',evento = '" + ag.Evento + "', respCadEv = '" + ag.RepCadastroEvento + "', dtCad = '" + Convert.ToDateTime(ag.DataCadastro).ToString("yyyy-MM-dd") + "', hrCad = '" + ag.HoraCadastro + "'WHERE codEv = '" + ag.Codigo + "'";
                MySqlCommand comandos = new MySqlCommand(alterar, conexao);
                comandos.ExecuteNonQuery();
                conexao.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de comandos: " + ex.Message);
            }
        }

        public void ExcluirEvento(Agenda ag)
        {
            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string excluir = "DELETE FROM agenda WHERE codEv = '" + ag.Codigo + "'";
                MySqlCommand comando = new MySqlCommand(excluir, conexao);
                comando.ExecuteNonQuery();
                conexao.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de comandos: " + ex.Message);
            }
        }

        public DataTable BuscaEventos(Agenda Ag)
        {
            try
            {
                DataTable dt = new System.Data.DataTable();
                if (Ag.TipoPesquisa == "Data")
                {
                    conexao = new MySqlConnection(Endereco());
                    conexao.Open();
                    string carrega = "SELECT codEv, dtCad, hrCad, dtEv, hrEv, localEv, evento, respCadEv FROM agenda WHERE dtEv= '" + Convert.ToDateTime(Ag.DataEvento).ToString("yyyy-MM-dd") + "'ORDER BY dtEv";
                    MySqlDataAdapter comandos = new MySqlDataAdapter(carrega, conexao);
                    comandos.Fill(dt);
                    conexao.Close();
                }
                else if (Ag.TipoPesquisa == "Evento")
                {
                    conexao = new MySqlConnection(Endereco());
                    conexao.Open();
                    string carrega = "SELECT codEv, dtCad, hrCad, dtEv, hrEv, localEv, evento, respCadEv FROM agenda WHERE evento LIKE '%" + Ag.Evento + "%'ORDER BY dtEv";
                    MySqlDataAdapter comandos = new MySqlDataAdapter(carrega, conexao);
                    comandos.Fill(dt);
                    conexao.Close();
                }
                else if (Ag.TipoPesquisa == "Tudo")
                {
                    conexao = new MySqlConnection(Endereco());
                    conexao.Open();
                    string carrega = "SELECT codEv, dtCad, hrCad, dtEv, hrEv, localEv, evento, respCadEv FROM agenda ORDER BY dtEv";
                    MySqlDataAdapter comandos = new MySqlDataAdapter(carrega, conexao);
                    comandos.Fill(dt);
                    conexao.Close();
                }
                else if (Ag.TipoPesquisa == "Intervalo")
                {
                    conexao = new MySqlConnection(Endereco());
                    conexao.Open();
                    string carrega = "SELECT codEv, dtCad, hrCad, dtEv, hrEv, localEv, evento, respCadEv FROM agenda WHERE dtEv BETWEEN'" + Convert.ToDateTime(Ag.DataEvento).ToString("yyyy-MM-dd") +"' AND '"+ Convert.ToDateTime(Ag.DataAuxiliar).ToString("yyyy-MM-dd") +"'ORDER BY dtEv";
                    MySqlDataAdapter comandos = new MySqlDataAdapter(carrega, conexao);
                    comandos.Fill(dt);
                    conexao.Close();
                }
                else if (Ag.TipoPesquisa == "Horário")
                {
                    conexao = new MySqlConnection(Endereco());
                    conexao.Open();
                    string carrega = "SELECT codEv, dtCad, hrCad, dtEv, STR_TO_DATE(hrEv, '%H:%i') AS hrEv, localEv, evento, respCadEv FROM agenda WHERE dtEv='" + Convert.ToDateTime(Ag.DataEvento).ToString("yyyy-MM-dd") + "' AND (STR_TO_DATE(hrEv, '%H:%i') BETWEEN '" + Convert.ToDateTime(Ag.HoraAuxiliar).ToString("HH:mm:ss") + "' AND '"+ Convert.ToDateTime(Ag.HoraEvento).ToString("HH:mm:ss") +"')";
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

        public DataTable RespCadastro(Agenda Ag)
        {
            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string carrega = "SELECT nome FROM usuario WHERE codUs= '" + Ag.Codigo +"'";                MySqlDataAdapter comandos = new MySqlDataAdapter(carrega, conexao);
                DataTable dt = new System.Data.DataTable();
                comandos.Fill(dt);
                conexao.Close();
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de comandos: " + ex.Message);
            }
        }
    }
}
