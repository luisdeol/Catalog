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
	
	WebServices.cadastrarEstabelecimento = function(json)
	{
		return $http.post(host + '/WsEstabelecimento.asmx/criarEstabelecimento', {dtoEstabelecimento:json})
	}
	
	WebServices.getCep = function(cep)
	{
		return $http.get('http://api.postmon.com.br/v1/cep/' +cep);
	}
	
	WebServices.editarListas = function(chave,dtoLista)
	{
		return $http.post(host + '/WsLista.asmx/editarLista', {dtoChave:chave,dtoLista:dtoLista});
	}

	WebServices.deletarListas = function(chave,dtoLista)
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
	
	
    return WebServices;
}]);