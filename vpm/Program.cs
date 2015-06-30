using System;
using System.Net.Http;
using System.IO;
using Mono.Options;
using System.Linq;

namespace VPM {
	class MainClass {
		readonly string serverRoot = "http://localhost:1234";

		async void CreatePackage(){
			var httpClient = new HttpClient ();
			var package = new Package { 
				Name = "stuff"
			};
			var content = new ByteArrayContent (package.Serialize ());
			var response = await httpClient.PostAsync(serverRoot + "/packages", content);			
			if (response.StatusCode != System.Net.HttpStatusCode.OK) {
				throw new Exception ("Bad Status Code: " + response.StatusCode);
			}
		}

		public static void InitPackage() {
			Console.WriteLine("Init Package");
		}

		static void AddVoxelToPackage(string voxelName){
			Console.WriteLine ("Add Voxel: " + voxelName);
		}

		static void AddTerrainToPackage(string terrainName){
			Console.WriteLine ("Add Terrain: " + terrainName);
		}

		static void AddItemToPackage(string itemName){
			Console.WriteLine ("Add Item: " + itemName);
		}

		static void AddFieldToPackage(string fieldName){
			Console.WriteLine("Add Field: " + fieldName);
		}

		static void AddActionToPackage(string actionName){
			Console.WriteLine ("Add Action: " + actionName);
		}

		static void AddUserInterface(string uiName){
			Console.WriteLine("Add User Interface: " + uiName);
		}

		public static void AddToPackage(string[] args){
			switch (args [0]) {
			case "voxel":
				AddVoxelToPackage (args[1]);
				break;
			case "terrain":
				AddTerrainToPackage (args [1]);
				break;
			case "item":
				AddItemToPackage (args [1]);
				break;
			case "field":
				AddFieldToPackage (args [1]);
				break;
			default:
				Console.Error.WriteLine ("Cannot Add: " + args [0]);
				break;
			}
		}

		public static void Main(string[] args) {
			var options = new OptionSet {
			};
			var commands = options.Parse (args);
			switch (commands [0]) {
			case "init":
				InitPackage ();
				break;
			case "add":
				var addArgs = commands.Skip (1).ToArray ();
				AddToPackage (addArgs);
				break;			
			default:
				Console.Error.WriteLine ("Command Not Recognized: " + commands [0]);
				break;
			}
		}
	}
}
