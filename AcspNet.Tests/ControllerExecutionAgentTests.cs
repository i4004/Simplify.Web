using AcspNet.Meta;
using AcspNet.Modules.Identity;
using Moq;
using NUnit.Framework;

namespace AcspNet.Tests
{
	[TestFixture]
	public class ControllerExecutionAgentTests
	{
		[Test]
		public void IsNonAnyPageController_DefaultController_True()
		{
			// Arrange

			var agent = new ControllerExecutor(new Mock<IAuthenticationState>().Object);
			var metaContainer = new ControllerMetaContainer(null,
				new ControllerExecParameters(null, null, 0, true));
			var metaContainer2 = new ControllerMetaContainer(null,
				new ControllerExecParameters("foo", "bar", 0, true));

			// Act & Assert

			Assert.IsTrue(agent.IsNonAnyPageController(metaContainer));
			Assert.IsTrue(agent.IsNonAnyPageController(metaContainer2));
		}

		[Test]
		public void IsNonAnyPageController_SpecificPageController_True()
		{
			// Arrange

			var agent = new ControllerExecutor(new Mock<IAuthenticationState>().Object);
			var metaContainer = new ControllerMetaContainer(null,
				new ControllerExecParameters("foo", "bar"));
			var metaContainer2 = new ControllerMetaContainer(null,
				new ControllerExecParameters("foo"));

			// Act & Assert

			Assert.IsTrue(agent.IsNonAnyPageController(metaContainer));
			Assert.IsTrue(agent.IsNonAnyPageController(metaContainer2));
		}

		[Test]
		public void IsNonAnyPageController_AnyPageController_False()
		{
			// Arrange

			var agent = new ControllerExecutor(new Mock<IAuthenticationState>().Object);
			var metaContainer = new ControllerMetaContainer(null);
			var metaContainer2 = new ControllerMetaContainer(null, new ControllerExecParameters());
			var metaContainer3 = new ControllerMetaContainer(null, new ControllerExecParameters(""));

			// Act & Assert

			Assert.IsFalse(agent.IsNonAnyPageController(metaContainer));
			Assert.IsFalse(agent.IsNonAnyPageController(metaContainer2));
			Assert.IsFalse(agent.IsNonAnyPageController(metaContainer3));
		}

		[Test]
		public void IsControllerCanBeExecutedOnCurrentPage_DefaultPage_CorrectControllersResult()
		{
			// Arrange

			var agent = new ControllerExecutor(new Mock<IAuthenticationState>().Object);
			var metaContainer = new ControllerMetaContainer(null,
				new ControllerExecParameters(null, null, 0, true));
			var metaContainer2 = new ControllerMetaContainer(null,
				new ControllerExecParameters("foo", "bar", 0, true));
			var metaContainer3 = new ControllerMetaContainer(null,
				new ControllerExecParameters("foo", "bar"));
			var metaContainer4 = new ControllerMetaContainer(null);
			var metaContainer5 = new ControllerMetaContainer(null, new ControllerExecParameters());

			// Act & Assert

			Assert.IsTrue(agent.IsControllerCanBeExecutedOnCurrentPage(metaContainer));
			Assert.IsTrue(agent.IsControllerCanBeExecutedOnCurrentPage(metaContainer2));
			Assert.IsFalse(agent.IsControllerCanBeExecutedOnCurrentPage(metaContainer3));
			Assert.IsTrue(agent.IsControllerCanBeExecutedOnCurrentPage(metaContainer4));
			Assert.IsTrue(agent.IsControllerCanBeExecutedOnCurrentPage(metaContainer5));
		}

		[Test]
		public void IsControllerCanBeExecutedOnCurrentPage_SpecifiedPage_CorrectControllersResult()
		{
			// Arrange

			var agent = new ControllerExecutor(new Mock<IAuthenticationState>().Object, "foo", "bar");
			var metaContainer = new ControllerMetaContainer(null,
				new ControllerExecParameters(null, null, 0, true));
			var metaContainer2 = new ControllerMetaContainer(null,
				new ControllerExecParameters("foo", "bar", 0, true));
			var metaContainer3 = new ControllerMetaContainer(null,
				new ControllerExecParameters("foo", "bar"));
			var metaContainer4 = new ControllerMetaContainer(null);
			var metaContainer5 = new ControllerMetaContainer(null,
				new ControllerExecParameters("foo"));
			var metaContainer6 = new ControllerMetaContainer(null, new ControllerExecParameters());

			// Act & Assert

			Assert.IsFalse(agent.IsControllerCanBeExecutedOnCurrentPage(metaContainer));
			Assert.IsFalse(agent.IsControllerCanBeExecutedOnCurrentPage(metaContainer2));
			Assert.IsTrue(agent.IsControllerCanBeExecutedOnCurrentPage(metaContainer3));
			Assert.IsTrue(agent.IsControllerCanBeExecutedOnCurrentPage(metaContainer4));
			Assert.IsFalse(agent.IsControllerCanBeExecutedOnCurrentPage(metaContainer5));
			Assert.IsTrue(agent.IsControllerCanBeExecutedOnCurrentPage(metaContainer6));
		}

