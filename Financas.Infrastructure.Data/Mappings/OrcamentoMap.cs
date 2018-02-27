using Financas.Domain.Entidade;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Financas.Domain.Entities;

namespace Financas.Infrastructure.Data.Mappings
{
    public class OrcamentoMap : EntityTypeConfiguration<Orcamento>
    {
        public OrcamentoMap()
        {

            //MAPEAMENTO TABELA
            ToTable("tb_orcamento", "dbo");

            //MAPEAMENTO CHAVE PRIMÁRIA
            HasKey(e => e.Id);

            Property(e => e.Id)
                .HasColumnName("orc_id")
              .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //MAPEAMENTO ATRIBUTOS
            Property(e => e.Tipo)
                .HasColumnName("orc_tipo");

            Property(e => e.Mes)
                .HasColumnName("orc_mes");

            Property(e => e.Ano)
                .HasColumnName("orc_ano");

            Property(e => e.Tipo)
                .HasColumnName("orc_tipo");

            Property(e => e.Valor)
                .HasColumnName("orc_valor");

            Property(e => e.UsuarioId)
                .HasColumnName("usu_id");

            Property(e => e.CategoriaId)
                .HasColumnName("cat_id");

            //MAPEAMENTO CHAVES ESTRANGEIRAS


            HasRequired(e => e.Usuario) 
                .WithMany(pk => pk.Orcamentos) 
                .HasForeignKey(e => e.UsuarioId); 

            HasRequired(e => e.Categoria) 
                .WithMany(pk => pk.Orcamentos) 
                .HasForeignKey(e => e.CategoriaId); 


            //PROPRIEDADES NÃO MAPEADAS (BOOLEANOS E ENUMERADORES)

        }
    }
}
