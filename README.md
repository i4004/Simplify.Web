.NET (4.5) [![AppVeyor build status](https://ci.appveyor.com/api/projects/status/89hirbi3bn5ajkvj)](https://ci.appveyor.com/project/i4004/acspnet) Mono (3.8.0) [![Travis build status](https://travis-ci.org/i4004/AcspNet.png?branch=master)](https://travis-ci.org/i4004/AcspNet)

[![Nuget version](http://img.shields.io/badge/nuget-v5.2.0-blue.png)](https://www.nuget.org/packages/AcspNet/)
[![NuGet Status](http://nugetstatus.com/AcspNet.png)](http://nugetstatus.com/packages/AcspNet)
[![Stories in Ready](https://badge.waffle.io/i4004/acspnet.png?label=ready&title=Ready)](https://waffle.io/i4004/acspnet)

---

![ACSP .NET](https://raw.github.com/i4004/AcspNet/master/Images/Icon128x128.png)

ACSP.NET (Advanced Controls Site Platform .NET) is a lightweight and fast server-side .NET web-framework based on MVC and OWIN

### Main features

* Based on MVC and MVVM patterns
* Comes as OWIN middleware
* Uses switchable IOC container for itself and controllers, views constructor injection ([Simplify.DI](https://github.com/i4004/Simplify/wiki/Simplify.DI))
* Mono-friendly
* Support async controllers
* Uses fast templates engine ([Simplify.Templates](https://github.com/i4004/Simplify/wiki/Simplify.Templates))
* Supports controllers which can be run on any page
* Localization-friendly (supports templates, string table and data files localization by default)
* Mocking-friendly

### Getting started

To get started you can install [visual studio AcspNet project templates](http://visualstudiogallery.msdn.microsoft.com/25a4534d-5a5b-4cce-aecf-523c3679a1c3) and read [this](https://github.com/i4004/AcspNet/wiki/Getting-started) article.

### Some examples

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
````html
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

````html
﻿<div class="container">
    Welcome to about page!
</div>
```

### [Detailed documentation](https://github.com/i4004/AcspNet/wiki)
