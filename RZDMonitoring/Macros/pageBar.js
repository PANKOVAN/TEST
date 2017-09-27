({
    // Панель инструментов с заголовком
    // параметры
    // id - id уникальный id списка
    // title - заголовок
    $pageBar: function (p) {
        let command = m.macro.getCommand(p.name);
        if (!command) command = { command: command, label: command };
        return {
            view: 'toolbar',
            css: 'page-title',
            borderless: true,
            cols: [
                { view: 'button', type: 'image', image: 'images/main/icons8-' + m.sys.def(p.img, command.icon) + '.png', css: 'page-but', width: 52 },
                { view: 'label', label: p.title || command.label},

                { $content: { $iconButton: { command: '$json' }, align: 'right', '?': true } },
                { $content: { $iconButton: { command: '$help' }, align: 'right', '?': true } },
                { $content: { $iconButton: { command: '$close' }, align: 'right', '?': true } }
            ]
        };
    }
});
