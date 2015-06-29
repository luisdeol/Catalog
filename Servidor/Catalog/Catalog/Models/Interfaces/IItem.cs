using Catalog.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Catalog.Models.Interfaces
{
	public interface IItem
	{
		DtoItem criarItem(int idProduto, double preco, int idEnderecoEstabelecimento);
		DtoItem abrirItem(int idProduto, int idEnderecoEstabelecimento);
		DtoItem itemMaisBarato(int idProduto);
	}
}