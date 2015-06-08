using Catalog.DTO;
using Catalog.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Catalog.Models
{
	public class Produto : IProduto
	{
		public DtoProduto cadastrarProduto(DtoProduto produto);
		public DtoProduto abrirProduto(int idProduto);
		public DtoProduto[] pesquisarProduto(string[] parametros);
		public DtoItem buscarItem(int idProduto, int idEstabelecimento);
		public DtoItem[] buscarItem(int idProduto);
		public DtoItem itemMaisBarato(int idProduto);
		public DtoEnderecoEstabelecimento[] estabelecimentosPossuidores(int idProduto);
	}
}