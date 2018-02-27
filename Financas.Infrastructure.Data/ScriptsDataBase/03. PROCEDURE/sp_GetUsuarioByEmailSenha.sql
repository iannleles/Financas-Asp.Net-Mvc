


CREATE PROC [dbo].[sp_GetUsuarioByEmailSenha] -- 'joaofama@gmail.com','H6EIocQ0f7s59Y+CxMFcrg=='
(
	@Email		 VARCHAR(50),
	@Senha		 VARCHAR(50)
) 

AS

SELECT [usu_id] AS Id
      ,[usu_nome] AS Nome
      ,[usu_email] AS Email
      ,[usu_senha] AS Senha
      ,[usu_saldoinicial] AS SaldoInicial
      ,[usu_datacadastro] AS DataCadastro 
      ,[usu_ativo] AS Ativo
  FROM [dbo].[tb_usuario]
WHERE usu_email = @Email AND usu_senha = @Senha

