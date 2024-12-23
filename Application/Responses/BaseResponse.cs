﻿namespace Application.Responses
{
    public class BaseResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }

        public BaseResponse(bool success, string message, T? data = default)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        // Convenience Methods
        public static BaseResponse<T> SuccessResponse(T data, string message = "Operation succeeded.")
        {
            return new BaseResponse<T>(true, message, data);
        }

        public static BaseResponse<T> FailureResponse(string message)
        {
            return new BaseResponse<T>(false, message);
        }
    }
}
