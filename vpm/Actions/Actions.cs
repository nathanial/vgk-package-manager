using System;
using Mono.Options;
using System.IO;
using System.Reflection;

namespace VPM
{

	public sealed class ActionContext {
		public string PackageFolder { get; set; }
		public string[] Arguments { get; set; }
	}

	public abstract class AddAction {
		public string Command { get; private set; }

		protected AddAction(string command){
			Command = command;
		}

		public abstract void Execute(ActionContext context);

		protected void EnsurePathExists(string path){
			throw new NotImplementedException ();
		}	

		protected string TextOfTemplate(string name){
			using (var reader = new StreamReader (Assembly.GetExecutingAssembly ().GetManifestResourceStream (name))) {
				return reader.ReadToEnd ();
			}
		}
	}

}

