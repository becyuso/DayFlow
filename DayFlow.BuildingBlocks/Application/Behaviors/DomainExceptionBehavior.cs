using DayFlow.BuildingBlocks.Application.Results;
using DayFlow.BuildingBlocks.Domain.Exceptions;
using MediatR;

namespace DayFlow.BuildingBlocks.Application.Behaviors
{
    /// <summary>
    /// 攔截 Domain 層拋出的業務規則例外(DomainException),
    /// 轉換成 Result / Result&lt;T&gt; 的失敗結果,
    /// 讓 Controller 端可以維持"檢查 result.Success"的一致寫法,
    /// 不需要額外用 try-catch 包住每一個 MediatR.Send() 呼叫。
    /// </summary>
    public sealed class DomainExceptionBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (DomainException ex) when (TryCreateFailureResult(ex, out var failureResult))
            {
                return failureResult!;
            }
        }

        /// <summary>
        /// 只有當 TResponse 本身是 Result 或 Result&lt;T&gt; 時,才把例外"吞掉"轉成失敗結果;
        /// 如果某個 Command/Query 的回傳型別不是 Result 家族(例如就是回傳 bool 或自訂型別),
        /// 這裡會回傳 false,讓例外正常往外拋,不會被靜默吞掉,避免掩蓋潛在的設計問題。
        /// </summary>
        private static bool TryCreateFailureResult(DomainException ex, out TResponse? result)
        {
            var responseType = typeof(TResponse);

            if (responseType == typeof(Result))
            {
                result = (TResponse)(object)Result.Fail(ex.Message);
                return true;
            }

            if (responseType.IsGenericType &&
                responseType.GetGenericTypeDefinition() == typeof(Result<>))
            {
                var failureMethod = responseType.GetMethod(
                    nameof(Result<object>.Fail),
                    new[] { typeof(string) });

                result = (TResponse)failureMethod!.Invoke(null, new object[] { ex.Message })!;
                return true;
            }

            result = default;
            return false;
        }
    }
}