		[Test]
		public void IsControllerCanBeExecutedOnCurrentPage_SpecifiedPageOnyAction_CorrectControllersResult()
		{
			// Arrange

			var agent = new ControllerExecutor(new Mock<IAuthenticationState>().Object, "foo");
			var metaContainer = new ControllerMetaContainer(null,
				new ControllerExecParameters("foo", "bar"));
			var metaContainer2 = new ControllerMetaContainer(null);
			var metaContainer3 = new ControllerMetaContainer(null,
				new ControllerExecParameters("foo"));

			// Act & Assert

			Assert.IsFalse(agent.IsControllerCanBeExecutedOnCurrentPage(metaContainer));
			Assert.IsTrue(agent.IsControllerCanBeExecutedOnCurrentPage(metaContainer2));
			Assert.IsTrue(agent.IsControllerCanBeExecutedOnCurrentPage(metaContainer3));
		}

		[Test]
		public void IsSecurityRulesViolated_NoSecurityRules_Ok()
		{
			// Arrange

			var agent = new ControllerExecutor(new Mock<IAuthenticationState>().Object);
			var metaContainer = new ControllerMetaContainer(null);
			var metaContainer2 = new ControllerMetaContainer(null, null, new ControllerSecurity());

			// Act & Assert

			Assert.AreEqual(SecurityViolationResult.Ok, agent.IsSecurityRulesViolated(metaContainer));
			Assert.AreEqual(SecurityViolationResult.Ok, agent.IsSecurityRulesViolated(metaContainer2));
		}

		[Test]
		public void IsSecurityRulesViolated_SecurityRulesSetNoHttpParameters_SecurityViolated()
		{
			// Arrange

			var agent = new ControllerExecutor(new Mock<IAuthenticationState>().Object);
			var metaContainer = new ControllerMetaContainer(null, null, new ControllerSecurity(true));
			var metaContainer2 = new ControllerMetaContainer(null, null, new ControllerSecurity(false, true));
			var metaContainer3 = new ControllerMetaContainer(null, null, new ControllerSecurity(false, false, true));

			// Act & Assert

			Assert.AreEqual(SecurityViolationResult.AuthenticationRequired, agent.IsSecurityRulesViolated(metaContainer));
			Assert.AreEqual(SecurityViolationResult.RequestTypeViolated, agent.IsSecurityRulesViolated(metaContainer2));
			Assert.AreEqual(SecurityViolationResult.RequestTypeViolated, agent.IsSecurityRulesViolated(metaContainer3));
		}

		[Test]
		public void IsSecurityRulesViolated_SecurityRulesSetAndHttpPostGetParametersIsCorrect_Ok()
		{
			// Arrange

			var agent = new ControllerExecutor(new Mock<IAuthenticationState>().Object, null, null, "GET");
			var agent2 = new ControllerExecutor(new Mock<IAuthenticationState>().Object, null, null, "POST");

			var metaContainer = new ControllerMetaContainer(null, null, new ControllerSecurity(false, true));
			var metaContainer2 = new ControllerMetaContainer(null, null, new ControllerSecurity(false, false, true));

			// Act & Assert

			Assert.AreEqual(SecurityViolationResult.Ok, agent.IsSecurityRulesViolated(metaContainer));
			Assert.AreEqual(SecurityViolationResult.Ok, agent2.IsSecurityRulesViolated(metaContainer2));
		}

		[Test]
		public void IsSecurityRulesViolated_AuthenticationRequiredUserNotAuthenticated_AuthenticationRequiredResult()
		{
			// Arrange

			var state = new Mock<IAuthenticationState>();
			state.SetupGet(x => x.IsAuthenticatedAsUser).Returns(false);

			var agent = new ControllerExecutor(state.Object);

			var metaContainer = new ControllerMetaContainer(null, null, new ControllerSecurity(true));

			// Act & Assert

			Assert.AreEqual(SecurityViolationResult.AuthenticationRequired, agent.IsSecurityRulesViolated(metaContainer));
		}

		[Test]
		public void IsSecurityRulesViolated_AuthenticationRequiredUserAuthenticated_Ok()
		{
			// Arrange

			var state = new Mock<IAuthenticationState>();
			state.SetupGet(x => x.IsAuthenticatedAsUser).Returns(true);

			var agent = new ControllerExecutor(state.Object);

			var metaContainer = new ControllerMetaContainer(null, null, new ControllerSecurity(true));

			// Act & Assert

			Assert.AreEqual(SecurityViolationResult.Ok, agent.IsSecurityRulesViolated(metaContainer));
		}
	}
}