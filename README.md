*TestBase* gets you off to a flying start when unit testing projects with dependencies.
It has rich, but easily extensible, fluent assertions, including EqualsByValue, Regex, Stream Comparision, and Ado.Net assertions.

TestBase.Shoulds
------------------
Chainable fluent assertions get you to the point concisely
```
UnitUnderTest.Action()
    .ShouldNotBeNull()
    .ShouldContain(expected);
UnitUnderTest.OtherAction()
    .ShouldEqualByValue(new {Id=1, Payload=expected, Additional=new[]{ expected1, expected2 }} )
    .Payload
        .ShouldMatchIgnoringCase(""I expected this"");
```

```
* ShouldBe(), ShouldMatch(), ShouldNotBe(), ShouldContain(), ShouldNotContain(), ShouldBeEmpty(), ShouldNotBeEmpty(), ShouldAll() and many more
* ShouldEqualByValue(), ShouldEqualByValueExceptForValues() works with all kinds of object and collections
* Stream.ShouldHaveSameStreamContentAs()` and `Stream.ShouldContain()
```

Testable Logging with `StringListLogger`:
```
MS Logging: ILoggerFactory factory=new LoggerFactory.AddProvider(new StringListLoggerProvider())
Serilogging: new LoggerConfiguration().WriteTo.StringList(stringList).CreateLogger()
//
var logger=new StringListLogger(); ... ; logger.LoggedLines.ShouldContain(x=>x.Matches("kilroy was here")
```

TestBase.FakeDb
------------------
Works with Ado.Net and technologies on top of it, including Dapper.
```
* fakeDbConnection.SetupForQuery(IEnumerable<TFakeData>; )
* fakeDbConnection.SetupForQuery(IEnumerable<Tuple<TFakeDataForTable1,TFakeDataForTable2>> )
* fakeDbConnection.SetupForQuery(fakeData, new[] {""FieldName1"", FieldName2""})
* fakeDbConnection.SetupForExecuteNonQuery(rowsAffected)
* fakeDbConnection.ShouldHaveUpdated(""tableName"", [Optional] fieldList, whereClauseField)
* fakeDbConnection.ShouldHaveSelected(""tableName"", [Optional] fieldList, whereClauseField)
* fakeDbConnection.ShouldHaveUpdated(""tableName"", [Optional] fieldList, whereClauseField)
* fakeDbConnection.ShouldHaveDeleted(""tableName"", whereClauseField)
* fakeDbConnection.ShouldHaveInvoked(cmd => predicate(cmd))
* fakeDbConnection.ShouldHaveXXX().ShouldHaveParameter(""name"", value)
* fakeDbConnection.Verify(x=>x.CommandText.Matches(""Insert [case] .*"") && x.Parameters[""id""].Value==1)
```

TestBase.Mvc
------------
```
ControllerUnderTest.Action()
  .ShouldbeViewResult()
  .ShouldHaveModel<TModel>()
  .ShouldEqualByValue(expected)
ControllerUnderTest.Action()
  .ShouldBeRedirectToRouteResult()
  .ShouldHaveRouteValue(""expectedKey"", [Optional] ""expectedValue"");

ShouldHaveViewDataContaining(), ShouldBeJsonResult() etc.
```

Version 3 on Net4 only
----------------------

TestBase-Mvc for MVC 4 & 5
-------------------------
* Includes mocking of HttpConttextBase and parsing of RouteConfig so that Controllers have a working Controller.Url when under test:
```
uut = new MyController().WithHttpContextAndRoutes(RouteConfig.RegisterRoutes);
uut.Url.Action("MyAction", "MyOtherController").ShouldEqual("/MyOtherController/MyAction");
```

Can be used in both NUnit & MS UnitTestFramework test projects.

* Building on Mono : define compile symbol NoMSTest to remove dependency on Microsoft.VisualStudio.QualityTools.UnitTestFramework

ChangeLog
---------
4.0.5.0 TestBase.Mvc partially ported to AspNetcore
4.0.4.0 StreamShoulds
4.0.3.0 StringListLogger as MS Logger and as Serilogger
4.0.1.0 Port to NetCore
3.0.3.0 Improves FakeDb setup
3.0.x.0 adds and/or corrects missing Shoulds()
2.0.5.0 adds some intellisense and FakeDbConnection.Verify(..., message,args) overload

