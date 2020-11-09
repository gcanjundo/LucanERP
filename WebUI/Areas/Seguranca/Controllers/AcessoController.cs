using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using BusinessLogicLayer.Seguranca;
using Dominio.Seguranca; 
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [Area("Seguranca")]
    public class AcessoController : Controller
    {
        KitandaConfig _kitandaConfig;
        public AcessoController(KitandaConfig kitandaConfig)
        {
            _kitandaConfig = kitandaConfig;
        }

        [HttpGet]
        public IActionResult RegistrarUtilizador()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegistrarUtilizador([Bind] UtilizadorDTO usuario)
        {
            /*
            if (ModelState.IsValid)
            {
                string RegistroStatus = _autentica.RegistrarUsuario(usuario);
                if (RegistroStatus == "Sucesso")
                {
                    ModelState.Clear();
                    TempData["Sucesso"] = "Registro realizado com sucesso!";
                    return View();
                }
                else
                {
                    TempData["Falhou"] = "O Registro do usuário falhou.";
                    return View();
                }
            }*/
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login ([Bind] AcessoDTO userCredentials)
        {
            userCredentials.Maquina = Environment.MachineName;
            userCredentials.IP = ObterEnderecoIP();
            userCredentials.ServerName = Environment.MachineName;
            userCredentials.CurrentPassword = _kitandaConfig.Encrypt(userCredentials.CurrentPassword);
            userCredentials.Url = _kitandaConfig.FilePath;

            userCredentials = AcessoRN.GetInstance().Entrar(userCredentials);

            if (string.IsNullOrEmpty(userCredentials.MensagemErro))
            {
                
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, userCredentials.Utilizador)
                    };

                ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "login");
                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                await HttpContext.SignInAsync(principal);

                if (User.Identity.IsAuthenticated)
                {
                    if (!userCredentials.Url.Contains("BranchSelectionSection"))
                    {
                        return RedirectToAction("BranchSelection", "Home");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }else
                {
                    TempData["LoginUsuarioFalhou"] = "O login Falhou. Informe as credenciais corretas " + User.Identity.Name;
                    return RedirectToAction("Login", "Login");
                }

            }
            else
            {
                TempData["LoginUsuarioFalhou"] = userCredentials.MensagemErro;
                return View();
            }
        }

        protected string ObterEnderecoIP()
        {
            string strHostName = Dns.GetHostName();
            IPHostEntry ipHostInfo = Dns.GetHostEntry(strHostName);
            return ipHostInfo.AddressList.Where(t => t.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).FirstOrDefault().ToString(); 
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Login");
        }



    }
}
