export function insertImage(editor, src) {
    editor
        .chain()
        .focus()
        .setImage({
        src
    })
        .run();
}
//# sourceMappingURL=image.js.map