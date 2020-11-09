using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Seguranca
{
    public class PermissaoFormularioDTO:RetornoDTO
    {
        public PermissaoFormularioDTO()
        {

        }

        public PermissaoFormularioDTO(PerfilDTO pPerfil)
        {
            Perfil = pPerfil;
        } 
         
        public FormularioDTO Formulario { get; set; } 

        public PerfilDTO Perfil { get; set; } 
        public int AllowInsert { get; set; } 
        public int AllowUpdate { get; set; } 
        public int AllowDelete { get; set; } 
        public int AllowSelect { get; set; }  
        public int AllowPrint { get; set; } 
        public int AllowAccess { get; set; }
    }

    
}
