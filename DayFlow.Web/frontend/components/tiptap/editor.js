import "../../styles/tiptap.css";
import { createTiptapEditor } from "../../editors/tiptap/index";
import { createToolbar } from "./toolbar";
import { registerEditor, destroyEditor } from "../../editors/tiptap/registry";
export function mountRichTextEditor(id, value) {
    console.log("mountRichTextEditor", id, value);
    destroyEditor(id);
    const element = document.getElementById(id);
    console.log("element", element);
    if (!element) {
        console.error(`Editor '${id}' not found`);
        return;
    }
    const editor = createTiptapEditor(element, value);
    registerEditor(id, editor);
    const toolbar = document.getElementById(`${id}_toolbar`);
    if (toolbar) {
        createToolbar(toolbar, editor);
    }
    editor.on("update", () => {
        const input = document.getElementById(`${id}_value`);
        if (!input)
            return;
        input.value =
            editor.getHTML();
        input.dispatchEvent(new Event("input", {
            bubbles: true
        }));
        input.dispatchEvent(new Event("change", {
            bubbles: true
        }));
    });
    return editor;
}
//# sourceMappingURL=editor.js.map