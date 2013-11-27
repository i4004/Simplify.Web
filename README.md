AcspNet
=======

Advanced Controls Site Platform .NET is an ASP.NET based web-sites plugin-based engine.
It is allows you to construct your web-site from a set of extensions (plugins). Each web site extension can do their own task.

* It is based on basic ASP.NET functionality (empty ASP.NET web-page);
* Web-site is constructing from extensions (plugins);
* Extensions can contain some functionality shared between other extensions or can be executed depending on HTTP query string parameters and do some web-page build etc.;
* It is NOT using ASP.NET controls and include their own fast web-page render (included in https://github.com/i4004/AcspCommonExtensions;
* For web-site front-end it is recommended to use clean HTML/CSS/JS technologies.
* Starting from version 2.0 you can set extension parameters via class attributes.
