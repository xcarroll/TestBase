﻿using System.Web.Mvc;

namespace TestBase.Shoulds
{
    public static class MvcActionResultShoulds
    {
        public static RedirectResult ShouldBeRedirectResult(
            this ActionResult actionResult,
            string            url       = null,
            bool?             permanent = null)
        {
            var result = actionResult.ShouldBeOfType<RedirectResult>();
            if (url != null) result.Url.ShouldEqual(url, "Expected the RedirectResult.Url to be {0}", url);

            if (permanent.HasValue)
                result.Permanent.ShouldEqual(permanent,
                                             "Expected the RedirectResult to {0} be permanent",
                                             permanent.Value ? "" : "not ");

            return result;
        }

        public static RedirectToRouteResult ShouldBeRedirectToRouteResult(this ActionResult @this)
        {
            return @this.ShouldBeOfType<RedirectToRouteResult>();
        }

        public static RedirectToRouteResult ShouldBeRedirectToRouteResult(
            this ActionResult @this,
            string            action,
            string            controller)
        {
            var result = @this.ShouldBeOfType<RedirectToRouteResult>();
            result.ShouldHaveRouteValue("controller", controller);

            if (string.IsNullOrEmpty(action))
                @this.ShouldBeRedirectToDefaultActionAndController();
            else
                result.ShouldHaveRouteValue("action", action);

            return result;
        }

        public static RedirectToRouteResult ShouldBeRedirectToRouteResultWithController(
            this ActionResult @this,
            string            controller)
        {
            var result = @this.ShouldBeOfType<RedirectToRouteResult>();
            result.ShouldHaveRouteValue("controller", controller);

            result.ShouldBeRedirectToDefaultAction();

            return result;
        }

        public static RedirectToRouteResult ShouldBeRedirectToDefaultActionAndController(this ActionResult @this)
        {
            var result = @this.ShouldBeOfType<RedirectToRouteResult>();

            if (result.RouteValues.ContainsKey("controller"))
                result.RouteValues["controller"]
                      .ShouldBeNullOrEmptyOrWhitespace(
                                                       "Expected ActionResult to be Redirect to default action, but a controller, '{0}', was specified.",
                                                       result.RouteValues["controller"]);

            result.ShouldBeRedirectToDefaultAction();

            return result;
        }

        public static RedirectToRouteResult ShouldBeRedirectToDefaultAction(this RedirectToRouteResult @this)
        {
            if (!@this.RouteValues.ContainsKey("action")) return @this;

            var action = @this.RouteValues["action"].ToString();

            if (!string.IsNullOrEmpty(action)) @this.ShouldHaveRouteValue("action", "index");

            return @this;
        }

        public static RedirectToRouteResult ShouldBeRedirectToActionResult(this ActionResult @this, string action)
        {
            var result = @this.ShouldBeOfType<RedirectToRouteResult>();

            result.RouteValues.ContainsKey("controller")
                  .ShouldBeFalse(string.Format("Controller redirect found '{0}' not none expected.",
                                               result.RouteValues["controller"]));

            result.ShouldBeRedirectToAction(action);

            return result;
        }

        public static RedirectToRouteResult ShouldBeRedirectToActionResult(
            this ActionResult @this,
            string            action,
            string            controller)
        {
            var result = @this.ShouldBeOfType<RedirectToRouteResult>();

            result.RouteValues.Keys.ShouldContain("controller");
            result.RouteValues["controller"].ToString().ToLower().ShouldEqual(controller.ToLower());
            result.ShouldBeRedirectToAction(action);
            return result;
        }

        public static ViewResult ShouldBeViewResult(this ActionResult @this)
        {
            return @this.ShouldBeOfType<ViewResult>();
        }

        public static ViewResultBase ShouldBeViewResult(this ActionResult @this, string viewName)
        {
            return @this.ShouldBeOfType<ViewResult>().ShouldBeViewResultNamed(viewName);
        }

        public static ViewResultBase ShouldBeViewResultNamed(this ActionResult @this, string viewName)
        {
            return @this.ShouldBeOfType<ViewResult>().ShouldBeViewNamed(viewName);
        }

        public static JsonResult ShouldBeJsonResult(this ActionResult @this)
        {
            return @this.ShouldBeOfType<JsonResult>();
        }

        public static PartialViewResult ShouldBePartialViewResult(this ActionResult @this)
        {
            return @this.ShouldBeOfType<PartialViewResult>();
        }

        public static RedirectToRouteResult ShouldBeRedirectToController(
            this RedirectToRouteResult @this,
            string                     controller)
        {
            @this.ShouldHaveRouteValue("controller", controller);
            return @this;
        }

        public static RedirectToRouteResult ShouldBeRedirectToAction(this RedirectToRouteResult @this, string action)
        {
            @this.ShouldHaveRouteValue("action", action);
            return @this;
        }
    }
}
