namespace DayFlow.BuildingBlocks.Application.Results
{
    public class Result : IResult
    {
        public bool IsSuccess { get; init; }

        public string? Code { get; init; }

        public string? Message { get; private set; }

        public void SetMessage(string message)
        {
            Message = message;
        }
        public static Result Ok()
        {
            return Ok(
                default);
        }

        public static Result Ok(
            string? code)
        {
            return new Result
            {
                IsSuccess = true,
                Code = code
            };
        }

        public static Result Fail(
            string code)
        {
            return new Result
            {
                IsSuccess = false,
                Code = code
            };
        }
    }

    public sealed class Result<T> : Result
    {
        public T? Data { get; init; }

        public static Result<T> Ok(
            T data)
        {
            return Ok(
                default,
                data);
        }

        public static Result<T> Ok(
            string? code,
            T data)
        {
            return new Result<T>
            {
                IsSuccess = true,
                Data = data,
                Code = code
            };
        }

        public new static Result<T> Fail(
            string code)
        {
            return new Result<T>
            {
                IsSuccess = false,
                Code = code
            };
        }

    }
}
