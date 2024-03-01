using Azure;
using MyStore.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyStore.Service
{
    public class AuthenticateRestClient<TEntity>
        where TEntity : class
    {
        private readonly HttpClient _httpClient;

        public AuthenticateRestClient(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<ResponseMessage<TEntity>> GetApiResponseAsync(string apiUrl)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
                var content = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    return new ResponseMessage<TEntity>
                    {
                        StatusCode = 200,
                        IsSuccess = true,
                        Result = JsonSerializer.Deserialize<TEntity>(content)
                    };
                }
                else
                {
                    return new ResponseMessage<TEntity>
                    {
                        StatusCode = 500,
                        Message = content,
                        IsSuccess = false
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseMessage<TEntity>
                {
                    StatusCode = 500,
                    Message = ex.Message,
                    IsSuccess = false
                };
            }

        }
    }
}
