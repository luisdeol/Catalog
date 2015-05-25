using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Catalog.DTO
{
	public class DtoRetornoObjeto : DtoRetorno
	{
		public Object objeto { get; set; }
		public DtoChave chave { get; set; }
	}
}