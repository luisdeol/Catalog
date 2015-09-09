angular.module('CatalogApp', ['services.WebServices'])
.controller('estabelecimentoController', function($scope, WebServices)
{
	$scope.estabelecimentos = [];
	$scope.pesquisarEstabelecimento = function(){	
	
		WebServices.pesquisarEstabelecimentos()
		.success(function(data, status, headers, config){
			
			var retorno = angular.fromJson(data.d);	
			if(retorno.tipoRetorno == "ACK"){
				for(var l=0; retorno.objeto.length > l; l++){
					var id = retorno.objeto[l].id;
					var nome = retorno.objeto[l].estabelecimento.nome;
					var rua = retorno.objeto[l].rua;
					var cidade = retorno.objeto[l].cidade;
					var estado = retorno.objeto[l].estado;
					var numero = retorno.objeto[l].numero;
					var cep = retorno.objeto[l].cep;
					var latitude = retorno.objeto[l].latitude;
					var longitude = retorno.objeto[l].longitude; 
					
					$scope.estabelecimentos[l] = {id:id, nome:nome, rua:rua, cidade:cidade, estado:estado, numero:numero, cep:cep, latitude:latitude, longitude:longitude};
				}
			}else{
				alert("Ocorreu um erro");
			}
		})
		.error(function(data, status, headers, config) {
			alert("Ocorreu um erro");
		});
	} 
})
.controller('listaController', function($scope, WebServices){
	$scope.produtos = [];
	$scope.abrirLista = function(){	
	
		WebServices.abrirLista()
		.success(function(data, status, headers, config){
			var retorno = angular.fromJson(data.d);	
			if(retorno.tipoRetorno == "ACK"){
				if(retorno.objeto.produtosDaLista != null){
					
					var flag = "linha"+"1";
					for(var l=0; retorno.objeto.produtosDaLista.length > l; l++){
						$scope.idLista = retorno.objeto.id;
						$scope.titulo = retorno.objeto.titulo;
						var precoTotal = retorno.objeto.precoTotal;
						var idProduto = retorno.objeto.produtosDaLista[l].idProduto;
						var quantidade = retorno.objeto.produtosDaLista[l].quantidade;
						var nomeProduto = retorno.objeto.produtosDaLista[l].produto.nome;
						var classe = flag;
						
						$scope.produtos[l] = {idProduto:idProduto, idLista:$scope.idLista, titulo:$scope.titulo, precoTotal:precoTotal, quantidade:quantidade, nomeProduto:nomeProduto, classe:classe};
						
						if(flag=="linha1")
							flag="linha2"
						else if(flag=="linha2")
							flag="linha1"

					}
				}		
			}else{
				alert("Ocorreu um erro");
			}
		})
		.error(function(data, status, headers, config){
			alert("Ocorreu um erro");
		});
	} 
})
.controller('listarItensController', function($scope, WebServices){
	$scope.itens = [];
	
	$scope.listarItensEm = function(estab){		
		var dtoLista = "{id:1}";
		var dtoEnderecoEstabelecimento = "{id:"+estab.id+"}";
		
		WebServices.listarItensEm(dtoLista, dtoEnderecoEstabelecimento)
		.success(function(data, status, headers, config){
			var retorno = angular.fromJson(data.d);	
			if(retorno.tipoRetorno == "ACK"){
				for(var l=0; retorno.objeto.produtosDaLista.length > l; l++){
					var idProduto = retorno.objeto.produtosDaLista[l].idProduto;
					var nome = retorno.objeto.produtosDaLista[l].item.produto.nome;
					var quantidade = retorno.objeto.produtosDaLista[l].quantidade;
					var preco = retorno.objeto.produtosDaLista[l].item.preco;
					$scope.nomeEstabelecimento = retorno.objeto.produtosDaLista[l].item.estabelecimento.estabelecimento.nome;
					$scope.latitude = retorno.objeto.produtosDaLista[l].item.estabelecimento.latitude;
					$scope.longitude = retorno.objeto.produtosDaLista[l].item.estabelecimento.longitude;
					
					$scope.produtos[l] = {id:idProduto,nomeProduto:nome,quantidade:quantidade,precoTotal:preco,classe:$scope.produtos[l].classe}
				}
			$scope.chamarMapa();
			}else{
				alert("Ocorreu um erro");
			}
		})
		.error(function(data, status, headers, config){
			alert("Ocorreu um erro");
		});
	} 
	
	$scope.chamarMapa = function(){	
		var center = new google.maps.LatLng($scope.latitude,$scope.longitude)
		var mapProp = {
			center: center,
			zoom:17,
			mapTypeId:google.maps.MapTypeId.ROADMAP
		};

		var map=new google.maps.Map(document.getElementById("googleMap"),mapProp);
		var marker=new google.maps.Marker({
			position:center,
		    animation:google.maps.Animation.BOUNCE
		});
		marker.setMap(map);
	} 
});