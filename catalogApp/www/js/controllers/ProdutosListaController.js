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
	var chave = "{idUsuario:'"+window.localStorage.idUsuario+"',token:'"+window.localStorage.token+"',ultimoAcesso:'"+window.localStorage.ultimoAcesso+"'}";
	$scope.produtos = [];
	$scope.estabelecimentos = [];
	
	//___________ PESQUISAR PRODUTO _____________//
	$scope.pesquisarProduto = function()
	{
		var dtoProduto = window.localStorage.dtoProduto;
		
		WebServices.pesquisarProduto(chave,dtoProduto)
			.success(function(data, status, headers, config)
			{
				var retorno = angular.fromJson(data.d);	
				if(retorno.tipoRetorno == "ACK") //logado
				{
					for(var l=0; retorno.objeto.length > l; l++)
					{
						var idProduto = retorno.objeto[l].id;
						var nome = retorno.objeto[l].nome;						
						$scope.produtos[l] = {idProduto:idProduto, nome:nome};
					}	
				}
				else //erro
				{
					modalAlerta.alerta("Ocorreu um erro",retorno.mensagem);
				}
			})
			.error(function(data, status, headers, config) {
				modalAlerta.alerta("Ocorreu um erro","Voce esta sem acesso a rede!");
			});
	}	
	
	//___________ ABRIR PRODUTO _____________//
	$scope.abrirProduto = function()
	{
		var url = window.location.href.toString();
		$scope.idProduto = url.split("?")[1];
		
		var dtoProduto = "{id:'"+$scope.idProduto+"', nome:'',codigoDeBarras:'',tipoCodigoDeBarras:'',fabricante:{fabricante:''},tipo:{tipo:''}}";
		
		WebServices.abrirProduto(chave,dtoProduto)
			.success(function(data, status, headers, config)
			{
				var retorno = angular.fromJson(data.d);	
				if(retorno.tipoRetorno == "ACK") //logado
				{
					$scope.nomeProdutoAberto = retorno.objeto.nome;
					$scope.tipoProdutoAberto = retorno.objeto.tipo.tipo;
					$scope.fabricanteProdutoAberto = retorno.objeto.fabricante.fabricante;
					
				}
				else //erro
				{
					modalAlerta.alerta("Ocorreu um erro",retorno.mensagem);
				}
			})
			.error(function(data, status, headers, config) {
				modalAlerta.alerta("Ocorreu um erro","Voce esta sem acesso a rede!");
			});
	}	
	
	//___________ LISTAR ESTABELECIMENTOS POSSUIDORES _____________//
	$scope.listarEstabelecimentosProssuidores = function()
	{
		var url = window.location.href.toString();
		$scope.idProduto = url.split("?")[1];
		
		var dtoProduto = "{id:'"+$scope.idProduto+"', nome:'',codigoDeBarras:'',tipoCodigoDeBarras:'',fabricante:{fabricante:''},tipo:{tipo:''}}";
		
		WebServices.listarEstabelecimentosProssuidores(chave,dtoProduto)
			.success(function(data, status, headers, config)
			{
				var retorno = angular.fromJson(data.d);	
				if(retorno.tipoRetorno == "ACK") //logado
				{
					for(var l=0; retorno.objeto.length > l; l++)
					{
						var idEstabelecimento = retorno.objeto[l].idEstabelecimento;
						var nome = retorno.objeto[l].estabelecimento.estabelecimento.nome;
						var preco = retorno.objeto[l].preco;						
						$scope.estabelecimentos[l] = {idEstabelecimento:idEstabelecimento, nome:nome,preco:preco};
					}	
				}
				else //erro
				{
					modalAlerta.alerta("Ocorreu um erro",retorno.mensagem);
				}
			})
			.error(function(data, status, headers, config) {
				modalAlerta.alerta("Ocorreu um erro","Voce esta sem acesso a rede!");
			});
	}	
	
	
});