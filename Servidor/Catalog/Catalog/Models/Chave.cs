using Catalog.DTO;
using Catalog.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Catalog.Models
{
	public class Chave : IChave
	{
		public DtoChave criarChave(int isUsuario)
		{
			return new DtoChave();
		}

		public bool validarChave(DtoChave chave)
		{
			return true;
		}

		public DtoChave atualizarChave(DtoChave chave)
		{
			return new DtoChave();
		}

		public void destruirChave(int idUsuario)
		{

		}
	}
}