using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Catalog.DTO
{
	public class DtoLista
	{
		public int id { get; set; }
		public string titulo { get; set; }
		public int idUsuario { get; set; }
		public int precoTotal { get; set; }
		public DtoProdutoDaLista[] produtosDaLista { get; set; }
	}
}