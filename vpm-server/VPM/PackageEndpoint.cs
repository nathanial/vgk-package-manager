using System;
using Nancy;
using Newtonsoft.Json;

namespace VPM
{
	public class PackageEndpoint : NancyModule {
		readonly IPackageDatabase _database;

		public PackageEndpoint(IPackageDatabase database) : base("/packages"){
			_database = database;
			Get ["/"] = (parameters) => {
				var response = (Response)JsonConvert.SerializeObject(FindAllPackages(),Formatting.Indented);
				response.ContentType = "application/json";
				return response;
			};
		}

		private Package[] FindAllPackages(){
			return _database.FindAllPackages ();
		}

	}
}

