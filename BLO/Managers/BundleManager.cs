using System.Web;
using System.Web.Optimization;
using BLO.Objects;

namespace BLO.Managers
{
    public class BundleManager
    {
        public static IHtmlString Modernizr { get { return Scripts.Render("~/bundles/modernizr"); } }
        public static IHtmlString Jquery { get { return Scripts.Render("~/bundles/jquery"); } }
        public static IHtmlString JqueryUI { get { return Scripts.Render("~/bundles/jqueryui"); } }
        public static IHtmlString JqueryUICss { get { return Styles.Render("~/Content/jqueryui"); } }
        public static IHtmlString Mobile { get { return Scripts.Render("~/bundles/mobile"); } }
        public static IHtmlString MobileCss { get { return Styles.Render("~/Content/mobileCss"); } }
        public static IHtmlString CommonCss { get { return Styles.Render("~/Content/css"); } }
        public static IHtmlString Common { get { return Scripts.Render("~/bundles/common"); } }
        public static IHtmlString zoomie { get { return Scripts.Render("~/bundles/zoomie"); } }
        public static IHtmlString zoomieCss { get { return Styles.Render("~/Content/zoomie"); } }
        public static IHtmlString flexSlider { get { return Scripts.Render("~/bundles/flex"); } }
        public static IHtmlString flexSliderCss { get { return Styles.Render("~/Content/flex"); } }
        public static IHtmlString tableCss { get { return Styles.Render("~/Content/tableCss"); } }
        public static IHtmlString UploadScript { get { return Scripts.Render("~/bundles/dropzone"); } }
        public static IHtmlString UploadCss { get { return Styles.Render("~/Content/dropzone"); } }
        public static IHtmlString Print { get { return Scripts.Render("~/bundles/Print"); } }
        public static IHtmlString Rem { get { return Scripts.Render("~/bundles/rem"); } }
        public static IHtmlString IE9 { get { return Scripts.Render("~/bundles/ie9"); } }
        public static IHtmlString LoginCss { get { return Styles.Render("~/Content/loginCss"); } }

        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-2.*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-1.11.4.js"));
            bundles.Add(new StyleBundle("~/Content/jqueryui").Include("~/Content/themes/base/all.css"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/font-awesome/4.4.0/css/font-awesome.min.css",
                "~/Content/jquery.qtip.css",
                "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/loginCss").Include("~/Content/login.css"));

            bundles.Add(new StyleBundle("~/Content/mobileCss").Include(
                "~/Content/normalize.css",
                "~/Content/foundation*",
                "~/Content/jquery.datetimepicker.css"));
            bundles.Add(new ScriptBundle("~/bundles/mobile").Include(
                        "~/Scripts/foundation/foundation.js",
                        "~/Scripts/foundation/foundation.abide.js",
                        "~/Scripts/foundation/foundation.accordian.js",
                        "~/Scripts/foundation/foundation.alert.js",
                        "~/Scripts/foundation/foundation.clearing.js",
                        "~/Scripts/foundation/foundation.dropdown.js",
                        "~/Scripts/foundation/foundation.equalizer.js",
                        "~/Scripts/foundation/foundation.interchange.js",
                        "~/Scripts/foundation/foundation.topbar.js",
                        "~/Scripts/foundation/foundation.offcanvas.js",
                        "~/Scripts/foundation/foundation.reveal.js",
                        "~/Scripts/foundation/foundation.tab.js",
                        "~/Scripts/foundation/foundation.tooltip.js",
                        "~/Scripts/jquery.datetimepicker.js"));

            bundles.Add(new ScriptBundle("~/bundles/common").Include(
                        "~/Scripts/json2.js",
                        "~/Scripts/date.js",
                        "~/Scripts/jquery.numeric.js",
                        "~/Scripts/jquery.keycodes.js",
                        "~/Scripts/JsMessage_en.js",
                        "~/Scripts/jquery.lazy.js",
                        "~/Scripts/jquery.qtip.js",
                        "~/Scripts/autocomplete.js",
                        "~/Scripts/common.js",
                        "~/Scripts/mainscript.js"));

            bundles.Add(new StyleBundle("~/Content/plupload").Include("~/Content/jquerypluploadqueue/css/jquery.plupload.queue.css"));
            bundles.Add(new ScriptBundle("~/bundles/plupload").Include(
                "~/Scripts/jquerypluploadqueue/jquery.plupload.queue.js",
                "~/Scripts/plupload.full.min.js"));

            bundles.Add(new StyleBundle("~/Content/zoomie").Include("~/Content/jquery.zoomie.css"));
            bundles.Add(new ScriptBundle("~/bundles/zoomie").Include("~/Scripts/jquery.zoomie.js"));

            bundles.Add(new StyleBundle("~/Content/flex").Include("~/Content/flexslider.css"));
            bundles.Add(new ScriptBundle("~/bundles/flex").Include("~/Scripts/jquery.flexslider.js"));

            bundles.Add(new StyleBundle("~/Content/dropzone").Include("~/Content/dropzone.css"));
            bundles.Add(new ScriptBundle("~/bundles/dropzone").Include("~/Scripts/dropzone.js"));

            //bundles.Add(new StyleBundle("~/Content/marketplace").Include("~/Content/marketplace.css"));

            bundles.Add(new StyleBundle("~/Content/tableCss").Include("~/Content/jquery.table.css"));

            bundles.Add(new ScriptBundle("~/bundles/Print").Include("~/Scripts/jquery.PrintArea.js"));

            bundles.Add(new ScriptBundle("~/bundles/rem").Include("~/Scripts/rem.js"));
            bundles.Add(new ScriptBundle("~/bundles/ie9").Include(
                "~/Scripts/html5/html5shiv.js",
                "~/Scripts/html5/nwmatcher-1.2.5-min.js",
                "~/Scripts/html5/selectivizr-1.0.3b.js",
                "~/Scripts/html5/respond.min.js"));

            BundleTable.EnableOptimizations = AppSettings.EnableOptimizations;
        }
    }
}
