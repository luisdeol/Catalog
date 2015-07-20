using Catalog.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Catalog.Webservices
{
	/// <summary>
	/// Summary description for WsProduto
	/// </summary>
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[System.ComponentModel.ToolboxItem(false)]
	// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
	[System.Web.Script.Services.ScriptService]
	public class WsProduto : System.Web.Services.WebService
	{

		[WebMethod]
		public string criarProduto(string dtoChave, string dtoProduto)
		{
			ControllerProduto cProduto = new ControllerProduto();
			return cProduto.criarProduto(dtoChave, dtoProduto);
		}

		[WebMethod]
		public string abrirProduto(string dtoChave, string dtoProduto)
		{
			ControllerProduto cProduto = new ControllerProduto();
			return cProduto.abrirProduto(dtoChave, dtoProduto);
		}

		[WebMethod]
		public string pesquisarProduto(string dtoChave, string parametros)
		{
			ControllerProduto cProduto = new ControllerProduto();
			return cProduto.pesquisarProduto(dtoChave, parametros);
		}

		[WebMethod]
		public string buscarItem(string dtoChave, string dtoProduto, string dtoEstabelecimento)
		{
			ControllerProduto cProduto = new ControllerProduto();
			return cProduto.buscarItem(dtoChave, dtoProduto, dtoEstabelecimento);
		}

        //[WebMethod]
        //public string buscarItens(string dtoChave, string dtoProduto)
        //{
        //    ControllerProduto cProduto = new ControllerProduto();
        //    return cProduto.buscarItens(dtoChave, dtoProduto);
        //}

		[WebMethod]
		public string listarEstabelecimentosProssuidores(string dtoChave, string dtoProduto)
		{
			ControllerProduto cProduto = new ControllerProduto();
			return cProduto.listarEstabelecimentosProssuidores(dtoChave, dtoProduto);
		}
	}
}
