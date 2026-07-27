namespace DayFlow.BuildingBlocks.Application.Messages
{
    public sealed class CommonMessageRegistrar
        : IMessageRegistrar
    {
        public void Register(
               IMessageRegistry registry)
        {
            registry.Add(
                CommonMessageCode.InvalidUser,
                "無效用戶");

            registry.Add(
                CommonMessageCode.Unauthorized,
                "您沒有權限執行此操作");

            registry.Add(
                CommonMessageCode.ValidationFailed,
                "資料驗證失敗");

            registry.Add(
                CommonMessageCode.NotFound,
                "找不到資料");

            registry.Add(
                CommonMessageCode.InternalError,
                "系統錯誤，請稍後再試");

            registry.Add(
                CommonMessageCode.CreateSuccess,
                "建立成功");

            registry.Add(
                CommonMessageCode.UpdateSuccess,
                "更新成功");

            registry.Add(
                CommonMessageCode.DeleteSuccess,
                "刪除成功");
        }

    }
}
