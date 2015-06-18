angular.module("EstabelecimentoControllers",[
'ionic',
'services.verificarLogin',
'services.googleMaps',
'model.estabelecimento'
])
.config(function($stateProvider, $urlRouterProvider) {
	
    $stateProvider
		.state('estabelecimento', {
			url: '/estabelecimentos',
			templateUrl: 'estabelecimentos.html',
			controller: 'EstabelecimentoController'
		})
})
.controller("EstabelecimentoController",function($scope,$http,$ionicModal,$ionicLoading,$compile,verificarLogin,googleMaps,estabelecimento){
	$scope.estabelecimentos = [];
	
	//___________ VERIFICAR LOGIN _____________//
	$scope.verificarLogin = function(lugarPagina)
	{
		verificarLogin.verificarEstabelecimento(lugarPagina);
		estabelecimento.openDataBase();
	}
	
	//_______________ ABRIR MODAL DE CADASTRO __________________//
	 $ionicModal.fromTemplateUrl('templates/modal.html', {
		scope: $scope
	  }).then(function(modal) {
		$scope.modal = modal;
	  });

	//_______________ CHAMAR MAPA _________________// 
	$scope.chamarMapa = function(latitudeEstabelecimento,longitudeEstabelecimento,nomeEstabelecimento)
	{
		window.localStorage.latitudeEstabelecimento = latitudeEstabelecimento;
		window.localStorage.longitudeEstabelecimento = longitudeEstabelecimento;
		window.localStorage.nomeEstabelecimento = nomeEstabelecimento;
		window.location = "googleMaps.html";
	}	
	
	//_______________ PEGAR ENDEREÇO POR CEP __________________//
	$scope.getEnderecoCep = function(cep)
	{
		if(cep.length == 9)
		{
			cep.split().splice(5,1);
			$http.get('http://api.postmon.com.br/v1/cep/' +cep).
			  success(function(data, status, headers, config)
			{
				var retorno = angular.fromJson(data);	
				document.getElementById("cidade").value = retorno.cidade;
				document.getElementById("estado").value = retorno.estado_info.nome;
				if(retorno.logradouro != undefined)
				document.getElementById("logradouro").value = retorno.logradouro;
			});			
		}
		else if(cep.length == 5)
		{
			document.getElementById("cep").value = cep +"-";
		}
	}

	//_______________ CADASTRAR ESTABELECIMENTO _________________// 
	$scope.cadastrarEstabelecimento = function()
	{
		var estab = new Object();
		estab.nome = document.getElementById("nome").value;
		estab.rua = document.getElementById("logradouro").value;
		estab.cidade = document.getElementById("cidade").value;
		estab.estado = document.getElementById("estado").value;
		estab.numero = document.getElementById("numero").value;
		estab.cep = document.getElementById("cep").value;
		
		$scope.estabelecimentos.push
		({ 
			nome: estab.nome, 
			rua: estab.rua, 
			cidade: estab.cidade, 
			estado: estab.estado, 
			numero: estab.numero, 
			cep: estab.cep 
		});
		
		estabelecimento.insertInto(1,estab.nome, estab.rua, estab.cidade, estab.estado, estab.numero, estab.cep);
		googleMaps.pegarLatitudeLongitude(estab.nome +" - "+ estab.rua +" - "+ estab.cidade +" - "+ estab.estado,function(){
			
			var json = "{nome: '"+estab.nome+"', rua: '"+estab.rua+"', cidade: '"+estab.cidade+"', estado: '"+estab.estado+"', numero: '"+estab.numero+"', cep: '"+estab.cep+"',latitude: '"+window.localStorage.latCadastroEstab+"',longitude: '"+window.localStorage.lonCadastroEstab+"'}";
			
			$http.post('http://localhost:51786/Webservices/WsEstabelecimento.asmx/criarEstabelecimento', {dtoEstabelecimento:json}).
			  success(function(data, status, headers, config)
			{
				var retorno = data.d;	
			});
			$scope.modal.hide();
		});
	}	
});