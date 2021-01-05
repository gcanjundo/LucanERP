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
    public class GrupoClienteController : Controller
    {
        private readonly KitandaConfig _kitandaConfig;
        public GrupoClienteController(KitandaConfig kitandaConfig)
        {
            _kitandaConfig = kitandaConfig;
        }
        void GetSessionDetails()
        {
            _kitandaConfig.pSessionInfo = HttpContext.Session.Get<AcessoDTO>("userCredencials");
            ViewData["_kitandaConfig"] = _kitandaConfig;
        }

        [HttpGet]
        public ActionResult CreateGrupoCliente()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateGrupoCliente([Bind] CategoriaDTO dto)
        {
            if (ModelState.IsValid)
            {
                GetSessionDetails();
                dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
                dto.Filial = _kitandaConfig.pSessionInfo.Filial;
                CategoriaRN.GetInstance().Salvar(dto);
                return RedirectToAction("CreateGrupoCliente");
            }
            return View(dto);
        }
        [HttpGet]
        public IActionResult UpdateGrupoCliente(int? id, [Bind] CategoriaDTO dto)
        {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            return View(dto);
        }
        [HttpPut]
        public IActionResult UpdateGrupoCliente([Bind] CategoriaDTO dto)
        {
            if (ModelState.IsValid)
            {
                GetSessionDetails();
                dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
                dto.Filial = _kitandaConfig.pSessionInfo.Filial;
                CategoriaRN.GetInstance().Salvar(dto);
                return RedirectToAction("UpdateGrupoCliente");
            }
            return View(dto);
        }
        public ActionResult DeleteGrupoCliente(CategoriaDTO dto)
        {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            CategoriaRN.GetInstance().Excluir(dto);
            return RedirectToAction("DeleteGrupoCliente");

        }
        public IActionResult ListGrupoCliente(CategoriaDTO dto)
        {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            return View(CategoriaRN.GetInstance().ObterPorFiltro(dto));
        }


        public IActionResult Pesquisar(CategoriaDTO dto)
        {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            return View(CategoriaRN.GetInstance().ObterPorFiltro(dto));
        }

        public IActionResult ListaGrupoClientes(CategoriaDTO dto)
        {
            IEnumerable<CategoriaDTO> lista = CategoriaRN.GetInstance().ObterPorFiltro(dto);
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