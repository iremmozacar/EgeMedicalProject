using System;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using EgeApp.Frontend.Mvc.Extensions;
using EgeApp.Frontend.Mvc.Models;
using EgeApp.Frontend.Mvc.Models.Other;
using EgeApp.Frontend.Mvc.Models.Product;
using EgeApp.Frontend.Mvc.Models.Response;

namespace EgeApp.Frontend.Mvc.Services;

public static class ProductService
{
    private static readonly string BaseUrl = "http://localhost:5200/api/Products";
    public static async Task<ResponseModel<List<ProductViewModel>>> GetHomesAsync(bool isHome = true)
    {
        using (HttpClient httpClient = new())
        {
            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync($"http://localhost:5200/api/Products/GetHomes/{isHome}");
            string contentResponse = await httpResponseMessage.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<ResponseModel<List<ProductViewModel>>>(contentResponse);
            return response;
        }
    }
    public static async Task<ResponseModel<List<ProductViewModel>>> GetAllAsync()
    {
        using (HttpClient httpClient = new())
        {
            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync($"http://localhost:5200/api/Products/GetAll");
            string contentResponse = await httpResponseMessage.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<ResponseModel<List<ProductViewModel>>>(contentResponse);
            return response;
        }
    }
    public static async Task<ResponseModel<ProductViewModel>> GetByIdAsync(int id)
    {
        using (HttpClient httpClient = new())
        {
            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync($"http://localhost:5200/api/Products/GetById/{id}");
            string contentResponse = await httpResponseMessage.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<ResponseModel<ProductViewModel>>(contentResponse);
            return response;
        }
    }
    public static async Task<ResponseModel<NoContent>> CreateAsync(ProductCreateViewModel model)
    {
        using (HttpClient httpClient = new())
        {
            
            using (Stream stream = model.Image.OpenReadStream())
            {
                var content = new MultipartFormDataContent();
                byte[] bytes = stream.ToByteArray();
                var byteContent = new ByteArrayContent(bytes);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue(model.Image.ContentType);
                content.Add(byteContent, "Image", model.Image.FileName);
                content.Add(new StringContent("products"), "FolderName");
                HttpResponseMessage httpResponseMessageImage = await httpClient.PostAsync("http://localhost:5200/api/Images/UploadImage", content);
                var httpResponseMessageImageString = await httpResponseMessageImage.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<ResponseModel<ImageUploadModel>>(httpResponseMessageImageString);
                if (response.IsSucceeded)
                {
                    model.ImageUrl = response.Data.Url;
                }
                else
                {
                    return new ResponseModel<NoContent> { Data = null, Error = response.Error, IsSucceeded = response.IsSucceeded };
                }
            }

        
            var serializeModel = JsonConvert.SerializeObject(model);
            var stringContent = new StringContent(serializeModel, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessageProduct = await httpClient.PostAsync("http://localhost:5200/api/Products/Create", stringContent);
            var httpResponseMessageProductString = await httpResponseMessageProduct.Content.ReadAsStringAsync();
            var responseProduct = JsonConvert.DeserializeObject<ResponseModel<ProductViewModel>>(httpResponseMessageProductString);
            return new ResponseModel<NoContent> { Data = null, Error = responseProduct.Error, IsSucceeded = responseProduct.IsSucceeded };
        }

    }
    public static async Task<ResponseModel<NoContent>> UpdateAsync(ProductUpdateViewModel model)
    {
        using (HttpClient httpClient = new())
        {
            if (model.Image != null)
            {
                
                using (Stream stream = model.Image.OpenReadStream())
                {
                    var content = new MultipartFormDataContent();
                    byte[] bytes = stream.ToByteArray();
                    var byteContent = new ByteArrayContent(bytes);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue(model.Image.ContentType);
                    content.Add(byteContent, "Image", model.Image.FileName);
                    content.Add(new StringContent("products"), "FolderName");
                    HttpResponseMessage httpResponseMessageImage = await httpClient.PostAsync("http://localhost:5200/api/Images/UploadImage", content);
                    var httpResponseMessageImageString = await httpResponseMessageImage.Content.ReadAsStringAsync();
                    var response = JsonConvert.DeserializeObject<ResponseModel<ImageUploadModel>>(httpResponseMessageImageString);
                    if (response.IsSucceeded)
                    {
                        model.ImageUrl = response.Data.Url;
                    }
                    else
                    {
                        return new ResponseModel<NoContent> { Data = null, Error = response.Error, IsSucceeded = response.IsSucceeded };
                    }
                }
            }
            model.CategoryId = 1;
           
            var serializeModel = JsonConvert.SerializeObject(model);
            var stringContent = new StringContent(serializeModel, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessageProduct = await httpClient.PutAsync("http://localhost:5200/api/Products/Update", stringContent);
            var httpResponseMessageProductString = await httpResponseMessageProduct.Content.ReadAsStringAsync();
            var responseProduct = JsonConvert.DeserializeObject<ResponseModel<ProductViewModel>>(httpResponseMessageProductString);
            return new ResponseModel<NoContent> { Data = null, Error = responseProduct.Error, IsSucceeded = responseProduct.IsSucceeded };
        }

    }
    public static async Task<ResponseModel<NoContent>> DeleteAsync(int id)
    {
        using (HttpClient httpClient = new())
        {
            HttpResponseMessage httpResponseMessage = await httpClient.DeleteAsync($"http://localhost:5200/api/Products/Delete/{id}");
            string contentResponse = await httpResponseMessage.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<ResponseModel<NoContent>>(contentResponse);
            return response;
        }
    }
    public static async Task<ResponseModel<ProductViewModel>> UpdateIsActiveAsync(int id)
    {
        using (HttpClient httpClient = new())
        {
            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync($"http://localhost:5200/api/Products/UpdateIsActive/{id}");
            string contentResponse = await httpResponseMessage.Content?.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<ResponseModel<NoContent>>(contentResponse);
            ResponseModel<ProductViewModel> result = new();
            if (response.IsSucceeded)
            {
                result = await GetByIdAsync(id);
            }
            else
            {
                result.Data = null;
                result.Error = response.Error;
                result.IsSucceeded = response.IsSucceeded;
            }
            return result;
        }
    }
    public static async Task<ResponseModel<ProductViewModel>> UpdateIsHomeAsync(int id)
    {
        using (HttpClient httpClient = new())
        {
            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync($"http://localhost:5200/api/Products/UpdateIsHome/{id}");
            string contentResponse = await httpResponseMessage.Content?.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<ResponseModel<NoContent>>(contentResponse);
            ResponseModel<ProductViewModel> result = new();
            if (response.IsSucceeded)
            {
                result = await GetByIdAsync(id);
            }
            else
            {
                result.Data = null;
                result.Error = response.Error;
                result.IsSucceeded = response.IsSucceeded;
            }
            return result;
        }

    }
    public static async Task<ResponseModel<List<ProductViewModel>>> GetDiscountedProductsAsync()
    {
        try
        {
            using (HttpClient httpClient = new())
            {
                httpClient.BaseAddress = new Uri(BaseUrl);

                var response = await httpClient.GetAsync("http://localhost:5200/api/Products/GetDiscountedProducts/GetDiscountedProducts");
                if (!response.IsSuccessStatusCode)
                {
                    var errorDetails = await response.Content.ReadAsStringAsync();
                    return new ResponseModel<List<ProductViewModel>>
                    {
                        IsSucceeded = false,
                        Error = $"API Error: {response.StatusCode} - {response.ReasonPhrase}, Details: {errorDetails}"
                    };
                }

                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ResponseModel<List<ProductViewModel>>>(content);

                return result ?? new ResponseModel<List<ProductViewModel>>
                {
                    IsSucceeded = false,
                    Error = "Received null or invalid data from API."
                };
            }
        }
        catch (Exception ex)
        {
            return new ResponseModel<List<ProductViewModel>>
            {
                IsSucceeded = false,
                Error = $"Exception occurred: {ex.Message}"
            };
        }
    }

    public static async Task<ResponseModel<List<ProductViewModel>>> GetBestSellersAsync(int topCount)
    {
        try
        {
            using (HttpClient httpClient = new())
            {
                httpClient.BaseAddress = new Uri(BaseUrl);

                var response = await httpClient.GetAsync($"http://localhost:5200/api/Products/GetBestSellers/GetBestSellers/{topCount}");
                if (!response.IsSuccessStatusCode)
                {
                    var errorDetails = await response.Content.ReadAsStringAsync();
                    return new ResponseModel<List<ProductViewModel>>
                    {
                        IsSucceeded = false,
                        Error = $"API Error: {response.StatusCode} - {response.ReasonPhrase}, Details: {errorDetails}"
                    };
                }

                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ResponseModel<List<ProductViewModel>>>(content);

                return result ?? new ResponseModel<List<ProductViewModel>>
                {
                    IsSucceeded = false,
                    Error = "Received null or invalid data from API."
                };
            }
        }
        catch (Exception ex)
        {
            return new ResponseModel<List<ProductViewModel>>
            {
                IsSucceeded = false,
                Error = $"Exception occurred: {ex.Message}"
            };
        }
    }
}