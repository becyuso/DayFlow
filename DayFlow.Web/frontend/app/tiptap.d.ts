import { mountRichTextEditor } from "../components/tiptap/editor";
declare global {
    interface Window {
        mountRichTextEditor: typeof mountRichTextEditor;
    }
}
export declare function initTiptap(): void;
//# sourceMappingURL=tiptap.d.ts.map