using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AcspNet.Extensions.Executable;

namespace AcspNet
{
	public sealed class AcspApplication : IAcspApplication
	{
		private static IAcspApplication CurrentApplication;

		private static Assembly MainAssemblyInstance;
		private static IAcspNetSettings SettingsInstance;

		private static List<ExecExtensionMetaContainer> ExecExtensionsMetaContainers = new List<ExecExtensionMetaContainer>();
		private static List<LibExtensionMetaContainer> LibExtensionsMetaContainers = new List<LibExtensionMetaContainer>();
		
		private AcspApplication()
		{
		}

		public static IAcspApplication Current
		{
			get
			{
				return CurrentApplication ?? (CurrentApplication = new AcspApplication());
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");

				CurrentApplication = value;
			}
		}

		public static Assembly MainAssembly
		{
			get
			{
				return MainAssemblyInstance ?? (MainAssemblyInstance = Assembly.GetCallingAssembly());
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");

				MainAssemblyInstance = value;
			}
		}

		public static IAcspNetSettings Settings
		{
			get
			{
				return SettingsInstance ?? (SettingsInstance = new AcspNetSettings());
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");

				SettingsInstance = value;
			}
		}
		
		///// <summary>
		///// Gets the System.Web.HttpRequest object for the current HTTP request
		///// </summary>
		//public HttpRequestBase Request
		//{
		//	get { return Context.Request; }
		//}

		/// <summary>
		/// Gets the web-site physical path, for example: C:\inetpub\wwwroot\YourSite
		/// </summary>
		/// <value>
		/// The site physical path.
		/// </value>
		public static string SitePhysicalPath { get; private set; }

		public static void Setup()
		{
			CreateMetaContainers(MainAssembly);

			InitializePaths();
		}

		private static void InitializePaths()
		{
			if (Request.PhysicalApplicationPath != null)
				AcspApplication.SitePhysicalPath = Request.PhysicalApplicationPath.Replace("\\", "/");
		}

		/// <summary>
		/// Get currently loaded executable extensions meta-data
		/// </summary>
		/// <returns></returns>
		public static IList<ExecExtensionMetaContainer> GetExecExtensionsMetaData()
		{
			return ExecExtensionsMetaContainers.ToArray();
		}

		/// <summary>
		/// Gets the library extensions meta data.
		/// </summary>
		/// <returns></returns>
		public static IList<LibExtensionMetaContainer> GetLibExtensionsMetaData()
		{
			return LibExtensionsMetaContainers.ToArray();
		}

		private static void CreateMetaContainers(Assembly callingAssembly)
		{
			var assemblyTypes = callingAssembly.GetTypes();

			var containingClass =
				assemblyTypes.FirstOrDefault(t => t.IsDefined(typeof(LoadExtensionsFromAssemblyOfAttribute), true)) ??
				assemblyTypes.FirstOrDefault(t => t.IsDefined(typeof(LoadIndividualExtensionsAttribute), true));

			if (containingClass == null)
				throw new AcspNetException("LoadExtensionsFromAssemblyOf attribute not found in AcspApplication.MainAssembly");

			var batchExtensionsAttributes = containingClass.GetCustomAttributes(typeof(LoadExtensionsFromAssemblyOfAttribute), false);
			var individualExtensionsAttributes = containingClass.GetCustomAttributes(typeof(LoadIndividualExtensionsAttribute), false);

			if (batchExtensionsAttributes.Length <= 1 && individualExtensionsAttributes.Length <= 1)
			{
				if (batchExtensionsAttributes.Length == 1)
					LoadExtensionsFromAssemblyOf(((LoadExtensionsFromAssemblyOfAttribute)batchExtensionsAttributes[0]).Types);

				var types = new Type[0];

				if (individualExtensionsAttributes.Length == 1)
					types = ((LoadIndividualExtensionsAttribute)individualExtensionsAttributes[0]).Types;

				if (!Settings.DisableAcspInternalExtensions)
					types = types.Concat(new List<Type> { typeof(MessagePageDisplay), typeof(ExtensionsProtector) }).ToArray();

				LoadIndividualExtensions(types);

				SortLibraryExtensionsMetaContainers();
				SortExecExtensionsMetaContainers();
			}
			else if (batchExtensionsAttributes.Length > 1)
				throw new Exception("Multiple LoadExtensionsFromAssemblyOf attributes found");
			else if (individualExtensionsAttributes.Length > 1)
				throw new Exception("Multiple LoadIndividualExtensions attributes found");
		}

