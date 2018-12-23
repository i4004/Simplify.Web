using System.Threading.Tasks;

namespace Simplify.Web.Core
{
	/// <summary>
	/// Provides request handing result
	/// </summary>
	public class RequestHandlingResult
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="RequestHandlingResult"/> class.
		/// </summary>
		/// <param name="status">The status.</param>
		/// <param name="task">The task.</param>
		public RequestHandlingResult(RequestHandlingStatus status, Task task)
		{
			Status = status;
			Task = task;
		}

		/// <summary>
		/// Gets the status of request handling
		/// </summary>
		/// <value>
		/// The status.
		/// </value>
		public RequestHandlingStatus Status { get; }

		/// <summary>
		/// Gets the task representing request handling.
		/// </summary>
		/// <value>
		/// The task.
		/// </value>
		public Task Task { get; }

		/// <summary>
		/// Creates unhandled request result with no task delay.
		/// </summary>
		/// <returns></returns>
		public static RequestHandlingResult UnhandledResult()
		{
			return new RequestHandlingResult(RequestHandlingStatus.RequestWasUnhandled, Task.Delay(0));
		}

		/// <summary>
		/// Creates handled request result with no task delay.
		/// </summary>
		/// <returns></returns>
		public static RequestHandlingResult HandledResult()
		{
			return new RequestHandlingResult(RequestHandlingStatus.RequestWasHandled, Task.Delay(0));
		}

		/// <summary>
		/// Creates handled request result with custom task.
		/// </summary>
		/// <param name="task">The task.</param>
		/// <returns></returns>
		public static RequestHandlingResult HandledResult(Task task)
		{
			return new RequestHandlingResult(RequestHandlingStatus.RequestWasHandled, task);
		}
	}
}