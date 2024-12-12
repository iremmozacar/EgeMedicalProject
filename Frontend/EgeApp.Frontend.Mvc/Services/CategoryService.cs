using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using EgeApp.Frontend.Mvc.Models;
using EgeApp.Frontend.Mvc.Models.Category;
using EgeApp.Frontend.Mvc.Models.Response;

namespace EgeApp.Frontend.Mvc.Services
{
    public static class CategoryService
    {
        private const string BaseUrl = "http://localhost:5200/api/Categories";
        private static readonly HttpClient HttpClient = new();

        public static async Task<ResponseModel<List<CategoryViewModel>>> GetActives(bool isActive = true)
        {
            return await GetAsync<List<CategoryViewModel>>($"{BaseUrl}/GetActives/{isActive}");
        }

        public static async Task<ResponseModel<List<CategoryViewModel>>> GetAllAsync()
        {
            return await GetAsync<List<CategoryViewModel>>($"{BaseUrl}/GetAll");
        }

        public static async Task<ResponseModel<CategoryViewModel>> GetByIdAsync(int id)
        {
            return await GetAsync<CategoryViewModel>($"{BaseUrl}/GetById/{id}");
        }

        public static async Task<ResponseModel<List<SelectListItem>>> GetSelectListItemsAsync()
        {
            try
            {
                
                var response = await GetActives();
                if (!response.IsSucceeded)
                {
                    return new ResponseModel<List<SelectListItem>>
                    {
                        IsSucceeded = false,
                        Error = response.Error ?? "Kategoriler yüklenemedi."
                    };
                }

          
                var selectListItems = response.Data.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();

                return new ResponseModel<List<SelectListItem>>
                {
                    IsSucceeded = true,
                    Data = selectListItems
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel<List<SelectListItem>>
                {
                    IsSucceeded = false,
                    Error = $"Exception: {ex.Message}"
                };
            }
        }
        public static async Task<ResponseModel<List<CategoryViewModel>>> GetAllCategoriesWithStatusAsync()
        {
            try
            {
                
                var response = await GetAllAsync();
                if (!response.IsSucceeded)
                {
                    return new ResponseModel<List<CategoryViewModel>>
                    {
                        IsSucceeded = false,
                        Error = response.Error ?? "Kategoriler yüklenemedi."
                    };
                }

             
                return new ResponseModel<List<CategoryViewModel>>
                {
                    IsSucceeded = true,
                    Data = response.Data
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel<List<CategoryViewModel>>
                {
                    IsSucceeded = false,
                    Error = $"Exception: {ex.Message}"
                };
            }
        }

        public static async Task<ResponseModel<List<CategoryViewModel>>> GetCategoriesWithProductsCountAsync()
        {
            try
            {
              
                var response = await GetAsync<List<CategoryViewModel>>($"{BaseUrl}/GetCategoriesWithProductsCount");
                if (!response.IsSucceeded)
                {
                    return new ResponseModel<List<CategoryViewModel>>
                    {
                        IsSucceeded = false,
                        Error = response.Error ?? "Kategoriler yüklenemedi."
                    };
                }

                return new ResponseModel<List<CategoryViewModel>>
                {
                    IsSucceeded = true,
                    Data = response.Data
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel<List<CategoryViewModel>>
                {
                    IsSucceeded = false,
                    Error = $"Exception: {ex.Message}"
                };
            }
        }
        public static async Task<ResponseModel<CategoryViewModel>> CreateAsync(CategoryCreateViewModel model)
        {
            return await PostAsync<CategoryCreateViewModel, CategoryViewModel>($"{BaseUrl}/Create", model);
        }

        public static async Task<ResponseModel<CategoryViewModel>> UpdateAsync(CategoryUpdateViewModel model)
        {
            try
            {
                var jsonContent = JsonConvert.SerializeObject(model);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await HttpClient.PutAsync($"{BaseUrl}/Update", content);
                var contentResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ResponseModel<CategoryViewModel>>(contentResponse) ?? new ResponseModel<CategoryViewModel> { IsSucceeded = false, Error = "Invalid response" };
            }
            catch (Exception ex)
            {
                return new ResponseModel<CategoryViewModel> { IsSucceeded = false, Error = $"Exception: {ex.Message}" };
            }
        }

        public static async Task<ResponseModel<bool>> DeleteAsync(int id)
        {
            return await DeleteAsync<bool>($"{BaseUrl}/Delete/{id}");
        }

        private static async Task<ResponseModel<T>> GetAsync<T>(string url)
        {
            try
            {
                Console.WriteLine($"GetAsync çağrıldı. URL: {url}");

                var response = await HttpClient.GetAsync(url);
                Console.WriteLine($"HTTP Durum Kodu: {(int)response.StatusCode}");

                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Gelen Yanıt: " + content);

                var deserializedResponse = JsonConvert.DeserializeObject<ResponseModel<T>>(content);
                if (deserializedResponse == null)
                {
                    Console.WriteLine("Deserialize işlemi başarısız oldu.");
                }
                return deserializedResponse ?? new ResponseModel<T>
                {
                    IsSucceeded = false,
                    Error = "Yanıt deserializasyonunda hata oluştu."
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"İstisna Yakalandı: {ex.Message}");
                return new ResponseModel<T>
                {
                    IsSucceeded = false,
                    Error = $"Exception: {ex.Message}"
                };
            }
        }

        private static async Task<ResponseModel<TResult>> PostAsync<TRequest, TResult>(string url, TRequest request)
        {
            try
            {
                var jsonContent = JsonConvert.SerializeObject(request);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await HttpClient.PostAsync(url, content);
                var contentResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ResponseModel<TResult>>(contentResponse) ?? new ResponseModel<TResult> { IsSucceeded = false, Error = "Invalid response" };
            }
            catch (Exception ex)
            {
                return new ResponseModel<TResult> { IsSucceeded = false, Error = $"Exception: {ex.Message}" };
            }
        }

        private static async Task<ResponseModel<TResult>> PutAsync<TRequest, TResult>(string url, TRequest request)
        {
            try
            {
                var jsonContent = JsonConvert.SerializeObject(request);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await HttpClient.PutAsync(url, content);
                var contentResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ResponseModel<TResult>>(contentResponse) ?? new ResponseModel<TResult> { IsSucceeded = false, Error = "Invalid response" };
            }
            catch (Exception ex)
            {
                return new ResponseModel<TResult> { IsSucceeded = false, Error = $"Exception: {ex.Message}" };
            }
        }

        private static async Task<ResponseModel<TResult>> DeleteAsync<TResult>(string url)
        {
            try
            {
                Console.WriteLine($"DELETE Request URL: {url}");
                var response = await HttpClient.DeleteAsync(url);
                Console.WriteLine($"HTTP Status Code: {(int)response.StatusCode}");

                var contentResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Response Content: {contentResponse}");

                var deserializedResponse = JsonConvert.DeserializeObject<ResponseModel<TResult>>(contentResponse);
                if (deserializedResponse == null)
                {
                    Console.WriteLine("Deserialization failed: Response is null.");
                }

                return deserializedResponse ?? new ResponseModel<TResult>
                {
                    IsSucceeded = false,
                    Error = "Invalid response"
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return new ResponseModel<TResult>
                {
                    IsSucceeded = false,
                    Error = $"Exception: {ex.Message}"
                };
            }
        }
    }
}
