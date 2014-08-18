using System;
using System.Collections.Generic;
using System.Linq;
using Simplify.Templates;

namespace AcspNet.Modules
{
	/// <summary>
	/// Web-site cacheable text templates loader
	/// </summary>
	public sealed class TemplateFactory : ITemplateFactory
	{
		private readonly IEnvironment _environment;
		private readonly string _language;
		private readonly string _defaultLanguage;
		private readonly bool _templatesMemoryCache;

		private readonly IDictionary<KeyValuePair<string, string>, string> _cache = new Dictionary<KeyValuePair<string, string>, string>();

		private readonly object _locker = new object();

		/// <summary>
		/// Initializes a new instance of the <see cref="TemplateFactory"/> class.
		/// </summary>
		public TemplateFactory(IEnvironment environment, string language, string defaultLanguage, bool templatesMemoryCache = false)
		{
			_environment = environment;
			_language = language;
			_defaultLanguage = defaultLanguage;
			_templatesMemoryCache = templatesMemoryCache;
		}

		/// <summary>
		/// Load web-site template from a file
		/// </summary>
		/// <param name="fileName">Template file name</param>
		/// <returns>Template class with loaded template</returns>
		public ITemplate Load(string fileName)
		{
			if (string.IsNullOrEmpty(fileName))
				throw new ArgumentNullException("fileName");

			if (!fileName.EndsWith(".tpl"))
				fileName = fileName + ".tpl";

			var filePath = string.Format("{0}{1}", _environment.TemplatesPhysicalPath, fileName);

			if (_templatesMemoryCache)
			{
				var tpl = TryLoadExistingTemplate(filePath);

				if (tpl != null)
					return tpl;

				lock (_locker)
				{
					tpl = TryLoadExistingTemplate(filePath);

					if (tpl != null)
						return tpl;

					tpl = new Template(filePath, _language, _defaultLanguage);
					_cache.Add(new KeyValuePair<string, string>(filePath, _language), tpl.Get());
					return tpl;
				}
			}

			return new Template(filePath, _language, _defaultLanguage);
		}

		private Template TryLoadExistingTemplate(string filePath)
		{
			var existingItem = _cache.FirstOrDefault(x => x.Key.Key == filePath && x.Key.Value == _language);

			if (!existingItem.Equals(default(KeyValuePair<KeyValuePair<string, string>, string>)))
				return new Template(existingItem.Value, false);

			return null;
		}
	}
}