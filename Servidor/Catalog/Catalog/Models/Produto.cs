using Catalog.DTO;
using Catalog.Linq;
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
			produto.nome = produto.nome.Trim();
			if (produto.nome == "")
				throw new DtoExcecao(DTO.Enum.CampoInvalido, "Nome do Produto");

			try
			{
				produto.tipo = (new TipoProduto()).abrirTipo(produto.idTipo);
				produto.fabricante = (new Fabricante()).cadastrarFabricante(produto.fabricante);
				produto.idFabricante = produto.fabricante.id;
			}
			catch (DtoExcecao ex)
			{
				throw ex;
			}
			catch
			{
				throw new DtoExcecao(DTO.Enum.CampoInvalido, "Tipo do Produto");
			}

			DBCatalogDataContext dataContext = new DBCatalogDataContext();

			tb_Produto produtoBanco;
			if (produto.codigoDeBarras == "")
			{
				produtoBanco = dataContext.tb_Produtos.FirstOrDefault(p => p.nome == produto.nome &&
																			p.idTipo == produto.idTipo &&
																			p.idFabricante == produto.idFabricante);
			}
			else
			{
				produtoBanco = dataContext.tb_Produtos.FirstOrDefault(p => p.nome == produto.nome &&
																			p.idTipo == produto.idTipo &&
																			p.idFabricante == produto.idFabricante &&
																			(p.codigoDeBarras == produto.codigoDeBarras || p.codigoDeBarras == ""));
			}

			if (produtoBanco != null)
			{
				produto.id = produtoBanco.id;
				if (produto.codigoDeBarras != "" && produtoBanco.codigoDeBarras == "")
				{//esse produto existe, mas sem codigo de barras
					produtoBanco.codigoDeBarras = produto.codigoDeBarras;
					//produtoBanco.tipoCodigo = produto.tipoCodigoDeBarras;
					dataContext.SubmitChanges();
				}

			}
			else
			{//não existe esse produto
				produtoBanco = new tb_Produto();
				produtoBanco.nome = produto.nome;
				produtoBanco.idTipo = produto.idTipo;
				produtoBanco.idFabricante = produto.idFabricante;
				produtoBanco.codigoDeBarras = produto.codigoDeBarras;
				//produtoBanco.tipoCodigo = produto.tipoCodigoDeBarras;
				dataContext.tb_Produtos.InsertOnSubmit(produtoBanco);
				dataContext.SubmitChanges();
				produto.id = dataContext.tb_Produtos.FirstOrDefault(p => p.idFabricante == produto.idFabricante &&
																		 p.idTipo == produto.idTipo &&
																		 p.nome == produto.nome).id;
			}

			return produto;
		}

		public DtoProduto abrirProduto(int idProduto)
		{
			if (idProduto < 1)
				throw new DtoExcecao(DTO.Enum.CampoInvalido, "id do produto");

			DtoProduto produto;
			try
			{
				DBCatalogDataContext dataContext = new DBCatalogDataContext();
				tb_Produto produtoBanco = dataContext.tb_Produtos.First(p => p.id == idProduto);
				produto = new DtoProduto();
				produto.id = produtoBanco.id;
				produto.nome = produtoBanco.nome;
				produto.codigoDeBarras = produtoBanco.codigoDeBarras;
				produto.tipo = new DtoTipo();
				produto.tipo.id = produto.idTipo = Convert.ToInt32(produtoBanco.idTipo);
				produto.tipo.tipo = produtoBanco.tb_Tipo.tipo;
				produto.fabricante = new DtoFabricante();
				produto.idFabricante = produto.fabricante.id = Convert.ToInt32(produtoBanco.idFabricante);
				produto.fabricante.fabricante = produtoBanco.tb_Fabricante.fabricante;
			}
			catch (Exception ex)
			{
				throw new DtoExcecao(DTO.Enum.ObjetoNaoEncontrado, "produto com id " + idProduto);
			}

			return produto;
		}

		public DtoProduto[] pesquisarProduto(DtoProduto parametros)
		{
			List<DtoProduto> produtos = new List<DtoProduto>();

			if (parametros.fabricante == null)
				parametros.fabricante = new DtoFabricante();
			if (parametros.tipo == null)
				parametros.tipo = new DtoTipo();

			DBCatalogDataContext dataContext = new DBCatalogDataContext();
			DtoProduto produto;
			if (parametros.codigoDeBarras != "")
			{
				var produtosBanco = from p in dataContext.tb_Produtos where p.codigoDeBarras == parametros.codigoDeBarras select p;

				foreach (tb_Produto produtoBanco in produtosBanco)
				{
					produto = new DtoProduto();
					produto.id = produtoBanco.id;
					produto.nome = produtoBanco.nome;
					produto.codigoDeBarras = produtoBanco.codigoDeBarras;
					produto.tipo = new DtoTipo();
					produto.tipo.id = produto.idTipo = Convert.ToInt32(produtoBanco.idTipo);
					produto.tipo.tipo = produtoBanco.tb_Tipo.tipo;
					produto.fabricante = new DtoFabricante();
					produto.idFabricante = produto.fabricante.id = Convert.ToInt32(produtoBanco.idFabricante);
					produto.fabricante.fabricante = produtoBanco.tb_Fabricante.fabricante;
					produtos.Add(produto);
				}
			}
			else
			{
				var produtosBanco = from p in dataContext.tb_Produtos where
										p.nome.StartsWith(parametros.nome) &&
										p.tb_Fabricante.fabricante.StartsWith(parametros.fabricante.fabricante) &&
										p.tb_Tipo.tipo.StartsWith(parametros.tipo.tipo)
									orderby
										p.nome
									select p;

				foreach (tb_Produto produtoBanco in produtosBanco)
				{
					produto = new DtoProduto();
					produto.id = produtoBanco.id;
					produto.nome = produtoBanco.nome;
					produto.codigoDeBarras = produtoBanco.codigoDeBarras;
					produto.tipo = new DtoTipo();
					produto.tipo.id = produto.idTipo = Convert.ToInt32(produtoBanco.idTipo);
					produto.tipo.tipo = produtoBanco.tb_Tipo.tipo;
					produto.fabricante = new DtoFabricante();
					produto.idFabricante = produto.fabricante.id = Convert.ToInt32(produtoBanco.idFabricante);
					produto.fabricante.fabricante = produtoBanco.tb_Fabricante.fabricante;
					produtos.Add(produto);
				}
			}

			return produtos.ToArray();
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