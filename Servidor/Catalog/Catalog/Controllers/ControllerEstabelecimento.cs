using Catalog.DTO;
using Catalog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Catalog.Controllers
{
    public class ControllerEstabelecimento
    {
        public void criarEstabelecimento(string DtoEstabelecimento)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            DtoEnderecoEstabelecimento enderecoEstabelecimento = js.Deserialize<DtoEnderecoEstabelecimento>(DtoEstabelecimento);
            DtoEstabelecimento estabelecimento = js.Deserialize<DtoEstabelecimento>(DtoEstabelecimento);
            Estabelecimento mEstabelecimento = new Estabelecimento();
            mEstabelecimento.criarEstabelecimento(enderecoEstabelecimento,estabelecimento);
        }
    }
}