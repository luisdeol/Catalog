using Catalog.DTO;
using Catalog.Linq;
using Catalog.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Catalog.Models
{
    public class Estabelecimento : IEstabelecimento
    {

        public DtoEnderecoEstabelecimento cadastrarEstabelecimento(DtoEnderecoEstabelecimento enderecoEstabelecimento)
		{
            DBCatalogDataContext dataContext = new DBCatalogDataContext();
            var estabelecimentoBanco = new tb_Estabelecimento();
            var ultimoEstabelecimentoSalvo = new tb_Estabelecimento(); 

			var enderecoEstabelecimentoBanco = dataContext.tb_EnderecoEstabelecimentos.FirstOrDefault(u => 
                u.rua ==  enderecoEstabelecimento.rua && 
                u.numero ==  enderecoEstabelecimento.numero);

            if (enderecoEstabelecimentoBanco == null)
            {
                enderecoEstabelecimentoBanco = new tb_EnderecoEstabelecimento();
                estabelecimentoBanco.estabelecimento = enderecoEstabelecimento.estabelecimento.nome;
                dataContext.tb_Estabelecimentos.InsertOnSubmit(estabelecimentoBanco);
                dataContext.SubmitChanges();

                ultimoEstabelecimentoSalvo = (from ues in dataContext.tb_Estabelecimentos orderby ues.id descending select ues).First();

                enderecoEstabelecimentoBanco.idEstabelecimento = ultimoEstabelecimentoSalvo.id;
                enderecoEstabelecimentoBanco.rua = enderecoEstabelecimento.rua;
                enderecoEstabelecimentoBanco.cidade = enderecoEstabelecimento.cidade;
                enderecoEstabelecimentoBanco.estado = enderecoEstabelecimento.estado;
                enderecoEstabelecimentoBanco.numero = enderecoEstabelecimento.numero;
                enderecoEstabelecimentoBanco.cep = enderecoEstabelecimento.cep;
                enderecoEstabelecimentoBanco.latitude = enderecoEstabelecimento.latitude;
                enderecoEstabelecimentoBanco.longitude = enderecoEstabelecimento.longitude;

                dataContext.tb_EnderecoEstabelecimentos.InsertOnSubmit(enderecoEstabelecimentoBanco);
                dataContext.SubmitChanges();
            }
            else
            {
				throw new DtoExcecao(DTO.Enum.CampoInvalido, "Estabelecimento ja existente");
            }

            var estabelecimentoRetorno = new DtoEnderecoEstabelecimento();
            estabelecimentoRetorno.estabelecimento.id = ultimoEstabelecimentoSalvo.id;
            estabelecimentoRetorno.estabelecimento.nome = ultimoEstabelecimentoSalvo.estabelecimento;
            estabelecimentoRetorno.cep = enderecoEstabelecimento.cep;
            estabelecimentoRetorno.cidade = enderecoEstabelecimento.cidade;
            estabelecimentoRetorno.estado = enderecoEstabelecimento.estado;
            estabelecimentoRetorno.idEstabelecimento = ultimoEstabelecimentoSalvo.id;
            estabelecimentoRetorno.latitude = enderecoEstabelecimento.latitude;
            estabelecimentoRetorno.longitude = enderecoEstabelecimento.longitude;
            estabelecimentoRetorno.numero = enderecoEstabelecimento.numero;
            estabelecimentoRetorno.rua = enderecoEstabelecimento.rua;

            return estabelecimentoRetorno;
		}

		public DtoItem[] procurarProduto(DtoEnderecoEstabelecimento enderecoEstabelecimento, DtoProduto parametros)
		{
			List<DtoItem> itensEncontrados = new List<DtoItem>();
			DBCatalogDataContext dataContext = new DBCatalogDataContext();
			tb_EnderecoEstabelecimento estabelecimentoBanco;
			try
			{
				estabelecimentoBanco = dataContext.tb_EnderecoEstabelecimentos.First(ee => ee.id == enderecoEstabelecimento.id);
			}
			catch
			{
				throw new DtoExcecao(DTO.Enum.ObjetoNaoEncontrado, "Estabelecimento");
			}

			Item mItem = new Item();

			if (parametros.idTipo == 0)
			{
				foreach (tb_Item itemBanco in estabelecimentoBanco.tb_Items)
					itensEncontrados.Add(mItem.abrirItem(Convert.ToInt32(itemBanco.idProduto), Convert.ToInt32(itemBanco.idEstabelecimento)));
			}
			else
			{
				foreach (tb_Item itemBanco in estabelecimentoBanco.tb_Items)
					if (itemBanco.tb_Produto.nome.StartsWith(parametros.nome) &&
						itemBanco.tb_Produto.idTipo == parametros.idTipo &&
						itemBanco.tb_Produto.tb_Fabricante.fabricante.StartsWith(parametros.fabricante.fabricante))
					{
						itensEncontrados.Add(mItem.abrirItem(Convert.ToInt32(itemBanco.idProduto), Convert.ToInt32(itemBanco.idEstabelecimento)));
					}
			}

			if (itensEncontrados.Count < 1)
				throw new DtoExcecao(DTO.Enum.ObjetoNaoEncontrado, "Item");

			return itensEncontrados.ToArray();
		}

		public DtoEnderecoEstabelecimento[] procurarEstabelecimento(DtoEnderecoEstabelecimento parametros)
		{
			DBCatalogDataContext dataContext = new DBCatalogDataContext();
			DtoEnderecoEstabelecimento[] estabelecimentos;

			var enderecosEstabelecimentosBanco = from ee in dataContext.tb_EnderecoEstabelecimentos
												 where ee.cep == parametros.cep
													&& ee.estado.StartsWith(parametros.estado)
													&& ee.cidade.StartsWith(parametros.cidade)
													&& ee.rua.StartsWith(parametros.rua)
													&& ee.numero == parametros.numero
													&& ee.tb_Estabelecimento.estabelecimento.StartsWith(parametros.estabelecimento.nome)
												 orderby ee.tb_Estabelecimento.estabelecimento
												 select ee;

			if (enderecosEstabelecimentosBanco.Count() < 1)
				throw new DtoExcecao(DTO.Enum.ObjetoNaoEncontrado, "estabelecimentos");

			estabelecimentos = new DtoEnderecoEstabelecimento[enderecosEstabelecimentosBanco.Count()];
			int i = 0;
			foreach (tb_EnderecoEstabelecimento enderecoEstabelecimentoBanco in enderecosEstabelecimentosBanco)
			{
				estabelecimentos[i] = new DtoEnderecoEstabelecimento();
				estabelecimentos[i].cep = enderecoEstabelecimentoBanco.cep;
				estabelecimentos[i].rua = enderecoEstabelecimentoBanco.rua;
				estabelecimentos[i].cidade = enderecoEstabelecimentoBanco.cidade;
				estabelecimentos[i].estado = enderecoEstabelecimentoBanco.estado;
				//estabelecimentos[i].latitude = Convert.ToDouble(enderecoEstabelecimentoBanco.latitude);
				//estabelecimentos[i].longitude = Convert.ToDouble(enderecoEstabelecimentoBanco.longitude);
				estabelecimentos[i].numero = enderecoEstabelecimentoBanco.numero;
				estabelecimentos[i].id = enderecoEstabelecimentoBanco.id;
				estabelecimentos[i].estabelecimento = new DtoEstabelecimento();
				estabelecimentos[i].estabelecimento.id = estabelecimentos[i].id;
				estabelecimentos[i].estabelecimento.nome = enderecoEstabelecimentoBanco.tb_Estabelecimento.estabelecimento;
				i++;
			}

			return estabelecimentos;
		}
    }
}