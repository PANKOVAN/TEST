({
    ///////////////////////////////////////////////////////////////////////////
    // Простая tree
    // $tree
    //  id              - id
    //  data            - источник данных
    //  className       - имя класса
    //  columns         - список колонок
    ///////////////////////////////////////////////////////////////////////////
    $tree: function (p) {
        return {
            id: p.id,
            view: "tree",
            select: true,
            borderless: true,
            //editable: true,
            //editaction: "dblclick",
            type: "lineTree",
            template: p.template,
            $url: {},
            //on: { "data->onParse": function (driver, data) { m.data.parseTree(data); } }
        }
    }
});
