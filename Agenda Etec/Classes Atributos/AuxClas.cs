using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda_Etec
{
    class AuxClas
    {
        private static int cd;
        public static int codigo
        {
            get { return cd; }
            set { cd = value; }
        }

        private static int resp;
        public static int respCadastro
        {
            get { return resp; }
            set { resp = value; }
        }

        private static int lgd;
        public static int logado
        {
            get { return lgd; }
            set { lgd = value; }
        }

        private static string sn;
        public static string senha
        {
            get { return sn; }
            set { sn = value; }
        }

        private static string ic;
        public static string icone
        {
            get { return ic; }
            set { ic = value; }
        }

        private static string lg;
        public static string login
        {
            get { return lg; }
            set { lg = value; }
        }

        private static string nm;
        public static string nome
        {
            get { return nm; }
            set { nm = value; }
        }

        private static string aces;
        public static string acesso
        {
            get { return aces; }
            set { aces = value; }
        }

        private static string hr;
        public static string horaEvento
        {
            get { return hr; }
            set { hr = value; }
        }

        private static string dt;
        public static string dataEvento
        {
            get { return dt; }
            set { dt = value; }
        }

        private static string lc;
        public static string localEvento
        {
            get { return lc; }
            set { lc = value; }
        }

        private static string ev;
        public static string evento
        {
            get { return ev; }
            set { ev = value; }
        }

        private static string ms;
        public static string processo
        {
            get { return ms; }
            set { ms = value; }
        }
    }
}
