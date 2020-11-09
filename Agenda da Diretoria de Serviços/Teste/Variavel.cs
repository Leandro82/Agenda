using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Teste
{
    class Variavel
    {
        private int nCod;
        private string nAux;
        private string nNome;
        private DateTime nData;
        private DateTime nDataIn;
        private DateTime nCad;
        private string nLocal;
        private string nHorario;
        private string nEvento;

        public int Codigo
        {
            get { return nCod; }
            set { nCod = value; }
        }

        public string Nome
        {
            get { return nNome; }
            set { nNome = value; }
        }

        public string Auxiliar
        {
            get { return nAux; }
            set { nAux = value; }
        }

        public DateTime Data
        {
            get { return nData; }
            set { nData = value; }
        }

        public DateTime Data2
        {
            get { return nDataIn; }
            set { nDataIn = value; }
        }

        public DateTime DtCadastro
        {
            get { return nCad; }
            set { nCad = value; }
        }

        public string Local
        {
            get { return nLocal; }
            set { nLocal = value; }
        }

        public string Horario
        {
            get { return nHorario; }
            set { nHorario = value; }
        }

        public string Evento
        {
            get { return nEvento; }
            set { nEvento = value; }
        }
    }
}
