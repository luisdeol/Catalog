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
        public string criarEstabelecimento(string dtoChave, string dtoEnderecoEstabelecimento)
        {
            ControllerEstabelecimento cEstabelecimento = new ControllerEstabelecimento();
            return cEstabelecimento.criarEstabelecimento(dtoChave, dtoEnderecoEstabelecimento);
        }

		[WebMethod]
		public string pesquisarEstabelecimentos(string dtoChave, string dtoEnderecoEstabelecimento)
		{
			ControllerEstabelecimento cEstabelecimento = new ControllerEstabelecimento();
			return cEstabelecimento.pesquisarEstabelecimentos(dtoChave, dtoEnderecoEstabelecimento);
		}
		
		[WebMethod]
		public string listarProdutos(string dtoChave, string dtoEnderecoEstabelecimento)
		{
			ControllerEstabelecimento cEstabelecimento = new ControllerEstabelecimento();
			return cEstabelecimento.listarProdutos(dtoChave, dtoEnderecoEstabelecimento);
		}
			
		[WebMethod]
		public string pesquisarProdutos(string dtoChave, string dtoEnderecoEstabelecimento, string parametros)
		{
			ControllerEstabelecimento cEstabelecimento = new ControllerEstabelecimento();
			return cEstabelecimento.pesquisarProdutos(dtoChave, dtoEnderecoEstabelecimento, parametros);
		}
		
		[WebMethod]
		public string finalizarCheckin(string dtoChave, string dtoEnderecoEstabelecimento, string dtoItensComprados)
		{
			ControllerEstabelecimento cEstabelecimento = new ControllerEstabelecimento();
			return cEstabelecimento.finalizarCheckin(dtoChave, dtoEnderecoEstabelecimento, dtoItensComprados);
		}
    }
}
