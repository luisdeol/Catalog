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
		public string criarLista(string dtoLista)
		{
			ControllerLista cLista = new ControllerLista();
			return cLista.criarLista(dtoLista);
		}

		[WebMethod]
		public string abrirLista(string dtoLista)
		{
			ControllerLista cLista = new ControllerLista();
			return cLista.abrirLista(dtoLista);
		}

        [WebMethod]
        public string pesquisarLista(int idUsuario)
        {
            ControllerLista cLista = new ControllerLista();
            return cLista.pesquisarLista(idUsuario);
        }

		[WebMethod]
		public string editarLista(string dtoLista)
		{
			ControllerLista cLista = new ControllerLista();
			return cLista.editarLista(dtoLista);
		}

		[WebMethod]
		public string excluirLista(string dtoLista)
		{
			ControllerLista cLista = new ControllerLista();
			return cLista.excluirLista(dtoLista);
		}

		[WebMethod]
		public string listarProdutos(string dtoLista)
		{
			ControllerLista cLista = new ControllerLista();
			return cLista.abrirLista(dtoLista);
		}

		[WebMethod]
		public string listarItensEm(int idLista, int idEstabelecimento)
		{
			ControllerLista cLista = new ControllerLista();
			return cLista.listarItensEm(idLista, idEstabelecimento);
		}

		[WebMethod]
		public string adicionarProduto(string dtoLista, string dtoProdutoDaLista)
		{
			ControllerLista cLista = new ControllerLista();
			return cLista.adicionarProduto(dtoLista, dtoProdutoDaLista);
		}

		[WebMethod]
		public string removerProduto(string dtoLista, string dtoProduto)
		{
			ControllerLista cLista = new ControllerLista();
			return cLista.removerProduto(dtoLista, dtoProduto);
		}
	}
}
