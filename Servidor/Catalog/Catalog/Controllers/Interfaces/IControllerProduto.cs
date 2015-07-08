using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Catalog.Controllers.Interfaces
{
	public interface IControllerProduto
	{
		string criarProduto(string dtoChave, string dtoProduto);
		string abrirProduto(string dtoChave, string dtoProduto);
		string pesquisarProduto(string dtoChave, string parametros);
		string buscarItem(string dtoChave, string dtoProduto, string dtoEstabelecimento);
		string listarEstabelecimentosProssuidores(string dtoChave, string dtoProduto);
	}
}