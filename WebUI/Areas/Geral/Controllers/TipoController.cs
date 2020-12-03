using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer.Geral;
using Dominio.Geral;
using Dominio.Seguranca;
using Microsoft.AspNetCore.Mvc;
using WebUI.Extensions;

namespace WebUI.Areas.Geral.Controllers
{
    [Area("Geral")]
    public class TipoController : Controller
    {
        private readonly KitandaConfig _kitandaConfig;
        public TipoController(KitandaConfig kitandaConfig)
        {
            _kitandaConfig = kitandaConfig;
        }
        void GetSessionDetails()
        {
            _kitandaConfig.pSessionInfo = HttpContext.Session.Get<AcessoDTO>("userCredencials");
            ViewData["_kitandaConfig"] = _kitandaConfig;
        }


        [HttpGet]
        public ActionResult CreateTipo()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateTipo([Bind] TipoDTO dto)
        {
            if (ModelState.IsValid)
            {
                TipoRN.GetInstance().Salvar(dto);

                return RedirectToAction("CreateTipo");
            }
            return View(dto);
        }
        [HttpGet]
        public IActionResult UpdateTipo(int? id, [Bind] TipoDTO dto)
        {
            return View(dto);
        }
        [HttpPut]
        public IActionResult UpdateTipo([Bind] TipoDTO dto)
        {
            if (ModelState.IsValid)
            {
                TipoRN.GetInstance().Salvar(dto);
                return RedirectToAction("UpdateTipo");
            }
            return View(dto);
        }
        public ActionResult DeleteTipo(TipoDTO dto)
        {
            TipoRN.GetInstance().Apagar(dto);
            return RedirectToAction("DeleteTipo");

        }
        public IActionResult ListTipo(TipoDTO dto)
        {

            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            return View(TipoRN.GetInstance().ObterPorFiltro(dto));
        }


        public IActionResult Pesquisar(TipoDTO dto)
        {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            return View(TipoRN.GetInstance().ObterPorFiltro(dto));
        }

        public IActionResult ListaTipo(TipoDTO dto)
        {
            IEnumerable<TipoDTO> lista = TipoRN.GetInstance().ObterPorFiltro(dto);
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