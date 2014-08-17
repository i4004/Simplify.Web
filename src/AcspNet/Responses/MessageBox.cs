using System;

namespace AcspNet.Responses
{
	/// <summary>
	/// Provides message box response (generate message box and puts it to data collector)
	/// </summary>
	public class MessageBox : ControllerResponse
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MessageBox"/> class.
		/// </summary>
		/// <param name="text">The message box text.</param>
		/// <param name="status">The message box status.</param>
		/// <param name="messageBoxTitle">The message box title.</param>
		public MessageBox(string text, MessageBoxStatus status = MessageBoxStatus.Error, string messageBoxTitle = null)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Processes this response
		/// </summary>
		/// <returns></returns>
		public override ControllerResponseResult Process()
		{
			throw new NotImplementedException();
		}
	}
}