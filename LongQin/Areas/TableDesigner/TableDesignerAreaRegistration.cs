using System.Web.Mvc;

namespace LongQin.Areas.TableDesigner
{
    public class TableDesignerAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "TableDesigner";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "TableDesigner_default",
                "TableDesigner/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}