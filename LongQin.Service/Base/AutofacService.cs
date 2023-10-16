using Autofac;
using LongQin.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LongQin.Service.Base
{
    public class AutofacService
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
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            builder.RegisterType<MenuService>().As<IMenuService>().InstancePerLifetimeScope();
            builder.RegisterType<RoleService>().As<IRoleService>().InstancePerLifetimeScope();
            builder.RegisterType<OrganizationService>().As<IOrganizationService>().InstancePerLifetimeScope();
            builder.RegisterType<DepartmentService>().As<IDepartmentService>().InstancePerLifetimeScope();
            builder.RegisterType<PositionService>().As<IPositionService>().InstancePerLifetimeScope();
            builder.RegisterType<LogService>().As<ILogService>().InstancePerLifetimeScope();
            builder.RegisterType<FormDesignerService>().As<IFormDesignerService>().InstancePerLifetimeScope();
            builder.RegisterType<FlowDesignerService>().As<IFlowDesignerService>().InstancePerLifetimeScope();
            builder.RegisterType<WorkFlowService>().As<IWorkFlowService>().InstancePerLifetimeScope();
            builder.RegisterType<NoticeService>().As<INoticeService>().InstancePerLifetimeScope();
            builder.RegisterType<TableDesignerService>().As<ITableDesignerService>().InstancePerLifetimeScope();
            container = builder.Build();
        }
    }
}
