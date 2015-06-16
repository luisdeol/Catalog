using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Catalog.DTO
{
	public class DtoEnderecoEstabelecimento
	{
		public int id { get; set; }
		public string rua { get; set; }
		public string cidade { get; set; }
		public string estado { get; set; }
		public string numero { get; set; }
		public string cep { get; set; }
		public float latitude { get; set; }
		public float longitude { get; set; }
		public int idEstabelecimento { get; set; }
		public DtoEstabelecimento estabelecimento { get; set; }
	}
}