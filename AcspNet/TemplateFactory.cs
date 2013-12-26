using ApplicationHelper.Templates;

namespace AcspNet
{
	/// <summary>
	/// Html templates factory
	/// </summary>
	public sealed class TemplateFactory
	{
		private Manager _manager;
		private Environment _ev;

		public TemplateFactory(Manager manager, Environment ev)
		{
			_manager = manager;
			_ev = ev;
		}

		/// <summary>
		/// Load template from a file
		/// </summary>
		/// <param name="fileName">Template file name</param>
		/// <returns>Template class with loaded template</returns>
		public Template Load(string fileName)
		{
			//if (Manager.Settings.TemplatesMemoryCache)

			var a = new Template();
			
			//var tpl = new Template(string.Format("{0}/{1}", _ev.TemplatesPhysicalPath, fileName),
				//Manager.Settings.DefaultLanguage, _ev.Language);

			return tpl;
		}
	}
}
