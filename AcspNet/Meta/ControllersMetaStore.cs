using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AcspNet.Modules.Controllers;

namespace AcspNet.Meta
{
	/// <summary>
	/// Loads and stores AcspNet controllers meta information
	/// </summary>
	public sealed class ControllersMetaStore : IControllersMetaStore
	{
		private static IList<string> _excludedAssembliesPrefixes = new List<string> { "mscorlib", "System", "Microsoft", "AspNet", "AcspNet", "DotNet", "Simplify" };
		private List<ControllerMetaContainer> _controllersMetaContainers = new List<ControllerMetaContainer>();

		/// <summary>
		/// Initializes a new instance of the <see cref="ControllersMetaStore"/> class.
		/// </summary>
		/// <param name="disableAcspInternalControllers">if set to <c>true</c> [disable acsp internal controllers].</param>
		public ControllersMetaStore(bool disableAcspInternalControllers = false)
		{
			CreateControllersMetaContainers(AppDomain.CurrentDomain.GetAssemblies(), disableAcspInternalControllers);
		}

		/// <summary>
		/// Gets or sets the excluded assemblies prefixes.
		/// </summary>
		/// <value>
		/// The excluded assemblies prefixes.
		/// </value>
		/// <exception cref="System.ArgumentNullException">value</exception>
		public static IList<string> ExcludedAssembliesPrefixes
		{
			get { return _excludedAssembliesPrefixes; }
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");

				_excludedAssembliesPrefixes = value;
			}
		}

		/// <summary>
		/// Get currently loaded executable extensions meta-data
		/// </summary>
		/// <returns></returns>
		public IList<ControllerMetaContainer> GetControllersMetaData()
		{
			return _controllersMetaContainers.AsReadOnly();
		}

		private static ControllerExecParameters GetControllerExecPatameters(Type controllerType)
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

			return !string.IsNullOrEmpty(action) || priority != 0 || runOnDefaultPage || ajaxRequest
				? new ControllerExecParameters(action, mode, priority, runOnDefaultPage, ajaxRequest)
				: null;
		}

		private static ControllerSecurity GetControllerSecurity(Type controllerType)
		{
			var authenticationRequired = false;
			var httpGet = false;
			var httpPost = false;

			var attributes = controllerType.GetCustomAttributes(typeof(AuthenticationRequiredAttribute), false);

			if (attributes.Length > 0)
				authenticationRequired = true;
			
			attributes = controllerType.GetCustomAttributes(typeof(HttpGetAttribute), false);

			if (attributes.Length > 0)
				httpGet = true;

			attributes = controllerType.GetCustomAttributes(typeof(HttpPostAttribute), false);

			if (attributes.Length > 0)
				httpPost = true;

			return authenticationRequired || httpGet || httpPost ? new ControllerSecurity(authenticationRequired, httpGet, httpPost) : null;
		}

		private static ControllerRole GetControllerRole(Type controllerType)
		{
			var http400 = false;
			var http403 = false;
			var http404 = false;

			var attributes = controllerType.GetCustomAttributes(typeof(Http400Attribute), false);

			if (attributes.Length > 0)
				http400 = true;

			attributes = controllerType.GetCustomAttributes(typeof(Http403Attribute), false);

			if (attributes.Length > 0)
				http403 = true;

			attributes = controllerType.GetCustomAttributes(typeof(Http404Attribute), false);

			if (attributes.Length > 0)
				http404 = true;

			return http403 || http404 ? new ControllerRole(http400, http403, http404) : null;
		}

		private void CreateControllersMetaContainers(IEnumerable<Assembly> assemblies, bool disableAcspInternalControllers)
		{
			var typesToIgnore = new List<Type>();
			var types = GetAssembliesTypes(assemblies);

			var ignoreContainingClass = types.FirstOrDefault(t => t.IsDefined(typeof(IgnoreControllersAttribute), true));

			if (ignoreContainingClass != null)
			{
				var attributes = ignoreContainingClass.GetCustomAttributes(typeof(IgnoreControllersAttribute), false);

				typesToIgnore.AddRange(((IgnoreControllersAttribute)attributes[0]).Types);
			}

			if (!disableAcspInternalControllers)
				types = types.Concat(new List<Type> {typeof (MessagePageDisplay)}).ToArray();

			LoadMetaData(types, typesToIgnore);
			SortControllersMetaContainers();
		}

		private static ICollection<Type> GetAssembliesTypes(IEnumerable<Assembly> assemblies)
		{
			var types = new List<Type>();

			foreach (var assembly in assemblies.Where(assembly => !ExcludedAssembliesPrefixes.Any(prefix => assembly.FullName.StartsWith(prefix))))
				types.AddRange(assembly.GetTypes());

			return types;
		}

		private void LoadMetaData(IEnumerable<Type> types, IEnumerable<Type> typesToIgnore)
		{
			foreach (var t in types.Where(t => t.BaseType != null && t.BaseType.FullName == "AcspNet.Controller"
				&& typesToIgnore.All(x => x.FullName != t.FullName) && _controllersMetaContainers.All(x => x.ControllerType != t)))
				AddControllerMetaContainer(t);
		}

		private void AddControllerMetaContainer(Type controllerType)
		{
			_controllersMetaContainers.Add(new ControllerMetaContainer(controllerType,
				GetControllerExecPatameters(controllerType), GetControllerSecurity(controllerType), GetControllerRole(controllerType)));
		}

		private void SortControllersMetaContainers()
		{
			_controllersMetaContainers = _controllersMetaContainers.OrderBy(x => x.ExecParameters == null ? 0 : x.ExecParameters.RunPriority).ToList();
		}
	}
}