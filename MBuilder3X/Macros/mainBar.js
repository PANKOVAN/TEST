({
    ///////////////////////////////////////////////////////////////////////////
    // mainBar      - главное меню
    //  logotext          - текст логотипа
    //  logoimg           - картинка логотипа
    //  commands          - массив команд (макро $mainButton) 
    ///////////////////////////////////////////////////////////////////////////
    $mainBar: function (p) {
        let result = {
            css: 'main-bar',
            height: 48,
            id: 'mainbar',
            cols: []
        };
        result.cols.push({ view: "label", label: 'MBuilder 3.0', css: 'logo', width: 200 });
        if (p.commands) {
            for (let i in p.commands) {
                try {
                    result.cols.push(p.commands[i]);
                }
                catch (e) {
                    m.log.add({ type: 'error', message: "Ошибки при вычислении макро $mainBar  (" + e.message + ") ..." });
                    return "";
                }
            }
        }
        result.cols.push({ id: 'mainlastindex', gravity: 20 });
        result.cols.push({ view: "button", type: 'icon', icon: 'question', css: 'main-but-help', align: 'right', width: 48, click: function () { m.main.runCommand('main-help') } });
        result.cols.push({ view: "button", type: 'icon', icon: 'times', css: 'main-but-close', align: 'right', width: 48, click: function () { m.main.removeTab() } });

        return result;
    }
});
