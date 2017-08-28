# Simplify.Web

![Simplify](https://raw.githubusercontent.com/i4004/Simplify.Web/master/Images/IconMedium.png)

Simplify.Web is a lightweight and fast server-side .NET web-framework based on MVC and OWIN for building HTTP based web-applications, RESTful APIs etc.

_This project is a continuator of [AcspNet web-framework](https://github.com/i4004/AcspNet)_

## Package status

| Latest version | [![Nuget version](http://img.shields.io/badge/nuget-v1.2-blue.png)](https://www.nuget.org/packages/Simplify.Web/) |
| :------ | :------: |
| **Dependencies** | [![NuGet Status](http://nugetstatus.com/Simplify.Web.png)](http://nugetstatus.com/packages/Simplify.Web) |

## Issues status

| Ready issues |
| :------ |
| [![Stories in Ready](https://badge.waffle.io/i4004/Simplify.Web.svg?label=ready&title=Ready)](http://waffle.io/i4004/Simplify) |

## Build status

| | **.NET (4.5.2)** | **Mono (Latest)** |
| :------ | :------ | :------: |
| **master** | [![AppVeyor Build status](https://ci.appveyor.com/api/projects/status/sln1ciuam2hobsv4/branch/master?svg=true)](https://ci.appveyor.com/project/i4004/simplify-web/branch/master) | [![Travis Build Status](https://travis-ci.org/i4004/Simplify.Web.svg?branch=master)](https://travis-ci.org/i4004/Simplify.Web) |
| **develop** | [![AppVeyor Build status](https://ci.appveyor.com/api/projects/status/sln1ciuam2hobsv4/branch/develop?svg=true)](https://ci.appveyor.com/project/i4004/simplify-web/branch/develop) | [![Travis Build Status](https://travis-ci.org/i4004/Simplify.Web.svg?branch=develop)](https://travis-ci.org/i4004/Simplify.Web) |

## Main features

* Based on MVC and MVVM patterns
* Comes as OWIN middleware
* Uses switchable IOC container for itself and controllers, views constructor injection ([Simplify.DI](https://github.com/i4004/Simplify/wiki/Simplify.DI))
* Mono-friendly
* Support async controllers
* Uses fast templates engine ([Simplify.Templates](https://github.com/i4004/Simplify/wiki/Simplify.Templates))
* Supports controllers which can be run on any page
* Localization-friendly (supports templates, string table and data files localization by default)
* Mocking-friendly

## Getting started

<!----To get started you can install [visual studio Simplify.Web project templates](http://visualstudiogallery.msdn.microsoft.com/25a4534d-5a5b-4cce-aecf-523c3679a1c3) and read [this](https://github.com/i4004/Simplify.Web/wiki/Getting-started) article.-->

### The examples below shows simple backend HTML generation, but you can easily use any front end technologies with Simplify.Web like AngularJS etc.

[Getting started page](https://github.com/i4004/Simplify.Web/wiki/Getting-started)

## Some examples

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

#### Templates example

##### Master.tpl
```html
﻿<!DOCTYPE html>
<html>
<head>
    <title>{Title}</title>
</head>
<body>
    {MainContent}
</body>
</html>
```

##### About.tpl

```html
﻿<div class="container">
    Welcome to about page!
</div>
```

### [Detailed documentation](https://github.com/i4004/Simplify.Web/wiki)
