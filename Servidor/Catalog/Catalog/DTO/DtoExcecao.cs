using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Catalog.DTO
{
	public class DtoExcecao : Exception
	{
		private string mensagem;
		private string codigoErro;
		public string destino { get; set; }

		public DtoExcecao(Enum tipo, string especificacao = "")
		{
			codigoErro = tipo.ToString();
			switch (tipo)
			{
				case Enum.ErroCriacao:
					mensagem = "Não foi possível realizar o cadastro de '" + especificacao + "'";
					break;
				case Enum.ObjetoNaoEncontrado:
					mensagem = "Nenhum resultado foi encontrado para " + especificacao;
					break;
				case Enum.CampoInvalido:
					mensagem = "Existem campos com valores inválidos:\n" + especificacao;
					break;
				case Enum.ChaveInvalida:
					mensagem = "É nescessário realizar login para utilizar o aplicativo!";
					break;
				case Enum.CriteriosDeBuscaInsuficientes:
					mensagem = "É nescessário que pelo menos um dos campos de busca contenha pelo menos 4 caracteres";
					break;
			}
		}

		public DtoRetornoErro ToDto()
		{
			DtoRetornoErro dto = new DtoRetornoErro(mensagem, codigoErro, destino);
			return dto;
		}
	}
}