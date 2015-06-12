using Catalog.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Catalog.Webservices
{
	/// <summary>
	/// Summary description for WsLista
	/// </summary>
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[System.ComponentModel.ToolboxItem(false)]
	// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
	[System.Web.Script.Services.ScriptService]
    public class WebService2 : System.Web.Services.WebService
	{
		[WebMethod]
		public string criarLista(string dtoChave, string dtoLista)
		{
			ControllerLista cLista = new ControllerLista();
			return cLista.criarLista(dtoChave, dtoLista);
		}

		[WebMethod]
		public string abrirLista(string dtoChave, string dtoLista)
		{
			ControllerLista cLista = new ControllerLista();
			return cLista.abrirLista(dtoChave, dtoLista);
		}

		[WebMethod]
		public string editarLista(string dtoChave, string dtoLista)
		{
			ControllerLista cLista = new ControllerLista();
			return cLista.editarLista(dtoChave, dtoLista);
		}

		[WebMethod]
		public string excluirLista(string dtoChave, string dtoLista)
		{
			ControllerLista cLista = new ControllerLista();
			return cLista.excluirLista(dtoChave, dtoLista);
		}

		[WebMethod]
		public string pesquisarLista(string dtoChave, string parametros)
		{
			ControllerLista cLista = new ControllerLista();
			return cLista.pesquisarLista(dtoChave, parametros);
		}

		[WebMethod]
		public string listarProdutos(string dtoChave, string dtoLista)
		{
			ControllerLista cLista = new ControllerLista();
			return cLista.listarProdutos(dtoChave, dtoLista);
		}

		[WebMethod]
		public string listarItensEm(string dtoChave, string dtoLista, string dtoEnderecoEstabelecimento)
		{
			ControllerLista cLista = new ControllerLista();
			return cLista.listarItensEm(dtoChave, dtoLista, dtoEnderecoEstabelecimento);
		}

		[WebMethod]
		public string adicionarProduto(string dtoChave, string dtoLista, string dtoProdutoDaLista)
		{
			ControllerLista cLista = new ControllerLista();
			return cLista.adicionarProduto(dtoChave, dtoLista, dtoProdutoDaLista);
		}

		[WebMethod]
		public string removerProduto(string dtoChave, string dtoLista, string dtoProduto)
		{
			ControllerLista cLista = new ControllerLista();
			return cLista.removerProduto(dtoChave, dtoLista, dtoProduto);
		}
	}
}
