﻿#if NETCOREAPP2_0
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

namespace TestBase.Tests.AspNetCoreMVC
{
    [TestFixture]
    public class WhenMockingAndFakingHttpContextForAnAspNetCoreController
    {
        public class FakeController : Controller{}

        [Test]
        public void Should_get_nonnull_context_httpcontext_request_response_urlhelper_tempdata_viewdata()
        {
            var uut= new FakeController().WithControllerContext();
            uut.ControllerContext.ShouldNotBeNull();
            uut.ControllerContext.HttpContext.ShouldNotBeNull();
            uut.ControllerContext.HttpContext.Request.ShouldNotBeNull();
            uut.Request.ShouldNotBeNull();
            uut.Response.ShouldNotBeNull();
            uut.Url.ShouldNotBeNull();
            uut.ViewData.ShouldNotBeNull();
            uut.TempData.ShouldNotBeNull();
            uut.HttpContext.ShouldBe(uut.ControllerContext.HttpContext);
        }

        [Test, Ignore("WIP: not implemented")]
        public void Should_get_nonnull_viewbag()
        {
            new FakeController().WithControllerContext().ViewData.ShouldNotBeNull();
        }

        [Test]
        public void UrlHelper_should_map_controller_and_action()
        {
            var uut = new FakeController().WithControllerContext();
            uut.Url.Action("a", "b").ShouldEqual("/b/a");
        }

        [Test]
        public void UrlHelper_should_respect_virtualpathtemplate()
        {
            var uut = new FakeController().WithControllerContext(virtualPathTemplate:"/some/{action}/{controller}/thing/");
            uut.Url.Action("a", "b").ShouldEqual("/some/a/b/thing/");
        }

        [Test]
        public void UrlHelper_should_map_controller_and_action_and_other_values()
        {
            var uut = new FakeController().WithControllerContext();
            uut.Url.Action("a", "b").ShouldEqual("/b/a");
            uut.Url.Action("a", "b", new {id=1, otherparameter="2"}).ShouldEqual("/b/a?id=1&otherparameter=2");
        }

        [Test]
        public void UrlHelper_should_map_tilde_to_root()
        {
            var uut = new FakeController().WithControllerContext();
            //
            uut.Url.Content("~/here.txt").ShouldBe("/here.txt");
        }

        //static void RegisterFakeRoutes(RouteCollection routes)
        //{
        //    routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
        //    routes.MapRoute(
        //        name: "Default",
        //        url: "custom/{controller}-{action}/{id}",
        //        defaults: new { controller = "CustomC", action = "CustomA", id = UrlParameter.Optional }
        //        );
        //}
    }
}
#endif