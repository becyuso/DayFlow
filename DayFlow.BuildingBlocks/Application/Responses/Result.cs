namespace DayFlow.BuildingBlocks.Application.Results
{
    public class Result : IResult
    {
        public bool Success { get; init; }

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
                Success = true,
                Code = code
            };
        }

        public static Result Fail(
            string code)
        {
            return new Result
            {
                Success = false,
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
                Success = true,
                Data = data,
                Code = code
            };
        }

        public new static Result<T> Fail(
            string code)
        {
            return new Result<T>
            {
                Success = false,
                Code = code
            };
        }

    }
}
