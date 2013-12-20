using ApplicationHelper.Templates;

namespace AcspNet.CoreExtensions.Library
{
	/// <summary>
	/// Html templates factory
	/// </summary>
	[Priority(-7)]
	[Version("1.2.4")]
	public sealed class TemplateFactory : LibExtension
	{
		/// <summary>
		/// Load template from a file
		/// </summary>
		/// <param name="fileName">Template file name</param>
		/// <returns>Template class with loaded template</returns>
		public Template Load(string fileName)
		{
			var ev = Manager.Get<EnvironmentVariables>();

			return new Template(string.Format("{0}/{1}", ev.TemplatesPhysicalPath, fileName), EngineSettings.DefaultLanguage, ev.Language);
		}
	}
}
