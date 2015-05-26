CREATE DATABASE Catalog;
USE Catalog;

CREATE TABLE tb_Usuario(
	id_usuario int primary key identity(1,1),
	email varchar(50) not null,
	senha varchar(50) not null,
	nome varchar(100) not null
);

CREATE TABLE tb_SenhaAlternativa(
	id_senhaAlternativa int primary key identity(1,1),
	senha varchar(50),
	dataDeCriacao Date,
	id_usuario_fk int foreign key references tb_Usuario(id_usuario)
);

CREATE TABLE tb_Chave(
	id_chave int primary key identity(1,1),
	token varchar(50),
	ultimoAcesso time,
	id_usuario_fk int foreign key references tb_Usuario(id_usuario)
);

CREATE TABLE tb_Lista(
	id_lista int primary key identity(1,1),
	titulo varchar(50),
	id_usuario_fk int foreign key references tb_Usuario(id_usuario)
)

CREATE TABLE tb_Fabricante(
	id_fabricante int primary key identity(1,1),
	fabricante varchar(50)
)

CREATE TABLE tb_Tipo(
	id_tipo int primary key identity(1,1),
	tipo varchar(50)
)

CREATE TABLE tb_Produto(
	id_produto int primary key identity(1,1),
	nome varchar(100),
	codigoDeBarras varchar(50),
	id_fabricante_fk int foreign key references tb_Fabricante(id_fabricante),
	id_tipo_fk int foreign key references tb_tipo(id_tipo)
);

CREATE TABLE tb_ProdutoDaLista(
	id_produtoDaLista int primary key identity(1,1),
	quantidade int,
	id_produto_fk int foreign key references tb_Produto(id_produto),
	id_lista_fk int foreign key references tb_Lista(id_lista)
)

CREATE TABLE tb_Estabelecimento(
	id_estabelecimento int primary key identity(1,1),
	estabelecimento varchar(50)
)

CREATE TABLE tb_EnderecoEstabelecimento(
	id_enderecoEstabelecimento int primary key identity(1,1),
	rua varchar(100),
	cidade varchar(100),
	estado varchar(100),
	numero varchar(50),
	cep varchar(100),
	latitude DOUBLE PRECISION,
	longitude DOUBLE PRECISION,
	id_estabelecimento_fk int foreign key references tb_Estabelecimento(id_estabelecimento)
)

CREATE TABLE tb_Item(
	id_iten int primary key identity(1,1),
	preco DOUBLE PRECISION,
	compraRecente date,
	qualificacao int,
	id_estabelecimento_fk int foreign key references tb_Estabelecimento(id_estabelecimento),
	id_produto_fk int foreign key references tb_Produto(id_produto),
);