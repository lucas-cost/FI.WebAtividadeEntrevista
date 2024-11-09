using System.Web;
using System.Web.Mvc;

namespace FI.WebAtividadeEntrevista
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
