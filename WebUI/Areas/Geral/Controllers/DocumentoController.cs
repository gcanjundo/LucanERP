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
    public class DocumentoController : Controller
    {
        private readonly KitandaConfig _kitandaConfig;
        public DocumentoController(KitandaConfig kitandaConfig)
        {
            _kitandaConfig = kitandaConfig;
        }
        void GetSessionDetails()
        {
            _kitandaConfig.pSessionInfo = HttpContext.Session.Get<AcessoDTO>("userCredencials");
            ViewData["_kitandaConfig"] = _kitandaConfig;
        }
        [HttpGet]
    public ActionResult CreateDocumento()
    {
        return View();
    }
    [HttpPost]
    public ActionResult CreateDocumento([Bind] DocumentoDTO dto)
    {
        if (ModelState.IsValid)
        {
                GetSessionDetails();
                dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
                dto.Filial = _kitandaConfig.pSessionInfo.Filial;
                DocumentoRN.GetInstance().Salvar(dto);

            return RedirectToAction("CreateDocumento");
        }
        return View(dto);
    }
    [HttpGet]
    public IActionResult UpdateDocumento(int? id, [Bind] DocumentoDTO dto)
    {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            return View(dto);
    }
    [HttpPut]
    public IActionResult UpdateDocumento([Bind] DocumentoDTO dto)
    {
        if (ModelState.IsValid)
        {
                GetSessionDetails();
                dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
                dto.Filial = _kitandaConfig.pSessionInfo.Filial;
                DocumentoRN.GetInstance().Salvar(dto);
            return RedirectToAction("UpdateDocumento");
        }
        return View(dto);
    }
    public ActionResult DeleteDocumento(DocumentoDTO dto)
    {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            DocumentoRN.GetInstance().Excluir(dto);
        return RedirectToAction("DeleteDocumento");

    }
    public IActionResult ListDocumento(DocumentoDTO dto)
    {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            return View(DocumentoRN.GetInstance().ObterPorFiltro(dto));
        }


    public IActionResult Pesquisar(DocumentoDTO dto)
    {
            GetSessionDetails();
            dto.Utilizador = _kitandaConfig.pSessionInfo.Utilizador;
            dto.Filial = _kitandaConfig.pSessionInfo.Filial;
            return View(DocumentoRN.GetInstance().ObterPorFiltro(dto));
        }

    public IActionResult ListaDocumento(DocumentoDTO dto)
    {
        IEnumerable<DocumentoDTO> lista = DocumentoRN.GetInstance().ObterPorFiltro(dto);
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