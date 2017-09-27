({
    // Список с заголовком и кнопкой "перечитать"
    // параметры
    // id - id уникальный id списка
    // title - заголовок
    // url - url контроллера для начитки
    // template - шаблон элемента списка
    // на элементах списка могут с помощью {common.pinBox()} могут располагаться элементы для прикнопливания элементов списка
    $titleList: function (p) {
        let hasPin = m.sys.def(p.hasPin, false);
        let r = {
            paddingX: 4,
            paddingY: 4,
            rows: [
            ]
        };
        if (p.title) r.rows.push({ $titleBar: {} });
          
        if (!p.on) p.on = {};
        if (!p.on.oAfterLoad) p.on.onAfterLoad = function () {
            let id = this.getFirstId();
            if (id) this.select(id);
        };
          
        r.rows.push(
            {
                onCommandPin: "m.commands.run('" + p.id + "', 'data/main/pin', null, null, p);",
                $list: {
                    id: p.id,
                    data: p.url,
                    template: p.template,
                    masterData: p.masterData,
                }
            }
        );
        return r;
    }
});
