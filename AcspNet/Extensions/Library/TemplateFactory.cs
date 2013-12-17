using ApplicationHelper.Templates;

namespace AcspNet.Extensions.Library
{
	/// <summary>
	/// Html templates factory
	/// </summary>
	[Priority(-7)]
	[Version("1.2.4")]
	public sealed class TemplateFactory : ILibExtension
	{
		private Manager _manager;

		/// <summary>
		/// Initializes the library extension.
		/// </summary>
		/// <param name="manager">The manager.</param>
		public void Initialize(Manager manager)
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
			var ev = _manager.Get<EnvironmentVariables>();

			return new Template(string.Format("{0}/{1}", ev.TemplatesPhysicalPath, fileName), EngineSettings.DefaultLanguage, ev.Language);
		}
	}
}
