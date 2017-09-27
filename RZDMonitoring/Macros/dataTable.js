({
    ///////////////////////////////////////////////////////////////////////////
    // Простая DATATABLE
    // $dataTable
    //  id              - id
    //  data            - источник данных
    //  className       - имя класса
    //  columns         - список колонок
    ///////////////////////////////////////////////////////////////////////////
    $dataTable: function (p) {
        return {
            id: p.id,
            view: "datatable",
            select: "row",
            multiselect: true,
            editable: true,
            editaction: "dblclick",
            scroll: true,
            $columns: {},
            $url: {}
        }
    }
});
