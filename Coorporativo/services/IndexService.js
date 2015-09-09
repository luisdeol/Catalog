angular.module('services.WebServices', [])

.factory('WebServices', ['$http', function ($http) {

	var WebServices = {};
	var host = 'http://localhost:51786/Webservices/';
	
	/*-- Serviços--*/

	WebServices.pesquisarEstabelecimentos = function(){
		return $http.post(host + '/WsEstabelecimento.asmx/pesquisarEstabelecimentos', {dtoEnderecoEstabelecimento:""});
	}
	
	WebServices.abrirLista = function(){
		return $http.post(host + '/WsLista.asmx/abrirLista', {dtoLista:"{id:'1'}"});
	}
	
	WebServices.listarItensEm = function(dtoLista,dtoEnderecoEstabelecimento){
		return $http.post(host + '/WsLista.asmx/listarItensEm', {dtoLista:dtoLista, dtoEnderecoEstabelecimento:dtoEnderecoEstabelecimento});
	}
	
    return WebServices;
}]);
