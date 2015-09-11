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
        public string criarEstabelecimento(string dtoEnderecoEstabelecimento)
        {
            ControllerEstabelecimento cEstabelecimento = new ControllerEstabelecimento();
            return cEstabelecimento.criarEstabelecimento(dtoEnderecoEstabelecimento);
        }

		[WebMethod]
		public string pesquisarEstabelecimentos()
		{
			ControllerEstabelecimento cEstabelecimento = new ControllerEstabelecimento();
			return cEstabelecimento.pesquisarEstabelecimentos();
		}
		
		[WebMethod]
		public string listarProdutos(string dtoEnderecoEstabelecimento)
		{
			ControllerEstabelecimento cEstabelecimento = new ControllerEstabelecimento();
			return cEstabelecimento.listarProdutos(dtoEnderecoEstabelecimento);
		}
			
		[WebMethod]
		public string pesquisarProdutos(string dtoEnderecoEstabelecimento, string parametros)
		{
			ControllerEstabelecimento cEstabelecimento = new ControllerEstabelecimento();
			return cEstabelecimento.pesquisarProdutos(dtoEnderecoEstabelecimento, parametros);
		}
		
		[WebMethod]
		public string finalizarCheckin(string dtoEnderecoEstabelecimento, string dtoItensComprados)
		{
			ControllerEstabelecimento cEstabelecimento = new ControllerEstabelecimento();
			return cEstabelecimento.finalizarCheckin(dtoEnderecoEstabelecimento, dtoItensComprados);
		}
    }
}
