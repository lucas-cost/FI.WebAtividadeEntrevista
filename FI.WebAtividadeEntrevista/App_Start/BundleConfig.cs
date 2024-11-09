using System.Web;
using System.Web.Optimization;

namespace FI.WebAtividadeEntrevista
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/jtable").Include(
                      "~/Scripts/jtable/jquery.jtable.min.js",
                      "~/Scripts/jtable/localization/jquery.jtable.pt-BR.js"));

            bundles.Add(new ScriptBundle("~/bundles/clientes").Include(
                      "~/Scripts/Clientes/FI.Clientes.js"));

            bundles.Add(new ScriptBundle("~/bundles/listClientes").Include(
                      "~/Scripts/Clientes/FI.ListClientes.js"));

            bundles.Add(new ScriptBundle("~/bundles/altClientes").Include(
                      "~/Scripts/Clientes/FI.AltClientes.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/jtable").Include(
                      "~/Scripts/jtable/themes/metro/darkgray/jtable.css"));
        }
    }
}
