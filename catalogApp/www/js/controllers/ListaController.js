angular.module('ListaControllers', 
[
'ionic',
'services.verificarLogin',
'services.WebServices',
'services.modalAlerta',
'model.lista',
'model.produto'
])
.config(function($stateProvider, $urlRouterProvider) {
	
    $stateProvider
		.state('listas', {
			url: '/listas',
			templateUrl: 'listas.html',
			controller: 'ListaController'
		})
		.state('produtos-lista/:id', {
			url: '/produtos-lista',
			templateUrl: 'produtos-lista.html',
			controller: 'ProdutosListaController'
		})
})
.controller('ListaController', function($scope, $ionicModal, $http, modalAlerta, verificarLogin, WebServices, $ionicPopup, listaModelo) 
{
	//chave e listas
	var idUsuario = window.localStorage.idUsuario;
	var chave = "{idUsuario:'"+idUsuario+"',token:'"+window.localStorage.token+"',ultimoAcesso:'"+window.localStorage.ultimoAcesso+"'}";
	$scope.listas = [];

	//________________ EDITAR LISTAS _____________//
	$scope.editarLista = function(lista,indiceLista) {
		for(var i=0; i< $scope.listas.length; i++)
		{
			if($scope.listas[i].indice == indiceLista)
			{
				var idLista = $scope.listas[i].id;
				var idUsuario = $scope.listas[i].idUsuario;
				var titulo = lista.nome;
				var dtoLista = "{id:'"+idLista+"',idUsuario:'"+idUsuario+"',titulo:'"+titulo+"'}";
			
				WebServices.editarListas(chave,dtoLista)
				.success(function(data, status, headers, config)
				{
					var retorno = angular.fromJson(data.d);		
				});
				
				// listaModelo.update(0, lista.nome, idUsuario, $scope.listas[i].titulo);
				$scope.listas[i].titulo = lista.nome;
			}
		}
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
	   var res = modalAlerta.confirmar("Deletar","Tem certeza que deseja deletar " + lista.titulo + " ?",function(res){
		   if(res)
		   {
				$scope.listas.splice($scope.listas.indexOf(lista), 1);
				var dtoLista = "{id:'"+lista.id+"',idUsuario:'"+lista.idUsuario+"',titulo:'"+lista.titulo+"'}";
												
				WebServices.excluirLista(chave,dtoLista)
				.success(function(data, status, headers, config)
				{
					var retorno = angular.fromJson(data.d);		
				});
		   } 	   	   
	   });
	};
	
  	
	//________________ PESQUISAR LISTAS _____________//
	$scope.pesquisarLista = function()
	{	
		// listaModelo.select(idUsuario, function(retorno){
			// for(var l=0; retorno.length > l; l++)
			// {
				// var idLista = retorno[l].id;
				// var idUsuario = retorno[l].id_usuario;
				// var titulo = retorno[l].nome;
				
				// $scope.listas[l] = {idLista:idLista,idUsuario:idUsuario,titulo:titulo,indice:l};
			// }		
		// });	
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
					console.log($scope.listas);
				}
			}
			else
			{
				modalAlerta.alerta("Ocorreu um erro",retorno.mensagem);
			}
		});
	} 

	//________________ CRIAR LISTA _________________//
	$scope.criarLista = function(lista)
	{
		if(lista != undefined){
			
			$scope.listas.push({ titulo: lista.nome});
			$scope.modal.hide();	
			var dtoLista = "{idUsuario:'"+idUsuario+"',titulo:'"+lista.nome+"'}";
			// var id = window.localStorage.idUltimoListaCriada-1;
			// listaModelo.insertInto(id, lista.nome, idUsuario);
			// window.localStorage.idUltimoListaCriada--;
			
			WebServices.criarListas(chave,dtoLista)
			.success(function(data, status, headers, config)
			{
				var retorno = angular.fromJson(data.d);	
				modalAlerta.alerta("lista","lista criada com sucesso!");
			});
		}
		else
		{
			modalAlerta.alerta("Lista","Adicione um nome a lista");
		}
	}

	//________________ VERIFICAR LOGIN _________________//
	$scope.verificarLogin = function(lugarPagina)
	{
		$scope.pesquisarLista(); //chama as listas
		listaModelo.openDataBase();
		$scope.produtos = [];
		verificarLogin.verificarLista(lugarPagina);
	}
	
	
	//_______________ ABRIR MODAL DE CADASTRO DA LISTA __________________//
	$ionicModal.fromTemplateUrl('templates/adicionarLista.html', {
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
})
//########################################################################################################//
.controller('ProdutosListaController', function($scope, $ionicModal, produto, verificarLogin, WebServices, modalAlerta)
{
	var chave = "{idUsuario:'"+window.localStorage.idUsuario+"',token:'"+window.localStorage.token+"',ultimoAcesso:'"+window.localStorage.ultimoAcesso+"'}";
	$scope.produtos = [];
	$scope.produtosEncontrados = [];
	
	var url = window.location.href.toString();
	$scope.idLista = url.split("?")[1];
	
	 //___________________ REMOVER PRODUTO ______________//
	$scope.removerProduto = function(produto) 
	{
	   var res = modalAlerta.confirmar("Deletar","Tem certeza que deseja deletar " + produto.nomeProduto + " ?",function(res){
		   if(res)
		   {
				$scope.produtos.splice($scope.produtos.indexOf(produto), 1);
				var dtoLista = "{id:'"+$scope.idLista+"',idUsuario:'"+window.localStorage.idUsuario+"',titulo:''}";
				var dtoProduto = "{id:'"+produto.idProduto+"',nome:'"+produto.nomeProduto+"',codigoDeBarras:''}";	 	
				
				WebServices.removerProduto(chave,dtoLista, dtoProduto)
				.success(function(data, status, headers, config)
				{
					var retorno = angular.fromJson(data.d);		
				});
		   } 	   	   
	   });
	};
	
	//_______________ ABRIR MODAL DE PESQUISA DO PRODUTO __________________//
	$ionicModal.fromTemplateUrl('templates/adicionarProduto.html', {
		scope: $scope
	}).then(function(pesquisa) {
		$scope.pesquisa = pesquisa;
	});
	
	$ionicModal.fromTemplateUrl('templates/cadastrarProduto.html', {
		scope: $scope
	}).then(function(cadastro) {
		$scope.cadastro = cadastro;
	});
	
	$ionicModal.fromTemplateUrl('templates/quantidadeProduto.html', {
		scope: $scope
	}).then(function(quantidade) {
		$scope.quantidade = quantidade;
	});
	
	$ionicModal.fromTemplateUrl('templates/checkin-lista.html', {
		scope: $scope
	}).then(function(checkin) {
		$scope.modalCheckin = checkin;
	});
	
	//__________ INICIAR CHECKIN ___________//
	$scope.iniciarCheckin = function(checkin)
	{	
		window.localStorage.flagCheckin = "lista";
		window.localStorage.idEstabCheckin = checkin.estabelecimento;		
		modalAlerta.sucesso("CheckIn","Efetuando...","#/checkin?"+$scope.idLista+"");
		$scope.modalCheckin.hide();
	}
	
	//__________ ESCOLHER ESTABELECIMENTO E LISTA _______________//
	$scope.escolherEstabLista = function()
	{	
		$scope.estabelecimentos = [];	
		
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
	
	
	//_______________ ABRIR MODAL DE CADASTRO DO PRODUTO __________________//
	$scope.cadastroProdutoModal = function(modal, cadastro)
	{
		$scope.pesquisa.hide();
		cadastro.show();
	}
	
	//_______________ ABRIR MODAL DE PESQUISA DO PRODUTO __________________//
	$scope.abrirModalPesquisa = function()
	{
		$scope.textoPesquisa = "";
		$scope.textoExplicativo = "";
		$scope.produtosEncontrados = [];
		$scope.pesquisa.show();
		document.getElementById("botaoAdd").className = "botao-flutuante hide";
		document.getElementById("blocoPesquisarProduto").className = "ng-valid ng-dirty ng-valid-parse";
		$scope.formularioPesquisa = false;
	}
	
	//________________ VERIFICAR LOGIN _________________//
	$scope.verificarLogin = function(lugarPagina)
	{
		$scope.abrirLista();
		produto.openDataBase();
		verificarLogin.verificarProduto(lugarPagina);
	}
	
	//_________________ ABRIR LISTA _________________//
	$scope.abrirLista = function()
	{	
		var dtoLista = "{id:'"+$scope.idLista+"',idUsuario:'"+window.localStorage.idUsuario+"',titulo:''}";
		
		WebServices.abrirLista(chave,dtoLista)
		.success(function(data, status, headers, config)
		{
			var retorno = angular.fromJson(data.d);	
			if(retorno.tipoRetorno == "ACK") //logado
			{	
				if(retorno.objeto.produtosDaLista != null)
				{
					for(var l=0; retorno.objeto.produtosDaLista.length > l; l++)
					{
						var idLista = retorno.objeto.id;
						$scope.titulo = retorno.objeto.titulo;
						var precoTotal = retorno.objeto.precoTotal;
						var idProduto = retorno.objeto.produtosDaLista[l].idProduto;
						var quantidade = retorno.objeto.produtosDaLista[l].quantidade;
						var nomeProduto = retorno.objeto.produtosDaLista[l].produto.nome;
						
						$scope.produtos[l] = {idProduto:idProduto, idLista:idLista, titulo:$scope.titulo, precoTotal:precoTotal, quantidade:quantidade, nomeProduto:nomeProduto};
					}
				}		
			}
			else //erro
			{
				modalAlerta.alerta("Ocorreu um erro",retorno.mensagem);
			}		
		});
		
	}	
	$scope.botaoAdicionar = true;
	
	//_________________ PESQUISAR PRODUTO _________________//
	$scope.pesquisarProduto = function(produto)
	{	
		var nome = produto.nome;
		var fabricante = produto.fabricante;
		var tipo = produto.tipo;
		
		
		if(nome==undefined) 		nome="";
		if(fabricante==undefined)	fabricante="";
		if(tipo==undefined)			tipo="";
		
		if(!isNaN(parseFloat(nome)) == false && !isNaN(parseFloat(fabricante)) == false )
		{
			var fabricante = "{fabricante:'"+fabricante+"'}";
			var tipo = "{tipo:'"+tipo+"'}";
				
			var dtoProduto = "{nome:'"+nome+"',codigoDeBarras:'',tipoCodigoDeBarras:'',fabricante:"+fabricante+",tipo:"+tipo+"}";			
			
			WebServices.pesquisarProduto(chave,dtoProduto)
			.success(function(data, status, headers, config)
			{
				var retorno = angular.fromJson(data.d);	
				if(retorno.tipoRetorno == "ACK") //logado
				{
					if(retorno.objeto.length == 0)
					{
						$scope.formularioPesquisa = true;
						document.getElementById("blocoPesquisarProduto").className = "ng-pristine ng-valid ng-hide";
						$scope.textoPesquisa = "Produtos Encontrados!";
						
						$scope.botaoAdicionar = false;
						document.getElementById("botaoAdd").className = "botao-flutuante";
						$scope.textoExplicativo = "Desculpe, mas nao encontramos nenhum resultado para o produto pesquisado. Por favor, cadastre um produto!"
					}
					else
					{
						$scope.formularioPesquisa = true;
						document.getElementById("blocoPesquisarProduto").className = "ng-pristine ng-valid ng-hide";
						$scope.textoPesquisa = "Produtos Encontrados!";
						
						for(var l=0; retorno.objeto.length > l; l++)
						{
							var id = retorno.objeto[l].id;
							var nome = retorno.objeto[l].nome;
							var marca = retorno.objeto[l].fabricante.fabricante;
							
							$scope.produtosEncontrados[l] = {id:id, nome:nome, marca:marca};
						}
					}
				}
				else
				{
					modalAlerta.alerta("Ocorreu um erro",retorno.mensagem);
				}	
			});	
		}
		else
		{
			modalAlerta.alerta("Ocorreu um erro","Formato invalido!");
		}	
	}	
	
	//_________________ CRIAR PRODUTO _________________//
	$scope.criarProduto = function(produto)
	{	
		if(!isNaN(parseFloat(produto.nome)) == false && !isNaN(parseFloat(produto.fabricante)) == false)
		{
			var fabricante = "{fabricante:'"+produto.fabricante+"'}";
			var tipo = "{tipo:'"+produto.tipo+"'}";
			var dtoProduto = "{nome:'"+produto.nome+"',codigoDeBarras:'"+produto.codBarra+"',idTipo:'"+produto.tipo+"',fabricante:"+fabricante+",tipo:"+tipo+"}";			
			
			WebServices.criarProduto(chave,dtoProduto)
			.success(function(data, status, headers, config)
			{
				var retorno = angular.fromJson(data.d);	
				if(retorno.tipoRetorno == "ACK") //logado
				{
					$scope.dtoLista = "{id:'"+$scope.idLista+"',titulo:'',idUsuario:'"+window.localStorage.idUsuario+"'}";		
					$scope.idProduto = retorno.objeto.id;	
					$scope.nome = retorno.objeto.nome;
					$scope.cadastro.hide();
					$scope.quantidade.show();
				}
				else
				{
					modalAlerta.alerta("Ocorreu um erro",retorno.mensagem);
				}
			});	
		}
		else
		{
			modalAlerta.alerta("Ocorreu um erro","Formato invalido!");
		}	
	}	
	
	//________________ ADICIONAR QUANT PRODUTO ENCONTRADO ______________//
	$scope.adicionarQuantidadeProdutoEncontrado = function(idProduto,nome)
	{
		$scope.quantidade.show();
		$scope.idProduto = idProduto;
		$scope.nome = nome;
		$scope.dtoLista = "{id:'"+$scope.idLista+"',titulo:'',idUsuario:'"+window.localStorage.idUsuario+"'}";
	}
	
	//_________________ ADICIONAR PRODUTO A LISTA _________________//
	$scope.adicionarProdutoALista = function(produto)
	{	
		var dtoProdutoDaLista = "{idLista:'"+$scope.idLista+"',idProduto:'"+$scope.idProduto+"',quantidade:'"+produto.quantidade+"',produto:{nome:'"+$scope.nome +"'}}";			
		
		WebServices.adicionarProdutoALista(chave,$scope.dtoLista,dtoProdutoDaLista)
		.success(function(data, status, headers, config)
		{
			var retorno = angular.fromJson(data.d);	
			if(retorno.tipoRetorno == "ACK") //logado
			{
				$scope.produtos.push({idProduto:$scope.idProduto, idLista:$scope.idLista, titulo:$scope.titulo,quantidade:produto.quantidade, nomeProduto:$scope.nome});
				console.log($scope.produtos);
				$scope.quantidade.hide();
				$scope.pesquisa.hide();
			}
			else
			{
				modalAlerta.alerta("Ocorreu um erro",retorno.mensagem);
			}
		});	
	}	
});