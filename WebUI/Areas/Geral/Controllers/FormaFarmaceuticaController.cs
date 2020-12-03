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
    public class FormaFarmaceuticaController : Controller
    {
        private readonly KitandaConfig _kitandaConfig;
        public FormaFarmaceuticaController(KitandaConfig kitandaConfig)
        {
            _kitandaConfig = kitandaConfig;
        }
        void GetSessionDetails()
        {
            _kitandaConfig.pSessionInfo = HttpContext.Session.Get<AcessoDTO>("userCredencials");
            ViewData["_kitandaConfig"] = _kitandaConfig;
        }

        [HttpGet]
        public ActionResult CreateFormaFarmaceutica()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateFormaFarmaceutica([Bind] FormaFarmaceuticaDTO dto)
        {
            if (ModelState.IsValid)
            {
                FormaFarmaceuticaRN.GetInstance().Salvar(dto);

                return RedirectToAction("CreateFormaFarmaceutica");
            }
            return View(dto);
        }
        [HttpGet]
        public IActionResult UpdateFormaFarmaceutica(int? id, [Bind] FormaFarmaceuticaDTO dto)
        {
            return View(dto);
        }
        [HttpPut]
        public IActionResult UpdateFormaFarmaceutica([Bind] FormaFarmaceuticaDTO dto)
        {
            if (ModelState.IsValid)
            {
                FormaFarmaceuticaRN.GetInstance().Salvar(dto);
                return RedirectToAction("UpdateFormaFarmaceutica");
            }
            return View(dto);
        }
        public ActionResult DeleteFormaFarmaceutica(FormaFarmaceuticaDTO dto)
        {
            FormaFarmaceuticaRN.GetInstance().Excluir(dto);
            return RedirectToAction("DeleteFormaFarmaceutica");

        }
        public IActionResult ListFormaFarmaceutica(FormaFarmaceuticaDTO dto)
        {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            return View(FormaFarmaceuticaRN.GetInstance().ObterPorFiltro(dto));
        }


        public IActionResult Pesquisar(FormaFarmaceuticaDTO dto)
        {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            return View(FormaFarmaceuticaRN.GetInstance().ObterPorFiltro(dto));
        }

        public IActionResult ListaFormaFarmaceutica(FormaFarmaceuticaDTO dto)
        {
            IEnumerable<FormaFarmaceuticaDTO> lista = FormaFarmaceuticaRN.GetInstance().ObterPorFiltro(dto);
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