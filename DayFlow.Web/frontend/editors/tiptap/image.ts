import type { Editor }
    from "@tiptap/core"

export function insertImage(
    editor: Editor,
    src: string
) {

    editor
        .chain()
        .focus()
        .setImage({
            src
        })
        .run()
}