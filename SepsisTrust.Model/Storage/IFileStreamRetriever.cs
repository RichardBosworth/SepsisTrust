using System.IO;
using System.Threading.Tasks;
using PCLStorage;

namespace SepsisTrust.Model.Storage
{
    public interface IFileStreamRetriever
    {
        Task<Stream> ObtainFileStreamAsync( string filePath, bool createIfNotFound, FileAccess fileAccessType );
    }

    public class FileStreamRetriever : IFileStreamRetriever
    {
        public async Task<Stream> ObtainFileStreamAsync( string filePath, bool createIfNotFound = false, FileAccess fileAccessType = FileAccess.ReadAndWrite )
        {
            var localStorage = FileSystem.Current.LocalStorage;

            if ( await localStorage.CheckExistsAsync(filePath) == ExistenceCheckResult.NotFound )
            {
                if ( fileAccessType == FileAccess.ReadAndWrite )
                {
                    await localStorage.CreateFileAsync(filePath, CreationCollisionOption.ReplaceExisting);
                }
                else
                {
                    return null;
                }
            }

            if (fileAccessType == FileAccess.ReadAndWrite)
            {
                await localStorage.CreateFileAsync(filePath, CreationCollisionOption.ReplaceExisting);
            }

            var file = await localStorage.GetFileAsync(filePath);
            return await file.OpenAsync(fileAccessType);
        }
    }
}