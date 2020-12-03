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
    public class TamanhoController : Controller
    {
        private readonly KitandaConfig _kitandaConfig;
        public TamanhoController(KitandaConfig kitandaConfig)
        {
            _kitandaConfig = kitandaConfig;
        }
        void GetSessionDetails()
        {
            _kitandaConfig.pSessionInfo = HttpContext.Session.Get<AcessoDTO>("userCredencials");
            ViewData["_kitandaConfig"] = _kitandaConfig;
        }


        [HttpGet]
        public ActionResult CreateTamanho()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateTamanho([Bind] TamanhoDTO dto)
        {
            if (ModelState.IsValid)
            {
                TamanhoRN.GetInstance().Salvar(dto);

                return RedirectToAction("CreateTamanho");
            }
            return View(dto);
        }
        [HttpGet]
        public IActionResult UpdateTamanho(int? id, [Bind] TamanhoDTO dto)
        {
            return View(dto);
        }
        [HttpPut]
        public IActionResult UpdateTamanho([Bind] TamanhoDTO dto)
        {
            if (ModelState.IsValid)
            {
                TamanhoRN.GetInstance().Salvar(dto);
                return RedirectToAction("UpdateTamanho");
            }
            return View(dto);
        }
        public ActionResult DeleteTamanho(TamanhoDTO dto)
        {
            TamanhoRN.GetInstance().Excluir(dto);
            return RedirectToAction("DeleteTamanho");

        }
        public IActionResult ListTamanho(TamanhoDTO dto)
        {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            return View(TamanhoRN.GetInstance().ObterPorFiltro(dto));
        }


        public IActionResult Pesquisar(TamanhoDTO dto)
        {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            return View(TamanhoRN.GetInstance().ObterPorFiltro(dto));
        }

        public IActionResult ListaTamanho(TamanhoDTO dto)
        {
            IEnumerable<TamanhoDTO> lista = TamanhoRN.GetInstance().ObterPorFiltro(dto);
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