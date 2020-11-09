using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.Seguranca;
using DataAccessLayer.Seguranca;
using System.IO;
using Dominio.Clinica;

namespace BusinessLogicLayer.Seguranca
{
    public class ConfiguracaoRN
    {
        private static ConfiguracaoRN _instancia;

        private ConfiguracaoDAO dao;

        public ConfiguracaoRN()
        {
          dao = new ConfiguracaoDAO();
        }

        public static ConfiguracaoRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new ConfiguracaoRN();
            }

            return _instancia;
        }

        public ConfiguracaoDTO SaveSystemConfiguration(ConfiguracaoDTO dto)
        {
            return dao.SysConfigAdd(dto);
        }

        

        public ConfiguracaoDTO GetSystemConfiguration(EmpresaDTO dto) 
        {
            return dao.ObterConfiguracaoActiva(dto);
        }



        public bool IsSupervisor(ConfiguracaoDTO pAppSettings, string pSuperVisorCode)
        {
            if (pAppSettings.SupervisorID == pSuperVisorCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ExecuteBackup(ConfiguracaoDTO dto)
        {
            dao.ExecuteBackup(dto);
        }


        public AcessoDTO GetClinicalSettings(AcessoDTO dto)
        {
            dto = dao.ObterConfiguracaoClinica(dto);
            dto.EscalaClinica = new List<EscalaDTO>();
            TimeSpan hora = dto.Settings.HorarioInicioP1.TimeOfDay;

            while (hora <= dto.Settings.HorarioTerminoP1.TimeOfDay)
            {
                if (!dto.EscalaClinica.Exists(t => t.Data.TimeOfDay == hora))
                    dto.EscalaClinica.Add(new EscalaDTO
                    {
                        Data = new DateTime(DateTime.Today.Year, DateTime.Today.Month,
                    DateTime.Today.Day, hora.Hours, hora.Minutes, hora.Seconds),
                        Descricao = hora.Hours.ToString() + ":" + hora.Minutes.ToString()
                    });
                hora = hora.Add(TimeSpan.FromMinutes(dto.Settings.DuracaoAtendimento));
                break;
            }

            return dto;
        } 

    }
}
