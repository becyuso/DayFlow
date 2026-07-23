import {
    mountRichTextEditor
}
    from "../components/tiptap/editor"

import {
    destroyAllEditors
}
    from "../editors/tiptap/registry"

declare global {
    interface Window {
        mountRichTextEditor:
        typeof mountRichTextEditor
    }
}

let initialized = false

export function initTiptap() {

    if (initialized)
        return

    initialized = true

    window.mountRichTextEditor =
        mountRichTextEditor

    window.addEventListener(
        "beforeunload",
        () => {

            destroyAllEditors()

        }
    )

    window.dispatchEvent(
        new Event(
            "tiptap-ready"
        )
    )

    console.log(
        "Tiptap initialized"
    )
}