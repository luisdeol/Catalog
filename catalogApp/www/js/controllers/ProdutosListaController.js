angular.module("ProdutosListaControllers",[
'ionic',
'services.modalAlerta',
'services.WebServices'
])
.config(function($stateProvider, $urlRouterProvider) {
	
    $stateProvider
		.state('produto', {
			url: '/produtos-pesquisados',
			templateUrl: 'produtos-pesquisados.html',
			controller: 'ProdutosController'
		})
		.state('abrirProduto', {
			url: '/abrirProduto',
			templateUrl: 'abrirProduto.html',
			controller: 'ProdutosController'
		})
})
.controller("ProdutosController",function($scope,$http,$ionicModal,$ionicLoading,$compile, modalAlerta,WebServices)
{
	$scope.nomeProduto = "Banana"
	$scope.marcaProduto = "nestle sadia"
	
	//___________ VERIFICAR LOGIN _____________//
	$scope.pesquisarProduto = function()
	{
		$scope.produtos = [
		{id:1,nome:"banana"},
		{id:2,nome:"Melancia"},
		{id:3,nome:"Rapadura"},
		{id:4,nome:"Tapioca"}
		];
	}	
});