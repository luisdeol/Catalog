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
			if (idProduto < 1)
				throw new DtoExcecao(DTO.Enum.ObjetoNaoEncontrado, "o produto solicitado");
			if (idEnderecoEstabelecimento < 1)
				throw new DtoExcecao(DTO.Enum.ObjetoNaoEncontrado, "o estabelecimento solicitado");
			if (preco < 1)
				throw new DtoExcecao(DTO.Enum.CampoInvalido, "Preço do Produto");

			DBCatalogDataContext dataContext = new DBCatalogDataContext();

			tb_Produto produtoBanco;
			try
			{ produtoBanco = dataContext.tb_Produtos.First(p => p.id == idProduto); }
			catch
			{ throw new DtoExcecao(DTO.Enum.ObjetoNaoEncontrado, "o produto solicitado"); }

			tb_EnderecoEstabelecimento enderecoEstabelecimentoBanco;
			try
			{ enderecoEstabelecimentoBanco = dataContext.tb_EnderecoEstabelecimentos.First(ee => ee.id == idEnderecoEstabelecimento); }
			catch
			{ throw new DtoExcecao(DTO.Enum.ObjetoNaoEncontrado, "o estabelecimento solicitado"); }

			DateTime dataAtual = DateTime.Now;
			tb_Item itemBanco;
			try
			{
				itemBanco = dataContext.tb_Items.First(i => i.idProduto == idProduto && i.idEstabelecimento == idEnderecoEstabelecimento && i.preco == preco && i.compraRecente == dataAtual);
				itemBanco.qualificacao++;
			}
			catch
			{
				itemBanco = new tb_Item();
				itemBanco.idEstabelecimento = idEnderecoEstabelecimento;
				itemBanco.idProduto = idProduto;
				itemBanco.preco = preco;
				itemBanco.qualificacao = 1;
				dataContext.tb_Items.InsertOnSubmit(itemBanco);
			}
			itemBanco.compraRecente = dataAtual;
			dataContext.SubmitChanges();

			DtoItem item = new DtoItem();
			item.id = (dataContext.tb_Items.First(i => i.idProduto == idProduto && i.idEstabelecimento == idEnderecoEstabelecimento && i.preco == preco && i.compraRecente == dataAtual)).id;
			item.idProduto = idProduto;
			item.idEstabelecimento = idEnderecoEstabelecimento;
			item.preco = preco;
			item.qualificacao = Convert.ToInt32(itemBanco.qualificacao);
			item.data = dataAtual;
			Produto mProduto = new Produto();
			item.produto = mProduto.abrirProduto(idProduto);
			Estabelecimento mEstabelecimento = new Estabelecimento();
			item.estabelecimento = mEstabelecimento.abrirEstabelecimento(idEnderecoEstabelecimento);

			return item;
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
			Estabelecimento mEstabelecimento = new Estabelecimento();
			item.estabelecimento = mEstabelecimento.abrirEstabelecimento(idEnderecoEstabelecimento);
			item.produto = mProduto.abrirProduto(idProduto);
			mProduto = null;

			var itensBanco = from i in dataContext.tb_Items
							 where i.idProduto == idProduto
								&& i.idEstabelecimento == idEnderecoEstabelecimento
							 orderby i.compraRecente descending, i.qualificacao descending
							 select i;

			tb_Item itemBanco;
			if (itensBanco.Count() < 1)
			{
				item.id = 0;
				return item;
			}
			else
			{
				itemBanco = itensBanco.First();
			}
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