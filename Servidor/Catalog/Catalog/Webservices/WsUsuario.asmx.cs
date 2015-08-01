using Catalog.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Catalog.Webservices
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod]
        public string logar(string dtoUsuario)
        {
            ControllerUsuario cUsuario = new ControllerUsuario();
            return cUsuario.logar(dtoUsuario);
        }

        [WebMethod]
        public string realizarCadastro(string dtoUsuario)
        {
            ControllerUsuario cUsuario = new ControllerUsuario();
            return cUsuario.realizarCadastro(dtoUsuario);
        }

        [WebMethod]
        public string alterarDadosCadastrais(string senha, string novaSenha)
        {
            ControllerUsuario cUsuario = new ControllerUsuario();
            return cUsuario.alterarDadosCadastrais(senha, novaSenha);
        }

        [WebMethod]
        public string recuperarSenha(string dtoUsuario)
        {
            ControllerUsuario cUsuario = new ControllerUsuario();
            return cUsuario.recuperarSenha(dtoUsuario);
        }
    }
}
