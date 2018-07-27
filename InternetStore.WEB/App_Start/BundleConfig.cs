using System.Web;
using System.Web.Optimization;

namespace InternetStore.WEB
{
    public class BundleConfig
    {
        //Дополнительные сведения об объединении см. по адресу: http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include("~/Scripts/jquery.validate*"));
            bundles.Add(new ScriptBundle("~/bundles/MainJS").Include(
               "~/Content/libs/bootstrap/js/bootstrap.min.js",
               "~/Content/libs/animate/wow.min.js",
               "~/Content/libs/jquery.mmenu.all/jquery.mmenu.all.min.js",
               "~/Content/libs/countdown/jquery.countdown.min.js",
               "~/Content/libs/jquery-appear/jquery.appear.min.js",
               "~/Content/libs/jquery-countto/jquery.countTo.min.js",
               "~/Content/libs/direction/js/jquery.hoverdir.js",
               "~/Content/libs/direction/js/modernizr.custom.97074.js",
               "~/Content/libs/isotope/isotope.pkgd.min.js",
               "~/Content/libs/isotope/fit-columns.js",
               "~/Content/libs/isotope/isotope-docs.min.js",
               "~/Content/libs/mansory/mansory.js",
               "~/Content/libs/prettyphoto-master/js/jquery.prettyPhoto.js",
               "~/Content/libs/slick-sider/slick.min.js",
               "~/Content/libs/countdown-timer/js/jquery.final-countdown.min.js",
               "~/Content/libs/countdown-timer/js/kinetic.js",
               "~/Content/libs/owl.carousel.min/owl.carousel.min.js",
               "~/Scripts/js/main.js",
               "~/Scripts/jquery.unobtrusive-ajax.js",
               "~/Scripts/myJS/layout-scripts.js",
               "~/Scripts/myJS/admin.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/ProductListJS").Include(
                "~/Scripts/myJS/add-product-to-cart.js",
                "~/Scripts/myJS/product-search.js",
                "~/Scripts/myJS/sift-by-category.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/CartJS").Include(
                "~/Scripts/myJS/cart.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/ChekoutJS").Include(
                "~/Scripts/myJS/check-out.js"
               )); 
            bundles.Add(new ScriptBundle("~/bundles/CurrentplantJS").Include(
                "~/Scripts/myJS/current-plant.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/ToastmessageJS").Include(
                "~/Scripts/toastmessage/jquery.toastmessage.js",
                "~/Scripts/toastmessage/jquery.toastmessage.tests.js"
                ));
            //-------------------------------------------------------------------------------------------------------------------------------------------//

            bundles.Add(new StyleBundle("~/bundles/MainCSS").Include(
                "~/Content/libs/bootstrap/css/bootstrap.min.css",
                "~/Content/libs/font-awesome/css/font-awesome.min.css",
                "~/Content/libs/animate/animated.css",
                "~/Content/libs/owl.carousel.min/owl.carousel.min.css",
                "~/Content/libs/jquery.mmenu.all/jquery.mmenu.all.css",
                "~/Content/libs/pe-icon-7-stroke/css/pe-icon-7-stroke.css",
                "~/Content/libs/direction/css/noJS.css",
                "~/Content/libs/prettyphoto-master/css/prettyPhoto.css",
                "~/Content/libs/slick-sider/slick.min.css",
                "~/Content/libs/countdown-timer/css/demo.css",
                "~/Content/main.css",
                "~/Content/home.css"
            ));
            bundles.Add(new StyleBundle("~/bundles/ToastmessageCSS").Include(
                "~/Content/toastmessage/jquery.toastmessage.css"
            ));
        }
    }
}
