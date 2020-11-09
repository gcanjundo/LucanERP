using Dominio.Geral;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Seguranca
{
    

    public class UtilizadorDTO:EntidadeDTO
    {
        
        public PerfilDTO Perfil { get; set; }
        public IdiomaDTO Idioma { get; set; }
        public int IdiomaID { get; set; } 
         

        public UtilizadorDTO()
        {
            Utilizador = "";
            SocialName = "";
            Email = "";
        }

        public UtilizadorDTO(string pUtilizador) 
        {
            Utilizador = pUtilizador;
            Situacao = string.Empty;
            SocialName = string.Empty;
            Email = string.Empty;
            Perfil = new PerfilDTO();
        }

        public UtilizadorDTO(string pUtilizador, int pFilial)
        {
            Utilizador = pUtilizador;
            Filial = pFilial.ToString();
            Perfil = new PerfilDTO();
        }

        public UtilizadorDTO(string pUtilizador, string pNome)
        {
            // TODO: Complete member initialization
            Utilizador = pUtilizador;
            SocialName = pNome;
            Perfil = new PerfilDTO();
        }

        public UtilizadorDTO(string pUtilizador, string pNome, string pEmail)
        {
            // TODO: Complete member initialization
            Utilizador = pUtilizador;
            SocialName = pNome;
            Email = pEmail;
            Perfil = new PerfilDTO();
        }

        public UtilizadorDTO(string pUtilizador, string pNome, int pPerfil)
        {
            Utilizador = pUtilizador;
            Perfil = new PerfilDTO { Codigo = pPerfil };
        } 

        public string DescricaoPerfil { get; set; }

        
    }
}
