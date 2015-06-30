using System;
using DBreeze;
using System.IO;
using System.Linq;

namespace VPM {


	public interface IPackageDatabase : IDisposable {
		Package[] FindAllPackages();
		void InsertPackage(Package package);
		void AddPackageVersion(PackageBlob blob);
	}

	public class DBreezePackageDatabase : IPackageDatabase {
		readonly DBreezeEngine _engine;

		public DBreezePackageDatabase(){
			string homePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			string dbFolder = Path.Combine(homePath, "VGK","VPM", "db");
			_engine = new DBreezeEngine (dbFolder);
		}

		public void Dispose () {
			_engine.Dispose();
		}

		public Package[] FindAllPackages(){
			using (var tran = _engine.GetTransaction ()) {
				return tran.SelectForward<string, byte[]> ("Packages")
						   .Select (x => x.Value.Deserialize<Package> ())
						   .ToArray ();
			}
		}

		public void InsertPackage(Package package){
			using (var tran = _engine.GetTransaction ()) {
				tran.Insert<string, byte[]> ("Packages", package.Name, package.Serialize());
				tran.Commit ();
			}
		}

		public void AddPackageVersion(PackageBlob blob){
			using (var tran = _engine.GetTransaction ()) {
				tran.Insert<string, byte[]> ("PackageVersions", blob.Name + ":" + blob.Version, blob.Serialize());
			}
		}
	}
}

