import { showToast } from "../components/toast/toast";
export function initToast() {
    const toastElement = document.querySelector("#toast-data");
    if (!toastElement) {
        return;
    }
    const json = toastElement.textContent?.trim();
    if (!json) {
        return;
    }
    try {
        const data = JSON.parse(json);
        showToast(data);
    }
    catch (error) {
        console.error("Invalid toast JSON.", error);
    }
}
//# sourceMappingURL=toast.js.map