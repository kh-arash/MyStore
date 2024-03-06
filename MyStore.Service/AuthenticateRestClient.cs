using Azure;
using MyStore.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Microsoft.IdentityModel.Tokens;

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

        public async Task<ResponseMessage<TEntity>> Get(string apiUrl, string? token = null)
        {
            try
            {
                if (!string.IsNullOrEmpty(token))
                    _httpClient.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue("Bearer", token);
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

        public async Task<ResponseMessage<TEntity>> Post(string apiUrl, object? request = null, string? token = null)
        {
            try
            {
                var jsonString = "";
                if (request != null)
                    jsonString = JsonSerializer.Serialize(request);
                var bodyContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
                if (!string.IsNullOrEmpty(token))
                    _httpClient.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = await _httpClient.PostAsync(apiUrl, bodyContent);
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
