using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Catalog.DTO
{
	public class DtoChave
	{
		public int id { get; set; }
		public int idUsuario { get; set; }
		public string token { get; set; }
		public DateTime ultimoAcesso { get; set; }
	}
}