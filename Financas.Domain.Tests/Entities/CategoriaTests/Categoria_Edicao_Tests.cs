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
    public class Categoria_Edicao_Tests : FinancasUnitTest
    {

        #region Domínio e Transporte

        private CategoriaDTO CategoriaDTOEdicao;

        #endregion

        #region Validadores e Mocks

        private CategoriaValidator validator;
        private Mock<ICategoriaRepository> mockCategoriaRepository;
        private Mock<IDespesaRepository> mockDespesaRepository;
        private Mock<ILancamentoRepository> mockLancamentoRepository;
        private Mock<IOrcamentoRepository> mockOrcamentoRepository;
        private Mock<Categoria> mockCategoria;

        #endregion

        public Categoria_Edicao_Tests()
        {
            mockCategoriaRepository = new Mock<ICategoriaRepository>();
            mockDespesaRepository = new Mock<IDespesaRepository>();
            mockLancamentoRepository = new Mock<ILancamentoRepository>();
            mockOrcamentoRepository = new Mock<IOrcamentoRepository>();
            mockCategoria = new Mock<Categoria>();

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
            CategoriaDTOEdicao = new CategoriaDTO();

            CategoriaDTOEdicao.Id = 1;
            CategoriaDTOEdicao.UsuarioId = 1;
            CategoriaDTOEdicao.CategoriaPaiId = 1;
            CategoriaDTOEdicao.Nome = "Categoria";
            CategoriaDTOEdicao.Tipo = "R";

            MockarNomeNaoDuplicado();
            MockarUsuarioValido();

            submeterOperacao = () =>
            {
                MockarConsultaCategoria();
                mockCategoria.Object.Editar(CategoriaDTOEdicao, validator);
            };

        }

        #region Preparação de Mocks

        private void MockarConsultaCategoria()
        {
             mockCategoriaRepository
            .Setup(m => m.GetById(CategoriaDTOEdicao.Id))
            .Returns(mockCategoria.Object);
        }

        private void MockarNomeNaoDuplicado()
        {
            mockCategoriaRepository
                .Setup(m => m.NomeNaoCadastrado(CategoriaDTOEdicao.Id, CategoriaDTOEdicao.Nome, CategoriaDTOEdicao.UsuarioId, CategoriaDTOEdicao.Tipo))
                .Returns(true);
        }

        private void MockarNomeDuplicado()
        {
            mockCategoriaRepository
                .Setup(m => m.NomeNaoCadastrado(CategoriaDTOEdicao.Id, CategoriaDTOEdicao.Nome, CategoriaDTOEdicao.UsuarioId, CategoriaDTOEdicao.Tipo))
                .Returns(false);
        }

        private void MockarUsuarioValido()
        {
            mockCategoriaRepository
                .Setup(m => m.UsuarioCadastroValido(CategoriaDTOEdicao.Id, CategoriaDTOEdicao.UsuarioId))
                .Returns(true);
        }

        private void MockarUsuarioInvalido()
        {
            mockCategoriaRepository
                .Setup(m => m.UsuarioCadastroValido(CategoriaDTOEdicao.Id, CategoriaDTOEdicao.UsuarioId))
                .Returns(false);
        }

        #endregion

        #region Testes de Exceção

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Categoria_Edicao_Exception_Id_Nulo()
        {
            CategoriaDTOEdicao.Id = null;
            MockarNomeNaoDuplicado();
            MockarUsuarioValido();
            TestException(submeterOperacao, CategoriaResources.IdObrigatorio);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Categoria_Edicao_Exception_UsuarioId_Nulo()
        {
            CategoriaDTOEdicao.UsuarioId = null;
            MockarUsuarioValido();
            TestException(submeterOperacao, CategoriaResources.UsuarioObrigatorio);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Categoria_Edicao_Exception_UsuarioId_Invalido()
        {
            MockarNomeNaoDuplicado();
            MockarUsuarioInvalido();
            TestException(submeterOperacao, CategoriaResources.UsuarioCadastroInvalido);
        }



        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Categoria_Edicao_Exception_Nome_Nulo()
        {
            CategoriaDTOEdicao.Nome = null;
            MockarNomeNaoDuplicado();
            MockarUsuarioValido();
            TestException(submeterOperacao, CategoriaResources.NomeObrigatorio);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Categoria_Edicao_Exception_Nome_MinLength_5()
        {
            CategoriaDTOEdicao.Nome = "A";
            MockarNomeNaoDuplicado();
            MockarUsuarioValido();
            TestException(submeterOperacao, CategoriaResources.NomeTamanhoInvalido);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Categoria_Edicao_Exception_Nome_MaxLength_50()
        {
            CategoriaDTOEdicao.Nome = "ABC ".PadRight(51, 'X');
            MockarNomeNaoDuplicado();
            MockarUsuarioValido();
            TestException(submeterOperacao, CategoriaResources.NomeTamanhoInvalido);
        }



        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Categoria_Edicao_Exception_Tipo_Nulo()
        {
            CategoriaDTOEdicao.Tipo = null;
            MockarUsuarioValido();
            TestException(submeterOperacao, CategoriaResources.TipoObrigatorio);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Categoria_Edicao_Exception_Tipo_MaxLength_1()
        {
            CategoriaDTOEdicao.Tipo = "RECEITA";
            MockarNomeNaoDuplicado();
            MockarUsuarioValido();
            TestException(submeterOperacao, CategoriaResources.TipoTamanhoInvalido);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Categoria_Edicao_Exception_Nome_Duplicado()
        {
            MockarUsuarioValido();
            MockarNomeDuplicado();

            TestException(submeterOperacao, CategoriaResources.NomeDuplicado);

        }

        #endregion

        #region Testes de Sucesso

        public void Categoria_Edicao_Success_Id()
        {
            submeterOperacao();

            Assert.AreEqual(
                CategoriaDTOEdicao.Id,
                mockCategoria.Object.Id);
        }

        [TestMethod]
        public void Categoria_Edicao_Success_UsuarioId()
        {
            submeterOperacao();

            Assert.AreEqual(
                CategoriaDTOEdicao.UsuarioId,
                mockCategoria.Object.UsuarioId);
        }


        [TestMethod]
        public void Categoria_Edicao_Success_Nome()
        {
            submeterOperacao();

            Assert.AreEqual(
                CategoriaDTOEdicao.Nome,
                mockCategoria.Object.Nome);
        }

        [TestMethod]
        public void Categoria_Edicao_Success_Tipo()
        {
            submeterOperacao();

            Assert.AreEqual(
                CategoriaDTOEdicao.Tipo,
                mockCategoria.Object.Tipo);
        }



        #endregion
    }
}
