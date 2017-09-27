({
    // $appData     - загрузка данных (используется для дальнейшей интерпретации json webix)
    // $data
    //  url   - url
    $appData: function (p) {
        let json = m.main.getAppJson(p.name, {}, p.folder);
        return { data: json };
    }
});
