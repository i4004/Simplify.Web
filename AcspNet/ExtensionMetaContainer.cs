using System;

namespace AcspNet
{
	/// <summary>
	/// Extension meta-data information container
	/// </summary>
	public class ExtensionMetaContainer
	{
		private readonly Type _extensionType;
		private readonly int _priority;
		private readonly string _version;

		/// <summary>
		/// Initializes a new instance of the <see cref="ExtensionMetaContainer"/> class.
		/// </summary>
		/// <param name="extensionType">Type of the extension.</param>
		/// <param name="priority">The priority.</param>
		/// <param name="version">The version.</param>
		public ExtensionMetaContainer(Type extensionType, int priority = 0, string version = "")
		{
			_extensionType = extensionType;
			_priority = priority;
			_version = version;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ExtensionMetaContainer"/> class.
		/// </summary>
		/// <param name="container">The container.</param>
		public ExtensionMetaContainer(ExtensionMetaContainer container)
		{
			_extensionType = container.ExtensionType;
			_priority = container.Priority;
			_version = container.Version;
		}

		/// <summary>
		/// Gets the type of the extension.
		/// </summary>
		/// <value>
		/// The type of the extension.
		/// </value>
		public Type ExtensionType
		{
			get { return _extensionType; }
		}

		/// <summary>
		/// Gets the priority.
		/// </summary>
		/// <value>
		/// The priority.
		/// </value>
		public int Priority
		{
			get { return _priority; }
		}

		/// <summary>
		/// Gets the version.
		/// </summary>
		/// <value>
		/// The version.
		/// </value>
		public string Version
		{
			get { return _version; }
		}
	}
}