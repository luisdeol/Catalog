using Catalog.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Catalog.Models.Interfaces
{
	public interface IProduto
	{
		DtoProduto cadastrarProduto(DtoProduto produto);
		DtoProduto abrirProduto(int idProduto);
		DtoProduto[] pesquisarProduto(DtoProduto parametros);
		DtoItem itemMaisBarato(int idProduto);
		DtoItem[] estabelecimentosPossuidores(int idProduto);
	}
}