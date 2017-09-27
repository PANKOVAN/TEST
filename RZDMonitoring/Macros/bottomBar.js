({
    // Панель инструментов с заголовком
    // параметры
    // id - id уникальный id списка
    // title - заголовок
    $bottomBar: function (p) {
        //m.sys.bp();
        if (!p) p = {};
        let r = {
            view: 'toolbar',
            css: 'bottom-bar',
            borderless: true,
            cols: [
                { gravity: 10 }
            ]
        };
        if (!p.commands) p.commands = ['$ok', '$cancel'];
        for (let i in p.commands) {
            let json = m.macro.run('$button', { name: p.commands[i] });
            if (!m.sys.isEmpty(json)) {
                json.align = 'right';
                json.minWidth = '100'
                r.cols.push(json);
            }
        }
        return r;
    }
});
