using System.Collections.Generic;
using System.Web;
using AcspNet.Authentication;
using Moq;
using NUnit.Framework;

namespace AcspNet.Tests
{
	[TestFixture]
	public class SessionAuthenticationTests
	{
		[Test]
		public void LogInSessionUser_RegularUser_DataAddedToSession()
		{
			var session = new Mock<HttpSessionStateBase>();
			var state = new Mock<IAuthenticationState>();

			var sa = new SessionAuthentication(session.Object, state.Object);

			sa.LogInSessionUser(5);

			session.Verify(
				x => x.Add(It.Is<string>(c => c == SessionAuthentication.SessionUserAuthenticationStatusFieldName), It.Is<object>(c => (string) c == "authenticated")),
				Times.Once());

			session.Verify(
				x => x.Add(It.Is<string>(c => c == SessionAuthentication.SessionUserIdFieldName), It.Is<object>(c => (int) c == 5)),
				Times.Once());
		}

		[Test]
		public void LogOutSessionUser_RegularUser_DataRemovedFromSession()
		{
			var session = new Mock<HttpSessionStateBase>();
			var state = new Mock<IAuthenticationState>();

			var sa = new SessionAuthentication(session.Object, state.Object);

			sa.LogOutSessionUser();

			session.Verify(
				x => x.Remove(It.Is<string>(c => c == SessionAuthentication.SessionUserAuthenticationStatusFieldName)),
				Times.Once());

			session.Verify(
				x => x.Remove(It.Is<string>(c => c == SessionAuthentication.SessionUserIdFieldName)),
				Times.Once());

			state.Verify(x => x.Reset(), Times.Once);
		}

		[Test]
		public void AuthenticateSessionUser_LoggedUser_DataRemovedFromSession()
		{
			var session = new Mock<HttpSessionStateBase>();
			var state = new Mock<IAuthenticationState>();

			session.Setup(x => x[It.Is<string>(c => c == SessionAuthentication.SessionUserAuthenticationStatusFieldName)])
				.Returns("authenticated");
			session.Setup(x => x[It.Is<string>(c => c == SessionAuthentication.SessionUserIdFieldName)])
				.Returns(5);

			//session.Object.Add(SessionAuthentication.SessionUserAuthenticationStatusFieldName, "authenticated");
			//session.Object.Add(SessionAuthentication.SessionUserIdFieldName, 5);

			var sa = new SessionAuthentication(session.Object, state.Object);

			sa.AuthenticateSessionUser();

			state.Verify(x => x.SetAuthenticated(It.Is<int>(c => c == 5), It.IsAny<string>()), Times.Once);
		}

		[Test]
		public void AuthenticateSessionUser_NotLoggedUser_DataRemovedFromSession()
		{
			var session = new Mock<HttpSessionStateBase>();
			var state = new Mock<IAuthenticationState>();

			var sa = new SessionAuthentication(session.Object, state.Object);

			sa.AuthenticateSessionUser();

			state.Verify(x => x.SetAuthenticated(It.IsAny<int>(), It.IsAny<string>()), Times.Never);
		}
	}
}