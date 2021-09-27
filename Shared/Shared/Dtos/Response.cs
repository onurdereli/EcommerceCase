using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shared.Dtos
{
    public class Response<T>
    {
        public T Data { get; set; }

        [JsonIgnore]
        public int StatusCode { get; set; }

        [JsonIgnore]
        public bool IsSuccessfull { get; set; }

        public List<string> Errors { get; set; }

        //Static Factory Methods
        public static Response<T> Success(T data, int statusCode) => new Response<T> { Data = data, StatusCode = statusCode, IsSuccessfull = true };

        public static Response<T> Success(int statusCode) => new Response<T> { Data = default(T), StatusCode = statusCode, IsSuccessfull = true };

        public static Response<T> Fail(List<string> errors, int statusCode) => new Response<T> { Errors = errors, StatusCode = statusCode, IsSuccessfull = false, Data = default };

        public static Response<T> Fail(string error, int statusCode) => new Response<T> { Errors = new List<string> { error }, StatusCode = statusCode, IsSuccessfull = false };
    }
}
