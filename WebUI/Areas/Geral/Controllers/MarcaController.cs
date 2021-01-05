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
    public class MarcaController : Controller
    {
        private readonly KitandaConfig _kitandaConfig;
        public MarcaController(KitandaConfig kitandaConfig)
        {
            _kitandaConfig = kitandaConfig;
        }
        void GetSessionDetails()
        {
            _kitandaConfig.pSessionInfo = HttpContext.Session.Get<AcessoDTO>("userCredencials");
            ViewData["_kitandaConfig"] = _kitandaConfig;
        }
        [HttpGet]
        public ActionResult CreateMarca()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateMarca([Bind] MarcaDTO dto)
        {
            if (ModelState.IsValid)
            {
                GetSessionDetails();
                dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
                dto.Filial = _kitandaConfig.pSessionInfo.Filial;
                MarcaRN.GetInstance().Salvar(dto);
                return RedirectToAction("CreateMarca");
            }
            return View(dto);
        }
        [HttpGet]
        public IActionResult UpdateMarca(int? id, [Bind] MarcaDTO dto)
        {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            return View(dto);
        }
        [HttpPut]
        public IActionResult UpdateMarca([Bind] MarcaDTO dto)
        {
            if (ModelState.IsValid)
            {
                GetSessionDetails();
                dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
                dto.Filial = _kitandaConfig.pSessionInfo.Filial;
                MarcaRN.GetInstance().Salvar(dto);
                return RedirectToAction("UpdateMarca");
            }
            return View(dto);
        }
        public ActionResult DeleteMarca(MarcaDTO dto)
        {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            MarcaRN.GetInstance().Excluir(dto);
            return RedirectToAction("DeleteMarca");

        }
        public IActionResult ListMarca(MarcaDTO dto)
        {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            return View(MarcaRN.GetInstance().ObterPorFiltro(dto));
        }


        public IActionResult Pesquisar(MarcaDTO dto)
        {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            return View(MarcaRN.GetInstance().ObterPorFiltro(dto));
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