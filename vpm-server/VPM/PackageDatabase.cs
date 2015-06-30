using System;
using DBreeze;
using System.IO;
using System.Linq;
using DBreeze.DataTypes;

namespace VPM
{
	public interface IPackageDatabase : IDisposable {

		Package[] FindAllPackages();

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
				return tran.SelectForward<string, DbMJSON<Package>> ("Packages")
						   .Select (x => x.Value.Get)
						   .ToArray();
			}
		}
	}
}

