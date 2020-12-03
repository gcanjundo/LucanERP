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
    public class VeiculoController : Controller
    {
        private readonly KitandaConfig _kitandaConfig;
        public VeiculoController(KitandaConfig kitandaConfig)
        {
            _kitandaConfig = kitandaConfig;
        }
        void GetSessionDetails()
        {
            _kitandaConfig.pSessionInfo = HttpContext.Session.Get<AcessoDTO>("userCredencials");
            ViewData["_kitandaConfig"] = _kitandaConfig;
        }
        [HttpGet]
        public ActionResult CreateVeiculo()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateVeiculo([Bind] VeiculoDTO dto)
        {
            if (ModelState.IsValid)
            {
                VeiculoRN.GetInstance().Salvar(dto);

                return RedirectToAction("CreateVeiculo");
            }
            return View(dto);
        }
        [HttpGet]
        public IActionResult UpdateVeiculo(int? id, [Bind] VeiculoDTO dto)
        {
            return View(dto);
        }
        [HttpPut]
        public IActionResult UpdateVeiculo([Bind] VeiculoDTO dto)
        {
            if (ModelState.IsValid)
            {
                VeiculoRN.GetInstance().Salvar(dto);
                return RedirectToAction("UpdateVeiculo");
            }
            return View(dto);
        }
        public ActionResult DeleteVeiculo(VeiculoDTO dto)
        {
            VeiculoRN.GetInstance().Excluir(dto);
            return RedirectToAction("DeleteVeiculo");

        }
        public IActionResult ListVeiculo(VeiculoDTO dto)
        {

            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            return View(VeiculoRN.GetInstance().ObterPorFiltro(dto));
        }


        public IActionResult Pesquisar(VeiculoDTO dto)
        {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            return View(VeiculoRN.GetInstance().ObterPorFiltro(dto));
        }

        public IActionResult ListaVeiculo(VeiculoDTO dto)
        {
            IEnumerable<VeiculoDTO> lista = VeiculoRN.GetInstance().ObterPorFiltro(dto);
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