﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using NUnit.Framework;
using TestBase.Shoulds;

namespace TestBase.Tests.HttpContextTests
{
    [TestFixture]
    public class WhenMockingAndFakingHttpContextForAController
    {
        public class FakeController : Controller{}

        [Test]
        public void Should_get_a_context_an_httpcontext_and_a_request()
        {
            var uut= new FakeController().WithHttpContextAndRoutes();
            uut.ControllerContext.ShouldNotBeNull();
            uut.ControllerContext.HttpContext.ShouldNotBeNull();
            uut.ControllerContext.HttpContext.Request.ShouldNotBeNull();
            uut.Request.ShouldNotBeNull();
        }

        [Test]
        public void Should_get_a_global_httpcontext_too()
        {
            var uut = new FakeController().WithHttpContextAndRoutes();
            HttpContext.Current.ShouldNotBeNull();
            HttpContext.Current.Request.Url.ShouldEqual(uut.Request.Url);
        }

        [Test]
        public void Should_get_a_working_urlhelper()
        {
            var uut = new FakeController().WithHttpContextAndRoutes();
            uut.Url.Action("a", "b").ShouldEqual("/b/a");
            uut.Url.Action("a", "b", new {id=1, otherparameter="2"}).ShouldEqual("/b/a/1?otherparameter=2");
            uut.Url.Action("Index", "Home").ShouldEqual("/"); //because RouteConfig defines Home/Index as the default
        }

        [Test]
        public void Should_get_a_working_urlhelper__Given_custom_route_config()
        {
            var uut = new FakeController().WithHttpContextAndRoutes(RegisterFakeRoutes);
            uut.Url.Action("a", "c").ShouldEqual("/custom/c-a");
            uut.Url.Action("a", "c", new { id = 1, otherparameter = "2" }).ShouldEqual("/custom/c-a/1?otherparameter=2");
            uut.Url.Action("Index", "Home").ShouldEqual("/custom/Home-Index"); 
        }

        [Test,Ignore("Can't yet do")]
        public void Should_be_able_test_route_mapping()
        {
            var uut = new FakeController().WithHttpContextAndRoutes(RegisterFakeRoutes);
            var rd = uut.Url.RouteCollection.GetRouteData(uut.HttpContext);
            rd.Values.ShouldContain(new KeyValuePair<string, object>("Controller", "Fake"));

        }

        static void RegisterFakeRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Default",
                url: "custom/{controller}-{action}/{id}",
                defaults: new { controller = "CustomC", action = "CustomA", id = UrlParameter.Optional }
                );
        }
    }
}
