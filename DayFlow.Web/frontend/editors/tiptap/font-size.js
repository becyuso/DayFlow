import { Extension } from "@tiptap/core";
import { TextStyle } from "@tiptap/extension-text-style";
export const FontSize = TextStyle.extend({
    addAttributes() {
        return {
            fontSize: {
                default: null,
                parseHTML(element) {
                    return element.style.fontSize;
                },
                renderHTML(attributes) {
                    if (!attributes.fontSize)
                        return {};
                    return {
                        style: `font-size:${attributes.fontSize}`
                    };
                }
            }
        };
    }
});
//# sourceMappingURL=font-size.js.map