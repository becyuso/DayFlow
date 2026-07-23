import { Editor } from "@tiptap/core";
const editors = new Map();
export function registerEditor(id, editor) {
    editors.set(id, editor);
}
export function getEditor(id) {
    return editors.get(id);
}
export function destroyEditor(id) {
    const editor = editors.get(id);
    if (!editor)
        return;
    editor.destroy();
    editors.delete(id);
}
export function destroyAllEditors() {
    editors.forEach(editor => editor.destroy());
    editors.clear();
}
//# sourceMappingURL=registry.js.map