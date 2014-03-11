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
		public void LogInSessionUser_RegularUser_DataSavedToSession()
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

			var sessions = new Dictionary<string, object>();
			session.Setup(x => x.Add(It.IsAny<string>(), It.IsAny<object>()))
				.Callback((string key, object value) =>
				{
					if (!sessions.ContainsKey(key))
						sessions.Add(key, value);
				});

			session.Setup(x => x[It.IsAny<string>()])
				.Returns((string key) => sessions.ContainsKey(key) ? sessions[key] : null);
			session.Setup(x => x.Remove(It.IsAny<string>())).Callback((string key) => sessions.Remove(key));

			session.Object.Add(SessionAuthentication.SessionUserAuthenticationStatusFieldName, "authenticated");
			session.Object.Add(SessionAuthentication.SessionUserIdFieldName, 5);

			var sa = new SessionAuthentication(session.Object, state.Object);

			sa.AuthenticateSessionUser();

			state.Verify(x => x.SetAuthenticated(It.Is<int>(c => c == 5), It.Is<string>(null)), Times.Once);
		}	
	}
}