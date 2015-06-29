using Catalog.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Catalog.Webservices
{
    /// <summary>
    /// Summary description for WsEstabelecimento
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class WsEstabelecimento : System.Web.Services.WebService
    {

        [WebMethod]
        public string criarEstabelecimento(string dtoEstabelecimento)
        {
            ControllerEstabelecimento cEstabelecimento = new ControllerEstabelecimento();
            cEstabelecimento.criarEstabelecimento(dtoEstabelecimento);
            return "sucesso"; //tem que retornar o DTO do EnderecoEstabelecimento com o DtoEstabelecimento
        }

		[WebMethod]
		public string pesquisarEstabelecimentos(string dtoChave, string dtoEnderecoEstabelecimento)
		{
			ControllerEstabelecimento cEstabelecimento = new ControllerEstabelecimento();
			return cEstabelecimento.pesquisarEstabelecimentos(dtoChave, dtoEnderecoEstabelecimento);
		}
    }
}
