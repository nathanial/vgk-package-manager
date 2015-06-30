using System;
using Nancy.ModelBinding;
using System.IO;
using ProtoBuf.Meta;
using System.Linq;

namespace VPM {

	public sealed class ProtobufNetBodyDeserializer : IBodyDeserializer {

		public bool CanDeserialize (string contentType, BindingContext context) {
			return IsProtoBufType(contentType);
		}
			
		public object Deserialize(string contentType, Stream bodyStream, BindingContext context) {
			return RuntimeTypeModel.Default.Deserialize(bodyStream, null, context.DestinationType);
		}

		static bool IsProtoBufType(string contentType){
			if (string.IsNullOrWhiteSpace(contentType)) {
				return false;
			}

			var contentMimeType = contentType.Split(';').First();
			return contentMimeType.Equals("application/x-protobuf", StringComparison.InvariantCultureIgnoreCase);
		}
	}
}

