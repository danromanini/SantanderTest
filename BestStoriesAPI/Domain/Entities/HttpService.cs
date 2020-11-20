using Domain.Interfaces;
using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class HttpService : IHttpService
    {   
        public HttpService()
        {
        
        }

        public Task<HttpCallResult<T>> CallHttp<T>(Func<IFlurlRequest, Task<T>> httpCallAction,
                                                         string baseUrl,
                                                         string suffixUrl,
                                                         string bearerToken,
                                                         int clientId = 0,
                                                         [CallerMemberName] string callerMemberName = "",
                                                         bool throwException = false)
        {
            return CallHttp((url) => httpCallAction(url.WithOAuthBearerToken(bearerToken)),
                            baseUrl,
                            suffixUrl,
                            clientId,
                            callerMemberName,
                            throwException);
        }

        public async Task<HttpCallResult<T>> CallHttp<T>(Func<Flurl.Url, Task<T>> httpCallAction,
                                                         string baseUrl,
                                                         string suffixUrl,
                                                         int clientId = 0,
                                                         [CallerMemberName] string callerMemberName = "",
                                                         bool throwException = false)
        {
            var transactionId = Guid.NewGuid();
            
            baseUrl = baseUrl == null || baseUrl.EndsWith("/") ? baseUrl : $"{baseUrl}/";
            if (string.IsNullOrEmpty(baseUrl) || baseUrl.Equals("/") || string.IsNullOrEmpty(suffixUrl))
            {
                return new HttpCallResult<T> { Success = false };
            }

            var stopwatch = new Stopwatch();
            try
            {
                suffixUrl = suffixUrl.StartsWith("/") ? suffixUrl.Substring(1) : suffixUrl;

                var url = baseUrl + suffixUrl;

                stopwatch.Start();

                var result = await httpCallAction(url);

                return new HttpCallResult<T> { Result = result, Success = true };
            }
            catch (Exception ex)
            {

                if (throwException)
                {
                    throw;
                }

                return new HttpCallResult<T>
                {
                    Success = false,
                    Exception = ex
                };
            }
        }
    }
}
