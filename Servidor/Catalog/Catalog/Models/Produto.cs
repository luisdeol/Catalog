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
				throw new Exception(); //nome do produto inválido

			try
			{
				produto.tipo = (new TipoProduto()).abrirTipo(produto.idTipo);
				produto.fabricante = (new Fabricante()).cadastrarFabricante(produto.fabricante);
				produto.idFabricante = produto.fabricante.id;
			}
			catch (Exception ex)
			{
				throw ex; // tipo inválido;
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