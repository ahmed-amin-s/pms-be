using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models
{
    public class Result
    {

        public Result(bool succeeded = true, string message = "", IEnumerable<string>? errors = null)
        {
            Succeeded = succeeded;
            Message = message;
            Errors = errors?.ToArray();
        }

        public bool Succeeded { get; init; }
        public string Message { get; init; }
        public int? Status { get; init; }
        public string? Title { get; init; }
        public string[]? Errors { get; init; }
        public string? Type { get; init; }
        public string? Detail { get; init; }
        public static Result Success()
        {
            return new Result(true, "", Array.Empty<string>());
        }

        public static Result Failure(IEnumerable<string> errors)
        {
            return new Result(false, errors.FirstOrDefault() ?? "", errors);
        }
    }
    public class Result<T>
    {
        public Result()
        {
        }
        public Result(bool success, string message, T data)
        {
            Succeeded = success;
            Message = message;
            Data = data;
        }
        public Result(bool success, string message)
        {
            Succeeded = success;
            Message = message;
        }
        public static Result<T> Succeed(T data)
        {
            return new Result<T>(true, "Success!", data);
        }
        public static Result<T> Succeed(string msg, T data)
        {
            return new Result<T>(true, msg, data);
        }
        public static Result<T> Succeed(string msg = "Operation Completed Successfully!")
        {
            return new Result<T>(true, msg);
        }
        public static Result<T> NotFound(string msg = "Item Not Found!")
        {
            return new Result<T>(false, msg);
        }
        public static Result<T> Failed(string msg = "Operation Failed!")
        {
            return new Result<T>(false, msg);
        }
        public static Result<T> Existed(string msg = "Item Already Exisited!")
        {
            return new Result<T>(false, msg);
        }

        public bool? Succeeded { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public List<string>? Errors { get; set; }
    }
}
