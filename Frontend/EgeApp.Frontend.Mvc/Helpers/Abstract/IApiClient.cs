using System;

namespace EgeApp.Frontend.Mvc.Helpers.Abstract;

public interface IApiClient
{
    Task<HttpResponseMessage> GetAsync(string endpoint);
    Task<HttpResponseMessage> PostAsync<T>(string endpoint, T data);
    Task<HttpResponseMessage> PutAsync<T>(string endpoint, T data);
    Task<HttpResponseMessage> DeleteAsync(string endpoint);
}
