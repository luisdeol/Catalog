angular.module('CheckinControllers', 
[
'ionic',
'services.WebServices',
'services.modalAlerta'
])
.config(function($stateProvider, $urlRouterProvider) {
	
    $stateProvider
		.state('checkin', {
			url: '/checkin',
			templateUrl: 'checkin.html',
			controller: 'CheckinController'
		})
})
.controller('CheckinController', function($scope, $ionicModal, $http, modalAlerta, WebServices, $ionicPopup) 
{
	//chave e listas
	var idUsuario = window.localStorage.idUsuario;
	var chave = "{idUsuario:'"+idUsuario+"',token:'"+window.localStorage.token+"',ultimoAcesso:'"+window.localStorage.ultimoAcesso+"'}";
	$scope.itens = [];
	$scope.itensMarcado = [];
	$scope.total = 0.00;
	$scope.flag = window.localStorage.flagCheckin;
	
	var url = window.location.href.toString();
	$scope.idLista = url.split("?")[1];
	
	//________________ VOLTAR A LISTA ______________//
	$scope.voltarALista = function(idLista)
	{
		location.href="#/produtos-lista?"+idLista+"";
	}
	//________________ LISTAR ITENS ________________//
	$scope.listarItens = function()
	{
		var dtoLista = "{id:"+$scope.idLista+"}";
		var dtoEnderecoEstabelecimento = "{id:"+window.localStorage.idEstabCheckin+"}";
		
		WebServices.listarItensEm(chave,dtoLista,dtoEnderecoEstabelecimento)
		.success(function(data, status, headers, config)
		{
			var retorno = angular.fromJson(data.d);	
			if(retorno.tipoRetorno == "ACK") //logado
			{
				for(var l=0; retorno.objeto.produtosDaLista.length > l; l++)
				{
					var idProduto = retorno.objeto.produtosDaLista[l].idProduto;
					var nome = retorno.objeto.produtosDaLista[l].item.produto.nome;
					var quantidade = retorno.objeto.produtosDaLista[l].quantidade;
					var preco = retorno.objeto.produtosDaLista[l].item.preco;
					
					$scope.itens[l] = {id:idProduto,nome:nome,quantidade:quantidade,preco:preco};
				}
			}
			else
			{
				modalAlerta.alerta("Ocorreu um erro",retorno.mensagem);
			}
		});
	}

	//________________ CHECKIN ITENS ____________________//	
	$scope.checkItens = function(item)
	{
		if(item.checked == true)
		{
			$scope.itensMarcado.push(item);
			modalAlerta.confirmar("Voce deseja editar esse preco?","R$"+item.preco.toFixed(2), function(res){
				var resposta = res;
				if(resposta == true)
				{
					$scope.item = item;
					$scope.precoModal.show();
					$scope.total -= item.preco*item.quantidade;
					item.checked = false;
					for(var i=0; i<$scope.itensMarcado.length; i++)
					{
						if($scope.itensMarcado[i].id == item.id)
						$scope.itensMarcado.splice(i,1);
					}
				}
			
			})
			$scope.total += item.preco*item.quantidade;
		}
		else
		{
			for(var i=0; i<$scope.itensMarcado.length; i++)
			{
				if($scope.itensMarcado[i].id == item.id)
				$scope.itensMarcado.splice(i,1);
			}
			$scope.total -= item.preco*item.quantidade;
		}
	}
	
	//________________ EDITAR PREÇO ____________________//	
	$scope.editarPreco = function(produto)
	{
		var novoPreco = produto.preco;
		var item = $scope.item;
		item.preco = Number(novoPreco);
		$scope.precoModal.hide();	
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
	
	//_______________ ABRIR MODAL DE CADASTRO DO PRODUTO __________________//
	$scope.cadastroProdutoModal = function(modal, cadastro)
	{
		$scope.pesquisa.hide();
		cadastro.show();
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
							console.log($scope.produtosEncontrados);
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
	
	//_______________ ADICIONAR QUANTIDADE PRODUTO ENCONTRADO _____________________//
	$scope.adicionarQuantidadeProdutoEncontrado = function(idProduto,nome)
	{
		$scope.quantidade.show();
		$scope.idProduto = idProduto;
		$scope.nome = nome;
		$scope.dtoLista = "{id:'"+$scope.idLista+"',titulo:'',idUsuario:'"+window.localStorage.idUsuario+"'}";
	}
	
	//_________________ ADICIONAR PRODUTO CHECKIN _________________//
	$scope.adicionarProdutoCheckin = function(produto)
	{			
		$scope.itens.push({id:$scope.idProduto, nome:$scope.nome,quantidade:produto.quantidade, preco:produto.preco});
		var dtoProdutoDaLista = "{idLista:'"+$scope.idLista+"',idProduto:'"+$scope.idProduto+"',quantidade:'"+produto.quantidade+"',produto:{nome:'"+$scope.nome +"'}}";			
		
		WebServices.adicionarProdutoALista(chave,$scope.dtoLista,dtoProdutoDaLista)
		.success(function(data, status, headers, config)
		{
			var retorno = angular.fromJson(data.d);	
			if(retorno.tipoRetorno == "ACK") //logado
			{
				modalAlerta.alerta("Sucesso","Produto adicionado!");
			}
			else
			{
				modalAlerta.alerta("Ocorreu um erro",retorno.mensagem);
			}
		});	
		
		$scope.quantidade.hide();
		$scope.pesquisa.hide();
	}	
	
	//________________ FINALIZAR CHECKIN __________________//
	$scope.finalizarCheckin = function()
	{
		var stringItens = "[";
		
		for(var i=0; i<$scope.itensMarcado.length; i++)
		{
			if($scope.itensMarcado[i].preco != 0)
			{
				stringItens += "{id:"+$scope.itensMarcado[i].id+",item:{produto:{id:"+$scope.itensMarcado[i].id+"},preco:"+$scope.itensMarcado[i].preco+"}},";	
			}
			else
			{
				modalAlerta.alerta("Ocorreu um erro","Preco nao foi editado!");
				return;
			}	
				
		}
		 
		var stringSemVirgula = stringItens.substring(0,(stringItens.length - 1));
		var itens = ""+stringSemVirgula+"]";
		console.log(itens);
		
		modalAlerta.confirmar("CheckOut","Voce deseja finalizar compra?", function(resposta){
			if(resposta == true)
			{
				var estab = "{id:"+window.localStorage.idEstabCheckin+"}";
				WebServices.finalizarCheckin(chave,estab,itens)
				.success(function(data, status, headers, config)
				{
					var retorno = angular.fromJson(data.d);	
					if(retorno.tipoRetorno == "ACK") //logado
					{
						modalAlerta.sucesso("CheckOut","Finalizando...","#/menu");
					}
					else
					{
						modalAlerta.alerta("Ocorreu um erro",retorno.mensagem);
					}
				});	
			}
		});
		
	
	}
	
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
	
	$ionicModal.fromTemplateUrl('templates/quantidadeProdutoCheckin.html', {
		scope: $scope
	}).then(function(quantidade) {
		$scope.quantidade = quantidade;
	});
	
	$ionicModal.fromTemplateUrl('templates/precoProdutoCheckin.html', {
		scope: $scope
	}).then(function(preco) {
		$scope.precoModal = preco;
	});
	
	
})
