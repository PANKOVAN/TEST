({
    ///////////////////////////////////////////////////////////////////////////
    // Простая tree
    // $tree
    //  id              - id
    //  data            - источник данных
    //  className       - имя класса
    //  columns         - список колонок
    ///////////////////////////////////////////////////////////////////////////
    $groupList: function (p) {
        return {
            id: p.id,
            view: "grouplist",
            select: true,
            //editable: true,
            //editaction: "dblclick",
            type: "lineTree",
            template: p.template,
            $url: {}
            //on: { "data->onParse": function (driver, data) { m.data.parseTree(data); } }
        }
    }
});
