import {
    Editor
}
    from "@tiptap/core"

import {
    tiptapExtensions
}
    from "./extensions"

export function createTiptapEditor(

    element: HTMLElement,

    content = ""

) {

    return new Editor({

        element,

        extensions:
            tiptapExtensions,

        content,

        editorProps: {
            attributes: {
                class:
                    "tiptap-content"
            }
        }
    })
}