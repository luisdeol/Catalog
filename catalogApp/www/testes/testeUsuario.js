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
			var retorno = scope.cadastrar({nome: "jonnathan", email: "jonnathan@gmail.com",senha: "1234", confirmarSenha: "12345"});
            expect(retorno).toBe(false);
        });
		
		it("(Cadastro)Preencha todos os campos", function () {
            controller("UsuarioController", {$scope: scope});
			var retorno = scope.cadastrar();
            expect(retorno).toBe(false);
        });
		
		it("(Login)Preencha todos os campos", function () {
            controller("UsuarioController", {$scope: scope});
			var retorno = scope.logar();
            expect(retorno).toBe(false);
        });
    });
});