using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Catalog.DTO
{
	public class DtoProdutoDaLista
	{
		public int id { get; set; }
		public int quantidade { get; set; }
		public int idLista { get; set; }
		public int idProduto { get; set; }
		public DtoProduto produto { get; set; }
		public DtoItem item { get; set; }
	}
}