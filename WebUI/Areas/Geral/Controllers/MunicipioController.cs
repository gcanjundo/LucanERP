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
    public class MunicipioController : Controller
    {
        private readonly KitandaConfig _kitandaConfig;
        public MunicipioController(KitandaConfig kitandaConfig)
        {
            _kitandaConfig = kitandaConfig;
        }
        void GetSessionDetails()
        {
            _kitandaConfig.pSessionInfo = HttpContext.Session.Get<AcessoDTO>("userCredencials");
            ViewData["_kitandaConfig"] = _kitandaConfig;
        }

        [HttpGet]
        public ActionResult CreateMunicipio()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateMunicipio([Bind] MunicipioDTO dto)
        {
            if (ModelState.IsValid)
            {
                MunicipioRN.GetInstance().Salvar(dto);

                return RedirectToAction("CreateMunicipio");
            }
            return View(dto);
        }
        [HttpGet]
        public IActionResult UpdateMunicipio(int? id, [Bind] MunicipioDTO dto)
        {
            return View(dto);
        }
        [HttpPut]
        public IActionResult UpdateMunicipio([Bind] MunicipioDTO dto)
        {
            if (ModelState.IsValid)
            {
                MunicipioRN.GetInstance().Salvar(dto);
                return RedirectToAction("UpdateMunicipio");
            }
            return View(dto);
        }
        public ActionResult DeleteMunicipio(MunicipioDTO dto)
        {
            MunicipioRN.GetInstance().Excluir(dto);
            return RedirectToAction("DeleteMunicipio");

        }
        public IActionResult ListMunicipio(MunicipioDTO dto)
        {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            return View(MunicipioRN.GetInstance().ObterPorFiltro(dto));
        }


        public IActionResult Pesquisar(MunicipioDTO dto)
        {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            return View(MunicipioRN.GetInstance().ObterPorFiltro(dto));
        }

        public IActionResult ListaMunicipio(MunicipioDTO dto)
        {
            IEnumerable<MunicipioDTO> lista = MunicipioRN.GetInstance().ObterPorFiltro(dto);
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