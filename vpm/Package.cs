using ProtoBuf;
using System.IO;

namespace VPM { 

	public static class ProtobufExtensions {
		public static byte[] Serialize<T>(this T obj){
			using(var stream = new MemoryStream()){
				ProtoBuf.Serializer.Serialize (stream, obj);
				return stream.ToArray ();
			}
		}

		public static T Deserialize<T>(this byte [] data){
			using (var stream = new MemoryStream (data)) {
				return ProtoBuf.Serializer.Deserialize<T>(stream);
			}
		}
	}


	[ProtoContract]
	public sealed class Package {

		[ProtoMember(1)]
		public string Name { get; set; }

		[ProtoMember(2)]
		public string[] Versions { get; set; }

	}

	[ProtoContract]
	public sealed class PackageBlob {

		[ProtoMember(1)]
		public string Name { get ;set; }

		[ProtoMember(2)]
		public string Version { get; set; }

		[ProtoMember(3)]
		public byte[] Zipfile { get; set; }

	}
		
}

