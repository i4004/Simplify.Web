using System;
using System.Diagnostics;

namespace AcspNet
{
	/// <summary>
	/// Controller meta-data information container
	/// </summary>
	public class ControllerMetaContainer
	{
		private readonly Type _controllerType; 
		
		private readonly string _action;
		private readonly string _mode;
		private readonly int _runPriority;
		private readonly bool _isDefaultPageController;
		private readonly bool _isAjaxRequest;

		/// <summary>
		/// Initializes a new instance of the <see cref="ControllerMetaContainer" /> class.
		/// </summary>
		/// <param name="controllerType">Type of the controller.</param>
		/// <param name="action">The action.</param>
		/// <param name="mode">The mode.</param>
		/// <param name="runPriority">The run priority.</param>
		/// <param name="defaultPageController">if set to <c>true</c> then the controller will be launched only on default page.</param>
		/// <param name="isAjaxRequest">if set to <c>true</c> then indicates what controller handles ajax requests.</param>
		/// <param name="isHttpGet">if set to <c>true</c> then indicates what controller handler only HTTP GET requests.</param>
		/// <param name="isHttpPost">if set to <c>true</c>then indicates what controller handler only HTTP POST requests.</param>
		public ControllerMetaContainer(Type controllerType, string action = null, string mode = null, int runPriority = 0,
			bool defaultPageController = false, bool isAjaxRequest = false, bool isHttpGet = false, bool isHttpPost = false)
		{
			IsHttpGet = isHttpGet;
			IsHttpPost = isHttpPost;
			_controllerType = controllerType;
			_action = action;
			_mode = mode;
			_runPriority = runPriority;
			_isDefaultPageController = defaultPageController;
			_isAjaxRequest = isAjaxRequest;
		}

		/// <summary>
		/// Gets the type of the controller.
		/// </summary>
		/// <value>
		/// The type of the extension.
		/// </value>
		public Type ControllerType
		{
			get { return _controllerType; }
		}

		/// <summary>
		/// Gets the action.
		/// </summary>
		/// <value>
		/// The action.
		/// </value>
		public string Action
		{
			get { return _action; }
		}

		/// <summary>
		/// Gets the mode.
		/// </summary>
		/// <value>
		/// The mode.
		/// </value>
		public string Mode
		{
			get { return _mode; }
		}

		/// <summary>
		/// Gets the run priority.
		/// </summary>
		/// <value>
		/// The run priority.
		/// </value>
		public int RunPriority
		{
			get { return _runPriority; }
		}

		/// <summary>
		/// Gets a value indicating whether the controller will be run only on default page
		/// </summary>
		/// <value>
		///   <c>true</c> if the controller will be run only on default page; otherwise, <c>false</c>.
		/// </value>
		public bool IsDefaultPageController
		{
			get { return _isDefaultPageController; }
		}

		/// <summary>
		/// Gets a value indicating whether controller is handles ajax request.
		/// </summary>
		public bool IsAjaxRequest
		{
			get { return _isAjaxRequest; }
		}

		public bool IsHttpGet { get; set; }
		public bool IsHttpPost { get; set; }
	}
}