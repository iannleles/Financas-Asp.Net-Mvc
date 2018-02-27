using System;

namespace Financas.Domain.Dtos
{
    public class UsuarioDTO
    {
        public int? Id { get; set; }
        public String Nome { get; set; }
        public String Email { get; set; }
        public String Senha { get; set; }
        public Decimal SaldoInicial { get; set; }
        public DateTime DataCadastro { get; set; }
        public Boolean Ativo { get; private set; }

    }
}

