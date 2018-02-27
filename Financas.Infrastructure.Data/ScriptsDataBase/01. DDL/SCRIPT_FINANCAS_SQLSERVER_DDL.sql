CREATE DATABASE Financas;

USE Financas;

CREATE TABLE tb_usuario (
usu_id INT NOT NULL IDENTITY PRIMARY KEY,
usu_nome VARCHAR(50) NOT NULL,
usu_email VARCHAR(50) NOT NULL,
usu_senha VARCHAR(50) NOT NULL,
usu_saldoinicial NUMERIC(18, 2) NOT NULL,
usu_datacadastro DATE NOT NULL,
usu_ativo BIT
)
GO

CREATE TABLE tb_categoria (
cat_id 		INT NOT NULL IDENTITY PRIMARY KEY,
cat_nome 	VARCHAR(50) NOT NULL,
cat_tipo 	CHAR(1) NOT NULL,
cat_id_pai 	INT,
usu_id		INT NOT NULL,
FOREIGN KEY (cat_id_pai) REFERENCES tb_categoria (cat_id),
FOREIGN KEY(usu_id) REFERENCES tb_usuario (usu_id)
)
GO

CREATE TABLE tb_despesa (
desp_id INT NOT NULL IDENTITY PRIMARY KEY,
desp_desc VARCHAR(50) NOT NULL,
desp_valor  NUMERIC(18, 2) NOT NULL,
desp_vencto INT NOT NULL,
usu_id INT NOT NULL,
cat_id INT NOT NULL,
FOREIGN KEY(usu_id) REFERENCES tb_usuario (usu_id),
FOREIGN KEY(cat_id) REFERENCES tb_categoria (cat_id)
)
GO

CREATE TABLE tb_lancamento (
lanc_id INT NOT NULL IDENTITY PRIMARY KEY,
lanc_data DATE NOT NULL,
lanc_valor NUMERIC(18, 2),
lanc_desc VARCHAR(50),
lanc_tipo CHAR(1),
usu_id INT NOT NULL,
cat_id INT NOT NULL,
mov_row INT,
FOREIGN KEY(usu_id) REFERENCES tb_usuario (usu_id),
FOREIGN KEY(cat_id) REFERENCES tb_categoria (cat_id)
)
GO

CREATE TABLE tb_orcamento (
orc_id INT NOT NULL IDENTITY PRIMARY KEY,
orc_tipo CHAR(1),
orc_mes INT,
orc_ano INT,
orc_valor NUMERIC(18, 2),
usu_id INT NOT NULL,
cat_id INT NOT NULL,
FOREIGN KEY(usu_id) REFERENCES tb_usuario (usu_id),
FOREIGN KEY(cat_id) REFERENCES tb_categoria (cat_id)
)
GO




