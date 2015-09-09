using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Catalog.Controllers.Interfaces
{
	public interface IControllerLista
	{
		string criarLista(string dtoLista);
		string abrirLista(string dtoLista);
		string editarLista(string dtoLista);
		string excluirLista(string dtoLista);
		string pesquisarLista(string dtoChave);
		string listarItensEm(string dtoLista, string dtoEnderecoEstabelecimento);
		string adicionarProduto(string dtoLista, string dtoProdutoDaLista);
		string removerProduto(string dtoLista, string dtoProduto);
	}
}