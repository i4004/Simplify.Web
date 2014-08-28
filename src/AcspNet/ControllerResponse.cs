﻿using AcspNet.Core;
using AcspNet.Modules;
using AcspNet.Modules.Html;

namespace AcspNet
{
	/// <summary>
	/// Provides controllers responses base class
	/// </summary>
	public abstract class ControllerResponse : ModulesAccessor
	{
		/// <summary>
		/// Current AcspNet context
		/// </summary>
		public virtual IAcspNetContext Context { get; internal set; }

		/// <summary>
		/// Gets the data collector.
		/// </summary>
		/// <value>
		/// The data collector.
		/// </value>
		public virtual IDataCollector DataCollector { get; internal set; }

		/// <summary>
		/// Gets the redirector.
		/// </summary>
		/// <value>
		/// The redirector.
		/// </value>
		public virtual IRedirector Redirector { get; internal set; }

		/// <summary>
		/// Gets the response writer.
		/// </summary>
		/// <value>
		/// The response writer.
		/// </value>
		public virtual IResponseWriter ResponseWriter { get; internal set; }
		
		/// <summary>
		/// Various HTML generation classes container
		/// </summary>
		/// <value>
		/// The various HTML generation classes container
		/// </value>
		public virtual IHtmlWrapper Html { get; internal set; }

		/// <summary>
		/// Processes this response
		/// </summary>
		public abstract ControllerResponseResult Process();
	}
}