using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer.Geral;
using Dominio.Geral;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.Geral.Controllers
{
    [Area("Geral")]
    public class DepartamentoController : Controller
    {
        private List<DepartamentoDTO> lista;

        public ActionResult SaveDepartamento(int? codigo, [Bind] DepartamentoDTO dto)
        {
            if (codigo == 0)
            {
                return View();
            }
            else
            {
                var result = DepartamentoRN.GetInstance().ObterPorPK(dto);
                return View(result);
            }

        }
        [HttpPost]
        public ActionResult SaveDepartamento([Bind] DepartamentoDTO dto)
        {
            if (ModelState.IsValid)
            {
                DepartamentoRN.GetInstance().Salvar(dto);

                return RedirectToAction("CreateDepartamento");
            }
            return View(dto);
        }

        public ActionResult DeleteDepartamento(DepartamentoDTO dto)
        {
            DepartamentoRN.GetInstance().Excluir(dto);
            return RedirectToAction("Departamento");

        }
        public IActionResult ListDepartamento(DepartamentoDTO dto)
        {
            lista = new List<DepartamentoDTO>();
            lista = DepartamentoRN.GetInstance().ObterPorFiltro(dto);
            return View(lista);
        }


        public IActionResult Pesquisar(DepartamentoDTO dto)
        {
            IEnumerable<DepartamentoDTO> resultado = DepartamentoRN.GetInstance().ObterPorFiltro(dto);
            return View(resultado);
        }

        public IActionResult ListaDepartamento(DepartamentoDTO dto)
        {
            IEnumerable<DepartamentoDTO> lista = DepartamentoRN.GetInstance().ObterPorFiltro(dto);
            var lblRegisto = "";
            if (lista.Count() >= 1)
            {
                lblRegisto = lista.Count() + " Artigo(s) Encontrado(s)";
            }
            else
            {
                lblRegisto = "Nenhum Artigo Encontrado";
            }
            return View(lista);
        }
    }
}