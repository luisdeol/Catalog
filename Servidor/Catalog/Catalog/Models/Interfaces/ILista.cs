using Catalog.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Catalog.Models.Interfaces
{
	public interface ILista
	{
		DtoLista criarLista(DtoLista lista);
		DtoLista abrirLista(int idLista);
		void editarLista(int idLista, string novoNome);
		void excluirLista(int idLista);
		DtoLista[] pesquisarListas(int idUsuario);
		DtoLista listarItensEm(int idLista, int idEstabelecimento);
		DtoProdutoDaLista adicionarProduto(DtoProdutoDaLista produto);
		void removerProduto(int idProduto);
	}
}