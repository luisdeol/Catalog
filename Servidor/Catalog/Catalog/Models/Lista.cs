using Catalog.DTO;
using Catalog.Linq;
using Catalog.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Catalog.Models
{
	public class Lista : ILista
	{
		public DtoLista criarLista(DtoLista lista)
		{
			lista.titulo = lista.titulo.Trim();
			if (lista.titulo == "")
				throw new DtoExcecao(DTO.Enum.CampoInvalido, "Titulo inválido");

			DBCatalogDataContext dataContext = new DBCatalogDataContext();

			var ListasSimilaresBanco = from l in dataContext.tb_Listas where l.titulo.StartsWith(lista.titulo) select l;

			if (ListasSimilaresBanco.Count() >= 1)
				lista.titulo += " " + DateTime.Now.ToString();

			tb_Lista listaBanco = new tb_Lista();
			listaBanco.titulo = lista.titulo;
			listaBanco.idUsuario = lista.idUsuario;
			dataContext.tb_Listas.InsertOnSubmit(listaBanco);
			dataContext.SubmitChanges();

			lista.id = dataContext.tb_Listas.First(l => l.idUsuario == lista.idUsuario && l.titulo == lista.titulo).id;

			return lista;
		}

		public DtoLista abrirLista(int idLista)
		{
			return null;
		}

		public void editarLista(int idLista, string novoNome)
		{
			/**/
		}

		public void excluirLista(int idLista)
		{
			/**/
		}

		public DtoLista[] pesquisarListas(int isUsuario)
		{
			return null;
		}

		public DtoLista listarProdutos(int idLista)
		{
			return null;
		}

		public DtoLista listarItensEm(int idLista, int idEstabelecimento)
		{
			return null;
		}

		public DtoProdutoDaLista adicionarProduto(DtoProdutoDaLista produto)
		{
			return null;
		}

		public void removerProduto(int idProduto)
		{
			/**/
		}
	}
}