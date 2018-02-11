using Autofac;
using Autofac.Integration.Mvc;
using System.Web.Mvc;
using UserPayment.Models;
using UserPayment.Models.Services;

namespace UserPayment
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            // получаем экземпляр контейнера
            var builder = new ContainerBuilder();

            // регистрируем контроллер в текущей сборке
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // регистрируем сопоставление типов            
            builder.RegisterGeneric(typeof(Repository<>))
                .As(typeof(IRepository<>))
                .InstancePerLifetimeScope();

            builder.RegisterType<ModelStateWrapper>().As<IValidationDictionary>()
                .InstancePerLifetimeScope();

            builder.RegisterType<AccountService>().As<IAccountService>()
                .InstancePerLifetimeScope();

            // создаем новый контейнер с теми зависимостями, которые определены выше
            var container = builder.Build();

            // установка сопоставителя зависимостей
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}