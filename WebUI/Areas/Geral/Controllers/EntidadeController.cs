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
        public class EntidadeController : Controller
        {
            private List<EntidadeDTO> lista;

            public ActionResult SaveEntidade(int? codigo, [Bind] EntidadeDTO dto)
            {
                if(codigo == 0)
                {
                return View();
                }
                else
                {
                    var result = EntidadeRN.GetInstance().ObterPorPK(dto);
                    return View(result);
                }
                
            }

            [HttpPost]
            public ActionResult CreateEntidade([Bind] EntidadeDTO dto)
            {
                if (ModelState.IsValid)
                {
                    EntidadeRN.GetInstance().Salvar(dto);

                    return RedirectToAction("CreateEntidade");
                }
                return View(dto);
            }
           
            public ActionResult DeleteEntidade(EntidadeDTO dto)
            {
                EntidadeRN.GetInstance().Eliminar(dto);
                return RedirectToAction("DeleteEntidade");

            }
            public IActionResult ListEntidade(EntidadeDTO dto)
            {
                lista = new List<EntidadeDTO>();
                lista = EntidadeRN.GetInstance().ObterPorFiltro(dto);
                return View(lista);
            }


            public IActionResult Pesquisar(EntidadeDTO dto)
            {
                IEnumerable<EntidadeDTO> resultado = EntidadeRN.GetInstance().ObterPorFiltro(dto);
                return View(resultado);
            }

            public IActionResult ListaDocumento(EntidadeDTO dto)
            {
                IEnumerable<EntidadeDTO> lista = EntidadeRN.GetInstance().ObterPorFiltro(dto);
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