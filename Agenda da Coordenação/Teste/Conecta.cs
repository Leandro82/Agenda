using System;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;
using System.Drawing.Imaging;
using System.Data.OleDb;
using System.Timers;
using System.Reflection;

namespace Teste
{
    class Conecta
    {
        public MySqlConnection conexao;
        string copias = "Persist Security Info=false;SERVER=10.66.122.42;DATABASE=copias;UID=secac;pwd=secac";

        public void cadastro(Variavel va)
        {
             //string caminho = "Persist Security Info=false;SERVER=10.66.123.200;DATABASE=agenda;UID=secac;pwd=secac";
            string caminho = "Persist Security Info=false;SERVER=10.66.122.42;DATABASE=agenda;UID=secac;pwd=secac";
                  
            try
            {
                conexao = new MySqlConnection(caminho);
                conexao.Open();

                string inserir = "INSERT INTO agenda(nome, data, dtCad, local, horario, evento)VALUES('" + va.Nome + "','" + Convert.ToDateTime(va.Data).ToString("yyyy-MM-dd") + "','" + Convert.ToDateTime(va.DtCadastro).ToString("yyyy-MM-dd") + "','" + va.Local + "','" + va.Horario + "','" + va.Evento + "')";
                
                MySqlCommand comandos = new MySqlCommand(inserir, conexao);
               


                comandos.ExecuteNonQuery();
                
                conexao.Close();
            }
            catch(Exception ex) {

                throw new Exception("Erro de comandos: " + ex.Message);
            }
        }

        public DataTable PrincipalImp()
        {
            try
            {
                conexao = new MySqlConnection(copias);
                conexao.Open();
                string selecionar = "SELECT cod, data, prof, quant, curso, frente, autCoord,dtEntrega, autoriza FROM requisicao WHERE autCoord='Sim' AND ocultar='Não'order by data";
                MySqlDataAdapter comandos = new MySqlDataAdapter(selecionar, conexao);
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

        public DataTable AvisoLib()
        {
            try
            {
                conexao = new MySqlConnection(copias);
                conexao.Open();
                string selecionar = "SELECT autCoord FROM requisicao WHERE autCoord='Não'";
                MySqlDataAdapter comandos = new MySqlDataAdapter(selecionar, conexao);
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
