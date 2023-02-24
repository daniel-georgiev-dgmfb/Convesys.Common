using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Twilight.Kernel.Serialisation;

namespace Twilight.Common.Serialisation.Binary
{
    [Obsolete("Use JSON serialization. Binary has been made obsolete. For reference https://docs.microsoft.com/en-us/dotnet/core/compatibility/core-libraries/5.0/binaryformatter-serialization-obsolete")]
    public class BinarySerialiser : ISerialiser
    {
        private readonly BinaryFormatter _binaryFormatter;

        public BinarySerialiser()
        {
            _binaryFormatter = new BinaryFormatter();
        }

        public Task<Stream> Serialise(object obj)
        {
            var memoryStream = new MemoryStream();
            
            _binaryFormatter.Serialize(memoryStream, obj);
            return Task.FromResult((Stream)memoryStream);
        }

        public Task<object> Deserialise(Stream stream)
        {
            var obj = _binaryFormatter.Deserialize(stream);
            return Task.FromResult(obj);
        }

        public Task<T> Deserialise<T>(Stream stream)
        {
            var obj = Deserialise(stream).Result;
            return Task.FromResult((T)obj);
        }

        public Task<bool> TrySerialise(object obj, out Stream stream)
        {
            throw new System.NotImplementedException();
        }
    }
}
