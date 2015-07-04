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

        public void criarEstabelecimento(DtoEnderecoEstabelecimento enderecoEstabelecimento)
		{
            DBCatalogDataContext dataContext = new DBCatalogDataContext();
            var estabelecimentoBanco = new tb_Estabelecimento();

			var enderecoEstabelecimentoBanco = dataContext.tb_EnderecoEstabelecimentos.FirstOrDefault(u => 
                u.rua ==  enderecoEstabelecimento.rua && 
                u.numero ==  enderecoEstabelecimento.numero);

            if (enderecoEstabelecimentoBanco == null)
            {
                enderecoEstabelecimentoBanco = new tb_EnderecoEstabelecimento();
                estabelecimentoBanco.estabelecimento = enderecoEstabelecimento.estabelecimento.nome;
                dataContext.tb_Estabelecimentos.InsertOnSubmit(estabelecimentoBanco);
                dataContext.SubmitChanges();

                var ultimoEstabelecimentoSalvo = (from ues in dataContext.tb_Estabelecimentos orderby ues.id descending select ues).First();

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
		}

		public DtoItem[] procurarProduto(DtoProduto parametros)
		{
			return null;
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