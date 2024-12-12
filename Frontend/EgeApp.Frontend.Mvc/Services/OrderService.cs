using System;
using System.Text;
using Newtonsoft.Json;
using EgeApp.Frontend.Mvc.Models.Order;
using EgeApp.Frontend.Mvc.Models.Product;
using EgeApp.Frontend.Mvc.Models.Response;

namespace EgeApp.Frontend.Mvc.Services;

public class OrderService
{
    public static async Task<ResponseModel<ProductViewModel>> AddOrderAsync(OrderCreateViewModel model)
    {
        using (HttpClient httpClient = new())
        {
            var serializeModel = JsonConvert.SerializeObject(model);
            var stringContent = new StringContent(serializeModel, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await httpClient.PostAsync("http://localhost:5200/api/orders/addorder", stringContent);
            var httpResponseMessageString = await httpResponseMessage.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<ResponseModel<ProductViewModel>>(httpResponseMessageString);
            return response;
        }
    }

    public static async Task<ResponseModel<List<OrderViewModel>>> GetOrdersByUserIdAsync(string userId)
    {
        using (HttpClient httpClient = new())
        {
            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync($"http://localhost:5200/api/Orders/GetOrdersByUserId/{userId}");
            string contentResponse = await httpResponseMessage.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<ResponseModel<List<OrderViewModel>>>(contentResponse);
            return response;
        }

    }

    public static async Task<ResponseModel<List<OrderViewModel>>> GetOrdersByOrderStateAsync(int orderState)
    {
        using (HttpClient httpClient = new())
        {
            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync($"http://localhost:5200/api/Orders/GetOrdersByOrderState/{orderState}");
            string contentResponse = await httpResponseMessage.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<ResponseModel<List<OrderViewModel>>>(contentResponse);
            return response;
        }
    }

    public static async Task<ResponseModel<List<OrderViewModel>>> GetOrdersAsync()
    {
        using (HttpClient httpClient = new())
        {
            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync($"http://localhost:5200/api/Orders/GetOrders");
            string contentResponse = await httpResponseMessage.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<ResponseModel<List<OrderViewModel>>>(contentResponse);
            return response;
        }
    }


}
