using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AcspNet
{
	/// <summary>
	/// Loads and stores AcspNet controllers meta information
	/// </summary>
	public sealed class ControllersMetaStore : IControllersMetaStore
	{
		private static IControllersMetaStore _currentApplication;

		private List<ControllerMetaContainer> _controllersMetaContainers = new List<ControllerMetaContainer>();

		/// <summary>
		/// Gets or sets the current ACSP application instance.
		/// </summary>
		/// <value>
		/// The current.
		/// </value>
		/// <exception cref="System.ArgumentNullException">value</exception>
		public static IControllersMetaStore Current
		{
			get
			{
				return _currentApplication ?? (_currentApplication = new ControllersMetaStore());
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");

				_currentApplication = value;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ControllersMetaStore"/> class.
		/// </summary>
		/// <param name="disableAcspInternalControllers">if set to <c>true</c> [disable acsp internal controllers].</param>
		public ControllersMetaStore(bool disableAcspInternalControllers = false)
		{
			var mainAssembly = Assembly.GetCallingAssembly();

			CreateControllersMetaContainers(mainAssembly, disableAcspInternalControllers);
		}

		/// <summary>
		/// Get currently loaded executable extensions meta-data
		/// </summary>
		/// <returns></returns>
		public IList<ControllerMetaContainer> GetControllersMetaData()
		{
			return _controllersMetaContainers.AsReadOnly();
		}

		private void CreateControllersMetaContainers(Assembly callingAssembly, bool disableAcspInternalControllers)
		{
			var assemblyTypes = callingAssembly.GetTypes();

			var containingClass =
				assemblyTypes.FirstOrDefault(t => t.IsDefined(typeof(LoadControllersFromAssemblyOfAttribute), true)) ??
				assemblyTypes.FirstOrDefault(t => t.IsDefined(typeof(LoadIndividualControllersAttribute), true));

			if (containingClass == null)
				throw new AcspException("LoadControllersFromAssemblyOfAttribute or LoadIndividualControllersAttribute attributes are not found in calling assembly");

			var batchControllersAttributes = containingClass.GetCustomAttributes(typeof(LoadControllersFromAssemblyOfAttribute), false);
			var individualControllersAttributes = containingClass.GetCustomAttributes(typeof(LoadIndividualControllersAttribute), false);

				if (batchControllersAttributes.Length >= 1)
					LoadControllersFromAssemblyOf(((LoadControllersFromAssemblyOfAttribute)batchControllersAttributes[0]).Types);

				var types = new Type[0];

				if (individualControllersAttributes.Length >= 1)
					types = ((LoadIndividualControllersAttribute)individualControllersAttributes[0]).Types;

				// todo
				//if (!disableAcspInternalControllers)
				//	types = types.Concat(new List<Type> { typeof(MessagePageDisplay), typeof(ExtensionsProtector) }).ToArray();

				LoadIndividualControllers(types);

				SortControllersMetaContainers();
		}

		private void LoadControllersFromAssemblyOf(params Type[] types)
		{
			foreach (var assemblyTypes in types.Select(classType => Assembly.GetAssembly(classType).GetTypes()))
				foreach (var t in assemblyTypes.Where(t => t.BaseType != null && t.BaseType.FullName == "AcspNet.Controller"))
					AddControllerMetaContainer(t);
		}

		private void LoadIndividualControllers(params Type[] types)
		{
			foreach (var t in types.Where(t => t.BaseType != null && t.BaseType.FullName == "AcspNet.ExecExtension").Where(t => _controllersMetaContainers.All(x => x.ControllerType != t)))
				AddControllerMetaContainer(t);
		}

		private void AddControllerMetaContainer(Type controllerType)
		{
			string action = null;
			string mode = null;
			var priority = 0;
			var runOnDefaultPage = false;
			var ajaxRequest = false;

			var attributes = controllerType.GetCustomAttributes(typeof(ActionAttribute), false);

			if (attributes.Length > 0)
				action = ((ActionAttribute)attributes[0]).Action;

			attributes = controllerType.GetCustomAttributes(typeof(ModeAttribute), false);

			if (attributes.Length > 0)
				mode = ((ModeAttribute)attributes[0]).Mode;

			attributes = controllerType.GetCustomAttributes(typeof(PriorityAttribute), false);

			if (attributes.Length > 0)
				priority = ((PriorityAttribute)attributes[0]).Priority;

			attributes = controllerType.GetCustomAttributes(typeof(DefaultPageAttribute), false);

			if (attributes.Length > 0)
				runOnDefaultPage = true;

			attributes = controllerType.GetCustomAttributes(typeof(AjaxAttribute), false);

			if (attributes.Length > 0)
				ajaxRequest = true;

			_controllersMetaContainers.Add(new ControllerMetaContainer(controllerType, action, mode, priority, runOnDefaultPage, ajaxRequest));
		}

		private void SortControllersMetaContainers()
		{
			_controllersMetaContainers = _controllersMetaContainers.OrderBy(x => x.RunPriority).ToList();
		}
	}
}