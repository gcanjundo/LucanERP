using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Geral
{
    public class EstatisticasDTO
    {
        private int _total = 0;

        public int Total
        {
            get { return _total; }
            set { _total = value; }
        }
        private int _totalAnoLectivo = 0;

        public int TotalAnoLectivo
        {
            get { return _totalAnoLectivo; }
            set { _totalAnoLectivo = value; }
        }
        private int _matriculados = 0;

        public int Matriculados
        {
            get { return _matriculados; }
            set { _matriculados = value; }
        }
        private int _naoMatriculados = 0;

        public int NaoMatriculados
        {
            get { return _naoMatriculados; }
            set { _naoMatriculados = value; }
        }
        private int _transferidos = 0;

        public int Transferidos
        {
            get { return _transferidos; }
            set { _transferidos = value; }
        }
        private int _semRegisto = 0;

        public int SemRegisto
        {
            get { return _semRegisto; }
            set { _semRegisto = value; }
        }
        private int _desistentes = 0;

        public int Desistentes
        {
            get { return _desistentes; }
            set { _desistentes = value; }
        }
        private int _novosAlunos = 0;

        public int NovosAlunos
        {
            get { return _novosAlunos; }
            set { _novosAlunos = value; }
        }

        private int _codigo = 0;

        public int Codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
        }
        private string _ano = "";

        public string Ano
        {
            get { return _ano; }
            set { _ano = value; }
        }
    }
}
