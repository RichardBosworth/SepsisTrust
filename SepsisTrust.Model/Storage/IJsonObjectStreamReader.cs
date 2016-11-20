using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SepsisTrust.Model.User;

namespace SepsisTrust.Model.Storage
{
    public interface IJsonObjectStreamReader
    {
        Task<T> Read<T>( Stream stream );
    }

    public class JsonObjectStreamReader : IJsonObjectStreamReader
    {
        public async Task<T> Read<T>( Stream stream )
        {
            T obj = default(T);
            if (stream != null)
            {
                using (var streamReader = new StreamReader(stream))
                {
                    var jsonText = await streamReader.ReadToEndAsync();
                    obj = JsonConvert.DeserializeObject<T>(jsonText);
                }
            }

            return obj;
        }
    }
}