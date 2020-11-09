using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;


namespace Agenda_Etec
{
    class ConectaUsuario
    {
        public MySqlConnection conexao;

        public string Endereco()
        {
            StringConexao str = new StringConexao();
            return str.Endereco();
        }

        public void Cadastro(Usuario us)
        {
            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string inserir = "INSERT INTO usuario(nome, email, login, senha, funcao, coordenacao, situacao, acesso)VALUES('" + us.Nome + "','" + us.Email + "','" + us.Login + "','" + us.Senha + "','" + us.Profissao1 + "','" + us.Profissao2 + "', '" + us.Situacao + "','" + us.Acesso + "')";
                MySqlCommand comandos = new MySqlCommand(inserir, conexao);
                comandos.ExecuteNonQuery();
                conexao.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de comandos: " + ex.Message);
            }
        }

        public void AtualizarDadosPessoais(Usuario us)
        {
            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string alterar = "UPDATE usuario SET nome='" + us.Nome + "',email='" + us.Email + "',funcao = '" + us.Profissao1 + "',coordenacao = '" + us.Profissao2 + "',situacao = '" + us.Situacao + "'WHERE codUs = '" + us.Codigo + "'";
                MySqlCommand comandos = new MySqlCommand(alterar, conexao);
                comandos.ExecuteNonQuery();
                conexao.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de comandos: " + ex.Message);
            }
        }

        public void AtualizarAcesso(Usuario us)
        {
            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string alterar = "UPDATE usuario SET login='" + us.Login + "',senha='" + us.Senha + "',acesso = '" + us.Acesso + "'WHERE codUs = '" + us.Codigo + "'";
                MySqlCommand comandos = new MySqlCommand(alterar, conexao);
                comandos.ExecuteNonQuery();
                conexao.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de comandos: " + ex.Message);
            }
        }

        public void ExcluirUsuario(Usuario us)
        {
            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string alterar = "DELETE FROM usuario WHERE codUs = '" + us.Codigo + "'";
                MySqlCommand comando = new MySqlCommand(alterar, conexao);
                comando.ExecuteNonQuery();
                conexao.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de comandos: " + ex.Message);
            }
        }

        public DataTable SelecionaUsuario(Usuario us)
        {
            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string carrega = "SELECT codUs, nome, email, login, funcao, coordenacao, situacao, acesso FROM usuario WHERE nome LIKE'%"+us.Nome+"%' ORDER BY nome";
                MySqlDataAdapter comandos = new MySqlDataAdapter(carrega, conexao);
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

        public DataTable SelecionaUsLogado(Usuario us)
        {
            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string carrega = "SELECT codUs, nome, funcao, coordenacao FROM usuario WHERE codUs='" + us.Codigo + "'";
                MySqlDataAdapter comandos = new MySqlDataAdapter(carrega, conexao);
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

        public Boolean VerificaLogin(Usuario us)
        {
            bool result = false;
            using (MySqlConnection cn = new MySqlConnection())
            {
                cn.ConnectionString = Endereco();
                try
                {
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM usuario WHERE login= '" + us.Login + "' AND senha= '" + us.Senha + "'", cn);
                    cn.Open();
                    MySqlDataReader dados = cmd.ExecuteReader();
                    result = dados.HasRows;
                }
                catch (MySqlException e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    cn.Close();
                }
            }
            return result;
        }

        public String BuscaNome(Usuario us)
        {
            string nome;
            using (MySqlConnection cn = new MySqlConnection())
            {
                cn.ConnectionString = Endereco();
                try
                {
                    MySqlDataAdapter sda = new MySqlDataAdapter("SELECT nome, login FROM usuario WHERE login= '" + us.Login + "' AND senha='" + us.Senha + "'", cn);
                    cn.Open();
                    DataTable dt = new System.Data.DataTable();
                    sda.Fill(dt);
                    int cont = dt.Rows.Count;
                    if (cont > 0)
                    {
                        nome = dt.Rows[0]["nome"].ToString();
                    }
                    else
                    {
                        nome = "";
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
            }
            return nome;
        }

        public Boolean VerificaAcesso(Usuario us)
        {
            bool acesso = false;
            using (MySqlConnection cn = new MySqlConnection())
            {
                cn.ConnectionString = Endereco();
                try
                {
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM usuario WHERE acesso= 'Sim'AND nome='" + BuscaNome(us) + "'", cn);
                    cn.Open();
                    MySqlDataReader dados = cmd.ExecuteReader();
                    acesso = dados.HasRows;
                }
                catch (MySqlException e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    cn.Close();
                }
            }
            return acesso;
        }

        public DataTable PrimeiroAcesso(Usuario us)
        {
            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string carrega = "SELECT codUs, nome, login, senha, acesso, funcao FROM usuario WHERE login= '" + us.Login + "'AND senha='" + us.Senha + "'";
                MySqlDataAdapter comandos = new MySqlDataAdapter(carrega, conexao);
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

        public String BuscaDataServidor()
        {
            string dia;
            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string carrega = "SELECT CURDATE()";
                MySqlDataAdapter comandos = new MySqlDataAdapter(carrega, conexao);
                DataTable dt = new System.Data.DataTable();
                comandos.Fill(dt);
                conexao.Close();
                dia = dt.Rows[0][0].ToString();
                return dia;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de comandos: " + ex.Message);
            }
        }

        public String BuscaHoraServidor()
        {
            string hora;
            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string carrega = "SELECT CURTIME()";
                MySqlDataAdapter comandos = new MySqlDataAdapter(carrega, conexao);
                DataTable dt = new System.Data.DataTable();
                comandos.Fill(dt);
                conexao.Close();
                hora = dt.Rows[0][0].ToString();
                return hora;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de comandos: " + ex.Message);
            }
        }

        public DataTable selecionaSenha(Usuario us)
        {
            try
            {
                conexao = new MySqlConnection(Endereco());
                conexao.Open();
                string carrega = "SELECT nome, email, senha FROM usuario WHERE email='" + us.Email + "'";
                MySqlDataAdapter comandos = new MySqlDataAdapter(carrega, conexao);
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
