using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer.Comercial;
using Dominio.Geral;
using Dominio.Seguranca;
using Microsoft.AspNetCore.Mvc;
using WebUI.Extensions;

namespace WebUI.Areas.Geral.Controllers
{
    [Area("Geral")]
    public class TechnicianController : Controller
    {
        private readonly KitandaConfig _kitandaConfig;
        public TechnicianController(KitandaConfig kitandaConfig)
        {
            _kitandaConfig = kitandaConfig;
        }
        void GetSessionDetails()
        {
            _kitandaConfig.pSessionInfo = HttpContext.Session.Get<AcessoDTO>("userCredencials");
            ViewData["_kitandaConfig"] = _kitandaConfig;
        }

        [HttpGet]
        public ActionResult CreateTechnician()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateTechnician([Bind] TechnicianDTO dto)
        {
            if (ModelState.IsValid)
            {
                TechnicianRN.GetInstance().Salvar(dto);

                return RedirectToAction("CreateTechnician");
            }
            return View(dto);
        }
        [HttpGet]
        public IActionResult UpdateTechnician(int? id, [Bind] TechnicianDTO dto)
        {
            return View(dto);
        }
        [HttpPut]
        public IActionResult UpdateTechnician([Bind] TechnicianDTO dto)
        {
            if (ModelState.IsValid)
            {
                TechnicianRN.GetInstance().Salvar(dto);
                return RedirectToAction("UpdateTechnician");
            }
            return View(dto);
        }
      
        public IActionResult ListTechnician(TechnicianDTO dto)
        {

            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            return View(TechnicianRN.GetInstance().ObterPorFiltro(dto));
        }


        public IActionResult Pesquisar(TechnicianDTO dto)
        {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            return View(TechnicianRN.GetInstance().ObterPorFiltro(dto));
        }

        public IActionResult ListaTechnician(TechnicianDTO dto)
        {
            IEnumerable<TechnicianDTO> lista = TechnicianRN.GetInstance().ObterPorFiltro(dto);
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