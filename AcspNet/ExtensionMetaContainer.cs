using System;

namespace AcspNet
{
	public class ExtensionMetaContainer
	{
		private readonly Type _extensionType;
		private readonly int _priority;
		private readonly string _version;

		public ExtensionMetaContainer(Type extensionType, int priority = 0, string version = "")
		{
			_extensionType = extensionType;
			_priority = priority;
			_version = version;
		}

		public ExtensionMetaContainer(ExtensionMetaContainer container)
		{
			_extensionType = container.ExtensionType;
			_priority = container.Priority;
			_version = container.Version;
		}

		public Type ExtensionType
		{
			get { return _extensionType; }
		}

		public int Priority
		{
			get { return _priority; }
		}

		public string Version
		{
			get { return _version; }
		}
	}
}