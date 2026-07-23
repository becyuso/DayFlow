import {
    Editor
}
    from "@tiptap/core"



const editors =
    new Map<string, Editor>()



export function registerEditor(

    id: string,

    editor: Editor

) {

    editors.set(
        id,
        editor
    )

}



export function getEditor(
    id: string
) {

    return editors.get(id)

}




export function destroyEditor(
    id: string
) {

    const editor =
        editors.get(id)


    if (!editor)
        return



    editor.destroy()


    editors.delete(id)

}




export function destroyAllEditors() {

    editors.forEach(
        editor =>
            editor.destroy()
    )


    editors.clear()

}