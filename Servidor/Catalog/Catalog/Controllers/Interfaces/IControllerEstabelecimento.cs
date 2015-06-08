using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Catalog.Controllers.Interfaces
{
	public interface IControllerEstabelecimento
	{
		string criarestabelecimento(string dtochave, string dtoestabelecimento);
		string pesquisarestabelecimentos(string dtochave, string parametros);
		string listarprodutos(string dtochave, string dtoestabelecimento);
		string pesquisarprodutos(string dtochave, string dtoestabelecimento);
	}
}