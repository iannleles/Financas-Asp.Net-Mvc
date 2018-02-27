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
    public class Categoria_Exclusao_Tests : FinancasUnitTest
    {

        #region Domínio e Transporte

        private CategoriaDTO CategoriaDTOExclusao;

        #endregion

        #region Validadores e Mocks

        private CategoriaValidator validator;
        private Mock<ICategoriaRepository> mockCategoriaRepository;
        private Mock<IDespesaRepository> mockDespesaRepository;
        private Mock<ILancamentoRepository> mockLancamentoRepository;
        private Mock<IOrcamentoRepository> mockOrcamentoRepository;
        private Mock<Categoria> mockCategoria;

        #endregion

        public Categoria_Exclusao_Tests()
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
            CategoriaDTOExclusao = new CategoriaDTO();
            CategoriaDTOExclusao.Id = 1;
            CategoriaDTOExclusao.UsuarioId = 1;


            submeterOperacao = () =>
            {
                MockarConsultaCategoria();
                mockCategoria.Object.Excluir(validator);
            };

        }

        #region Preparação de Mocks

        private void MockarConsultaCategoria()
        {
            mockCategoriaRepository
           .Setup(m => m.GetById(CategoriaDTOExclusao.Id))
           .Returns(mockCategoria.Object);
        }

        private void MockarUsuarioValido()
        {
            mockCategoriaRepository
                .Setup(m => m.UsuarioCadastroValido(mockCategoria.Object.Id, mockCategoria.Object.UsuarioId))
                .Returns(true);
        }

        private void MockarUsuarioInvalido()
        {
            mockCategoriaRepository
                .Setup(m => m.UsuarioCadastroValido(mockCategoria.Object.Id, mockCategoria.Object.UsuarioId))
                .Returns(false);
        }

        private void MockarIdValidoExiste()
        {
            mockCategoriaRepository
                .Setup(m => m.IdCategoriaExiste(mockCategoria.Object.Id))
                .Returns(true);
        }

        private void MockarIdInvalidoNaoExiste()
        {
            mockCategoriaRepository
                .Setup(m => m.IdCategoriaExiste(mockCategoria.Object.Id))
                .Returns(false);
        }

        private void MockarCategoriasNaoVinculadas()
        {
            mockCategoriaRepository
                .Setup(m => m.CategoriasPaiNaoVinculadas(mockCategoria.Object.Id))
                .Returns(true);
        }

        private void MockarCategoriasVinculadas()
        {
            mockCategoriaRepository
                .Setup(m => m.CategoriasPaiNaoVinculadas(mockCategoria.Object.Id))
                .Returns(false);
        }

        private void MockarDespesasNaoVinculadas()
        {
            mockDespesaRepository
                .Setup(m => m.CategoriasNaoVinculadas(mockCategoria.Object.Id))
                .Returns(true);
        }

        private void MockarDespesasVinculadas()
        {
            mockDespesaRepository
                .Setup(m => m.CategoriasNaoVinculadas(mockCategoria.Object.Id))
                .Returns(false);
        }

        private void MockarLancamentosNaoVinculados()
        {
            mockLancamentoRepository
                .Setup(m => m.CategoriasNaoVinculadas(mockCategoria.Object.Id))
                .Returns(true);
        }

        private void MockarLancamentosVinculados()
        {
            mockLancamentoRepository
                .Setup(m => m.CategoriasNaoVinculadas(mockCategoria.Object.Id))
                .Returns(false);
        }

        private void MockarOrcamentosNaoVinculados()
        {
            mockOrcamentoRepository
                .Setup(m => m.CategoriasNaoVinculadas(mockCategoria.Object.Id))
                .Returns(true);
        }

        private void MockarOrcamentosVinculados()
        {
            mockOrcamentoRepository
                .Setup(m => m.CategoriasNaoVinculadas(mockCategoria.Object.Id))
                .Returns(false);
        }
        
        #endregion

        #region Testes de Exceção

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Categoria_Exclusao_Exception_Id_NaoExiste()
        {
            MockarIdInvalidoNaoExiste();
            MockarUsuarioValido();
            MockarCategoriasNaoVinculadas();
            MockarDespesasNaoVinculadas();
            MockarLancamentosNaoVinculados();
            MockarOrcamentosNaoVinculados();

            TestException(submeterOperacao, CategoriaResources.CategoriaNaoEncontrada);
        }


        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Categoria_Exclusao_Exception_UsuarioId_Invalido()
        {
            MockarIdValidoExiste();
            MockarUsuarioInvalido();
            MockarCategoriasNaoVinculadas();
            MockarDespesasNaoVinculadas();
            MockarLancamentosNaoVinculados();
            MockarOrcamentosNaoVinculados();

            TestException(submeterOperacao, CategoriaResources.UsuarioCadastroInvalido);
        }


        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Categoria_Exclusao_Exception_Id_CategoriasVinculadas()
        {
            MockarIdValidoExiste();
            MockarUsuarioValido();
            MockarCategoriasVinculadas();
            MockarDespesasNaoVinculadas();
            MockarLancamentosNaoVinculados();
            MockarOrcamentosNaoVinculados();

            TestException(submeterOperacao, CategoriaResources.CategoriaVinculadas);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Categoria_Exclusao_Exception_Id_DespesaVinculadas()
        {
            MockarIdValidoExiste();
            MockarUsuarioValido();
            MockarCategoriasNaoVinculadas();
            MockarDespesasVinculadas();
            MockarLancamentosNaoVinculados();
            MockarOrcamentosNaoVinculados();

            TestException(submeterOperacao, CategoriaResources.DespesasVinculadas);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Categoria_Exclusao_Exception_Id_LancamentosVinculadas()
        {
            MockarIdValidoExiste();
            MockarUsuarioValido();
            MockarCategoriasNaoVinculadas();
            MockarDespesasNaoVinculadas();
            MockarLancamentosVinculados();
            MockarOrcamentosNaoVinculados();

            TestException(submeterOperacao, CategoriaResources.LancamentosVinculados);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Categoria_Exclusao_Exception_Id_OrcamentosVinculados()
        {
            MockarIdValidoExiste();
            MockarUsuarioValido();
            MockarCategoriasNaoVinculadas();
            MockarDespesasNaoVinculadas();
            MockarLancamentosNaoVinculados();
            MockarOrcamentosVinculados();

            TestException(submeterOperacao, CategoriaResources.OrcamentosVinculados);
        }

        #endregion

        #region Testes de Sucesso

        #endregion

    }
}
