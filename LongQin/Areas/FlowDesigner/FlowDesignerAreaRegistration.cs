using System.Web.Mvc;

namespace LongQin.Areas.FlowDesigner
{
    public class FlowDesignerAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "FlowDesigner";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "FlowDesigner_default",
                "FlowDesigner/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}