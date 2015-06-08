using Catalog.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Catalog.Models.Interfaces
{
	public interface IEstabelecimento
	{
		DtoEnderecoEstabelecimento cadastrarEstabelecimento(DtoEnderecoEstabelecimento estabelecimento);
		DtoItem[] procurarProduto(string[] parametros);
	}
}