using System;
using Mono.Options;
using System.IO;
using System.Reflection;

namespace VPM
{
	public sealed class AddVoxel : AddAction {

		public AddVoxel () : base("voxel")
		{
		}

		public override void Execute (ActionContext context){
			EnsurePathExists(Path.Combine(context.PackageFolder, "src", "voxels"));
			CreateVoxelFile (context);
		}

		void CreateVoxelFile(ActionContext context){
			var filePath = Path.Combine (context.PackageFolder, "src", "voxels", context.Arguments [0]);
			File.WriteAllText (filePath, TextOfTemplate ("VoxelDefinitionTemplate.tt"));
		}
	}
}

