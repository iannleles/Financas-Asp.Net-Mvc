using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Financas.Domain.Entities;

namespace Financas.Infrastructure.Data.Mappings
{
    public class LancamentoMap : EntityTypeConfiguration<Lancamento>
    {
        public LancamentoMap()
        {

            //MAPEAMENTO TABELA
            ToTable("tb_lancamento", "dbo");

            //MAPEAMENTO CHAVE PRIMÁRIA
            HasKey(e => e.Id);

            Property(e => e.Id)
                .HasColumnName("lanc_id")
              .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //MAPEAMENTO ATRIBUTOS
            Property(e => e.Data)
                .HasColumnName("lanc_data")
                .IsRequired();

            Property(e => e.Valor)
                .HasColumnName("lanc_valor");

            Property(e => e.Descricao)
                .HasColumnName("lanc_desc");

            Property(e => e.Tipo)
                .HasColumnName("lanc_tipo");

            Property(e => e.UsuarioId)
                .HasColumnName("usu_id");

            Property(e => e.CategoriaId)
                .HasColumnName("cat_id");

            //MAPEAMENTO CHAVES ESTRANGEIRA
            HasRequired(e => e.Usuario) 
                .WithMany(pk => pk.Lancamentos) 
                .HasForeignKey(e => e.UsuarioId); 

            HasRequired(e => e.Categoria) 
                .WithMany(pk => pk.Lancamentos) 
                .HasForeignKey(e => e.CategoriaId);

            //PROPRIEDADES NÃO MAPEADAS (BOOLEANOS E ENUMERADORES)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      
        }
    }
}
