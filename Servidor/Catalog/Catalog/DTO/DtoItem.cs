using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Catalog.DTO
{
	public class DtoItem
	{
		public int id { get; set; }
		public double preco { get; set; }
		public DateTime data { get; set; }
		public int qualificacao { get; set; }
		public int idProduto { get; set; }
		public DtoProduto produto { get; set; }
		public int idEstabelecimento { get; set; }
		public DtoEstabelecimento estabelecimento { get; set; }
	}
}