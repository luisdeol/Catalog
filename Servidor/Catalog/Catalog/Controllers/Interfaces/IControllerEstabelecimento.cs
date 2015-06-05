using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Catalog.Controllers.Interfaces
{
	public interface IControllerEstabelecimento
	{
		public string criarestabelecimento(string dtochave, string dtoestabelecimento);
		public string pesquisarestabelecimentos(string dtochave, string parametros);
		public string listarprodutos(string dtochave, string dtoestabelecimento);
		public string pesquisarprodutos(string dtochave, string dtoestabelecimento);
	}
}