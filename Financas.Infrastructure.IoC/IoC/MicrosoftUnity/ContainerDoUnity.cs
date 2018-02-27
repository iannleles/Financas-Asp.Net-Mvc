using Financas.Application.Services;
using Financas.Domain.Contracts.Repositories;
using Financas.Domain.Contracts.Repositories.Common;
using Financas.Domain.Contracts.Services;
using Financas.Infrastructure.Data.Contexts;
using Financas.Infrastructure.Data.Contracts;
using Financas.Infrastructure.Data.EF.Repositorio;
using Financas.Infrastructure.Data.Repositories;
using Financas.Infrastructure.Data.Repositories.Common;
using Microsoft.Practices.Unity;

namespace Financas.Infrastructure.IoC.IoC.MicrosoftUnity
{
    public static class ContainerDoUnity
    {

        #region Atributos
        static IUnityContainer container;
        #endregion

        #region Construtores
        static ContainerDoUnity()
        {
            //CriaContainer();
        }

        #endregion

        #region Propriedades
        public static IUnityContainer Container
        {
            get
            {
                return container;
            }

            set
            {
                container = value;
            }
        }
        #endregion

        #region Injeção de Dependências
        public static void CriaContainer(IUnityContainer container)
        {

            //Resolução de Contexto
            container.RegisterType<IDbContext, FinancasContext>(new HierarchicalLifetimeManager());

            //Resolução de Unidades de Trabalho
            container.RegisterType<IUnitOfWork, UnitOfWork>(new HierarchicalLifetimeManager());

            //Resolução de Repositórios
            container.RegisterType<IUsuarioRepository, UsuarioRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ICategoriaRepository, CategoriaRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IDespesaRepository, DespesaRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ILancamentoRepository, LancamentoRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IOrcamentoRepository, OrcamentoRepository>(new HierarchicalLifetimeManager());

            //Resolução de Aplicação            
            container.RegisterType<IUsuarioAppService, UsuarioAppService>(new HierarchicalLifetimeManager());
            container.RegisterType<IAutenticacaoAppService, AutenticacaoAppService>(new HierarchicalLifetimeManager());
            container.RegisterType<ICategoriaAppService, CategoriaAppService>(new HierarchicalLifetimeManager());
            container.RegisterType<ILancamentoAppService, LancamentoAppService>(new HierarchicalLifetimeManager());
            container.RegisterType<IDespesaAppService, DespesaAppService>(new HierarchicalLifetimeManager());
            container.RegisterType<IOrcamentoAppService, OrcamentoAppService>(new HierarchicalLifetimeManager());
            container.RegisterType<IDashboardAppService, DashboardAppService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRelatorioAppService, RelatorioAppService>(new HierarchicalLifetimeManager());

        }

        public static void InicializaContainer(IUnityContainer containerInjetado)
        {
            CriaContainer(containerInjetado);
        }

        #endregion

    }
}



