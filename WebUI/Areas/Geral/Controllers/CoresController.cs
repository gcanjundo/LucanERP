using BusinessLogicLayer;
using BusinessLogicLayer.Geral;
using DataAccessLayer.Geral;
using Dominio.Geral;
using Dominio.Seguranca;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Extensions;

namespace WebUI.Areas.Geral.Controllers
{
    [Area("Geral")]
    public class CoresController : Controller
    {
        private readonly KitandaConfig _kitandaConfig;
        public CoresController(KitandaConfig kitandaConfig)
        {
            _kitandaConfig = kitandaConfig;
        }
        void GetSessionDetails()
        {
            _kitandaConfig.pSessionInfo = HttpContext.Session.Get<AcessoDTO>("userCredencials");
            ViewData["_kitandaConfig"] = _kitandaConfig;
        }

        [HttpGet]
        public ActionResult CreateCores()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateCores([Bind] CoresDTO dto)
        {
            if (ModelState.IsValid)
            {
                CoresRN.GetInstance().Salvar(dto);
                return RedirectToAction("CreateCores");
            }
            return View(dto);
        }
        [HttpGet]
        public IActionResult UpdateCores(int? id, [Bind] CoresDTO dto)
        {
            return View(dto);
        }
        [HttpPut]
        public IActionResult UpdateCores([Bind] CoresDTO dto)
        {
            if (ModelState.IsValid)
            {
                CoresRN.GetInstance().Salvar(dto);
                return RedirectToAction("UpdateCores");
            }
            return View(dto);
        }
        public ActionResult DeleteCores(CoresDTO dto)
        {
            CoresRN.GetInstance().Excluir(dto);
            return RedirectToAction("DeleteArmazem");

        }
        public IActionResult ListCores(CoresDTO dto)
        {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            return View(CoresRN.GetInstance().ObterPorFiltro(dto));
        }


        public IActionResult Pesquisar(CoresDTO dto)
        {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            return View(CoresRN.GetInstance().ObterPorFiltro(dto));
        }
    }


}
