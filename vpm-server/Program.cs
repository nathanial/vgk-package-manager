using System;
using Nancy.Hosting.Self;
using Nancy;

namespace VPM
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			
			StaticConfiguration.DisableErrorTraces = false;
			using (var host = new NancyHost (new Uri ("http://localhost:1234"))) {
				host.Start ();
				Console.WriteLine ("Listening at http://localhost:1234");
				Console.ReadLine ();
			}
		}
	}





}
