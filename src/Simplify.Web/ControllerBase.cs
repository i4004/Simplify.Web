using System;
using Simplify.Templates;
using Simplify.Web.Responses;

namespace Simplify.Web
{
	/// <summary>
	/// Controllers base class
	/// </summary>
	public abstract class ControllerBase : ActionModulesAccessor
	{
		/// <summary>
		/// Gets the route parameters.
		/// </summary>
		/// <value>
		/// The route parameters.
		/// </value>
		public virtual dynamic RouteParameters { get; internal set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Ajax" /> class.
		/// </summary>
		/// <param name="ajaxData">The ajax data.</param>
		/// <param name="statusCode">The HTTP response status code.</param>
		/// <param name="contentType">Type of the content.</param>
		protected Ajax Ajax(string ajaxData, int statusCode = 200, string contentType = null)
		{
			return new Ajax(ajaxData, statusCode, contentType);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Ajax" /> class.
		/// </summary>
		/// <param name="ajaxData">The ajax data.</param>
		/// <param name="contentType">Type of the content.</param>
		protected Ajax Ajax(string ajaxData, string contentType)
		{
			return new Ajax(ajaxData, contentType);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Ajax" /> class.
		/// </summary>
		/// <param name="statusCode">The HTTP response status code.</param>
		protected Ajax Ajax(int statusCode = 204)
		{
			return new Ajax(statusCode);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Responses.Tpl" /> class.
		/// </summary>
		/// <param name="template">The template.</param>
		/// <param name="title">The site title.</param>
		/// <param name="statusCode">The HTTP response status code.</param>
		protected Tpl Tpl(ITemplate template, string title = null, int statusCode = 200)
		{
			return new Tpl(template, title, statusCode);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Responses.Tpl"/> class.
		/// </summary>
		/// <param name="data">The data for main content variable.</param>
		/// <param name="title">The site title.</param>
		/// <param name="statusCode">The HTTP response status code.</param>
		protected Tpl Tpl(string data, string title = null, int statusCode = 200)
		{
			return new Tpl(data, title, statusCode);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Responses.Tpl" /> class.
		/// </summary>
		/// <param name="templateFileName">Name of the template file.</param>
		/// <param name="title">The title.</param>
		/// <param name="statusCode">The HTTP response status code.</param>
		/// <exception cref="ArgumentNullException"></exception>
		protected StaticTpl StaticTpl(string templateFileName, string title = null, int statusCode = 200)
		{
			return new StaticTpl(templateFileName, title, statusCode);
		}
	}
}