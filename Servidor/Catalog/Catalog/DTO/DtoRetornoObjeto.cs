using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Catalog.DTO
{
	public class DtoRetornoObjeto : DtoRetorno
	{
		public Object objeto { get; set; }
		public DtoChave chave { get; set; }

		public DtoRetornoObjeto(DtoChave chave, string destino = "this")
			: base("ACK", destino)
		{
			this.chave = chave;
			this.objeto = null;
		}

		public DtoRetornoObjeto(DtoChave chave, Object objeto, string destino = "this")
			: base("ACK", destino)
		{
			this.chave = chave;
			this.objeto = objeto;
		}
	}
}