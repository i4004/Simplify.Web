using System.Collections.Specialized;
using AcspNet.Html;
using Moq;
using NUnit.Framework;

namespace AcspNet.Tests
{
	[TestFixture]
	public class IdVerifierTests
	{
		[Test]
		public void VeiryQueryStringID_NullID_MessageBoxDisplayed()
		{
			var mb = new Mock<IMessageBox>();
			var queryString = new NameValueCollection();

			var verifier = new IdVerifier(queryString, null, mb.Object);

			var id = verifier.VeiryQueryStringID();

			Assert.IsNull(id);

			mb.Verify(x => x.ShowSt(It.Is<string>(c => c == "NotifyPageDataError"), It.Is<MessageBoxStatus>(c => c == MessageBoxStatus.Error), It.IsAny<string>()), Times.Once);
		}

		[Test]
		public void VeiryQueryStringID_InvalidID_MessageBoxDisplayed()
		{
			var mb = new Mock<IMessageBox>();
			var queryString = new NameValueCollection();

			queryString.Add("ID", "test");

			var verifier = new IdVerifier(queryString, null, mb.Object);

			var id = verifier.VeiryQueryStringID();

			Assert.IsNull(id);

			mb.Verify(x => x.ShowSt(It.Is<string>(c => c == "NotifyPageDataError"), It.Is<MessageBoxStatus>(c => c == MessageBoxStatus.Error), It.IsAny<string>()), Times.Once);
		}

		[Test]
		public void VeiryQueryStringID_ValidID_MessageBoxDisplayed()
		{
			var mb = new Mock<IMessageBox>();
			var queryString = new NameValueCollection();

			queryString.Add("ID", "5");

			var verifier = new IdVerifier(queryString, null, mb.Object);

			var id = verifier.VeiryQueryStringID();

			Assert.AreEqual(5, id);

			mb.Verify(x => x.ShowSt(It.Is<string>(c => c == "NotifyPageDataError"), It.Is<MessageBoxStatus>(c => c == MessageBoxStatus.Error), It.IsAny<string>()), Times.Never);
		}

		[Test]
		public void CheckAndGetFormID_NullID_MessageBoxDisplayed()
		{
			var mb = new Mock<IMessageBox>();
			var form = new NameValueCollection();

			var verifier = new IdVerifier(null, form, mb.Object);

			var id = verifier.VerifyFormID();

			Assert.IsNull(id);

			mb.Verify(x => x.ShowSt(It.Is<string>(c => c == "NotifyPageDataError"), It.Is<MessageBoxStatus>(c => c == MessageBoxStatus.Error), It.IsAny<string>()), Times.Once);
		}

		[Test]
		public void CheckAndGetFormID_InvalidID_MessageBoxDisplayed()
		{
			var mb = new Mock<IMessageBox>();
			var form = new NameValueCollection();

			form.Add("ID", "test");

			var verifier = new IdVerifier(null, form, mb.Object);

			var id = verifier.VerifyFormID();

			Assert.IsNull(id);

			mb.Verify(x => x.ShowSt(It.Is<string>(c => c == "NotifyPageDataError"), It.Is<MessageBoxStatus>(c => c == MessageBoxStatus.Error), It.IsAny<string>()), Times.Once);
		}

		[Test]
		public void CheckAndGetFormID_ValidID_MessageBoxDisplayed()
		{
			var mb = new Mock<IMessageBox>();
			var form = new NameValueCollection();

			form.Add("ID", "5");

			var verifier = new IdVerifier(null, form, mb.Object);

			var id = verifier.VerifyFormID();

			Assert.AreEqual(5, id);

			mb.Verify(x => x.ShowSt(It.Is<string>(c => c == "NotifyPageDataError"), It.Is<MessageBoxStatus>(c => c == MessageBoxStatus.Error), It.IsAny<string>()), Times.Never);
		}

		[Test]
		public void VerifyQueryStringIdAjax_NullID_MessageBoxDisplayed()
		{
			var mb = new Mock<IMessageBox>();
			var queryString = new NameValueCollection();

			mb.Setup(x => x.GetInlineSt(It.Is<string>(c => c == "NotifyPageDataError"), It.Is<MessageBoxStatus>(c => c == MessageBoxStatus.Error))).Returns("FooData");

			var verifier = new IdVerifier(queryString, null, mb.Object);

			var id = verifier.VerifyQueryStringIdAjax();

			Assert.IsNull(id);

			mb.Verify(x => x.GetInlineSt(It.Is<string>(c => c == "NotifyPageDataError"), It.Is<MessageBoxStatus>(c => c == MessageBoxStatus.Error)), Times.Once);
			Assert.AreEqual("FooData", verifier.MessageBoxToDisplay);
		}

		[Test]
		public void VerifyQueryStringIdAjax_InvalidID_MessageBoxDisplayed()
		{
			var mb = new Mock<IMessageBox>();
			var queryString = new NameValueCollection();

			mb.Setup(x => x.GetInlineSt(It.Is<string>(c => c == "NotifyPageDataError"), It.Is<MessageBoxStatus>(c => c == MessageBoxStatus.Error))).Returns("FooData");
			queryString.Add("ID", "test");

			var verifier = new IdVerifier(queryString, null, mb.Object);

			var id = verifier.VerifyQueryStringIdAjax();

			Assert.IsNull(id);

			mb.Verify(x => x.GetInlineSt(It.Is<string>(c => c == "NotifyPageDataError"), It.Is<MessageBoxStatus>(c => c == MessageBoxStatus.Error)), Times.Once);
			Assert.AreEqual("FooData", verifier.MessageBoxToDisplay);
		}

		[Test]
		public void VerifyQueryStringIdAjax_ValidID_MessageBoxDisplayed()
		{
			var mb = new Mock<IMessageBox>();
			var queryString = new NameValueCollection {{"ID", "5"}};

			var verifier = new IdVerifier(queryString, null, mb.Object);

			var id = verifier.VerifyQueryStringIdAjax();

			Assert.AreEqual(5, id);

			mb.Verify(x => x.GetInlineSt(It.Is<string>(c => c == "NotifyPageDataError"), It.Is<MessageBoxStatus>(c => c == MessageBoxStatus.Error)), Times.Never);
			Assert.IsNull(verifier.MessageBoxToDisplay);
		}
	}
}