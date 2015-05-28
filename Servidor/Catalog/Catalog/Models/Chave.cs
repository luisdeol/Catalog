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
		public DtoChave criarChave(int idUsuario)
		{
            Random rnd = new Random();
            DtoChave DtoChave = new DtoChave();
            Linq.DBCatalogDataContext dataContext = new Linq.DBCatalogDataContext();
            //var chave = (from c in dataContext.tb_Chaves where c.idUsuario == idUsuario select c).FirstOrDefault();
			var chave = dataContext.tb_Chaves.FirstOrDefault(c => c.idUsuario == idUsuario);

			if(chave != null)
			{
				chave.token = rnd.Next(1000, 9999).ToString();
				chave.ultimoAcesso = new TimeSpan();
			}
			else
			{
				Linq.tb_Chave novaChave = new Linq.tb_Chave();
				novaChave.idUsuario = idUsuario;
				novaChave.token = rnd.Next(1000, 9999).ToString();
				novaChave.ultimoAcesso = new TimeSpan();
				dataContext.tb_Chaves.InsertOnSubmit(novaChave);
			}
			dataContext.SubmitChanges();
            return DtoChave;
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