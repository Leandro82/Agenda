using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda_Etec
{
    class Agenda
    {
        private int nCod;
        private string nDtCad;
        private string nHrCad;
        private string nDtEv;
        private string nHrEv;
        private string nLocal;
        private string nEvento;
        private string nAx;
        private string nHrAx;
        private int nResp;
        private string nTp;

        public int Codigo
        {
            get { return nCod; }
            set { nCod = value; }
        }

        public string DataCadastro
        {
            get { return nDtCad; }
            set { nDtCad = value; }
        }

        public string HoraCadastro
        {
            get { return nHrCad; }
            set { nHrCad = value; }
        }

        public string DataEvento
        {
            get { return nDtEv; }
            set { nDtEv = value; }
        }

        public string HoraEvento
        {
            get { return nHrEv; }
            set { nHrEv = value; }
        }

        public string Local
        {
            get { return nLocal; }
            set { nLocal = value; }
        }

        public string Evento
        {
            get { return nEvento; }
            set { nEvento = value; }
        }

        public int RepCadastroEvento
        {
            get { return nResp; }
            set { nResp = value; }
        }

        public string TipoPesquisa
        {
            get { return nTp; }
            set { nTp = value; }
        }

        public string DataAuxiliar
        {
            get { return nAx; }
            set { nAx = value; }
        }

        public string HoraAuxiliar
        {
            get { return nHrAx; }
            set { nHrAx = value; }
        }
    }
}
