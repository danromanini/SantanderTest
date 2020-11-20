using System;
using System.Threading.Tasks;
using Flurl.Http;
using System.Runtime.CompilerServices;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IHttpService
    {
        Task<HttpCallResult<T>> CallHttp<T>(Func<Flurl.Url, Task<T>> httpCallAction,
                                            string baseUrl,
                                            string suffixUrl,
                                            int clientId = 0,
                                            [CallerMemberName] string callerMemberName = "",
                                            bool throwException = false);

        Task<HttpCallResult<T>> CallHttp<T>(Func<IFlurlRequest, Task<T>> httpCallAction,
                                            string baseUrl,
                                            string suffixUrl,
                                            string bearerToken,
                                            int clientId = 0,
                                            [CallerMemberName] string callerMemberName = "",
                                            bool throwException = false);
    }
}
