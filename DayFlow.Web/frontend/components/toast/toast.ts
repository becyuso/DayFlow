import toastr from "toastr";

import "toastr/build/toastr.min.css";
import "@/styles/toast.css";

export type ToastType =
    {
        type:
        | "success"
        | "error"
        | "warning"
        | "info";

        message: string;
    };

export function showToast(
    data: ToastType
) {

    toastr.options =
    {
        closeButton: true,

        progressBar: true,

        positionClass:
            "toast-top-right",

        timeOut: 5000,

        extendedTimeOut: 1000,

        showDuration: 300,

        hideDuration: 300,

        showMethod: "slideDown",

        hideMethod: "slideUp"
    };

    switch (data.type) {

        case "success":
            toastr.success(
                data.message,
                "成功"
            );
            break;

        case "error":
            toastr.error(
                data.message,
                "錯誤"
            );
            break;

        case "warning":
            toastr.warning(
                data.message,
                "警告"
            );
            break;

        default:
            toastr.info(
                data.message,
                "通知"
            );
    }
}