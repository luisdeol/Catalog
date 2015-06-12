angular.module('catalogApp', ['ionic'])
.controller('ListaController', function($scope,$ionicModal,$http,$ionicPopup,$timeout) {
  
	var chave = "{idUsuario:'"+window.localStorage.idUsuario+"',token:'"+window.localStorage.token+"',ultimoAcesso:'"+window.localStorage.ultimoAcesso+"'}";
	$scope.listas = [];
  
	$scope.data = {
		showDelete: false
	};
  
	//________________ EDITAR LISTAS _____________//
	$scope.editarLista = function(lista,indiceLista) {
		for(var i=0; i< $scope.listas.length; i++)
		{
			if($scope.listas[i].indice == indiceLista)
			{
				var idLista = $scope.listas[i].idLista;
				var idUsuario = $scope.listas[i].idUsuario;
				var titulo = lista.nome;
				$scope.listas[i].titulo = lista.nome;
			}
		}
		var dtoLista = "{idLista:'"+idLista+"',idUsuario:'"+idUsuario+"',titulo:'"+titulo+"'}";
	
		$http.post('http://localhost:51786/Webservices/WsLista.asmx/editarLista', {dtoChave:chave,dtoLista:dtoLista}).
		success(function(data, status, headers, config)
		{
			var retorno = angular.fromJson(data.d);	
				
		});
	};
	
    //__________________ MOVER LISTA _________________//
	$scope.moverLista = function(lista, fromIndex, toIndex)
	{
		$scope.listas.splice(fromIndex, 1);
		$scope.listas.splice(toIndex, 0, lista);
	};
  
    //___________________ DELETAR LISTA ______________//
	$scope.deletarLista = function(lista) 
	{
		$scope.listas.splice($scope.listas.indexOf(lista), 1);
	};
  	
	//________________ PESQUISAR LISTAS _____________//
	$scope.pesquisarLista = function()
	{	
		$http.post('http://localhost:51786/Webservices/WsLista.asmx/pesquisarLista', {dtoChave:chave,parametros:""}).
	    success(function(data, status, headers, config)
		{
			var retorno = angular.fromJson(data.d);	
			for(var l=0; retorno.objeto.length > l; l++)
			{
				var idLista = retorno.objeto[l].id;
				var idUsuario = retorno.objeto[l].idUsuario;
				var titulo = retorno.objeto[l].titulo;
				
				$scope.listas[l] = {idLista:idLista,idUsuario:idUsuario,titulo:titulo,indice:l};
			}
		});
	}

	//________________ CRIAR LISTA _________________//
	$scope.criarLista = function(lista)
	{
		if(lista != undefined){
			$scope.listas.push({ nome: lista.nome});
			$scope.modal.hide();	
			$scope.alerta("Lista","Lista criada com sucesso!");
		}
		else
		{
			$scope.alerta("Lista","Adicione um nome a lista");
		}
	}
  
	//________________ VERIFICAR LOGIN _________________//
	$scope.verificarLogin = function(lugarPagina)
	{
		var idUsuario = window.localStorage.idUsuario;
		var token = window.localStorage.token;
		var ultimoAcesso = window.localStorage.ultimoAcesso;
		document.getElementById("imgAdd").className = "img-add-lista";
		
		if((idUsuario != undefined && idUsuario != "") && 
			(token != undefined && token != "") && 
			(ultimoAcesso != undefined && ultimoAcesso != "")) //ta logado
		{
			$scope.pesquisarLista(); //chama as listas
			if(lugarPagina != "listas.html")
				window.location = lugarPagina;	
		}
		else //nao esta logado
		{
			window.location = "index.html#/tab/login";
		}	
	}
	
	//_______________ ABRIR MODAL DE CADASTRO DA LISTA __________________//
	$ionicModal.fromTemplateUrl('templates/modal.html', {
		scope: $scope
	}).then(function(modal) {
		$scope.modal = modal;
	});
	
	//_______________ MODAL EDITAR LISTA __________________//
	$scope.modalEditarLista = function(indiceLista) {
	   $scope.lista = {};
	   

	   var myPopup = $ionicPopup.show
	   ({
		 template: '<input type="text" ng-model="lista.nome">',
		 title: 'Editar Lista',
		 subTitle: 'Edite o nome da lista',
		 scope: $scope,
		 buttons: [
		   { text: 'Cancel' },
		   {
			 text: '<b>Save</b>',
			 type: 'button-positive',
			 onTap: function(e) {
			   if (!$scope.lista.nome) {
				 e.preventDefault();
			   } else {
				 myPopup.close;  
				 $scope.editarLista($scope.lista,indiceLista);
			   }
			 }
		   },
		 ]
		});
	}
	
	//____________ ALERTA ____________//
	$scope.alerta = function(mensagem,subMensagem)
	{
		var alertPopup = $ionicPopup.alert({
		title: mensagem,
		template: subMensagem
		});
		
		 $timeout(function() 
		{
		  alertPopup.close();
		}, 3000);
	};
  
});