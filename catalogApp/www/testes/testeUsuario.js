describe("Testes de Usuario", function () {
    beforeEach(module("catalogApp"));

    describe("UsuarioController", function () {
        var scope,
            controller;

        beforeEach(inject(function ($rootScope, $controller) {
            scope = $rootScope.$new();
            controller = $controller;
        }));

        it("Senhas diferentes deve retornar falso", function () {
            controller("UsuarioController", {$scope: scope});
			var retorno = scope.cadastrar({nome: "jonnathan", email: "jonnathan@gmail.com",senha: "1234", confirmarSenha: "1235678"});
            expect(retorno).toBe(false);
        });
    });
});