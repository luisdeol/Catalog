using Catalog.DTO;
using Catalog.Linq;
using Catalog.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Catalog.Models
{
	public class Chave : IChave
	{
		public DtoChave criarChave(int idUsuario)
		{
            Random rnd = new Random();
            DtoChave DtoChave = new DtoChave();
            DBCatalogDataContext dataContext = new DBCatalogDataContext();
            //var chave = (from c in dataContext.tb_Chaves where c.idUsuario == idUsuario select c).FirstOrDefault();
			var chave = dataContext.tb_Chaves.FirstOrDefault(c => c.idUsuario == idUsuario);

			if(chave != null)
			{
				chave.token = rnd.Next(1000, 9999).ToString();
				chave.ultimoAcesso = new TimeSpan();
				dataContext.SubmitChanges();
			}
			else
			{
				chave = new Linq.tb_Chave();
				chave.idUsuario = idUsuario;
				chave.token = rnd.Next(1000, 9999).ToString();
				chave.ultimoAcesso = new TimeSpan();
				dataContext.tb_Chaves.InsertOnSubmit(chave);

			}
			dataContext.SubmitChanges();

			DtoChave.idUsuario = chave.idUsuario;
			DtoChave.token = chave.token;
			DtoChave.ultimoAcesso = chave.ultimoAcesso.ToString();

            return DtoChave;
		}

		public bool validarChave(DtoChave chave)
		{
			DBCatalogDataContext dataContext = new DBCatalogDataContext();
			try
			{
				dataContext.tb_Chaves.First(c => c.idUsuario == chave.idUsuario && c.token == chave.token && c.ultimoAcesso.Equals(chave.ultimoAcesso));
			}
			catch
			{
				throw new DtoExcecao(DTO.Enum.ChaveInvalida);
			}
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