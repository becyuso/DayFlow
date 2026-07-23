import StarterKit from "@tiptap/starter-kit"
import Image from "@tiptap/extension-image"
import { TextStyle } from "@tiptap/extension-text-style"
import FontFamily from "@tiptap/extension-font-family"
import { FontSize } from "./font-size"
// import Link from "@tiptap/extension-link"
// import Placeholder from "@tiptap/extension-placeholder"

export const tiptapExtensions = [
    StarterKit,
    Image,
    TextStyle,
    FontFamily.configure({
        types: [
            "textStyle"
        ]
    }),
    FontSize

    // Link.configure({
    //     openOnClick: false,
    //     autolink: true,
    //     linkOnPaste: true
    // }),

    // Placeholder.configure({
    //     placeholder: "請輸入內容..."
    // })
]