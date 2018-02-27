using System.Collections.Generic;
using Financas.Domain.Contracts.Repositories;
using Financas.Domain.Dtos;
using Financas.Domain.Entities;
using Financas.Infrastructure.Data.Contracts;
using Financas.Infrastructure.Data.Repositories.Common;
using System.Linq;
using System;


namespace Financas.Infrastructure.Data.EF.Repositorio
{

    public class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(IDbContext context)
            : base(context)
        {

        }


        public UsuarioDTO ObterUsuarioPorEmailSenha(string email, string senha)
        {
            try
            {

                string sqlQuery = string.Format("EXEC sp_GetUsuarioByEmailSenha '{0}','{1}'", email, senha);
                IEnumerable<UsuarioDTO> data = this.dbContext.SQLQuery<UsuarioDTO>(sqlQuery);

                return data.FirstOrDefault();

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

    }
}
