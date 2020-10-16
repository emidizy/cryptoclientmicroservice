using AppCore.Shared.Interfaces;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace AppCore.Shared.Services
{
    public class HttpClient : IHttpClient
    {
        private IRestResponse _restResponse;
        private readonly Logger _logger;

        public HttpClient(IRestResponse restResponse, 
            Logger logger)
        {
            _restResponse = restResponse;
            _logger = logger;
        }

        public async Task<string> Post(string parameter, string url, string requestId, string header = null, string token = null)
        {

            try
            {
                RestClient client = new RestClient(url);

                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
               
                request.AddParameter("application/json", parameter, ParameterType.RequestBody);

                _restResponse = await client.ExecuteAsync(request);

                _logger.LogInfo($"[HttpClient][Post] => {url};{Environment.NewLine}" +
                   $"Req Body => {parameter};{Environment.NewLine}" +
                   $"Req Id => {requestId};{Environment.NewLine}" +
                   $"Response => {_restResponse.Content}" +
                   $"ErrorIfAny => {_restResponse.ErrorException}");

            }
            catch (Exception ex)
            {
                _logger.LogError($"[HttpClient][Post][Err] => {ex.Message}| {JsonConvert.SerializeObject(ex.InnerException)} {Environment.NewLine}" +
                    $"ENDPOINT => {url};{Environment.NewLine}" +
                    $"REQUESTBODY => {parameter};{Environment.NewLine}" +
                    $"REQUEST ID => {requestId};{Environment.NewLine}" +
                    $"HTTPRequestError => {_restResponse.ErrorException}");
            }
            return _restResponse.Content;
        }

       
    }
}