		private static void LoadExtensionsFromAssemblyOf(params Type[] types)
		{
			foreach (var assemblyTypes in types.Select(classType => Assembly.GetAssembly(classType).GetTypes()))
			{
				foreach (var t in assemblyTypes.Where(t => t.BaseType != null && t.BaseType.FullName == "AcspNet.LibExtension"))
					AddLibExtensionMetaContainer(t);

				foreach (var t in assemblyTypes.Where(t => t.BaseType != null && t.BaseType.FullName == "AcspNet.ExecExtension"))
					AddExecExtensionMetaContainer(t);
			}
		}

		private static void LoadIndividualExtensions(params Type[] types)
		{
			foreach (var t in types.Where(t => t.BaseType != null && t.BaseType.FullName == "AcspNet.LibExtension").Where(t => LibExtensionsMetaContainers.All(x => x.ExtensionType != t)))
				AddLibExtensionMetaContainer(t);

			foreach (var t in types.Where(t => t.BaseType != null && t.BaseType.FullName == "AcspNet.ExecExtension").Where(t => ExecExtensionsMetaContainers.All(x => x.ExtensionType != t)))
				AddExecExtensionMetaContainer(t);
		}

		private static void AddLibExtensionMetaContainer(Type extensionType)
		{
			LibExtensionsMetaContainers.Add(new LibExtensionMetaContainer(CreateExtensionMetaContainer(extensionType)));
		}

		private static ExtensionMetaContainer CreateExtensionMetaContainer(Type extensionType)
		{
			var priority = 0;
			var version = "";

			var attributes = extensionType.GetCustomAttributes(typeof(PriorityAttribute), false);

			if (attributes.Length > 0)
				priority = ((PriorityAttribute)attributes[0]).Priority;

			attributes = extensionType.GetCustomAttributes(typeof(VersionAttribute), false);

			if (attributes.Length > 0)
				version = ((VersionAttribute)attributes[0]).Version;

			return new ExtensionMetaContainer(extensionType, priority, version);
		}

		private static void AddExecExtensionMetaContainer(Type extensionType)
		{
			var action = "";
			var mode = "";
			var runType = RunType.OnAction;

			var attributes = extensionType.GetCustomAttributes(typeof(ActionAttribute), false);

			if (attributes.Length > 0)
				action = ((ActionAttribute)attributes[0]).Action;

			attributes = extensionType.GetCustomAttributes(typeof(ModeAttribute), false);

			if (attributes.Length > 0)
				mode = ((ModeAttribute)attributes[0]).Mode;

			attributes = extensionType.GetCustomAttributes(typeof(RunTypeAttribute), false);

			if (attributes.Length > 0)
				runType = ((RunTypeAttribute)attributes[0]).RunType;

			ExecExtensionsMetaContainers.Add(new ExecExtensionMetaContainer(CreateExtensionMetaContainer(extensionType), action, mode, runType));
		}

		private static void SortLibraryExtensionsMetaContainers()
		{
			LibExtensionsMetaContainers = LibExtensionsMetaContainers.OrderBy(x => x.Priority).ToList();
		}

		private static void SortExecExtensionsMetaContainers()
		{
			ExecExtensionsMetaContainers = ExecExtensionsMetaContainers.OrderBy(x => x.Priority).ToList();
		}
	}
}