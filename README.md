Advanced Controls Site Platform .NET is an ASP.NET based web-sites plugin-based engine.
It is allows you to construct your web-site from a set of extensions (plugins). Each web site extension can do their own task.

* It is based on basic ASP.NET functionality (empty ASP.NET web-page);
* Web-site is constructing from extensions (plugins);
* Extensions can contain some functionality shared between other extensions or can be executed depending on HTTP query string parameters and do some web-page build etc.;
* It is NOT using ASP.NET controls and include their own fast web-page render (included in https://github.com/i4004/AcspCommonExtensions;
* For web-site front-end it is recommended to use clean HTML/CSS/JS technologies;
* Starting from version 2.0 you can set extension parameters via class attributes.

AcspNet have two types of extensions:
* Executable (exec) extensions which can run depending on HTTP query string parameters only;
* Library (lib) extensions which can be used by other lib or exec extensions.

Default ASP.NET page should be named as **index.aspx**

Recommended extrensions folder structure:

```text
YourProject
  -Extensions
    -Executable
      -Extension1.cs
      -Extension2.cs
    -Library
      -Extension3.cs
      -Extension4.cs
```

Example
==

#### index.aspx

```aspx-cs
<%@ Page language="c#" Inherits="ExampleProject.Index" Codebehind="index.aspx.cs" %>
```

#### index.aspx.cs

```csharp
using System;
using System.Web.UI;
using AcspCommonExtensions.Library;
using AcspDev.Extensions.Executable.Dev;
using AcspNet;
using BarcodeInfoWebStat.Extensions.Executable;

namespace ExampleProject
{
	[LoadExtensionsFromAssemblyOf(typeof(MainPage), typeof(EngineSettings))]
	[LoadIndividualExtensions(typeof(ExtensionsInfo))]
	public class Index : Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			new Manager().Run();
		}
	}
}
```


#### Main page extension MainPage.cs example

```csharp
using AcspCommonExtensions.Library;
using AcspCommonExtensions.Library.Authentication;
using AcspNet;

namespace ExampleProject.Extensions.Executable
{
	[RunType(RunType.MainPage)]
	[Version("1.0")]
	public class MainPage : IExecExtension
	{
		public void Invoke(Manager manager)
		{
			var auth = manager.Get<Auth>();
			var dataCollector = manager.Get<DataCollector>();

			if (!auth.IsAuthenticatedAsUser)
			{
				var tpl = manager.Get<TemplateFactory>().LoadTemplate("UserLogin/LoginForm.tpl");

				dataCollector.Set("MainContainerData", tpl.Text);
			}
			else
				manager.Redirect("?act=userProfile");
		}
	}
}
```

#### UserLogin.cs extension which processing login Ajax request from login form

```csharp
using System;
using AcspCommonExtensions.Library;
using AcspCommonExtensions.Library.Authentication;
using AcspCommonExtensions.Library.Controls;
using AcspNet;
using ApplicationHelper.Logs;
using ExampleProject.Database;
using ExampleProject.Database.Entities;

namespace ExampleProject.Extensions.Executable
{
	[Action("login")]
	[Version("1.0")]
	internal class UserLogin : IExecExtension
	{
		public void Invoke(Manager manager)
		{
			var messageBox = manager.Get<MessageBox>();
			var dataCollector = manager.Get<DataCollector>();
			var auth = manager.Get<Auth>();

			try
			{
				var name = manager.QueryString["userName"];
				var password = manager.QueryString["password"];

				var user = DbWorker.DbConnection.GetObject<User>(x => x.Name == name && x.Password == password);

				if (user != null)
				{
					auth.LogInSessionUser(user.ID);

					dataCollector.DisplayPartial("1");
				}
				else
					dataCollector.DisplayPartial(messageBox.GetSmallSt("NotifyWrongUserNameOrPassword"));

			}
			catch (Exception e)
			{
				Logger.Write(e);
				dataCollector.DisplayPartial(messageBox.GetSmallSt("NotifyUnexpectedSiteError"));
			}
		}
	}
}
```

#### UserLogout.cs extension which processing logout request from login form

```csharp
using AcspCommonExtensions.Library.Authentication;
using AcspNet;

namespace ExampleProject.Extensions.Executable
{
	[Action("logout")]
	[Priority(-5)]
	[Version("1.0.1")]
	internal class UserLogout : IExecExtension
	{
		public void Invoke(Manager manager)
		{
			var auth = manager.Get<Auth>();

			auth.LogOutSessionUser();

			manager.Redirect("index.aspx");
		}
	}
}
```
