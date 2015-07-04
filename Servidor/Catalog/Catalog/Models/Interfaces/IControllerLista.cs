using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Catalog.Controllers.Interfaces
{
	public interface IControllerLista
	{
		string criarLista(string dtoChave, string dtoLista);
		string abrirLista(string dtoChave, string dtoLista);
		string editarLista(string dtoChave, string dtoLista);
		string excluirLista(string dtoChave, string dtoLista);
		string pesquisarLista(string dtoChave);
		string listarItensEm(string dtoChave, string dtoLista, string dtoEnderecoEstabelecimento);
		string adicionarProduto(string dtoChave, string dtoLista, string dtoProdutoDaLista);
		string removerProduto(string dtoChave, string dtoLista, string dtoProduto);
	}
}