using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.Clinica;
using DataAccessLayer.Clinica;
using Dominio.Seguranca;

namespace BusinessLogicLayer.Clinica
{
    public class EscalaRN
    {
         private static EscalaRN _instancia;

        private EscalaDAO dao;

        public EscalaRN()
        {
          dao = new EscalaDAO();
        }

        public static EscalaRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new EscalaRN();
            }

            return _instancia;
        }

        public void Salvar(List<EscalaDTO> pLista) 
        {
            foreach(var escala in pLista)
            {
                dao.Salvar(escala);
            }
        }

        public List<EscalaDTO> ObterEscala(EscalaDTO dto)
        {
            return dao.ObterEscala(dto);
        }
        public List<EscalaDTO> ObterEscalaProfissional(EscalaDTO dto)
        {
            List<EscalaDTO> escala = ObterEscala(dto);

            if (escala.Count == 0)
            {
                var profissional = ProfissionalRN.GetInstance().ObterPorPK(dto.Profissional);
                escala.Add(new EscalaDTO
                {
                    Dia = 1,
                    DescricaoDia = "Domingo",
                    Profissional = profissional
                });

                escala.Add(new EscalaDTO
                {
                    Dia = 2,
                    DescricaoDia = "Segunda-Feira",
                    Profissional = profissional
                });
                escala.Add(new EscalaDTO
                {
                    Dia = 3,
                    DescricaoDia = "Terça-Feira",
                    Profissional = profissional
                });
                escala.Add(new EscalaDTO
                {
                    Dia = 4,
                    DescricaoDia = "Quarta-Feira",
                    Profissional = profissional
                });
                escala.Add(new EscalaDTO
                {
                    Dia = 5,
                    DescricaoDia = "Quinta-Feira",
                    Profissional = profissional
                });
                escala.Add(new EscalaDTO
                {
                    Dia = 6,
                    DescricaoDia = "Sexta-Feira",
                    Profissional = profissional
                });
                escala.Add(new EscalaDTO
                {
                    Dia = 7,
                    DescricaoDia = "Sábado", 
                    Profissional = profissional
                    
                });
                
            }

            return escala;
        }
        
         
        
    }
}
