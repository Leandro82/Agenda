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


namespace Teste
{
    class CriaUsuario
    {
        public MySqlConnection conexao;
        string acesso = "";

        public DataTable Usuario()
        {
            Conecta cl = new Conecta();
            MySqlConnection con = new MySqlConnection("Persist Security Info=false;SERVER=10.66.122.42;DATABASE=agenda;UID=secac;pwd=secac");
            string vSQL = "Select * FROM usuario";
            MySqlDataAdapter vDataAdapter = new MySqlDataAdapter(vSQL, con);
            DataTable vTable = new DataTable();
            vDataAdapter.Fill(vTable);
            return vTable;
        }


        
        public void cadastro(Usuario us)
        {
            us.Acesso = acesso;
            string caminho = "Persist Security Info=false;SERVER=10.66.122.42;DATABASE=agenda;UID=secac;pwd=secac";
            //string caminho = "Persist Security Info=false;SERVER=localhost;DATABASE=agenda;UID=root;pwd=";

            try
            {
                conexao = new MySqlConnection(caminho);
                conexao.Open();

                string inserir = "INSERT INTO usuario(nome, login, senha, acesso)VALUES('"+us.Nome+"','"+us.Login+"','"+us.Senha+"','"+us.Acesso+"')";

                MySqlCommand comandos = new MySqlCommand(inserir, conexao);



                comandos.ExecuteNonQuery();

                conexao.Close();
            }
            catch (Exception ex)
            {

                throw new Exception("Erro de comandos: " + ex.Message);
            }
        }
    }
}
