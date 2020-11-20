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

        [HttpGet]
        public ActionResult CreateDepartamento()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateDepartamento([Bind] DepartamentoDTO dto)
        {
            if (ModelState.IsValid)
            {
                DepartamentoRN.GetInstance().Salvar(dto);

                return RedirectToAction("CreateDepartamento");
            }
            return View(dto);
        }
        [HttpGet]
        public IActionResult UpdateDepartamento(int? id, [Bind] DepartamentoDTO dto)
        {
            return View(dto);
        }
        [HttpPut]
        public IActionResult UpdateDepartamento([Bind] DepartamentoDTO dto)
        {
            if (ModelState.IsValid)
            {
                DepartamentoRN.GetInstance().Salvar(dto);
                return RedirectToAction("UpdateDepartamento");
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