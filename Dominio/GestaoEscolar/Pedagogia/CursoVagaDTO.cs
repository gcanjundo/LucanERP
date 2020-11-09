using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.GestaoEscolar.Pedagogia
{
    public class CursoVagaDTO
    {
        public CursoVagaDTO(CursoDTO curso, AnoLectivoDTO anoLectivo, int vaga)
        {
            this.VagAnoLectivo = anoLectivo;
            this.VagCurso = curso;
            this.VagVaga = vaga;
        }

        private CursoDTO _vagCurso;

        public CursoDTO VagCurso
        {
            get { return _vagCurso; }
            set { _vagCurso = value; }
        }
        private AnoLectivoDTO _vagAnoLectivo;

        public AnoLectivoDTO VagAnoLectivo
        {
            get { return _vagAnoLectivo; }
            set { _vagAnoLectivo = value; }
        }
        private int _vagVaga;

        public int VagVaga
        {
            get { return _vagVaga; }
            set { _vagVaga = value; }
        }
    }

    public class ListaCursosVagasDTO : List<CursoVagaDTO> 
    {
    
    }
}
