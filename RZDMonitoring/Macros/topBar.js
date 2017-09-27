({
    // Панель инструментов с заголовком
    // параметры
    // id - id уникальный id списка
    // title - заголовок
    $topBar: function (p) {
        //m.bp();
        if (!p) p = {};
        let r = {
            view: 'toolbar',
            css: 'top-bar',
            borderless: true,
            cols: [
            ]
        };
        if (!p.commands) p.commands = [];
        for (let i in p.commands) {
            let json = m.macro.run('$iconButton', { command: p.commands[i] });
            if (!m.sys.isEmpty(json)) {
                //json.minWidth = '100'
                r.cols.push(json);
            }
        }
        return r;
    }
});
