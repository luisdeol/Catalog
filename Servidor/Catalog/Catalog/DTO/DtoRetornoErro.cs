using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Catalog.DTO
{
	public class DtoRetornoErro : DtoRetorno
	{
		public string codigoErro { get; set; }
		public string mensagem { get; set; }

		public DtoRetornoErro(string mensagem, string codigoErro = "000", string destino = "this")
			: base("NAK", destino)
		{
			this.mensagem = mensagem;
			this.codigoErro = codigoErro;
		}

		public DtoRetornoErro(Enum tipo, string especificacao, string destino = "this")
			: base("NAK", destino)
		{
			codigoErro = tipo.ToString();
			switch(tipo)
			{
				case Enum.ErroCriacao:
					mensagem = "Não foi possível realizar o cadastro de '" + especificacao + "'";
					break;
				case Enum.ObjetoNaoEncontrado:
					mensagem = "Nenhum resultado foi encontrado";
					break;
				case Enum.CampoInvalido:
					mensagem = "Existem campos com valores inválidos:\n" + especificacao;
					break;
				case Enum.ChaveInvalida:
					mensagem = "É nescessário realizar login para utilizar o aplicativo!";
					break;
			}
		}
	}
}