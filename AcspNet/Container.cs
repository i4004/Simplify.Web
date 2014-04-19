﻿namespace AcspNet
{
	/// <summary>
	/// AcspNet controllers and views base class
	/// </summary>
	public abstract class Container : IHideObjectMembers
	{
		/// <summary>
		/// Current HTTP and ACSP context
		/// </summary>
		public virtual IAcspContext Context { get; internal set; }

		/// <summary>
		/// Current request environment data.
		/// </summary>
		public virtual IEnvironment Environment { get; internal set; }

		/// <summary>
		/// Current language manager
		/// </summary>
		public virtual ILanguageManager LanguageManager { get; internal set; }

		/// <summary>
		/// Text and XML files loader.
		/// </summary>
		public virtual IFileReader FileReader { get; internal set; }

		/// <summary>
		/// Text templates loader.
		/// </summary>
		public virtual ITemplateFactory TemplateFactory { get; internal set; }

		/// <summary>
		/// Localizable text items string table.
		/// </summary>
		public IStringTable StringTable { get; internal set; }

		/// <summary>
		/// Web-site master page data collector.
		/// </summary>
		public IDataCollector DataCollector { get; internal set; }
		
		///// <summary>
		///// Current ACSP executing processor controller
		///// </summary>
		//public IAcspProcessorContoller ProcessorContoller { get; internal set; }

		///// <summary>
		///// Various HTML generation classes
		///// </summary>
		//public IHtmlWrapper Html { get; internal set; }

		///// <summary>
		///// Current ACSP executing processor controller
		///// </summary>
		//internal AcspProcessor Processor { get; set; }

		///// <summary>
		///// Interface that is used to control users login/logout/autnenticate via cookie or session and stores current user name/password/id unformation
		///// </summary>
		//public IAuthenticationModule AuthenticationModule { get; internal set; }

		///// <summary>
		///// Additional extensions
		///// </summary>
		//public IExtensionsWrapper Extensions { get; internal set; }

		internal virtual ContainerFactory ContainerFactory { get; set; }

		/// <summary>
		/// Gets library extension instance
		/// </summary>
		/// <typeparam name="T">Library extension instance to get</typeparam>
		/// <returns>Library extension</returns>
		public T GetView<T>()
			where T : View
		{
			var type = typeof(T);

			return (T)ContainerFactory.CreateContainer(type);
		}
	}
}