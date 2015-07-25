using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Catalog.Controllers.Interfaces
{
	public interface IControllerEstabelecimento
	{
		string criarEstabelecimento(string dtoChave, string dtoEnderecoEstabelecimento);
		string pesquisarEstabelecimentos(string dtoChave, string dtoEnderecoEstabelecimento);
		string listarProdutos(string dtoChave, string dtoEnderecoEstabelecimento);
		string pesquisarProdutos(string dtoChave, string dtoEnderecoEstabelecimento, string parametros);
		string abrirEstabelecimento(string dtoChave, string dtoEnderecoEstabelecimento);
		string finalizarCheckin(string dtoChave, string dtoEnderecoEstabelecimento, string dtoItensComprados);
	}
}