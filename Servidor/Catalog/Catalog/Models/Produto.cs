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
		public DtoProduto cadastrarProduto(DtoProduto produto)
		{
			return new DtoProduto();
		}

		public DtoProduto abrirProduto(int idProduto)
		{
			return new DtoProduto();
		}

		public DtoProduto[] pesquisarProduto(string[] parametros)
		{
			return new DtoProduto[5];
		}

		public DtoItem buscarItem(int idProduto, int idEstabelecimento)
		{
			return new DtoItem();
		}

		public DtoItem[] buscarItem(int idProduto)
		{
			return new DtoItem[5];
		}

		public DtoItem itemMaisBarato(int idProduto)
		{
			return new DtoItem();
		}

		public DtoEnderecoEstabelecimento[] estabelecimentosPossuidores(int idProduto)
		{
			return new DtoEnderecoEstabelecimento[5];
		}

	}
}