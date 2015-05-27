using Catalog.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Catalog.Webservices
{
	public class WsUsuario
	{
		public string logar(string dtoUsuario)
		{
			ControllerUsuario cUsuario = new ControllerUsuario();
			return cUsuario.logar(dtoUsuario);
		}
	}
}