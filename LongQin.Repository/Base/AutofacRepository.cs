using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongQin.Repository
{
    public class AutofacRepository
    {
        static IContainer container = null;

        public static T Resolve<T>()
        {
            try
            {
                if (container == null)
                {
                    Register();
                }
            }
            catch (Exception ex)
            {
                throw new System.Exception("IOC实例化出错!" + ex.Message);
            }
            return container.Resolve<T>();
        }

        public static void Register()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();
            builder.RegisterType<MenuRepository>().As<IMenuRepository>().InstancePerLifetimeScope();
            builder.RegisterType<RoleRepository>().As<IRoleRepository>().InstancePerLifetimeScope();
            builder.RegisterType<OrganizationRepository>().As<IOrganizationRepository>().InstancePerLifetimeScope();
            builder.RegisterType<DepartmentRepository>().As<IDepartmentRepository>().InstancePerLifetimeScope();
            builder.RegisterType<PositionRepository>().As<IPositionRepository>().InstancePerLifetimeScope();
            builder.RegisterType<LogRepository>().As<ILogRepository>().InstancePerLifetimeScope();
            builder.RegisterType<FormDesignerRepository>().As<IFormDesignerRepository>().InstancePerLifetimeScope();
            builder.RegisterType<FlowDesignerRepository>().As<IFlowDesignerRepository>().InstancePerLifetimeScope();
            builder.RegisterType<WorkFlowRepository>().As<IWorkFlowRepository>().InstancePerLifetimeScope();
            builder.RegisterType<NoticeRepository>().As<INoticeRepository>().InstancePerLifetimeScope();
            builder.RegisterType<TableDesignerRepository>().As<ITableDesignerRepository>().InstancePerLifetimeScope();
            container = builder.Build();
        }
    }
}
