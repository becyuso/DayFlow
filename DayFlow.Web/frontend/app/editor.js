import { createEditor } from '@/editors/tiptap';
import '@/styles/editor.css';
document.addEventListener("DOMContentLoaded", () => {
    const element = document.querySelector("#editor");
    if (!element) {
        return;
    }
    createEditor(element);
});
//# sourceMappingURL=editor.js.map