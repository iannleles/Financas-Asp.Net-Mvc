using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Financas.Domain.Entities;

namespace Financas.Infrastructure.Data.Mappings
{

    public class UsuarioMap : EntityTypeConfiguration<Usuario>
    {
        public UsuarioMap()
        {

            //MAPEAMENTO TABELA
            ToTable("tb_usuario", "dbo");

            //MAPEAMENTO CHAVE PRIMÁRIA
            HasKey(e => e.Id);

            Property(e => e.Id)
                .HasColumnName("usu_id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //MAPEAMENTO ATRIBUTOS
            Property(e => e.Nome)
                .HasColumnName("usu_nome")
                .IsRequired();

            Property(e => e.Email)
                .HasColumnName("usu_email")
                .IsRequired();

            Property(e => e.Senha)
                .HasColumnName("usu_senha")
                .IsRequired();

            Property(e => e.SaldoInicial)
                .HasColumnName("usu_saldoinicial")
                .IsRequired();

            Property(e => e.DataCadastro)
                .HasColumnName("usu_datacadastro")
                .IsRequired();

            Property(e => e.Ativo)
                .HasColumnName("usu_ativo")
                .IsRequired();

            //MAPEAMENTO CHAVES ESTRANGEIRAS

            //PROPRIEDADES NÃO MAPEADAS (BOOLEANOS E ENUMERADORES)


        }
    }
}
