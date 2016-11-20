using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SepsisTrust.Model.Storage
{
    public interface IJsonObjectStreamWriter
    {
        Task Write<T>( Stream stream, T entity );
    }

    public class JsonObjectStreamWriter : IJsonObjectStreamWriter
    {
        public async Task Write<T>( Stream stream, T entity )
        {
            if (stream != null)
            {
                using (var streamWriter = new StreamWriter(stream))
                {
                    var objectText = JsonConvert.SerializeObject(entity);
                    await streamWriter.WriteAsync(objectText);
                }
            }
;
        }
    }
}