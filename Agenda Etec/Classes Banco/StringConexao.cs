using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda_Etec
{
    class StringConexao
    {
        string caminho;

        public string Endereco()
        {
            caminho = "Persist Security Info = false; SERVER = 10.66.121.42; DATABASE = agendaetec; UID = secac; pwd = secac";
            //caminho = "Persist Security Info=false;SERVER=localhost;DATABASE=agendaetec;UID=root;pwd=;Allow User Variables=True;Convert Zero Datetime=True;default command timeout=0";
            return caminho;
        }
    }
}
