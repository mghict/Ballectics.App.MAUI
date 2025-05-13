namespace Ballectics.App.Services
{
    public class ConnectivityService
    {
        private readonly HttpClient httpClient;

        public ConnectivityService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<bool> HasActiveInternetAsync()
        {
            try
            {
                if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
                    return false;

                //using var cts= new CancellationTokenSource(TimeSpan.FromSeconds(5));

                //var response =await httpClient.GetAsync("https://clients3.google.com/generate_204");

                //return response.IsSuccessStatusCode;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool HasActiveInternet()
        {
            return HasActiveInternetAsync().Result;
        }
    }
}
