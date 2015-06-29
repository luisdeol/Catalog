using Catalog.DTO;
using Catalog.Linq;
using Catalog.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Catalog.Models
{
	public class Item : IItem
	{
		public DtoItem criarItem(int idProduto, double preco, int idEnderecoEstabelecimento)
		{
			return new DtoItem();
		}

		public DtoItem abrirItem(int idProduto, int idEnderecoEstabelecimento)
		{
			if (idProduto < 1)
				throw new DtoExcecao(DTO.Enum.ObjetoNaoEncontrado, "o produto solicitado");
			if (idEnderecoEstabelecimento < 1)
				throw new DtoExcecao(DTO.Enum.ObjetoNaoEncontrado, "o estabelecimento solicitado");

			DBCatalogDataContext dataContext = new DBCatalogDataContext();

			Produto mProduto = new Produto();
			DtoItem item = new DtoItem();
			item.idProduto = idProduto;
			item.idEstabelecimento = idEnderecoEstabelecimento;
			item.produto = mProduto.abrirProduto(idProduto);
			mProduto = null;

			var itensBanco = from i in dataContext.tb_Items
							 where i.idProduto == idProduto
								&& i.idEstabelecimento == idEnderecoEstabelecimento
							 orderby i.compraRecente descending
							 select i;

			if (itensBanco.Count() < 1)
			{
				item.id = 0;
				return item;
			}

			if(itensBanco.Count() > 1)
				itensBanco = from i in itensBanco
							 where i.idProduto == idProduto
								&& i.idEstabelecimento == idEnderecoEstabelecimento
								&& i.compraRecente == itensBanco.First().compraRecente
							 orderby i.qualificacao descending
							 select i;

			tb_Item itemBanco = itensBanco.First();
			item.id = itemBanco.id;
			item.preco = Convert.ToDouble(itemBanco.preco);
			item.qualificacao = Convert.ToInt32(itemBanco.qualificacao);
			item.data = Convert.ToDateTime(itemBanco.compraRecente);

			return item;
		}

		public DtoItem itemMaisBarato(int idProduto)
		{
			return new DtoItem();
		}
	}
}