USE [Financas]
GO







--USUARIO
INSERT INTO [dbo].[tb_usuario]
           ([usu_nome]
           ,[usu_email]
           ,[usu_senha]
           ,[usu_saldoinicial]
           ,[usu_datacadastro]
           ,[usu_ativo])
     VALUES
           ('Demonstração'
           ,'demofinancas@gmail.com'
           ,'6PwjbyiaGqi7SjUEsiawJQ=='
           ,0.00
           ,'2017-06-01'
           ,1)
GO


--CATEGORIA


INSERT INTO [dbo].[tb_categoria]
           ([cat_nome]
           ,[cat_tipo]
           ,[cat_id_pai]
           ,[usu_id])

SELECT 
       [cat_nome]
      ,[cat_tipo]
      ,[cat_id_pai]
      ,2
  FROM [Financas].[dbo].[tb_categoria]
  WHERE usu_id = 1 



update tb_categoria set cat_id_pai = (SELECT Cat_id from tb_categoria where usu_id = 2 and cat_nome = 'Transporte')
where usu_id = 2 and cat_id_pai = 15


select * from tb_categoria where 
cat_id in(
select distinct  cat_id_pai from tb_categoria where usu_id = 2 and cat_id_pai is not null)
order by cat_nome

select * from tb_categoria where usu_id = 2



--LANCAMENTO

INSERT INTO [dbo].[tb_lancamento]
           ([lanc_data]
           ,[lanc_valor]
           ,[lanc_desc]
           ,[lanc_tipo]
           ,[usu_id]
           ,[cat_id])
SELECT
       (select [dbo].[func_DateFromParts](2017,month([lanc_data]), day(lanc_data), 0, 0, 0))
      ,[lanc_valor]
      ,[lanc_desc]
      ,[lanc_tipo]
      ,2
      ,(
	  SELECT TOP 1 cat.cat_id FROM tb_categoria cat WHERE cat.usu_id = 2 and CAT.cat_nome = C.cat_nome
	  )
  FROM [dbo].[tb_lancamento] L 
  JOIN tb_categoria C
  ON L.cat_id = C.cat_id
  WHERE L.usu_id = 1 and year(lanc_data) = 2016






--DESPESA

INSERT INTO [dbo].[tb_despesa]
           ([desp_desc]
           ,[desp_valor]
           ,[desp_vencto]
           ,[usu_id]
           ,[cat_id])
SELECT
      [desp_desc]
      ,[desp_valor]
      ,[desp_vencto]
      ,2
      ,(SELECT cat_id fROM tb_categoria WHERE usu_id = 2 and cat_nome = C.cat_nome)
  FROM [Financas].[dbo].[tb_despesa] D
  JOIN tb_categoria C on
  D.cat_id = C.cat_id
where D.usu_id = 1


--ORCAMENTO
INSERT INTO [dbo].[tb_orcamento]
           ([orc_tipo]
           ,[orc_mes]
           ,[orc_ano]
           ,[orc_valor]
           ,[usu_id]
           ,[cat_id])

SELECT 
      [orc_tipo]
      ,12
      ,2017
      ,[orc_valor]
      ,2
      ,(SELECT cat_id fROM tb_categoria WHERE usu_id = 2 and cat_nome = C.cat_nome)
  FROM [Financas].[dbo].[tb_orcamento] ORC

   JOIN tb_categoria C on
  ORC.cat_id = C.cat_id
  WHERE ORC.usu_id = 1 and ORC.orc_mes = 1 and ORC.orc_ano = 2017


  select * from tb_orcamento where usu_id = 2

