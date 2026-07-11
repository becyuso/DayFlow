import { Editor } from '@tiptap/core'
import StarterKit from '@tiptap/starter-kit'

export function createEditor(
    element:HTMLElement
)
{

    return new Editor({
        element,

        extensions:[
            StarterKit
        ],

        content:`
            <h2>
                Hello Tiptap
            </h2>

            <p>
                Enterprise Editor
            </p>
        `
    })
}