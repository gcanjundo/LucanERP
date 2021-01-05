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
    public class TabelaPrecoController : Controller
    {
        private readonly KitandaConfig _kitandaConfig;
        public TabelaPrecoController(KitandaConfig kitandaConfig)
        {
            _kitandaConfig = kitandaConfig;
        }
        void GetSessionDetails()
        {
            _kitandaConfig.pSessionInfo = HttpContext.Session.Get<AcessoDTO>("userCredencials");
            ViewData["_kitandaConfig"] = _kitandaConfig;
        }


        [HttpGet]
        public ActionResult CreateTabelaPreco()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateTabelaPreco([Bind] TabelaPrecoDTO dto)
        {
            if (ModelState.IsValid)
            {
                GetSessionDetails();
                dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
                dto.Filial = _kitandaConfig.pSessionInfo.Filial;
                TabelaPrecoRN.GetInstance().Salvar(dto);

                return RedirectToAction("CreateTabelaPreco");
            }
            return View(dto);
        }
        [HttpGet]
        public IActionResult UpdateTabelaPreco(int? id, [Bind] TabelaPrecoDTO dto)
        {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            return View(dto);
        }
        [HttpPut]
        public IActionResult UpdateTabelaPreco([Bind] TabelaPrecoDTO dto)
        {
            if (ModelState.IsValid)
            {
                GetSessionDetails();
                dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
                dto.Filial = _kitandaConfig.pSessionInfo.Filial;
                TabelaPrecoRN.GetInstance().Salvar(dto);
                return RedirectToAction("UpdateTabelaPreco");
            }
            return View(dto);
        }
        public ActionResult DeleteTabelaPreco(TabelaPrecoDTO dto)
        {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            TabelaPrecoRN.GetInstance().Apagar(dto);
            return RedirectToAction("DeleteTabelaPreco");

        }
        public IActionResult ListTabelaPreco(TabelaPrecoDTO dto)
        {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            return View(TabelaPrecoRN.GetInstance().ObterPorFiltro(dto));
        }


        public IActionResult Pesquisar(TabelaPrecoDTO dto)
        {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            return View(TabelaPrecoRN.GetInstance().ObterPorFiltro(dto));
        }

        public IActionResult ListaTabelaPreco(TabelaPrecoDTO dto)
        {
            IEnumerable<TabelaPrecoDTO> lista = TabelaPrecoRN.GetInstance().ObterPorFiltro(dto);
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