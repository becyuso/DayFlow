import type {
    Editor
}
    from "@tiptap/core"



export function createToolbar(
    element: HTMLElement,
    editor: Editor
) {


    element.innerHTML = `

<div class="tiptap-toolbar">


    <!-- Font -->

    <select
        class="tiptap-select"
        data-command="font">

        <option value="">
            字型
        </option>

        <option value="Arial">
            Arial
        </option>

        <option value="Microsoft JhengHei">
            微軟正黑體
        </option>

        <option value="Noto Sans TC">
            Noto Sans TC
        </option>

        <option value="serif">
            Serif
        </option>

    </select>



    <select
        class="tiptap-select"
        data-command="size">

        <option value="">
            大小
        </option>

        <option value="12px">
            12
        </option>

        <option value="14px">
            14
        </option>

        <option value="16px">
            16
        </option>

        <option value="18px">
            18
        </option>

        <option value="24px">
            24
        </option>

        <option value="32px">
            32
        </option>

    </select>



    <div class="tiptap-separator"></div>



    <!-- Text -->


    <button
        type="button"
        class="tiptap-tool"
        data-command="bold"
        title="粗體">

        <strong>B</strong>

    </button>



    <button
        type="button"
        class="tiptap-tool"
        data-command="italic"
        title="斜體">

        <em>I</em>

    </button>



    <button
        type="button"
        class="tiptap-tool"
        data-command="strike"
        title="刪除線">

        <s>S</s>

    </button>



    <div class="tiptap-separator"></div>



    <!-- Heading -->


    <button
        type="button"
        class="tiptap-tool"
        data-command="h1">

        H1

    </button>



    <button
        type="button"
        class="tiptap-tool"
        data-command="h2">

        H2

    </button>



    <button
        type="button"
        class="tiptap-tool"
        data-command="h3">

        H3

    </button>



    <div class="tiptap-separator"></div>



    <!-- List -->


    <button
        type="button"
        class="tiptap-tool"
        data-command="bullet">

        • List

    </button>



    <button
        type="button"
        class="tiptap-tool"
        data-command="ordered">

        1. List

    </button>



    <button
        type="button"
        class="tiptap-tool"
        data-command="quote">

        ❝

    </button>



    <div class="tiptap-separator"></div>



    <!-- Code -->


    <button
        type="button"
        class="tiptap-tool"
        data-command="code">

        &lt;/&gt;

    </button>



    <button
        type="button"
        class="tiptap-tool"
        data-command="codeblock">

        Code

    </button>



    <button
        type="button"
        class="tiptap-tool"
        data-command="hr">

        ―

    </button>



    <div class="tiptap-separator"></div>



    <!-- Media -->


    <button
        type="button"
        class="tiptap-tool"
        data-command="image">

        🖼

    </button>



    <div class="tiptap-separator"></div>



    <!-- History -->


    <button
        type="button"
        class="tiptap-tool"
        data-command="undo">

        ↶

    </button>



    <button
        type="button"
        class="tiptap-tool"
        data-command="redo">

        ↷

    </button>



</div>

`



    // Button click

    element.onclick =
        (e) => {


            const button =
                (e.target as HTMLElement)
                    .closest("button")



            if (!button)
                return



            executeCommand(
                button.dataset.command,
                editor
            )

        }



    // Select change

    element.onchange =
        (e) => {


            const select =
                e.target as HTMLSelectElement



            const command =
                select.dataset.command



            if (command === "font") {


                editor
                    .chain()
                    .focus()
                    .setFontFamily(
                        select.value
                    )
                    .run()


            }



            if (command === "size") {


                editor
                    .chain()
                    .focus()
                    .setMark(
                        "textStyle",
                        {

                            fontSize:
                                select.value

                        }
                    )
                    .run()


            }

        }


}




function executeCommand(

    command: string | undefined,

    editor: Editor

) {


    switch (command) {



        case "bold":

            editor.chain()
                .focus()
                .toggleBold()
                .run()

            break



        case "italic":

            editor.chain()
                .focus()
                .toggleItalic()
                .run()

            break



        case "strike":

            editor.chain()
                .focus()
                .toggleStrike()
                .run()

            break



        case "h1":

            editor.chain()
                .focus()
                .toggleHeading({
                    level: 1
                })
                .run()

            break



        case "h2":

            editor.chain()
                .focus()
                .toggleHeading({
                    level: 2
                })
                .run()

            break



        case "h3":

            editor.chain()
                .focus()
                .toggleHeading({
                    level: 3
                })
                .run()

            break



        case "bullet":

            editor.chain()
                .focus()
                .toggleBulletList()
                .run()

            break



        case "ordered":

            editor.chain()
                .focus()
                .toggleOrderedList()
                .run()

            break



        case "quote":

            editor.chain()
                .focus()
                .toggleBlockquote()
                .run()

            break



        case "code":

            editor.chain()
                .focus()
                .toggleCode()
                .run()

            break



        case "codeblock":

            editor.chain()
                .focus()
                .toggleCodeBlock()
                .run()

            break



        case "hr":

            editor.chain()
                .focus()
                .setHorizontalRule()
                .run()

            break



        case "image":


            const url =
                window.prompt(
                    "圖片網址"
                )


            if (url) {


                editor.chain()
                    .focus()
                    .setImage({
                        src: url
                    })
                    .run()


            }


            break



        case "undo":

            editor.chain()
                .focus()
                .undo()
                .run()

            break



        case "redo":

            editor.chain()
                .focus()
                .redo()
                .run()

            break

    }

}