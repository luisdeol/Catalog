using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Catalog.Controllers.Interfaces
{
	public interface IControllerLista
	{
		public string criarLista(string dtoChave, string dtoLista);
		public string abrirLista(string dtoChave, string dtoLista);
		public string editarLista(string dtoChave, string dtoLista);
		public string excluirLista(string dtoChave, string dtoLista);
		public string pesquisarLista(string dtoChave, string parametros);
		public string listarProdutos(string dtoChave, string dtoLista);
		public string listarItensEm(string dtoChave, string dtoLista, string dtoEnderecoEstabelecimento);
		public string adicionarProduto(string dtoChave, string dtoLista, string dtoProdutoDaLista);
		public string removerProduto(string dtoChave, string dtoLista, string dtoProduto);
	}
}