using System.Web;
using System.Web.Optimization;

namespace SIEE.WEB.UI
{
    public class BundleConfig
    {
        //Дополнительные сведения об объединении см. по адресу: http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/Libs/jquery-1.10.2.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/Libs/jquery.validate*"));

            // Используйте версию Modernizr для разработчиков, чтобы учиться работать. Когда вы будете готовы перейти к работе,
            // используйте средство сборки на сайте http://modernizr.com, чтобы выбрать только нужные тесты.
            /*bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/Libs/modernizr-*"));*/

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/Libs/bootstrap.min.js",
            //          "~/Scripts/Libs/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/Libs/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/Libs/bootstrap.min.css",
                      "~/Content/Site/site.css"));
        }
    }
}
