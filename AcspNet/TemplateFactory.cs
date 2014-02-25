using System;
using System.Collections.Generic;
using System.Linq;

using Simplify.Templates;

namespace AcspNet
{
	/// <summary>
	/// Text templates loader.
	/// </summary>
	public sealed class TemplateFactory : ITemplateFactory
	{
		private readonly Manager _manager;

		private static readonly IDictionary<KeyValuePair<string, string>, string> Cache = new Dictionary<KeyValuePair<string, string>, string>();
		private static readonly object Locker = new object();

		/// <summary>
		/// Initializes a new instance of the <see cref="TemplateFactory"/> class.
		/// </summary>
		/// <param name="manager">The manager.</param>
		public TemplateFactory(Manager manager)
		{
			_manager = manager;
		}

		/// <summary>
		/// Load template from a file
		/// </summary>
		/// <param name="fileName">Template file name</param>
		/// <returns>Template class with loaded template</returns>
		public Template Load(string fileName)
		{
			if(string.IsNullOrEmpty(fileName))
				throw new ArgumentNullException("fileName");

			var filePath = string.Format("{0}/{1}", _manager.Environment.TemplatesPhysicalPath, fileName);

			if (_manager.Environment.TemplatesMemoryCache)
			{
				var existingItem = Cache.FirstOrDefault(x => x.Key.Key == filePath && x.Key.Value == _manager.Environment.Language);

				if (!existingItem.Equals(default(KeyValuePair<KeyValuePair<string, string>, string>)))
					return new Template(existingItem.Value, false);

				lock (Locker)
				{
					existingItem = Cache.FirstOrDefault(x => x.Key.Key == filePath && x.Key.Value == _manager.Environment.Language);

					if (!existingItem.Equals(default(KeyValuePair<KeyValuePair<string, string>, string>)))
						return new Template(existingItem.Value, false);

					var tpl = new Template(filePath, _manager.Environment.Language, Manager.Settings.DefaultLanguage);
					Cache.Add(new KeyValuePair<string, string>(filePath, _manager.Environment.Language), tpl.Get());
					return tpl;					
				}
			}

			return new Template(filePath, _manager.Environment.Language, Manager.Settings.DefaultLanguage);
		}
	}
}
