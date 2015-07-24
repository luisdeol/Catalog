angular.module('services.WebServices', ['ionic'])

.factory('WebServices', ['$http', function ($http) {

	var WebServices = {};
	var host = 'http://localhost:51786/Webservices';

	WebServices.logar = function(json)
	{
		return $http.post(host + '/WsUsuario.asmx/logar', {dtoUsuario:json});
	}
	
	WebServices.cadastrar = function(json)
	{
		return $http.post(host + '/WsUsuario.asmx/realizarCadastro', {dtoUsuario:json});
	}
	
	WebServices.criarEstabelecimento = function(chave, dtoEstab)
	{
		return $http.post(host + '/WsEstabelecimento.asmx/criarEstabelecimento', {dtoChave:chave, dtoEnderecoEstabelecimento:dtoEstab})
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
	
	WebServices.editarListas = function(chave,dtoLista)
	{
		return $http.post(host + '/WsLista.asmx/editarLista', {dtoChave:chave,dtoLista:dtoLista});
	}

	WebServices.excluirLista = function(chave,dtoLista)
	{
		return $http.post(host + '/WsLista.asmx/excluirLista', {dtoChave:chave,dtoLista:dtoLista});
	}
	
	WebServices.pesquisarListas = function(chave)
	{
		return $http.post(host + '/WsLista.asmx/pesquisarLista', {dtoChave:chave,parametros:""});
	}
	
	WebServices.criarListas = function(chave,dtoLista)
	{
		return $http.post(host + '/WsLista.asmx/criarLista', {dtoChave:chave,dtoLista:dtoLista});
	}
	
	WebServices.abrirLista = function(chave,dtoLista)
	{
		return $http.post(host + '/WsLista.asmx/abrirLista', {dtoChave:chave,dtoLista:dtoLista});
	}
	
	WebServices.recuperarSenha = function(dtoUsuario)
	{
		return $http.post(host + '/WsUsuario.asmx/recuperarSenha', {dtoUsuario:dtoUsuario});
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