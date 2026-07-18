import {
    showToast,
    type ToastType
}
    from "../components/toast/toast";

export function initToast(): void {
    const toastElement =
        document.querySelector<HTMLScriptElement>(
            "#toast-data"
        );

    if (!toastElement) {
        return;
    }


    const json =
        toastElement.textContent?.trim();


    if (!json) {
        return;
    }

    try {

        const data =
            JSON.parse(json) as ToastType;

        showToast(data);
    }
    catch (error: unknown) {
        console.error(
            "Invalid toast JSON.",
            error
        );
    }
}