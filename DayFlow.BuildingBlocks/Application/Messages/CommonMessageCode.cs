namespace DayFlow.BuildingBlocks.Application.Messages
{
    public static class CommonMessageCode
    {
        /// <summary>
        /// 使用者不存在
        /// </summary>
        public const string InvalidUser =
            "COMMON_INVALID_USER";

        /// <summary>
        /// 沒有權限
        /// </summary>
        public const string Unauthorized =
            "COMMON_UNAUTHORIZED";

        /// <summary>
        /// 請求資料驗證失敗
        /// </summary>
        public const string ValidationFailed =
            "COMMON_VALIDATION_FAILED";

        /// <summary>
        /// 資源不存在
        /// </summary>
        public const string NotFound =
            "COMMON_NOT_FOUND";

        /// <summary>
        /// 未預期錯誤
        /// </summary>
        public const string InternalError =
            "COMMON_INTERNAL_ERROR";

        /// <summary>
        /// 建立成功
        /// </summary>
        public const string CreateSuccess =
            "COMMON_CREATE_SUCCESS";

        /// <summary>
        /// 修改成功
        /// </summary>
        public const string UpdateSuccess =
            "COMMON_UPDATE_SUCCESS";

        /// <summary>
        /// 刪除成功
        /// </summary>
        public const string DeleteSuccess =
            "COMMON_DELETE_SUCCESS";
    }
}
