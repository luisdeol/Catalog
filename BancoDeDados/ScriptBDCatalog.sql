CREATE DATABASE Catalog;
USE Catalog;

CREATE TABLE tb_Usuario(
	id int primary key identity(1,1),
	email varchar(50) not null,
	senha varchar(50) not null,
	nome varchar(100) not null
);

CREATE TABLE tb_SenhaAlternativa(
	idUsuario int,
	senha varchar(50),
	dataDeCriacao Date,
	PRIMARY KEY CLUSTERED(idUsuario),
	FOREIGN KEY (idUsuario) REFERENCES tb_Usuario(id)
);

CREATE TABLE tb_Chave(
	idUsuario int,
	token varchar(50),
	ultimoAcesso time,
	PRIMARY KEY CLUSTERED(idUsuario),
	FOREIGN KEY (idUsuario) REFERENCES tb_Usuario(id)
);

CREATE TABLE tb_Lista(
	id int primary key identity(1,1),
	titulo varchar(50),
	idUsuario int foreign key references tb_Usuario(id)
)

CREATE TABLE tb_Fabricante(
	id int primary key identity(1,1),
	fabricante varchar(50)
)

CREATE TABLE tb_Tipo(
	id int primary key identity(1,1),
	tipo varchar(50)
)

CREATE TABLE tb_Produto(
	id int primary key identity(1,1),
	nome varchar(100),
	codigoDeBarras varchar(50),
	idFabricante int foreign key references tb_Fabricante(id),
	idTipo int foreign key references tb_tipo(id)
);

CREATE TABLE tb_ProdutoDaLista(
	id int primary key identity(1,1),
	quantidade int,
	idProduto int foreign key references tb_Produto(id),
	idLista int foreign key references tb_Lista(id)
)

CREATE TABLE tb_Estabelecimento(
	id int primary key identity(1,1),
	estabelecimento varchar(50)
)

CREATE TABLE tb_EnderecoEstabelecimento(
	id int primary key identity(1,1),
	rua varchar(100),
	cidade varchar(100),
	estado varchar(100),
	numero varchar(50),
	cep varchar(100),
	latitude DOUBLE PRECISION,
	longitude DOUBLE PRECISION,
	idEstabelecimento int foreign key references tb_Estabelecimento(id)
)

CREATE TABLE tb_Item(
	id int primary key identity(1,1),
	preco DOUBLE PRECISION,
	compraRecente date,
	qualificacao int,
	idEstabelecimento int foreign key references tb_EnderecoEstabelecimento(id),
	idProduto int foreign key references tb_Produto(id)
);