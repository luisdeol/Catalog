angular.module("EstabelecimentoControllers",[
'ionic',
'services.verificarLogin',
'services.googleMaps',
'model.estabelecimento',
'services.modalAlerta',
'services.WebServices'
])
.config(function($stateProvider, $urlRouterProvider) {
	
    $stateProvider
		.state('estabelecimento', {
			url: '/estabelecimentos',
			templateUrl: 'estabelecimentos.html',
			controller: 'EstabelecimentoController'
		})
})
.controller("EstabelecimentoController",function($scope,$http,$ionicModal,$ionicLoading,$compile,verificarLogin,googleMaps,estabelecimento,modalAlerta,WebServices){
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
			WebServices.getCep(cep)
			.success(function(data, status, headers, config)
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
		else if(!isNaN(cep.replace(/-/g, "")) == false) //se cep nao for um numero
		{
			modalAlerta.alerta('CEP inválido!','CEP deve conter apenas números');
			document.getElementById("cep").value = "";
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
		
		if(estab.nome.length>=3   && 
		   estab.rua.length>=3    && 
		   estab.cidade.length>=3 && 
		   estab.estado.length>=2 && 
		   estab.numero!="")
		{
			$scope.estabelecimentos.push
			({ 
				nome: estab.nome, 
				rua: estab.rua, 
				cidade: estab.cidade, 
				estado: estab.estado, 
				numero: estab.numero, 
				cep: estab.cep 
			});
			
			googleMaps.pegarLatitudeLongitude(estab.nome +" - "+ estab.rua +" - "+ estab.cidade +" - "+ estab.estado,function(){
				
				var json = "{nome: '"+estab.nome+"', rua: '"+estab.rua+"', cidade: '"+estab.cidade+"', estado: '"+estab.estado+"', numero: '"+estab.numero+"', cep: '"+estab.cep+"',latitude: '"+window.localStorage.latCadastroEstab+"',longitude: '"+window.localStorage.lonCadastroEstab+"'}";
				
				WebServices.cadastrarEstabelecimento(json)
				.success(function(data, status, headers, config)
				{
					var retorno = data.d;	
				});
				$scope.modal.hide();
			});		
		}
		else
		{
			modalAlerta.alerta('ERRO!','Existem campos inválidos!');
		}
	}	
});