import "toastr/build/toastr.min.css";
import "@/styles/toast.css";
export type ToastType = {
    type: "success" | "error" | "warning" | "info";
    message: string;
};
export declare function showToast(data: ToastType): void;
//# sourceMappingURL=toast.d.ts.map