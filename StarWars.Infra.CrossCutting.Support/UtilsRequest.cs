using RestSharp;

namespace StarWars.Infra.CrossCutting.Support
{
    public static class UtilsRequest
    {
        public static string Get(string url)
        {
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            RestResponse response = client.Execute(request);
            return response.Content;
        }
    }
}
