using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Catalog.DTO
{
	public class DtoUsuario
	{
		public int id { get; set; }
		public string nome { get; set; }
		public string email { get; set; }
		public string senha { get; set; }
	}
}