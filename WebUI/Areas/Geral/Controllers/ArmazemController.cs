using BusinessLogicLayer.Geral;
using DataAccessLayer.Geral;
using Dominio.Geral;
using Dominio.Seguranca;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Extensions;

namespace WebUI.Areas.Geral.Controllers
{
    [Area("Geral")]
    public class ArmazemController : Controller
    {
       
    

        private readonly KitandaConfig _kitandaConfig;
        public ArmazemController(KitandaConfig kitandaConfig)
        {
            _kitandaConfig = kitandaConfig;
        }
        void GetSessionDetails()
        {
            _kitandaConfig.pSessionInfo = HttpContext.Session.Get<AcessoDTO>("userCredencials");
            ViewData["_kitandaConfig"] = _kitandaConfig;
        }


        [HttpGet]
        public ActionResult CreateArmazem()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateArmazem([Bind] ArmazemDTO dto)
        {
            if (ModelState.IsValid)
            {
                ArmazemRN.GetInstance().Salvar(dto);
              
                return RedirectToAction("CreateArmazem");
            }
            return View(dto);
        }
        [HttpGet]
        public IActionResult UpdateArmazem(int? id, [Bind] ArmazemDTO dto)
        {
            return View(dto);
        }
        [HttpPut]
        public IActionResult UpdateArmazem([Bind] ArmazemDTO dto)
        {
            if (ModelState.IsValid)
            {
                ArmazemRN.GetInstance().Salvar(dto);
                return RedirectToAction("UpdateArmazem");
            }
            return View(dto);
        }
        public ActionResult DeleteArmazem(ArmazemDTO dto)
        {
            ArmazemRN.GetInstance().Apagar(dto);
            return RedirectToAction("DeleteArmazem");

        }
        public IActionResult ListArmazem(ArmazemDTO dto)
        {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            return View(ArmazemRN.GetInstance().ObterPorFiltro(dto));
        }


        public IActionResult Pesquisar(ArmazemDTO dto)
        {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            return View(ArmazemRN.GetInstance().ObterPorFiltro(dto));
        }

    }
}
