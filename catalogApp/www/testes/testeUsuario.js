describe("Testes de Usuario", function () {
    beforeEach(module("UsuarioControllers"));

    describe("UsuarioController", function () {
        var scope,
            controller;

        beforeEach(inject(function ($rootScope, $controller) {
            scope = $rootScope.$new();
            controller = $controller;
        }));

        it("(Cadastro)Senhas diferentes devem retornar falso", function () {
            controller("UsuarioController", {$scope: scope});
			var retorno = scope.cadastrar({nome: "jonnathan", email: "jonnathan@gmail.com",senha: "123456", confirmarSenha: "1234567"});
            expect(retorno).toBe(false);
        });
		
		it("(Cadastro)Senha deve conter mais de 5 digitos", function () {
            controller("UsuarioController", {$scope: scope});
			
			var user = new Object();
			user.nome = "jonnathan";
			user.email = "jonnathan@gmail.com";
			user.senha = "1234";
			user.confirmarSenha = "123456";
			
			var retorno = scope.cadastrar(user);
            expect(retorno).toBe(false);
        });
		
		it("(Cadastro)Email inválido", function () {
            controller("UsuarioController", {$scope: scope});
			
			var user = new Object();
			user.nome = "jonnathan";
			user.email = "jonnathangmail.com"; 
			user.senha = "123456";
			user.confirmarSenha = "123456";
			
			scope.cadastrar(user);
			
            expect(scope.erro).toBe(true);
        });
		
		it("(Login)Preencha todos os campos", function () {
            controller("UsuarioController", {$scope: scope});
			var retorno = scope.logar();
            expect(retorno).toBe(false);
        });
    });
});