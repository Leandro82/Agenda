using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data.OleDb;
using System.Data;

namespace Teste
{
    class ConectaUsuario
    {

       public MySqlConnection conexao;
        string acesso = "";
        string caminho = "Persist Security Info=false;SERVER=10.66.122.42;DATABASE=agenda;UID=secac;pwd=secac";

        public void cadastro(Usuario us)
        {
            try
            {
                conexao = new MySqlConnection(caminho);
                conexao.Open();
                string inserir = "UPDATE usuario SET senha='" + us.Senha + "' , login = '" + us.Login + "', acesso = '" + us.Acesso + "'WHERE nome = '"+us.Nome+"'";
                MySqlCommand comandos = new MySqlCommand(inserir, conexao);
                comandos.ExecuteNonQuery();
                conexao.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de comandos: " + ex.Message);
            }
        }

        public void excluirUs(Variavel va)
        {
            try
            {
                MySqlConnection conexao;
                conexao = new MySqlConnection(caminho);
                conexao.Open();
                string alterar = "DELETE FROM usuario WHERE nome = '" + va.Nome + "'";
                MySqlCommand comando = new MySqlCommand(alterar, conexao);
                comando.ExecuteNonQuery();
                conexao.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de comandos: " + ex.Message);
            }
        }

        public DataTable Usuarios()
        {
            try
            {
                conexao = new MySqlConnection(caminho);
                conexao.Open();
                string carrega = "SELECT nome, login FROM usuario ORDER BY nome";
                MySqlDataAdapter comandos = new MySqlDataAdapter(carrega, caminho);
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

        public DataTable Usuario()
        {
            conexao = new MySqlConnection(caminho);
            conexao.Open();
            string carrega = "Select * FROM usuario";
            MySqlDataAdapter comandos = new MySqlDataAdapter(carrega, conexao);
            DataTable dt = new System.Data.DataTable();
            comandos.Fill(dt);
            conexao.Close();
            return dt;
        }

        public DataTable usuariosPriv()
        {
            try
            {
                conexao = new MySqlConnection(caminho);
                conexao.Open();
                string carrega = "SELECT nome, privilegio FROM usuario ORDER BY nome";
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

        public void alteraPriv(Variavel va)
        {
            DataTable dt = new System.Data.DataTable();
            try
            {
                if (va.Auxiliar == "True") 
                {
                    conexao = new MySqlConnection(caminho);
                    conexao.Open();
                    string alterar = "UPDATE usuario SET privilegio = '" + va.Evento + "'WHERE nome= '" + va.Nome + "'";
                    MySqlCommand comando = new MySqlCommand(alterar, conexao);
                    comando.ExecuteNonQuery();
                    conexao.Close();
                }
                else if (va.Auxiliar == "False")
                {
                    conexao = new MySqlConnection(caminho);
                    conexao.Open();
                    string alterar = "UPDATE usuario SET privilegio = '" + va.Evento + "'WHERE nome= '" + va.Nome + "'";
                    MySqlCommand comando = new MySqlCommand(alterar, conexao);
                    comando.ExecuteNonQuery();
                    conexao.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de comandos: " + ex.Message);
            }
        }
    }
}
