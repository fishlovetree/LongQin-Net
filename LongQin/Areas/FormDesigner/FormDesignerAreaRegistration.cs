using System.Web.Mvc;

namespace LongQin.Areas.FormDesigner
{
    public class FormDesignerAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "FormDesigner";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "FormDesigner_default",
                "FormDesigner/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}