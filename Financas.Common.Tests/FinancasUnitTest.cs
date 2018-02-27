using System;
using System.Linq;
using System.Text;
using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Financas.Common.Tests
{
    public class FinancasUnitTest
    {
        private TestContext testContextInstance;

        protected Action submeterOperacao;

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        public void TestException(Action action, string mensagemEsperada)
        {
            try
            {
                action.Invoke();
            }
            catch (ValidationException erro)
            {
                if (erro.Errors.Count() != 1)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine();
                    sb.AppendLine("TESTE INVÁLIDO. MAIS DE UMA EXCEPTION ENCONTRADA.");
                    foreach (var itemErro in erro.Errors)
                    {
                        sb.AppendLine(itemErro.ErrorMessage);
                    }
                    throw new Exception(sb.ToString());
                }
                if (erro.Errors.Count() == 1)
                {
                    string mensagemRecebida = erro.Errors.FirstOrDefault().ErrorMessage;

                    if (mensagemEsperada != mensagemRecebida)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine();
                        sb.AppendLine("TESTE INVÁLIDO. MENSAGEM INVÁLIDA.");
                        sb.AppendLine("MENSAGEM ESPERADA: " + mensagemEsperada);
                        sb.AppendLine("MENSAGEM RECEBIDA: " + mensagemRecebida);
                        throw new Exception(sb.ToString());
                    }
                    else
                    {
                        TestContext.WriteLine(erro.Message);
                        throw (erro);
                    }
                }
                else
                {
                    TestContext.WriteLine(erro.Message);
                    throw (erro);
                }
            }
            catch (Exception erro)
            {
                throw (erro);
            }
        }
    }
}
