import { createEditor } from '@/editors/tiptap'
import '@/styles/editor.css'


document.addEventListener(
    "DOMContentLoaded",
    ()=>{
        const element = document.querySelector<HTMLElement>("#editor")

        if(!element)
        {
            return
        }

        createEditor(element)
    }
)