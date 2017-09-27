({
    // Панель инструментов с заголовком
    // параметры
    // id - id уникальный id списка
    // title - заголовок
    $titleBar: function (p) {
        return {
            view: 'toolbar',
            css: 'section-bar '+m.sys.def(p.css,'section-default'),
            borderless: true,
            cols: [
                { view: 'label', label: p.title, css: 'section-title' },
            ]
        };
    }
});
