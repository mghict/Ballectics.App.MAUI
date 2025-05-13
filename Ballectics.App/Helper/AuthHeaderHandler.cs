using System.Net.Http.Headers;
using Ballectics.App.Services;

namespace Ballectics.App.Helper
{
    public class AuthHeaderHandler : DelegatingHandler
    {
        private readonly StorageService _storageService;

        public AuthHeaderHandler(StorageService storageService)
        {
            _storageService = storageService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _storageService.GetTokenAsync();
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = null;
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
