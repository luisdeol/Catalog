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
            var chave = from c in dataContext.tb_Chaves where c.idUsuario == idUsuario select c;

            if(chave.Count() == 0)
            {
                
               
            }
            else
            {
                foreach (var chav in chave)
                {
                    chav.token = rnd.Next(1000, 9999).ToString();
                    chav.ultimoAcesso = new TimeSpan();
                    dataContext.tb_Chaves.InsertOnSubmit(chav);
                    dataContext.SubmitChanges();
                }
            }
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