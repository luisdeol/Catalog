using Catalog.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Catalog.Models.Interfaces
{
	public interface ITipoProduto
	{
		DtoTipo abrirTipo(int idTipo);
		DtoTipo abrirTipo(string tipo);
	}
}