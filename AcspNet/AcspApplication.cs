using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AcspNet
{
	public sealed class AcspApplication : IAcspApplication
	{
		private static IAcspApplication CurrentApplication;

		private readonly List<ExecExtensionMetaContainer> _execExtensionsMetaContainers = new List<ExecExtensionMetaContainer>();
		private readonly List<LibExtensionMetaContainer> _libExtensionsMetaContainers = new List<LibExtensionMetaContainer>();

		public static IAcspApplication Current
		{
			get
			{
				return CurrentApplication;
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");

				CurrentApplication = value;
			}
		}

		/// <summary>
		/// Get currently loaded executable extensions meta-data
		/// </summary>
		/// <returns></returns>
		public IList<ExecExtensionMetaContainer> GetExecExtensionsMetaData()
		{
			return _execExtensionsMetaContainers.ToArray();
		}

		/// <summary>
		/// Gets the library extensions meta data.
		/// </summary>
		/// <returns></returns>
		public IList<LibExtensionMetaContainer> GetLibExtensionsMetaData()
		{
			return _libExtensionsMetaContainers.ToArray();
		}

		private static void CreateMetaContainers(Assembly callingAssembly)
		{
			//var assemblyTypes = callingAssembly.GetTypes();

			//var containingClass =
			//	assemblyTypes.FirstOrDefault(t => t.IsDefined(typeof(LoadExtensionsFromAssemblyOfAttribute), true)) ??
			//	assemblyTypes.FirstOrDefault(t => t.IsDefined(typeof(LoadIndividualExtensionsAttribute), true));

			//	if (containingClass == null)
			//		throw new AcspNetException("LoadExtensionsFromAssemblyOf attribute not found in your classes");

			//	var batchExtensionsAttributes = containingClass.GetCustomAttributes(typeof(LoadExtensionsFromAssemblyOfAttribute), false);
			//	var individualExtensionsAttributes = containingClass.GetCustomAttributes(typeof(LoadIndividualExtensionsAttribute), false);

			//	if (batchExtensionsAttributes.Length <= 1 && individualExtensionsAttributes.Length <= 1)
			//	{
			//		if (batchExtensionsAttributes.Length == 1)
			//			LoadExtensionsFromAssemblyOf(((LoadExtensionsFromAssemblyOfAttribute)batchExtensionsAttributes[0]).Types);

			//		var types = new Type[0];

			//		if (individualExtensionsAttributes.Length == 1)
			//			types = ((LoadIndividualExtensionsAttribute)individualExtensionsAttributes[0]).Types;

			//		if (!AcspNetSettingsInstance.Value.DisableAcspInternalExtensions)
			//			types = types.Concat(new List<Type> { typeof(MessagePageDisplay), typeof(ExtensionsProtector) }).ToArray();

			//		LoadIndividualExtensions(types);

			//		SortLibraryExtensionsMetaContainers();
			//		SortExecExtensionsMetaContainers();
			//	}
			//	else if (batchExtensionsAttributes.Length > 1)
			//		throw new Exception("Multiple LoadExtensionsFromAssemblyOf attributes found");
			//	else if (individualExtensionsAttributes.Length > 1)
			//		throw new Exception("Multiple LoadIndividualExtensions attributes found");
		}
	}
}