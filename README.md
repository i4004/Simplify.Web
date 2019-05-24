# Simplify.Web

![Simplify](https://raw.githubusercontent.com/i4004/Simplify.Web/master/Images/IconMedium.png)

Simplify.Web is a lightweight and fast server-side .NET web-framework based on MVC and OWIN for building HTTP based web-applications, RESTful APIs etc.

_This project is a continuator of [AcspNet web-framework](https://github.com/i4004/AcspNet)_

## Package status

| Latest version | [![Nuget version](http://img.shields.io/badge/nuget-v2.0-blue.svg)](https://www.nuget.org/packages/Simplify.Web/) |
| :------ | :------: |
| **Dependencies** | [![Libraries.io dependency status for latest release](https://img.shields.io/librariesio/release/nuget/Simplify.Web.svg)](https://libraries.io/nuget/Simplify.Web) |

## Issues

[![Issues board](https://dxssrr2j0sq4w.cloudfront.net/3.2.0/img/external/zenhub-badge.svg)](https://app.zenhub.com/workspaces/simplify-5ce3859397ab7c51aa180635/board?repos=17025953,51341283,66346856,66425973,66536426)


## Build status

| Branch | **.NET (4.6.2)** |
| :------ | :------ |
| **master** | [![AppVeyor Build status](https://ci.appveyor.com/api/projects/status/sln1ciuam2hobsv4/branch/master?svg=true)](https://ci.appveyor.com/project/i4004/simplify-web/branch/master) |
| **develop** | [![AppVeyor Build status](https://ci.appveyor.com/api/projects/status/sln1ciuam2hobsv4/branch/develop?svg=true)](https://ci.appveyor.com/project/i4004/simplify-web/branch/develop) |

## Main features

* Comes as Microsoft.AspNetCore OWIN middleware
* Can be used as an API backend only with other front-end frameworks
* Based on MVC and MVVM patterns
* Lightweigh & Fast
* Uses switchable IOC container for itself and controllers, views constructor injection ([Simplify.DI](https://github.com/i4004/Simplify/wiki/Simplify.DI))
* Support async controllers
* Supports controllers which can be run on any page
* Localization-friendly (supports templates, string table and data files localization by default)
* Uses fast templates engine ([Simplify.Templates](https://github.com/i4004/Simplify/wiki/Simplify.Templates))
* Mocking-friendly
* Mono-friendly

## Getting started

Below is the list of sample applications showing different variations of Simplify.Web usage

* [Only as an API backend with Angular + Bootstrap UI SPA](https://github.com/i4004/Simplify.Web/tree/master/src/SampleApps/SampleApp.Angular)
* [Very simple Kestrel-based Application with backend page](https://github.com/i4004/Simplify.Web/tree/master/src/SampleApps/SampleApp.Kestrel)
* [Kestrel-based Application with backend HTML generation, localization, authentication](https://github.com/i4004/Simplify.Web/tree/master/src/SampleApps/SampleApp.SelfHosted)
* [Only as an API backend with Vue.js + Bootstrap UI SPA](https://github.com/i4004/Simplify.Web/tree/master/src/SampleApps/SampleApp.Vue)
* [Only as an API backend with Vue.js + Element UI SPA](https://github.com/i4004/Simplify.Web/tree/master/src/SampleApps/SampleApp.Vue.Element)
* [Very simple Kestrel-based Application hosted as windows-service](https://github.com/i4004/Simplify.Web/tree/master/src/SampleApps/SampleApp.WindowsServiceHosted)

[Getting started page](https://github.com/i4004/Simplify.Web/wiki/Getting-started)

### Just some simple controllers example

#### Simple static page controller

```csharp
// Controller will be executed only on HTTP GET request like http://mysite.com/about
[Get("about")]
public class AboutController : Controller
{
    public override ControllerResponse Invoke()
    {
        // About.tpl content will be inserted into {MainContent} in Master.tpl
        return new StaticTpl("Static/About", StringTable.PageTitleAbout);
    }
}
```

#### API controller example

```csharp
[Get("api/weatherTypes")]
public class SampleDataController : Controller
{
    private static readonly string[] Summaries =
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public override ControllerResponse Invoke()
    {
        try
        {
            return new Json(items);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);

            return StatusCode(500);
        }
    }
}
```

#### Any page controller with high run priority example

Runs on any request and adds login panel to a pages

```csharp
// Controller will be executed on any request and will be launched before other controllers (because they have Priority = 0 by default)
[Priority(-1)]
public class LoginPanelController : AsyncController
{
    public override async Task<ControllerResponse> Invoke()
    {
        return Context.Context.Authentication.User == null
            // Data from GuestPanel.tpl will be inserted into {LoginPanel} in Master.tpl
            ? new InlineTpl("LoginPanel", await TemplateFactory.LoadAsync("Shared/LoginPanel/GuestPanel"))
            // Data from LoggedUserPanelView will be inserted into {LoginPanel} in Master.tpl
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
        // Loading template from LoggedUserPanel.tpl asynchronously
        var tpl = await TemplateFactory.LoadAsync("Shared/LoginPanel/LoggedUserPanel");

        // Setting userName into {UserName} variable in LoggedUserPanel.tpl
        tpl.Add("UserName", userName);

        return tpl;
    }
}
```

### [Detailed documentation](https://github.com/i4004/Simplify.Web/wiki)
