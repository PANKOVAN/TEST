({
    ///////////////////////////////////////////////////////////////////////////
    // toolBar
    // $toolbutton
    //  objId             - id источника данных
    //  data              - источник (адаптер) данных
    ///////////////////////////////////////////////////////////////////////////
    $toolBar: function (p) {
        let result = {
            view: 'toolbar',
            id: p.id,
            cols: []
        };
        //m.sys.breakpoint();
        let r = webix.ajax().sync().get("data/" + p.data, { commands: 1 }, function (text, data) {
            let commands = data.json();
            for (let i in commands) {
                try {
                    result.cols.push(m.macro.run('$toolButton', { name: '$' + m.sys.webixName(commands[i]), params: p }));
                }
                catch (e) {
                    m.log.add({ type: 'error', message: "Ошибки при вычислении макро $toolBar  (" + e.message + ") ..." });
                    return "";
                }
            }
        }
        );

        return result;
    }
});
