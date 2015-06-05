using Catalog.Controllers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Catalog.Controllers
{
	public class ControllerLista : IControllerLista
	{
		public string criarLista(string dtoChave, string dtoLista)
		{
			JavaScriptSerializer js = new JavaScriptSerializer();
			return "";
		}

		public string abrirLista(string dtoChave, string dtoLista)
		{
			return "";
		}

		public string editarLista(string dtoChave, string dtoLista)
		{
			return "";
		}

		public string excluirLista(string dtoChave, string dtoLista)
		{
			return "";
		}

		public string pesquisarLista(string dtoChave, string parametros)
		{
			return "";
		}

		public string listarProdutos(string dtoChave, string dtoLista)
		{
			return "";
		}

		public string listarItensEm(string dtoChave, string dtoLista, string dtoEstabelecimento)
		{
			return "";
		}

		public string adicionarProduto(string dtoChave, string dtoLista, string dtoProduto)
		{
			return "";
		}

		public string removerProduto(string dtoChave, string dtoLista, string dtoProduto)
		{
			return "";
		}
	}
}