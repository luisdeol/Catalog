using Catalog.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Catalog.Models.Interfaces
{
	public class IChave
	{
		public DtoChave criarChave(int isUsuario);
		public bool validarChave(DtoChave chave);
		public DtoChave atualizarChave(DtoChave chave);
		public void destruirChave(int idUsuario);
	}
}