using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Catalog.DTO
{
	public class DtoRetornoErro : DtoRetorno
	{
		public string codigoErro { get; set; }
		public string mensagem { get; set; }
	}
}