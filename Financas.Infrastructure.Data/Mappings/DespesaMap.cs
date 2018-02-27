using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Financas.Domain.Entities;

namespace Financas.Infrastructure.Data.Mappings
{
    public class DespesaMap : EntityTypeConfiguration<Despesa>
    {
        public DespesaMap()
        {

            //MAPEAMENTO TABELA
            ToTable("tb_despesa", "dbo");

            //MAPEAMENTO CHAVE PRIMÁRIA
            HasKey(e => e.Id);

            Property(e => e.Id)
                .HasColumnName("desp_id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //MAPEAMENTO ATRIBUTOS
            Property(e => e.Descricao)
                .HasColumnName("desp_desc");

            Property(e => e.Valor)
                .HasColumnName("desp_valor");

            Property(e => e.Vencto)
                .HasColumnName("desp_vencto");

            Property(e => e.UsuarioId)
                .HasColumnName("usu_id");

            Property(e => e.CategoriaId)
                .HasColumnName("cat_id");



            //MAPEAMENTO CHAVES ESTRANGEIRAS

            HasRequired(e => e.Usuario) 
                .WithMany(pk => pk.Despesas) 
                .HasForeignKey(e => e.UsuarioId); 

            HasRequired(e => e.Categoria) 
                .WithMany(pk => pk.Despesas) 
                .HasForeignKey(e => e.CategoriaId); 

            //PROPRIEDADES NÃO MAPEADAS (BOOLEANOS E ENUMERADORES)

        }
    }
}
