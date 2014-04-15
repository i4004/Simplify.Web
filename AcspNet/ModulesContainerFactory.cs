namespace AcspNet
{
	public class ModulesContainerFactory : IModulesContainerFactory
	{
		private readonly string _sitePhysicalPath;
		private readonly IAcspSettings _settings;
		private readonly string _language;

		internal ModulesContainerFactory(string sitePhysicalPath, IAcspSettings settings, string language)
		{
			_sitePhysicalPath = sitePhysicalPath;
			_settings = settings;
			_language = language;
		}

		public ModulesContainer CreateContainer()
		{
			var container = new ModulesContainer();
			var env = new Environment(_sitePhysicalPath, _settings);

			var fileReader = new FileReader(env.DataPath, _sitePhysicalPath, _language, _settings.DefaultLanguage);

			container.FileReader = fileReader;

			return container;
		}
	}
}