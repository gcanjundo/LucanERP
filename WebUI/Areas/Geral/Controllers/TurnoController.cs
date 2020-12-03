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
    public class TurnoController : Controller
    {
        private readonly KitandaConfig _kitandaConfig;
        public TurnoController(KitandaConfig kitandaConfig)
        {
            _kitandaConfig = kitandaConfig;
        }
        void GetSessionDetails()
        {
            _kitandaConfig.pSessionInfo = HttpContext.Session.Get<AcessoDTO>("userCredencials");
            ViewData["_kitandaConfig"] = _kitandaConfig;
        }

        [HttpGet]
        public ActionResult CreateTurno()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateTurno([Bind] TurnoDTO dto)
        {
            if (ModelState.IsValid)
            {
                TurnoRN.GetInstance().Salvar(dto);

                return RedirectToAction("CreateTurno");
            }
            return View(dto);
        }
        [HttpGet]
        public IActionResult UpdateTurno(int? id, [Bind] TurnoDTO dto)
        {
            return View(dto);
        }
        [HttpPut]
        public IActionResult UpdateTurno([Bind] TurnoDTO dto)
        {
            if (ModelState.IsValid)
            {
                TurnoRN.GetInstance().Salvar(dto);
                return RedirectToAction("UpdateTurno");
            }
            return View(dto);
        }
        public ActionResult DeleteTurno(TurnoDTO dto)
        {
            TurnoRN.GetInstance().Excluir(dto);
            return RedirectToAction("DeleteTurno");

        }
        public IActionResult ListTurno(TurnoDTO dto)
        {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            return View(TurnoRN.GetInstance().ObterPorFiltro(dto));
        }


        public IActionResult Pesquisar(TurnoDTO dto)
        {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            return View(TurnoRN.GetInstance().ObterPorFiltro(dto));
        }

        public IActionResult ListaTurno(TurnoDTO dto)
        {
            IEnumerable<TurnoDTO> lista = TurnoRN.GetInstance().ObterPorFiltro(dto);
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