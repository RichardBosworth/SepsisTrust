using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace AzureData.Authentication
{
    /// <summary>
    /// Provides functionality to authenticate users.
    /// </summary>
    public interface IAuthenticationProvider
    {
        Task<MobileServiceUser> Authenticate();

    }
}