({
    // Список с заголовком и кнопкой "перечитать"
    // параметры
    // id - id уникальный id списка
    // title - заголовок
    // url - url контроллера для начитки
    // template - шаблон элемента списка
    // на элементах списка могут с помощью {common.pinBox()} могут располагаться элементы для прикнопливания элементов списка
    $titleTreeTable: function (p) {
        let r = {
            paddingX: 4,
            paddingY: 4,
            rows: [
            ]
        };
        if (p.title) r.rows.push({ $titleBar: {} });
        r.rows.push(
            {
                $treeTable: {}
            });
        return r;
    }
});
