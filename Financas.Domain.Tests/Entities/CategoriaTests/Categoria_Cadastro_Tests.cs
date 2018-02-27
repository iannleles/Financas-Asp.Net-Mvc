using Financas.Common.Tests;
using Financas.Domain.Contracts.Repositories;
using Financas.Domain.Dtos;
using Financas.Domain.Entidade.Validators;
using Financas.Domain.Entities;
using Financas.Domain.Resources;
using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Financas.Domain.Tests.Entities.CategoriaTests
{
    [TestClass]
    public class Categoria_Cadastro_Tests : FinancasUnitTest
    {

        #region Domínio e Transporte

        private CategoriaDTO CategoriaDTOCadastro;
        private Categoria categoria;

        #endregion

        #region Validadores e Mocks

        private CategoriaValidator validator;
        private Mock<ICategoriaRepository> mockCategoriaRepository;
        private Mock<IDespesaRepository> mockDespesaRepository;
        private Mock<ILancamentoRepository> mockLancamentoRepository;
        private Mock<IOrcamentoRepository> mockOrcamentoRepository;

        #endregion

        public Categoria_Cadastro_Tests()
        {
            mockCategoriaRepository = new Mock<ICategoriaRepository>();
            mockDespesaRepository = new Mock<IDespesaRepository>();
            mockLancamentoRepository = new Mock<ILancamentoRepository>();
            mockOrcamentoRepository = new Mock<IOrcamentoRepository>();

            validator = new CategoriaValidator(
                mockCategoriaRepository.Object,
                mockDespesaRepository.Object,
                mockLancamentoRepository.Object,
                mockOrcamentoRepository.Object);
        }



        [ClassInitialize()]
        public static void InicializarTestesCategoria(TestContext testContext)
        {

        }

        [TestInitialize()]
        public void InicializarTesteUnitario()
        {
            CategoriaDTOCadastro = new CategoriaDTO();

            CategoriaDTOCadastro.UsuarioId = 1;
            CategoriaDTOCadastro.CategoriaPaiId = 1;
            CategoriaDTOCadastro.Nome = "Categoria";
            CategoriaDTOCadastro.Tipo = "R";

            MockarNomeNaoDuplicado();

            submeterOperacao = () =>
            {
                categoria = Categoria.Cadastrar(
                   CategoriaDTOCadastro, validator);
            };
        }

        #region Preparação de Mocks

        private void MockarNomeNaoDuplicado()
        {
            mockCategoriaRepository
                .Setup(m => m.NomeNaoCadastrado(CategoriaDTOCadastro.Id, CategoriaDTOCadastro.Nome, CategoriaDTOCadastro.UsuarioId, CategoriaDTOCadastro.Tipo))
                .Returns(true);
        }

        private void MockarNomeDuplicado()
        {
            mockCategoriaRepository
                .Setup(m => m.NomeNaoCadastrado(CategoriaDTOCadastro.Id, CategoriaDTOCadastro.Nome, CategoriaDTOCadastro.UsuarioId, CategoriaDTOCadastro.Tipo))
                .Returns(false);
        }

        #endregion

        #region Testes de Exceção

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Categoria_Cadastro_Exception_UsuarioId_Nulo()
        {
            CategoriaDTOCadastro.UsuarioId = null;
            TestException(submeterOperacao, CategoriaResources.UsuarioObrigatorio);
        }


        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Categoria_Cadastro_Exception_Nome_Nulo()
        {
            CategoriaDTOCadastro.Nome = null;
            MockarNomeNaoDuplicado();
            TestException(submeterOperacao, CategoriaResources.NomeObrigatorio);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Categoria_Cadastro_Exception_Nome_MinLength_5()
        {
            CategoriaDTOCadastro.Nome = "A";
            MockarNomeNaoDuplicado();
            TestException(submeterOperacao, CategoriaResources.NomeTamanhoInvalido);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Categoria_Cadastro_Exception_Nome_MaxLength_50()
        {
            CategoriaDTOCadastro.Nome = "ABC ".PadRight(51, 'X');
            MockarNomeNaoDuplicado();
            TestException(submeterOperacao, CategoriaResources.NomeTamanhoInvalido);
        }



        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Categoria_Cadastro_Exception_Tipo_Nulo()
        {
            CategoriaDTOCadastro.Tipo = null;
            TestException(submeterOperacao, CategoriaResources.TipoObrigatorio);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Categoria_Cadastro_Exception_Tipo_MaxLength_1()
        {
            CategoriaDTOCadastro.Tipo = "RECEITA";
            TestException(submeterOperacao, CategoriaResources.TipoTamanhoInvalido);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Categoria_Cadastro_Exception_Nome_Duplicado()
        {
            CategoriaDTOCadastro.Nome = "Titulo Cadastrado";
            MockarNomeDuplicado();
            TestException(submeterOperacao, CategoriaResources.NomeDuplicado);
        }


        #endregion

        #region Testes de Sucesso


        [TestMethod]
        public void Categoria_Cadastro_Success_UsuarioId()
        {
            submeterOperacao();

            Assert.AreEqual(
                CategoriaDTOCadastro.UsuarioId,
                categoria.UsuarioId);
        }


        [TestMethod]
        public void Categoria_Cadastro_Success_Nome()
        {
            submeterOperacao();

            Assert.AreEqual(
                CategoriaDTOCadastro.Nome,
                categoria.Nome);
        }

        [TestMethod]
        public void Categoria_Cadastro_Success_Tipo()
        {
            submeterOperacao();

            Assert.AreEqual(
                CategoriaDTOCadastro.Tipo,
                categoria.Tipo);
        }



        #endregion
    }
}
