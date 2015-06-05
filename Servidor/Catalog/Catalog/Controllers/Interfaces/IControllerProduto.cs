using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Catalog.Controllers.Interfaces
{
	public interface IControllerProduto
	{
		public string criarProduto(string dtoChave, string dtoProduto);
		public string abrirProduto(string dtoChave, string dtoProduto);
		public string pesquisarProduto(string dtoChave, string parametros);
		public string buscarItem(string dtoChave, string dtoProduto, string dtoEstabelecimento);
		public string buscarItens(string dtoChave, string dtoProduto);
		public string listarEstabelecimentosProssuidores(string dtoChave, string dtoProduto);
	}
}