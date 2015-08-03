angular.module('services.WebServices', ['ionic'])

.factory('WebServices', ['$http', function ($http) {

	var WebServices = {};
	var host = 'http://192.168.1.97/Webservices/';
	
	/*-- Serviços de Usuario--*/

	WebServices.logar = function(json)
	{
		return $http.post(host + '/WsUsuario.asmx/logar', {dtoUsuario:json});
	}
	
	WebServices.cadastrar = function(json)
	{
		return $http.post(host + '/WsUsuario.asmx/realizarCadastro', {dtoUsuario:json});
	}
	
	WebServices.alterarDadosCadastrais = function(senha,novaSenha)
	{
		return $http.post(host + '/WsUsuario.asmx/alterarDadosCadastrais', {senha:senha, novaSenha:novaSenha});
	}
	
	WebServices.recuperarSenha = function(dtoUsuario)
	{
		return $http.post(host + '/WsUsuario.asmx/recuperarSenha', {dtoUsuario:dtoUsuario});
	}
	/*-- Serviços de Estabelecimento --*/
	
	WebServices.criarEstabelecimento = function(chave, dtoEstab)
	{
		return $http.post(host + '/WsEstabelecimento.asmx/criarEstabelecimento', {dtoChave:chave, dtoEnderecoEstabelecimento:dtoEstab})
	}
	
	WebServices.finalizarCheckin = function(chave, dtoEstab, dtoItens)
	{
		return $http.post(host + '/WsEstabelecimento.asmx/finalizarCheckin', {dtoChave:chave, dtoEnderecoEstabelecimento:dtoEstab,dtoItensComprados:dtoItens})
	}
	
	WebServices.pesquisarEstabelecimento = function(chave, dtoEstab)
	{
		return $http.post(host + '/WsEstabelecimento.asmx/pesquisarEstabelecimentos', {dtoChave:chave, dtoEnderecoEstabelecimento:dtoEstab})
	}
	
	WebServices.pesquisarProdutosEstabelecimento = function(chave, dtoEstab, parametros)
	{
		return $http.post(host + '/WsEstabelecimento.asmx/pesquisarProdutos', {dtoChave:chave, dtoEnderecoEstabelecimento:dtoEstab, parametros:parametros})
	}
	
	WebServices.getCep = function(cep)
	{
		return $http.get('http://api.postmon.com.br/v1/cep/' +cep);
	}
	
	/*-- Serviços de Lista --*/
	
	WebServices.editarListas = function(chave,dtoLista)
	{
		return $http.post(host + '/WsLista.asmx/editarLista', {dtoChave:chave,dtoLista:dtoLista});
	}

	WebServices.excluirLista = function(chave,dtoLista)
	{
		return $http.post(host + '/WsLista.asmx/excluirLista', {dtoChave:chave,dtoLista:dtoLista});
	}
	
	WebServices.removerProduto = function(chave,dtoLista,dtoProduto)
	{
		return $http.post(host + '/WsLista.asmx/removerProduto', {dtoChave:chave,dtoLista:dtoLista,dtoProduto:dtoProduto});
	}
	
	WebServices.pesquisarListas = function(chave)
	{
		return $http.post(host + '/WsLista.asmx/pesquisarLista', {dtoChave:chave,parametros:""});
	}
	
	WebServices.listarItensEm = function(chave,dtoLista,dtoEnderecoEstabelecimento)
	{
		return $http.post(host + '/WsLista.asmx/listarItensEm', {dtoChave:chave,dtoLista:dtoLista,dtoEnderecoEstabelecimento:dtoEnderecoEstabelecimento});
	}
	
	WebServices.criarListas = function(chave,dtoLista)
	{
		return $http.post(host + '/WsLista.asmx/criarLista', {dtoChave:chave,dtoLista:dtoLista});
	}
	
	WebServices.adicionarProdutoALista = function(chave,dtoLista,dtoProdutoDaLista)
	{
		return $http.post(host + '/WsLista.asmx/adicionarProduto', {dtoChave:chave,dtoLista:dtoLista,dtoProdutoDaLista:dtoProdutoDaLista});
	}
	
	
	WebServices.abrirLista = function(chave,dtoLista)
	{
		return $http.post(host + '/WsLista.asmx/abrirLista', {dtoChave:chave,dtoLista:dtoLista});
	}
	
	/*-- Serviços de Lista --*/
	
	WebServices.criarProduto = function(chave,dtoProduto)
	{
		return $http.post(host + '/WsProduto.asmx/criarProduto', {dtoChave:chave,dtoProduto:dtoProduto});
	}
	
	WebServices.pesquisarProduto = function(chave,dtoProduto)
	{
		return $http.post(host + '/WsProduto.asmx/pesquisarProduto', {dtoChave:chave,parametros:dtoProduto});
	}
	
	WebServices.abrirProduto = function(chave,dtoProduto)
	{
		return $http.post(host + '/WsProduto.asmx/abrirProduto', {dtoChave:chave,dtoProduto:dtoProduto});
	}
	
	WebServices.listarEstabelecimentosProssuidores = function(chave,dtoProduto)
	{
		return $http.post(host + '/WsProduto.asmx/listarEstabelecimentosProssuidores', {dtoChave:chave,dtoProduto:dtoProduto});
	}
	
	
    return WebServices;
}]);