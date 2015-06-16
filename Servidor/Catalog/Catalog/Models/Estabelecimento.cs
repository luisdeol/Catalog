using Catalog.DTO;
using Catalog.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Catalog.Models
{
    public class Estabelecimento
    {
        public void criarEstabelecimento(DtoEnderecoEstabelecimento enderecoEstabelecimento,DtoEstabelecimento estabelecimento)
		{
            DBCatalogDataContext dataContext = new DBCatalogDataContext();
			var estabelecimentoBanco = dataContext.tb_EnderecoEstabelecimentos.FirstOrDefault(u => 
                u.rua ==  enderecoEstabelecimento.rua && 
                u.numero ==  enderecoEstabelecimento.numero);

            var estab = dataContext.tb_Estabelecimentos.FirstOrDefault(u => u.estabelecimento == estabelecimento.nome);


            if (estabelecimentoBanco == null && estab == null)
            {
                estabelecimentoBanco = new Linq.tb_EnderecoEstabelecimento();
                estab = new Linq.tb_Estabelecimento();
                estab.estabelecimento = estabelecimento.nome;
                estabelecimentoBanco.rua =  enderecoEstabelecimento.rua;
                estabelecimentoBanco.cidade =  enderecoEstabelecimento.cidade;
                estabelecimentoBanco.estado =  enderecoEstabelecimento.estado;  
                estabelecimentoBanco.numero =  enderecoEstabelecimento.numero;
                estabelecimentoBanco.cep =  enderecoEstabelecimento.cep;
                estabelecimentoBanco.latitude =  enderecoEstabelecimento.latitude;
                estabelecimentoBanco.longitude =  enderecoEstabelecimento.longitude;

                dataContext.tb_EnderecoEstabelecimentos.InsertOnSubmit(estabelecimentoBanco);
                dataContext.tb_Estabelecimentos.InsertOnSubmit(estab);
                dataContext.SubmitChanges();
            }
            else
            {
				throw new DtoExcecao(DTO.Enum.CampoInvalido, "Estabelecimento ja existente");
            }
		}

    }
}