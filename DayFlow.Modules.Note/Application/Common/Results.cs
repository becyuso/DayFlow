
using DayFlow.Modules.Notes.Application.Common.Interfaces;

namespace DayFlow.Modules.Notes.Application.Common
{
    public class Result : IResult
    {
        public bool Success { get; init; }

        public string? Message { get; init; }

        public static Result Ok(
            string? message = null)
        {
            return new Result
            {
                Success = true,
                Message = message
            };
        }

        public static Result Fail(
            string message)
        {
            return new Result
            {
                Success = false,
                Message = message
            };
        }
    }

    public sealed class Result<T> : Result
    {

        public T? Data { get; init; }

        public static Result<T> Ok(
            T data,
            string? message = null)
        {
            return new Result<T>
            {
                Success = true,
                Data = data,
                Message = message
            };
        }

        public new static Result<T> Fail(
            string message)
        {
            return new Result<T>
            {
                Success = false,
                Message = message
            };
        }

    }
}
