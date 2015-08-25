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
		.state('abrirEstabelecimento', {
			url: '/produtos-estabelecimento',
			templateUrl: 'produtos-estabelecimento.html',
			controller: 'AbrirEstabelecimentoController'
		})
})
.controller("EstabelecimentoController",function($scope,$http,$ionicModal,$ionicLoading,$compile,verificarLogin,googleMaps,estabelecimento,modalAlerta,WebServices){
	$scope.estabelecimentos = [];
	var chave = "{idUsuario:'"+window.localStorage.idUsuario+"',token:'"+window.localStorage.token+"',ultimoAcesso:'"+window.localStorage.ultimoAcesso+"'}";
	window.localStorage.idUltimoEstabelecimentoCriado = 0;
	
	//_________________ ABRIR ESTABELECIMENTO _________________//
	$scope.abrirEstabelecimento = function(idEstab, nomeEstab, latitude, longitude)
	{
		location.href="#/produtos-estabelecimento?"+idEstab+"";
		window.localStorage.estabAberto = nomeEstab;
		window.localStorage.latitudeEstabAberto = latitude;
		window.localStorage.longitudeEstabAberto = longitude;
	}
	
	//___________ VERIFICAR LOGIN _____________//
	$scope.verificarLogin = function(lugarPagina)
	{
		verificarLogin.verificarEstabelecimento(lugarPagina);
		$scope.pesquisarEstabelecimento();
		estabelecimento.openDataBase();
	}
	
	//_______________ ABRIR MODAL DE CADASTRO __________________//
	 $ionicModal.fromTemplateUrl('templates/adicionarEstabelecimento.html', {
		scope: $scope
	  }).then(function(estabelecimento) {
		$scope.estabelecimentoModal = estabelecimento;
	  });

	//_______________ CHAMAR MAPA _________________// 
	$scope.chamarMapa = function(latitudeEstabelecimento,longitudeEstabelecimento,nomeEstabelecimento)
	{
		if(latitudeEstabelecimento!=0 && longitudeEstabelecimento!=0)
		{
			window.localStorage.latitudeEstabelecimento = latitudeEstabelecimento;
			window.localStorage.longitudeEstabelecimento = longitudeEstabelecimento;
			window.localStorage.nomeEstabelecimento = nomeEstabelecimento;
			window.location = "googleMaps.html";			
		}
		else
		{
			modalAlerta.alerta('Sem Localização!','Estabelecimento não possui localização');
		}
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
	
	//_______________ SALVAR IMAGEM __________________//
	$scope.salvarImagem = function()
	{
		var oFReader = new FileReader(); 
		try 
		{
			oFReader.readAsDataURL(document.getElementById("file").files[0]);
			oFReader.onload = function (oFREvent) 
			{ 
				window.localStorage.estabImagem = oFREvent.target.result; 
			}; 		
		}
		catch(err) 
		{
			window.localStorage.estabImagem = "";
		}
	}
	
	//________________ PESQUISAR ESTABELECIMENTO _____________//
	$scope.pesquisarEstabelecimento = function()
	{	
		WebServices.pesquisarEstabelecimento(chave, "")
		.success(function(data, status, headers, config)
		{
			var retorno = angular.fromJson(data.d);	
			if(retorno.tipoRetorno == "ACK")
			{
				for(var l=0; retorno.objeto.length > l; l++)
				{
					var id = retorno.objeto[l].id;
					var nome = retorno.objeto[l].estabelecimento.nome;
					var rua = retorno.objeto[l].rua;
					var cidade = retorno.objeto[l].cidade;
					var estado = retorno.objeto[l].estado;
					var numero = retorno.objeto[l].numero;
					var cep = retorno.objeto[l].cep;
					var latitude = retorno.objeto[l].latitude;
					var longitude = retorno.objeto[l].longitude; 
					var imagem = "";
					
					$scope.estabelecimentos[l] = {id:id, nome:nome, rua:rua, cidade:cidade, estado:estado, numero:numero, cep:cep, latitude:latitude, longitude:longitude, imagem:imagem};
				}
			}
			else
			{
				modalAlerta.alerta("Ocorreu um erro",retorno.mensagem);
			}
		})
		.error(function(data, status, headers, config) {
			modalAlerta.alerta("Ocorreu um erro","Voce esta sem acesso a rede!");
		});
	} 

	//_______________ CADASTRAR ESTABELECIMENTO _________________// 
	$scope.cadastrarEstabelecimento = function()
	{
		$scope.salvarImagem();
		var estab = new Object();
		estab.nome = document.getElementById("nome").value;
		estab.rua = document.getElementById("logradouro").value;
		estab.cidade = document.getElementById("cidade").value;
		estab.estado = document.getElementById("estado").value;
		estab.numero = document.getElementById("numero").value;
		estab.cep = document.getElementById("cep").value;
		
		if( estab.nome.length>=3   && 
		    estab.rua.length>=3    && 
		    estab.cidade.length>=3 && 
		    estab.estado.length>=2 && 
		    estab.numero!=""       &&
		    estab.numero>0)
		{
			
			googleMaps.pegarLatitudeLongitude(estab.nome +" - "+ estab.rua +" - "+ estab.cidade +" - "+ estab.estado,function(){
				
				var dtoEstab = "{estabelecimento:{nome:'"+estab.nome+"'}, rua: '"+estab.rua+"', cidade: '"+estab.cidade+"', estado: '"+estab.estado+"', numero: '"+estab.numero+"', cep: '"+estab.cep+"',latitude: '"+window.localStorage.latCadastroEstab+"',longitude: '"+window.localStorage.lonCadastroEstab+"'}";
				
				// var id = window.localStorage.idUltimoEstabelecimentoCriado-1;
				// estabelecimento.insertInto(id, estab.nome, estab.rua, estab.cidade, estab.estado, estab.numero, estab.cep, window.localStorage.latCadastroEstab, window.localStorage.lonCadastroEstab, window.localStorage.estabImagem);
				// window.localStorage.idUltimoEstabelecimentoCriado--;
				
				WebServices.criarEstabelecimento(chave, dtoEstab)
				.success(function(data, status, headers, config)
				{
					var retorno = angular.fromJson(data.d);	
					if(retorno.tipoRetorno == "ACK")
					{
						modalAlerta.alerta("Estabelecimento","Estabelecimento cadastrado com sucesso!");
						$scope.pesquisarEstabelecimento();
					}
					else //erro
					{
						modalAlerta.alerta("Ocorreu um erro",retorno.mensagem);
					}
				})
				.error(function(data, status, headers, config) {
					modalAlerta.alerta("Ocorreu um erro","Voce esta sem acesso a rede!");
				});
				
				$scope.estabelecimentoModal.hide();
			});		
		}
		else
		{
			modalAlerta.alerta('ERRO!','Existem campos inválidos!');
		}
	}	
})

.controller("AbrirEstabelecimentoController",function($scope,$http,$ionicModal,$ionicLoading,$compile,verificarLogin,googleMaps,estabelecimento,modalAlerta,WebServices){
	
	var chave = "{idUsuario:'"+window.localStorage.idUsuario+"',token:'"+window.localStorage.token+"',ultimoAcesso:'"+window.localStorage.ultimoAcesso+"'}";
	$scope.produtos = [];
	$scope.nome = window.localStorage.estabAberto;
	
	var url = window.location.href.toString();
	$scope.idEstabelecimento = url.split("?")[1];
	
	//________________ PESQUISAR PRODUTO ESTABELECIMENTO _____________//
	$scope.pesquisarProdutosEstabelecimento = function()
	{	
		var dtoEstab = "{id:'"+ $scope.idEstabelecimento +"'}";
		var dtoProd = "{nome:'',codigoDeBarras:'',tipoCodigoDeBarras:null,fabricante:{fabricante:''},tipo:{tipo:''}}";
	
		WebServices.pesquisarProdutosEstabelecimento(chave,dtoEstab,dtoProd)
		.success(function(data, status, headers, config)
		{
			var retorno = angular.fromJson(data.d);	
			if(retorno.tipoRetorno == "ACK")
			{
				for(var l=0; retorno.objeto.length > l; l++)
				{
					var id = retorno.objeto[l].id;
					var nomeProduto = retorno.objeto[l].produto.nome;
					var preco = retorno.objeto[l].preco;
					$scope.latitude = retorno.objeto[l].estabelecimento.latitude;
					$scope.longitude = retorno.objeto[l].estabelecimento.longitude;
					$scope.produtos[l] = {id:id, nome:$scope.nome, nomeProduto:nomeProduto, preco:preco};
				}
			}
			else //erro
			{
				modalAlerta.alerta("Ocorreu um erro",retorno.mensagem);
			}
			
			$scope.mapaEstabelecimento(window.localStorage.latitudeEstabAberto,window.localStorage.longitudeEstabAberto);	
		})
		.error(function(data, status, headers, config) {
			modalAlerta.alerta("Ocorreu um erro","Voce esta sem acesso a rede!");
		});
	} 
	
	//_____________ MAPA ESTABELECIMENTO ___________//
	$scope.mapaEstabelecimento = function(latitude, longitude) 
	{
		if(latitude != undefined && longitude != undefined)
		{
			var mapProp = {
				center:new google.maps.LatLng(latitude,longitude),
				zoom:17,
				mapTypeId:google.maps.MapTypeId.ROADMAP
			};
			var map=new google.maps.Map(document.getElementById("googleMap"),mapProp);

			var myLatlng = new google.maps.LatLng(latitude,longitude);
			var marker = new google.maps.Marker({
				position: myLatlng,
				map: map,
			});
		}
		else
		{
			document.getElementById("imgSemLocalizacao").src = "img/semLocalizacao.png";
		}
	}
	
	$ionicModal.fromTemplateUrl('templates/checkin-estabelecimento.html', {
		scope: $scope
	}).then(function(checkin) {
		$scope.modalCheckin = checkin;
	});
	
	//__________ INICIAR CHECKIN ___________//
	$scope.iniciarCheckin = function(checkin)
	{	
		window.localStorage.flagCheckin = "estabelecimento";
		window.localStorage.idEstabCheckin = $scope.idEstabelecimento;		
		modalAlerta.sucesso("CheckIn","Efetuando...","#/checkin?"+checkin.lista+"");
		$scope.modalCheckin.hide();
	}
	
		//__________ ESCOLHER ESTABELECIMENTO E LISTA _______________//
	$scope.escolherEstabLista = function()
	{	
		$scope.listas = []

		WebServices.pesquisarListas(chave)
	    .success(function(data, status, headers, config)
		{
			var retorno = angular.fromJson(data.d);	
			if(retorno.tipoRetorno == "ACK") //logado
			{
				for(var l=0; retorno.objeto.length > l; l++)
				{
					$scope.idLista = retorno.objeto[l].id;
					var idUsuario = retorno.objeto[l].idUsuario;
					var titulo = retorno.objeto[l].titulo;
					
					$scope.listas[l] = {id:$scope.idLista,idUsuario:idUsuario,titulo:titulo,indice:l};
				}
			}
			else
			{
				modalAlerta.alerta("Ocorreu um erro",retorno.mensagem);
			}
		});
		
	}
	
})
