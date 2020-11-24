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
    public class MarcaController : Controller
    {
        private List<MarcaDTO> lista;

        [HttpGet]
        public ActionResult SaveMarca(int? codigo, [Bind] MarcaDTO dto)
        {
            if (codigo == 0)
            {
                return View();
            }
            else
            {
                var result = MarcaRN.GetInstance().ObterPorPK(dto);
                return View(result);
            }
        }
        [HttpPost]
        public ActionResult SaveMarca([Bind] MarcaDTO dto)
        {
            if (ModelState.IsValid)
            {
                MarcaRN.GetInstance().Salvar(dto);

                return RedirectToAction("CreateMarca");
            }
            return View(dto);
        }

        public ActionResult DeleteMarca(MarcaDTO dto)
        {
            MarcaRN.GetInstance().Excluir(dto);
            return RedirectToAction("DeleteMarca");

        }
        public IActionResult ListMarca(MarcaDTO dto)
        {
            lista = new List<MarcaDTO>();
            lista = MarcaRN.GetInstance().ObterPorFiltro(dto);
            return View(lista);
        }


        public IActionResult Pesquisar(MarcaDTO dto)
        {
            IEnumerable<MarcaDTO> resultado = MarcaRN.GetInstance().ObterPorFiltro(dto);
            return View(resultado);
        }

        public IActionResult ListaMarca(MarcaDTO dto)
        {
            IEnumerable<MarcaDTO> lista = MarcaRN.GetInstance().ObterPorFiltro(dto);
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