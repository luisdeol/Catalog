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
		DtoProduto[] pesquisarProduto(string[] parametros);
		DtoItem buscarItem(int idProduto, int idEstabelecimento);
		DtoItem[] buscarItem(int idProduto);
		DtoItem itemMaisBarato(int idProduto);
		DtoEnderecoEstabelecimento[] estabelecimentosPossuidores(int idProduto);
	}
}