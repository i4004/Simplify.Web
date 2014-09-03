![ACSP .NET](https://raw.github.com/i4004/AcspNet/master/Images/Icon128x128.png)

ACSP.NET is a lightweight and fast .NET web-framework based on MVC and OWIN

### Main features

* Based on MVC and MVVM patterns
* Comes as OWIN middleware
* Uses switchable IOC container for itself and controllers, views constructor injection ([Simplify.DI](https://github.com/i4004/Simplify/tree/master/src/Simplify.DI))
* Mono-friendly
* Support async controllers
* Uses fast templates engine ([Simplify.Templates](https://github.com/i4004/Simplify/tree/master/src/Simplify.Templates))
* Supports controllers which can be run on any page
* Localization-friendly (supports templates, string table and data files localization by default)
* Mocking-friendly

### Examples

#### Simple static page controller
```csharp
[Get("about")]
public class AboutController : Controller
{
    public override ControllerResponse Invoke()
    {
        return new Tpl(TemplateFactory.Load("Static/About"), StringTable.PageTitleAbout);
    }
}
```

#### Any page controller with high run priority example
Runs on any request and adds login panel to a pages
```csharp
[Priority(-1)]
public class LoginPanelController : AsyncController
{
    public override async Task<ControllerResponse> Invoke()
    {
        return Context.Context.Authentication.User == null
            ? new InlineTpl("LoginPanel", await TemplateFactory.LoadAsync("Shared/LoginPanel/GuestPanel"))
            : new InlineTpl("LoginPanel", await GetView<LoggedUserPanelView>().Get(Context.Context.Authentication.User.Identity.Name));
    }
}
```

#### View example
```csharp
public class LoggedUserPanelView : View
{
    public async Task<ITemplate> Get(string userName)
    {
        var tpl = await TemplateFactory.LoadAsync("Shared/LoginPanel/LoggedUserPanel");

        tpl.Add("UserName", userName);

        return tpl;
    }
}
```

Status
===
 .NET (4.5) .... Mono (3.6.0)

[![AppVeyor build status](https://ci.appveyor.com/api/projects/status/89hirbi3bn5ajkvj)](https://ci.appveyor.com/project/i4004/acspnet) [![Travis build status](https://travis-ci.org/i4004/AcspNet.png?branch=master)](https://travis-ci.org/i4004/AcspNet)
[![Nuget version](http://img.shields.io/badge/nuget-v5.0-blue.png)](https://www.nuget.org/packages/AcspNet/)
[![Stories in Ready](https://badge.waffle.io/i4004/acspnet.png?label=ready&title=Ready)](https://waffle.io/i4004/acspnet)
