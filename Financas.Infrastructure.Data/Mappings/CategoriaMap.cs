using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Financas.Domain.Entities;

namespace Financas.Infrastructure.Data.Mappings
{
    public class CategoriaMap : EntityTypeConfiguration<Categoria>
    {
        public CategoriaMap()
        {

            //MAPEAMENTO TABELA
            ToTable("tb_categoria", "dbo");

            //MAPEAMENTO CHAVE PRIMÁRIA
            HasKey(e => e.Id);

            Property(e => e.Id)
                .HasColumnName("cat_id")
              .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //MAPEAMENTO ATRIBUTOS
            Property(e => e.Nome)
                .HasColumnName("cat_nome")
                .HasMaxLength(255)
                .IsRequired();

            Property(e => e.Tipo)
                .HasColumnName("cat_tipo");

            Property(e => e.CategoriaPaiId)
                .HasColumnName("cat_id_pai");

            Property(e => e.UsuarioId)
                .HasColumnName("usu_id");


            //MAPEAMENTO CHAVES ESTRANGEIRAS

            HasOptional(e => e.CategoriaPai) 
                .WithMany(pk => pk.CategoriaFilhas) 
                .HasForeignKey(fk => fk.CategoriaPaiId);


            HasRequired(e => e.Usuario)
                .WithMany(pk => pk.Categorias) 
                .HasForeignKey(e => e.UsuarioId); 

            //PROPRIEDADES NÃO MAPEADAS (BOOLEANOS E ENUMERADORES)

        }


    }
}
