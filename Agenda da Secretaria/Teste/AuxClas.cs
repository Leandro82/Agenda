using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Teste
{
    class AuxClas
    {
        private static string _usuario;

        public static string usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }

        private static string aux;

        public static string auxiliar
        {
            get { return aux; }
            set { aux = value; }
        }

        private static string priv;

        public static string privilegio
        {
            get { return priv; }
            set { priv = value; }
        }

        private static string _login;
        public static string login
        {
            get { return _login; }
            set { _login = value; }
        }

        private static string _icone;
        public static string icone
        {
            get { return _icone; }
            set { _icone = value; }
        }

        private static int _quant;
        public static int quantidade
        {
            get { return _quant; }
            set { _quant = value; }
        }

    }
}
