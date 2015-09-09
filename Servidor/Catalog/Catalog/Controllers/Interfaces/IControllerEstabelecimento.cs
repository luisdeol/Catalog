using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Catalog.Controllers.Interfaces
{
	public interface IControllerEstabelecimento
	{
		string criarEstabelecimento(string dtoEnderecoEstabelecimento);
		string pesquisarEstabelecimentos(string dtoEnderecoEstabelecimento);
		string listarProdutos(string dtoEnderecoEstabelecimento);
		string pesquisarProdutos(string dtoEnderecoEstabelecimento, string parametros);
		string abrirEstabelecimento(string dtoEnderecoEstabelecimento);
		string finalizarCheckin(string dtoEnderecoEstabelecimento, string dtoItensComprados);
	}
}