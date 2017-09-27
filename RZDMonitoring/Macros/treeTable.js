({
    ///////////////////////////////////////////////////////////////////////////
    // Простая treetable
    // $treeTable
    //  id              - id
    //  data            - источник данных
    //  className       - имя класса
    //  columns         - список колонок
    ///////////////////////////////////////////////////////////////////////////
    $treeTable: function (p) {
        return {
            id: p.id,
            view: "treetable",
            select: "row",
            multiselect: true,
            editable: true,
            editaction: "dblclick",
            $columns: {},
            $url: {}
            //on: { "data->onParse": function (driver, data) { m.data.parseTree(data); } }
        }
    }
});
