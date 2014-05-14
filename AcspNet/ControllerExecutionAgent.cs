using System;
using AcspNet.Meta;
using AcspNet.Modules.Identity;

namespace AcspNet
{
	public class ControllerExecutionAgent : IControllerExecutionAgent
	{
		private readonly string _currentAction;
		private readonly string _currentMode;
		private readonly string _httpMethod;
		private readonly IAuthenticationState _authenticationState;

		/// <summary>
		/// Initializes a new instance of the <see cref="ControllerExecutor"/> class.
		/// </summary>
		/// <param name="authenticationState">State of the authentication.</param>
		/// <param name="currentAction">The current action.</param>
		/// <param name="currentMode">The current mode.</param>
		/// <param name="httpMethod">The HTTP method.</param>
		public ControllerExecutionAgent(IAuthenticationState authenticationState, string currentAction = null, string currentMode = null, string httpMethod = null)
		{
			_currentAction = currentAction;
			_currentMode = currentMode;
			_httpMethod = httpMethod;
			_authenticationState = authenticationState;
		}

		/// <summary>
		/// Determines whether controller is not executed on any page.
		/// </summary>
		/// <param name="metaContainer">The controller meta container.</param>
		/// <returns></returns>
		public bool IsNonAnyPageController(ControllerMetaContainer metaContainer)
		{
			if (metaContainer.ExecParameters == null)
				return false;

			if (metaContainer.ExecParameters.IsDefaultPageController)
				return true;

			return !string.IsNullOrEmpty(metaContainer.ExecParameters.Action);
		}

		/// <summary>
		/// Determines whether controller can be executed on current page.
		/// </summary>
		/// <param name="metaContainer">The controller meta container.</param>
		/// <returns></returns>
		public bool IsControllerCanBeExecutedOnCurrentPage(ControllerMetaContainer metaContainer)
		{
			// Is default page
			if (string.IsNullOrEmpty(_currentAction) && string.IsNullOrEmpty(_currentMode))
			{
				// Is any page controller
				if (metaContainer.ExecParameters == null)
					return true;

				// Is Default page controller
				if (metaContainer.ExecParameters.IsDefaultPageController)
					return true;

				// Is any page controller
				if (string.IsNullOrEmpty(metaContainer.ExecParameters.Action))
					return true;
			}
			else
			{
				// Is any page controller
				if (metaContainer.ExecParameters == null)
					return true;

				if (!metaContainer.ExecParameters.IsDefaultPageController)
				{
					// Is any page controller
					if (string.IsNullOrEmpty(metaContainer.ExecParameters.Action))
						return true;

					// Is exact action mode controller and not default page controller
					if (String.Equals(metaContainer.ExecParameters.Action, _currentAction, StringComparison.CurrentCultureIgnoreCase) &&
					String.Equals(metaContainer.ExecParameters.Mode, _currentMode, StringComparison.CurrentCultureIgnoreCase))
						return true;
				}
			}

			return false;
		}

		/// <summary>
		/// Determines whether controller security rules violated.
		/// </summary>
		/// <param name="metaContainer">The controller meta container.</param>
		/// <returns></returns>
		public SecurityViolationResult IsSecurityRulesViolated(ControllerMetaContainer metaContainer)
		{
			// If there is no security
			if (metaContainer.Security == null)
				return SecurityViolationResult.Ok;

			if (metaContainer.Security.IsAuthenticationRequired && !_authenticationState.IsAuthenticatedAsUser)
				return SecurityViolationResult.AuthenticationRequired;

			if (metaContainer.Security.IsHttpGet && _httpMethod != "GET")
				return SecurityViolationResult.RequestTypeViolated;

			if (metaContainer.Security.IsHttpPost && _httpMethod != "POST")
				return SecurityViolationResult.RequestTypeViolated;

			return SecurityViolationResult.Ok;
		}
	}
}