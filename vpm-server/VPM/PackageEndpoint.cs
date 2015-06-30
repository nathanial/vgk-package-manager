using System;
using Nancy;
using Nancy.ModelBinding;
using Newtonsoft.Json;

namespace VPM
{
	public class PackageEndpoint : NancyModule {
		readonly IPackageDatabase _database;

		public PackageEndpoint(IPackageDatabase database) : base("/packages"){
			_database = database;
			Get ["/"] = (parameters) => new Response {
				ContentType = "application/x-protobuf",
				StatusCode = HttpStatusCode.OK,
				Contents = stream => ProtoBuf.Serializer.Serialize (stream, FindAllPackages ())
			};
			Post ["/"] = (parameters) => {
				var package = this.Bind<Package>();
				package.Versions = new string[]{}; // ignore versions
				_database.InsertPackage(package);
				return HttpStatusCode.OK;
			};
			Post ["/add-version"] = (parameters) => {
				var packageBlob = this.Bind<PackageBlob>();
				_database.AddPackageVersion(packageBlob);
				return HttpStatusCode.OK;
			};
		}

		private Package[] FindAllPackages(){
			return _database.FindAllPackages ();
		}

	}
}

