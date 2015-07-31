var app = angular.module("PrincipalControllers",[
'ionic',
'services.verificarLogin',
'services.WebServices',
'services.modalAlerta'
])
.controller("PrincipalController",function($scope,$ionicModal,$ionicPopup,$timeout,verificarLogin, WebServices, modalAlerta)
{
	var chave = "{idUsuario:'"+window.localStorage.idUsuario+"',token:'"+window.localStorage.token+"',ultimoAcesso:'"+window.localStorage.ultimoAcesso+"'}";
	
	//___________ VERIFICAR LOGIN _____________//
	$scope.verificarLogin = function(lugarPagina)
	{
		localizacao();
		verificarLogin.verificarPrincipal(lugarPagina);
	};
	
	//______________ LOGOUT _____________//
	$scope.logout = function()
	{
		window.localStorage.idUsuario = "";
		window.localStorage.token = "";
		window.localStorage.ultimoAcesso = "";
		window.location = "#/home";
	};
	
	//____________ ALERTA ____________//
	$scope.alerta = function(mensagem,subMensagem,destino)
	{
		var alertPopup = $ionicPopup.alert({
		title: mensagem,
		template: subMensagem
		});
		
		 $timeout(function() 
		{
		  window.location = destino;
		  alertPopup.close();
		}, 3000);
	};
	
	//_______________ ABRIR MODAL DE CADASTRO __________________//
	$ionicModal.fromTemplateUrl('templates/alterarDados.html', {
		scope: $scope
	}).then(function(modal) {
		$scope.modal = modal;
	});
	
	//_______________ CHECKIN __________________//
	$ionicModal.fromTemplateUrl('templates/checkin-principal.html', {
		scope: $scope
	}).then(function(checkin) {
		$scope.modalCheckin = checkin;
	});
	
	//__________ INICIAR CHECKIN ___________//
	$scope.iniciarCheckin = function(checkin)
	{	
		window.localStorage.flagCheckin = "principal";
		window.localStorage.idEstabCheckin = checkin.estabelecimento;		
		modalAlerta.sucesso("CheckIn","Efetuando...","#/checkin?"+checkin.lista+"");
		$scope.modalCheckin.hide();
	}
	
	//__________ ESCOLHER ESTABELECIMENTO E LISTA _______________//
	$scope.escolherEstabLista = function()
	{	
	
		$scope.estabelecimentos = [];
		$scope.listas = []
		
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
	
	//_______________ PESQUISAR PRODUTO __________________//
	$ionicModal.fromTemplateUrl('templates/pesquisarProduto.html', {
		scope: $scope
	}).then(function(modal) {
		$scope.modalPesquisar = modal;
	});
	
	$scope.pesquisarProduto = function(produto)
	{
		if(produto != undefined)//campos foram preenchidos
		{
			var nome = produto.nome;
			var fabricante = produto.fabricante;
			var tipo = produto.tipo;
			
			if(nome==undefined)
			{
				nome="";
			}
			if(fabricante==undefined)
			{
				fabricante="";
			}
			if(tipo==undefined)
			{
				tipo="";
			}
			
			var fabricante = "{fabricante:'"+fabricante+"'}";
			var tipo = "{tipo:'"+tipo+"'}";
			window.localStorage.dtoProduto = "{nome:'"+nome+"',codigoDeBarras:'',tipoCodigoDeBarras:null,fabricante:"+fabricante+",tipo:"+tipo+"}";
					
			modalAlerta.sucesso("Pesquisa","Pesquisando...","#/produtos-pesquisados");
			$scope.modalPesquisar.hide();
		}
		else //campos vazios
		{
			modalAlerta.alerta("Ocorreu um erro","Preencha todos os campos!");
			return false;
		}
	
	}
	
});

//_______________ GEOLOCALIZAÇÃO ___________________//
function localizacao()
{
	window.localStorage.latitudeUsuario = -5.8123501;
	window.localStorage.longitudeUsuario = -35.2025723;
	
	if (navigator.geolocation)
		navigator.geolocation.getCurrentPosition(showPosition);
}
function showPosition(position) 
{
	window.localStorage.latitudeUsuario = position.coords.latitude;
	window.localStorage.longitudeUsuario = position.coords.longitude;
}