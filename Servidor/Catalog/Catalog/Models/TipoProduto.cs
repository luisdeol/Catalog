using Catalog.DTO;
using Catalog.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Catalog.Models
{
	public class TipoProduto : ITipoProduto
	{
		public DtoTipo abrirTipo(int idTipo)
		{
			return new DtoTipo();
		}

		public DtoTipo abrirTipo(string tipo)
		{
			return new DtoTipo();
		}

	}
}