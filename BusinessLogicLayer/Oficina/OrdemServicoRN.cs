using BusinessLogicLayer.Geral;
using DataAccessLayer.Oficina;
using Dominio.Geral;
using Dominio.Oficina;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Oficina
{
    public class OrdemServicoRN
    {
        private static OrdemServicoRN _instancia;
        private OrdemServicoDAO dao;
        GenericRN _genericClass = new GenericRN();
        public OrdemServicoRN()
        {
            dao = new OrdemServicoDAO();
        }

        public static OrdemServicoRN GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new OrdemServicoRN();
            }

            return _instancia;
        }

        public Tuple<int, string, bool> Save(OrdemServicoDTO dto)
        {
            
            if (dto.VehicleID <= 0)
            {
                dto.VehicleID = VeiculoRN.GetInstance().Salvar(dto.Veiculo).VeiculoID;
            }
            dto = dao.Add(dto);
            if (dto.Sucesso)
            {  
                return new Tuple<int, string, bool>(dto.WorkOrderID, _genericClass.SuccessMessage(), true);
            }
            else
            {
                return new Tuple<int, string, bool>(-1, _genericClass.ErrorMessage(dto.MensagemErro), false);
            } 
        }

        public OrdemServicoDTO ObterPorPK(OrdemServicoDTO dto)
        {
            dto = dao.ObterPorPK(dto); 
            dto.Veiculo = VeiculoRN.GetInstance().ObterPorPK(new VeiculoDTO(dto.VehicleID)); 
            return dto;

        }

        public List<OrdemServicoDTO>ObterPorFiltro(OrdemServicoDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        public List<OrderDetailDTO> GetOrderItemList(OrderDetailDTO dto)
        {
            return dao.ObterDetalhesPorFiltro(dto);
        }

        public List<OrdemServicoDTO> ObterMarcacoes(OrdemServicoDTO dto)
        {

            dto.LookupField1 = string.Empty;
            dto.Reference = string.Empty;
            dto.CreatedBy = string.Empty;
            dto.WorkOrderID = -1;
            return ObterPorFiltro(dto).Where(t => t.BookingDate > DateTime.MinValue).ToList();
        }

        public OrdemServicoDTO Excluir(OrdemServicoDTO dto)
        { 
            dao.Excluir(dto);

            return dto;
        }

        public string CreateBooking(OrdemServicoDTO dto)
        {
            return Save(dto).Item2;  
        }

        public void AddInvoice(OrdemServicoDTO dto)
        {
            dao.AddInvoice(dto);
        }
    }
}
