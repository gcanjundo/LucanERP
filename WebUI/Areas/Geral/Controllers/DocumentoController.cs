using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer.Geral;
using Dominio.Geral;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.Geral.Controllers
{ 
    [Area("Geral")]
    public class DocumentoController : Controller
    {
    private List<DocumentoDTO> lista;

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
            DocumentoRN.GetInstance().Salvar(dto);

            return RedirectToAction("CreateDocumento");
        }
        return View(dto);
    }
    [HttpGet]
    public IActionResult UpdateDocumento(int? id, [Bind] DocumentoDTO dto)
    {
        return View(dto);
    }
    [HttpPut]
    public IActionResult UpdateDocumento([Bind] DocumentoDTO dto)
    {
        if (ModelState.IsValid)
        {
            DocumentoRN.GetInstance().Salvar(dto);
            return RedirectToAction("UpdateDocumento");
        }
        return View(dto);
    }
    public ActionResult DeleteDocumento(DocumentoDTO dto)
    {
        DocumentoRN.GetInstance().Excluir(dto);
        return RedirectToAction("DeleteDocumento");

    }
    public IActionResult ListDocumento(DocumentoDTO dto)
    {
        lista = new List<DocumentoDTO>();
        lista = DocumentoRN.GetInstance().ObterPorFiltro(dto);
        return View(lista);
    }


    public IActionResult Pesquisar(DocumentoDTO dto)
    {
        IEnumerable<DocumentoDTO> resultado = DocumentoRN.GetInstance().ObterPorFiltro(dto);
        return View(resultado);
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