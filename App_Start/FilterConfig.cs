using System.Web;
using System.Web.Mvc;

namespace Project___Intro_To_Computer_Networking
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
