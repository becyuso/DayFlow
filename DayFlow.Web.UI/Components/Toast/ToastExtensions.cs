using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Text.Json;

namespace DayFlow.Web.UI.Components.Toast
{
    public static class ToastExtensions
    {
        public static void ToastSuccess(this ITempDataDictionary tempData, string message) =>
            SetToast(tempData, ToastType.Success, message);

        public static void ToastSuccess(this ITempDataDictionary tempData, string title, string message) =>
            SetToast(tempData, ToastType.Success, title, message);

        public static void ToastWarning(this ITempDataDictionary tempData, string message) =>
            SetToast(tempData, ToastType.Warning, message);

        public static void ToastWarning(this ITempDataDictionary tempData, string title, string message) =>
            SetToast(tempData, ToastType.Warning, title, message);

        public static void ToastInfo(this ITempDataDictionary tempData, string message) =>
            SetToast(tempData, ToastType.Info, message);

        public static void ToastInfo(this ITempDataDictionary tempData, string title, string message) =>
            SetToast(tempData, ToastType.Info, title, message);

        public static void ToastError(this ITempDataDictionary tempData, string message) =>
            SetToast(tempData, ToastType.Error, message);

        public static void ToastError(this ITempDataDictionary tempData, string title, string message) =>
            SetToast(tempData, ToastType.Error, title, message);

        private static void SetToast(ITempDataDictionary tempData, ToastType toastType, string message) =>
            SetToast(tempData, toastType, default, message);

        private static void SetToast(ITempDataDictionary tempData, ToastType toastType, string? title, string message)
        {
            tempData["Toast"] = JsonSerializer.Serialize(new ToastViewModel
            {
                Type = toastType.ToString().ToLower(),
                Title = title,
                Message = message
            });
        }
    }
}
