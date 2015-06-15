angular.module('ionicApp', ['ionic'])

.controller('ListaController', function($scope) {
  
  $scope.data = {
    showDelete: false
  };
  
  $scope.edit = function(item) {
    alert('Edit Item: ' + item.id);
  };
  $scope.share = function(item) {
    alert('Share Item: ' + item.id);
  };
  
  $scope.moveItem = function(item, fromIndex, toIndex) {
    $scope.items.splice(fromIndex, 1);
    $scope.items.splice(toIndex, 0, item);
  };
  
  $scope.onItemDelete = function(item) {
    $scope.items.splice($scope.items.indexOf(item), 1);
  };
  
  $scope.items = [
    { id: 0, nome:"Semanal" },
    { id: 1 ,nome:"Mensal" },
    { id: 2 ,nome:"Diária" },
    { id: 3 ,nome:"Fevereiro" },
    { id: 4 ,nome:"Janeiro" },
    { id: 5 ,nome:"Natal" },
    { id: 6 ,nome:"Mecanico"},
    { id: 7 ,nome:"Eletrodomésticos" },
    { id: 8 ,nome:"Novembro" },
    { id: 9 ,nome:"Aniversário de clara"},
    { id: 10 ,nome:"Frutas" }
  ];
  
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
			if(lugarPagina != "listas.html")
				window.location = lugarPagina;	
		}
		else //nao esta logado
		{
			window.location = "index.html#/tab/login";
		}	
	}
  
});