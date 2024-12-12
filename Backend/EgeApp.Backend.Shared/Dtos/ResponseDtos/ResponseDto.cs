using System.Text.Json.Serialization;

namespace EgeApp.Backend.Shared.Dtos.ResponseDtos
{
   
    public class ResponseDto<T>
    {
        public T Data { get; set; }
        public string Error { get; set; }
        [JsonIgnore]
        public int StatusCode { get; set; }
        public bool IsSucceeded { get; set; }

        public static ResponseDto<T> Success(T data, int statusCode)
        {
            return new ResponseDto<T>
            {
                Data = data,
                StatusCode = statusCode,
                IsSucceeded = true
            };
        }
        public static ResponseDto<T> Success(int statusCode)
        {
            return new ResponseDto<T>
            {
                Data = default(T),
                StatusCode = statusCode,
                IsSucceeded = true
            };
        }
        public static ResponseDto<T> Fail(string error, int statusCode)
        {
            return new ResponseDto<T>
            {
                Error = error,
                StatusCode = statusCode,
                IsSucceeded = false
            };
        }
    }
}
