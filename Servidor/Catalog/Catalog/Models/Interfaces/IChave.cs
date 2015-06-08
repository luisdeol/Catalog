using Catalog.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Catalog.Models.Interfaces
{
	public interface IChave
	{
		DtoChave criarChave(int isUsuario);
		bool validarChave(DtoChave chave);
		DtoChave atualizarChave(DtoChave chave);
		void destruirChave(int idUsuario);
	}
}