using System;
using Microsoft.Owin.Hosting;

namespace AcspNet.Examples.SelfHosted
{
	class Program
	{
		static void Main()
		{
			using (WebApp.Start<Startup>("http://localhost:8080"))
			{
				Console.WriteLine("Running a http server on port 8080");
				Console.ReadLine();
			}
		}
	}
}
